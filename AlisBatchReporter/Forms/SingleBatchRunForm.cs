using System.Data;
using System.Windows.Forms;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Forms
{
    public partial class SingleBatchRunForm : Form
    {
        private readonly SimpleQuery _gbaQuery;

        public SingleBatchRunForm(SimpleQuery gbaQuery)
        {           
            InitializeComponent();
            _gbaQuery = gbaQuery;
            dataGridView1.Hide();
        }

        private void SingleBatchRunForm_Load(object sender, System.EventArgs e)
        {
            // Show progress and populate data
            dataGridView1.DataSource = bindingSource1;
            progressBar1.Visible = true;
            backgroundWorker1.RunWorkerAsync(_gbaQuery);
            dataGridView1.Show();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            SimpleQuery report = e.Argument as SimpleQuery;
            e.Result = report?.DoQuery();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            DataTable workerResult = (DataTable)e.Result;
            bindingSource1.DataSource = workerResult;
            progressBar1.Visible = false;
        }
    }
}
