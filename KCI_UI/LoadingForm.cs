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

        private async void LoadingForm_Shown(object sender, EventArgs e)
        {
            new MainForm(await Task.Run(() => Dependencies.CreateKasperskyModel()),
                await Task.Run(() => Dependencies.CreateAutoInstallRequirementsModel()),
                await SqlConnector.GetAvailableLicenses(),
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
