using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AlisBatchReporter.Classes;
using AlisBatchReporter.Models.EntityFramwork;
using AlisBatchReporter.Properties;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using Global = AlisBatchReporter.Classes.Global;
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
            _batchRunNumberContextMenu.MenuItems.Add("Chunk Process", ChunkProcess);
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
                    Value = 1
                },
                new ComboboxItem
                {
                    Text = @"Task List",
                    Value = 2
                },
                new ComboboxItem
                {
                    Text = @"Processing Rate",
                    Value = 3
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

        private async void ChunkProcess(object sender, EventArgs e)
        {
            var dialogForm = new Form();
            var pb = new ProgressBar {Style = ProgressBarStyle.Marquee};
            var bs = new BindingSource();
            var dg = new DataGridView {DataSource = bs};
            dialogForm.Controls.AddRange(new Control[] {pb, dg});
            var query = QueryRepo.ChunkProcess;
            query.Params = new ParamObject(dataGridView1.CurrentCell.Value.ToString(), GetTaskId(dataGridView1));
            dialogForm.Show();
            var result = await QueryManager.Query(query.QueryDynamication());
            dg.Dock = DockStyle.Fill;
            bs.DataSource = result;
            pb.Hide();
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
            var queryCode = ((ComboboxItem) comboBoxFunc.SelectedItem).Value;
            var selectedQuery = QueryFactory.GetQuery((int) queryCode);

            // Create Query Params
            var typeRadioButton = !string.IsNullOrEmpty(polFilterTextBox.Text)
                ? $@"AND (gba.primary_key LIKE '{polFilterTextBox.Text}' 
                    OR gba.primary_key = convert(VARCHAR(10),(SELECT id FROM p_client_role where policy_no = {
                        polFilterTextBox.Text
                    } and closing_status = 0 and role = 91)))"
                : "";
            var onlyErrors = "AND gba.entry_type in (5,6)";
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
            DataTable result;
            try
            {
                result = await QueryManager.Query(selectedQuery.QueryDynamication());
            }
            catch (SqlException exception)
            {
                MessageBox.Show(exception.Message + $@"{Environment.NewLine}VPN Connected?");
                Console.WriteLine(exception);
                throw;
            }

            bindingSource1.DataSource = result;
            progressBar1.Visible = false;
            dataGridView1.Show();
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
        }

        /// <summary>
        ///     Exports the datagridview values to Excel.
        /// </summary>
        private string ExportToExcel()
        {
            // Creating a Excel object. 
            _Application excel = new Application();
            _Workbook workbook = excel.Workbooks.Add(Type.Missing);
            string fileName = null;
            try
            {
                _Worksheet worksheet = workbook.ActiveSheet;

                worksheet.Name = DateTime.Today.ToString("M");
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
                                dataGridView1.Rows[i - 1].Cells[j].Value.ToString();
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }
                worksheet.Columns.AutoFit();
                worksheet.Range["A1:Z100"].WrapText = false;
                //Getting the location and file name of the excel to save from user. 
                var saveDialog =
                    new SaveFileDialog
                    {
                        Filter = @"Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                        FilterIndex = 2,
                        FileName = Settings.Default.SharingFolder + "Daily_Report_" + Global.SavedCredentials.Name +
                                   "_" + Global.SavedCredentials.Db + "_" + DateTime.Today.ToString("M") + ".xlsx"
                    };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show(@"Export Successful");
                }
                fileName = saveDialog.FileName;
            }
            catch (Exception ex)
            {
                mainPanel.Text += @"Failed!";
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
            }
            return fileName;
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null
                && !string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Equals("Did Not Finish"))
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style =
                    new DataGridViewCellStyle {ForeColor = Color.Red};
            else
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = dataGridView1.DefaultCellStyle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show($@"Send Report To Distribution List?", @"Confirm Send",
                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No) return;
            distributionProcessTxt.Visible = true;
            distributionProcessTxt.Text += @"Creating Report...";
            var attachment = ExportToExcel();
            distributionProcessTxt.Text += $@"Done!{Environment.NewLine}";
            if (!string.IsNullOrEmpty(attachment))
            {
                List<string> to;
                using (var db = new AlisDbContext())
                {
                    to = db.Distributions.Where(d => d.DistFlag).Select(d => d.EmailAddress).ToList();
                }
                if (to.Any())
                {
                    distributionProcessTxt.Text += @"Sending Mails To Distribution List...";
                    var body = $@"Hello, 
Attached the daily batches error report for:
Date: {DateTime.Today:D}
Environment: {Global.SavedCredentials.Name}
DB: {Global.SavedCredentials.Db}";
                    MailModule.SendEmailFromAccount(
                        new Microsoft.Office.Interop.Outlook.Application(),
                        $@"Daily Report - {Global.SavedCredentials.Name}, {Global.SavedCredentials.Db} - {
                                DateTime.Today
                            :D}",
                        to,
                        body,
                        "roni.axelrad@sapiens.com",
                        attachment);
                    MessageBox.Show(@"Mail Sent!");
                }
                else
                {
                    distributionProcessTxt.Text +=
                        $@"Empty Distribution List, Mail Was Not Sent, Please Add Receipients On Settings.";
                }
            }
            distributionProcessTxt.Visible = false;
        }

        private void bindingSource1_ListChanged(object sender, ListChangedEventArgs e)
        {
            exportButton.Visible = bindingSource1.Count > 0;
            sendEmailButton.Visible = bindingSource1.Count > 0;
        }
    }
}