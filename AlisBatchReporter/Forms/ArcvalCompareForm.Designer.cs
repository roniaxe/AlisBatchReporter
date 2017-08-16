namespace AlisBatchReporter.Forms
{
    partial class ArcvalCompareForm
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
            this.compareButton = new System.Windows.Forms.Button();
            this.progressTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.copyCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // compareButton
            // 
            this.compareButton.Location = new System.Drawing.Point(33, 39);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(114, 40);
            this.compareButton.TabIndex = 0;
            this.compareButton.Text = "Compare";
            this.compareButton.UseVisualStyleBackColor = true;
            // 
            // progressTextBox
            // 
            this.progressTextBox.Location = new System.Drawing.Point(233, 39);
            this.progressTextBox.Multiline = true;
            this.progressTextBox.Name = "progressTextBox";
            this.progressTextBox.Size = new System.Drawing.Size(642, 258);
            this.progressTextBox.TabIndex = 1;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(33, 257);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(114, 40);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // copyCheckBox
            // 
            this.copyCheckBox.AutoSize = true;
            this.copyCheckBox.Location = new System.Drawing.Point(33, 102);
            this.copyCheckBox.Name = "copyCheckBox";
            this.copyCheckBox.Size = new System.Drawing.Size(108, 24);
            this.copyCheckBox.TabIndex = 3;
            this.copyCheckBox.Text = "Copy Files";
            this.copyCheckBox.UseVisualStyleBackColor = true;
            // 
            // ArcvalCompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 352);
            this.Controls.Add(this.copyCheckBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.progressTextBox);
            this.Controls.Add(this.compareButton);
            this.Name = "ArcvalCompareForm";
            this.Text = "ArcvalCompareForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button compareButton;
        private System.Windows.Forms.TextBox progressTextBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.CheckBox copyCheckBox;
    }
}