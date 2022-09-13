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
    public partial class LoadingForm : Form
    {
        private new MainForm Parent;

        public LoadingForm(MainForm parent)
        {
            InitializeComponent();
            Parent = parent;
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            this.Text = Parent.Text;
            this.loadingLabel.Text = $"Iniciando {Parent.Text}...";
        }

        private void LoadingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason.Equals(CloseReason.UserClosing))
                Application.Exit();
        }
    }
}
