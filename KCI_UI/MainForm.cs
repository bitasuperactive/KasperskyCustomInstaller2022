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
        private Dictionary<ProductId, string> availableLicenses;
        private ConfigurationForm configurationForm;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            loadingPanel.BringToFront();
            Progress<ProgressReportModel> progress = new();
            progress.ProgressChanged += obtainDependecies_UpdateProgress;
            await Task.Run(() => ObtainDependecies(progress));
            loadingPanel.SendToBack();
            ShowActivationButton();
            ShowAvailableLicenses();
        }


        #region Métodos
        private Task ObtainDependecies(IProgress<ProgressReportModel> progress)
        {
            // TODO - (!!!) Mantener dependencias tras el reinicio como administrador.

            progress.Report(new(0, "Creando modelo del producto instalado"));
            kaspersky = Dependencies.CreateKasperskyModel();

            ProgressReportModel progressReport = new(15, "Evaluando requisitos de las funciones adicionales");

            Progress<ProgressReportModel> requirementsProgress = new((report) =>
            {
                progress.Report(new(progressReport.Percentage + (int)(report.Percentage * .62), progressReport.Description + ":\n" + report.Description));
            });

            progress.Report(progressReport);
            requirementsForm = new RequirementsForm(kaspersky.Id, requirementsProgress);

            progress.Report(new(77, "Obteniendo licencias de activación"));
            availableLicenses = SqlConnector.GetAvailableLicenses().Result;

            progress.Report(new(99, "Recuperando configuración"));
            configurationForm = new ConfigurationForm(kaspersky.Installed, requirementsForm.Requirements.DatabaseAccesible);

            progress.Report(new(100, "Dependencias generadas con éxito"));
            return Task.FromResult(true);
        }

        private void obtainDependecies_UpdateProgress(object sender, ProgressReportModel e)
        {
            loadingLabel.Text = e.Description + "..." + e.Percentage + "%";
        }

        /// <summary>
        /// Resaltar el producto instalado y mostrar su botón de activación.
        /// </summary>
        private void ShowActivationButton()
        {
            Color color = Color.LightGoldenrodYellow;

            switch (kaspersky.Id)
            {
                case ProductId.KAV:
                    kavRadioButton.BackColor = color;
                    kavActivationButton.Visible = true;
                    break;
                case ProductId.KIS:
                    kisRadioButton.BackColor = color;
                    kisActivationButton.Visible = true;
                    break;
                case ProductId.KTS:
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
            foreach (ProductId id in availableLicenses.Keys)
            {
                switch (id)
                {
                    case ProductId.KAV:
                        Show(kavAvailableLicensesLabel);
                        kavActivationButton.Enabled = true;
                        break;
                    case ProductId.KIS:
                        Show(kisAvailableLicensesLabel);
                        kisActivationButton.Enabled = true;
                        break;
                    case ProductId.KTS:
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
        private void githubButton_Click(object sender, EventArgs e) =>
            ProcessExecutor.BrowseToUrl("https://github.com/bitasuperactive/KasperskyCustomInstaller2022");

        private void productComparisionButton_Click(object sender, EventArgs e) =>
            ProcessExecutor.BrowseToUrl("https://www.kaspersky.es/home-security");

        private void configurationButton_Click(object sender, EventArgs e)
        {
            configurationForm.ShowDialog(this);
        }

        private void kavRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            configurationForm.ProductToInstall = ProductId.KAV;
            EnableInstallationButtons();
        }

        private void kisRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            configurationForm.ProductToInstall = ProductId.KIS;
            EnableInstallationButtons();
        }

        private void ktsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            configurationForm.ProductToInstall = ProductId.KTS;
            EnableInstallationButtons();
        }

        // TODO - Manejar adecuadamente el progreso de los procesos de la instalación.
        private void defaultInstallationButton_Click(object sender, EventArgs e)
        {
            // TODO - Guardar config = defaultInstall.
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += Installation_ProgressChanged;
            DefaultInstallation installation = new(kaspersky, configurationForm.Configuration, progress, new CancellationToken());
            installation.RunInstallation();
        }

        private void Installation_ProgressChanged(object? sender, ProgressReportModel e)
        {
            Debug.WriteLine(e.Description + ": " + e.Percentage + "%");
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
