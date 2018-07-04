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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chkOneDrive = new System.Windows.Forms.CheckBox();
            this.chkWindowsStoreApps = new System.Windows.Forms.CheckBox();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.grpTreatments.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInitiateTreatment
            // 
            this.btnInitiateTreatment.Location = new System.Drawing.Point(440, 199);
            this.btnInitiateTreatment.Name = "btnInitiateTreatment";
            this.btnInitiateTreatment.Size = new System.Drawing.Size(168, 64);
            this.btnInitiateTreatment.TabIndex = 1;
            this.btnInitiateTreatment.Text = "Initiate Treatment";
            this.btnInitiateTreatment.UseVisualStyleBackColor = true;
            this.btnInitiateTreatment.Click += new System.EventHandler(this.BtnInitiateTreatment_Click);
            // 
            // grpTreatments
            // 
            this.grpTreatments.Controls.Add(this.checkBox1);
            this.grpTreatments.Controls.Add(this.chkOneDrive);
            this.grpTreatments.Controls.Add(this.chkWindowsStoreApps);
            this.grpTreatments.Location = new System.Drawing.Point(12, 12);
            this.grpTreatments.Name = "grpTreatments";
            this.grpTreatments.Size = new System.Drawing.Size(422, 251);
            this.grpTreatments.TabIndex = 2;
            this.grpTreatments.TabStop = false;
            this.grpTreatments.Text = "Treatments to Apply";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 42);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(184, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Tag = "DeprovisionStoreApps";
            this.checkBox1.Text = "Deprovision Windows Store Apps";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // chkOneDrive
            // 
            this.chkOneDrive.AutoSize = true;
            this.chkOneDrive.Checked = true;
            this.chkOneDrive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOneDrive.Location = new System.Drawing.Point(6, 65);
            this.chkOneDrive.Name = "chkOneDrive";
            this.chkOneDrive.Size = new System.Drawing.Size(250, 17);
            this.chkOneDrive.TabIndex = 1;
            this.chkOneDrive.Tag = "OneDrive";
            this.chkOneDrive.Text = "Remove OneDrive (including all OneDrive data)";
            this.chkOneDrive.UseVisualStyleBackColor = true;
            // 
            // chkWindowsStoreApps
            // 
            this.chkWindowsStoreApps.AutoSize = true;
            this.chkWindowsStoreApps.Checked = true;
            this.chkWindowsStoreApps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWindowsStoreApps.Location = new System.Drawing.Point(6, 19);
            this.chkWindowsStoreApps.Name = "chkWindowsStoreApps";
            this.chkWindowsStoreApps.Size = new System.Drawing.Size(210, 17);
            this.chkWindowsStoreApps.TabIndex = 0;
            this.chkWindowsStoreApps.Tag = "RemoveStoreApps";
            this.chkWindowsStoreApps.Text = "Remove Installed Windows Store Apps";
            this.chkWindowsStoreApps.UseVisualStyleBackColor = true;
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.Location = new System.Drawing.Point(12, 269);
            this.txtResults.MaxLength = 65535;
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(596, 267);
            this.txtResults.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 548);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.grpTreatments);
            this.Controls.Add(this.btnInitiateTreatment);
            this.Name = "frmMain";
            this.Text = "Chemo";
            this.grpTreatments.ResumeLayout(false);
            this.grpTreatments.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnInitiateTreatment;
        private System.Windows.Forms.GroupBox grpTreatments;
        private System.Windows.Forms.CheckBox chkOneDrive;
        private System.Windows.Forms.CheckBox chkWindowsStoreApps;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}