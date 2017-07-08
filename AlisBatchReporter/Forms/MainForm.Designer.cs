namespace AlisBatchReporter.Forms
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateExportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unallocatedSuspenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eftExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dMFIOutboundValidationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.currentRunningLabel = new System.Windows.Forms.Label();
            this.findObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(667, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.validateExportsToolStripMenuItem,
            this.findObjectToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.newToolStripMenuItem.Text = "New Report";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // validateExportsToolStripMenuItem
            // 
            this.validateExportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unallocatedSuspenseToolStripMenuItem,
            this.eftExportToolStripMenuItem,
            this.dMFIOutboundValidationsToolStripMenuItem});
            this.validateExportsToolStripMenuItem.Name = "validateExportsToolStripMenuItem";
            this.validateExportsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.validateExportsToolStripMenuItem.Text = "Validate Exports";
            // 
            // unallocatedSuspenseToolStripMenuItem
            // 
            this.unallocatedSuspenseToolStripMenuItem.Name = "unallocatedSuspenseToolStripMenuItem";
            this.unallocatedSuspenseToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.unallocatedSuspenseToolStripMenuItem.Text = "Unallocated Suspense";
            this.unallocatedSuspenseToolStripMenuItem.Click += new System.EventHandler(this.unallocatedSuspenseToolStripMenuItem_Click);
            // 
            // eftExportToolStripMenuItem
            // 
            this.eftExportToolStripMenuItem.Name = "eftExportToolStripMenuItem";
            this.eftExportToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.eftExportToolStripMenuItem.Text = "Eft Export";
            this.eftExportToolStripMenuItem.Click += new System.EventHandler(this.eftExportToolStripMenuItem_Click);
            // 
            // dMFIOutboundValidationsToolStripMenuItem
            // 
            this.dMFIOutboundValidationsToolStripMenuItem.Name = "dMFIOutboundValidationsToolStripMenuItem";
            this.dMFIOutboundValidationsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.dMFIOutboundValidationsToolStripMenuItem.Text = "DMFI Outbound Validations";
            this.dMFIOutboundValidationsToolStripMenuItem.Click += new System.EventHandler(this.dMFIOutboundValidationsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.updateToolStripMenuItem.Text = "Update";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(13, 57);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Running Environment:";
            // 
            // currentRunningLabel
            // 
            this.currentRunningLabel.AutoSize = true;
            this.currentRunningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentRunningLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.currentRunningLabel.Location = new System.Drawing.Point(303, 57);
            this.currentRunningLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.currentRunningLabel.Name = "currentRunningLabel";
            this.currentRunningLabel.Size = new System.Drawing.Size(0, 24);
            this.currentRunningLabel.TabIndex = 2;
            // 
            // findObjectToolStripMenuItem
            // 
            this.findObjectToolStripMenuItem.Name = "findObjectToolStripMenuItem";
            this.findObjectToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.findObjectToolStripMenuItem.Text = "Find Object";
            this.findObjectToolStripMenuItem.Click += new System.EventHandler(this.findObjectToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 297);
            this.Controls.Add(this.currentRunningLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Lucida Bright", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "Alis Batch Reporter - Main";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label currentRunningLabel;
        private System.Windows.Forms.ToolStripMenuItem validateExportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unallocatedSuspenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eftExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dMFIOutboundValidationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findObjectToolStripMenuItem;
    }
}

