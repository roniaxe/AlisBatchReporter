using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AlisBatchReporter.Views.MVPTest
{
    public partial class ConfForm : Form, IConfigView
    {
        public event Action EnvSelected;
        public event Action DbSelected;
        public event Action Saved;
        public IList<Env> Envs => comboBoxEnv.DataSource as IList<Env>;
        public Env SelectedEnv => comboBoxEnv.SelectedItem as Env;
        public string Host => SelectedEnv.HostAddress;
        public string Password => textBoxPassword.Text;
        public string UserName => textBoxUser.Text;
        public bool SaveForLater => checkBoxSave.Checked;

        public ConfForm()
        {
            InitializeComponent();
            BindComponents();
        }

        private void BindComponents()
        {
            comboBoxEnv.SelectedIndexChanged += OnSelectedIndexChanged;
            saveButton.Click += OnSaveButtonClick;
            cancelButton.Click += OnCancelButtonClick;
            comboBoxEnv.DisplayMember = "Name";

        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            Saved?.Invoke();
            Close();
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            EnvSelected?.Invoke();
        }

        public void LoadEnvs(IList<Env> envList)
        {
            comboBoxEnv.DataSource = envList;
        }

        public void LoadEnv(Env selectedEnv)
        {
            textBoxServerAddress.Text = selectedEnv.HostAddress;
        }
    }
}
