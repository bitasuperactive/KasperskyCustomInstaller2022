namespace KCI_UI
{
    partial class LoadingForm
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
            this.loadingLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loadingLabel
            // 
            this.loadingLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingLabel.Location = new System.Drawing.Point(0, 0);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(417, 137);
            this.loadingLabel.TabIndex = 0;
            this.loadingLabel.Text = "[loadingLabel]";
            this.loadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(417, 137);
            this.Controls.Add(this.loadingLabel);
            this.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[LoadingFormText]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoadingForm_FormClosing);
            this.Load += new System.EventHandler(this.LoadingForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Label loadingLabel;
    }
}