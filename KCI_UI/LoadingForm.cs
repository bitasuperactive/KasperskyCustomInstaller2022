using KCI_Library.DataAccess;
using KCI_Library.Models;

namespace KCI_UI
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        // TODO - Evitar utilizar este formulario como el principal de la aplicación.
        // TODO - ¿Por qué es necesario utilizar <Task.Run> cuando el método ya es una tarea asincrónica?
        private async void LoadingForm_Load(object sender, EventArgs e)
        {
            new MainForm(Dependencies.CreateKasperskyModel(),
                await Task.Run(() => Dependencies.CreateAutoInstallRequirementsModel()),
                await Task.Run(() => SqlConnector.GetAvailableLicenses()),
                new ConfigurationModel()).Show();

            this.Hide();
        }

        private void LoadingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason.Equals(CloseReason.UserClosing))
                Application.Exit();
        }
    }
}
