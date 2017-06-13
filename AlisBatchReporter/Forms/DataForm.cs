using System;
using System.Collections.Generic;
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
        private readonly ContextMenu _taskIdContextMenu = new ContextMenu();

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
            _taskIdContextMenu.MenuItems.Add("Running Time", RunningTime);
            fromDate.Value = DateTime.Today.AddDays(-1);
            PopulateFuncCombobox();
        }

        private void PopulateFuncCombobox()
        {
            List<ComboboxItem> funcItems = new List<ComboboxItem>
            {
                new ComboboxItem
                {
                    Text = "Report",
                    Value = "BatchAudit.sql"
                },
                new ComboboxItem
                {
                    Text = "Task List",
                    Value = "TaskList.sql"
                }
            };
            comboBoxFunc.Items.AddRange(funcItems.ToArray());
            comboBoxFunc.SelectedIndex = 0;
        }

        private void RunningTime(object sender, EventArgs e)
        {
            // Get the right click batch run number
            var taskId = dataGridView1.CurrentCell.Value.ToString();

            // Create query
            SimpleQuery timeingQuery = new SimpleQuery(@"..\..\Resources\SQL\RunningTime.sql", GetBatchRunNumber(dataGridView1), taskId);

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
                progressBar1.Visible = false;
                MessageBox.Show($@"Seconds Ran: {str}");
            };
            backgroundWorker.RunWorkerAsync(timeingQuery);
        }

        private void LoadGbaData(object sender, EventArgs e)
        {
            // Get the right click batch run number
            var batchRunNum = dataGridView1.CurrentCell.Value.ToString();
            


            // Create query, and send it to form
            SimpleQuery gbaQuery = new SimpleQuery(@"..\..\Resources\SQL\SimpleGBA.sql", batchRunNum, GetTaskId(dataGridView1));
            SingleBatchRunForm singleForm = new SingleBatchRunForm(gbaQuery);
            singleForm.Show();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string GetTaskId(DataGridView grid)
        {
            string taskId = null;
            var row = grid.CurrentCell.RowIndex;
            var column = grid.Columns["Task Id"]?.Index;
            if (column != null)
            {
                taskId = grid.Rows[row].Cells[(int)column].Value.ToString();
            }
            return taskId;
        }

        private string GetBatchRunNumber(DataGridView grid)
        {
            string batchRunNumber = null;
            var row = grid.CurrentCell.RowIndex;
            var column = grid.Columns["Batch Run Number"]?.Index;
            if (column != null)
            {
                batchRunNumber = grid.Rows[row].Cells[(int)column].Value.ToString();
            }
            return batchRunNumber;
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
                    if (cellHeaderText.Equals("Task ID"))
                    {
                        // Set pressed cell as active
                        dataGridView1.CurrentCell = dataGridView1.Rows[hitTestInfo.RowIndex]
                            .Cells[hitTestInfo.ColumnIndex];

                        // Set cell to be selected 
                        dataGridView1.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex].Selected = true;

                        // Focus on the grid
                        dataGridView1.Focus();

                        // Show context menu items
                        _taskIdContextMenu.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                }
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string selectedFunc = ((ComboboxItem) comboBoxFunc.SelectedItem).Value.ToString();
            // Create query
            ResetComps();
            ReportQuery newQuery = new ReportQuery(
                @"..\..\Resources\SQL\" + selectedFunc,
                fromDate.Value.ToShortDateString(),
                toDate.Value.ToShortDateString()
            );

            // Set the datasource, run query (populate grid in backgroundWorker1_RunWorkerCompleted)   
            dataGridView1.DataSource = bindingSource1;
            progressBar1.Visible = true;
            dataGridView1.Show();
            backgroundWorker1.RunWorkerAsync(newQuery);
        }

        private void ResetComps()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
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
            exportButton.Visible = dataGridView1.Rows.Count != 0;
        }

        /// <summary> 
        /// Exports the datagridview values to Excel. 
        /// </summary> 
        private void ExportToExcel()
        {
            // Creating a Excel object. 
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);

            try
            {
                Microsoft.Office.Interop.Excel._Worksheet worksheet = workbook.ActiveSheet;

                worksheet.Name = "ExportedFromDatGrid";

                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                //Loop through each row and read value from each column. 
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == 1)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Columns[j].HeaderText;
                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] =
                                dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                //Getting the location and file name of the excel to save from user. 
                SaveFileDialog saveDialog =
                    new SaveFileDialog
                    {
                        Filter = @"Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                        FilterIndex = 2
                    };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show(@"Export Successful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
    }
}