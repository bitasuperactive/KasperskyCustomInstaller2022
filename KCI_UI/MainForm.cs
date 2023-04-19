using KCI_Library;
using KCI_Library.DataAccess;
using KCI_Library.Models;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.PerformanceData;
using System.Drawing;

// TODO - (!) Error handling.
namespace KCI_UI
{
#pragma warning disable IDE1006 // Estilos de nombres
    public partial class MainForm : Form
    {
        public KasperskyModel Kaspersky { get; private set; }
        public AutoInstallRequirementsModel AutoInstallRequirements { get; set; }
        /// <summary>
        /// Par clave-valor.
        /// <para>
        /// Clave: Id del producto con licencias disponibles. <br/>
        /// Valor: Última fecha de actualización de las licencias.
        /// </para>
        /// </summary>
        public Dictionary<DatabaseId, string> AvailableLicenses { get; private set; }
        public ConfigurationModel Configuration { get; private set; }

        private BackgroundWorker backgroundWorker;
        private TaskCompletionSource<bool> taskCompletionSource;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            backgroundWorker = new();
            backgroundWorker.DoWork += DoBackgroundWork;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += UpdateProgress;
            taskCompletionSource = new();
            backgroundWorker.RunWorkerCompleted += WorkerCompleted;

            loadingPanel.BringToFront();
            backgroundWorker.RunWorkerAsync();
            await taskCompletionSource.Task;
            ShowActivationButton();
            ShowAvailableLicenses();
            loadingPanel.SendToBack();
        }

        #region Métodos
        private async void DoBackgroundWork(object sender, DoWorkEventArgs e) //object sender, DoWorkEventArgs e
        {
            backgroundWorker.ReportProgress(0);
            Kaspersky = Dependencies.CreateKasperskyModel();

            backgroundWorker.ReportProgress(25);
            AutoInstallRequirements = Dependencies.CreateAutoInstallRequirementsModel();

            backgroundWorker.ReportProgress(50);
            AvailableLicenses = await SqlConnector.GetAvailableLicenses();

            backgroundWorker.ReportProgress(99);
            ConfigurationModel previousConfiguration = new(Properties.Settings.Default.KeepKasperskyConfig,
                Properties.Settings.Default.OfflineSetup,
                Properties.Settings.Default.DoNotUseDatabaseLicenses,
                Properties.Settings.Default.KasperskySecureConnection);
            Configuration = previousConfiguration.ValidateConfiguration(Kaspersky.Installed, AutoInstallRequirements.DatabaseAccesible);

            backgroundWorker.ReportProgress(100);
        }
        private void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
            loadingLabel.Text = "Iniciando..." + e.ProgressPercentage + "%";
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            taskCompletionSource.SetResult(true);
        }

        /// <summary>
        /// Resaltar el producto instalado y mostrar su botón de activación.
        /// </summary>
        private void ShowActivationButton()
        {
            Color color = Color.LightGoldenrodYellow;

            switch (Kaspersky.Id)
            {
                case DatabaseId.kav:
                    kavRadioButton.BackColor = color;
                    kavActivationButton.Visible = true;
                    break;
                case DatabaseId.kis:
                    kisRadioButton.BackColor = color;
                    kisActivationButton.Visible = true;
                    break;
                case DatabaseId.kts:
                    ktsRadioButton.BackColor = color;
                    ktsActivationButton.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// Mostrar si hay licencias disponibles para cada versión de Kaspersky y 
        /// la fecha de la última actualización de las mismas.
        /// </summary>
        private void ShowAvailableLicenses()
        {
            foreach (DatabaseId id in AvailableLicenses.Keys)
            {
                switch (id)
                {
                    case DatabaseId.kav:
                        Show(kavAvailableLicensesLabel);
                        kavActivationButton.Enabled = true;
                        break;
                    case DatabaseId.kis:
                        Show(kisAvailableLicensesLabel);
                        kisActivationButton.Enabled = true;
                        break;
                    case DatabaseId.kts:
                        Show(ktsAvailableLicensesLabel);
                        ktsActivationButton.Enabled = true;
                        break;
                }

                void Show(Label label)
                {
                    label.Text = "Licencias disponibles";
                    label.ForeColor = Color.Green;
                    toolTip.SetToolTip(label, "Actualizadas el " + AvailableLicenses[id]);
                }
            }
        }

        private void EnableInstallationButtons()
        {
            if (!defaultInstallationButton.Enabled)
                defaultInstallationButton.Enabled = true;

            if (!autoInstallationButton.Enabled && AutoInstallRequirements.AllMet)
                autoInstallationButton.Enabled = true;
        }
        #endregion

        #region Eventos
        private void configurationButton_Click(object sender, EventArgs e)
        {
            ConfigurationForm form = new(Configuration, Kaspersky.Installed, AutoInstallRequirements.DatabaseAccesible);
            DialogResult = form.ShowDialog(this);

            if (DialogResult == DialogResult.OK)
                Configuration = form.Configuration;
        }

        private void githubButton_Click(object sender, EventArgs e) =>
            ProcessExecutor.BrowseToUrl("https://github.com/bitasuperactive/KasperskyCustomInstaller2022");

        private void productComparisionButton_Click(object sender, EventArgs e) =>
            ProcessExecutor.BrowseToUrl("https://www.kaspersky.es/home-security");

        private void kavRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.ProductToInstall = DatabaseId.kav;
            EnableInstallationButtons();
        }

        private void kisRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.ProductToInstall = DatabaseId.kis;
            EnableInstallationButtons();
        }

        private void ktsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.ProductToInstall = DatabaseId.kts;
            EnableInstallationButtons();
        }

        // TODO - Manejar adecuadamente el progreso de los procesos de la instalación.
        private void defaultInstallationButton_Click(object sender, EventArgs e)
        {
            // TODO - Realizar instalación habitual.
            throw new NotImplementedException();
        }

        private void autoInstallationButton_Click(object sender, EventArgs e)
        {
            if (AutoInstallRequirements.AllMet)
            {
                // TODO - Realizar instalación automática.
                throw new NotImplementedException();
            }
            else
            {
                new RequirementsForm().ShowDialog(this);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
