namespace KCI_UI
{
    partial class ConfigurationForm
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
            this.keepKasConfigCheckBox = new System.Windows.Forms.CheckBox();
            this.offlineSetupCheckBox = new System.Windows.Forms.CheckBox();
            this.justUseDefaultLicenseCheckBox = new System.Windows.Forms.CheckBox();
            this.installKscCheckBox = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.checkBoxesPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.buttonsPanel.SuspendLayout();
            this.checkBoxesPanel.SuspendLayout();
            this.titlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // keepKasConfigCheckBox
            // 
            this.keepKasConfigCheckBox.Checked = true;
            this.keepKasConfigCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keepKasConfigCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.keepKasConfigCheckBox.Location = new System.Drawing.Point(10, 10);
            this.keepKasConfigCheckBox.Name = "keepKasConfigCheckBox";
            this.keepKasConfigCheckBox.Size = new System.Drawing.Size(506, 26);
            this.keepKasConfigCheckBox.TabIndex = 1;
            this.keepKasConfigCheckBox.Text = "Mantener configuración de Kaspersky";
            this.keepKasConfigCheckBox.UseVisualStyleBackColor = true;
            // 
            // offlineSetupCheckBox
            // 
            this.offlineSetupCheckBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.offlineSetupCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.offlineSetupCheckBox.Location = new System.Drawing.Point(10, 36);
            this.offlineSetupCheckBox.Name = "offlineSetupCheckBox";
            this.offlineSetupCheckBox.Size = new System.Drawing.Size(506, 26);
            this.offlineSetupCheckBox.TabIndex = 2;
            this.offlineSetupCheckBox.Text = "Asistente de instalación completo (offline)";
            this.offlineSetupCheckBox.UseVisualStyleBackColor = false;
            // 
            // justUseDefaultLicenseCheckBox
            // 
            this.justUseDefaultLicenseCheckBox.Checked = true;
            this.justUseDefaultLicenseCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.justUseDefaultLicenseCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.justUseDefaultLicenseCheckBox.Location = new System.Drawing.Point(10, 62);
            this.justUseDefaultLicenseCheckBox.Name = "justUseDefaultLicenseCheckBox";
            this.justUseDefaultLicenseCheckBox.Size = new System.Drawing.Size(506, 26);
            this.justUseDefaultLicenseCheckBox.TabIndex = 3;
            this.justUseDefaultLicenseCheckBox.Text = "Solo renovar una menusalidad";
            this.justUseDefaultLicenseCheckBox.UseVisualStyleBackColor = true;
            // 
            // installKscCheckBox
            // 
            this.installKscCheckBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.installKscCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.installKscCheckBox.Location = new System.Drawing.Point(10, 88);
            this.installKscCheckBox.Name = "installKscCheckBox";
            this.installKscCheckBox.Size = new System.Drawing.Size(506, 26);
            this.installKscCheckBox.TabIndex = 4;
            this.installKscCheckBox.Text = "Instalar Kaspersky Secure Connection";
            this.installKscCheckBox.UseVisualStyleBackColor = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancelButton.Location = new System.Drawing.Point(384, 10);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(132, 35);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancelar";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.applyButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.applyButton.Location = new System.Drawing.Point(252, 10);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(132, 35);
            this.applyButton.TabIndex = 6;
            this.applyButton.Text = "Aplicar";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonsPanel.Controls.Add(this.applyButton);
            this.buttonsPanel.Controls.Add(this.cancelButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonsPanel.Location = new System.Drawing.Point(0, 207);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Padding = new System.Windows.Forms.Padding(10);
            this.buttonsPanel.Size = new System.Drawing.Size(526, 55);
            this.buttonsPanel.TabIndex = 7;
            // 
            // checkBoxesPanel
            // 
            this.checkBoxesPanel.Controls.Add(this.installKscCheckBox);
            this.checkBoxesPanel.Controls.Add(this.justUseDefaultLicenseCheckBox);
            this.checkBoxesPanel.Controls.Add(this.offlineSetupCheckBox);
            this.checkBoxesPanel.Controls.Add(this.keepKasConfigCheckBox);
            this.checkBoxesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxesPanel.Location = new System.Drawing.Point(0, 68);
            this.checkBoxesPanel.Name = "checkBoxesPanel";
            this.checkBoxesPanel.Padding = new System.Windows.Forms.Padding(10);
            this.checkBoxesPanel.Size = new System.Drawing.Size(526, 139);
            this.checkBoxesPanel.TabIndex = 8;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Font = new System.Drawing.Font("Calibri", 12.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.Location = new System.Drawing.Point(10, 10);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(506, 48);
            this.titleLabel.TabIndex = 9;
            this.titleLabel.Text = "Configuración de la instalación";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // titlePanel
            // 
            this.titlePanel.Controls.Add(this.titleLabel);
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel.Location = new System.Drawing.Point(0, 0);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Padding = new System.Windows.Forms.Padding(10);
            this.titlePanel.Size = new System.Drawing.Size(526, 68);
            this.titlePanel.TabIndex = 10;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(526, 262);
            this.Controls.Add(this.checkBoxesPanel);
            this.Controls.Add(this.buttonsPanel);
            this.Controls.Add(this.titlePanel);
            this.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kaspersky Custom Installer";
            this.buttonsPanel.ResumeLayout(false);
            this.checkBoxesPanel.ResumeLayout(false);
            this.titlePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CheckBox keepKasConfigCheckBox;
        private CheckBox offlineSetupCheckBox;
        private CheckBox justUseDefaultLicenseCheckBox;
        private CheckBox installKscCheckBox;
        private Button cancelButton;
        private Button applyButton;
        private Panel buttonsPanel;
        private Panel checkBoxesPanel;
        private Label titleLabel;
        private Panel titlePanel;
    }
}