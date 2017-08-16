using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Forms
{
    public partial class SingleBatchRunForm : Form
    {
        private readonly QueryRepo _gbaQuery;

        public SingleBatchRunForm(QueryRepo gbaQuery)
        {           
            InitializeComponent();
            _gbaQuery = gbaQuery;
            dataGridView1.Hide();
        }

        private async void SingleBatchRunForm_Load(object sender, System.EventArgs e)
        {
            // Show progress and populate data
            await RunQuery();                     
        }

        private async Task RunQuery()
        {
            dataGridView1.DataSource = bindingSource1;
            progressBar1.Visible = true;
            DataTable result = await QueryManager.Query(_gbaQuery.QueryDynamication());
            bindingSource1.DataSource = result;
            progressBar1.Visible = false;
            dataGridView1.Show();
        }    
    }
}
