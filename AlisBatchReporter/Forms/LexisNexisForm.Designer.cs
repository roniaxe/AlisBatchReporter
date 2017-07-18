namespace AlisBatchReporter.Forms
{
    partial class LexisNexisForm
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
            this.validateButton = new System.Windows.Forms.Button();
            this.processTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // validateButton
            // 
            this.validateButton.Location = new System.Drawing.Point(43, 38);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(75, 23);
            this.validateButton.TabIndex = 0;
            this.validateButton.Text = "Validate";
            this.validateButton.UseVisualStyleBackColor = true;
            // 
            // processTextBox
            // 
            this.processTextBox.Location = new System.Drawing.Point(43, 82);
            this.processTextBox.Multiline = true;
            this.processTextBox.Name = "processTextBox";
            this.processTextBox.Size = new System.Drawing.Size(321, 157);
            this.processTextBox.TabIndex = 1;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(43, 267);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // LexisNexisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 352);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.processTextBox);
            this.Controls.Add(this.validateButton);
            this.Name = "LexisNexisForm";
            this.Text = "LexisNexisForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.TextBox processTextBox;
        private System.Windows.Forms.Button closeButton;
    }
}