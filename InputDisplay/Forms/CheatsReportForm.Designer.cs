namespace InputDisplay.Forms
{
    partial class CheatsReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheatsReportForm));
            this.DismissButton = new System.Windows.Forms.Button();
            this.reportTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DismissButton
            // 
            this.DismissButton.Location = new System.Drawing.Point(379, 327);
            this.DismissButton.Name = "DismissButton";
            this.DismissButton.Size = new System.Drawing.Size(75, 23);
            this.DismissButton.TabIndex = 1;
            this.DismissButton.Text = "OK";
            this.DismissButton.UseVisualStyleBackColor = true;
            this.DismissButton.Click += new System.EventHandler(this.DismissButton_Click);
            // 
            // reportTxt
            // 
            this.reportTxt.Location = new System.Drawing.Point(13, 13);
            this.reportTxt.Multiline = true;
            this.reportTxt.Name = "reportTxt";
            this.reportTxt.ReadOnly = true;
            this.reportTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.reportTxt.Size = new System.Drawing.Size(441, 299);
            this.reportTxt.TabIndex = 2;
            // 
            // CheatsReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 362);
            this.Controls.Add(this.reportTxt);
            this.Controls.Add(this.DismissButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CheatsReportForm";
            this.Text = "Cheats Report";
            this.Load += new System.EventHandler(this.CheatsReportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DismissButton;
        private System.Windows.Forms.TextBox reportTxt;
    }
}