namespace AlisBatchReporter.Forms
{
    partial class DataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.fromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.sendEmailButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.onlyErrorsRadioButton = new System.Windows.Forms.RadioButton();
            this.allTypesRadioButton = new System.Windows.Forms.RadioButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.polFilterLabel = new System.Windows.Forms.Label();
            this.polFilterTextBox = new System.Windows.Forms.TextBox();
            this.labelReportType = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.comboBoxFunc = new System.Windows.Forms.ComboBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.distributionProcessTxt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(24, 267);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1399, 219);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // toDate
            // 
            this.toDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.toDate.Location = new System.Drawing.Point(364, 130);
            this.toDate.Margin = new System.Windows.Forms.Padding(4);
            this.toDate.Name = "toDate";
            this.toDate.Size = new System.Drawing.Size(176, 29);
            this.toDate.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(87, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "To Date:";
            // 
            // fromDate
            // 
            this.fromDate.Checked = false;
            this.fromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fromDate.Location = new System.Drawing.Point(364, 75);
            this.fromDate.Margin = new System.Windows.Forms.Padding(4);
            this.fromDate.Name = "fromDate";
            this.fromDate.Size = new System.Drawing.Size(176, 29);
            this.fromDate.TabIndex = 8;
            this.fromDate.ValueChanged += new System.EventHandler(this.fromDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(87, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "From Date:";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(91, 198);
            this.createButton.Margin = new System.Windows.Forms.Padding(4);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(100, 28);
            this.createButton.TabIndex = 6;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.AutoSize = true;
            this.mainPanel.Controls.Add(this.distributionProcessTxt);
            this.mainPanel.Controls.Add(this.sendEmailButton);
            this.mainPanel.Controls.Add(this.exportButton);
            this.mainPanel.Controls.Add(this.groupBox1);
            this.mainPanel.Controls.Add(this.dataGridView1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1500, 663);
            this.mainPanel.TabIndex = 11;
            // 
            // sendEmailButton
            // 
            this.sendEmailButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendEmailButton.Image = ((System.Drawing.Image)(resources.GetObject("sendEmailButton.Image")));
            this.sendEmailButton.Location = new System.Drawing.Point(1327, 213);
            this.sendEmailButton.Margin = new System.Windows.Forms.Padding(4);
            this.sendEmailButton.Name = "sendEmailButton";
            this.sendEmailButton.Size = new System.Drawing.Size(35, 36);
            this.sendEmailButton.TabIndex = 15;
            this.sendEmailButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sendEmailButton.UseVisualStyleBackColor = true;
            this.sendEmailButton.Visible = false;
            this.sendEmailButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Image = ((System.Drawing.Image)(resources.GetObject("exportButton.Image")));
            this.exportButton.Location = new System.Drawing.Point(1388, 213);
            this.exportButton.Margin = new System.Windows.Forms.Padding(4);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(35, 36);
            this.exportButton.TabIndex = 14;
            this.exportButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Visible = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.polFilterLabel);
            this.groupBox1.Controls.Add(this.polFilterTextBox);
            this.groupBox1.Controls.Add(this.labelReportType);
            this.groupBox1.Controls.Add(this.closeButton);
            this.groupBox1.Controls.Add(this.comboBoxFunc);
            this.groupBox1.Controls.Add(this.createButton);
            this.groupBox1.Controls.Add(this.fromDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.toDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(24, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(919, 234);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.onlyErrorsRadioButton);
            this.groupBox2.Controls.Add(this.allTypesRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(599, 57);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(280, 71);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Record Type Filter";
            // 
            // onlyErrorsRadioButton
            // 
            this.onlyErrorsRadioButton.AutoSize = true;
            this.onlyErrorsRadioButton.Checked = true;
            this.onlyErrorsRadioButton.Location = new System.Drawing.Point(25, 28);
            this.onlyErrorsRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.onlyErrorsRadioButton.Name = "onlyErrorsRadioButton";
            this.onlyErrorsRadioButton.Size = new System.Drawing.Size(101, 21);
            this.onlyErrorsRadioButton.TabIndex = 20;
            this.onlyErrorsRadioButton.TabStop = true;
            this.onlyErrorsRadioButton.Text = "Only Errors";
            this.onlyErrorsRadioButton.UseVisualStyleBackColor = true;
            // 
            // allTypesRadioButton
            // 
            this.allTypesRadioButton.AutoSize = true;
            this.allTypesRadioButton.Location = new System.Drawing.Point(147, 28);
            this.allTypesRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.allTypesRadioButton.Name = "allTypesRadioButton";
            this.allTypesRadioButton.Size = new System.Drawing.Size(87, 21);
            this.allTypesRadioButton.TabIndex = 19;
            this.allTypesRadioButton.TabStop = true;
            this.allTypesRadioButton.Text = "All Types";
            this.allTypesRadioButton.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(599, 190);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(280, 36);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 12;
            this.progressBar1.Visible = false;
            // 
            // polFilterLabel
            // 
            this.polFilterLabel.AutoSize = true;
            this.polFilterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.polFilterLabel.Location = new System.Drawing.Point(595, 27);
            this.polFilterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.polFilterLabel.Name = "polFilterLabel";
            this.polFilterLabel.Size = new System.Drawing.Size(111, 24);
            this.polFilterLabel.TabIndex = 17;
            this.polFilterLabel.Text = "Policy Filter:";
            this.polFilterLabel.Visible = false;
            // 
            // polFilterTextBox
            // 
            this.polFilterTextBox.Location = new System.Drawing.Point(745, 25);
            this.polFilterTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.polFilterTextBox.Name = "polFilterTextBox";
            this.polFilterTextBox.Size = new System.Drawing.Size(135, 22);
            this.polFilterTextBox.TabIndex = 16;
            this.polFilterTextBox.WordWrap = false;
            // 
            // labelReportType
            // 
            this.labelReportType.AutoSize = true;
            this.labelReportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReportType.Location = new System.Drawing.Point(87, 23);
            this.labelReportType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReportType.Name = "labelReportType";
            this.labelReportType.Size = new System.Drawing.Size(119, 24);
            this.labelReportType.TabIndex = 15;
            this.labelReportType.Text = "Report Type:";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(441, 198);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 28);
            this.closeButton.TabIndex = 12;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // comboBoxFunc
            // 
            this.comboBoxFunc.FormattingEnabled = true;
            this.comboBoxFunc.Location = new System.Drawing.Point(364, 23);
            this.comboBoxFunc.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxFunc.Name = "comboBoxFunc";
            this.comboBoxFunc.Size = new System.Drawing.Size(176, 24);
            this.comboBoxFunc.TabIndex = 13;
            this.comboBoxFunc.SelectedIndexChanged += new System.EventHandler(this.comboBoxFunc_SelectedIndexChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // distributionProcessTxt
            // 
            this.distributionProcessTxt.Location = new System.Drawing.Point(1123, 14);
            this.distributionProcessTxt.Multiline = true;
            this.distributionProcessTxt.Name = "distributionProcessTxt";
            this.distributionProcessTxt.ReadOnly = true;
            this.distributionProcessTxt.Size = new System.Drawing.Size(182, 234);
            this.distributionProcessTxt.TabIndex = 12;
            this.distributionProcessTxt.Visible = false;
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 663);
            this.Controls.Add(this.mainPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DataForm";
            this.Text = "DataForm";
            this.Load += new System.EventHandler(this.DataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DateTimePicker toDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker fromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button closeButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.ComboBox comboBoxFunc;
        private System.Windows.Forms.Label labelReportType;
        private System.Windows.Forms.Label polFilterLabel;
        private System.Windows.Forms.TextBox polFilterTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton onlyErrorsRadioButton;
        private System.Windows.Forms.RadioButton allTypesRadioButton;
        private System.Windows.Forms.Button sendEmailButton;
        private System.Windows.Forms.TextBox distributionProcessTxt;
    }
}