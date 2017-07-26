﻿namespace AlisBatchReporter.Views.MVPTest
{
    partial class ConfForm
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
            this.refreshButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.labelDb = new System.Windows.Forms.Label();
            this.comboBoxDb = new System.Windows.Forms.ComboBox();
            this.groupBoxDbCred.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelReset
            // 
            this.labelReset.AutoSize = true;
            this.labelReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReset.Location = new System.Drawing.Point(111, 68);
            this.labelReset.Name = "labelReset";
            this.labelReset.Size = new System.Drawing.Size(161, 17);
            this.labelReset.TabIndex = 29;
            this.labelReset.Text = "Reset Saved Passwords";
            // 
            // groupBoxDbCred
            // 
            this.groupBoxDbCred.Controls.Add(this.textBoxServerAddress);
            this.groupBoxDbCred.Controls.Add(this.labelEvnAddress);
            this.groupBoxDbCred.Controls.Add(this.buttonGetDb);
            this.groupBoxDbCred.Controls.Add(this.checkBoxSave);
            this.groupBoxDbCred.Controls.Add(this.labelEnv);
            this.groupBoxDbCred.Controls.Add(this.comboBoxDb);
            this.groupBoxDbCred.Controls.Add(this.labelDb);
            this.groupBoxDbCred.Controls.Add(this.labelPass);
            this.groupBoxDbCred.Controls.Add(this.comboBoxEnv);
            this.groupBoxDbCred.Controls.Add(this.labelUsername);
            this.groupBoxDbCred.Controls.Add(this.textBoxPassword);
            this.groupBoxDbCred.Controls.Add(this.textBoxUser);
            this.groupBoxDbCred.Location = new System.Drawing.Point(65, 88);
            this.groupBoxDbCred.Name = "groupBoxDbCred";
            this.groupBoxDbCred.Size = new System.Drawing.Size(355, 219);
            this.groupBoxDbCred.TabIndex = 28;
            this.groupBoxDbCred.TabStop = false;
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Location = new System.Drawing.Point(149, 49);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.Size = new System.Drawing.Size(128, 20);
            this.textBoxServerAddress.TabIndex = 18;
            // 
            // labelEvnAddress
            // 
            this.labelEvnAddress.AutoSize = true;
            this.labelEvnAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEvnAddress.Location = new System.Drawing.Point(20, 47);
            this.labelEvnAddress.Name = "labelEvnAddress";
            this.labelEvnAddress.Size = new System.Drawing.Size(125, 20);
            this.labelEvnAddress.TabIndex = 17;
            this.labelEvnAddress.Text = "Server Address";
            // 
            // buttonGetDb
            // 
            this.buttonGetDb.Location = new System.Drawing.Point(202, 145);
            this.buttonGetDb.Name = "buttonGetDb";
            this.buttonGetDb.Size = new System.Drawing.Size(75, 23);
            this.buttonGetDb.TabIndex = 15;
            this.buttonGetDb.Text = "Retrieve DB";
            this.buttonGetDb.UseVisualStyleBackColor = true;
            // 
            // checkBoxSave
            // 
            this.checkBoxSave.AutoSize = true;
            this.checkBoxSave.Location = new System.Drawing.Point(24, 149);
            this.checkBoxSave.Name = "checkBoxSave";
            this.checkBoxSave.Size = new System.Drawing.Size(51, 17);
            this.checkBoxSave.TabIndex = 15;
            this.checkBoxSave.Text = "Save";
            this.checkBoxSave.UseVisualStyleBackColor = true;
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
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPass.Location = new System.Drawing.Point(20, 113);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(83, 20);
            this.labelPass.TabIndex = 13;
            this.labelPass.Text = "Password";
            // 
            // comboBoxEnv
            // 
            this.comboBoxEnv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEnv.FormattingEnabled = true;
            this.comboBoxEnv.Location = new System.Drawing.Point(149, 15);
            this.comboBoxEnv.Name = "comboBoxEnv";
            this.comboBoxEnv.Size = new System.Drawing.Size(128, 21);
            this.comboBoxEnv.TabIndex = 6;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.Location = new System.Drawing.Point(20, 80);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(86, 20);
            this.labelUsername.TabIndex = 12;
            this.labelUsername.Text = "Username";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(149, 113);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(128, 20);
            this.textBoxPassword.TabIndex = 9;
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(149, 80);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(128, 20);
            this.textBoxUser.TabIndex = 8;
            // 
            // refreshButton
            // 
            this.refreshButton.Image = global::AlisBatchReporter.Properties.Resources.Button_Refresh_icon;
            this.refreshButton.Location = new System.Drawing.Point(66, 50);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(39, 35);
            this.refreshButton.TabIndex = 25;
            this.refreshButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(345, 336);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 24;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(65, 336);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 23;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // labelDb
            // 
            this.labelDb.AutoSize = true;
            this.labelDb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDb.Location = new System.Drawing.Point(20, 186);
            this.labelDb.Name = "labelDb";
            this.labelDb.Size = new System.Drawing.Size(81, 20);
            this.labelDb.TabIndex = 27;
            this.labelDb.Text = "Database";
            // 
            // comboBoxDb
            // 
            this.comboBoxDb.FormattingEnabled = true;
            this.comboBoxDb.Location = new System.Drawing.Point(149, 192);
            this.comboBoxDb.Name = "comboBoxDb";
            this.comboBoxDb.Size = new System.Drawing.Size(128, 21);
            this.comboBoxDb.TabIndex = 26;
            // 
            // ConfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 488);
            this.Controls.Add(this.labelReset);
            this.Controls.Add(this.groupBoxDbCred);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Name = "ConfForm";
            this.Text = "ConfForm";
            this.groupBoxDbCred.ResumeLayout(false);
            this.groupBoxDbCred.PerformLayout();
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
    }
}