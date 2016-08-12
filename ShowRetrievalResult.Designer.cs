namespace InformationRetrieval
{ 
    partial class ShowRetrievalResult
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
            this.textBoxRetrievalMethod = new System.Windows.Forms.TextBox();
            this.lblRecall = new System.Windows.Forms.Label();
            this.lblPrecision = new System.Windows.Forms.Label();
            this.lblIAP = new System.Windows.Forms.Label();
            this.lblNIAP = new System.Windows.Forms.Label();
            this.textBoxRecall = new System.Windows.Forms.TextBox();
            this.textBoxPrecision = new System.Windows.Forms.TextBox();
            this.textBoxIAP = new System.Windows.Forms.TextBox();
            this.textBoxNIAP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxRetrievalMethod
            // 
            this.textBoxRetrievalMethod.Location = new System.Drawing.Point(17, 21);
            this.textBoxRetrievalMethod.Name = "textBoxRetrievalMethod";
            this.textBoxRetrievalMethod.Size = new System.Drawing.Size(404, 20);
            this.textBoxRetrievalMethod.TabIndex = 0;
            // 
            // lblRecall
            // 
            this.lblRecall.AutoSize = true;
            this.lblRecall.Location = new System.Drawing.Point(21, 68);
            this.lblRecall.Name = "lblRecall";
            this.lblRecall.Size = new System.Drawing.Size(129, 13);
            this.lblRecall.TabIndex = 1;
            this.lblRecall.Text = "Average Recall Collection";
            // 
            // lblPrecision
            // 
            this.lblPrecision.AutoSize = true;
            this.lblPrecision.Location = new System.Drawing.Point(21, 99);
            this.lblPrecision.Name = "lblPrecision";
            this.lblPrecision.Size = new System.Drawing.Size(142, 13);
            this.lblPrecision.TabIndex = 2;
            this.lblPrecision.Text = "Average Precision Collection";
            // 
            // lblIAP
            // 
            this.lblIAP.AutoSize = true;
            this.lblIAP.Location = new System.Drawing.Point(21, 130);
            this.lblIAP.Name = "lblIAP";
            this.lblIAP.Size = new System.Drawing.Size(154, 13);
            this.lblIAP.TabIndex = 3;
            this.lblIAP.Text = "Interpolation Average Precision";
            // 
            // lblNIAP
            // 
            this.lblNIAP.AutoSize = true;
            this.lblNIAP.Location = new System.Drawing.Point(21, 161);
            this.lblNIAP.Name = "lblNIAP";
            this.lblNIAP.Size = new System.Drawing.Size(177, 13);
            this.lblNIAP.TabIndex = 4;
            this.lblNIAP.Text = "Non Interpolation Average Precision";
            // 
            // textBoxRecall
            // 
            this.textBoxRecall.Location = new System.Drawing.Point(204, 61);
            this.textBoxRecall.Name = "textBoxRecall";
            this.textBoxRecall.Size = new System.Drawing.Size(217, 20);
            this.textBoxRecall.TabIndex = 5;
            // 
            // textBoxPrecision
            // 
            this.textBoxPrecision.Location = new System.Drawing.Point(204, 92);
            this.textBoxPrecision.Name = "textBoxPrecision";
            this.textBoxPrecision.Size = new System.Drawing.Size(217, 20);
            this.textBoxPrecision.TabIndex = 6;
            // 
            // textBoxIAP
            // 
            this.textBoxIAP.Location = new System.Drawing.Point(204, 123);
            this.textBoxIAP.Name = "textBoxIAP";
            this.textBoxIAP.Size = new System.Drawing.Size(217, 20);
            this.textBoxIAP.TabIndex = 7;
            // 
            // textBoxNIAP
            // 
            this.textBoxNIAP.Location = new System.Drawing.Point(204, 154);
            this.textBoxNIAP.Name = "textBoxNIAP";
            this.textBoxNIAP.Size = new System.Drawing.Size(217, 20);
            this.textBoxNIAP.TabIndex = 8;
            // 
            // ShowRetrievalResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 261);
            this.Controls.Add(this.textBoxNIAP);
            this.Controls.Add(this.textBoxIAP);
            this.Controls.Add(this.textBoxPrecision);
            this.Controls.Add(this.textBoxRecall);
            this.Controls.Add(this.lblNIAP);
            this.Controls.Add(this.lblIAP);
            this.Controls.Add(this.lblPrecision);
            this.Controls.Add(this.lblRecall);
            this.Controls.Add(this.textBoxRetrievalMethod);
            this.Name = "ShowRetrievalResult";
            this.Text = "ShowRetrievalResult";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRetrievalMethod;
        private System.Windows.Forms.Label lblRecall;
        private System.Windows.Forms.Label lblPrecision;
        private System.Windows.Forms.Label lblIAP;
        private System.Windows.Forms.Label lblNIAP;
        private System.Windows.Forms.TextBox textBoxRecall;
        private System.Windows.Forms.TextBox textBoxPrecision;
        private System.Windows.Forms.TextBox textBoxIAP;
        private System.Windows.Forms.TextBox textBoxNIAP;
    }
}