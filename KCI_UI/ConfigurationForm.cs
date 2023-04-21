using KCI_Library.Models;

namespace KCI_UI
{
#pragma warning disable IDE1006 // Estilos de nombres
    public partial class ConfigurationForm : Form
    {
        public ConfigurationModel Configuration { get; private set; }
        private bool KavInstalled { get; set; }
        private bool DbAccesible { get; set; }

        public ConfigurationForm(ConfigurationModel configuration, bool kavInstalled, bool dbAccesible)
        {
            this.Configuration = configuration;
            this.KavInstalled = kavInstalled;
            this.DbAccesible = dbAccesible;
            InitializeComponent();
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            ShowConfiguration();
        }

        #region Métodos
        // Muestra los valores del modelo de configuración.
        private void ShowConfiguration()
        {
            keepKasperskyConfigCheckBox.Enabled = KavInstalled;
            offlineSetupCheckBox.Enabled = DbAccesible;
            doNotUseDatabaseLicensesCheckBox.Enabled = DbAccesible;

            keepKasperskyConfigCheckBox.Checked = Configuration.KeepKasperskyConfig;
            offlineSetupCheckBox.Checked = Configuration.OfflineSetup;
            doNotUseDatabaseLicensesCheckBox.Checked = Configuration.OfflineSetup;
            kasperskySecureConnectionCheckBox.Checked = Configuration.KasperskySecureConnection;
        }

        // Guarda el modelo de configuración en los ajustes de la aplicación.
        private void SaveConfiguration()
        {
            ConfigurationModel newConfiguration = new(keepKasperskyConfigCheckBox.Checked,
                offlineSetupCheckBox.Checked,
                doNotUseDatabaseLicensesCheckBox.Checked,
                kasperskySecureConnectionCheckBox.Checked);
            newConfiguration = newConfiguration.ValidateConfiguration(KavInstalled, DbAccesible); // TODO - ¿Innecesario?

            if (newConfiguration.CompareTo(Configuration) == 1)
                return;

            Configuration = newConfiguration;

            Properties.Settings.Default.KeepKasperskyConfig = newConfiguration.KeepKasperskyConfig;
            Properties.Settings.Default.OfflineSetup = newConfiguration.OfflineSetup;
            Properties.Settings.Default.DoNotUseDatabaseLicenses = newConfiguration.DoNotUseDatabaseLicenses;
            Properties.Settings.Default.KasperskySecureConnection = newConfiguration.KasperskySecureConnection;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Eventos
        // Actualiza la configuración.
        private void applyButton_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        // Cierra el formulario.
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
