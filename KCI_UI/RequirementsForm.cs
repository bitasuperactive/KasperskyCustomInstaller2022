using KCI_Library;
using KCI_Library.Models;
using KCI_Library.DataAccess;

namespace KCI_UI
{
#pragma warning disable IDE1006 // Estilos de nombres
    public partial class RequirementsForm : Form
    {
        private new MainForm Parent { get; set; }

        public RequirementsForm(MainForm parent)
        {
            InitializeComponent();
            Parent = parent;
        }

        private void RequirementsForm_Load(object sender, EventArgs e)
        {
            ShowMissingRequirements();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            UpdateMissingRequirements();
            ShowMissingRequirements();
        }

        #region Métodos
        // Muestra los requisitos incumplidos.
        private void ShowMissingRequirements()
        {
            AutoInstallRequirementsModel requirements = Parent.AutoInstallRequirements;

            if (requirements.AllMet)
            {
                MessageBox.Show(this, "Todos los requisitos han sido satisfechos, ahora puedes realizar una instalación automática.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            // Orden inverso para preservar el orden en la interfaz.
            kasClosedRequirementPanel.Visible = !requirements.KasClosed;
            passwordProtectionRequirementPanel.Visible = !requirements.PasswordProtectionDisabled;
            adminPanel.Visible = !requirements.Admin;
        }

        // Actualiza los requisitos incumplidos.
        private void UpdateMissingRequirements()
        {
            Parent.AutoInstallRequirements = Dependencies.CreateAutoInstallRequirementsModel();
        }
        #endregion

        #region Eventos
        // Reinicia la aplicación como administrador.
        private void restartAsAdminButton_Click(object sender, EventArgs e)
        {
            // Obtiene la ruta completa del ensamblado en ejecución, omitiéndo la extensión ".dll" 
            // para obtener el ejecutable de la aplicación.
            string thisAssemblyLocation = new(System.Reflection.Assembly.GetExecutingAssembly().Location.SkipLast(4).ToArray());

            // Si se ha modificado la configuración, avisar al usuario de su reseteo.
            if (Parent.Configuration.CompareTo(new ConfigurationModel()) == 0)
                MessageBox.Show(this, "La configuración de la instalación se restablecerá a sus valores por defecto.", 
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (ProcessExecutor.AsAdmin(thisAssemblyLocation))
                Application.Exit();
            else
                MessageBox.Show(this, "Permisos de administrador denegados.", 
                    "Instalación automática", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Abre el enlace de ayuda para deshabilitar Kaspersky Password Protection.
        private void pwdProtectionMoreInfoButton_Click(object sender, EventArgs e)
        {
            switch (Parent.Kaspersky.Id)
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
            this.Close();
        }
        #endregion
    }
}
