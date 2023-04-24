using KCI_Library;
using KCI_Library.DataAccess;
using KCI_Library.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

// TODO - (!) Error handling.
namespace KCI_UI
{
#pragma warning disable IDE1006 // Estilos de nombres
    public partial class MainForm : Form
    {
        private KasperskyModel kaspersky;
        private RequirementsForm requirementsForm;
        /// <summary>
        /// Pares clave-valor.
        /// <para>
        /// Clave: Id del producto con licencias disponibles. <br/>
        /// Valor: Última fecha de actualización de las licencias.
        /// </para>
        /// </summary>
        private Dictionary<DatabaseId, string> availableLicenses;
        private ConfigurationForm configurationForm;
        private TaskHandler obtainDependenciesTaskHandler;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            obtainDependenciesTaskHandler = new(ObtainDependecies);
            obtainDependenciesTaskHandler.TaskStarted += obtainDependecies_Started;
            obtainDependenciesTaskHandler.ProgressChanged += obtainDependecies_UpdateProgress;
            obtainDependenciesTaskHandler.TaskCompleted += obtainDependecies_Completed;

            await obtainDependenciesTaskHandler.RunAsync();
            MessageBox.Show(configurationForm.ProductToInstall.ToString());
            ShowActivationButton();
            ShowAvailableLicenses();

            requirementsForm.ShowDialog(this);
        }


        #region Métodos
        private void ObtainDependecies(IProgress<double> progress, CancellationToken cancellation)
        {
            progress.Report(0);

            kaspersky = Dependencies.CreateKasperskyModel();
            progress.Report(10);

            requirementsForm = new RequirementsForm(kaspersky.Id);
            progress.Report(50);

            availableLicenses = SqlConnector.GetAvailableLicenses().Result;
            progress.Report(99);

            configurationForm = new(kaspersky.Installed, requirementsForm.Requirements.DatabaseAccesible);
            progress.Report(100);
        }

        /// <summary>
        /// Resaltar el producto instalado y mostrar su botón de activación.
        /// </summary>
        private void ShowActivationButton()
        {
            Color color = Color.LightGoldenrodYellow;

            switch (kaspersky.Id)
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
            foreach (DatabaseId id in availableLicenses.Keys)
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
                    toolTip.SetToolTip(label, "Actualizadas el " + availableLicenses[id]);
                }
            }
        }

        private void EnableInstallationButtons()
        {
            if (!defaultInstallationButton.Enabled)
                defaultInstallationButton.Enabled = true;

            if (!autoInstallationButton.Enabled && requirementsForm.Requirements.AllMet)
                autoInstallationButton.Enabled = true;
        }
        #endregion

        #region Eventos
        private void obtainDependecies_Started(object sender, EventArgs e)
        {
            loadingPanel.Invoke(new Action(() => loadingPanel.BringToFront()));
        }

        private void obtainDependecies_UpdateProgress(object sender, double e)
        {
            loadingLabel.Invoke(new Action(() => loadingLabel.Text = "Iniciando..." + e + "%"));
        }

        private void obtainDependecies_Completed(object sender, EventArgs e)
        {
            loadingPanel.Invoke(new Action(() => loadingPanel.SendToBack()));
        }

        private void configurationButton_Click(object sender, EventArgs e)
        {
            configurationForm.ShowDialog(this);
        }

        private void githubButton_Click(object sender, EventArgs e) =>
            ProcessExecutor.BrowseToUrl("https://github.com/bitasuperactive/KasperskyCustomInstaller2022");

        private void productComparisionButton_Click(object sender, EventArgs e) =>
            ProcessExecutor.BrowseToUrl("https://www.kaspersky.es/home-security");

        private void kavRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            configurationForm.ProductToInstall = DatabaseId.kav;
            EnableInstallationButtons();
        }

        private void kisRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            configurationForm.ProductToInstall = DatabaseId.kis;
            EnableInstallationButtons();
        }

        private void ktsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            configurationForm.ProductToInstall = DatabaseId.kts;
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
            if (requirementsForm.Requirements.AllMet)
            {
                // TODO - Realizar instalación automática.
                throw new NotImplementedException();
            }
            else
            {
                requirementsForm.ShowDialog(this);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
