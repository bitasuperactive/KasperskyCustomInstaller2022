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
            this.keepKasperskyConfigCheckBox = new System.Windows.Forms.CheckBox();
            this.offlineSetupCheckBox = new System.Windows.Forms.CheckBox();
            this.doNotUseDatabaseLicensesCheckBox = new System.Windows.Forms.CheckBox();
            this.kasperskySecureConnectionCheckBox = new System.Windows.Forms.CheckBox();
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
            // keepKasperskyConfigCheckBox
            // 
            this.keepKasperskyConfigCheckBox.Checked = true;
            this.keepKasperskyConfigCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keepKasperskyConfigCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.keepKasperskyConfigCheckBox.Location = new System.Drawing.Point(10, 10);
            this.keepKasperskyConfigCheckBox.Name = "keepKasperskyConfigCheckBox";
            this.keepKasperskyConfigCheckBox.Size = new System.Drawing.Size(506, 26);
            this.keepKasperskyConfigCheckBox.TabIndex = 1;
            this.keepKasperskyConfigCheckBox.Text = "Mantener configuración de Kaspersky";
            this.keepKasperskyConfigCheckBox.UseVisualStyleBackColor = true;
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
            // doNotUseDatabaseLicensesCheckBox
            // 
            this.doNotUseDatabaseLicensesCheckBox.Checked = true;
            this.doNotUseDatabaseLicensesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.doNotUseDatabaseLicensesCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.doNotUseDatabaseLicensesCheckBox.Location = new System.Drawing.Point(10, 62);
            this.doNotUseDatabaseLicensesCheckBox.Name = "doNotUseDatabaseLicensesCheckBox";
            this.doNotUseDatabaseLicensesCheckBox.Size = new System.Drawing.Size(506, 26);
            this.doNotUseDatabaseLicensesCheckBox.TabIndex = 3;
            this.doNotUseDatabaseLicensesCheckBox.Text = "Solo renovar una menusalidad";
            this.doNotUseDatabaseLicensesCheckBox.UseVisualStyleBackColor = true;
            // 
            // kasperskySecureConnectionCheckBox
            // 
            this.kasperskySecureConnectionCheckBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.kasperskySecureConnectionCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.kasperskySecureConnectionCheckBox.Location = new System.Drawing.Point(10, 88);
            this.kasperskySecureConnectionCheckBox.Name = "kasperskySecureConnectionCheckBox";
            this.kasperskySecureConnectionCheckBox.Size = new System.Drawing.Size(506, 26);
            this.kasperskySecureConnectionCheckBox.TabIndex = 4;
            this.kasperskySecureConnectionCheckBox.Text = "Instalar Kaspersky Secure Connection";
            this.kasperskySecureConnectionCheckBox.UseVisualStyleBackColor = false;
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
            this.checkBoxesPanel.Controls.Add(this.kasperskySecureConnectionCheckBox);
            this.checkBoxesPanel.Controls.Add(this.doNotUseDatabaseLicensesCheckBox);
            this.checkBoxesPanel.Controls.Add(this.offlineSetupCheckBox);
            this.checkBoxesPanel.Controls.Add(this.keepKasperskyConfigCheckBox);
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
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            this.buttonsPanel.ResumeLayout(false);
            this.checkBoxesPanel.ResumeLayout(false);
            this.titlePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CheckBox keepKasperskyConfigCheckBox;
        private CheckBox offlineSetupCheckBox;
        private CheckBox doNotUseDatabaseLicensesCheckBox;
        private CheckBox kasperskySecureConnectionCheckBox;
        private Button cancelButton;
        private Button applyButton;
        private Panel buttonsPanel;
        private Panel checkBoxesPanel;
        private Label titleLabel;
        private Panel titlePanel;
    }
}