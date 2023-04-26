using KCI_Library;
using KCI_Library.DataAccess;
using KCI_Library.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

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
        private BackgroundWorker worker;

        public MainForm()
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {

            loadingPanel.BringToFront();
            worker.RunWorkerAsync();
            while (worker.IsBusy)
                await Task.Delay(100);
            ShowActivationButton();
            ShowAvailableLicenses();
        }


        #region Métodos
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            ProgressReportModel progressReport = new();

            worker.ReportProgress(0, progressReport.Set("Creando modelo del producto instalado", 0));
            kaspersky = Dependencies.CreateKasperskyModel();

            worker.ReportProgress(10, progressReport.Set("Evaluando requisitos de las funciones adicionales", 10));
            requirementsForm = new RequirementsForm(kaspersky.Id);

            worker.ReportProgress(50, progressReport.Set("Obteniendo licencias de activación", 50));
            availableLicenses = SqlConnector.GetAvailableLicenses().Result;

            worker.ReportProgress(99, progressReport.Set("Recuperando configuración", 99));
            configurationForm = new(kaspersky.Installed, requirementsForm.Requirements.DatabaseAccesible);

            worker.ReportProgress(100, progressReport.Set("Dependencias generadas con éxito", 100));
            e.Result = "Dependencias generadas con éxito";
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Debug.Print(e.ProgressPercentage + "%");
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadingPanel.SendToBack();
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
