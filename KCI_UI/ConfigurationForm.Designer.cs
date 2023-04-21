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
            components = new System.ComponentModel.Container();
            keepKasperskyConfigCheckBox = new CheckBox();
            offlineSetupCheckBox = new CheckBox();
            doNotUseDatabaseLicensesCheckBox = new CheckBox();
            kasperskySecureConnectionCheckBox = new CheckBox();
            cancelButton = new Button();
            applyButton = new Button();
            buttonsPanel = new Panel();
            checkBoxesPanel = new Panel();
            titleLabel = new Label();
            titlePanel = new Panel();
            toolTip1 = new ToolTip(components);
            buttonsPanel.SuspendLayout();
            checkBoxesPanel.SuspendLayout();
            titlePanel.SuspendLayout();
            SuspendLayout();
            // 
            // keepKasperskyConfigCheckBox
            // 
            keepKasperskyConfigCheckBox.Dock = DockStyle.Top;
            keepKasperskyConfigCheckBox.Location = new Point(10, 10);
            keepKasperskyConfigCheckBox.Name = "keepKasperskyConfigCheckBox";
            keepKasperskyConfigCheckBox.Size = new Size(350, 26);
            keepKasperskyConfigCheckBox.TabIndex = 1;
            keepKasperskyConfigCheckBox.Text = "Mantener configuración de Kaspersky";
            keepKasperskyConfigCheckBox.UseVisualStyleBackColor = true;
            // 
            // offlineSetupCheckBox
            // 
            offlineSetupCheckBox.BackColor = Color.WhiteSmoke;
            offlineSetupCheckBox.Dock = DockStyle.Top;
            offlineSetupCheckBox.Location = new Point(10, 36);
            offlineSetupCheckBox.Name = "offlineSetupCheckBox";
            offlineSetupCheckBox.Size = new Size(350, 26);
            offlineSetupCheckBox.TabIndex = 2;
            offlineSetupCheckBox.Text = "Asistente de instalación completo (offline)";
            toolTip1.SetToolTip(offlineSetupCheckBox, "Recomendado si el instalador habitual da problemas.");
            offlineSetupCheckBox.UseVisualStyleBackColor = false;
            // 
            // doNotUseDatabaseLicensesCheckBox
            // 
            doNotUseDatabaseLicensesCheckBox.Checked = true;
            doNotUseDatabaseLicensesCheckBox.CheckState = CheckState.Checked;
            doNotUseDatabaseLicensesCheckBox.Dock = DockStyle.Top;
            doNotUseDatabaseLicensesCheckBox.Location = new Point(10, 62);
            doNotUseDatabaseLicensesCheckBox.Name = "doNotUseDatabaseLicensesCheckBox";
            doNotUseDatabaseLicensesCheckBox.Size = new Size(350, 26);
            doNotUseDatabaseLicensesCheckBox.TabIndex = 3;
            doNotUseDatabaseLicensesCheckBox.Text = "Solo renovar una menusalidad";
            doNotUseDatabaseLicensesCheckBox.UseVisualStyleBackColor = true;
            // 
            // kasperskySecureConnectionCheckBox
            // 
            kasperskySecureConnectionCheckBox.BackColor = Color.WhiteSmoke;
            kasperskySecureConnectionCheckBox.Dock = DockStyle.Top;
            kasperskySecureConnectionCheckBox.Location = new Point(10, 88);
            kasperskySecureConnectionCheckBox.Name = "kasperskySecureConnectionCheckBox";
            kasperskySecureConnectionCheckBox.Size = new Size(350, 26);
            kasperskySecureConnectionCheckBox.TabIndex = 4;
            kasperskySecureConnectionCheckBox.Text = "Instalar Kaspersky Secure Connection (VPN)";
            kasperskySecureConnectionCheckBox.UseVisualStyleBackColor = false;
            // 
            // cancelButton
            // 
            cancelButton.Cursor = Cursors.Hand;
            cancelButton.Dock = DockStyle.Right;
            cancelButton.Location = new Point(228, 10);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(132, 35);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Cancelar";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // applyButton
            // 
            applyButton.Cursor = Cursors.Hand;
            applyButton.Dock = DockStyle.Right;
            applyButton.Location = new Point(96, 10);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(132, 35);
            applyButton.TabIndex = 6;
            applyButton.Text = "Aplicar";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // buttonsPanel
            // 
            buttonsPanel.BackColor = Color.Gainsboro;
            buttonsPanel.Controls.Add(applyButton);
            buttonsPanel.Controls.Add(cancelButton);
            buttonsPanel.Dock = DockStyle.Bottom;
            buttonsPanel.Location = new Point(0, 207);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Padding = new Padding(10);
            buttonsPanel.Size = new Size(370, 55);
            buttonsPanel.TabIndex = 7;
            // 
            // checkBoxesPanel
            // 
            checkBoxesPanel.Controls.Add(kasperskySecureConnectionCheckBox);
            checkBoxesPanel.Controls.Add(doNotUseDatabaseLicensesCheckBox);
            checkBoxesPanel.Controls.Add(offlineSetupCheckBox);
            checkBoxesPanel.Controls.Add(keepKasperskyConfigCheckBox);
            checkBoxesPanel.Dock = DockStyle.Fill;
            checkBoxesPanel.Location = new Point(0, 68);
            checkBoxesPanel.Name = "checkBoxesPanel";
            checkBoxesPanel.Padding = new Padding(10);
            checkBoxesPanel.Size = new Size(370, 139);
            checkBoxesPanel.TabIndex = 8;
            // 
            // titleLabel
            // 
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.Font = new Font("Calibri", 12.8F, FontStyle.Regular, GraphicsUnit.Point);
            titleLabel.Location = new Point(10, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(350, 48);
            titleLabel.TabIndex = 9;
            titleLabel.Text = "Configuración de la instalación";
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // titlePanel
            // 
            titlePanel.Controls.Add(titleLabel);
            titlePanel.Dock = DockStyle.Top;
            titlePanel.Location = new Point(0, 0);
            titlePanel.Name = "titlePanel";
            titlePanel.Padding = new Padding(10);
            titlePanel.Size = new Size(370, 68);
            titlePanel.TabIndex = 10;
            // 
            // ConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            ClientSize = new Size(370, 262);
            Controls.Add(checkBoxesPanel);
            Controls.Add(buttonsPanel);
            Controls.Add(titlePanel);
            Font = new Font("Calibri", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            HelpButton = true;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfigurationForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Kaspersky Custom Installer";
            Load += ConfigurationForm_Load;
            buttonsPanel.ResumeLayout(false);
            checkBoxesPanel.ResumeLayout(false);
            titlePanel.ResumeLayout(false);
            ResumeLayout(false);
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
        private ToolTip toolTip1;
    }
}