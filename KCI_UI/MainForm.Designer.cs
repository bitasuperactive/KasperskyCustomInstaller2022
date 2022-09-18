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
            this.separator1 = new System.Windows.Forms.Label();
            this.comparisionButton = new System.Windows.Forms.Button();
            this.kavActivationButton = new System.Windows.Forms.Button();
            this.kisActivationButton = new System.Windows.Forms.Button();
            this.ktsActivationButton = new System.Windows.Forms.Button();
            this.radioPanel = new System.Windows.Forms.Panel();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.databaseNotAccesibleLabel = new System.Windows.Forms.Label();
            this.topPanel.SuspendLayout();
            this.radioPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // githubButton
            // 
            resources.ApplyResources(this.githubButton, "githubButton");
            this.githubButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.githubButton.FlatAppearance.BorderSize = 0;
            this.githubButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.githubButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.githubButton.ForeColor = System.Drawing.Color.Blue;
            this.githubButton.Name = "githubButton";
            this.toolTip.SetToolTip(this.githubButton, resources.GetString("githubButton.ToolTip"));
            this.githubButton.UseVisualStyleBackColor = true;
            this.githubButton.Click += new System.EventHandler(this.githubButton_Click);
            // 
            // kavRadioButton
            // 
            resources.ApplyResources(this.kavRadioButton, "kavRadioButton");
            this.kavRadioButton.FlatAppearance.BorderSize = 0;
            this.kavRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(205)))), ((int)(((byte)(72)))));
            this.kavRadioButton.ForeColor = System.Drawing.Color.Black;
            this.kavRadioButton.Name = "kavRadioButton";
            this.toolTip.SetToolTip(this.kavRadioButton, resources.GetString("kavRadioButton.ToolTip"));
            this.kavRadioButton.UseVisualStyleBackColor = true;
            this.kavRadioButton.CheckedChanged += new System.EventHandler(this.kavRadioButton_CheckedChanged);
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
            this.kisRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(205)))), ((int)(((byte)(72)))));
            this.kisRadioButton.ForeColor = System.Drawing.Color.Black;
            this.kisRadioButton.Name = "kisRadioButton";
            this.toolTip.SetToolTip(this.kisRadioButton, resources.GetString("kisRadioButton.ToolTip"));
            this.kisRadioButton.UseVisualStyleBackColor = true;
            this.kisRadioButton.CheckedChanged += new System.EventHandler(this.kisRadioButton_CheckedChanged);
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
            this.ktsRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(205)))), ((int)(((byte)(72)))));
            this.ktsRadioButton.ForeColor = System.Drawing.Color.Black;
            this.ktsRadioButton.Name = "ktsRadioButton";
            this.toolTip.SetToolTip(this.ktsRadioButton, resources.GetString("ktsRadioButton.ToolTip"));
            this.ktsRadioButton.UseVisualStyleBackColor = true;
            this.ktsRadioButton.CheckedChanged += new System.EventHandler(this.ktsRadioButton_CheckedChanged);
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
            this.configButton.BackColor = System.Drawing.SystemColors.Control;
            this.configButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.configButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlText;
            this.configButton.Name = "configButton";
            this.toolTip.SetToolTip(this.configButton, resources.GetString("configButton.ToolTip"));
            this.configButton.UseVisualStyleBackColor = false;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // defaultInstallButton
            // 
            resources.ApplyResources(this.defaultInstallButton, "defaultInstallButton");
            this.defaultInstallButton.BackColor = System.Drawing.Color.Transparent;
            this.defaultInstallButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.defaultInstallButton.Name = "defaultInstallButton";
            this.toolTip.SetToolTip(this.defaultInstallButton, resources.GetString("defaultInstallButton.ToolTip"));
            this.defaultInstallButton.UseVisualStyleBackColor = false;
            this.defaultInstallButton.Click += new System.EventHandler(this.defaultInstallButton_Click);
            // 
            // autoInstallButton
            // 
            resources.ApplyResources(this.autoInstallButton, "autoInstallButton");
            this.autoInstallButton.BackColor = System.Drawing.Color.Transparent;
            this.autoInstallButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.autoInstallButton.Name = "autoInstallButton";
            this.toolTip.SetToolTip(this.autoInstallButton, resources.GetString("autoInstallButton.ToolTip"));
            this.autoInstallButton.UseVisualStyleBackColor = false;
            this.autoInstallButton.Click += new System.EventHandler(this.autoInstallButton_Click);
            // 
            // topPanel
            // 
            resources.ApplyResources(this.topPanel, "topPanel");
            this.topPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.topPanel.Controls.Add(this.separator1);
            this.topPanel.Controls.Add(this.comparisionButton);
            this.topPanel.Controls.Add(this.githubButton);
            this.topPanel.Controls.Add(this.configButton);
            this.topPanel.Name = "topPanel";
            this.toolTip.SetToolTip(this.topPanel, resources.GetString("topPanel.ToolTip"));
            // 
            // separator1
            // 
            resources.ApplyResources(this.separator1, "separator1");
            this.separator1.Name = "separator1";
            this.toolTip.SetToolTip(this.separator1, resources.GetString("separator1.ToolTip"));
            // 
            // comparisionButton
            // 
            resources.ApplyResources(this.comparisionButton, "comparisionButton");
            this.comparisionButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comparisionButton.FlatAppearance.BorderSize = 0;
            this.comparisionButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.comparisionButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.comparisionButton.ForeColor = System.Drawing.Color.Blue;
            this.comparisionButton.Name = "comparisionButton";
            this.toolTip.SetToolTip(this.comparisionButton, resources.GetString("comparisionButton.ToolTip"));
            this.comparisionButton.UseVisualStyleBackColor = true;
            this.comparisionButton.Click += new System.EventHandler(this.comparisionButton_Click);
            // 
            // kavActivationButton
            // 
            resources.ApplyResources(this.kavActivationButton, "kavActivationButton");
            this.kavActivationButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.kavActivationButton.Name = "kavActivationButton";
            this.toolTip.SetToolTip(this.kavActivationButton, resources.GetString("kavActivationButton.ToolTip"));
            this.kavActivationButton.UseVisualStyleBackColor = true;
            // 
            // kisActivationButton
            // 
            resources.ApplyResources(this.kisActivationButton, "kisActivationButton");
            this.kisActivationButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.kisActivationButton.Name = "kisActivationButton";
            this.toolTip.SetToolTip(this.kisActivationButton, resources.GetString("kisActivationButton.ToolTip"));
            this.kisActivationButton.UseVisualStyleBackColor = true;
            // 
            // ktsActivationButton
            // 
            resources.ApplyResources(this.ktsActivationButton, "ktsActivationButton");
            this.ktsActivationButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ktsActivationButton.Name = "ktsActivationButton";
            this.toolTip.SetToolTip(this.ktsActivationButton, resources.GetString("ktsActivationButton.ToolTip"));
            this.ktsActivationButton.UseVisualStyleBackColor = true;
            // 
            // radioPanel
            // 
            resources.ApplyResources(this.radioPanel, "radioPanel");
            this.radioPanel.Controls.Add(this.ktsActivationButton);
            this.radioPanel.Controls.Add(this.ktsAvailableLicensesLabel);
            this.radioPanel.Controls.Add(this.kisAvailableLicensesLabel);
            this.radioPanel.Controls.Add(this.ktsRadioButton);
            this.radioPanel.Controls.Add(this.kisActivationButton);
            this.radioPanel.Controls.Add(this.kavAvailableLicensesLabel);
            this.radioPanel.Controls.Add(this.kavActivationButton);
            this.radioPanel.Controls.Add(this.kisRadioButton);
            this.radioPanel.Controls.Add(this.kavRadioButton);
            this.radioPanel.Name = "radioPanel";
            this.toolTip.SetToolTip(this.radioPanel, resources.GetString("radioPanel.ToolTip"));
            // 
            // buttonsPanel
            // 
            resources.ApplyResources(this.buttonsPanel, "buttonsPanel");
            this.buttonsPanel.Controls.Add(this.databaseNotAccesibleLabel);
            this.buttonsPanel.Controls.Add(this.defaultInstallButton);
            this.buttonsPanel.Controls.Add(this.autoInstallButton);
            this.buttonsPanel.Name = "buttonsPanel";
            this.toolTip.SetToolTip(this.buttonsPanel, resources.GetString("buttonsPanel.ToolTip"));
            // 
            // databaseNotAccesibleLabel
            // 
            resources.ApplyResources(this.databaseNotAccesibleLabel, "databaseNotAccesibleLabel");
            this.databaseNotAccesibleLabel.BackColor = System.Drawing.Color.White;
            this.databaseNotAccesibleLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.databaseNotAccesibleLabel.Name = "databaseNotAccesibleLabel";
            this.toolTip.SetToolTip(this.databaseNotAccesibleLabel, resources.GetString("databaseNotAccesibleLabel.ToolTip"));
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.buttonsPanel);
            this.Controls.Add(this.radioPanel);
            this.Controls.Add(this.topPanel);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.radioPanel.ResumeLayout(false);
            this.radioPanel.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            this.buttonsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button githubButton;
        private ToolTip toolTip;
        private RadioButton kavRadioButton;
        private Label kavAvailableLicensesLabel;
        private Label kisAvailableLicensesLabel;
        private RadioButton kisRadioButton;
        private Label ktsAvailableLicensesLabel;
        private RadioButton ktsRadioButton;
        private Button configButton;
        private Button autoInstallButton;
        private Panel topPanel;
        private Button kavActivationButton;
        private Button kisActivationButton;
        private Button ktsActivationButton;
        private Panel radioPanel;
        private Panel buttonsPanel;
        private Label databaseNotAccesibleLabel;
        private Button comparisionButton;
        private Label separator1;
        private Button defaultInstallButton;
    }
}