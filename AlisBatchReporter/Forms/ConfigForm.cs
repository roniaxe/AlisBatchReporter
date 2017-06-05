using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using AlisBatchReporter.Classes;
using AlisBatchReporter.Infra;

namespace AlisBatchReporter.Forms
{
    public partial class ConfigForm : Form
    {
        private readonly StringBuilder _connString = new StringBuilder();
        private List<ComboboxItem> _envComboboxItems;

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            //ReadConfigurations();
            PopulateEnvCombobox();
            checkBoxSave.Checked = Global.SavedCheckBox;
        }

        private void PopulateEnvCombobox()
        {
            _envComboboxItems = new List<ComboboxItem>
            {
                new ComboboxItem()
                {
                    Text = "Prod",
                    Value = "10.134.5.30"
                },
                new ComboboxItem
                {
                    Text = "Rackspace",
                    Value = "756027-LSQLDEV1.FBLIFE.COM"
                },
                new ComboboxItem
                {
                    Text = "Sapiens",
                    Value = "alis-db-sql3"
                }
            };
            comboBoxEnv.Items.AddRange(_envComboboxItems.ToArray());
            ComboboxItem selected = _envComboboxItems.Find(item => item.Text.Equals(Global.Env));
            comboBoxEnv.SelectedItem = selected;
            //ReadFromAppSettings();
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
            if (ValidateForm())
            {
                string user = textBoxUser.Text;
                string pass = textBoxPassword.Text.Protect();
                string serverAddress = textBoxServerAddress.Text;
                string db = (string) comboBoxDb.SelectedItem;
                if (checkBoxSave.Checked)
                {                    
                    if (((ComboboxItem) comboBoxEnv.SelectedItem).Text.Equals("Rackspace"))
                    {
                        Properties.Settings.Default.RackspaceUser = user;
                        Properties.Settings.Default.RackspacePass = pass;
                        Properties.Settings.Default.RackspaceAdd = serverAddress;
                        Properties.Settings.Default.RackspaceDb = db;
                    }
                    if (((ComboboxItem) comboBoxEnv.SelectedItem).Text.Equals("Prod"))
                    {
                        Properties.Settings.Default.ProdUser = user;
                        Properties.Settings.Default.ProdPass = pass;
                        Properties.Settings.Default.ProdAdd = serverAddress;
                        Properties.Settings.Default.ProdDb = db;
                    }
                    if (((ComboboxItem) comboBoxEnv.SelectedItem).Text.Equals("Sapiens"))
                    {
                        Properties.Settings.Default.SapiensUser = user;
                        Properties.Settings.Default.SapiensPass = pass;
                        Properties.Settings.Default.SapiensAdd = serverAddress;
                        Properties.Settings.Default.SapiensDb = db;
                    }
                    Properties.Settings.Default.LastSaveEnv = ((ComboboxItem)comboBoxEnv.SelectedItem).Text;
                    Properties.Settings.Default.LastSaveConnStr = _connString.ToString();
                    Properties.Settings.Default.LastSaveDb = db;
                }
                else
                {
                    Properties.Settings.Default.LastSaveEnv = null;
                    Properties.Settings.Default.LastSaveConnStr = null;
                    Properties.Settings.Default.LastSaveDb = null;
                }
                Properties.Settings.Default.SaveCredentialsSelected = checkBoxSave.Checked;
                Properties.Settings.Default.Save();
                UpdateConnString();
                Global.ChosenConnection = _connString.ToString();
                Global.Env = ((ComboboxItem) comboBoxEnv.SelectedItem).Text;
                Global.Db = db;
                Close();
                ((MainForm) ParentForm)?.Activate();
            }
        }

        private bool ValidateForm()
        {
            bool isValid = true;
            // Clear error provider only once.
            errorProvider1.Clear();

            //use if condition for every condtion, dont use else-if
            if (comboBoxEnv.SelectedItem == null)
            {
                errorProvider1.SetError(comboBoxEnv, "field required!");
                isValid = false;
            }

            if (string.IsNullOrEmpty(textBoxServerAddress.Text.Trim()))
            {
                errorProvider1.SetError(textBoxServerAddress, "field required!");
                isValid = false;
            }

            if (string.IsNullOrEmpty(textBoxUser.Text.Trim()))
            {
                errorProvider1.SetError(textBoxUser, "field required!");
                isValid = false;
            }

            if (string.IsNullOrEmpty(textBoxPassword.Text.Trim()))
            {
                errorProvider1.SetError(textBoxPassword, "field required!");
                isValid = false;
            }

            if (comboBoxDb.SelectedItem == null)
            {
                errorProvider1.SetError(comboBoxDb, "field required!");
                isValid = false;
            }
            return isValid;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            Close();
            ((MainForm) ParentForm)?.Activate();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            InitComponents();
        }

        private void InitComponents()
        {
            textBoxServerAddress.Text = "";
            textBoxUser.Text = "";
            textBoxPassword.Text = "";
            checkBoxSave.Checked = false;
            comboBoxDb.SelectedItem = null;
            comboBoxDb.DataSource = null;
            comboBoxDb.Items.Clear();
        }

        private void buttonGetDb_Click(object sender, EventArgs e)
        {
            if (comboBoxEnv.Items.Count == 0 || textBoxUser.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show(@"Fill Mandatory Fields");
            }
            else
            {
                try
                {
                    List<string> list = new List<string>();
                    UpdateConnString();

                    using (SqlConnection con = new SqlConnection(_connString.ToString()))
                    {
                        con.Open();
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
                catch (SqlException sqlException)
                {
                    MessageBox.Show(sqlException.Message);
                }
            }
        }

        private void UpdateConnString()
        {
            _connString.Clear();

            _connString
                .Append("Data Source=")
                .Append(textBoxServerAddress.Text).Append(";")
                .Append("User ID=").Append(textBoxUser.Text).Append(";")
                .Append("Password=").Append(textBoxPassword.Text).Append(";")
                .Append("Persist Security Info=True;");
            if (comboBoxDb.SelectedItem != null)
            {
                _connString.Append("Initial Catalog=")
                    .Append(comboBoxDb.SelectedItem).Append(";");
            }
        }

        private void comboBoxDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateConnString();
        }

        private void comboBoxEnv_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadFromAppSettings();
            textBoxServerAddress.Text = ((ComboboxItem) comboBoxEnv.SelectedItem).Value.ToString();
        }

        private void ReadFromAppSettings()
        {
            InitComponents();
            if (Properties.Settings.Default.SaveCredentialsSelected)
            {
                if (((ComboboxItem) comboBoxEnv.SelectedItem).Text.Equals("Rackspace"))
                {
                    textBoxUser.Text = Properties.Settings.Default.RackspaceUser;
                    textBoxServerAddress.Text = Properties.Settings.Default.RackspaceAdd;
                    textBoxPassword.Text = !string.IsNullOrEmpty(Properties.Settings.Default.RackspacePass)
                        ? Properties.Settings.Default.RackspacePass.Unprotect()
                        : "";
                }
                if (((ComboboxItem) comboBoxEnv.SelectedItem).Text.Equals("Prod"))
                {
                    textBoxUser.Text = Properties.Settings.Default.ProdUser;
                    textBoxServerAddress.Text = Properties.Settings.Default.ProdAdd;
                    textBoxPassword.Text = !string.IsNullOrEmpty(Properties.Settings.Default.ProdPass)
                        ? Properties.Settings.Default.ProdPass.Unprotect()
                        : "";
                }
                if (((ComboboxItem) comboBoxEnv.SelectedItem).Text.Equals("Sapiens"))
                {
                    textBoxUser.Text = Properties.Settings.Default.SapiensUser;
                    textBoxServerAddress.Text = Properties.Settings.Default.SapiensAdd;
                    textBoxPassword.Text = !string.IsNullOrEmpty(Properties.Settings.Default.SapiensPass)
                        ? Properties.Settings.Default.SapiensPass.Unprotect()
                        : "";
                }
                checkBoxSave.Checked = Properties.Settings.Default.SaveCredentialsSelected;
            }
        }
    }
}