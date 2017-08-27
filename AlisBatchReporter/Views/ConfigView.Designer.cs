namespace AlisBatchReporter.Views
{
    partial class ConfigView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelReset = new System.Windows.Forms.Label();
            this.groupBoxDbCred = new System.Windows.Forms.GroupBox();
            this.textBoxServerAddress = new System.Windows.Forms.TextBox();
            this.labelEvnAddress = new System.Windows.Forms.Label();
            this.buttonGetDb = new System.Windows.Forms.Button();
            this.checkBoxSave = new System.Windows.Forms.CheckBox();
            this.labelEnv = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.comboBoxEnv = new System.Windows.Forms.ComboBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.labelDb = new System.Windows.Forms.Label();
            this.comboBoxDb = new System.Windows.Forms.ComboBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBoxDbCred.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelReset
            // 
            this.labelReset.AutoSize = true;
            this.labelReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReset.Location = new System.Drawing.Point(108, 58);
            this.labelReset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReset.Name = "labelReset";
            this.labelReset.Size = new System.Drawing.Size(206, 22);
            this.labelReset.TabIndex = 22;
            this.labelReset.Text = "Reset Saved Passwords";
            // 
            // groupBoxDbCred
            // 
            this.groupBoxDbCred.Controls.Add(this.textBoxServerAddress);
            this.groupBoxDbCred.Controls.Add(this.labelEvnAddress);
            this.groupBoxDbCred.Controls.Add(this.buttonGetDb);
            this.groupBoxDbCred.Controls.Add(this.checkBoxSave);
            this.groupBoxDbCred.Controls.Add(this.labelEnv);
            this.groupBoxDbCred.Controls.Add(this.labelPass);
            this.groupBoxDbCred.Controls.Add(this.comboBoxEnv);
            this.groupBoxDbCred.Controls.Add(this.labelUsername);
            this.groupBoxDbCred.Controls.Add(this.textBoxPassword);
            this.groupBoxDbCred.Controls.Add(this.textBoxUser);
            this.groupBoxDbCred.Location = new System.Drawing.Point(48, 86);
            this.groupBoxDbCred.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxDbCred.Name = "groupBoxDbCred";
            this.groupBoxDbCred.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxDbCred.Size = new System.Drawing.Size(473, 231);
            this.groupBoxDbCred.TabIndex = 21;
            this.groupBoxDbCred.TabStop = false;
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Location = new System.Drawing.Point(199, 60);
            this.textBoxServerAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.Size = new System.Drawing.Size(169, 22);
            this.textBoxServerAddress.TabIndex = 2;
            // 
            // labelEvnAddress
            // 
            this.labelEvnAddress.AutoSize = true;
            this.labelEvnAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEvnAddress.Location = new System.Drawing.Point(27, 58);
            this.labelEvnAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEvnAddress.Name = "labelEvnAddress";
            this.labelEvnAddress.Size = new System.Drawing.Size(160, 25);
            this.labelEvnAddress.TabIndex = 17;
            this.labelEvnAddress.Text = "Server Address";
            // 
            // buttonGetDb
            // 
            this.buttonGetDb.Location = new System.Drawing.Point(269, 178);
            this.buttonGetDb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGetDb.Name = "buttonGetDb";
            this.buttonGetDb.Size = new System.Drawing.Size(100, 28);
            this.buttonGetDb.TabIndex = 6;
            this.buttonGetDb.Text = "Retrieve DB";
            this.buttonGetDb.UseVisualStyleBackColor = true;
            this.buttonGetDb.Click += new System.EventHandler(this.buttonGetDb_Click);
            // 
            // checkBoxSave
            // 
            this.checkBoxSave.AutoSize = true;
            this.checkBoxSave.Location = new System.Drawing.Point(32, 183);
            this.checkBoxSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxSave.Name = "checkBoxSave";
            this.checkBoxSave.Size = new System.Drawing.Size(62, 21);
            this.checkBoxSave.TabIndex = 5;
            this.checkBoxSave.Text = "Save";
            this.checkBoxSave.UseVisualStyleBackColor = true;
            // 
            // labelEnv
            // 
            this.labelEnv.AutoSize = true;
            this.labelEnv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEnv.Location = new System.Drawing.Point(27, 20);
            this.labelEnv.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEnv.Name = "labelEnv";
            this.labelEnv.Size = new System.Drawing.Size(132, 25);
            this.labelEnv.TabIndex = 10;
            this.labelEnv.Text = "Environment";
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPass.Location = new System.Drawing.Point(27, 139);
            this.labelPass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(106, 25);
            this.labelPass.TabIndex = 13;
            this.labelPass.Text = "Password";
            // 
            // comboBoxEnv
            // 
            this.comboBoxEnv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEnv.FormattingEnabled = true;
            this.comboBoxEnv.Location = new System.Drawing.Point(199, 18);
            this.comboBoxEnv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxEnv.Name = "comboBoxEnv";
            this.comboBoxEnv.Size = new System.Drawing.Size(169, 24);
            this.comboBoxEnv.TabIndex = 1;
            this.comboBoxEnv.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnv_SelectedIndexChanged);
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.Location = new System.Drawing.Point(27, 98);
            this.labelUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(110, 25);
            this.labelUsername.TabIndex = 12;
            this.labelUsername.Text = "Username";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(199, 139);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(169, 22);
            this.textBoxPassword.TabIndex = 4;
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(199, 98);
            this.textBoxUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(169, 22);
            this.textBoxUser.TabIndex = 3;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(421, 450);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(48, 450);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 28);
            this.saveButton.TabIndex = 16;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // labelDb
            // 
            this.labelDb.AutoSize = true;
            this.labelDb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDb.Location = new System.Drawing.Point(75, 366);
            this.labelDb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDb.Name = "labelDb";
            this.labelDb.Size = new System.Drawing.Size(104, 25);
            this.labelDb.TabIndex = 20;
            this.labelDb.Text = "Database";
            // 
            // comboBoxDb
            // 
            this.comboBoxDb.FormattingEnabled = true;
            this.comboBoxDb.Location = new System.Drawing.Point(224, 364);
            this.comboBoxDb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxDb.Name = "comboBoxDb";
            this.comboBoxDb.Size = new System.Drawing.Size(192, 24);
            this.comboBoxDb.TabIndex = 7;
            this.comboBoxDb.SelectedIndexChanged += new System.EventHandler(this.comboBoxDb_SelectedIndexChanged);
            // 
            // refreshButton
            // 
            this.refreshButton.Image = global::AlisBatchReporter.Properties.Resources.Button_Refresh_icon;
            this.refreshButton.Location = new System.Drawing.Point(48, 36);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(52, 43);
            this.refreshButton.TabIndex = 18;
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelReset);
            this.Controls.Add(this.groupBoxDbCred);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.labelDb);
            this.Controls.Add(this.comboBoxDb);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ConfigView";
            this.Size = new System.Drawing.Size(613, 556);
            this.Load += new System.EventHandler(this.ConfigView_Load);
            this.groupBoxDbCred.ResumeLayout(false);
            this.groupBoxDbCred.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelReset;
        private System.Windows.Forms.GroupBox groupBoxDbCred;
        private System.Windows.Forms.TextBox textBoxServerAddress;
        private System.Windows.Forms.Label labelEvnAddress;
        private System.Windows.Forms.Button buttonGetDb;
        private System.Windows.Forms.CheckBox checkBoxSave;
        private System.Windows.Forms.Label labelEnv;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.ComboBox comboBoxEnv;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label labelDb;
        private System.Windows.Forms.ComboBox comboBoxDb;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
