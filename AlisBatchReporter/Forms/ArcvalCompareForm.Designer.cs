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
            this.overrideCheckBox = new System.Windows.Forms.CheckBox();
            this.outboundFileNameTextBox = new System.Windows.Forms.TextBox();
            this.sourceFileNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fileNamesPanel = new System.Windows.Forms.Panel();
            this.uatRadioButton = new System.Windows.Forms.RadioButton();
            this.prodRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CancelButton = new System.Windows.Forms.Button();
            this.fileNamesPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // compareButton
            // 
            this.compareButton.Location = new System.Drawing.Point(22, 25);
            this.compareButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(76, 26);
            this.compareButton.TabIndex = 0;
            this.compareButton.Text = "Compare";
            this.compareButton.UseVisualStyleBackColor = true;
            // 
            // progressTextBox
            // 
            this.progressTextBox.Location = new System.Drawing.Point(330, 25);
            this.progressTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressTextBox.Multiline = true;
            this.progressTextBox.Name = "progressTextBox";
            this.progressTextBox.ReadOnly = true;
            this.progressTextBox.Size = new System.Drawing.Size(321, 169);
            this.progressTextBox.TabIndex = 1;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(22, 167);
            this.closeButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(76, 26);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // copyCheckBox
            // 
            this.copyCheckBox.AutoSize = true;
            this.copyCheckBox.Location = new System.Drawing.Point(22, 100);
            this.copyCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.copyCheckBox.Name = "copyCheckBox";
            this.copyCheckBox.Size = new System.Drawing.Size(74, 17);
            this.copyCheckBox.TabIndex = 3;
            this.copyCheckBox.Text = "Copy Files";
            this.copyCheckBox.UseVisualStyleBackColor = true;
            // 
            // overrideCheckBox
            // 
            this.overrideCheckBox.AutoSize = true;
            this.overrideCheckBox.Location = new System.Drawing.Point(112, 27);
            this.overrideCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.overrideCheckBox.Name = "overrideCheckBox";
            this.overrideCheckBox.Size = new System.Drawing.Size(121, 17);
            this.overrideCheckBox.TabIndex = 4;
            this.overrideCheckBox.Text = "Override File Names";
            this.overrideCheckBox.UseVisualStyleBackColor = true;
            // 
            // outboundFileNameTextBox
            // 
            this.outboundFileNameTextBox.Location = new System.Drawing.Point(86, 45);
            this.outboundFileNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.outboundFileNameTextBox.Name = "outboundFileNameTextBox";
            this.outboundFileNameTextBox.Size = new System.Drawing.Size(101, 20);
            this.outboundFileNameTextBox.TabIndex = 5;
            // 
            // sourceFileNameTextBox
            // 
            this.sourceFileNameTextBox.Location = new System.Drawing.Point(86, 10);
            this.sourceFileNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sourceFileNameTextBox.Name = "sourceFileNameTextBox";
            this.sourceFileNameTextBox.Size = new System.Drawing.Size(101, 20);
            this.sourceFileNameTextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Source File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Outbound File";
            // 
            // fileNamesPanel
            // 
            this.fileNamesPanel.Controls.Add(this.label2);
            this.fileNamesPanel.Controls.Add(this.label1);
            this.fileNamesPanel.Controls.Add(this.sourceFileNameTextBox);
            this.fileNamesPanel.Controls.Add(this.outboundFileNameTextBox);
            this.fileNamesPanel.Location = new System.Drawing.Point(112, 55);
            this.fileNamesPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fileNamesPanel.Name = "fileNamesPanel";
            this.fileNamesPanel.Size = new System.Drawing.Size(214, 92);
            this.fileNamesPanel.TabIndex = 9;
            this.fileNamesPanel.Visible = false;
            // 
            // uatRadioButton
            // 
            this.uatRadioButton.AutoSize = true;
            this.uatRadioButton.Checked = true;
            this.uatRadioButton.Location = new System.Drawing.Point(53, 8);
            this.uatRadioButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.uatRadioButton.Name = "uatRadioButton";
            this.uatRadioButton.Size = new System.Drawing.Size(47, 17);
            this.uatRadioButton.TabIndex = 10;
            this.uatRadioButton.TabStop = true;
            this.uatRadioButton.Text = "UAT";
            this.uatRadioButton.UseVisualStyleBackColor = true;
            // 
            // prodRadioButton
            // 
            this.prodRadioButton.AutoSize = true;
            this.prodRadioButton.Location = new System.Drawing.Point(4, 8);
            this.prodRadioButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.prodRadioButton.Name = "prodRadioButton";
            this.prodRadioButton.Size = new System.Drawing.Size(47, 17);
            this.prodRadioButton.TabIndex = 11;
            this.prodRadioButton.Text = "Prod";
            this.prodRadioButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.prodRadioButton);
            this.panel1.Controls.Add(this.uatRadioButton);
            this.panel1.Location = new System.Drawing.Point(112, 162);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(106, 32);
            this.panel1.TabIndex = 12;
            // 
            // CancelButton
            // 
            this.CancelButton.Enabled = false;
            this.CancelButton.Location = new System.Drawing.Point(22, 59);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(76, 26);
            this.CancelButton.TabIndex = 13;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // ArcvalCompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 229);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.fileNamesPanel);
            this.Controls.Add(this.overrideCheckBox);
            this.Controls.Add(this.copyCheckBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.progressTextBox);
            this.Controls.Add(this.compareButton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ArcvalCompareForm";
            this.Text = "ArcvalCompareForm";
            this.fileNamesPanel.ResumeLayout(false);
            this.fileNamesPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button compareButton;
        private System.Windows.Forms.TextBox progressTextBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.CheckBox copyCheckBox;
        private System.Windows.Forms.CheckBox overrideCheckBox;
        private System.Windows.Forms.TextBox outboundFileNameTextBox;
        private System.Windows.Forms.TextBox sourceFileNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel fileNamesPanel;
        private System.Windows.Forms.RadioButton uatRadioButton;
        private System.Windows.Forms.RadioButton prodRadioButton;
        private System.Windows.Forms.Panel panel1;
        private new System.Windows.Forms.Button CancelButton;
    }
}