using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Forms
{
    public partial class ConfigForm : Form
    {
        private readonly List<ConnectionString> _connStringsList = new List<ConnectionString>();

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            ReadConfigurations();
        }

        private void ReadConfigurations()
        {
            ResetComp();
            var xelement = XElement.Load(@"..\.." + "\\connectionStrings.config");

            //Run query
            var connStringsList = from conn in xelement.Descendants("ConnectionString")
                select new
                {
                    Env = conn.Element("Env")?.Value,
                    Database = conn.Element("DataBase")?.Value,
                    ConnString = conn.Element("Connection")?.Value
                };
            //Loop through results

            foreach (var connectionString in connStringsList)
            {
                ConnectionString conn = new ConnectionString(connectionString.Env,
                    connectionString.Database, connectionString.ConnString);
                AddToComboBox(conn);
                PopulateConnectionStringList(conn);
            }
        }

        private void ResetComp()
        {
            comboBox1.Items.Clear();
            comboBox1.ResetText();
            comboBox1.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void PopulateConnectionStringList(ConnectionString connectionString)
        {
            _connStringsList.Add(connectionString);
        }

        private void AddToComboBox(ConnectionString connectionString)
        {
            ComboboxItem item = new ComboboxItem
            {
                Text = connectionString.Env + ": " + connectionString.Database,
                Value = connectionString.Env
            };
            comboBox1.Items.Add(item);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connectionString = GetConnectionString(((ComboboxItem) comboBox1.SelectedItem).Value);

            if (connectionString != null)
            {
                textBox1.Text = connectionString.Env;
                textBox2.Text = connectionString.Database;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var connectionString = GetConnectionString(((ComboboxItem) comboBox1.SelectedItem).Value);

            if (connectionString != null)
            {
                Global.ChosenConnection = connectionString.ConnString;
                Global.Env = connectionString.Env;
            }
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private ConnectionString GetConnectionString(object item)
        {
            return _connStringsList.FirstOrDefault(conn => conn.Env.Equals(item));
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            ReadConfigurations();
        }
    }
}