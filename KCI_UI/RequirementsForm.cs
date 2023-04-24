using KCI_Library;
using KCI_Library.Models;
using KCI_Library.DataAccess;
using System.ComponentModel;
using System.Timers;
using System.Diagnostics;

// TODO - (!) Implementar cancelación de la actualización de requisitos.
namespace KCI_UI
{
#pragma warning disable IDE1006 // Estilos de nombres
    public partial class RequirementsForm : Form
    {
        public AutoInstallRequirementsModel Requirements { get; private set; }
        private DatabaseId kasperskyId;
        private TaskHandler getAutoInstallRequirementsTaskHandler;

        public RequirementsForm(DatabaseId kasperskyId)
        {
            GetAutoInstallRequirements();
            this.kasperskyId = kasperskyId;
            InitializeComponent();
        }

        private void RequirementsForm_Load(object sender, EventArgs e)
        {
            ShowMissingRequirements();

            getAutoInstallRequirementsTaskHandler.TaskStarted += getAutoInstallRequirements_Started;
            getAutoInstallRequirementsTaskHandler.ProgressChanged += getAutoInstallRequirements_ProgressChanged;
            getAutoInstallRequirementsTaskHandler.TaskCompleted += getAutoInstallRequirements_Completed;
            getAutoInstallRequirementsTaskHandler.TaskCancelled += getAutoInstallRequirementsTaskHandler.TaskCompleted;
        }

        #region Métodos
        // Actualiza los requisitos incumplidos.
        private void GetAutoInstallRequirements()
        {
            getAutoInstallRequirementsTaskHandler = new((p, t) => Requirements = Dependencies.CreateAutoInstallRequirementsModel(p, t).Result);

            getAutoInstallRequirementsTaskHandler.RunAsync().Wait();
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
        private void refreshButton_Click(object sender, EventArgs e)
        {
            GetAutoInstallRequirements();
            ShowMissingRequirements();
        }

        private void getAutoInstallRequirements_Started(object sender, EventArgs e)
        {
            refreshButton.Invoke(new Action(() => refreshButton.Enabled = false));
            closeButton.Invoke(new Action(() => closeButton.Text = "Cancelar"));
        }
        private void getAutoInstallRequirements_ProgressChanged(object sender, double e)
        {
            refreshButton.Invoke(new Action(() => refreshButton.Text = e + "%"));
        }
        private void getAutoInstallRequirements_Completed(object sender, EventArgs e)
        {
            refreshButton.Invoke(new Action(() => {
                refreshButton.Text = "Actualizar";
                refreshButton.Enabled = true;
            }));
            closeButton.Invoke(new Action(() => closeButton.Text = "Cerrar"));
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
            if (getAutoInstallRequirementsTaskHandler != null && getAutoInstallRequirementsTaskHandler.IsRunning)
            {
                getAutoInstallRequirementsTaskHandler.Cancel();
                return;
            }
            this.Close();
        }
        #endregion
    }
}
