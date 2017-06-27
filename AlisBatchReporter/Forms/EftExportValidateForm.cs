using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
            fileValidations(eftFileContent);
            foreach (var row in eftFileContent)
            {
                if (row.StartsWith("6"))
                {
                    int intData;
                    int.TryParse(row.Substring(48, 6), out intData);
                    _idNums.Add(intData);
                }
            }
            _idNums.Sort((i1, i2) => i2.CompareTo(i1));
            TriggerBgWorkerForQuery(query);
        }

        private void fileValidations(string[] eftFileContent)
        {
            StringBuilder errorString = new StringBuilder();
            bool firstFive = true;
            List<int> construction = new List<int>();
            int listIdx = 0;
            for (int i = 0; i < eftFileContent.Length; i++)
            {
                if (string.IsNullOrEmpty(eftFileContent[i]))
                {
                    errorString.Append($"Empty Row In Line: {i}{Environment.NewLine}");
                }
                if (i == 0)
                {
                    if (!eftFileContent[i].StartsWith("1"))
                    {
                        errorString.Append($"Bad Start Of File: {eftFileContent[i]}{Environment.NewLine}");
                    }
                    construction.Add(1);
                    listIdx++;
                }
                if (eftFileContent[i].StartsWith("5"))
                {
                    if (firstFive)
                    {
                        firstFive = false;
                    }
                    else
                    {
                        if (construction[listIdx - 1] != 8)
                        {
                            errorString.Append($"Comp Start Without End In Line {i}: {eftFileContent[i]}{Environment.NewLine}");
                        }
                    }
                    construction.Add(5);
                    listIdx++;

                }
                if (eftFileContent[i].StartsWith("8"))
                {
                    if (construction[listIdx - 1] != 5)
                    {
                        errorString.Append($"End Comp Without Start In Line {i}: {eftFileContent[i]}{Environment.NewLine}");
                    }
                    construction.Add(8);
                    listIdx++;
                }
                if (i == eftFileContent.Length - 1)
                {
                    if (!eftFileContent[i].StartsWith("9"))
                    {
                        errorString.Append($"No End Of File {i}: {eftFileContent[i]}{Environment.NewLine}");
                    }
                    else
                    {
                        errorString.Append($"End Of File Line: {eftFileContent[i]}{Environment.NewLine}");
                    }
                }
            }
            errorTextBox.Text = errorString.ToString();
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
                DataView dv = workerResult.DefaultView;
                dv.Sort = "individual_identification_number desc";
                DataTable sorted = dv.ToTable();
                var list = sorted.Rows.OfType<DataRow>()
                    .Select(dr => dr.Field<string>("individual_identification_number")).ToList();
                List<int> intList = list.Select(int.Parse).ToList();
                intList.Sort((i1, i2) => i2.CompareTo(i1));
                var inFileNotInDb = _idNums.Except(intList).ToList();
                var inDbNotInFile = intList.Except(_idNums).ToList();
                //var inFileNotInDbBindingList = ToDataTable(inFileNotInDb);
                //var inDbNotInFileBindingList = ToDataTable(inDbNotInFile);
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