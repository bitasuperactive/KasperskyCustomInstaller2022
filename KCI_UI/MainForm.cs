using KCI_Library;
using System;
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
            if (Dependencies.KasInfo.Installed)
            {
                // Preseleccionar la versión de Kas instalada.
                // Mostrar el estado de la licencia de Kas.
                licenseStateLabel.Visible = true;
                switch (Dependencies.KasInfo[Dependencies.KasInfoType.Id])
                {
                    case "kav":
                        kavRadioButton.BackColor = Color.LightGoldenrodYellow;
                        if (Dependencies.KasInfo[Dependencies.KasInfoType.LicenseExpired].Equals("False"))
                        {
                            licenseStateLabel.Text = "Licencia de Kaspersky Antivirus activa";
                            licenseStateLabel.ForeColor = Color.Green;
                        }
                        else
                        {
                            licenseStateLabel.Text = "Licencia de Kaspersky Antivirus expirada";
                            licenseStateLabel.ForeColor = Color.DarkRed;
                        }
                        break;
                    case "kis":
                        kisRadioButton.BackColor = Color.LightGoldenrodYellow;
                        if (Dependencies.KasInfo[Dependencies.KasInfoType.LicenseExpired].Equals("False"))
                        {
                            licenseStateLabel.Text = "Licencia de Kaspersky Internet Security activa";
                            licenseStateLabel.ForeColor = Color.Green;
                        }
                        else
                        {
                            licenseStateLabel.Text = "Licencia de Kaspersky Internet Security expirada";
                            licenseStateLabel.ForeColor = Color.DarkRed;
                        }
                        break;
                    case "kts":
                        ktsRadioButton.BackColor = Color.LightGoldenrodYellow;
                        if (Dependencies.KasInfo[Dependencies.KasInfoType.LicenseExpired].Equals("False"))
                        {
                            licenseStateLabel.Text = "Licencia de Kaspersky Total Security activa";
                            licenseStateLabel.ForeColor = Color.Green;
                        }
                        else
                        {
                            licenseStateLabel.Text = "Licencia de Kaspersky Total Security expirada";
                            licenseStateLabel.ForeColor = Color.DarkRed;
                        }
                        break;
                }
            }

            // Mostrar estado del servidor.
            if (SqlConnector.GetConnectionState() is ConnectionState.Open)
            {
                databaseStateLabel.Text = "Base de datos operativa";
                databaseStateLabel.ForeColor = Color.Green;
            }
            else
            {
                databaseStateLabel.Text = "Base de datos innacesible";
                databaseStateLabel.ForeColor = Color.DarkRed;
            }

            // Enumerar los requisitos faltantes para la instalación automática.
            string text = string.Empty;
            foreach (Dependencies.AutoInstallRequirementType r in Dependencies.AutoInstallRequirements.Keys)
            {
                if (!Dependencies.AutoInstallRequirements[r])
                    switch (r)
                    {
                        case Dependencies.AutoInstallRequirementType.Admin:
                            text += "* Se requieren permisos de administrador." + Environment.NewLine;
                            restartAsAdminButton.Visible = true;
                            break;
                        case Dependencies.AutoInstallRequirementType.PasswordProtectionDisabled:
                            text += "* Es necesario deshabilitar la protección por contraseña de Kaspersky (?)." + Environment.NewLine;
                            break;
                        case Dependencies.AutoInstallRequirementType.MyDatabase:
                            text += "* La base de datos no se encuentra disponible." + Environment.NewLine;
                            break;
                        case Dependencies.AutoInstallRequirementType.Closed:
                            text += "* Es necesario cerrar Kaspersky." + Environment.NewLine;
                            break;
                    }
            }
            autoInstallRequirementsTextBox.Text = text;

            // Mostrar si hay o no licencias disponibles para cada versión de Kas y 
            // la fecha de la última actualización de las mismas.
            foreach (Dependencies.DatabaseTableIds id in Dependencies.AvailableLicenses)
            {
                switch (id)
                {
                    case Dependencies.DatabaseTableIds.kav:
                        kavAvailableLicensesLabel.Text = "Licencias disponibles";
                        kavAvailableLicensesLabel.ForeColor = Color.Green;
                        toolTip.SetToolTip(kavAvailableLicensesLabel, "Actualizadas el " + 
                            Dependencies.GetDatabaseData(id)[Dependencies.DatabaseDataType.LastUpdated]);
                        break;
                    case Dependencies.DatabaseTableIds.kis:
                        kisAvailableLicensesLabel.Text = "Licencias disponibles";
                        kisAvailableLicensesLabel.ForeColor = Color.Green;
                        toolTip.SetToolTip(kisAvailableLicensesLabel, "Actualizadas el " +
                            Dependencies.GetDatabaseData(id)[Dependencies.DatabaseDataType.LastUpdated]);
                        break;
                    case Dependencies.DatabaseTableIds.kts:
                        ktsAvailableLicensesLabel.Text = "Licencias disponibles";
                        ktsAvailableLicensesLabel.ForeColor = Color.Green;
                        toolTip.SetToolTip(ktsAvailableLicensesLabel, "Actualizadas el " +
                            Dependencies.GetDatabaseData(id)[Dependencies.DatabaseDataType.LastUpdated]);
                        break;
                }
            }
        }

        private void githubButton_Click(object sender, EventArgs e) => GitHub.BrowseToThisRepository();

        private void restartAsAdminButton_Click(object sender, EventArgs e)
        {
            // Obtiene la ruta completa del ensamblado en ejecución, omitiéndo la extensión ".dll" 
            // para obtener el ejecutable de la aplicación.
            string thisAssemblyLocation = new(System.Reflection.Assembly.GetExecutingAssembly().Location.SkipLast(4).ToArray());

            if (ProcessExecutor.AsAdmin(thisAssemblyLocation))
                // TODO - (%) Controlar cierre de la aplicación.
                Application.Exit();
            else
                // TODO - Crear un mensaje de error en condiciones.
                MessageBox.Show(this, "Permisos de administrador denegados.", "Kaspersky Custom Installer");
        }
    }
}
