using System.Data.Entity;
using System.Windows.Forms;
using AlisBatchReporter.Views;

namespace AlisBatchReporter.Forms
{
    public partial class ConfigForm : Form
    {
        private readonly DbContext _db;

        public ConfigForm(DbContext dbContext)
        {
            _db = dbContext;
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, System.EventArgs e)
        {
            var configViewControl = new ConfigView(_db);
            panel1.Controls.Add(configViewControl);
        }
    }
}