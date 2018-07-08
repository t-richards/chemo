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
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Remove Windows Store Apps");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Deprovision Windows Store Packages");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Remove OneDrive");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Disable Cortana");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Apps", new System.Windows.Forms.TreeNode[] {
            treeNode23,
            treeNode24,
            treeNode25,
            treeNode26});
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Disable Force-Reboot After Windows Update");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Require Ctrl+Alt+Del At Sign In");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Config", new System.Windows.Forms.TreeNode[] {
            treeNode28,
            treeNode29});
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Disable Internet Explorer");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Features", new System.Windows.Forms.TreeNode[] {
            treeNode31});
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Privacy");
            this.btnInitiateTreatment = new System.Windows.Forms.Button();
            this.grpTreatments = new System.Windows.Forms.GroupBox();
            this.treeViewTreatments = new Chemo.ChemoTreeView();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
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
            treeNode23.Checked = true;
            treeNode23.Name = "Node3";
            treeNode23.Tag = "RemoveStoreApps";
            treeNode23.Text = "Remove Windows Store Apps";
            treeNode23.ToolTipText = "Removes most pre-installed Windows Store apps.";
            treeNode24.Checked = true;
            treeNode24.Name = "Node4";
            treeNode24.Tag = "DeprovisionStoreApps";
            treeNode24.Text = "Deprovision Windows Store Packages";
            treeNode24.ToolTipText = "Deprovisions all packages. This prevents Windows Store application from re-appear" +
    "ing when a new user is created, or when a feature update is applied.";
            treeNode25.Checked = true;
            treeNode25.Name = "Node5";
            treeNode25.Tag = "OneDrive";
            treeNode25.Text = "Remove OneDrive";
            treeNode25.ToolTipText = "Completely removes OneDrive including ALL ONEDRIVE DATA.";
            treeNode26.Checked = true;
            treeNode26.Name = "Node7";
            treeNode26.Tag = "DisableCortana";
            treeNode26.Text = "Disable Cortana";
            treeNode26.ToolTipText = "Prevents Cortana from appearing in the taskbar. A sign out is required to complet" +
    "e this operation.";
            treeNode27.Checked = true;
            treeNode27.Name = "Node0";
            treeNode27.Text = "Apps";
            treeNode27.ToolTipText = "Treatments related to store apps or other apps.";
            treeNode28.Checked = true;
            treeNode28.Name = "Node8";
            treeNode28.Tag = "WindowsUpdateReboot";
            treeNode28.Text = "Disable Force-Reboot After Windows Update";
            treeNode28.ToolTipText = "Prevents Windows from automatically rebooting after applying updates.";
            treeNode29.Checked = true;
            treeNode29.Name = "Node9";
            treeNode29.Tag = "RequireCtrlAltDel";
            treeNode29.Text = "Require Ctrl+Alt+Del At Sign In";
            treeNode29.ToolTipText = "Requires the user to press Ctrl+Alt+Del at the sign in screen for security reason" +
    "s.";
            treeNode30.Checked = true;
            treeNode30.Name = "Node1";
            treeNode30.Text = "Config";
            treeNode30.ToolTipText = "Opinionated configuration changes.";
            treeNode31.Checked = true;
            treeNode31.Name = "Node6";
            treeNode31.Tag = "InternetExplorer";
            treeNode31.Text = "Disable Internet Explorer";
            treeNode31.ToolTipText = "Disables Internet Explorer 11. A system reboot is required to complete this opera" +
    "tion.";
            treeNode32.Checked = true;
            treeNode32.Name = "Node0";
            treeNode32.Text = "Features";
            treeNode32.ToolTipText = "Windows Feature toggles.";
            treeNode33.Checked = true;
            treeNode33.Name = "Node2";
            treeNode33.Text = "Privacy";
            treeNode33.ToolTipText = "Regain control of your privacy.";
            this.treeViewTreatments.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode27,
            treeNode30,
            treeNode32,
            treeNode33});
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
            this.txtResults.Location = new System.Drawing.Point(3, 3);
            this.txtResults.MaxLength = 65535;
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(531, 433);
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
            this.splitContainer1.Panel2.Controls.Add(this.txtResults);
            this.splitContainer1.Panel2.Controls.Add(this.btnInitiateTreatment);
            this.splitContainer1.Size = new System.Drawing.Size(830, 522);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.TabIndex = 4;
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
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
    }
}