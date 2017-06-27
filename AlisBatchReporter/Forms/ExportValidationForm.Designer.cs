namespace AlisBatchReporter.Forms
{
    partial class ExportValidationForm
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
            this.components = new System.ComponentModel.Container();
            this.exportTablesComboBox = new System.Windows.Forms.ComboBox();
            this.exportTableLabel = new System.Windows.Forms.Label();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.keyLabel = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.externalRadioButton = new System.Windows.Forms.RadioButton();
            this.internalRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // exportTablesComboBox
            // 
            this.exportTablesComboBox.FormattingEnabled = true;
            this.exportTablesComboBox.Location = new System.Drawing.Point(25, 53);
            this.exportTablesComboBox.Name = "exportTablesComboBox";
            this.exportTablesComboBox.Size = new System.Drawing.Size(150, 21);
            this.exportTablesComboBox.TabIndex = 0;
            // 
            // exportTableLabel
            // 
            this.exportTableLabel.AutoSize = true;
            this.exportTableLabel.Location = new System.Drawing.Point(22, 37);
            this.exportTableLabel.Name = "exportTableLabel";
            this.exportTableLabel.Size = new System.Drawing.Size(67, 13);
            this.exportTableLabel.TabIndex = 1;
            this.exportTableLabel.Text = "Export Table";
            // 
            // keyTextBox
            // 
            this.keyTextBox.Location = new System.Drawing.Point(6, 74);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(100, 20);
            this.keyTextBox.TabIndex = 2;
            this.keyTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyTextBox_KeyPress);
            // 
            // keyLabel
            // 
            this.keyLabel.AutoSize = true;
            this.keyLabel.Location = new System.Drawing.Point(6, 58);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(25, 13);
            this.keyLabel.TabIndex = 3;
            this.keyLabel.Text = "Key";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(25, 211);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 23);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(258, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(526, 237);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(684, 8);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.internalRadioButton);
            this.groupBox1.Controls.Add(this.externalRadioButton);
            this.groupBox1.Controls.Add(this.keyTextBox);
            this.groupBox1.Controls.Add(this.keyLabel);
            this.groupBox1.Location = new System.Drawing.Point(25, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // externalRadioButton
            // 
            this.externalRadioButton.AutoSize = true;
            this.externalRadioButton.Location = new System.Drawing.Point(98, 31);
            this.externalRadioButton.Name = "externalRadioButton";
            this.externalRadioButton.Size = new System.Drawing.Size(63, 17);
            this.externalRadioButton.TabIndex = 4;
            this.externalRadioButton.Text = "External";
            this.externalRadioButton.UseVisualStyleBackColor = true;
            this.externalRadioButton.CheckedChanged += new System.EventHandler(this.externalRadioButton_CheckedChanged);
            // 
            // internalRadioButton
            // 
            this.internalRadioButton.AutoSize = true;
            this.internalRadioButton.Checked = true;
            this.internalRadioButton.Location = new System.Drawing.Point(9, 31);
            this.internalRadioButton.Name = "internalRadioButton";
            this.internalRadioButton.Size = new System.Drawing.Size(60, 17);
            this.internalRadioButton.TabIndex = 5;
            this.internalRadioButton.TabStop = true;
            this.internalRadioButton.Text = "Internal";
            this.internalRadioButton.UseVisualStyleBackColor = true;
            this.internalRadioButton.CheckedChanged += new System.EventHandler(this.internalRadioButton_CheckedChanged);
            // 
            // ExportValidationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 286);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.exportTableLabel);
            this.Controls.Add(this.exportTablesComboBox);
            this.Name = "ExportValidationForm";
            this.Text = "ExportValidationForm";
            this.Load += new System.EventHandler(this.ExportValidationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox exportTablesComboBox;
        private System.Windows.Forms.Label exportTableLabel;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.Label keyLabel;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton internalRadioButton;
        private System.Windows.Forms.RadioButton externalRadioButton;
    }
}