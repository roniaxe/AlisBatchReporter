using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Windows.Forms;
using AlisBatchReporter.Classes;
using AlisBatchReporter.Presentors;

namespace AlisBatchReporter.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Global.Name) || string.IsNullOrEmpty(Global.ConnString))
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
            var configForm = new ConfigForm();
            configForm.Show();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Global.Name) && !string.IsNullOrEmpty(Global.Db))
                currentRunningLabel.Text = Global.Name + @" - " + Global.Db;
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
            using (var conn =
                new SqlCeConnection("DataSource=\"alisReporter.sdf\"; Password=\"12345\";"))
            {
                conn.Open();
                var command = new SqlCeCommand(
                    @"SELECT * FROM saved_credentials WHERE CHOSE_LAST='1' AND SAVED = '1'",
                    conn);
                var da = new SqlCeDataAdapter(command);
                var dt = new DataTable();
                da.Fill(dt);
                command.ExecuteNonQuery();
                if (dt.Rows.Count <= 0) return;
                foreach (DataRow r in dt.Rows)
                    Global.PropSetter(
                        (int) r[0],
                        r[1].ToString(),
                        r[2].ToString(),
                        r[3].ToString(),
                        r[4].ToString(),
                        r[5].ToString(),
                        r[6].ToString(),
                        r[7].ToString(),
                        "1");
            }
        }

        private void unallocatedSuspenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var exportValidationForm = new ExportValidationForm();
            exportValidationForm.Show();
        }

        private void eftExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Global.Name) || string.IsNullOrEmpty(Global.ConnString))
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