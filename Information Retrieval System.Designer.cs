namespace InformationRetrieval
{
    partial class InformationRetrievalSystem
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
            this.btnDocFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBoxDocFile = new System.Windows.Forms.TextBox();
            this.btnQueryFile = new System.Windows.Forms.Button();
            this.textBoxQueryFile = new System.Windows.Forms.TextBox();
            this.textBoxRelevanceJudgement = new System.Windows.Forms.TextBox();
            this.btnRelevanceJudgement = new System.Windows.Forms.Button();
            this.btnStemming = new System.Windows.Forms.Button();
            this.btnStopWords = new System.Windows.Forms.Button();
            this.btnIndexing = new System.Windows.Forms.Button();
            this.btnWeighting = new System.Windows.Forms.Button();
            this.btnInvertedFile = new System.Windows.Forms.Button();
            this.btnRetrieval = new System.Windows.Forms.Button();
            this.btnPseudoRelFB = new System.Windows.Forms.Button();
            this.btnEvaluation = new System.Windows.Forms.Button();
            this.btnPhraseRank = new System.Windows.Forms.Button();
            this.btnS = new System.Windows.Forms.RadioButton();
            this.btnR = new System.Windows.Forms.RadioButton();
            this.btnZ = new System.Windows.Forms.RadioButton();
            this.gBoxFeature = new System.Windows.Forms.GroupBox();
            this.btnRetTermPhRank = new System.Windows.Forms.Button();
            this.btnEvalTermPhRank = new System.Windows.Forms.Button();
            this.radioButton3Term = new System.Windows.Forms.RadioButton();
            this.radioButton6Term = new System.Windows.Forms.RadioButton();
            this.lblK = new System.Windows.Forms.Label();
            this.textBoxK = new System.Windows.Forms.TextBox();
            this.gBoxFeature.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDocFile
            // 
            this.btnDocFile.Location = new System.Drawing.Point(26, 12);
            this.btnDocFile.Name = "btnDocFile";
            this.btnDocFile.Size = new System.Drawing.Size(99, 23);
            this.btnDocFile.TabIndex = 0;
            this.btnDocFile.Text = "Document File";
            this.btnDocFile.UseVisualStyleBackColor = true;
            this.btnDocFile.Click += new System.EventHandler(this.btnDocumentFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBoxDocFile
            // 
            this.textBoxDocFile.Location = new System.Drawing.Point(153, 12);
            this.textBoxDocFile.Name = "textBoxDocFile";
            this.textBoxDocFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxDocFile.Size = new System.Drawing.Size(300, 20);
            this.textBoxDocFile.TabIndex = 1;
            // 
            // btnQueryFile
            // 
            this.btnQueryFile.Location = new System.Drawing.Point(26, 63);
            this.btnQueryFile.Name = "btnQueryFile";
            this.btnQueryFile.Size = new System.Drawing.Size(99, 23);
            this.btnQueryFile.TabIndex = 2;
            this.btnQueryFile.Text = "Query File";
            this.btnQueryFile.UseVisualStyleBackColor = true;
            this.btnQueryFile.Click += new System.EventHandler(this.btnQueryFile_Click);
            // 
            // textBoxQueryFile
            // 
            this.textBoxQueryFile.Location = new System.Drawing.Point(153, 65);
            this.textBoxQueryFile.Name = "textBoxQueryFile";
            this.textBoxQueryFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxQueryFile.Size = new System.Drawing.Size(300, 20);
            this.textBoxQueryFile.TabIndex = 3;
            // 
            // textBoxRelevanceJudgement
            // 
            this.textBoxRelevanceJudgement.Location = new System.Drawing.Point(153, 114);
            this.textBoxRelevanceJudgement.Name = "textBoxRelevanceJudgement";
            this.textBoxRelevanceJudgement.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxRelevanceJudgement.Size = new System.Drawing.Size(300, 20);
            this.textBoxRelevanceJudgement.TabIndex = 4;
            // 
            // btnRelevanceJudgement
            // 
            this.btnRelevanceJudgement.Location = new System.Drawing.Point(12, 112);
            this.btnRelevanceJudgement.Name = "btnRelevanceJudgement";
            this.btnRelevanceJudgement.Size = new System.Drawing.Size(135, 23);
            this.btnRelevanceJudgement.TabIndex = 5;
            this.btnRelevanceJudgement.Text = "Relevance Judgement";
            this.btnRelevanceJudgement.UseVisualStyleBackColor = true;
            this.btnRelevanceJudgement.Click += new System.EventHandler(this.btnRelevanceJudgement_Click);
            // 
            // btnStemming
            // 
            this.btnStemming.Location = new System.Drawing.Point(197, 157);
            this.btnStemming.Name = "btnStemming";
            this.btnStemming.Size = new System.Drawing.Size(95, 23);
            this.btnStemming.TabIndex = 6;
            this.btnStemming.Text = "Stemming";
            this.btnStemming.UseVisualStyleBackColor = true;
            this.btnStemming.Click += new System.EventHandler(this.btnStemming_Click);
            // 
            // btnStopWords
            // 
            this.btnStopWords.Location = new System.Drawing.Point(12, 157);
            this.btnStopWords.Name = "btnStopWords";
            this.btnStopWords.Size = new System.Drawing.Size(122, 23);
            this.btnStopWords.TabIndex = 7;
            this.btnStopWords.Text = "Remove Stop Words";
            this.btnStopWords.UseVisualStyleBackColor = true;
            this.btnStopWords.Click += new System.EventHandler(this.btnStopWords_Click);
            // 
            // btnIndexing
            // 
            this.btnIndexing.Location = new System.Drawing.Point(346, 157);
            this.btnIndexing.Name = "btnIndexing";
            this.btnIndexing.Size = new System.Drawing.Size(122, 23);
            this.btnIndexing.TabIndex = 9;
            this.btnIndexing.Text = "Indexing";
            this.btnIndexing.UseVisualStyleBackColor = true;
            this.btnIndexing.Click += new System.EventHandler(this.btnIndexing_Click);
            // 
            // btnWeighting
            // 
            this.btnWeighting.Location = new System.Drawing.Point(12, 207);
            this.btnWeighting.Name = "btnWeighting";
            this.btnWeighting.Size = new System.Drawing.Size(122, 23);
            this.btnWeighting.TabIndex = 10;
            this.btnWeighting.Text = "Weighting";
            this.btnWeighting.UseVisualStyleBackColor = true;
            this.btnWeighting.Click += new System.EventHandler(this.btnWeighting_Click);
            // 
            // btnInvertedFile
            // 
            this.btnInvertedFile.Location = new System.Drawing.Point(183, 207);
            this.btnInvertedFile.Name = "btnInvertedFile";
            this.btnInvertedFile.Size = new System.Drawing.Size(122, 23);
            this.btnInvertedFile.TabIndex = 11;
            this.btnInvertedFile.Text = "Make Inverted File";
            this.btnInvertedFile.UseVisualStyleBackColor = true;
            this.btnInvertedFile.Click += new System.EventHandler(this.btnInvertedFile_Click);
            // 
            // btnRetrieval
            // 
            this.btnRetrieval.Location = new System.Drawing.Point(346, 207);
            this.btnRetrieval.Name = "btnRetrieval";
            this.btnRetrieval.Size = new System.Drawing.Size(122, 23);
            this.btnRetrieval.TabIndex = 13;
            this.btnRetrieval.Text = "Retrieval";
            this.btnRetrieval.UseVisualStyleBackColor = true;
            this.btnRetrieval.Click += new System.EventHandler(this.btnRetrieval_Click);
            // 
            // btnPseudoRelFB
            // 
            this.btnPseudoRelFB.Location = new System.Drawing.Point(153, 279);
            this.btnPseudoRelFB.Name = "btnPseudoRelFB";
            this.btnPseudoRelFB.Size = new System.Drawing.Size(171, 23);
            this.btnPseudoRelFB.TabIndex = 14;
            this.btnPseudoRelFB.Text = "Pseudo Relevance Feedback";
            this.btnPseudoRelFB.UseVisualStyleBackColor = true;
            this.btnPseudoRelFB.Click += new System.EventHandler(this.btnPseudoRelFB_Click);
            // 
            // btnEvaluation
            // 
            this.btnEvaluation.Location = new System.Drawing.Point(12, 279);
            this.btnEvaluation.Name = "btnEvaluation";
            this.btnEvaluation.Size = new System.Drawing.Size(122, 23);
            this.btnEvaluation.TabIndex = 17;
            this.btnEvaluation.Text = "Evaluation";
            this.btnEvaluation.UseVisualStyleBackColor = true;
            this.btnEvaluation.Click += new System.EventHandler(this.btnEvaluation_Click);
            // 
            // btnPhraseRank
            // 
            this.btnPhraseRank.Location = new System.Drawing.Point(346, 279);
            this.btnPhraseRank.Name = "btnPhraseRank";
            this.btnPhraseRank.Size = new System.Drawing.Size(122, 23);
            this.btnPhraseRank.TabIndex = 18;
            this.btnPhraseRank.Text = "Phrase Rank";
            this.btnPhraseRank.UseVisualStyleBackColor = true;
            this.btnPhraseRank.Click += new System.EventHandler(this.btnPhraseRank_Click);
            // 
            // btnS
            // 
            this.btnS.AutoSize = true;
            this.btnS.Location = new System.Drawing.Point(6, 19);
            this.btnS.Name = "btnS";
            this.btnS.Size = new System.Drawing.Size(32, 17);
            this.btnS.TabIndex = 19;
            this.btnS.TabStop = true;
            this.btnS.Text = "S";
            this.btnS.UseVisualStyleBackColor = true;
            // 
            // btnR
            // 
            this.btnR.AutoSize = true;
            this.btnR.Location = new System.Drawing.Point(44, 19);
            this.btnR.Name = "btnR";
            this.btnR.Size = new System.Drawing.Size(33, 17);
            this.btnR.TabIndex = 20;
            this.btnR.TabStop = true;
            this.btnR.Text = "R";
            this.btnR.UseVisualStyleBackColor = true;
            // 
            // btnZ
            // 
            this.btnZ.AutoSize = true;
            this.btnZ.Location = new System.Drawing.Point(77, 19);
            this.btnZ.Name = "btnZ";
            this.btnZ.Size = new System.Drawing.Size(32, 17);
            this.btnZ.TabIndex = 21;
            this.btnZ.TabStop = true;
            this.btnZ.Text = "Z";
            this.btnZ.UseVisualStyleBackColor = true;
            // 
            // gBoxFeature
            // 
            this.gBoxFeature.Controls.Add(this.btnS);
            this.gBoxFeature.Controls.Add(this.btnZ);
            this.gBoxFeature.Controls.Add(this.btnR);
            this.gBoxFeature.Location = new System.Drawing.Point(353, 308);
            this.gBoxFeature.Name = "gBoxFeature";
            this.gBoxFeature.Size = new System.Drawing.Size(115, 39);
            this.gBoxFeature.TabIndex = 22;
            this.gBoxFeature.TabStop = false;
            this.gBoxFeature.Text = "Omit One Feature";
            // 
            // btnRetTermPhRank
            // 
            this.btnRetTermPhRank.Location = new System.Drawing.Point(36, 356);
            this.btnRetTermPhRank.Name = "btnRetTermPhRank";
            this.btnRetTermPhRank.Size = new System.Drawing.Size(162, 28);
            this.btnRetTermPhRank.TabIndex = 23;
            this.btnRetTermPhRank.Text = "Retrieval Term PhRank";
            this.btnRetTermPhRank.UseVisualStyleBackColor = true;
            this.btnRetTermPhRank.Click += new System.EventHandler(this.btnRetTermPhRank_Click);
            // 
            // btnEvalTermPhRank
            // 
            this.btnEvalTermPhRank.Location = new System.Drawing.Point(268, 356);
            this.btnEvalTermPhRank.Name = "btnEvalTermPhRank";
            this.btnEvalTermPhRank.Size = new System.Drawing.Size(162, 28);
            this.btnEvalTermPhRank.TabIndex = 24;
            this.btnEvalTermPhRank.Text = "Evaluation Term PhRank";
            this.btnEvalTermPhRank.UseVisualStyleBackColor = true;
            this.btnEvalTermPhRank.Click += new System.EventHandler(this.btnEvalTermPhRank_Click);
            // 
            // radioButton3Term
            // 
            this.radioButton3Term.AutoSize = true;
            this.radioButton3Term.Location = new System.Drawing.Point(346, 256);
            this.radioButton3Term.Name = "radioButton3Term";
            this.radioButton3Term.Size = new System.Drawing.Size(58, 17);
            this.radioButton3Term.TabIndex = 25;
            this.radioButton3Term.TabStop = true;
            this.radioButton3Term.Text = "3 Term";
            this.radioButton3Term.UseVisualStyleBackColor = true;
            // 
            // radioButton6Term
            // 
            this.radioButton6Term.AutoSize = true;
            this.radioButton6Term.Location = new System.Drawing.Point(410, 256);
            this.radioButton6Term.Name = "radioButton6Term";
            this.radioButton6Term.Size = new System.Drawing.Size(58, 17);
            this.radioButton6Term.TabIndex = 26;
            this.radioButton6Term.TabStop = true;
            this.radioButton6Term.Text = "6 Term";
            this.radioButton6Term.UseVisualStyleBackColor = true;
            // 
            // lblK
            // 
            this.lblK.AutoSize = true;
            this.lblK.Location = new System.Drawing.Point(213, 317);
            this.lblK.Name = "lblK";
            this.lblK.Size = new System.Drawing.Size(25, 13);
            this.lblK.TabIndex = 15;
            this.lblK.Text = "k = ";
            // 
            // textBoxK
            // 
            this.textBoxK.Location = new System.Drawing.Point(234, 314);
            this.textBoxK.Name = "textBoxK";
            this.textBoxK.Size = new System.Drawing.Size(27, 20);
            this.textBoxK.TabIndex = 16;
            // 
            // InformationRetrievalSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 406);
            this.Controls.Add(this.radioButton6Term);
            this.Controls.Add(this.radioButton3Term);
            this.Controls.Add(this.btnEvalTermPhRank);
            this.Controls.Add(this.btnRetTermPhRank);
            this.Controls.Add(this.gBoxFeature);
            this.Controls.Add(this.btnPhraseRank);
            this.Controls.Add(this.btnEvaluation);
            this.Controls.Add(this.textBoxK);
            this.Controls.Add(this.lblK);
            this.Controls.Add(this.btnPseudoRelFB);
            this.Controls.Add(this.btnRetrieval);
            this.Controls.Add(this.btnInvertedFile);
            this.Controls.Add(this.btnWeighting);
            this.Controls.Add(this.btnIndexing);
            this.Controls.Add(this.btnStopWords);
            this.Controls.Add(this.btnStemming);
            this.Controls.Add(this.btnRelevanceJudgement);
            this.Controls.Add(this.textBoxRelevanceJudgement);
            this.Controls.Add(this.textBoxQueryFile);
            this.Controls.Add(this.btnQueryFile);
            this.Controls.Add(this.textBoxDocFile);
            this.Controls.Add(this.btnDocFile);
            this.Name = "InformationRetrievalSystem";
            this.Text = "InformationRetrievalSystemForm";
            this.gBoxFeature.ResumeLayout(false);
            this.gBoxFeature.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDocFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBoxDocFile;
        private System.Windows.Forms.Button btnQueryFile;
        private System.Windows.Forms.TextBox textBoxQueryFile;
        private System.Windows.Forms.TextBox textBoxRelevanceJudgement;
        private System.Windows.Forms.Button btnRelevanceJudgement;
        private System.Windows.Forms.Button btnStemming;
        private System.Windows.Forms.Button btnStopWords;
        private System.Windows.Forms.Button btnIndexing;
        private System.Windows.Forms.Button btnWeighting;
        private System.Windows.Forms.Button btnInvertedFile;
        private System.Windows.Forms.Button btnRetrieval;
        private System.Windows.Forms.Button btnPseudoRelFB;
        private System.Windows.Forms.Button btnEvaluation;
        private System.Windows.Forms.Button btnPhraseRank;
        private System.Windows.Forms.RadioButton btnS;
        private System.Windows.Forms.RadioButton btnR;
        private System.Windows.Forms.RadioButton btnZ;
        private System.Windows.Forms.GroupBox gBoxFeature;
        private System.Windows.Forms.Button btnRetTermPhRank;
        private System.Windows.Forms.Button btnEvalTermPhRank;
        private System.Windows.Forms.RadioButton radioButton3Term;
        private System.Windows.Forms.RadioButton radioButton6Term;
        private System.Windows.Forms.Label lblK;
        private System.Windows.Forms.TextBox textBoxK;
    }
}