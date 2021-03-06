﻿using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Forms
{
    public partial class FindObjectForm : Form
    {
        private readonly ContextMenu _taskIdContextMenu = new ContextMenu();

        public FindObjectForm()
        {
            InitializeComponent();
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            FindObjectQuery query =
                new FindObjectQuery(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\SQL\FindObject.sql", 
                fromDate.Text, 
                toDate.Text, 
                entityTextBox1.Text, 
                entityTextBox2.Text, 
                entityTextBox3.Text);



            var quer = QueryRepo.FindObject;
            quer.Params = new ParamObject(
                $@"'{fromDate.Value:MM/dd/yyyy}'",
                $@"'{toDate.Value:MM/dd/yyyy}'",
                $@"'{entityTextBox1.Text}'",
                string.IsNullOrEmpty(entityTextBox2.Text) ? "" : $@"OR PRIMARY_KEY LIKE '{entityTextBox2.Text}'",
                string.IsNullOrEmpty(entityTextBox3.Text) ? "" : $@"OR PRIMARY_KEY LIKE '{entityTextBox3.Text}'");

            dataGridView1.DataSource = bindingSource1;
            dataGridView1.Hide();
            progressBar1.Visible = true;
            var result = await QueryManager.Query(quer.QueryDynamication());
            bindingSource1.DataSource = result;
            progressBar1.Visible = false;
            dataGridView1.Show();
        }

        private void FindObjectForm_Load(object sender, EventArgs e)
        {
            fromDate.Value = DateTime.Today.AddDays(-1);
            _taskIdContextMenu.MenuItems.Add("Find Object's Refs", ObjectRefs);
        }

        private void ObjectRefs(object sender, EventArgs e)
        {
            // Get Value
            var currentValue = dataGridView1.CurrentCell.Value.ToString();
            string queryToUse = GetQueryName(GetTaskId(dataGridView1));

            // Create query, and send it to form
            RefQuery refQuery = new RefQuery(Path.GetDirectoryName(Application.ExecutablePath)+$@"\Resources\SQL\{queryToUse}", 
                currentValue);

            // Run Query
            progressBar1.Visible = true;
            BackgroundWorker backgroundWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            backgroundWorker.DoWork += (o, args) =>
            {
                RefQuery report = args.Argument as RefQuery;
                try
                {
                    args.Result = report?.DoQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            backgroundWorker.RunWorkerCompleted += (o, args) =>
            {
                DataTable workerResult = (DataTable)args.Result;
                bindingSource2.DataSource = workerResult;
                dataGridView2.DataSource = bindingSource2;
                //foreach (DataGridViewRow dr in dataGridView2.Rows)
                //{
                //    TreeNode tn = new TreeNode("Tree");
                //    foreach (DataGridViewCell cell in dr.Cells)
                //    {
                //        tn.Nodes.Add(cell.Value as string);
                //    }
                //    treeView1.Nodes.Add(tn);
                //}

                progressBar1.Visible = false;
            };
            backgroundWorker.RunWorkerAsync(refQuery);
        }

        private string GetQueryName(string currentValue)
        {
            string queryName = "";
            switch (currentValue)
            {
                case "3097":
                    queryName = "CmaByBatchRunNo.sql";
                    break;
            }
            return queryName;
        }

        private void TriggerBgWorkerForQuery(FindObjectQuery query)
        {
            progressBar1.Visible = true;
            BackgroundWorker backgroundWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            backgroundWorker.DoWork += (o, args) =>
            {
                FindObjectQuery report = args.Argument as FindObjectQuery;
                try
                {
                    args.Result = report?.DoQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }               
            };
            backgroundWorker.RunWorkerCompleted += (o, args) =>
            {
                DataTable workerResult = (DataTable)args.Result;
                bindingSource1.DataSource = workerResult;
                dataGridView1.DataSource = bindingSource1;
                progressBar1.Visible = false;
            };
            backgroundWorker.RunWorkerAsync(query);
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            var hitTestInfo = dataGridView1.HitTest(e.X, e.Y);
            if (e.Button == MouseButtons.Right && hitTestInfo.RowIndex != -1 && hitTestInfo.ColumnIndex != -1)
            {                               
                // Set pressed cell as active
                dataGridView1.CurrentCell = dataGridView1.Rows[hitTestInfo.RowIndex]
                    .Cells[hitTestInfo.ColumnIndex];

                // Set cell to be selected 
                dataGridView1.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex].Selected = true;

                // Focus on the grid
                dataGridView1.Focus();

                if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
                {
                    var cellHeaderText = dataGridView1.Columns[hitTestInfo.ColumnIndex].HeaderText;                   
                    if (cellHeaderText.Equals("Task ID"))
                    {
                        // Get Task Id
                        var taskId = dataGridView1.CurrentCell.Value.ToString();
                        if (taskId.Equals("1") || taskId.Equals("2"))
                        {
                            // Show context menu items
                            _taskIdContextMenu.Show(dataGridView1, new Point(e.X, e.Y));
                        }                       
                    }
                    if (cellHeaderText.Equals("Batch Run No"))
                    {
                        // Get Task Id
                        var taskId = GetTaskId(dataGridView1);
                        if (taskId.Equals("3097"))
                        {
                            // Show context menu items
                            _taskIdContextMenu.Show(dataGridView1, new Point(e.X, e.Y));
                        }
                    }
                }
            }
        }

        private string GetTaskId(DataGridView grid)
        {
            string batchRunNumber = null;
            var row = grid.CurrentCell.RowIndex;
            var column = grid.Columns["Task ID"]?.Index;
            if (column != null)
            {
                batchRunNumber = grid.Rows[row].Cells[(int)column].Value.ToString();
            }
            return batchRunNumber;
        }

        private void entityTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entityTextBox1.Text))
            {
                entityTextBox2.Enabled = true;
            }
            else
            {
                entityTextBox2.Enabled = false;
            }
        }

        private void entityTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entityTextBox2.Text))
            {
                entityTextBox3.Enabled = true;
            }
            else
            {
                entityTextBox3.Enabled = false;
            }
        }

        private void fromDate_ValueChanged(object sender, EventArgs e)
        {
            toDate.Value = fromDate.Value.AddDays(1);
        }
    }
}
