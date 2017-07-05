namespace AlisBatchReporter.Forms
{
    partial class DmfiValidationsForm
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
            this.validationTypesCombobox = new System.Windows.Forms.ComboBox();
            this.validationLabel = new System.Windows.Forms.Label();
            this.validateButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // validationTypesCombobox
            // 
            this.validationTypesCombobox.FormattingEnabled = true;
            this.validationTypesCombobox.Items.AddRange(new object[] {
            "AnalyticFeed"});
            this.validationTypesCombobox.Location = new System.Drawing.Point(12, 80);
            this.validationTypesCombobox.Name = "validationTypesCombobox";
            this.validationTypesCombobox.Size = new System.Drawing.Size(121, 21);
            this.validationTypesCombobox.TabIndex = 0;
            // 
            // validationLabel
            // 
            this.validationLabel.AutoSize = true;
            this.validationLabel.Location = new System.Drawing.Point(12, 64);
            this.validationLabel.Name = "validationLabel";
            this.validationLabel.Size = new System.Drawing.Size(80, 13);
            this.validationLabel.TabIndex = 1;
            this.validationLabel.Text = "Validation Type";
            // 
            // validateButton
            // 
            this.validateButton.Location = new System.Drawing.Point(12, 120);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(75, 23);
            this.validateButton.TabIndex = 2;
            this.validateButton.Text = "Validate";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.validateButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 149);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Copying Files..";
            this.label1.Visible = false;
            // 
            // DmfiValidationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 584);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.validateButton);
            this.Controls.Add(this.validationLabel);
            this.Controls.Add(this.validationTypesCombobox);
            this.Name = "DmfiValidationsForm";
            this.Text = "DMFIValidationsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox validationTypesCombobox;
        private System.Windows.Forms.Label validationLabel;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
    }
}