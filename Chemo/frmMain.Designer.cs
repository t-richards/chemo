namespace Chemo
{
    partial class frmMain
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
            System.Windows.Forms.TreeNode treeNode61 = new System.Windows.Forms.TreeNode("Remove Windows Store Apps");
            System.Windows.Forms.TreeNode treeNode62 = new System.Windows.Forms.TreeNode("Deprovision Windows Store Packages");
            System.Windows.Forms.TreeNode treeNode63 = new System.Windows.Forms.TreeNode("Remove OneDrive");
            System.Windows.Forms.TreeNode treeNode64 = new System.Windows.Forms.TreeNode("Disable Cortana");
            System.Windows.Forms.TreeNode treeNode65 = new System.Windows.Forms.TreeNode("Apps", new System.Windows.Forms.TreeNode[] {
            treeNode61,
            treeNode62,
            treeNode63,
            treeNode64});
            System.Windows.Forms.TreeNode treeNode66 = new System.Windows.Forms.TreeNode("Disable Force-Reboot After Windows Update");
            System.Windows.Forms.TreeNode treeNode67 = new System.Windows.Forms.TreeNode("Require Ctrl+Alt+Del at Sign In");
            System.Windows.Forms.TreeNode treeNode68 = new System.Windows.Forms.TreeNode("Disable Internet Search Results in Start Menu");
            System.Windows.Forms.TreeNode treeNode69 = new System.Windows.Forms.TreeNode("Set System Clock to UTC");
            System.Windows.Forms.TreeNode treeNode70 = new System.Windows.Forms.TreeNode("Turn Off App Recommendations");
            System.Windows.Forms.TreeNode treeNode71 = new System.Windows.Forms.TreeNode("Turn Off Game Bar");
            System.Windows.Forms.TreeNode treeNode72 = new System.Windows.Forms.TreeNode("Config", new System.Windows.Forms.TreeNode[] {
            treeNode66,
            treeNode67,
            treeNode68,
            treeNode69,
            treeNode70,
            treeNode71});
            System.Windows.Forms.TreeNode treeNode73 = new System.Windows.Forms.TreeNode("Disable Internet Explorer");
            System.Windows.Forms.TreeNode treeNode74 = new System.Windows.Forms.TreeNode("Features", new System.Windows.Forms.TreeNode[] {
            treeNode73});
            System.Windows.Forms.TreeNode treeNode75 = new System.Windows.Forms.TreeNode("Privacy");
            this.btnInitiateTreatment = new System.Windows.Forms.Button();
            this.grpTreatments = new System.Windows.Forms.GroupBox();
            this.treeViewTreatments = new Chemo.ChemoTreeView();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblProgressPercent = new System.Windows.Forms.Label();
            this.prgTreatmentApplication = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpTreatments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInitiateTreatment
            // 
            this.btnInitiateTreatment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitiateTreatment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitiateTreatment.Location = new System.Drawing.Point(288, 442);
            this.btnInitiateTreatment.Name = "btnInitiateTreatment";
            this.btnInitiateTreatment.Size = new System.Drawing.Size(246, 77);
            this.btnInitiateTreatment.TabIndex = 1;
            this.btnInitiateTreatment.Text = "Initiate Treatment";
            this.btnInitiateTreatment.UseVisualStyleBackColor = true;
            this.btnInitiateTreatment.Click += new System.EventHandler(this.BtnInitiateTreatment_Click);
            // 
            // grpTreatments
            // 
            this.grpTreatments.Controls.Add(this.treeViewTreatments);
            this.grpTreatments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTreatments.Location = new System.Drawing.Point(0, 0);
            this.grpTreatments.Name = "grpTreatments";
            this.grpTreatments.Size = new System.Drawing.Size(289, 522);
            this.grpTreatments.TabIndex = 2;
            this.grpTreatments.TabStop = false;
            this.grpTreatments.Text = "Treatments to Apply";
            // 
            // treeViewTreatments
            // 
            this.treeViewTreatments.CheckBoxes = true;
            this.treeViewTreatments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTreatments.Location = new System.Drawing.Point(3, 16);
            this.treeViewTreatments.Name = "treeViewTreatments";
            treeNode61.Checked = true;
            treeNode61.Name = "RemoveStoreApps";
            treeNode61.Tag = "RemoveStoreApps";
            treeNode61.Text = "Remove Windows Store Apps";
            treeNode61.ToolTipText = "Removes most pre-installed Windows Store apps.";
            treeNode62.Checked = true;
            treeNode62.Name = "DeprovisionStoreApps";
            treeNode62.Tag = "DeprovisionStoreApps";
            treeNode62.Text = "Deprovision Windows Store Packages";
            treeNode62.ToolTipText = "Deprovisions all packages. This prevents Windows Store application from re-appear" +
    "ing when a new user is created, or when a feature update is applied.";
            treeNode63.Checked = true;
            treeNode63.Name = "OneDrive";
            treeNode63.Tag = "OneDrive";
            treeNode63.Text = "Remove OneDrive";
            treeNode63.ToolTipText = "Completely removes OneDrive including ALL ONEDRIVE DATA.";
            treeNode64.Checked = true;
            treeNode64.Name = "DisableCortana";
            treeNode64.Tag = "DisableCortana";
            treeNode64.Text = "Disable Cortana";
            treeNode64.ToolTipText = "Prevents Cortana from appearing in the taskbar. A sign out is required to complet" +
    "e this operation.";
            treeNode65.Checked = true;
            treeNode65.Name = "Apps";
            treeNode65.Text = "Apps";
            treeNode65.ToolTipText = "Treatments related to store apps or other apps.";
            treeNode66.Checked = true;
            treeNode66.Name = "WindowsUpdateReboot";
            treeNode66.Tag = "WindowsUpdateReboot";
            treeNode66.Text = "Disable Force-Reboot After Windows Update";
            treeNode66.ToolTipText = "Prevents Windows from automatically rebooting after applying updates.";
            treeNode67.Checked = true;
            treeNode67.Name = "RequireCtrlAltDel";
            treeNode67.Tag = "RequireCtrlAltDel";
            treeNode67.Text = "Require Ctrl+Alt+Del at Sign In";
            treeNode67.ToolTipText = "Requires the user to press Ctrl+Alt+Del at the sign in screen for security reason" +
    "s.";
            treeNode68.Checked = true;
            treeNode68.Name = "DisableInternetSearchResults";
            treeNode68.Tag = "DisableInternetSearchResults";
            treeNode68.Text = "Disable Internet Search Results in Start Menu";
            treeNode68.ToolTipText = "Prevents internet junk from appearing when searching apps, files, etc. in the sta" +
    "rt menu.";
            treeNode69.Checked = true;
            treeNode69.Name = "SetClockUTC";
            treeNode69.Tag = "SetClockUTC";
            treeNode69.Text = "Set System Clock to UTC";
            treeNode69.ToolTipText = "Sets the system\'s hardware clock to Coordinated Universal Time (UTC). The Windows" +
    " default is localtime.";
            treeNode70.Checked = true;
            treeNode70.Name = "SuggestedApps";
            treeNode70.Tag = "SuggestedApps";
            treeNode70.Text = "Turn Off App Recommendations";
            treeNode70.ToolTipText = "Prevents \"recommended\" applications from displaying on the start menu.";
            treeNode71.Checked = true;
            treeNode71.Name = "GameBar";
            treeNode71.Tag = "GameBar";
            treeNode71.Text = "Turn Off Game Bar";
            treeNode71.ToolTipText = "Turns off the game bar for both apps and games.";
            treeNode72.Checked = true;
            treeNode72.Name = "Config";
            treeNode72.Text = "Config";
            treeNode72.ToolTipText = "Opinionated configuration changes.";
            treeNode73.Checked = true;
            treeNode73.Name = "InternetExplorer";
            treeNode73.Tag = "InternetExplorer";
            treeNode73.Text = "Disable Internet Explorer";
            treeNode73.ToolTipText = "Disables Internet Explorer 11. A system reboot is required to complete this opera" +
    "tion.";
            treeNode74.Checked = true;
            treeNode74.Name = "Features";
            treeNode74.Text = "Features";
            treeNode74.ToolTipText = "Windows Feature toggles.";
            treeNode75.Checked = true;
            treeNode75.Name = "Privacy";
            treeNode75.Text = "Privacy";
            treeNode75.ToolTipText = "Regain control of your privacy.";
            this.treeViewTreatments.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode65,
            treeNode72,
            treeNode74,
            treeNode75});
            this.treeViewTreatments.ShowNodeToolTips = true;
            this.treeViewTreatments.Size = new System.Drawing.Size(283, 503);
            this.treeViewTreatments.TabIndex = 0;
            this.treeViewTreatments.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTreatments_AfterCheck);
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.BackColor = System.Drawing.SystemColors.Window;
            this.txtResults.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.Location = new System.Drawing.Point(3, 45);
            this.txtResults.MaxLength = 65535;
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(531, 391);
            this.txtResults.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpTreatments);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblProgressPercent);
            this.splitContainer1.Panel2.Controls.Add(this.prgTreatmentApplication);
            this.splitContainer1.Panel2.Controls.Add(this.txtResults);
            this.splitContainer1.Panel2.Controls.Add(this.btnInitiateTreatment);
            this.splitContainer1.Size = new System.Drawing.Size(830, 522);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.TabIndex = 4;
            // 
            // lblProgressPercent
            // 
            this.lblProgressPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgressPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressPercent.Location = new System.Drawing.Point(464, 3);
            this.lblProgressPercent.Name = "lblProgressPercent";
            this.lblProgressPercent.Size = new System.Drawing.Size(70, 36);
            this.lblProgressPercent.TabIndex = 5;
            this.lblProgressPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // prgTreatmentApplication
            // 
            this.prgTreatmentApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgTreatmentApplication.Location = new System.Drawing.Point(3, 3);
            this.prgTreatmentApplication.Name = "prgTreatmentApplication";
            this.prgTreatmentApplication.Size = new System.Drawing.Size(455, 36);
            this.prgTreatmentApplication.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(854, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Chemo";
            this.grpTreatments.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnInitiateTreatment;
        private System.Windows.Forms.GroupBox grpTreatments;
        private System.Windows.Forms.TextBox txtResults;
        private ChemoTreeView treeViewTreatments;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ProgressBar prgTreatmentApplication;
        private System.Windows.Forms.Label lblProgressPercent;
    }
}