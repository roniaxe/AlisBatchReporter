using System.Data.Entity;
using System.Windows.Forms;
using AlisBatchReporter.Views;

namespace AlisBatchReporter.Forms
{
    public partial class ConfigForm : Form
    {

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, System.EventArgs e)
        {
            var configViewControl = new ConfigView();
            panel1.Controls.Add(configViewControl);
        }
    }
}