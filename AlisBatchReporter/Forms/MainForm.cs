using System;
using System.Windows.Forms;
using AlisBatchReporter.Classes;
using AlisBatchReporter.Presentors;
using AlisBatchReporter.Views.MVPTest;

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
            if (string.IsNullOrEmpty(Global.Env) || string.IsNullOrEmpty(Global.ChosenConnection))
            {
                MessageBox.Show(@"Please Choose Environment In Settings", @"No Database Selected");
            }

            else
            {
                DataForm dataForm = new DataForm();
                dataForm.Show();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm();
            configForm.Show();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Global.Env) && !string.IsNullOrEmpty(Global.Db))
            {
                currentRunningLabel.Text = Global.Env + @" - " + Global.Db;
            }
            else
            {
                currentRunningLabel.Text = "";
            }
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
            Global.Env = !string.IsNullOrEmpty(Properties.Settings.Default.LastSaveEnv)
                ? Properties.Settings.Default.LastSaveEnv
                : "";
            Global.Db = !string.IsNullOrEmpty(Properties.Settings.Default.LastSaveDb)
                ? Properties.Settings.Default.LastSaveDb
                : "";
            Global.ChosenConnection = !string.IsNullOrEmpty(Properties.Settings.Default.LastSaveConnStr)
                ? Properties.Settings.Default.LastSaveConnStr
                : "";
            Global.SavedCheckBox = Properties.Settings.Default.SaveCredentialsSelected;
        }

        private void unallocatedSuspenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportValidationForm exportValidationForm = new ExportValidationForm();
            exportValidationForm.Show();
        }

        private void eftExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Global.Env) || string.IsNullOrEmpty(Global.ChosenConnection))
            {
                MessageBox.Show(@"Please Choose Environment In Settings", @"No Database Selected");
            }
            else
            {
                EftExportValidateForm eftExportValidateForm = new EftExportValidateForm();
                eftExportValidateForm.Show();
            }            
        }

        private void dMFIOutboundValidationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DmfiValidationsForm dmfiValidationsForm = new DmfiValidationsForm();
            dmfiValidationsForm.Show();
        }

        private void findObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindObjectForm findObjectForm = new FindObjectForm();
            findObjectForm.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConfForm confForm = new ConfForm();
            EnvRepository repository = new EnvRepository();
            ConfigPresenter unused = new ConfigPresenter(confForm, repository);
            confForm.Show();
        }

        private void lexisNexisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LexisNexisForm lexisNexisForm = new LexisNexisForm();
            LexisNexisPresentor lexisNexisPresentor = new LexisNexisPresentor(lexisNexisForm);

            lexisNexisForm.Show();
        }

        private void arcvalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArcvalCompareForm arcvalCompareForm = new ArcvalCompareForm();
            ArcvalPresentor arcvalPresentor = new ArcvalPresentor(arcvalCompareForm);

            arcvalCompareForm.Show();
        }
    }
}