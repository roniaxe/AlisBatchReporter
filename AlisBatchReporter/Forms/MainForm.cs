using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlServerCe;
using System.Linq;
using System.Windows.Forms;
using AlisBatchReporter.Classes;
using AlisBatchReporter.Models;
using AlisBatchReporter.Presentors;

namespace AlisBatchReporter.Forms
{
    public partial class MainForm : Form
    {
        private readonly AlisDbContext _db;
        public MainForm(DbContext dbContext)
        {
            _db = (AlisDbContext) dbContext;
            _db.Database.Initialize(true);
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Global.SavedCredentials.Name) || string.IsNullOrEmpty(Global.SavedCredentials.ConnString))
            {
                MessageBox.Show(@"Please Choose Environment In Settings", @"No Database Selected");
            }

            else
            {
                var dataForm = new DataForm();
                dataForm.Show();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var configForm = new ConfigForm(_db);
            configForm.Show();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (Global.SavedCredentials != null)
                currentRunningLabel.Text = Global.SavedCredentials.Name + @" - " + Global.SavedCredentials.Db;
            else
                currentRunningLabel.Text = "";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($@"Version: {Global.Version}" + "\n" +
                            @"For Issues/Requests - Contact Roni Axelrad - roni.axelrad@sapiens.com");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //using (var conn =
            //    new SqlCeConnection("DataSource=\"alisReporter.sdf\"; Password=\"12345\";"))
            //{
            //    using (var db = new alisReporterContext(conn))
            //    {
            //        var res = db.Saved_credentials
            //            .Where(i => i.CHOSE_LAST.Equals("1") && i.SAVED.Equals("1")).ToList();
            //        Global.PropSetter(
            //            res[0].ID,
            //            res[0].USERNAME, 
            //            res[0].PASSWORD, 
            //            res[0].HOST,
            //            res[0].DB, 
            //            res[0].NAME, 
            //            res[0].CONN_STRING, 
            //            res[0].CHOSE_LAST,
            //            res[0].SAVED);
            //    }
            //}
            SavedCredentials restoreFromDb = _db.SavedCredentialses.FirstOrDefault(i => i.ChoseLast && i.Saved);
            Global.PropSetter(restoreFromDb);
        }

        private void unallocatedSuspenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var exportValidationForm = new ExportValidationForm();
            exportValidationForm.Show();
        }

        private void eftExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Global.SavedCredentials.Name) || string.IsNullOrEmpty(Global.SavedCredentials.ConnString))
            {
                MessageBox.Show(@"Please Choose Environment In Settings", @"No Database Selected");
            }
            else
            {
                var eftExportValidateForm = new EftExportValidateForm();
                eftExportValidateForm.Show();
            }
        }

        private void dMFIOutboundValidationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dmfiValidationsForm = new DmfiValidationsForm();
            dmfiValidationsForm.Show();
        }

        private void findObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var findObjectForm = new FindObjectForm();
            findObjectForm.Show();
        }

        private void lexisNexisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lexisNexisForm = new LexisNexisForm();
            var lexisNexisPresentor = new LexisNexisPresentor(lexisNexisForm);

            lexisNexisForm.Show();
        }

        private void arcvalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var arcvalCompareForm = new ArcvalCompareForm();
            var arcvalPresentor = new ArcvalPresentor(arcvalCompareForm);

            arcvalCompareForm.Show();
        }
    }
}