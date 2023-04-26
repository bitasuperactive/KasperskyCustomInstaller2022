using KCI_Library;
using KCI_Library.Models;
using System.Diagnostics;

namespace KCI_UI
{
#pragma warning disable IDE1006 // Estilos de nombres
    public partial class ConfigurationForm : Form
    {
        private bool kavInstalled;
        private bool dbAccesible;

        public ConfigurationForm(bool kavInstalled, bool dbAccesible)
        {
            this.kavInstalled = kavInstalled;
            this.dbAccesible = dbAccesible;
            InitializeComponent();
        }

        public ConfigurationModel Configuration
        {
            get
            {
                ConfigurationModel configuration = new(Properties.Settings.Default.KeepKasperskyConfig,
                    Properties.Settings.Default.OfflineSetup,
                    Properties.Settings.Default.DoNotUseDatabaseLicenses,
                    Properties.Settings.Default.KasperskySecureConnection);

                return configuration.ValidateConfiguration(kavInstalled, dbAccesible);
            }
            private set
            {
                value = value.ValidateConfiguration(kavInstalled, dbAccesible);

                Properties.Settings.Default.KeepKasperskyConfig = value.KeepKasperskyConfig;
                Properties.Settings.Default.OfflineSetup = value.OfflineSetup;
                Properties.Settings.Default.DoNotUseDatabaseLicenses = value.DoNotUseDatabaseLicenses;
                Properties.Settings.Default.KasperskySecureConnection = value.KasperskySecureConnection;
                Properties.Settings.Default.Save();
            }
        }

        public DatabaseId ProductToInstall
        {
            get
            {
                return (DatabaseId)Enum.Parse(typeof(DatabaseId), Properties.Settings.Default.ProductToInstall);
            }
            set
            {
                Properties.Settings.Default.ProductToInstall = value.ToString();
            }
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("Producto a instalar: " + ProductToInstall.ToString());

            ShowConfiguration();
        }

        #region Métodos
        // Muestra los valores del modelo de configuración.
        private void ShowConfiguration()
        {
            keepKasperskyConfigCheckBox.Enabled = kavInstalled;
            offlineSetupCheckBox.Enabled = dbAccesible;
            doNotUseDatabaseLicensesCheckBox.Enabled = dbAccesible;

            ConfigurationModel configuration = Configuration;
            keepKasperskyConfigCheckBox.Checked = configuration.KeepKasperskyConfig;
            offlineSetupCheckBox.Checked = configuration.OfflineSetup;
            doNotUseDatabaseLicensesCheckBox.Checked = configuration.OfflineSetup;
            kasperskySecureConnectionCheckBox.Checked = configuration.KasperskySecureConnection;
        }

        // Guarda el modelo de configuración en los ajustes de la aplicación.
        private void SaveConfiguration()
        {
            ConfigurationModel newConfiguration = new(keepKasperskyConfigCheckBox.Checked,
                offlineSetupCheckBox.Checked,
                doNotUseDatabaseLicensesCheckBox.Checked,
                kasperskySecureConnectionCheckBox.Checked);

            if (newConfiguration.CompareTo(Configuration) == 0)
                Configuration = newConfiguration;
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
