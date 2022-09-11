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

// TODO - (!!!) Revisar la coherencia de los nombres en todas las clases, objetos y textos.
// TODO - (!!!) Comentar todo el código creado.

namespace KCI_UI
{
    public partial class MainForm : Form
    {
        public KasperskyModel Kaspersky { get; private set; }
        public Dictionary<DatabaseId, string> AvailableLicenses { get; private set; }
        public AutoInstallRequirementsModel AutoInstallRequirements { get; set; }
        public ConfigurationModel Configuration { get; set; }

        public MainForm()
        {
            Kaspersky = Dependencies.CreateKasperskyModel();
            AvailableLicenses = SqlConnector.GetAvailableLicenses();
            AutoInstallRequirements = Dependencies.CreateAutoInstallRequirementsModel();
            Configuration = new ConfigurationModel();

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            HighlightKasperskyProduct();
            EnableActivationButton();
            CheckDatabaseAccesible();
            ShowAvailableLicenses();
        }

        #region Métodos
        // Resaltar el producto instalado.
        private void HighlightKasperskyProduct()
        {
            Color color = Color.LightGoldenrodYellow;

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

        // Mostrar si hay licencias disponibles para cada versión de Kaspersky y, 
        // la fecha de la última actualización de las mismas.
        private void ShowAvailableLicenses()
        {
            foreach (DatabaseId id in AvailableLicenses.Keys)
            {
                string lastUpdated = AvailableLicenses[id];

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

        // Comprueba si la base de datos es accesible.
        private void CheckDatabaseAccesible()
        {
            if (!AutoInstallRequirements.DatabaseAccesible)
                databaseNotAccesibleLabel.Visible = true;
        }

        // Mostrar el botón de activación de la licencia en el producto instalado, y
        // habilitarlo si hubieran licencias disponibles.
        private void EnableActivationButton()
        {
            switch (Kaspersky.Id)
            {
                case DatabaseId.kav:
                    kavActivationButton.Visible = true;
                    if (AvailableLicenses.ContainsKey(DatabaseId.kav))
                        kavActivationButton.Enabled = true;
                    break;
                case DatabaseId.kis:
                    kisActivationButton.Visible = true;
                    if (AvailableLicenses.ContainsKey(DatabaseId.kis))
                        kisActivationButton.Enabled = true;
                    break;
                case DatabaseId.kts:
                    ktsActivationButton.Visible = true;
                    if (AvailableLicenses.ContainsKey(DatabaseId.kts))
                        ktsActivationButton.Enabled = true;
                    break;
            }
        }
        #endregion

        #region Eventos
        private void githubButton_Click(object sender, EventArgs e) => 
            ProcessExecutor.BrowseToUrl("https://github.com/bitasuperactive/KasperskyCustomInstaller2022");

        private void configButton_Click(object sender, EventArgs e) => 
            new ConfigurationForm(this).ShowDialog(this);

        private void kavRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!defaultInstallButton.Enabled)
                defaultInstallButton.Enabled = true;

            if (!autoInstallButton.Enabled && AutoInstallRequirements.DatabaseAccesible)
            {
                autoInstallButton.Enabled = true;
                databaseNotAccesibleLabel.Visible = false;
            }
        }

        private void kisRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!defaultInstallButton.Enabled)
                defaultInstallButton.Enabled = true;

            if (!autoInstallButton.Enabled && AutoInstallRequirements.DatabaseAccesible)
            {
                autoInstallButton.Enabled = true;
                databaseNotAccesibleLabel.Visible = false;
            }
        }

        private void ktsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!defaultInstallButton.Enabled)
                defaultInstallButton.Enabled = true;

            if (!autoInstallButton.Enabled && AutoInstallRequirements.DatabaseAccesible)
            {
                autoInstallButton.Enabled = true;
                databaseNotAccesibleLabel.Visible = false;
            }
        }

        private void defaultInstallButton_Click(object sender, EventArgs e)
        {
            // TODO - Realizar instalación habitual.
        }

        private void autoInstallButton_Click(object sender, EventArgs e)
        {
            if (AutoInstallRequirements.AllMet)
            {
                // TODO - Realizar instalación automática.
            }
            else
            {
                new RequirementsForm(this).ShowDialog(this);
            }
        }
        #endregion
    }
}
