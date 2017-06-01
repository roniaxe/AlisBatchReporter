using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Forms
{
    public partial class ConfigForm : Form
    {
        readonly StringBuilder _connString = new StringBuilder();

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            ReadConfigurations();
            PopulateEnvCombobox();
            checkBoxSave.Checked = Properties.Settings.Default.SaveCredentialsSelected;
        }

        private void PopulateEnvCombobox()
        {
            ComboboxItem prod = new ComboboxItem
            {
                Text = "Prod",
                Value = "Data Source=10.134.5.30;Persist Security Info=True;User ID=Ebachmeir;Password=9Ke2n!47T"
            };
            ComboboxItem rackspace = new ComboboxItem
            {
                Text = "Rackspace",
                Value = "Data Source=756027-LSQLDEV1.FBLIFE.COM;Persist Security Info=True;"
            };
            ComboboxItem sapiens = new ComboboxItem
            {
                Text = "Sapiens",
                Value = "Data Source=alis-db-sql3;Persist Security Info=True;User ID=AlisUser;Password=it12345*"
            };
            comboBoxEnv.Items.Add(prod);
            comboBoxEnv.Items.Add(rackspace);
            comboBoxEnv.Items.Add(sapiens);
        }

        private void ReadConfigurations()
        {
            var xelement = XElement.Load(System.IO.Path.GetDirectoryName(Application.ExecutablePath) +
                                         @"\connectionStrings.config");

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
                //PopulateConnectionStringList(conn);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_connString.ToString()))
            {
                Global.ChosenConnection = _connString.ToString();
                Global.Env = ((ComboboxItem) comboBoxEnv.SelectedItem).Text;
                Properties.Settings.Default.SaveCredentialsSelected = checkBoxSave.Checked;
            }
            if (checkBoxSave.Checked)
            {
                if (Global.Env.Equals("Rackspace"))
                {
                    Properties.Settings.Default.RackspaceUser = textBoxUser.Text;
                    Properties.Settings.Default.RackspacePass = textBoxPassword.Text;
                }
                if (Global.Env.Equals("Prod"))
                {
                    Properties.Settings.Default.ProdUser = textBoxUser.Text;
                    Properties.Settings.Default.ProdPass = textBoxPassword.Text;
                }
                if (Global.Env.Equals("Sapiens"))
                {
                    Properties.Settings.Default.SapiensUser = textBoxUser.Text;
                    Properties.Settings.Default.SapiensPass = textBoxPassword.Text;
                }
                Properties.Settings.Default.Save();
            }
            Close();
            ((MainForm) ParentForm)?.Activate();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            ReadConfigurations();
        }

        private void buttonGetDb_Click(object sender, EventArgs e)
        {
            if (comboBoxEnv.Items.Count == 0 || textBoxUser.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show(@"Fill Mandatory Fields");
            }
            else
            {
                List<string> list = new List<string>();
                _connString.Append(((ComboboxItem) comboBoxEnv.SelectedItem).Value)
                    .Append("User ID=").Append(textBoxUser.Text).Append(";")
                    .Append("Password=").Append(textBoxPassword.Text).Append(";");

                using (SqlConnection con = new SqlConnection(_connString.ToString()))
                {
                    con.Open();

                    // Set up a command with the given query and associate
                    // this with the current connection.
                    using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                    {
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(dr[0].ToString());
                            }
                        }
                    }
                }
                comboBoxDb.DataSource = list;
            }
        }

        private void comboBoxDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDb.SelectedItem != null)
            {
                _connString.Append("Initial Catalog=")
                    .Append(comboBoxDb.SelectedItem);
            }
        }

        private void comboBoxEnv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SaveCredentialsSelected)
            {
                if (Global.Env.Equals("Rackspace"))
                {
                    textBoxUser.Text = Properties.Settings.Default.RackspaceUser;
                    textBoxPassword.Text = Properties.Settings.Default.RackspacePass;
                }
                if (Global.Env.Equals("Prod"))
                {
                    textBoxUser.Text = Properties.Settings.Default.ProdUser;
                    textBoxPassword.Text = Properties.Settings.Default.ProdPass;
                }
                if (Global.Env.Equals("Sapiens"))
                {
                    textBoxUser.Text = Properties.Settings.Default.SapiensUser;
                    textBoxPassword.Text = Properties.Settings.Default.SapiensPass;
                }
            }
        }
    }
}