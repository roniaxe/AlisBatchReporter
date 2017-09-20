using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AlisBatchReporter.Classes;
using AlisBatchReporter.Infra;
using AlisBatchReporter.Models;
using AlisBatchReporter.Models.EntityFramwork;
using AlisBatchReporter.Properties;

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
            PopulateDistList();
            ClearBoxes();
            senderUserTxtBox.Text = Settings.Default.SenderMail;
            sharingFolderTxtBox.Text = Settings.Default.SharingFolder;
        }

        private void PopulateDistList()
        {
            inDistList.DataSource = null;
            using (var db = new AlisDbContext("CompactDBContext"))
            {
                var inList = db.Distributions.Where(dist => dist.DistFlag).ToList();
                var bs = new BindingSource {DataSource = inList};
                inDistList.DisplayMember = "DisplayMember";
                inDistList.DataSource = bs;
            }           
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateForm(false)) return;
            if (string.IsNullOrEmpty(senderUserTxtBox.Text))
            {
                errorProvider1.SetError(senderUserTxtBox, "field required!");
                tabControl1.SelectedTab = tabControl1.TabPages["distPage"];
                senderUserTxtBox.Focus();
                return;
            }
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
                dbContext.SaveChanges();
            }
            Settings.Default.SenderMail = senderUserTxtBox.Text;
            Settings.Default.SharingFolder = sharingFolderTxtBox.Text;
            Settings.Default.Save();
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
                tabControl1.SelectedTab = tabControl1.TabPages["connectionPage"];
                comboBoxEnv.Focus();
            }

            if (string.IsNullOrEmpty(textBoxServerAddress.Text.Trim()))
            {
                errorProvider1.SetError(textBoxServerAddress, "field required!");
                isValid = false;
                tabControl1.SelectedTab = tabControl1.TabPages["connectionPage"];
                textBoxServerAddress.Focus();
            }

            if (string.IsNullOrEmpty(textBoxUser.Text.Trim()))
            {
                errorProvider1.SetError(textBoxUser, "field required!");
                isValid = false;
                tabControl1.SelectedTab = tabControl1.TabPages["connectionPage"];
                textBoxUser.Focus();
            }

            if (string.IsNullOrEmpty(textBoxPassword.Text.Trim()))
            {
                errorProvider1.SetError(textBoxPassword, "field required!");
                isValid = false;
                tabControl1.SelectedTab = tabControl1.TabPages["connectionPage"];
                textBoxPassword.Focus();
            }

            if (!dbValidation && comboBoxDb.SelectedItem == null)
            {
                errorProvider1.SetError(comboBoxDb, "field required!");
                isValid = false;
                tabControl1.SelectedTab = tabControl1.TabPages["connectionPage"];
                comboBoxDb.Focus();
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
            var dbListQuery = "";
            switch (((ComboboxItem) comboBoxEnv.SelectedValue).Value)
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

        private bool ValidateAddMember()
        {
            errorProvider2.Clear();
            var isValid = true;
            if (string.IsNullOrEmpty(firstNameTextBox.Text))
            {
                errorProvider2.SetError(firstNameTextBox, "field is required!");
                tabControl1.SelectedTab = tabControl1.TabPages["distPage"];
                firstNameTextBox.Focus();
                isValid = false;
            }
            if (string.IsNullOrEmpty(lastNameTextBox.Text))
            {
                errorProvider2.SetError(lastNameTextBox, "field is required!");
                tabControl1.SelectedTab = tabControl1.TabPages["distPage"];
                lastNameTextBox.Focus();
                isValid = false;
            }
            if (string.IsNullOrEmpty(emailTextBox.Text))
            {
                errorProvider2.SetError(emailTextBox, "field is required!");
                tabControl1.SelectedTab = tabControl1.TabPages["distPage"];
                emailTextBox.Focus();
                isValid = false;
            }
            return isValid;
        }


        private void inDistList_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeMemberButton.Enabled = inDistList.SelectedIndex >= 0;
        }

        private void ClearBoxes()
        {
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            emailTextBox.Text = "";
            idTextBox.Text = null;
            idTextBox.Value = -1;
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            if (!ValidateAddMember()) return;
            if (string.IsNullOrEmpty(idTextBox.Text) || idTextBox.Value < 0)
            {
                Distribution dist = new Distribution
                {
                    FirstName = firstNameTextBox.Text,
                    LastName = lastNameTextBox.Text,
                    EmailAddress = emailTextBox.Text,
                    DistFlag = true
                };
                dist.DisplayMember = dist.ToString();
                using (var db = new AlisDbContext())
                {
                    db.Distributions.Add(dist);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show(@"Email Address Already Exists, Please Add A Different One", @"Duplicate Email Error");
                    }
                }
            }
            else
            {
                using (var db = new AlisDbContext())
                {
                    var foundItemFromDb = db.Distributions.FirstOrDefault(i => i.Id.ToString().Equals(idTextBox.Text));
                    if (foundItemFromDb != null)
                    {
                        foundItemFromDb.FirstName = firstNameTextBox.Text;
                        foundItemFromDb.LastName = lastNameTextBox.Text;
                        foundItemFromDb.EmailAddress = emailTextBox.Text;
                        foundItemFromDb.DisplayMember = foundItemFromDb.ToString();
                    }
                    db.SaveChanges();
                }
            }
            PopulateDistList();
            ClearBoxes();
        }

        private void removeMemberButton_Click(object sender, EventArgs e)
        {
            using (var db = new AlisDbContext())
            {
                var foundItemFromDb = db.Distributions.FirstOrDefault(i => i.Id == ((Distribution)inDistList.SelectedItem).Id);
                if (foundItemFromDb != null)
                {
                    db.Distributions.Remove(foundItemFromDb);
                    db.SaveChanges();
                    PopulateDistList();
                    ClearBoxes();
                }                
            }
        }
    }
}