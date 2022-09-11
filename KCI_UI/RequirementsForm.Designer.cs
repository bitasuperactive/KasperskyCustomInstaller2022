namespace KCI_UI
{
    partial class RequirementsForm
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
            this.adminPanel = new System.Windows.Forms.Panel();
            this.restartAsAdminButton = new System.Windows.Forms.Button();
            this.adminLabel = new System.Windows.Forms.Label();
            this.passwordProtectionRequirementPanel = new System.Windows.Forms.Panel();
            this.passwordProtectionMoreInfoButton = new System.Windows.Forms.Button();
            this.passwordProtectionDisabledLabel = new System.Windows.Forms.Label();
            this.kasClosedRequirementPanel = new System.Windows.Forms.Panel();
            this.kasClosedMoreInfoButton = new System.Windows.Forms.Button();
            this.kasClosedLabel = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.adminPanel.SuspendLayout();
            this.passwordProtectionRequirementPanel.SuspendLayout();
            this.kasClosedRequirementPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.titlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // adminPanel
            // 
            this.adminPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminPanel.Controls.Add(this.restartAsAdminButton);
            this.adminPanel.Controls.Add(this.adminLabel);
            this.adminPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.adminPanel.Location = new System.Drawing.Point(0, 99);
            this.adminPanel.Name = "adminPanel";
            this.adminPanel.Padding = new System.Windows.Forms.Padding(10);
            this.adminPanel.Size = new System.Drawing.Size(424, 79);
            this.adminPanel.TabIndex = 20;
            this.adminPanel.Visible = false;
            // 
            // restartAsAdminButton
            // 
            this.restartAsAdminButton.AutoSize = true;
            this.restartAsAdminButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.restartAsAdminButton.BackColor = System.Drawing.Color.Transparent;
            this.restartAsAdminButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.restartAsAdminButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.restartAsAdminButton.FlatAppearance.BorderSize = 0;
            this.restartAsAdminButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.restartAsAdminButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.restartAsAdminButton.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.restartAsAdminButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.restartAsAdminButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.restartAsAdminButton.Location = new System.Drawing.Point(10, 35);
            this.restartAsAdminButton.Name = "restartAsAdminButton";
            this.restartAsAdminButton.Size = new System.Drawing.Size(404, 31);
            this.restartAsAdminButton.TabIndex = 3;
            this.restartAsAdminButton.Text = "Solicitar";
            this.restartAsAdminButton.UseVisualStyleBackColor = false;
            this.restartAsAdminButton.Click += new System.EventHandler(this.restartAsAdminButton_Click);
            // 
            // adminLabel
            // 
            this.adminLabel.BackColor = System.Drawing.Color.Yellow;
            this.adminLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.adminLabel.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.adminLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.adminLabel.Location = new System.Drawing.Point(10, 10);
            this.adminLabel.Name = "adminLabel";
            this.adminLabel.Size = new System.Drawing.Size(404, 25);
            this.adminLabel.TabIndex = 0;
            this.adminLabel.Text = "Permisos de administrador.";
            // 
            // passwordProtectionRequirementPanel
            // 
            this.passwordProtectionRequirementPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.passwordProtectionRequirementPanel.Controls.Add(this.passwordProtectionMoreInfoButton);
            this.passwordProtectionRequirementPanel.Controls.Add(this.passwordProtectionDisabledLabel);
            this.passwordProtectionRequirementPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordProtectionRequirementPanel.Location = new System.Drawing.Point(0, 178);
            this.passwordProtectionRequirementPanel.Name = "passwordProtectionRequirementPanel";
            this.passwordProtectionRequirementPanel.Padding = new System.Windows.Forms.Padding(10);
            this.passwordProtectionRequirementPanel.Size = new System.Drawing.Size(424, 79);
            this.passwordProtectionRequirementPanel.TabIndex = 21;
            this.passwordProtectionRequirementPanel.Visible = false;
            // 
            // passwordProtectionMoreInfoButton
            // 
            this.passwordProtectionMoreInfoButton.AutoSize = true;
            this.passwordProtectionMoreInfoButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.passwordProtectionMoreInfoButton.BackColor = System.Drawing.Color.White;
            this.passwordProtectionMoreInfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.passwordProtectionMoreInfoButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordProtectionMoreInfoButton.FlatAppearance.BorderSize = 0;
            this.passwordProtectionMoreInfoButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.passwordProtectionMoreInfoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.passwordProtectionMoreInfoButton.Font = new System.Drawing.Font("Calibri", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.passwordProtectionMoreInfoButton.ForeColor = System.Drawing.Color.Blue;
            this.passwordProtectionMoreInfoButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.passwordProtectionMoreInfoButton.Location = new System.Drawing.Point(10, 35);
            this.passwordProtectionMoreInfoButton.Name = "passwordProtectionMoreInfoButton";
            this.passwordProtectionMoreInfoButton.Size = new System.Drawing.Size(404, 31);
            this.passwordProtectionMoreInfoButton.TabIndex = 2;
            this.passwordProtectionMoreInfoButton.Text = "Más información";
            this.passwordProtectionMoreInfoButton.UseVisualStyleBackColor = false;
            this.passwordProtectionMoreInfoButton.Click += new System.EventHandler(this.pwdProtectionMoreInfoButton_Click);
            // 
            // passwordProtectionDisabledLabel
            // 
            this.passwordProtectionDisabledLabel.BackColor = System.Drawing.Color.Yellow;
            this.passwordProtectionDisabledLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordProtectionDisabledLabel.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.passwordProtectionDisabledLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.passwordProtectionDisabledLabel.Location = new System.Drawing.Point(10, 10);
            this.passwordProtectionDisabledLabel.Name = "passwordProtectionDisabledLabel";
            this.passwordProtectionDisabledLabel.Size = new System.Drawing.Size(404, 25);
            this.passwordProtectionDisabledLabel.TabIndex = 0;
            this.passwordProtectionDisabledLabel.Text = "Deshabilitar Kaspersky Password Protection.";
            // 
            // kasClosedRequirementPanel
            // 
            this.kasClosedRequirementPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kasClosedRequirementPanel.Controls.Add(this.kasClosedMoreInfoButton);
            this.kasClosedRequirementPanel.Controls.Add(this.kasClosedLabel);
            this.kasClosedRequirementPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.kasClosedRequirementPanel.Location = new System.Drawing.Point(0, 257);
            this.kasClosedRequirementPanel.Name = "kasClosedRequirementPanel";
            this.kasClosedRequirementPanel.Padding = new System.Windows.Forms.Padding(10);
            this.kasClosedRequirementPanel.Size = new System.Drawing.Size(424, 79);
            this.kasClosedRequirementPanel.TabIndex = 22;
            this.kasClosedRequirementPanel.Visible = false;
            // 
            // kasClosedMoreInfoButton
            // 
            this.kasClosedMoreInfoButton.AutoSize = true;
            this.kasClosedMoreInfoButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kasClosedMoreInfoButton.BackColor = System.Drawing.Color.White;
            this.kasClosedMoreInfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.kasClosedMoreInfoButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.kasClosedMoreInfoButton.FlatAppearance.BorderSize = 0;
            this.kasClosedMoreInfoButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.kasClosedMoreInfoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.kasClosedMoreInfoButton.Font = new System.Drawing.Font("Calibri", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.kasClosedMoreInfoButton.ForeColor = System.Drawing.Color.Blue;
            this.kasClosedMoreInfoButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.kasClosedMoreInfoButton.Location = new System.Drawing.Point(10, 35);
            this.kasClosedMoreInfoButton.Name = "kasClosedMoreInfoButton";
            this.kasClosedMoreInfoButton.Size = new System.Drawing.Size(404, 31);
            this.kasClosedMoreInfoButton.TabIndex = 2;
            this.kasClosedMoreInfoButton.Text = "Más información";
            this.kasClosedMoreInfoButton.UseVisualStyleBackColor = false;
            this.kasClosedMoreInfoButton.Click += new System.EventHandler(this.kasClosedMoreInfoButton_Click);
            // 
            // kasClosedLabel
            // 
            this.kasClosedLabel.BackColor = System.Drawing.Color.Yellow;
            this.kasClosedLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.kasClosedLabel.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.kasClosedLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.kasClosedLabel.Location = new System.Drawing.Point(10, 10);
            this.kasClosedLabel.Name = "kasClosedLabel";
            this.kasClosedLabel.Size = new System.Drawing.Size(404, 25);
            this.kasClosedLabel.TabIndex = 0;
            this.kasClosedLabel.Text = "Cerrar las aplicaciones de Kaspersky.";
            // 
            // refreshButton
            // 
            this.refreshButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.refreshButton.Location = new System.Drawing.Point(150, 10);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(132, 35);
            this.refreshButton.TabIndex = 3;
            this.refreshButton.Text = "Actualizar";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonsPanel.Controls.Add(this.refreshButton);
            this.buttonsPanel.Controls.Add(this.closeButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonsPanel.Location = new System.Drawing.Point(0, 381);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Padding = new System.Windows.Forms.Padding(10);
            this.buttonsPanel.Size = new System.Drawing.Size(424, 55);
            this.buttonsPanel.TabIndex = 23;
            // 
            // closeButton
            // 
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeButton.Location = new System.Drawing.Point(282, 10);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(132, 35);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Cerrar";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Font = new System.Drawing.Font("Calibri", 12.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.Location = new System.Drawing.Point(10, 10);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(404, 79);
            this.titleLabel.TabIndex = 24;
            this.titleLabel.Text = "Faltan los siguientes requisitos para poder realizar una instalación automática:";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // titlePanel
            // 
            this.titlePanel.Controls.Add(this.titleLabel);
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel.Location = new System.Drawing.Point(0, 0);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Padding = new System.Windows.Forms.Padding(10);
            this.titlePanel.Size = new System.Drawing.Size(424, 99);
            this.titlePanel.TabIndex = 25;
            // 
            // RequirementsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(424, 436);
            this.Controls.Add(this.buttonsPanel);
            this.Controls.Add(this.kasClosedRequirementPanel);
            this.Controls.Add(this.passwordProtectionRequirementPanel);
            this.Controls.Add(this.adminPanel);
            this.Controls.Add(this.titlePanel);
            this.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RequirementsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Instalación automática";
            this.Load += new System.EventHandler(this.RequirementsForm_Load);
            this.adminPanel.ResumeLayout(false);
            this.adminPanel.PerformLayout();
            this.passwordProtectionRequirementPanel.ResumeLayout(false);
            this.passwordProtectionRequirementPanel.PerformLayout();
            this.kasClosedRequirementPanel.ResumeLayout(false);
            this.kasClosedRequirementPanel.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            this.titlePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Panel adminPanel;
        private Button restartAsAdminButton;
        private Label adminLabel;
        private Panel passwordProtectionRequirementPanel;
        private Button passwordProtectionMoreInfoButton;
        private Label passwordProtectionDisabledLabel;
        private Panel kasClosedRequirementPanel;
        private Button kasClosedMoreInfoButton;
        private Label kasClosedLabel;
        private Button refreshButton;
        private Panel buttonsPanel;
        private Button closeButton;
        private Label titleLabel;
        private Panel titlePanel;
    }
}