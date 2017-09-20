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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.connectionPage = new System.Windows.Forms.TabPage();
            this.distPage = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.inDistList = new System.Windows.Forms.ListBox();
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.idTextBox = new System.Windows.Forms.NumericUpDown();
            this.addMemberButton = new System.Windows.Forms.Button();
            this.removeMemberButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.senderUserTxtBox = new System.Windows.Forms.TextBox();
            this.sharingFolderTxtBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBoxDbCred.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.connectionPage.SuspendLayout();
            this.distPage.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idTextBox)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelReset
            // 
            this.labelReset.AutoSize = true;
            this.labelReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReset.Location = new System.Drawing.Point(67, 48);
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
            this.groupBoxDbCred.Location = new System.Drawing.Point(22, 71);
            this.groupBoxDbCred.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxDbCred.Name = "groupBoxDbCred";
            this.groupBoxDbCred.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxDbCred.Size = new System.Drawing.Size(473, 231);
            this.groupBoxDbCred.TabIndex = 21;
            this.groupBoxDbCred.TabStop = false;
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Location = new System.Drawing.Point(199, 60);
            this.textBoxServerAddress.Margin = new System.Windows.Forms.Padding(4);
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
            this.buttonGetDb.Margin = new System.Windows.Forms.Padding(4);
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
            this.checkBoxSave.Margin = new System.Windows.Forms.Padding(4);
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
            this.comboBoxEnv.Margin = new System.Windows.Forms.Padding(4);
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
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(169, 22);
            this.textBoxPassword.TabIndex = 4;
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(199, 98);
            this.textBoxUser.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(169, 22);
            this.textBoxUser.TabIndex = 3;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(845, 579);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(163, 579);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4);
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
            this.labelDb.Location = new System.Drawing.Point(49, 351);
            this.labelDb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDb.Name = "labelDb";
            this.labelDb.Size = new System.Drawing.Size(104, 25);
            this.labelDb.TabIndex = 20;
            this.labelDb.Text = "Database";
            // 
            // comboBoxDb
            // 
            this.comboBoxDb.FormattingEnabled = true;
            this.comboBoxDb.Location = new System.Drawing.Point(198, 349);
            this.comboBoxDb.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDb.Name = "comboBoxDb";
            this.comboBoxDb.Size = new System.Drawing.Size(192, 24);
            this.comboBoxDb.TabIndex = 7;
            this.comboBoxDb.SelectedIndexChanged += new System.EventHandler(this.comboBoxDb_SelectedIndexChanged);
            // 
            // refreshButton
            // 
            this.refreshButton.Image = global::AlisBatchReporter.Properties.Resources.Button_Refresh_icon;
            this.refreshButton.Location = new System.Drawing.Point(22, 38);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(4);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(37, 32);
            this.refreshButton.TabIndex = 18;
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.connectionPage);
            this.tabControl1.Controls.Add(this.distPage);
            this.tabControl1.Location = new System.Drawing.Point(159, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(790, 524);
            this.tabControl1.TabIndex = 23;
            // 
            // connectionPage
            // 
            this.connectionPage.Controls.Add(this.refreshButton);
            this.connectionPage.Controls.Add(this.comboBoxDb);
            this.connectionPage.Controls.Add(this.labelReset);
            this.connectionPage.Controls.Add(this.labelDb);
            this.connectionPage.Controls.Add(this.groupBoxDbCred);
            this.connectionPage.Location = new System.Drawing.Point(4, 25);
            this.connectionPage.Name = "connectionPage";
            this.connectionPage.Padding = new System.Windows.Forms.Padding(3);
            this.connectionPage.Size = new System.Drawing.Size(782, 495);
            this.connectionPage.TabIndex = 0;
            this.connectionPage.Text = "Connection";
            this.connectionPage.UseVisualStyleBackColor = true;
            // 
            // distPage
            // 
            this.distPage.Controls.Add(this.sharingFolderTxtBox);
            this.distPage.Controls.Add(this.label8);
            this.distPage.Controls.Add(this.panel2);
            this.distPage.Controls.Add(this.removeMemberButton);
            this.distPage.Controls.Add(this.panel1);
            this.distPage.Controls.Add(this.addMemberButton);
            this.distPage.Controls.Add(this.inDistList);
            this.distPage.Location = new System.Drawing.Point(4, 25);
            this.distPage.Name = "distPage";
            this.distPage.Padding = new System.Windows.Forms.Padding(3);
            this.distPage.Size = new System.Drawing.Size(782, 495);
            this.distPage.TabIndex = 1;
            this.distPage.Text = "Mail Distribution";
            this.distPage.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.idTextBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.emailTextBox);
            this.panel1.Controls.Add(this.lastNameTextBox);
            this.panel1.Controls.Add(this.firstNameTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(26, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 183);
            this.panel1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Email:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "First Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Last Name:";
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(87, 133);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(209, 22);
            this.emailTextBox.TabIndex = 7;
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Location = new System.Drawing.Point(87, 88);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(142, 22);
            this.lastNameTextBox.TabIndex = 6;
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Location = new System.Drawing.Point(87, 42);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(142, 22);
            this.firstNameTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add To Distribution List";
            // 
            // inDistList
            // 
            this.inDistList.FormattingEnabled = true;
            this.inDistList.ItemHeight = 16;
            this.inDistList.Location = new System.Drawing.Point(431, 19);
            this.inDistList.Name = "inDistList";
            this.inDistList.Size = new System.Drawing.Size(305, 452);
            this.inDistList.Sorted = true;
            this.inDistList.TabIndex = 0;
            this.inDistList.SelectedIndexChanged += new System.EventHandler(this.inDistList_SelectedIndexChanged);
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // idTextBox
            // 
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(243, 42);
            this.idTextBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(92, 22);
            this.idTextBox.TabIndex = 15;
            // 
            // addMemberButton
            // 
            this.addMemberButton.Location = new System.Drawing.Point(26, 229);
            this.addMemberButton.Name = "addMemberButton";
            this.addMemberButton.Size = new System.Drawing.Size(75, 37);
            this.addMemberButton.TabIndex = 5;
            this.addMemberButton.Text = "Add";
            this.addMemberButton.UseVisualStyleBackColor = true;
            this.addMemberButton.Click += new System.EventHandler(this.addMemberButton_Click);
            // 
            // removeMemberButton
            // 
            this.removeMemberButton.Enabled = false;
            this.removeMemberButton.Location = new System.Drawing.Point(314, 229);
            this.removeMemberButton.Name = "removeMemberButton";
            this.removeMemberButton.Size = new System.Drawing.Size(75, 37);
            this.removeMemberButton.TabIndex = 16;
            this.removeMemberButton.Text = "Remove";
            this.removeMemberButton.UseVisualStyleBackColor = true;
            this.removeMemberButton.Click += new System.EventHandler(this.removeMemberButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.senderUserTxtBox);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(26, 290);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(363, 82);
            this.panel2.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Sender Mail Credentials:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 17);
            this.label6.TabIndex = 19;
            this.label6.Text = "Email Address:";
            // 
            // senderUserTxtBox
            // 
            this.senderUserTxtBox.Location = new System.Drawing.Point(123, 43);
            this.senderUserTxtBox.Name = "senderUserTxtBox";
            this.senderUserTxtBox.Size = new System.Drawing.Size(212, 22);
            this.senderUserTxtBox.TabIndex = 21;
            // 
            // sharingFolderTxtBox
            // 
            this.sharingFolderTxtBox.Location = new System.Drawing.Point(149, 395);
            this.sharingFolderTxtBox.Name = "sharingFolderTxtBox";
            this.sharingFolderTxtBox.Size = new System.Drawing.Size(240, 22);
            this.sharingFolderTxtBox.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 395);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 17);
            this.label8.TabIndex = 23;
            this.label8.Text = "Sharing Folder:";
            // 
            // ConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cancelButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ConfigView";
            this.Size = new System.Drawing.Size(1204, 688);
            this.Load += new System.EventHandler(this.ConfigView_Load);
            this.groupBoxDbCred.ResumeLayout(false);
            this.groupBoxDbCred.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.connectionPage.ResumeLayout(false);
            this.connectionPage.PerformLayout();
            this.distPage.ResumeLayout(false);
            this.distPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idTextBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage connectionPage;
        private System.Windows.Forms.TabPage distPage;
        private System.Windows.Forms.ListBox inDistList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.NumericUpDown idTextBox;
        private System.Windows.Forms.Button addMemberButton;
        private System.Windows.Forms.Button removeMemberButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox senderUserTxtBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox sharingFolderTxtBox;
        private System.Windows.Forms.Label label8;
    }
}
