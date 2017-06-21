using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                    Value = "UnallocatedSuspenseReport.SQL"
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
            SimpleExportQuery query = 
                new SimpleExportQuery(@"..\..\Resources\SQL\" + sqlFileName, keyTextBox.Text);

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
                DataTable workerResult = (DataTable)args.Result;
                bindingSource1.DataSource = workerResult;
                dataGridView1.Visible = true;
                progressBar1.Visible = false;
            };
            backgroundWorker.RunWorkerAsync(query);
        }
    }
}