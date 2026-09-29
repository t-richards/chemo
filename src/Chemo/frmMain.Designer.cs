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
            this.splitDetails = new System.Windows.Forms.SplitContainer();
            this.lstTreatments = new Chemo.TreatmentListView();
            this.columnTreatment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnInitiateTreatment = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgTreatmentApplication = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitDetails)).BeginInit();
            this.splitDetails.Panel1.SuspendLayout();
            this.splitDetails.Panel2.SuspendLayout();
            this.splitDetails.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // splitDetails
            //
            this.splitDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitDetails.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitDetails.Location = new System.Drawing.Point(12, 36);
            this.splitDetails.Name = "splitDetails";
            this.splitDetails.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // splitDetails.Panel1
            //
            this.splitDetails.Panel1.Controls.Add(this.lstTreatments);
            //
            // splitDetails.Panel2
            //
            this.splitDetails.Panel2.Controls.Add(this.txtDetails);
            this.splitDetails.Size = new System.Drawing.Size(836, 488);
            this.splitDetails.SplitterDistance = 330;
            this.splitDetails.TabIndex = 0;
            //
            // lstTreatments
            //
            this.lstTreatments.CheckBoxes = true;
            this.lstTreatments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnTreatment,
            this.columnStatus,
            this.columnTime});
            this.lstTreatments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTreatments.FullRowSelect = true;
            this.lstTreatments.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstTreatments.HideSelection = false;
            this.lstTreatments.Location = new System.Drawing.Point(0, 0);
            this.lstTreatments.MultiSelect = false;
            this.lstTreatments.Name = "lstTreatments";
            this.lstTreatments.ShowItemToolTips = true;
            this.lstTreatments.Size = new System.Drawing.Size(836, 330);
            this.lstTreatments.TabIndex = 0;
            this.lstTreatments.UseCompatibleStateImageBehavior = false;
            this.lstTreatments.View = System.Windows.Forms.View.Details;
            this.lstTreatments.SelectedIndexChanged += new System.EventHandler(this.LstTreatments_SelectedIndexChanged);
            //
            // columnTreatment
            //
            this.columnTreatment.Text = "Treatment";
            this.columnTreatment.Width = 420;
            //
            // columnStatus
            //
            this.columnStatus.Text = "Status";
            this.columnStatus.Width = 260;
            //
            // columnTime
            //
            this.columnTime.Text = "Time";
            this.columnTime.Width = 150;
            //
            // txtDetails
            //
            this.txtDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetails.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtDetails.Location = new System.Drawing.Point(0, 0);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.PlaceholderText = "Select a treatment to see what it does and what happened when it last ran.";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDetails.Size = new System.Drawing.Size(836, 154);
            this.txtDetails.TabIndex = 0;
            this.txtDetails.WordWrap = false;
            //
            // btnAnalyze
            //
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalyze.Location = new System.Drawing.Point(600, 536);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(120, 30);
            this.btnAnalyze.TabIndex = 1;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.BtnAnalyze_Click);
            //
            // btnInitiateTreatment
            //
            this.btnInitiateTreatment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitiateTreatment.Location = new System.Drawing.Point(728, 536);
            this.btnInitiateTreatment.Name = "btnInitiateTreatment";
            this.btnInitiateTreatment.Size = new System.Drawing.Size(120, 30);
            this.btnInitiateTreatment.TabIndex = 2;
            this.btnInitiateTreatment.Text = "Initiate Treatment";
            this.btnInitiateTreatment.UseVisualStyleBackColor = true;
            this.btnInitiateTreatment.Click += new System.EventHandler(this.BtnInitiateTreatment_Click);
            //
            // statusStrip1
            //
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.prgTreatmentApplication});
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(860, 22);
            this.statusStrip1.TabIndex = 3;
            //
            // lblStatus
            //
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(643, 17);
            this.lblStatus.Spring = true;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // prgTreatmentApplication
            //
            this.prgTreatmentApplication.Name = "prgTreatmentApplication";
            this.prgTreatmentApplication.Size = new System.Drawing.Size(200, 16);
            //
            // menuStrip1
            //
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(860, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            //
            // helpToolStripMenuItem
            //
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionToolStripMenuItem,
            this.githubToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            //
            // versionToolStripMenuItem
            //
            this.versionToolStripMenuItem.Enabled = false;
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.versionToolStripMenuItem.Text = "Version";
            //
            // githubToolStripMenuItem
            //
            this.githubToolStripMenuItem.Name = "githubToolStripMenuItem";
            this.githubToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.githubToolStripMenuItem.Text = "View on GitHub";
            this.githubToolStripMenuItem.Click += new System.EventHandler(this.GithubToolStripMenuItem_Click);
            //
            // frmMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.Controls.Add(this.splitDetails);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.btnInitiateTreatment);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Chemo";
            this.splitDetails.Panel1.ResumeLayout(false);
            this.splitDetails.Panel2.ResumeLayout(false);
            this.splitDetails.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDetails)).EndInit();
            this.splitDetails.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitDetails;
        private TreatmentListView lstTreatments;
        private System.Windows.Forms.ColumnHeader columnTreatment;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private System.Windows.Forms.ColumnHeader columnTime;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnInitiateTreatment;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar prgTreatmentApplication;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem githubToolStripMenuItem;
    }
}
