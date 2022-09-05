namespace KCI_UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.githubButton = new System.Windows.Forms.Button();
            this.restartAsAdminButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.kavRadioButton = new System.Windows.Forms.RadioButton();
            this.kisRadioButton = new System.Windows.Forms.RadioButton();
            this.ktsRadioButton = new System.Windows.Forms.RadioButton();
            this.usualInstallButton = new System.Windows.Forms.Button();
            this.autoInstallButton = new System.Windows.Forms.Button();
            this.installationConfigListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // githubButton
            // 
            resources.ApplyResources(this.githubButton, "githubButton");
            this.githubButton.BackgroundImage = global::KCI_UI.Properties.Resources.github_logo;
            this.githubButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.githubButton.FlatAppearance.BorderSize = 0;
            this.githubButton.ForeColor = System.Drawing.Color.Transparent;
            this.githubButton.Name = "githubButton";
            this.toolTip1.SetToolTip(this.githubButton, resources.GetString("githubButton.ToolTip"));
            this.githubButton.UseVisualStyleBackColor = true;
            this.githubButton.Click += new System.EventHandler(this.githubButton_Click);
            // 
            // restartAsAdminButton
            // 
            resources.ApplyResources(this.restartAsAdminButton, "restartAsAdminButton");
            this.restartAsAdminButton.BackgroundImage = global::KCI_UI.Properties.Resources.uac_icon;
            this.restartAsAdminButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.restartAsAdminButton.FlatAppearance.BorderSize = 0;
            this.restartAsAdminButton.ForeColor = System.Drawing.Color.Transparent;
            this.restartAsAdminButton.Name = "restartAsAdminButton";
            this.toolTip1.SetToolTip(this.restartAsAdminButton, resources.GetString("restartAsAdminButton.ToolTip"));
            this.restartAsAdminButton.UseVisualStyleBackColor = true;
            this.restartAsAdminButton.Click += new System.EventHandler(this.restartAsAdminButton_Click);
            // 
            // kavRadioButton
            // 
            resources.ApplyResources(this.kavRadioButton, "kavRadioButton");
            this.kavRadioButton.FlatAppearance.BorderSize = 0;
            this.kavRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Khaki;
            this.kavRadioButton.ForeColor = System.Drawing.Color.Black;
            this.kavRadioButton.Name = "kavRadioButton";
            this.toolTip1.SetToolTip(this.kavRadioButton, resources.GetString("kavRadioButton.ToolTip"));
            this.kavRadioButton.UseVisualStyleBackColor = true;
            // 
            // kisRadioButton
            // 
            resources.ApplyResources(this.kisRadioButton, "kisRadioButton");
            this.kisRadioButton.FlatAppearance.BorderSize = 0;
            this.kisRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Khaki;
            this.kisRadioButton.ForeColor = System.Drawing.Color.Black;
            this.kisRadioButton.Name = "kisRadioButton";
            this.toolTip1.SetToolTip(this.kisRadioButton, resources.GetString("kisRadioButton.ToolTip"));
            this.kisRadioButton.UseVisualStyleBackColor = true;
            // 
            // ktsRadioButton
            // 
            resources.ApplyResources(this.ktsRadioButton, "ktsRadioButton");
            this.ktsRadioButton.FlatAppearance.BorderSize = 0;
            this.ktsRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Khaki;
            this.ktsRadioButton.ForeColor = System.Drawing.Color.Black;
            this.ktsRadioButton.Name = "ktsRadioButton";
            this.toolTip1.SetToolTip(this.ktsRadioButton, resources.GetString("ktsRadioButton.ToolTip"));
            this.ktsRadioButton.UseVisualStyleBackColor = true;
            // 
            // usualInstallButton
            // 
            resources.ApplyResources(this.usualInstallButton, "usualInstallButton");
            this.usualInstallButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.usualInstallButton.Name = "usualInstallButton";
            this.toolTip1.SetToolTip(this.usualInstallButton, resources.GetString("usualInstallButton.ToolTip"));
            this.usualInstallButton.UseVisualStyleBackColor = true;
            this.usualInstallButton.Click += new System.EventHandler(this.usualInstallButton_Click);
            // 
            // autoInstallButton
            // 
            resources.ApplyResources(this.autoInstallButton, "autoInstallButton");
            this.autoInstallButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.autoInstallButton.Name = "autoInstallButton";
            this.toolTip1.SetToolTip(this.autoInstallButton, resources.GetString("autoInstallButton.ToolTip"));
            this.autoInstallButton.UseVisualStyleBackColor = true;
            // 
            // installationConfigListBox
            // 
            resources.ApplyResources(this.installationConfigListBox, "installationConfigListBox");
            this.installationConfigListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.installationConfigListBox.FormattingEnabled = true;
            this.installationConfigListBox.Items.AddRange(new object[] {
            resources.GetString("installationConfigListBox.Items"),
            resources.GetString("installationConfigListBox.Items1"),
            resources.GetString("installationConfigListBox.Items2"),
            resources.GetString("installationConfigListBox.Items3")});
            this.installationConfigListBox.Name = "installationConfigListBox";
            this.installationConfigListBox.Tag = "Configuración de la instalación";
            this.toolTip1.SetToolTip(this.installationConfigListBox, resources.GetString("installationConfigListBox.ToolTip"));
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.installationConfigListBox);
            this.Controls.Add(this.githubButton);
            this.Controls.Add(this.ktsRadioButton);
            this.Controls.Add(this.restartAsAdminButton);
            this.Controls.Add(this.kisRadioButton);
            this.Controls.Add(this.kavRadioButton);
            this.Controls.Add(this.autoInstallButton);
            this.Controls.Add(this.usualInstallButton);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "MainForm";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button githubButton;
        private Button restartAsAdminButton;
        private ToolTip toolTip1;
        private RadioButton kavRadioButton;
        private RadioButton kisRadioButton;
        private RadioButton ktsRadioButton;
        private Button usualInstallButton;
        private Button autoInstallButton;
        private CheckedListBox installationConfigListBox;
    }
}