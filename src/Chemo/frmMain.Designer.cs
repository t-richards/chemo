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
            this.btnInitiateTreatment = new System.Windows.Forms.Button();
            this.grpTreatments = new System.Windows.Forms.GroupBox();
            this.treeViewTreatments = new Chemo.ChemoTreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstResults = new System.Windows.Forms.ListView();
            this.column1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAnalyze = new System.Windows.Forms.Button();
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
            this.btnInitiateTreatment.Location = new System.Drawing.Point(395, 482);
            this.btnInitiateTreatment.Name = "btnInitiateTreatment";
            this.btnInitiateTreatment.Size = new System.Drawing.Size(120, 40);
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
            this.grpTreatments.Size = new System.Drawing.Size(332, 525);
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
            this.treeViewTreatments.ShowNodeToolTips = true;
            this.treeViewTreatments.Size = new System.Drawing.Size(326, 506);
            this.treeViewTreatments.TabIndex = 0;
            this.treeViewTreatments.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewTreatments_AfterCheck);
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
            this.splitContainer1.Size = new System.Drawing.Size(854, 525);
            this.splitContainer1.SplitterDistance = 332;
            this.splitContainer1.TabIndex = 4;
            // 
            // lstResults
            // 
            this.lstResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column1,
            this.column2,
            this.column3});
            this.lstResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstResults.HideSelection = false;
            this.lstResults.Location = new System.Drawing.Point(3, 45);
            this.lstResults.Name = "lstResults";
            this.lstResults.ShowItemToolTips = true;
            this.lstResults.Size = new System.Drawing.Size(512, 431);
            this.lstResults.TabIndex = 7;
            this.lstResults.UseCompatibleStateImageBehavior = false;
            this.lstResults.View = System.Windows.Forms.View.Details;
            this.lstResults.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LstResults_MouseClick);
            // 
            // column1
            // 
            this.column1.Text = "";
            this.column1.Width = 209;
            // 
            // column2
            // 
            this.column2.Text = "";
            this.column2.Width = 169;
            // 
            // column3
            // 
            this.column3.Text = "";
            this.column3.Width = 118;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAnalyze.Location = new System.Drawing.Point(3, 482);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(120, 40);
            this.btnAnalyze.TabIndex = 6;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.BtnAnalyze_Click);
            // 
            // lblProgressPercent
            // 
            this.lblProgressPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgressPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressPercent.Location = new System.Drawing.Point(445, 3);
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
            this.prgTreatmentApplication.Size = new System.Drawing.Size(436, 36);
            this.prgTreatmentApplication.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(878, 24);
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
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 564);
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
        private System.Windows.Forms.ColumnHeader column1;
        private System.Windows.Forms.ColumnHeader column2;
        private System.Windows.Forms.ColumnHeader column3;
    }
}