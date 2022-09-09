using KCI_Library;
using KCI_Library.DataAccess;
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
        private KasperskyModel Kaspersky { get; set; }

        public MainForm()
        {
            InitializeComponent();
            Kaspersky = Dependencies.CreateKasperskyModel();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PreselectKasperskyProduct();
            ShowLicenseState();
            ShowMissingAutoInstallRequiremenets();
            ShowAvailableLicenses();
        }

        // Preselecciona la versión de Kas instalada.
        private void PreselectKasperskyProduct()
        {
            Color color = Color.Bisque;

            switch (Kaspersky.Id)
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
            if (!Kaspersky.Installed)
                return;

            switch (Kaspersky.LicenseExpired)
            {
                case true:
                    licenseStateLabel.Text = $"Licencia de {Kaspersky.FullName} expirada";
                    licenseStateLabel.ForeColor = Color.DarkRed;
                    break;
                case false:
                    licenseStateLabel.Text = $"Licencia de {Kaspersky.FullName} activa";
                    licenseStateLabel.ForeColor = Color.Green;
                    break;
            }

            licenseStateLabel.Visible = true;
        }

        // Enumerar los requisitos faltantes para la instalación automática.
        private void ShowMissingAutoInstallRequiremenets()
        {
            AutoInstallRequirementsModel requirements = Dependencies.CreateAutoInstallRequirementsModel();

            if (requirements.AllMet)
                return;

            string text = "Para poder llevar a cabo una instalación automática, faltan los siguientes requisitos:" + Environment.NewLine;

            //autoInstallButton.Enabled = false;

            if (!requirements.DatabaseAccesible)
                text += "* La base de datos no se encuentra disponible (reintentar)." + Environment.NewLine;
            else
            {
                if (!requirements.Admin)
                {
                    text += "* Se requieren permisos de administrador (ejecutar como admin)." + Environment.NewLine;
                    //restartAsAdminButton.Visible = true;
                }
                if (!requirements.PasswordProtectionDisabled)
                    text += "* Es necesario deshabilitar la protección por contraseña de Kaspersky (?)." + Environment.NewLine;
                if (!requirements.KasClosed)
                    text += "* Es necesario cerrar Kaspersky (?)." + Environment.NewLine;
            }

            //autoInstallRequirementsTextBox.Text = text;
        }

        // Mostrar si hay o no licencias disponibles para cada versión de Kas y 
        // la fecha de la última actualización de las mismas.
        private void ShowAvailableLicenses()
        {
            Dictionary<DatabaseId, string> availableLicenses = SqlConnector.GetAvailableLicenses();

            foreach (DatabaseId id in availableLicenses.Keys)
            {
                string lastUpdated = availableLicenses[id];

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

        private void githubButton_Click(object sender, EventArgs e) => GitHub.BrowseToThisRepository();

        // TODO - Implementar reinicio como admin en TextBox.
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

        private void configButton_Click(object sender, EventArgs e)
        {
            Form form = new ConfigurationForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.Show(this);
        }
    }
}
