using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Forms
{
    public partial class EftExportValidateForm : Form
    {
        private readonly List<int> _idNums = new List<int>();

        public EftExportValidateForm()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            ResetComps();
            EftExportQuery query =
                new EftExportQuery(@"..\..\Resources\SQL\IEftExport.sql");

            var eftFileContent = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\EFTExport.txt");
            StructureValidation(eftFileContent);
            //FileValidations(eftFileContent);
            foreach (var row in eftFileContent)
            {
                if (row.StartsWith("6"))
                {
                    int intData;
                    int.TryParse(row.Substring(48, 6), out intData);
                    _idNums.Add(intData);
                }
            }
            //_idNums.Sort((i1, i2) => i2.CompareTo(i1));
            TriggerBgWorkerForQuery(query);
        }

        private void StructureValidation(string[] eftFileContent)
        {
            bool mainShulter = false, childShulter = false, exitLoop = false;
            for (int i = 0; i < eftFileContent.Length; i++)
            {
                if (string.IsNullOrEmpty(eftFileContent[i]))
                {
                    errorTextBox.AppendText($@"Empty row! Line {i + 1}{Environment.NewLine}");
                }
                else
                {
                    switch (eftFileContent[i][0])
                    {
                        case '1':
                            if (i != 0)
                            {
                                errorTextBox.AppendText(
                                    $@"Beginning of file, not in the beginning. Line {i + 1}{Environment.NewLine}");
                            }
                            mainShulter = true;
                            break;
                        case '5':
                            //if (!mainShulter)
                            //{
                            //    errorTextBox.AppendText(
                            //        $@"Beginning of company before beginning of file. Line {i + 1}{
                            //                Environment.NewLine
                            //            }");
                            //}
                            if (childShulter)
                            {
                                errorTextBox.AppendText(
                                    $@"Beginning of company while company didn't close. Line {i + 1}{
                                            Environment.NewLine
                                        }");
                            }
                            childShulter = true;
                            break;
                        case '6':
                        case '7':
                            if (!childShulter)
                            {
                                errorTextBox.AppendText(
                                    $@"Bank row without company open. Line {i + 1}{Environment.NewLine}");
                            }
                            break;
                        case '8':
                            if (!childShulter)
                            {
                                errorTextBox.AppendText(
                                    $@"End company without beginning. Line {i + 1}{Environment.NewLine}");
                            }
                            childShulter = false;
                            break;
                        case '9':
                            if (!mainShulter)
                            {
                                errorTextBox.AppendText(
                                    $@"End of file without beginning. Line {i + 1}{Environment.NewLine}");
                            }
                            else
                            {
                                exitLoop = true;
                            }
                            mainShulter = false;
                            break;
                    }
                    if (exitLoop)
                    {
                        break;
                    }
                }
            }
            if (mainShulter)
            {
                errorTextBox.AppendText(
                    $@"No End Of File. {Environment.NewLine}");
            }
            if (childShulter)
            {
                errorTextBox.AppendText(
                    $@"Missing Company End. {Environment.NewLine}");
            }
        }

        private void TriggerBgWorkerForQuery(EftExportQuery query)
        {
            progressBar1.Visible = true;
            BackgroundWorker backgroundWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            progressBar1.Visible = true;
            backgroundWorker.DoWork += (o, args) =>
            {
                EftExportQuery report = args.Argument as EftExportQuery;
                args.Result = report?.DoQuery();
            };
            backgroundWorker.RunWorkerCompleted += (o, args) =>
            {
                DataTable workerResult = (DataTable) args.Result;
                //DataView dv = workerResult.DefaultView;
                //dv.Sort = "individual_identification_number desc";
                //DataTable sorted = dv.ToTable();
                var list = workerResult.Rows.OfType<DataRow>()
                    .Select(dr => dr.Field<string>("individual_identification_number")).ToList();
                List<int> intList = list.Select(int.Parse).ToList();
                //intList.Sort((i1, i2) => i2.CompareTo(i1));
                var inFileNotInDb = _idNums.Except(intList).ToList();
                var inDbNotInFile = intList.Except(_idNums).ToList();
                bindingSource1.DataSource = inFileNotInDb.Select(x => new {Value = x}).ToList();
                bindingSource2.DataSource = inDbNotInFile.Select(x => new {Value = x}).ToList();
                dataGridView1.DataSource = bindingSource1;
                dataGridView2.DataSource = bindingSource2;
                progressBar1.Visible = false;
            };
            backgroundWorker.RunWorkerAsync(query);
        }

        private void ResetComps()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            errorTextBox.Text = "";
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType &&
                            prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                    ? Nullable.GetUnderlyingType(prop.PropertyType)
                    : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}