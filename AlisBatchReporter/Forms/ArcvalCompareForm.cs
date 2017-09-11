using System;
using System.Windows.Forms;
using AlisBatchReporter.Views;

namespace AlisBatchReporter.Forms
{
    public partial class ArcvalCompareForm : Form, IArcvalView
    {
        public ArcvalCompareForm()
        {
            InitializeComponent();
            BindComponents();
        }

        public event Action Compared;

        public event Action OverrideFilesChecked;

        public Panel OverridePanel => fileNamesPanel;

        public string ProcessTextBox
        {
            get => progressTextBox.Text;
            set => progressTextBox.Text = value;
        }

        public bool CopyFiles => copyCheckBox.Checked;

        public string SourceFileName
        {
            get => sourceFileNameTextBox.Text;
            set => sourceFileNameTextBox.Text = value;
        }

        public string OutboundFileName
        {
            get => outboundFileNameTextBox.Text;
            set => outboundFileNameTextBox.Text = value;
        }

        public bool ProdRadioButton => prodRadioButton.Checked;

        public bool UatRadioButton => uatRadioButton.Checked;

        public bool OverrideFiles => overrideCheckBox.Checked;

        public void LogProcess(string text, bool newLine)
        {
            ProcessTextBox += text;
            if (newLine)
                ProcessTextBox += Environment.NewLine;
        }

        private void BindComponents()
        {
            compareButton.Click += ComparedClicked;
            closeButton.Click += CloseClicked;
            overrideCheckBox.CheckedChanged += OverrideCheckedChanged;
        }

        private void OverrideCheckedChanged(object sender, EventArgs e)
        {
            OverrideFilesChecked?.Invoke();
        }

        private void CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void ComparedClicked(object sender, EventArgs e)
        {
            Compared?.Invoke();
        }
    }
}