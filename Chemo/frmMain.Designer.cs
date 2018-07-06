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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Remove Installed Windows Store Apps");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Deprovision Windows Store Packages");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Remove OneDrive (including all data)");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Disable Internet Explorer 11");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Disable Cortana");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Apps", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Disable Windows Update Auto-Reboot");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Require Ctrl+Alt+Del At Logon");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Config", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Privacy");
            this.btnInitiateTreatment = new System.Windows.Forms.Button();
            this.grpTreatments = new System.Windows.Forms.GroupBox();
            this.treeViewTreatments = new System.Windows.Forms.TreeView();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.grpTreatments.SuspendLayout();
            this.SuspendLayout();
            //
            // btnInitiateTreatment
            //
            this.btnInitiateTreatment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitiateTreatment.Location = new System.Drawing.Point(393, 120);
            this.btnInitiateTreatment.Name = "btnInitiateTreatment";
            this.btnInitiateTreatment.Size = new System.Drawing.Size(258, 90);
            this.btnInitiateTreatment.TabIndex = 1;
            this.btnInitiateTreatment.Text = "Initiate Treatment";
            this.btnInitiateTreatment.UseVisualStyleBackColor = true;
            this.btnInitiateTreatment.Click += new System.EventHandler(this.BtnInitiateTreatment_Click);
            //
            // grpTreatments
            //
            this.grpTreatments.Controls.Add(this.treeViewTreatments);
            this.grpTreatments.Location = new System.Drawing.Point(12, 12);
            this.grpTreatments.Name = "grpTreatments";
            this.grpTreatments.Size = new System.Drawing.Size(375, 201);
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
            treeNode1.Checked = true;
            treeNode1.Name = "Node3";
            treeNode1.Tag = "RemoveStoreApps";
            treeNode1.Text = "Remove Installed Windows Store Apps";
            treeNode1.ToolTipText = "Removes most pre-installed Windows Store apps, except for Calculator, the store i" +
    "tself, and WSL.";
            treeNode2.Checked = true;
            treeNode2.Name = "Node4";
            treeNode2.Tag = "DeprovisionStoreApps";
            treeNode2.Text = "Deprovision Windows Store Packages";
            treeNode3.Checked = true;
            treeNode3.Name = "Node7";
            treeNode3.Tag = "OneDrive";
            treeNode3.Text = "Remove OneDrive (including all data)";
            treeNode4.Checked = true;
            treeNode4.Name = "Node8";
            treeNode4.Tag = "InternetExplorer";
            treeNode4.Text = "Disable Internet Explorer 11";
            treeNode5.Checked = true;
            treeNode5.Name = "Node9";
            treeNode5.Tag = "DisableCortana";
            treeNode5.Text = "Disable Cortana";
            treeNode6.Checked = true;
            treeNode6.Name = "Node0";
            treeNode6.Text = "Apps";
            treeNode7.Checked = true;
            treeNode7.Name = "Node5";
            treeNode7.Tag = "WindowsUpdateReboot";
            treeNode7.Text = "Disable Windows Update Auto-Reboot";
            treeNode8.Checked = true;
            treeNode8.Name = "Node6";
            treeNode8.Tag = "RequireCtrlAltDel";
            treeNode8.Text = "Require Ctrl+Alt+Del At Logon";
            treeNode9.Checked = true;
            treeNode9.Name = "Node2";
            treeNode9.Text = "Config";
            treeNode10.Checked = true;
            treeNode10.Name = "Node1";
            treeNode10.Text = "Privacy";
            this.treeViewTreatments.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode9,
            treeNode10});
            this.treeViewTreatments.ShowNodeToolTips = true;
            this.treeViewTreatments.Size = new System.Drawing.Size(369, 182);
            this.treeViewTreatments.TabIndex = 4;
            this.treeViewTreatments.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTreatments_AfterCheck);
            //
            // txtResults
            //
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.Location = new System.Drawing.Point(12, 219);
            this.txtResults.MaxLength = 65535;
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(796, 308);
            this.txtResults.TabIndex = 3;
            //
            // frmMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 539);
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
        private System.Windows.Forms.TreeView treeViewTreatments;
    }
}