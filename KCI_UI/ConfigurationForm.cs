using KCI_Library.Models;

namespace KCI_UI
{
    public partial class ConfigurationForm : Form
    {
        private new MainForm Parent { get; set; }

        public ConfigurationForm(MainForm parent)
        {
            InitializeComponent();
            Parent = parent;
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            SetDefaultConfig();
        }

        #region Métodos
        // Establece la configuración por defecto o, la recupera.
        private void SetDefaultConfig()
        {
            ConfigurationModel configuration = Parent.Configuration;
            bool databaseAccesible = Parent.AutoInstallRequirements.DatabaseAccesible;

            // Muestra el nombre completo del producto instalado en las configuraciones que lo utilicen.
            if (Parent.Kaspersky.Installed)
                keepKasperskyConfigCheckBox.Text = $"Mantener configuración de {Parent.Kaspersky.FullName}";

            // Bloquea aquellas configuracinoes dependientes del producto instalado y la base de datos.
            keepKasperskyConfigCheckBox.Enabled = Parent.Kaspersky.Installed;
            offlineSetupCheckBox.Enabled = databaseAccesible;
            doNotUseDatabaseLicensesCheckBox.Enabled = databaseAccesible;

            if (configuration.CompareTo(new ConfigurationModel()) == 1)
            {
                keepKasperskyConfigCheckBox.Checked = Parent.Kaspersky.Installed;
                offlineSetupCheckBox.Checked = false;
                doNotUseDatabaseLicensesCheckBox.Checked = !databaseAccesible;
                kasperskySecureConnectionCheckBox.Checked = false;
            }
            else
            {
                keepKasperskyConfigCheckBox.Checked = configuration.KeepKasperskyConfig;
                offlineSetupCheckBox.Checked = configuration.OfflineSetup;
                doNotUseDatabaseLicensesCheckBox.Checked = configuration.DoNotUseDatabaseLicenses;
                kasperskySecureConnectionCheckBox.Checked = configuration.KasperskySecureConnection;
            }
        }

        // Crea un modelo de configuración en base de los checkboxes ticados.
        private ConfigurationModel CreateConfigModel()
        {
            return new ConfigurationModel(
                keepKasperskyConfigCheckBox.Checked,
                offlineSetupCheckBox.Checked,
                doNotUseDatabaseLicensesCheckBox.Checked,
                kasperskySecureConnectionCheckBox.Checked);
        }
        #endregion

        #region Eventos
        // Actualiza la configuración.
        private void applyButton_Click(object sender, EventArgs e)
        {
            Parent.Configuration = CreateConfigModel();
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
