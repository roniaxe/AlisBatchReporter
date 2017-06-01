namespace AlisBatchReporter.Forms
{
    partial class ConfigForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.comboBoxEnv = new System.Windows.Forms.ComboBox();
            this.comboBoxDb = new System.Windows.Forms.ComboBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelEnv = new System.Windows.Forms.Label();
            this.labelDb = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.groupBoxDbCred = new System.Windows.Forms.GroupBox();
            this.buttonGetDb = new System.Windows.Forms.Button();
            this.checkBoxSave = new System.Windows.Forms.CheckBox();
            this.groupBoxDbCred.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(150, 349);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(255, 349);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Image = global::AlisBatchReporter.Properties.Resources.Button_Refresh_icon;
            this.refreshButton.Location = new System.Drawing.Point(503, 12);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(39, 35);
            this.refreshButton.TabIndex = 5;
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // comboBoxEnv
            // 
            this.comboBoxEnv.FormattingEnabled = true;
            this.comboBoxEnv.Location = new System.Drawing.Point(149, 15);
            this.comboBoxEnv.Name = "comboBoxEnv";
            this.comboBoxEnv.Size = new System.Drawing.Size(128, 21);
            this.comboBoxEnv.TabIndex = 6;
            this.comboBoxEnv.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnv_SelectedIndexChanged);
            // 
            // comboBoxDb
            // 
            this.comboBoxDb.FormattingEnabled = true;
            this.comboBoxDb.Location = new System.Drawing.Point(282, 279);
            this.comboBoxDb.Name = "comboBoxDb";
            this.comboBoxDb.Size = new System.Drawing.Size(145, 21);
            this.comboBoxDb.TabIndex = 7;
            this.comboBoxDb.SelectedIndexChanged += new System.EventHandler(this.comboBoxDb_SelectedIndexChanged);
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(149, 51);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(128, 20);
            this.textBoxUser.TabIndex = 8;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(149, 84);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(128, 20);
            this.textBoxPassword.TabIndex = 9;
            // 
            // labelEnv
            // 
            this.labelEnv.AutoSize = true;
            this.labelEnv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEnv.Location = new System.Drawing.Point(20, 16);
            this.labelEnv.Name = "labelEnv";
            this.labelEnv.Size = new System.Drawing.Size(102, 20);
            this.labelEnv.TabIndex = 10;
            this.labelEnv.Text = "Environment";
            // 
            // labelDb
            // 
            this.labelDb.AutoSize = true;
            this.labelDb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDb.Location = new System.Drawing.Point(170, 280);
            this.labelDb.Name = "labelDb";
            this.labelDb.Size = new System.Drawing.Size(81, 20);
            this.labelDb.TabIndex = 11;
            this.labelDb.Text = "Database";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.Location = new System.Drawing.Point(20, 51);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(86, 20);
            this.labelUsername.TabIndex = 12;
            this.labelUsername.Text = "Username";
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPass.Location = new System.Drawing.Point(20, 84);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(83, 20);
            this.labelPass.TabIndex = 13;
            this.labelPass.Text = "Password";
            // 
            // groupBoxDbCred
            // 
            this.groupBoxDbCred.Controls.Add(this.buttonGetDb);
            this.groupBoxDbCred.Controls.Add(this.checkBoxSave);
            this.groupBoxDbCred.Controls.Add(this.labelEnv);
            this.groupBoxDbCred.Controls.Add(this.labelPass);
            this.groupBoxDbCred.Controls.Add(this.comboBoxEnv);
            this.groupBoxDbCred.Controls.Add(this.labelUsername);
            this.groupBoxDbCred.Controls.Add(this.textBoxPassword);
            this.groupBoxDbCred.Controls.Add(this.textBoxUser);
            this.groupBoxDbCred.Location = new System.Drawing.Point(150, 98);
            this.groupBoxDbCred.Name = "groupBoxDbCred";
            this.groupBoxDbCred.Size = new System.Drawing.Size(355, 143);
            this.groupBoxDbCred.TabIndex = 14;
            this.groupBoxDbCred.TabStop = false;
            // 
            // buttonGetDb
            // 
            this.buttonGetDb.Location = new System.Drawing.Point(202, 116);
            this.buttonGetDb.Name = "buttonGetDb";
            this.buttonGetDb.Size = new System.Drawing.Size(75, 23);
            this.buttonGetDb.TabIndex = 15;
            this.buttonGetDb.Text = "Retrieve DB";
            this.buttonGetDb.UseVisualStyleBackColor = true;
            this.buttonGetDb.Click += new System.EventHandler(this.buttonGetDb_Click);
            // 
            // checkBoxSave
            // 
            this.checkBoxSave.AutoSize = true;
            this.checkBoxSave.Location = new System.Drawing.Point(24, 120);
            this.checkBoxSave.Name = "checkBoxSave";
            this.checkBoxSave.Size = new System.Drawing.Size(51, 17);
            this.checkBoxSave.TabIndex = 15;
            this.checkBoxSave.Text = "Save";
            this.checkBoxSave.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 475);
            this.Controls.Add(this.groupBoxDbCred);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.labelDb);
            this.Controls.Add(this.comboBoxDb);
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.groupBoxDbCred.ResumeLayout(false);
            this.groupBoxDbCred.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ComboBox comboBoxEnv;
        private System.Windows.Forms.ComboBox comboBoxDb;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelEnv;
        private System.Windows.Forms.Label labelDb;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.GroupBox groupBoxDbCred;
        private System.Windows.Forms.CheckBox checkBoxSave;
        private System.Windows.Forms.Button buttonGetDb;
    }
}