using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Forms
{
    public partial class DataForm : Form
    {
        private readonly ContextMenu _batchRunNumberContextMenu = new ContextMenu();

        public DataForm()
        {
            InitializeComponent();
        }

        private void DataForm_Load(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            exportButton.Visible = false;


            // Create the context menu items
            _batchRunNumberContextMenu.MenuItems.Add("Load GBA", LoadGbaData);
            _batchRunNumberContextMenu.MenuItems.Add("Running Time", RunningTime);
        }

        private void RunningTime(object sender, EventArgs e)
        {
            // Get the right click batch run number
            var batchRunNum = dataGridView1.CurrentCell.Value.ToString();

            // Create query
            SimpleQuery timeingQuery = new SimpleQuery(@"..\..\Resources\SQL\RunningTime.sql", batchRunNum);

            BackgroundWorker backgroundWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            progressBar1.Visible = true;
            backgroundWorker.DoWork += (o, args) =>
            {
                SimpleQuery report = args.Argument as SimpleQuery;
                args.Result = report?.DoQuery();
            };
            backgroundWorker.RunWorkerCompleted += (o, args) =>
            {
                DataTable workerResult = (DataTable) args.Result;
                TimeSpan time = TimeSpan.FromSeconds((int) workerResult.Rows[0].ItemArray[0]);
                string str = time.ToString(@"hh\:mm\:ss\:fff");
                MessageBox.Show($@"Seconds Ran: {str}");
                progressBar1.Visible = false;
            };
            backgroundWorker.RunWorkerAsync(timeingQuery);
        }

        private void LoadGbaData(object sender, EventArgs e)
        {
            // Get the right click batch run number
            var batchRunNum = dataGridView1.CurrentCell.Value.ToString();

            // Create query, and send it to form
            SimpleQuery gbaQuery = new SimpleQuery(@"..\..\Resources\SQL\SimpleGBA.sql", batchRunNum);
            SingleBatchRunForm singleForm = new SingleBatchRunForm(gbaQuery);
            singleForm.Show();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dataGridView1.HitTest(e.X, e.Y);
                if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
                {
                    var cellHeaderText = dataGridView1.Columns[hitTestInfo.ColumnIndex].HeaderText;
                    if (cellHeaderText.Equals("Batch Run Number"))
                    {
                        // Set pressed cell as active
                        dataGridView1.CurrentCell = dataGridView1.Rows[hitTestInfo.RowIndex]
                            .Cells[hitTestInfo.ColumnIndex];

                        // Set cell to be selected 
                        dataGridView1.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex].Selected = true;

                        // Focus on the grid
                        dataGridView1.Focus();

                        // Show context menu items
                        _batchRunNumberContextMenu.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                }
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            // Create query
            ReportQuery newQuery = new ReportQuery(
                @"..\..\Resources\SQL\BatchAudit.sql",
                fromDate.Value.ToShortDateString(),
                toDate.Value.ToShortDateString()
            );

            // Set the datasource, run query (populate grid in backgroundWorker1_RunWorkerCompleted)
            dataGridView1.DataSource = bindingSource1;
            progressBar1.Visible = true;
            backgroundWorker1.RunWorkerAsync(newQuery);
            dataGridView1.Show();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ReportQuery report = e.Argument as ReportQuery;
            e.Result = report?.DoQuery();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            DataTable workerResult = (DataTable) e.Result;
            bindingSource1.DataSource = workerResult;
            progressBar1.Visible = false;
        }
    }
}