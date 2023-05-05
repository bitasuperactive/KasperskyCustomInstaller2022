using KCI_Library;
using KCI_Library.Models;
using KCI_Library.DataAccess;
using System.ComponentModel;
using System.Timers;
using System.Diagnostics;
using System.Threading;
using System;

// TODO - (!) Implementar cancelación de la actualización de requisitos.
namespace KCI_UI
{
#pragma warning disable IDE1006 // Estilos de nombres
    public partial class RequirementsForm : Form
    {
        public AutoInstallRequirementsModel Requirements { get; private set; }
        private ProductId kasperskyId;
        private CancellationTokenSource cancellationTokenSource;

        public RequirementsForm(ProductId kasperskyId, Progress<ProgressReportModel> progress)
        {
            this.kasperskyId = kasperskyId;
            Requirements = Dependencies.CreateAutoInstallRequirementsModel(progress, new CancellationToken()).Result;

            InitializeComponent();
        }

        private void RequirementsForm_Load(object sender, EventArgs e)
        {
            ShowMissingRequirements();
        }

        #region Métodos
        // Muestra los requisitos incumplidos.
        private void ShowMissingRequirements()
        {
            if (Requirements.AllMet)
            {
                MessageBox.Show(this, "Todos los requisitos han sido satisfechos, ahora puedes realizar una instalación automática.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            // Orden inverso para preservar el orden en la interfaz.
            kasClosedRequirementPanel.Visible = !Requirements.KasClosed;
            passwordProtectionRequirementPanel.Visible = !Requirements.PasswordProtectionDisabled;
            adminPanel.Visible = !Requirements.Admin;
        }
        private async void refreshButton_Click(object sender, EventArgs e)
        {
            Progress<ProgressReportModel> progress = new();
            progress.ProgressChanged += UpdateRequirements_ProgressChanged;
            cancellationTokenSource = new CancellationTokenSource();

            refreshButton.Enabled = false;
            closeButton.Text = "Cancelar";

            try
            {
                Requirements = await Dependencies.CreateAutoInstallRequirementsModel(progress, cancellationTokenSource.Token, true);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("TAREA CANCELADA");
                return;
            }
            finally
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
                refreshButton.Text = "Actualizar";
                refreshButton.Enabled = true;
                closeButton.Text = "Cerrar";
            }

            ShowMissingRequirements();
        }

        private void UpdateRequirements_ProgressChanged(object? sender, ProgressReportModel e)
        {
            refreshButton.Text = e.Percentage + "%";
        }
        #endregion

        #region Eventos

        // Reinicia la aplicación como administrador.
        private void restartAsAdminButton_Click(object sender, EventArgs e)
        {
            // Obtiene la ruta completa del ensamblado en ejecución, omitiéndo la extensión ".dll" 
            // para obtener el ejecutable de la aplicación.
            string thisAssemblyLocation = new(System.Reflection.Assembly.GetExecutingAssembly().Location.SkipLast(4).ToArray());

            if (ProcessExecutor.AsAdmin(thisAssemblyLocation))
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show(this, "Permisos de administrador denegados.",
                    "Instalación automática", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Abre el enlace de ayuda para deshabilitar Kaspersky Password Protection.
        private void pwdProtectionMoreInfoButton_Click(object sender, EventArgs e)
        {
            switch (kasperskyId)
            {
                case ProductId.KAV:
                    ProcessExecutor.BrowseToUrl("https://support.kaspersky.com/KAV/2021/en-US/70756.htm");
                    break;
                case ProductId.KIS:
                    ProcessExecutor.BrowseToUrl("https://support.kaspersky.com/KIS/2019/es-ES/70756.htm");
                    break;
                case ProductId.KTS:
                    ProcessExecutor.BrowseToUrl("https://support.kaspersky.com/KTS/21.2/es-ES/70756.htm");
                    break;
            }
        }

        // Abre el enlace de ayuda para cerrar la aplicación del producto.
        private void kasClosedMoreInfoButton_Click(object sender, EventArgs e)
        {
            ProcessExecutor.BrowseToUrl("https://imgur.com/a/dsxJbjY");
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource is not null)
            {
                cancellationTokenSource.Cancel();
                return;
            }
            this.Close();
        }
        #endregion
    }
}
