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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.licenseStateLabel = new System.Windows.Forms.Label();
            this.kavRadioButton = new System.Windows.Forms.RadioButton();
            this.kavAvailableLicensesLabel = new System.Windows.Forms.Label();
            this.kisRadioButton = new System.Windows.Forms.RadioButton();
            this.kisAvailableLicensesLabel = new System.Windows.Forms.Label();
            this.ktsRadioButton = new System.Windows.Forms.RadioButton();
            this.ktsAvailableLicensesLabel = new System.Windows.Forms.Label();
            this.configButton = new System.Windows.Forms.Button();
            this.defaultInstallButton = new System.Windows.Forms.Button();
            this.autoInstallButton = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.topPanel.SuspendLayout();
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
            this.toolTip.SetToolTip(this.githubButton, resources.GetString("githubButton.ToolTip"));
            this.githubButton.UseVisualStyleBackColor = true;
            this.githubButton.Click += new System.EventHandler(this.githubButton_Click);
            // 
            // licenseStateLabel
            // 
            resources.ApplyResources(this.licenseStateLabel, "licenseStateLabel");
            this.licenseStateLabel.Name = "licenseStateLabel";
            this.toolTip.SetToolTip(this.licenseStateLabel, resources.GetString("licenseStateLabel.ToolTip"));
            // 
            // kavRadioButton
            // 
            resources.ApplyResources(this.kavRadioButton, "kavRadioButton");
            this.kavRadioButton.FlatAppearance.BorderSize = 0;
            this.kavRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Khaki;
            this.kavRadioButton.ForeColor = System.Drawing.Color.Black;
            this.kavRadioButton.Name = "kavRadioButton";
            this.toolTip.SetToolTip(this.kavRadioButton, resources.GetString("kavRadioButton.ToolTip"));
            this.kavRadioButton.UseVisualStyleBackColor = true;
            // 
            // kavAvailableLicensesLabel
            // 
            resources.ApplyResources(this.kavAvailableLicensesLabel, "kavAvailableLicensesLabel");
            this.kavAvailableLicensesLabel.BackColor = System.Drawing.Color.Transparent;
            this.kavAvailableLicensesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.kavAvailableLicensesLabel.Name = "kavAvailableLicensesLabel";
            this.toolTip.SetToolTip(this.kavAvailableLicensesLabel, resources.GetString("kavAvailableLicensesLabel.ToolTip"));
            // 
            // kisRadioButton
            // 
            resources.ApplyResources(this.kisRadioButton, "kisRadioButton");
            this.kisRadioButton.FlatAppearance.BorderSize = 0;
            this.kisRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Khaki;
            this.kisRadioButton.ForeColor = System.Drawing.Color.Black;
            this.kisRadioButton.Name = "kisRadioButton";
            this.toolTip.SetToolTip(this.kisRadioButton, resources.GetString("kisRadioButton.ToolTip"));
            this.kisRadioButton.UseVisualStyleBackColor = true;
            // 
            // kisAvailableLicensesLabel
            // 
            resources.ApplyResources(this.kisAvailableLicensesLabel, "kisAvailableLicensesLabel");
            this.kisAvailableLicensesLabel.BackColor = System.Drawing.Color.Transparent;
            this.kisAvailableLicensesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.kisAvailableLicensesLabel.Name = "kisAvailableLicensesLabel";
            this.toolTip.SetToolTip(this.kisAvailableLicensesLabel, resources.GetString("kisAvailableLicensesLabel.ToolTip"));
            // 
            // ktsRadioButton
            // 
            resources.ApplyResources(this.ktsRadioButton, "ktsRadioButton");
            this.ktsRadioButton.FlatAppearance.BorderSize = 0;
            this.ktsRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Khaki;
            this.ktsRadioButton.ForeColor = System.Drawing.Color.Black;
            this.ktsRadioButton.Name = "ktsRadioButton";
            this.toolTip.SetToolTip(this.ktsRadioButton, resources.GetString("ktsRadioButton.ToolTip"));
            this.ktsRadioButton.UseVisualStyleBackColor = true;
            // 
            // ktsAvailableLicensesLabel
            // 
            resources.ApplyResources(this.ktsAvailableLicensesLabel, "ktsAvailableLicensesLabel");
            this.ktsAvailableLicensesLabel.BackColor = System.Drawing.Color.Transparent;
            this.ktsAvailableLicensesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.ktsAvailableLicensesLabel.Name = "ktsAvailableLicensesLabel";
            this.toolTip.SetToolTip(this.ktsAvailableLicensesLabel, resources.GetString("ktsAvailableLicensesLabel.ToolTip"));
            // 
            // configButton
            // 
            resources.ApplyResources(this.configButton, "configButton");
            this.configButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.configButton.Name = "configButton";
            this.toolTip.SetToolTip(this.configButton, resources.GetString("configButton.ToolTip"));
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // defaultInstallButton
            // 
            resources.ApplyResources(this.defaultInstallButton, "defaultInstallButton");
            this.defaultInstallButton.Name = "defaultInstallButton";
            this.toolTip.SetToolTip(this.defaultInstallButton, resources.GetString("defaultInstallButton.ToolTip"));
            this.defaultInstallButton.UseVisualStyleBackColor = true;
            // 
            // autoInstallButton
            // 
            resources.ApplyResources(this.autoInstallButton, "autoInstallButton");
            this.autoInstallButton.Name = "autoInstallButton";
            this.toolTip.SetToolTip(this.autoInstallButton, resources.GetString("autoInstallButton.ToolTip"));
            this.autoInstallButton.UseVisualStyleBackColor = true;
            // 
            // topPanel
            // 
            resources.ApplyResources(this.topPanel, "topPanel");
            this.topPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.topPanel.Controls.Add(this.githubButton);
            this.topPanel.Controls.Add(this.configButton);
            this.topPanel.Controls.Add(this.licenseStateLabel);
            this.topPanel.Name = "topPanel";
            this.toolTip.SetToolTip(this.topPanel, resources.GetString("topPanel.ToolTip"));
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.autoInstallButton);
            this.Controls.Add(this.defaultInstallButton);
            this.Controls.Add(this.ktsAvailableLicensesLabel);
            this.Controls.Add(this.ktsRadioButton);
            this.Controls.Add(this.kisAvailableLicensesLabel);
            this.Controls.Add(this.kisRadioButton);
            this.Controls.Add(this.kavAvailableLicensesLabel);
            this.Controls.Add(this.kavRadioButton);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "MainForm";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button githubButton;
        private ToolTip toolTip;
        private RadioButton kavRadioButton;
        private Label licenseStateLabel;
        private Label kavAvailableLicensesLabel;
        private Label kisAvailableLicensesLabel;
        private RadioButton kisRadioButton;
        private Label ktsAvailableLicensesLabel;
        private RadioButton ktsRadioButton;
        private Button configButton;
        private Button defaultInstallButton;
        private Button autoInstallButton;
        private Panel topPanel;
    }
}