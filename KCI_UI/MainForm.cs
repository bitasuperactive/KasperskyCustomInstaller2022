using KCI_Library;
using KCI_Library.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KCI_UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PreselectKasperskyProduct();
            ShowLicenseState();
            ShowDatabaseState();
            ShowMissingAutoInstallRequiremenets();
            ShowAvailableLicenses();
        }

        // Mostrar si hay o no licencias disponibles para cada versión de Kas y 
        // la fecha de la última actualización de las mismas.
        private void ShowAvailableLicenses()
        {
            foreach (DatabaseId id in GlobalConfig.AvailableLicenses.Keys)
            {
                string lastUpdated = GlobalConfig.AvailableLicenses[id];

                switch (id)
                {
                    case DatabaseId.kav:
                        kavAvailableLicensesLabel.Text = "Licencias disponibles";
                        kavAvailableLicensesLabel.ForeColor = Color.Green;
                        toolTip.SetToolTip(kavAvailableLicensesLabel, "Actualizadas el " + lastUpdated);
                        break;
                    case DatabaseId.kis:
                        kisAvailableLicensesLabel.Text = "Licencias disponibles";
                        kisAvailableLicensesLabel.ForeColor = Color.Green;
                        toolTip.SetToolTip(kisAvailableLicensesLabel, "Actualizadas el " + lastUpdated);
                        break;
                    case DatabaseId.kts:
                        ktsAvailableLicensesLabel.Text = "Licencias disponibles";
                        ktsAvailableLicensesLabel.ForeColor = Color.Green;
                        toolTip.SetToolTip(ktsAvailableLicensesLabel, "Actualizadas el " + lastUpdated);
                        break;
                }
            }
        }

        // Preselecciona la versión de Kas instalada.
        private void PreselectKasperskyProduct()
        {
            Color color = Color.Bisque;

            switch (GlobalConfig.Kaspersky.Id)
            {
                case DatabaseId.kav:
                    kavRadioButton.BackColor = color;
                    break;
                case DatabaseId.kis:
                    kisRadioButton.BackColor = color;
                    break;
                case DatabaseId.kts:
                    ktsRadioButton.BackColor = color;
                    break;
            }
        }

        // Muestra el estado de la licencia de Kas.
        private void ShowLicenseState()
        {
            KasperskyModel kaspersky = GlobalConfig.Kaspersky;

            if (!kaspersky.Installed)
                return;

            switch (kaspersky.LicenseExpired)
            {
                case true:
                    licenseStateLabel.Text = $"Licencia de {GlobalConfig.Kaspersky.FullName} expirada";
                    licenseStateLabel.ForeColor = Color.DarkRed;
                    break;
                case false:
                    licenseStateLabel.Text = $"Licencia de {GlobalConfig.Kaspersky.FullName} activa";
                    licenseStateLabel.ForeColor = Color.Green;
                    break;
            }

            licenseStateLabel.Visible = true;
        }

        // Mostrar estado del servidor.
        private void ShowDatabaseState()
        {
            /*if (GlobalConfig.Connection.Opened)
            {
                databaseStateLabel.Text = "Base de datos operativa";
                databaseStateLabel.ForeColor = Color.Green;
            }
            else
            {
                databaseStateLabel.Text = "Base de datos innacesible";
                databaseStateLabel.ForeColor = Color.DarkRed;
            }*/
        }

        // Enumerar los requisitos faltantes para la instalación automática.
        private void ShowMissingAutoInstallRequiremenets()
        {
            AutoInstallRequirementsModel requirements = GlobalConfig.AutoInstallRequirements;

            if (requirements.AllMet)
                return;

            string text = "Para poder llevar a cabo una instalación automática, faltan los siguientes requisitos:" + Environment.NewLine;

            autoInstallButton.Enabled = false;

            if (!requirements.Admin)
            {
                text += "* Se requieren permisos de administrador." + Environment.NewLine;
                restartAsAdminButton.Visible = true;
            }
            if (!requirements.PasswordProtectionDisabled)
                text += "* Es necesario deshabilitar la protección por contraseña de Kaspersky (?)." + Environment.NewLine;
            if (!requirements.DatabaseAccesible)
                text += "* La base de datos no se encuentra disponible." + Environment.NewLine;
            if (!requirements.KasClosed)
                text += "* Es necesario cerrar Kaspersky." + Environment.NewLine;

            autoInstallRequirementsTextBox.Text = text;
        }

        private void githubButton_Click(object sender, EventArgs e) => GitHub.BrowseToThisRepository();

        private void restartAsAdminButton_Click(object sender, EventArgs e)
        {
            // Obtiene la ruta completa del ensamblado en ejecución, omitiéndo la extensión ".dll" 
            // para obtener el ejecutable de la aplicación.
            string thisAssemblyLocation = new(System.Reflection.Assembly.GetExecutingAssembly().Location.SkipLast(4).ToArray());

            if (ProcessExecutor.AsAdmin(thisAssemblyLocation))
                // TODO - (?) Controlar cierre de la aplicación.
                Application.Exit();
            else
                MessageBox.Show(this, "Permisos de administrador denegados.", "Kaspersky Custom Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
