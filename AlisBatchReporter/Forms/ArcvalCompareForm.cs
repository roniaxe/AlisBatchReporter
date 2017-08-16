using System;
using System.Windows.Forms;
using AlisBatchReporter.Views;

namespace AlisBatchReporter.Forms
{
    public partial class ArcvalCompareForm : Form, IArcvalView
    {
        public event Action Compared;
        public string ProcessTextBox => progressTextBox.Text;
        public bool CopyFiles => copyCheckBox.Checked;
        public ArcvalCompareForm()
        {
            InitializeComponent();
            BindComponents();
        }

        private void BindComponents()
        {
            compareButton.Click += ComparedClicked;
            closeButton.Click += CloseClicked;
        }

        private void CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void ComparedClicked(object sender, EventArgs e)
        {
            Compared?.Invoke();
        }
        
        public void LogProcess(string text, bool newLine)
        {
            progressTextBox.AppendText(text);
            if (newLine)
            {
                progressTextBox.AppendText(Environment.NewLine);
            }
        }
    }
}
