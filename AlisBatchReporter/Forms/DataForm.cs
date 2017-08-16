using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using AlisBatchReporter.Classes;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using MenuItem = System.Windows.Forms.MenuItem;
using Point = System.Drawing.Point;

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

            // Create the context menu items
            _batchRunNumberContextMenu.MenuItems.Add("Load GBA - Errors only", LoadGbaData);
            _batchRunNumberContextMenu.MenuItems.Add("Load GBA - All", LoadGbaData);
            _taskIdContextMenu.MenuItems.Add("Running Time", RunningTime);
            fromDate.Value = DateTime.Today.AddDays(-1);
            PopulateFuncCombobox();
        }

        private void PopulateFuncCombobox()
        {
            ComboboxItem[] funcItems =
            {
                new ComboboxItem
                {
                    Text = @"Report",
                    Value = @"\Resources\SQL\BatchAudit.sql"
                },
                new ComboboxItem
                {
                    Text = @"Task List",
                    Value = @"\Resources\SQL\TaskList.sql"
                },
                new ComboboxItem
                {
                    Text = @"Processing Rate",
                    Value = @"\Resources\SQL\TaskList.sql"
                }
            };
            comboBoxFunc.DisplayMember = "Text";
            comboBoxFunc.ValueMember = "Value";
            comboBoxFunc.Items.AddRange(funcItems.ToArray<object>());
            comboBoxFunc.SelectedIndex = 0;
        }

        private async void RunningTime(object sender, EventArgs e)
        {
            // Get the right click batch run number
            var taskId = dataGridView1.CurrentCell.Value.ToString();

            var query = QueryRepo.TaskRunningTime;
            query.Params = new ParamObject(GetBatchRunNumber(dataGridView1), taskId, "false");
            progressBar1.Visible = true;
            var result = await QueryManager.Query(query.QueryDynamication());
            var time = TimeSpan.FromSeconds((int) result.Rows[0].ItemArray[0]);
            var str = time.ToString(@"hh\:mm\:ss\:fff");
            progressBar1.Visible = false;
            MessageBox.Show($@"Seconds Ran: {str}");
        }

        private void LoadGbaData(object sender, EventArgs e)
        {
            var errorsOnly = ((MenuItem) sender).Text.Equals("Load GBA - Errors only")
                ? "AND GBA.ENTRY_TYPE IN (5,6)"
                : "";
            var batchRunNum = dataGridView1.CurrentCell.Value.ToString();
            var query = QueryRepo.SimpleGba;
            query.Params = new ParamObject(batchRunNum, GetTaskId(dataGridView1), errorsOnly);
            var singleForm = new SingleBatchRunForm(query);
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
                taskId = grid.Rows[row].Cells[(int) column].Value.ToString();
            return taskId;
        }

        private string GetBatchRunNumber(DataGridView grid)
        {
            string batchRunNumber = null;
            var row = grid.CurrentCell.RowIndex;
            var column = grid.Columns["Batch Run Number"]?.Index;
            if (column != null)
                batchRunNumber = grid.Rows[row].Cells[(int) column].Value.ToString();
            return batchRunNumber;
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            var hitTestInfo = dataGridView1.HitTest(e.X, e.Y);
            if (e.Button == MouseButtons.Right && hitTestInfo.RowIndex != -1 && hitTestInfo.ColumnIndex != -1)
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
                    if (cellHeaderText.Equals("Task Id"))
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

        private async void createButton_Click(object sender, EventArgs e)
        {
            // Create Query
            var selectedQuery = ((ComboboxItem) comboBoxFunc.SelectedItem).Text.Equals("Task List")
                ? QueryRepo.TaskList
                : QueryRepo.ErrorReport;

            if (((ComboboxItem) comboBoxFunc.SelectedItem).Text.Equals("Processing Rate"))
            {
                selectedQuery = QueryRepo.ProcessingRateReport;
            }

            // Create Query Params
            var typeRadioButton = !string.IsNullOrEmpty(polFilterTextBox.Text)
                ? $@"AND gba.primary_key LIKE '{polFilterTextBox.Text}'"
                : "";
            string onlyErrors = "AND gba.entry_type in (5,6)";
            if (!string.IsNullOrEmpty(polFilterTextBox.Text) && allTypesRadioButton.Checked)
                onlyErrors = "";

            // Adding Params to Query
            selectedQuery.Params = new ParamObject(
                $@"'{fromDate.Value:MM/dd/yyyy}'",
                $@"'{toDate.Value:MM/dd/yyyy}'",
                typeRadioButton,
                onlyErrors);

            ResetComps();
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.Hide();
            progressBar1.Visible = true;
            var result = await QueryManager.Query(selectedQuery.QueryDynamication());
            bindingSource1.DataSource = result;
            progressBar1.Visible = false;
            dataGridView1.Show();
            exportButton.Visible = dataGridView1.Rows.Count != 0;

            //var newQuery = new ReportQuery(
            //    Path.GetDirectoryName(Application.ExecutablePath) + selectedFunc,
            //    fromDate.Value.ToString("MM/dd/yyyy"),
            //    toDate.Value.ToString("MM/dd/yyyy"),
            //    polFilterTextBox.Text,
            //    typeRadioButton
            //);
            //// Set the datasource, run query (populate grid in backgroundWorker1_RunWorkerCompleted)   
            //dataGridView1.DataSource = bindingSource1;
            //progressBar1.Visible = true;
            //dataGridView1.Show();
            //backgroundWorker1.RunWorkerAsync(newQuery);
        }

        private void ResetComps()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var report = e.Argument as ReportQuery;
            try
            {
                e.Result = report?.DoQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            var workerResult = (DataTable) e.Result;
            bindingSource1.DataSource = workerResult;
            progressBar1.Visible = false;
            exportButton.Visible = dataGridView1.Rows.Count != 0;
        }

        /// <summary>
        ///     Exports the datagridview values to Excel.
        /// </summary>
        private void ExportToExcel()
        {
            // Creating a Excel object. 
            _Application excel = new Application();
            _Workbook workbook = excel.Workbooks.Add(Type.Missing);

            try
            {
                _Worksheet worksheet = workbook.ActiveSheet;

                worksheet.Name = "ExportedFromDatGrid";

                var cellRowIndex = 1;
                var cellColumnIndex = 1;

                //Loop through each row and read value from each column. 
                for (var i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (var j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == 1)
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Columns[j].HeaderText;
                        else
                            worksheet.Cells[cellRowIndex, cellColumnIndex] =
                                dataGridView1.Rows[i-1].Cells[j].Value.ToString();
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                //Getting the location and file name of the excel to save from user. 
                var saveDialog =
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

        private void fromDate_ValueChanged(object sender, EventArgs e)
        {
            toDate.Value = fromDate.Value.AddDays(1);
        }

        private void comboBoxFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboboxItem) comboBoxFunc.SelectedItem).Text.Equals("Report"))
            {
                polFilterLabel.Visible = true;
                polFilterTextBox.Visible = true;
                groupBox2.Visible = true;
            }
            else
            {
                groupBox2.Visible = false;
                polFilterTextBox.Text = "";
                polFilterLabel.Visible = false;
                polFilterTextBox.Visible = false;
            }
        }
    }
}