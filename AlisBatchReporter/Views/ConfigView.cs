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

namespace AlisBatchReporter.Views
{
    public partial class ConfigView : UserControl
    {
        private readonly StringBuilder _connString = new StringBuilder();
        public ConfigView()
        {
            InitializeComponent();
        }

        private void ConfigView_Load(object sender, EventArgs e)
        {
            PopulateEnvCombobox();
            checkBoxSave.Checked = Global.SavedCheckBox;
        }

        private void PopulateEnvCombobox()
        {
            var envComboboxItems = new List<ComboboxItem>
            {
                new ComboboxItem()
                {
                    Text = "Prod",
                    Value = "10.134.5.30"
                },
                new ComboboxItem
                {
                    Text = "White SIT",
                    Value = "876630-sqldev.fblife.com"
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
            comboBoxEnv.DisplayMember = "Text";
            comboBoxEnv.DataSource = envComboboxItems;
            ComboboxItem selected = envComboboxItems.Find(item => item.Text.Equals(Global.Env)) ?? (ComboboxItem)comboBoxEnv.Items[0];
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
                string db = (string)comboBoxDb.SelectedItem;
                if (checkBoxSave.Checked)
                {
                    UpdateConnString();
                    if (((ComboboxItem)comboBoxEnv.SelectedItem).Text.Equals("Rackspace"))
                    {
                        Properties.Settings.Default.RackspaceUser = user;
                        Properties.Settings.Default.RackspacePass = pass;
                        Properties.Settings.Default.RackspaceAdd = serverAddress;
                        Properties.Settings.Default.RackspaceDb = db;
                    }
                    if (((ComboboxItem)comboBoxEnv.SelectedItem).Text.Equals("Prod"))
                    {
                        Properties.Settings.Default.ProdUser = user;
                        Properties.Settings.Default.ProdPass = pass;
                        Properties.Settings.Default.ProdAdd = serverAddress;
                        Properties.Settings.Default.ProdDb = db;
                    }
                    if (((ComboboxItem)comboBoxEnv.SelectedItem).Text.Equals("Sapiens"))
                    {
                        Properties.Settings.Default.SapiensUser = user;
                        Properties.Settings.Default.SapiensPass = pass;
                        Properties.Settings.Default.SapiensAdd = serverAddress;
                        Properties.Settings.Default.SapiensDb = db;
                    }
                    if (((ComboboxItem)comboBoxEnv.SelectedItem).Text.Equals("White SIT"))
                    {
                        Properties.Settings.Default.WhiteSitUser = user;
                        Properties.Settings.Default.WhiteSitPass = pass;
                        Properties.Settings.Default.WhiteSitAdd = serverAddress;
                        Properties.Settings.Default.WhiteSitDb = db;
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
                Global.SavedCheckBox = checkBoxSave.Checked;
                Global.ChosenConnection = _connString.ToString();
                Global.Env = ((ComboboxItem)comboBoxEnv.SelectedItem).Text;
                Global.Db = db;
                Properties.Settings.Default.SaveCredentialsSelected = Global.SavedCheckBox;
                Properties.Settings.Default.Save();
                Close();
                FocusParent();
            }
        }

        private void FocusParent()
        {
            Form parentForm = Parent as Form;
            parentForm?.BringToFront();
            parentForm?.Activate();
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
            FocusParent();
        }

        private void Close()
        {
            Form tmp = FindForm();
            tmp?.Close();
            tmp?.Dispose();
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
                        using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases ORDER BY name", con))
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
            textBoxServerAddress.Text = ((ComboboxItem)comboBoxEnv.SelectedItem).Value.ToString();
        }

        private void ReadFromAppSettings()
        {
            InitComponents();
            if (Properties.Settings.Default.SaveCredentialsSelected)
            {
                if (((ComboboxItem)comboBoxEnv.SelectedItem).Text.Equals("Rackspace"))
                {
                    textBoxUser.Text = Properties.Settings.Default.RackspaceUser;
                    textBoxServerAddress.Text = Properties.Settings.Default.RackspaceAdd;
                    textBoxPassword.Text = !string.IsNullOrEmpty(Properties.Settings.Default.RackspacePass)
                        ? Properties.Settings.Default.RackspacePass.Unprotect()
                        : "";
                }
                if (((ComboboxItem)comboBoxEnv.SelectedItem).Text.Equals("Prod"))
                {
                    textBoxUser.Text = Properties.Settings.Default.ProdUser;
                    textBoxServerAddress.Text = Properties.Settings.Default.ProdAdd;
                    textBoxPassword.Text = !string.IsNullOrEmpty(Properties.Settings.Default.ProdPass)
                        ? Properties.Settings.Default.ProdPass.Unprotect()
                        : "";
                }
                if (((ComboboxItem)comboBoxEnv.SelectedItem).Text.Equals("Sapiens"))
                {
                    textBoxUser.Text = Properties.Settings.Default.SapiensUser;
                    textBoxServerAddress.Text = Properties.Settings.Default.SapiensAdd;
                    textBoxPassword.Text = !string.IsNullOrEmpty(Properties.Settings.Default.SapiensPass)
                        ? Properties.Settings.Default.SapiensPass.Unprotect()
                        : "";
                }
                if (((ComboboxItem)comboBoxEnv.SelectedItem).Text.Equals("White SIT"))
                {
                    textBoxUser.Text = Properties.Settings.Default.WhiteSitUser;
                    textBoxServerAddress.Text = Properties.Settings.Default.WhiteSitAdd;
                    textBoxPassword.Text = !string.IsNullOrEmpty(Properties.Settings.Default.WhiteSitPass)
                        ? Properties.Settings.Default.WhiteSitPass.Unprotect()
                        : "";
                }
                checkBoxSave.Checked = Global.SavedCheckBox;
            }
        }
    }
}
