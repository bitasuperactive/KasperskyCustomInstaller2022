using KCI_Library;
using KCI_Library.Models;
using KCI_Library.DataAccess;
using System.ComponentModel;
using System.Timers;
using System.Diagnostics;
using System.Threading;

// TODO - (!) Implementar cancelación de la actualización de requisitos.
namespace KCI_UI
{
#pragma warning disable IDE1006 // Estilos de nombres
    public partial class RequirementsForm : Form
    {
        public AutoInstallRequirementsModel Requirements { get; private set; }
        private DatabaseId kasperskyId;
        private BackgroundWorker worker;

        public RequirementsForm(DatabaseId kasperskyId)
        {
            InitializeComponent();
            this.kasperskyId = kasperskyId;

            worker = new();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync();
            while (worker.IsBusy)
                Thread.Sleep(100);
        }

        private void RequirementsForm_Load(object sender, EventArgs e)
        {
            ShowMissingRequirements();

            worker.WorkerSupportsCancellation = true;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted +=  worker_RunWorkerCompleted;
        }

        #region Métodos
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            bool waitForPwrdProtection = e.Argument == null ? false : (bool)e.Argument;

            Requirements = Dependencies.CreateAutoInstallRequirementsModel(worker, waitForPwrdProtection).Result;

            e.Result = Requirements;
        }

        private void worker_ProgressChanged(object sender, EventArgs e)
        {
            refreshButton.Text = e + "%";
        }
        private void worker_RunWorkerCompleted(object sender, EventArgs e)
        {
            refreshButton.Text = "Actualizar";
            refreshButton.Enabled = true;
            closeButton.Text = "Cerrar";
        }

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
        #endregion

        #region Eventos
        private async void refreshButton_Click(object sender, EventArgs e)
        {
            worker.RunWorkerAsync(true);
            while (worker.IsBusy)
                await Task.Delay(100);
            ShowMissingRequirements();
        }

        // Reinicia la aplicación como administrador.
        private void restartAsAdminButton_Click(object sender, EventArgs e)
        {
            // Obtiene la ruta completa del ensamblado en ejecución, omitiéndo la extensión ".dll" 
            // para obtener el ejecutable de la aplicación.
            string thisAssemblyLocation = new(System.Reflection.Assembly.GetExecutingAssembly().Location.SkipLast(4).ToArray());

            if (ProcessExecutor.AsAdmin(thisAssemblyLocation))
                Application.Exit();
            else
                MessageBox.Show(this, "Permisos de administrador denegados.", 
                    "Instalación automática", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Abre el enlace de ayuda para deshabilitar Kaspersky Password Protection.
        private void pwdProtectionMoreInfoButton_Click(object sender, EventArgs e)
        {
            switch (kasperskyId)
            {
                case DatabaseId.kav:
                    ProcessExecutor.BrowseToUrl("https://support.kaspersky.com/KAV/2021/en-US/70756.htm");
                    break;
                case DatabaseId.kis:
                    ProcessExecutor.BrowseToUrl("https://support.kaspersky.com/KIS/2019/es-ES/70756.htm");
                    break;
                case DatabaseId.kts:
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
            if (worker != null && worker.IsBusy)
            {
                worker.CancelAsync();
                return;
            }
            this.Close();
        }
        #endregion
    }
}
