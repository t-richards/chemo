namespace Chemo
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
            if (disposing)
            {
                components?.Dispose();
                categoryFont.Dispose();
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treatmentList = new Chemo.Controls.TreatmentListView();
            this.treatmentColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.detailsTextBox = new System.Windows.Forms.TextBox();
            this.analyzeButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemThemeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightThemeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkThemeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            //
            // splitContainer
            //
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(12, 36);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // splitContainer.Panel1
            //
            this.splitContainer.Panel1.Controls.Add(this.treatmentList);
            //
            // splitContainer.Panel2
            //
            this.splitContainer.Panel2.Controls.Add(this.detailsTextBox);
            this.splitContainer.Size = new System.Drawing.Size(780, 488);
            this.splitContainer.SplitterDistance = 330;
            this.splitContainer.TabIndex = 0;
            //
            // treatmentList
            //
            this.treatmentList.CheckBoxes = true;
            this.treatmentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.treatmentColumn,
            this.statusColumn,
            this.timeColumn});
            this.treatmentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treatmentList.FullRowSelect = true;
            this.treatmentList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.treatmentList.HideSelection = false;
            this.treatmentList.Location = new System.Drawing.Point(0, 0);
            this.treatmentList.MultiSelect = false;
            this.treatmentList.Name = "treatmentList";
            this.treatmentList.ShowItemToolTips = true;
            this.treatmentList.Size = new System.Drawing.Size(780, 330);
            this.treatmentList.TabIndex = 0;
            this.treatmentList.UseCompatibleStateImageBehavior = false;
            this.treatmentList.View = System.Windows.Forms.View.Details;
            this.treatmentList.SelectedIndexChanged += new System.EventHandler(this.TreatmentList_SelectedIndexChanged);
            //
            // treatmentColumn
            //
            this.treatmentColumn.Text = "Treatment";
            this.treatmentColumn.Width = 340;
            //
            // statusColumn
            //
            this.statusColumn.Text = "Status";
            this.statusColumn.Width = 220;
            //
            // timeColumn
            //
            this.timeColumn.Text = "Time";
            this.timeColumn.Width = 199;
            //
            // detailsTextBox
            //
            this.detailsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.detailsTextBox.Location = new System.Drawing.Point(0, 0);
            this.detailsTextBox.Multiline = true;
            this.detailsTextBox.Name = "detailsTextBox";
            this.detailsTextBox.ReadOnly = true;
            this.detailsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.detailsTextBox.Size = new System.Drawing.Size(780, 154);
            this.detailsTextBox.TabIndex = 0;
            this.detailsTextBox.WordWrap = false;
            //
            // analyzeButton
            //
            this.analyzeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.analyzeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.analyzeButton.Location = new System.Drawing.Point(544, 536);
            this.analyzeButton.Name = "analyzeButton";
            this.analyzeButton.Size = new System.Drawing.Size(120, 30);
            this.analyzeButton.TabIndex = 1;
            this.analyzeButton.Text = "Analyze";
            this.analyzeButton.UseVisualStyleBackColor = true;
            this.analyzeButton.Click += new System.EventHandler(this.AnalyzeButton_Click);
            //
            // applyButton
            //
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.applyButton.Location = new System.Drawing.Point(672, 536);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(120, 30);
            this.applyButton.TabIndex = 2;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            //
            // statusStrip
            //
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 578);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(804, 22);
            this.statusStrip.TabIndex = 3;
            //
            // statusLabel
            //
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(587, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // progressBar
            //
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 16);
            //
            // menuStrip
            //
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewMenuItem,
            this.helpMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(804, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip";
            //
            // viewMenuItem
            //
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewMenuItem.Text = "View";
            //
            // themeMenuItem
            //
            this.themeMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemThemeMenuItem,
            this.lightThemeMenuItem,
            this.darkThemeMenuItem});
            this.themeMenuItem.Name = "themeMenuItem";
            this.themeMenuItem.Size = new System.Drawing.Size(180, 22);
            this.themeMenuItem.Text = "Theme";
            //
            // systemThemeMenuItem
            //
            this.systemThemeMenuItem.Checked = true;
            this.systemThemeMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.systemThemeMenuItem.Name = "systemThemeMenuItem";
            this.systemThemeMenuItem.Size = new System.Drawing.Size(180, 22);
            this.systemThemeMenuItem.Text = "Use system setting";
            this.systemThemeMenuItem.Click += new System.EventHandler(this.ThemeMenuItem_Click);
            //
            // lightThemeMenuItem
            //
            this.lightThemeMenuItem.Name = "lightThemeMenuItem";
            this.lightThemeMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lightThemeMenuItem.Text = "Light";
            this.lightThemeMenuItem.Click += new System.EventHandler(this.ThemeMenuItem_Click);
            //
            // darkThemeMenuItem
            //
            this.darkThemeMenuItem.Name = "darkThemeMenuItem";
            this.darkThemeMenuItem.Size = new System.Drawing.Size(180, 22);
            this.darkThemeMenuItem.Text = "Dark";
            this.darkThemeMenuItem.Click += new System.EventHandler(this.ThemeMenuItem_Click);
            //
            // helpMenuItem
            //
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionMenuItem,
            this.githubMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "Help";
            //
            // versionMenuItem
            //
            this.versionMenuItem.Name = "versionMenuItem";
            this.versionMenuItem.Size = new System.Drawing.Size(180, 22);
            this.versionMenuItem.Text = "Version";
            this.versionMenuItem.Click += new System.EventHandler(this.VersionMenuItem_Click);
            //
            // githubMenuItem
            //
            this.githubMenuItem.Name = "githubMenuItem";
            this.githubMenuItem.Size = new System.Drawing.Size(180, 22);
            this.githubMenuItem.Text = "View on GitHub";
            this.githubMenuItem.Click += new System.EventHandler(this.GithubMenuItem_Click);
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 600);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.analyzeButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Chemo";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer;
        private Chemo.Controls.TreatmentListView treatmentList;
        private System.Windows.Forms.ColumnHeader treatmentColumn;
        private System.Windows.Forms.ColumnHeader statusColumn;
        private System.Windows.Forms.ColumnHeader timeColumn;
        private System.Windows.Forms.TextBox detailsTextBox;
        private System.Windows.Forms.Button analyzeButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemThemeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightThemeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkThemeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem githubMenuItem;
    }
}
