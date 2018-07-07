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
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Remove Windows Store Apps");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Deprovision Windows Store Packages");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Remove OneDrive");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Disable Cortana");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Apps", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Disable Force-Reboot After Windows Update");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Require Ctrl+Alt+Del At Login");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Config", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Disable Internet Explorer");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Features", new System.Windows.Forms.TreeNode[] {
            treeNode20});
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Privacy");
            this.btnInitiateTreatment = new System.Windows.Forms.Button();
            this.grpTreatments = new System.Windows.Forms.GroupBox();
            this.treeViewTreatments = new Chemo.ChemoTreeView();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.grpTreatments.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInitiateTreatment
            // 
            this.btnInitiateTreatment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitiateTreatment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitiateTreatment.Location = new System.Drawing.Point(596, 472);
            this.btnInitiateTreatment.Name = "btnInitiateTreatment";
            this.btnInitiateTreatment.Size = new System.Drawing.Size(246, 77);
            this.btnInitiateTreatment.TabIndex = 1;
            this.btnInitiateTreatment.Text = "Initiate Treatment";
            this.btnInitiateTreatment.UseVisualStyleBackColor = true;
            this.btnInitiateTreatment.Click += new System.EventHandler(this.BtnInitiateTreatment_Click);
            // 
            // grpTreatments
            // 
            this.grpTreatments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpTreatments.Controls.Add(this.treeViewTreatments);
            this.grpTreatments.Location = new System.Drawing.Point(12, 12);
            this.grpTreatments.Name = "grpTreatments";
            this.grpTreatments.Size = new System.Drawing.Size(300, 537);
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
            treeNode12.Checked = true;
            treeNode12.Name = "Node3";
            treeNode12.Tag = "RemoveStoreApps";
            treeNode12.Text = "Remove Windows Store Apps";
            treeNode12.ToolTipText = "Removes most junk Windows Store Apps.";
            treeNode13.Checked = true;
            treeNode13.Name = "Node4";
            treeNode13.Tag = "DeprovisionStoreApps";
            treeNode13.Text = "Deprovision Windows Store Packages";
            treeNode13.ToolTipText = "Deprovisions all packages. This prevents Windows Store application from re-appear" +
    "ing when a new user is created, or when a feature update is applied.";
            treeNode14.Checked = true;
            treeNode14.Name = "Node5";
            treeNode14.Tag = "OneDrive";
            treeNode14.Text = "Remove OneDrive";
            treeNode14.ToolTipText = "Removes OneDrive including ALL USER DATA.";
            treeNode15.Checked = true;
            treeNode15.Name = "Node7";
            treeNode15.Tag = "DisableCortana";
            treeNode15.Text = "Disable Cortana";
            treeNode15.ToolTipText = "Disables Cortana. A sign out is required to complete this operation.";
            treeNode16.Checked = true;
            treeNode16.Name = "Node0";
            treeNode16.Text = "Apps";
            treeNode17.Checked = true;
            treeNode17.Name = "Node8";
            treeNode17.Tag = "WindowsUpdateReboot";
            treeNode17.Text = "Disable Force-Reboot After Windows Update";
            treeNode17.ToolTipText = "Prevents automatic rebooting after applying Windows Updates.";
            treeNode18.Checked = true;
            treeNode18.Name = "Node9";
            treeNode18.Tag = "RequireCtrlAltDel";
            treeNode18.Text = "Require Ctrl+Alt+Del At Login";
            treeNode18.ToolTipText = "Requires Ctrl+Alt+Del to be pressed at the sign in screen.";
            treeNode19.Checked = true;
            treeNode19.Name = "Node1";
            treeNode19.Text = "Config";
            treeNode20.Checked = true;
            treeNode20.Name = "Node6";
            treeNode20.Tag = "InternetExplorer";
            treeNode20.Text = "Disable Internet Explorer";
            treeNode20.ToolTipText = "Disables Internet Explorer 11.";
            treeNode21.Checked = true;
            treeNode21.Name = "Node0";
            treeNode21.Text = "Features";
            treeNode22.Checked = true;
            treeNode22.Name = "Node2";
            treeNode22.Text = "Privacy";
            this.treeViewTreatments.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode19,
            treeNode21,
            treeNode22});
            this.treeViewTreatments.ShowNodeToolTips = true;
            this.treeViewTreatments.Size = new System.Drawing.Size(294, 518);
            this.treeViewTreatments.TabIndex = 0;
            this.treeViewTreatments.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTreatments_AfterCheck);
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.Location = new System.Drawing.Point(318, 12);
            this.txtResults.MaxLength = 65535;
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(524, 454);
            this.txtResults.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 561);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.grpTreatments);
            this.Controls.Add(this.btnInitiateTreatment);
            this.Name = "frmMain";
            this.Text = "Chemo";
            this.grpTreatments.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnInitiateTreatment;
        private System.Windows.Forms.GroupBox grpTreatments;
        private System.Windows.Forms.TextBox txtResults;
        private ChemoTreeView treeViewTreatments;
    }
}