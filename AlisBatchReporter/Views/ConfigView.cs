using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        private readonly AlisDbContext _db;
        private bool _envNoise;

        public ConfigView(DbContext db)
        {
            _db = (AlisDbContext) db;
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
            if (!ValidateForm()) return;
            var envId = ((ComboboxItem) comboBoxEnv.SelectedItem).Value;
            var user = textBoxUser.Text;
            var pass = textBoxPassword.Text.Protect();
            var envName = ((ComboboxItem) comboBoxEnv.SelectedItem).Text;
            var serverAddress = textBoxServerAddress.Text;
            var db = (string) comboBoxDb.SelectedItem;
            UpdateConnString();
            _db.SavedCredentialses.ToList().ForEach(c => c.ChoseLast = false);
            _db.SaveChanges();
            var toUpdate = _db.SavedCredentialses.FirstOrDefault(sc => sc.Id == (int) envId);
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
                    _db.SaveChanges();
            }
            Global.PropSetter(toUpdate);
            Close();
            FocusParent();
        }

        private void FocusParent()
        {
            var parentForm = Parent as Form;
            parentForm?.BringToFront();
            parentForm?.Activate();
        }

        private bool ValidateForm()
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
            var tmp = FindForm();
            tmp?.Close();
            tmp?.Dispose();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            const string command1 = @"UPDATE saved_credentials SET SAVED='0'";
            DbQuery(command1);
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
                MessageBox.Show(@"Fill Mandatory Fields");
            else
                try
                {
                    var list = new List<string>();
                    UpdateConnString();

                    using (var con = new SqlConnection(_connString.ToString()))
                    {
                        con.Open();
                        using (var cmd = new SqlCommand("SELECT name from sys.databases ORDER BY name", con))
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
                .Append("Data Source=")
                .Append(textBoxServerAddress.Text).Append(";")
                .Append("User ID=").Append(textBoxUser.Text).Append(";")
                .Append("Password=").Append(textBoxPassword.Text).Append(";")
                .Append("Persist Security Info=True;");
            if (comboBoxDb.SelectedItem != null)
                _connString.Append("Initial Catalog=")
                    .Append(comboBoxDb.SelectedItem).Append(";");
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
            var queryResult = _db.SavedCredentialses
                .FirstOrDefault(item => item.Id == (int) envId);
            if (queryResult != null)
            {
                textBoxServerAddress.Text = queryResult.Host;
                if (queryResult.Saved)
                {
                    checkBoxSave.Checked = queryResult.Saved;
                    textBoxPassword.Text = string.IsNullOrEmpty(queryResult.Password)
                        ? ""
                        : queryResult.Password.Unprotect();
                    textBoxUser.Text = queryResult.Username;
                }
            }
        }
    }
}