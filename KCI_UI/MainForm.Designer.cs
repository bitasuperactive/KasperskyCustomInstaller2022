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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            githubButton = new Button();
            toolTip = new ToolTip(components);
            kavRadioButton = new RadioButton();
            kavAvailableLicensesLabel = new Label();
            kisRadioButton = new RadioButton();
            kisAvailableLicensesLabel = new Label();
            ktsRadioButton = new RadioButton();
            ktsAvailableLicensesLabel = new Label();
            configurationButton = new Button();
            defaultInstallationButton = new Button();
            autoInstallationButton = new Button();
            topPanel = new Panel();
            separator1 = new Label();
            productComparisionButton = new Button();
            kavActivationButton = new Button();
            kisActivationButton = new Button();
            ktsActivationButton = new Button();
            radioPanel = new Panel();
            buttonsPanel = new Panel();
            loadingPanel = new Panel();
            loadingLabel = new Label();
            topPanel.SuspendLayout();
            radioPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            loadingPanel.SuspendLayout();
            SuspendLayout();
            // 
            // githubButton
            // 
            resources.ApplyResources(githubButton, "githubButton");
            githubButton.Cursor = Cursors.Hand;
            githubButton.FlatAppearance.BorderSize = 0;
            githubButton.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            githubButton.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
            githubButton.ForeColor = Color.Blue;
            githubButton.Name = "githubButton";
            toolTip.SetToolTip(githubButton, resources.GetString("githubButton.ToolTip"));
            githubButton.UseVisualStyleBackColor = true;
            githubButton.Click += githubButton_Click;
            // 
            // kavRadioButton
            // 
            resources.ApplyResources(kavRadioButton, "kavRadioButton");
            kavRadioButton.FlatAppearance.BorderSize = 0;
            kavRadioButton.FlatAppearance.CheckedBackColor = Color.FromArgb(232, 205, 72);
            kavRadioButton.ForeColor = Color.Black;
            kavRadioButton.Name = "kavRadioButton";
            toolTip.SetToolTip(kavRadioButton, resources.GetString("kavRadioButton.ToolTip"));
            kavRadioButton.UseVisualStyleBackColor = true;
            kavRadioButton.CheckedChanged += kavRadioButton_CheckedChanged;
            // 
            // kavAvailableLicensesLabel
            // 
            resources.ApplyResources(kavAvailableLicensesLabel, "kavAvailableLicensesLabel");
            kavAvailableLicensesLabel.BackColor = Color.Transparent;
            kavAvailableLicensesLabel.ForeColor = Color.DarkRed;
            kavAvailableLicensesLabel.Name = "kavAvailableLicensesLabel";
            toolTip.SetToolTip(kavAvailableLicensesLabel, resources.GetString("kavAvailableLicensesLabel.ToolTip"));
            // 
            // kisRadioButton
            // 
            resources.ApplyResources(kisRadioButton, "kisRadioButton");
            kisRadioButton.FlatAppearance.BorderSize = 0;
            kisRadioButton.FlatAppearance.CheckedBackColor = Color.FromArgb(232, 205, 72);
            kisRadioButton.ForeColor = Color.Black;
            kisRadioButton.Name = "kisRadioButton";
            toolTip.SetToolTip(kisRadioButton, resources.GetString("kisRadioButton.ToolTip"));
            kisRadioButton.UseVisualStyleBackColor = true;
            kisRadioButton.CheckedChanged += kisRadioButton_CheckedChanged;
            // 
            // kisAvailableLicensesLabel
            // 
            resources.ApplyResources(kisAvailableLicensesLabel, "kisAvailableLicensesLabel");
            kisAvailableLicensesLabel.BackColor = Color.Transparent;
            kisAvailableLicensesLabel.ForeColor = Color.DarkRed;
            kisAvailableLicensesLabel.Name = "kisAvailableLicensesLabel";
            toolTip.SetToolTip(kisAvailableLicensesLabel, resources.GetString("kisAvailableLicensesLabel.ToolTip"));
            // 
            // ktsRadioButton
            // 
            resources.ApplyResources(ktsRadioButton, "ktsRadioButton");
            ktsRadioButton.FlatAppearance.BorderSize = 0;
            ktsRadioButton.FlatAppearance.CheckedBackColor = Color.FromArgb(232, 205, 72);
            ktsRadioButton.ForeColor = Color.Black;
            ktsRadioButton.Name = "ktsRadioButton";
            toolTip.SetToolTip(ktsRadioButton, resources.GetString("ktsRadioButton.ToolTip"));
            ktsRadioButton.UseVisualStyleBackColor = true;
            ktsRadioButton.CheckedChanged += ktsRadioButton_CheckedChanged;
            // 
            // ktsAvailableLicensesLabel
            // 
            resources.ApplyResources(ktsAvailableLicensesLabel, "ktsAvailableLicensesLabel");
            ktsAvailableLicensesLabel.BackColor = Color.Transparent;
            ktsAvailableLicensesLabel.ForeColor = Color.DarkRed;
            ktsAvailableLicensesLabel.Name = "ktsAvailableLicensesLabel";
            toolTip.SetToolTip(ktsAvailableLicensesLabel, resources.GetString("ktsAvailableLicensesLabel.ToolTip"));
            // 
            // configurationButton
            // 
            resources.ApplyResources(configurationButton, "configurationButton");
            configurationButton.BackColor = SystemColors.Control;
            configurationButton.Cursor = Cursors.Hand;
            configurationButton.FlatAppearance.BorderColor = SystemColors.ControlText;
            configurationButton.Name = "configurationButton";
            toolTip.SetToolTip(configurationButton, resources.GetString("configurationButton.ToolTip"));
            configurationButton.UseVisualStyleBackColor = false;
            configurationButton.Click += configurationButton_Click;
            // 
            // defaultInstallationButton
            // 
            resources.ApplyResources(defaultInstallationButton, "defaultInstallationButton");
            defaultInstallationButton.BackColor = Color.Transparent;
            defaultInstallationButton.Cursor = Cursors.Hand;
            defaultInstallationButton.Name = "defaultInstallationButton";
            toolTip.SetToolTip(defaultInstallationButton, resources.GetString("defaultInstallationButton.ToolTip"));
            defaultInstallationButton.UseVisualStyleBackColor = false;
            defaultInstallationButton.Click += defaultInstallationButton_Click;
            // 
            // autoInstallationButton
            // 
            resources.ApplyResources(autoInstallationButton, "autoInstallationButton");
            autoInstallationButton.BackColor = Color.Transparent;
            autoInstallationButton.Cursor = Cursors.Hand;
            autoInstallationButton.Name = "autoInstallationButton";
            toolTip.SetToolTip(autoInstallationButton, resources.GetString("autoInstallationButton.ToolTip"));
            autoInstallationButton.UseVisualStyleBackColor = false;
            autoInstallationButton.Click += autoInstallationButton_Click;
            // 
            // topPanel
            // 
            resources.ApplyResources(topPanel, "topPanel");
            topPanel.BackColor = SystemColors.ControlLight;
            topPanel.Controls.Add(separator1);
            topPanel.Controls.Add(productComparisionButton);
            topPanel.Controls.Add(githubButton);
            topPanel.Controls.Add(configurationButton);
            topPanel.Name = "topPanel";
            toolTip.SetToolTip(topPanel, resources.GetString("topPanel.ToolTip"));
            // 
            // separator1
            // 
            resources.ApplyResources(separator1, "separator1");
            separator1.ForeColor = Color.DimGray;
            separator1.Name = "separator1";
            toolTip.SetToolTip(separator1, resources.GetString("separator1.ToolTip"));
            // 
            // productComparisionButton
            // 
            resources.ApplyResources(productComparisionButton, "productComparisionButton");
            productComparisionButton.Cursor = Cursors.Hand;
            productComparisionButton.FlatAppearance.BorderSize = 0;
            productComparisionButton.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            productComparisionButton.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
            productComparisionButton.ForeColor = Color.Blue;
            productComparisionButton.Name = "productComparisionButton";
            toolTip.SetToolTip(productComparisionButton, resources.GetString("productComparisionButton.ToolTip"));
            productComparisionButton.UseVisualStyleBackColor = true;
            productComparisionButton.Click += productComparisionButton_Click;
            // 
            // kavActivationButton
            // 
            resources.ApplyResources(kavActivationButton, "kavActivationButton");
            kavActivationButton.Cursor = Cursors.Hand;
            kavActivationButton.Name = "kavActivationButton";
            toolTip.SetToolTip(kavActivationButton, resources.GetString("kavActivationButton.ToolTip"));
            kavActivationButton.UseVisualStyleBackColor = true;
            // 
            // kisActivationButton
            // 
            resources.ApplyResources(kisActivationButton, "kisActivationButton");
            kisActivationButton.Cursor = Cursors.Hand;
            kisActivationButton.Name = "kisActivationButton";
            toolTip.SetToolTip(kisActivationButton, resources.GetString("kisActivationButton.ToolTip"));
            kisActivationButton.UseVisualStyleBackColor = true;
            // 
            // ktsActivationButton
            // 
            resources.ApplyResources(ktsActivationButton, "ktsActivationButton");
            ktsActivationButton.Cursor = Cursors.Hand;
            ktsActivationButton.Name = "ktsActivationButton";
            toolTip.SetToolTip(ktsActivationButton, resources.GetString("ktsActivationButton.ToolTip"));
            ktsActivationButton.UseVisualStyleBackColor = true;
            // 
            // radioPanel
            // 
            resources.ApplyResources(radioPanel, "radioPanel");
            radioPanel.Controls.Add(ktsActivationButton);
            radioPanel.Controls.Add(ktsAvailableLicensesLabel);
            radioPanel.Controls.Add(kisAvailableLicensesLabel);
            radioPanel.Controls.Add(ktsRadioButton);
            radioPanel.Controls.Add(kisActivationButton);
            radioPanel.Controls.Add(kavAvailableLicensesLabel);
            radioPanel.Controls.Add(kavActivationButton);
            radioPanel.Controls.Add(kisRadioButton);
            radioPanel.Controls.Add(kavRadioButton);
            radioPanel.Name = "radioPanel";
            toolTip.SetToolTip(radioPanel, resources.GetString("radioPanel.ToolTip"));
            // 
            // buttonsPanel
            // 
            resources.ApplyResources(buttonsPanel, "buttonsPanel");
            buttonsPanel.Controls.Add(defaultInstallationButton);
            buttonsPanel.Controls.Add(autoInstallationButton);
            buttonsPanel.Name = "buttonsPanel";
            toolTip.SetToolTip(buttonsPanel, resources.GetString("buttonsPanel.ToolTip"));
            // 
            // loadingPanel
            // 
            resources.ApplyResources(loadingPanel, "loadingPanel");
            loadingPanel.Controls.Add(loadingLabel);
            loadingPanel.Name = "loadingPanel";
            toolTip.SetToolTip(loadingPanel, resources.GetString("loadingPanel.ToolTip"));
            // 
            // loadingLabel
            // 
            resources.ApplyResources(loadingLabel, "loadingLabel");
            loadingLabel.ForeColor = Color.Black;
            loadingLabel.Name = "loadingLabel";
            toolTip.SetToolTip(loadingLabel, resources.GetString("loadingLabel.ToolTip"));
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(buttonsPanel);
            Controls.Add(radioPanel);
            Controls.Add(topPanel);
            Controls.Add(loadingPanel);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            radioPanel.ResumeLayout(false);
            radioPanel.PerformLayout();
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            loadingPanel.ResumeLayout(false);
            ResumeLayout(false);
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
        private Button configurationButton;
        private Button autoInstallationButton;
        private Panel topPanel;
        private Button kavActivationButton;
        private Button kisActivationButton;
        private Button ktsActivationButton;
        private Panel radioPanel;
        private Panel buttonsPanel;
        private Button productComparisionButton;
        private Label separator1;
        private Button defaultInstallationButton;
        private Panel loadingPanel;
        private Label loadingLabel;
    }
}