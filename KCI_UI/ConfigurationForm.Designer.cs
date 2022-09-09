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
            this.configurationListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // configurationListBox
            // 
            this.configurationListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.configurationListBox.FormattingEnabled = true;
            this.configurationListBox.Items.AddRange(new object[] {
            "Mantener configuración de Kaspersky [?]",
            "Asistente de instalación completo [?]",
            "Solo renovar licencia de prueba por defecto (30 días)",
            "Instalar Kaspersky Secure Connection [?]"});
            this.configurationListBox.Location = new System.Drawing.Point(12, 13);
            this.configurationListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.configurationListBox.Name = "configurationListBox";
            this.configurationListBox.Size = new System.Drawing.Size(363, 80);
            this.configurationListBox.TabIndex = 0;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(387, 235);
            this.Controls.Add(this.configurationListBox);
            this.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConfigurationForm";
            this.ResumeLayout(false);

        }

        #endregion

        private CheckedListBox configurationListBox;
    }
}