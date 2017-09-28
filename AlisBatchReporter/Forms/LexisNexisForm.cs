using System;
using System.Windows.Forms;
using AlisBatchReporter.Views;

namespace AlisBatchReporter.Forms
{
    public partial class LexisNexisForm : Form, ILexisNexisView
    {
        public event Action NexisLexisValidated;
        public string ProcessText => processTextBox.Text;


        public LexisNexisForm()
        {
            InitializeComponent();
            BindComponents();
        }

        private void BindComponents()
        {
            validateButton.Click += OnValidateClick;
            closeButton.Click += OnCloseNexisLexis;
        }

        private void OnCloseNexisLexis(object sender, EventArgs e)
        {
            Close();
        }

        private void OnValidateClick(object sender, EventArgs e)
        {
            NexisLexisValidated?.Invoke();
        }

        public void LogProcess(string logData, bool newLine)
        {
            processTextBox.AppendText(logData + (newLine ? Environment.NewLine : ""));
        }
    }
}