using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Forms
{
    public partial class ExportValidationForm : Form
    {
        public ExportValidationForm()
        {
            InitializeComponent();
        }

        private void ExportValidationForm_Load(object sender, EventArgs e)
        {
            PopulateExportComboBox();
        }

        private void PopulateExportComboBox()
        {
            List<ComboboxItem> exportTableList = new List<ComboboxItem>
            {
                new ComboboxItem
                {
                    Text = "i_unallocated_suspense_report".ToUpper(),
                    Value = @"\Resources\SQL\UnallocatedSuspenseReport.SQL"
                }
            };
            exportTablesComboBox.Items.AddRange(exportTableList.ToArray());
        }

        private void ResetComps()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            ResetComps();
            var sqlFileName = ((ComboboxItem) exportTablesComboBox.SelectedItem).Value;
            var keyType = internalRadioButton.Checked ? "Internal" : "External";
            SimpleExportQuery query =
                new SimpleExportQuery(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + sqlFileName, keyTextBox.Text, keyType);

            TriggerBgWorkerForQuery(query);
        }

        private void TriggerBgWorkerForQuery(SimpleExportQuery query)
        {
            dataGridView1.DataSource = bindingSource1;
            progressBar1.Visible = true;
            BackgroundWorker backgroundWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            progressBar1.Visible = true;
            backgroundWorker.DoWork += (o, args) =>
            {
                SimpleExportQuery report = args.Argument as SimpleExportQuery;
                args.Result = report?.DoQuery();
            };
            backgroundWorker.RunWorkerCompleted += (o, args) =>
            {
                DataTable workerResult = (DataTable) args.Result;
                bindingSource1.DataSource = workerResult;
                dataGridView1.Visible = true;
                progressBar1.Visible = false;
            };
            backgroundWorker.RunWorkerAsync(query);
        }

        private void keyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (internalRadioButton.Checked)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                var textBox = sender as TextBox;
                if (textBox != null && e.KeyChar == '.' && textBox.Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void externalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            keyTextBox.Text = "";
        }

        private void internalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            keyTextBox.Text = "";
        }
    }
}