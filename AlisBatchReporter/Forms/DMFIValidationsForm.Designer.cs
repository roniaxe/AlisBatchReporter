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
            this.validateButton = new System.Windows.Forms.Button();
            this.processTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // validationTypesCombobox
            // 
            this.validationTypesCombobox.FormattingEnabled = true;
            this.validationTypesCombobox.Items.AddRange(new object[] {
            "IRF1 File Comparison",
            "IRF2 File Comparison"});
            this.validationTypesCombobox.Location = new System.Drawing.Point(12, 80);
            this.validationTypesCombobox.Name = "validationTypesCombobox";
            this.validationTypesCombobox.Size = new System.Drawing.Size(121, 21);
            this.validationTypesCombobox.TabIndex = 0;
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
            // processTextBox
            // 
            this.processTextBox.Location = new System.Drawing.Point(12, 210);
            this.processTextBox.Multiline = true;
            this.processTextBox.Name = "processTextBox";
            this.processTextBox.Size = new System.Drawing.Size(506, 163);
            this.processTextBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            // 
            // DmfiValidationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 584);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.processTextBox);
            this.Controls.Add(this.validateButton);
            this.Controls.Add(this.validationTypesCombobox);
            this.Name = "DmfiValidationsForm";
            this.Text = "DMFIValidationsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox validationTypesCombobox;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.TextBox processTextBox;
        private System.Windows.Forms.Label label1;
    }
}