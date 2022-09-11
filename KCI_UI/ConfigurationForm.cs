﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KCI_Library.Models;

namespace KCI_UI
{
    public partial class ConfigurationForm : Form
    {
        private new MainForm Parent { get; set; }

        public ConfigurationForm(MainForm parent)
        {
            Parent = parent;

            InitializeComponent();
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
                keepKasConfigCheckBox.Text = $"Mantener configuración de {Parent.Kaspersky.FullName}";

            // Bloquea aquellas configuracinoes dependientes del producto instalado y la base de datos.
            keepKasConfigCheckBox.Enabled = Parent.Kaspersky.Installed;
            offlineSetupCheckBox.Enabled = databaseAccesible;
            justUseDefaultLicenseCheckBox.Enabled = databaseAccesible;

            if (configuration.CompareTo(new ConfigurationModel()) == 1)
            {
                keepKasConfigCheckBox.Checked = Parent.Kaspersky.Installed;
                offlineSetupCheckBox.Checked = false;
                justUseDefaultLicenseCheckBox.Checked = !databaseAccesible;
                installKscCheckBox.Checked = false;
            }
            else
            {
                keepKasConfigCheckBox.Checked = configuration.KeepKasperskyConfig;
                offlineSetupCheckBox.Checked = configuration.UseOfflineSetup;
                justUseDefaultLicenseCheckBox.Checked = configuration.DoNotUseDatabaseLicenses;
                installKscCheckBox.Checked = configuration.KasperskySecureConnection;
            }
        }

        // Crea un modelo de configuración en base de los checkboxes ticados.
        private ConfigurationModel CreateConfigModel()
        {
            return new ConfigurationModel(
                keepKasConfigCheckBox.Checked,
                offlineSetupCheckBox.Checked,
                justUseDefaultLicenseCheckBox.Checked,
                installKscCheckBox.Checked);
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
