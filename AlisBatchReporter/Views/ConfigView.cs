using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using AlisBatchReporter.Classes;
using AlisBatchReporter.Infra;
using AlisBatchReporter.Models;

namespace AlisBatchReporter.Views
{
    public partial class ConfigView : UserControl
    {
        private readonly StringBuilder _connString = new StringBuilder();
        private bool _envNoise;

        public ConfigView()
        {
            InitializeComponent();
        }

        private void ConfigView_Load(object sender, EventArgs e)
        {
            PopulateEnvCombobox();
        }

        private void PopulateEnvCombobox()
        {
            var envComboboxItems = new List<ComboboxItem>
            {
                new ComboboxItem
                {
                    Text = "Prod",
                    Value = 1
                },
                new ComboboxItem
                {
                    Text = "Uat",
                    Value = 2
                },
                new ComboboxItem
                {
                    Text = "White/Red/Blue",
                    Value = 3
                },
                new ComboboxItem
                {
                    Text = "Sapiens",
                    Value = 4
                }
            };
            comboBoxEnv.DisplayMember = "Text";
            _envNoise = false;
            comboBoxEnv.DataSource = envComboboxItems;
            _envNoise = true;
            var selected = envComboboxItems.Find(item => item.Value.Equals(Global.SavedCredentials?.Id)) ??
                           envComboboxItems.FirstOrDefault();
            comboBoxEnv.SelectedItem = selected;
            ReadFromAppSettings();
        }

        private void ReadConfigurations()
        {
            var xelement = XElement.Load(Path.GetDirectoryName(Application.ExecutablePath) +
                                         @"\connectionStrings.config");

            //Run query
            var connStringsList = from conn in xelement.Descendants("ConnectionString")
                select new
                {
                    Env = conn.Element("Name")?.Value,
                    Database = conn.Element("DataBase")?.Value,
                    ConnString = conn.Element("Connection")?.Value
                };
            //Loop through results

            foreach (var connectionString in connStringsList)
            {
                //PopulateConnectionStringList(conn);
            }
        }

        private void DbQuery(string command)
        {
            using (var conn =
                new SqlCeConnection("DataSource=\"alisReporter.sdf\"; Password=\"12345\";"))
            {
                conn.Open();
                var cmd = new SqlCeCommand(command, conn);
                cmd.ExecuteNonQuery();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateForm(false)) return;
            var envId = ((ComboboxItem) comboBoxEnv.SelectedItem).Value;
            var user = textBoxUser.Text;
            var pass = textBoxPassword.Text.Protect();
            var envName = ((ComboboxItem) comboBoxEnv.SelectedItem).Text;
            var serverAddress = textBoxServerAddress.Text;
            var db = (string) comboBoxDb.SelectedItem;
            UpdateConnString();
            using (var dbContext = new AlisDbContext())
            {
                dbContext.SavedCredentialses.ToList().ForEach(c => c.ChoseLast = false);
                dbContext.SaveChanges();
                var toUpdate = dbContext.SavedCredentialses.Find(envId);
                if (toUpdate != null)
                {
                    toUpdate.Username = user;
                    toUpdate.Password = pass;
                    toUpdate.Host = serverAddress;
                    toUpdate.Db = db;
                    toUpdate.Name = envName;
                    toUpdate.ConnString = _connString.ToString();
                    toUpdate.ChoseLast = true;
                    toUpdate.Saved = checkBoxSave.Checked;
                    if (checkBoxSave.Checked)
                        dbContext.SaveChanges();
                }
                Global.PropSetter(toUpdate);
            }
            Close();
        }

        private bool ValidateForm(bool dbValidation)
        {
            var isValid = true;
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

            if (!dbValidation && comboBoxDb.SelectedItem == null)
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
        }

        private void Close()
        {
            var tmp = FindForm();
            tmp?.Close();
            tmp?.Dispose();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(@"To Delete Saved Credentials??",
                @"Confirm Delete!",
                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                using (var dbContext = new AlisDbContext())
                {
                    dbContext.SavedCredentialses.ToList().ForEach(cred => cred.Saved = false);
                    dbContext.SaveChanges();
                }
                InitComponents();
            }
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
            if (!ValidateForm(true)) return;
            string dbListQuery = "";
            switch (((ComboboxItem)comboBoxEnv.SelectedValue).Value)
            {
                case 1:
                    dbListQuery = @"SELECT db_name FROM auth_db_prod.dbo.sys_auth_data";
                    break;
                case 2:
                    dbListQuery = @"SELECT db_name FROM auth_uat.dbo.sys_auth_data";
                    break;
                case 3:
                    dbListQuery = @"SELECT db_name FROM 
                                    SIT_red_auth_db.dbo.sys_auth_data
                                    UNION
                                    SELECT db_name FROM 
                                    SIT_white_auth_db.dbo.sys_auth_data
                                    UNION
                                    SELECT db_name FROM 
                                    SIT_blue_auth_db.dbo.sys_auth_data";
                    break;
                case 4:
                    dbListQuery = @"SELECT db_name FROM sys_auth_data";
                    break;
            }
            try
            {
                UpdateConnString();

                var list = new List<string>();                
                using (var con = new SqlConnection(_connString.ToString()))
                {
                    con.Open();                   
                    using (var cmd = new SqlCommand(dbListQuery, con))
                    {
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                                list.Add(dr[0].ToString());
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

        private void UpdateConnString()
        {
            _connString.Clear();

            _connString
                .Append("Data Source=").Append(textBoxServerAddress.Text).Append(";")
                .Append("User ID=").Append(textBoxUser.Text).Append(";")
                .Append("Password=").Append(textBoxPassword.Text).Append(";")
                .Append("Persist Security Info=True;");
            if (comboBoxDb.SelectedItem != null)
                _connString.Append("Initial Catalog=").Append(comboBoxDb.SelectedItem).Append(";");
        }

        private void comboBoxDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateConnString();
        }

        private void comboBoxEnv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_envNoise)
                return;
            ReadFromAppSettings();
        }

        private void ReadFromAppSettings()
        {
            InitComponents();
            var envId = ((ComboboxItem) comboBoxEnv.SelectedItem).Value;
            using (var dbContext = new AlisDbContext("CompactDBContext"))
            {
                var queryResult = dbContext.SavedCredentialses.Find(envId);

                if (queryResult == null) return;

                textBoxServerAddress.Text = queryResult.Host;

                if (!queryResult.Saved) return;

                checkBoxSave.Checked = queryResult.Saved;
                textBoxPassword.Text = string.IsNullOrEmpty(queryResult.Password)
                    ? ""
                    : queryResult.Password.Unprotect();
                textBoxUser.Text = queryResult.Username;
            }
        }
    }
}