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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Remove Windows Store Apps");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Deprovision Windows Store Packages");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Remove OneDrive");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Disable Cortana");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Apps", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Disable Force-Reboot After Windows Update");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Require Ctrl+Alt+Del at Sign In");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Disable Internet Search Results in Start Menu");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Set System Clock to UTC");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Turn Off App Recommendations");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Turn Off Game Bar");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Config", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Disable Internet Explorer");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Features", new System.Windows.Forms.TreeNode[] {
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Privacy");
            this.btnInitiateTreatment = new System.Windows.Forms.Button();
            this.grpTreatments = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstResults = new System.Windows.Forms.ListView();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.lblProgressPercent = new System.Windows.Forms.Label();
            this.prgTreatmentApplication = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnResultItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTimeTaken = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeViewTreatments = new Chemo.ChemoTreeView();
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
            this.btnInitiateTreatment.Location = new System.Drawing.Point(289, 442);
            this.btnInitiateTreatment.Name = "btnInitiateTreatment";
            this.btnInitiateTreatment.Size = new System.Drawing.Size(221, 77);
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
            this.grpTreatments.Size = new System.Drawing.Size(313, 522);
            this.grpTreatments.TabIndex = 2;
            this.grpTreatments.TabStop = false;
            this.grpTreatments.Text = "Treatments to Apply";
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
            this.splitContainer1.Panel2.Controls.Add(this.lstResults);
            this.splitContainer1.Panel2.Controls.Add(this.btnAnalyze);
            this.splitContainer1.Panel2.Controls.Add(this.lblProgressPercent);
            this.splitContainer1.Panel2.Controls.Add(this.prgTreatmentApplication);
            this.splitContainer1.Panel2.Controls.Add(this.btnInitiateTreatment);
            this.splitContainer1.Size = new System.Drawing.Size(830, 522);
            this.splitContainer1.SplitterDistance = 313;
            this.splitContainer1.TabIndex = 4;
            // 
            // lstResults
            // 
            this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnResultItem,
            this.columnTimeTaken});
            this.lstResults.Location = new System.Drawing.Point(3, 45);
            this.lstResults.Name = "lstResults";
            this.lstResults.ShowItemToolTips = true;
            this.lstResults.Size = new System.Drawing.Size(531, 391);
            this.lstResults.TabIndex = 7;
            this.lstResults.UseCompatibleStateImageBehavior = false;
            this.lstResults.View = System.Windows.Forms.View.Details;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAnalyze.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalyze.Location = new System.Drawing.Point(3, 442);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(221, 77);
            this.btnAnalyze.TabIndex = 6;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // lblProgressPercent
            // 
            this.lblProgressPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgressPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressPercent.Location = new System.Drawing.Point(440, 3);
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
            this.prgTreatmentApplication.Size = new System.Drawing.Size(431, 36);
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
            // columnResultItem
            // 
            this.columnResultItem.Text = "Result";
            this.columnResultItem.Width = 137;
            // 
            // columnTimeTaken
            // 
            this.columnTimeTaken.Text = "Time Taken";
            this.columnTimeTaken.Width = 154;
            // 
            // treeViewTreatments
            // 
            this.treeViewTreatments.CheckBoxes = true;
            this.treeViewTreatments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTreatments.Location = new System.Drawing.Point(3, 16);
            this.treeViewTreatments.Name = "treeViewTreatments";
            treeNode1.Checked = true;
            treeNode1.Name = "RemoveStoreApps";
            treeNode1.Tag = "RemoveStoreApps";
            treeNode1.Text = "Remove Windows Store Apps";
            treeNode1.ToolTipText = "Removes most pre-installed Windows Store apps.";
            treeNode2.Checked = true;
            treeNode2.Name = "DeprovisionStoreApps";
            treeNode2.Tag = "DeprovisionStoreApps";
            treeNode2.Text = "Deprovision Windows Store Packages";
            treeNode2.ToolTipText = "Deprovisions all packages. This prevents Windows Store application from re-appear" +
    "ing when a new user is created, or when a feature update is applied.";
            treeNode3.Checked = true;
            treeNode3.Name = "OneDrive";
            treeNode3.Tag = "OneDrive";
            treeNode3.Text = "Remove OneDrive";
            treeNode3.ToolTipText = "Completely removes OneDrive including ALL ONEDRIVE DATA.";
            treeNode4.Checked = true;
            treeNode4.Name = "DisableCortana";
            treeNode4.Tag = "DisableCortana";
            treeNode4.Text = "Disable Cortana";
            treeNode4.ToolTipText = "Prevents Cortana from appearing in the taskbar. A sign out is required to complet" +
    "e this operation.";
            treeNode5.Checked = true;
            treeNode5.Name = "Apps";
            treeNode5.Text = "Apps";
            treeNode5.ToolTipText = "Treatments related to store apps or other apps.";
            treeNode6.Checked = true;
            treeNode6.Name = "WindowsUpdateReboot";
            treeNode6.Tag = "WindowsUpdateReboot";
            treeNode6.Text = "Disable Force-Reboot After Windows Update";
            treeNode6.ToolTipText = "Prevents Windows from automatically rebooting after applying updates.";
            treeNode7.Checked = true;
            treeNode7.Name = "RequireCtrlAltDel";
            treeNode7.Tag = "RequireCtrlAltDel";
            treeNode7.Text = "Require Ctrl+Alt+Del at Sign In";
            treeNode7.ToolTipText = "Requires the user to press Ctrl+Alt+Del at the sign in screen for security reason" +
    "s.";
            treeNode8.Checked = true;
            treeNode8.Name = "DisableInternetSearchResults";
            treeNode8.Tag = "DisableInternetSearchResults";
            treeNode8.Text = "Disable Internet Search Results in Start Menu";
            treeNode8.ToolTipText = "Prevents internet junk from appearing when searching apps, files, etc. in the sta" +
    "rt menu.";
            treeNode9.Checked = true;
            treeNode9.Name = "SetClockUTC";
            treeNode9.Tag = "SetClockUTC";
            treeNode9.Text = "Set System Clock to UTC";
            treeNode9.ToolTipText = "Sets the system\'s hardware clock to Coordinated Universal Time (UTC). The Windows" +
    " default is localtime.";
            treeNode10.Checked = true;
            treeNode10.Name = "SuggestedApps";
            treeNode10.Tag = "SuggestedApps";
            treeNode10.Text = "Turn Off App Recommendations";
            treeNode10.ToolTipText = "Prevents \"recommended\" applications from displaying on the start menu.";
            treeNode11.Checked = true;
            treeNode11.Name = "GameBar";
            treeNode11.Tag = "GameBar";
            treeNode11.Text = "Turn Off Game Bar";
            treeNode11.ToolTipText = "Turns off the game bar for both apps and games.";
            treeNode12.Checked = true;
            treeNode12.Name = "Config";
            treeNode12.Text = "Config";
            treeNode12.ToolTipText = "Opinionated configuration changes.";
            treeNode13.Checked = true;
            treeNode13.Name = "InternetExplorer";
            treeNode13.Tag = "InternetExplorer";
            treeNode13.Text = "Disable Internet Explorer";
            treeNode13.ToolTipText = "Disables Internet Explorer 11. A system reboot is required to complete this opera" +
    "tion.";
            treeNode14.Checked = true;
            treeNode14.Name = "Features";
            treeNode14.Text = "Features";
            treeNode14.ToolTipText = "Windows Feature toggles.";
            treeNode15.Checked = true;
            treeNode15.Name = "Privacy";
            treeNode15.Text = "Privacy";
            treeNode15.ToolTipText = "Regain control of your privacy.";
            this.treeViewTreatments.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode12,
            treeNode14,
            treeNode15});
            this.treeViewTreatments.ShowNodeToolTips = true;
            this.treeViewTreatments.Size = new System.Drawing.Size(307, 503);
            this.treeViewTreatments.TabIndex = 0;
            this.treeViewTreatments.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTreatments_AfterCheck);
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
        private ChemoTreeView treeViewTreatments;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ProgressBar prgTreatmentApplication;
        private System.Windows.Forms.Label lblProgressPercent;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.ListView lstResults;
        private System.Windows.Forms.ColumnHeader columnResultItem;
        private System.Windows.Forms.ColumnHeader columnTimeTaken;
    }
}