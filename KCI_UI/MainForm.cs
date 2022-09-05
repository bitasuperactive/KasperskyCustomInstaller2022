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
            installationConfigListBox.SetItemChecked(0, true);
        }

        private void githubButton_Click(object sender, EventArgs e) => GitHubAccess.BrowseToThisGitHubRepository();

        private void restartAsAdminButton_Click(object sender, EventArgs e)
        {
            // Obtiene la ruta completa del ensamblado en ejecución, omitiéndo la extensión ".dll" 
            // para obtener el ejecutable de la aplicación.
            string thisAssemblyLocation = new(System.Reflection.Assembly.GetExecutingAssembly().Location.SkipLast(4).ToArray());

            if (ProcessExecutor.RunAsAdmin(thisAssemblyLocation))
                // TODO - (%) Controlar cierre de la aplicación.
                Application.Exit();
            else
                // TODO - Crear un mensaje de error en condiciones.
                MessageBox.Show(this, "Permisos de administrador denegados.", "Kaspersky Custom Installer");
        }

        private void usualInstallButton_Click(object sender, EventArgs e)
        {
            TestingMethod();
        }

        // MÉTODO DE TESTEO.
        private static void TestingMethod()
        {
            string text = "Installed : " + Dependencies.KasIsInstalled.ToString() + "\n";
            foreach (Dependencies.KasInfoType type in Dependencies.KasInfo.Keys)
            {
                text += type + " : " + Dependencies.KasInfo[type] + "\n";
            }
            MessageBox.Show(text, "Kaspersky Information");

            text = "";
            foreach (Dependencies.AutoInstallRequirementType type in Dependencies.AutoInstallRequirements.Keys)
            {
                text += type + " : " + Dependencies.AutoInstallRequirements[type] + "\n";
            }
            MessageBox.Show(text, "Automatic Installation Requirements");
        }
    }
}
