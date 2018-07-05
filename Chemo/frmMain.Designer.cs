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
            this.chkInternetExplorer = new System.Windows.Forms.CheckBox();
            this.chkDeprovisionStoreApps = new System.Windows.Forms.CheckBox();
            this.chkOneDrive = new System.Windows.Forms.CheckBox();
            this.chkRemoveStoreApps = new System.Windows.Forms.CheckBox();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.grpTreatments.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInitiateTreatment
            // 
            this.btnInitiateTreatment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitiateTreatment.Location = new System.Drawing.Point(393, 12);
            this.btnInitiateTreatment.Name = "btnInitiateTreatment";
            this.btnInitiateTreatment.Size = new System.Drawing.Size(369, 121);
            this.btnInitiateTreatment.TabIndex = 1;
            this.btnInitiateTreatment.Text = "Initiate Treatment";
            this.btnInitiateTreatment.UseVisualStyleBackColor = true;
            this.btnInitiateTreatment.Click += new System.EventHandler(this.BtnInitiateTreatment_Click);
            // 
            // grpTreatments
            // 
            this.grpTreatments.Controls.Add(this.chkInternetExplorer);
            this.grpTreatments.Controls.Add(this.chkDeprovisionStoreApps);
            this.grpTreatments.Controls.Add(this.chkOneDrive);
            this.grpTreatments.Controls.Add(this.chkRemoveStoreApps);
            this.grpTreatments.Location = new System.Drawing.Point(12, 12);
            this.grpTreatments.Name = "grpTreatments";
            this.grpTreatments.Size = new System.Drawing.Size(375, 121);
            this.grpTreatments.TabIndex = 2;
            this.grpTreatments.TabStop = false;
            this.grpTreatments.Text = "Treatments to Apply";
            // 
            // chkInternetExplorer
            // 
            this.chkInternetExplorer.AutoSize = true;
            this.chkInternetExplorer.Checked = true;
            this.chkInternetExplorer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInternetExplorer.Location = new System.Drawing.Point(6, 88);
            this.chkInternetExplorer.Name = "chkInternetExplorer";
            this.chkInternetExplorer.Size = new System.Drawing.Size(141, 17);
            this.chkInternetExplorer.TabIndex = 3;
            this.chkInternetExplorer.Tag = "InternetExplorer";
            this.chkInternetExplorer.Text = "Disable Internet Explorer";
            this.chkInternetExplorer.UseVisualStyleBackColor = true;
            // 
            // chkDeprovisionStoreApps
            // 
            this.chkDeprovisionStoreApps.AutoSize = true;
            this.chkDeprovisionStoreApps.Checked = true;
            this.chkDeprovisionStoreApps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeprovisionStoreApps.Location = new System.Drawing.Point(6, 42);
            this.chkDeprovisionStoreApps.Name = "chkDeprovisionStoreApps";
            this.chkDeprovisionStoreApps.Size = new System.Drawing.Size(184, 17);
            this.chkDeprovisionStoreApps.TabIndex = 2;
            this.chkDeprovisionStoreApps.Tag = "DeprovisionStoreApps";
            this.chkDeprovisionStoreApps.Text = "Deprovision Windows Store Apps";
            this.chkDeprovisionStoreApps.UseVisualStyleBackColor = true;
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
            // chkRemoveStoreApps
            // 
            this.chkRemoveStoreApps.AutoSize = true;
            this.chkRemoveStoreApps.Checked = true;
            this.chkRemoveStoreApps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemoveStoreApps.Location = new System.Drawing.Point(6, 19);
            this.chkRemoveStoreApps.Name = "chkRemoveStoreApps";
            this.chkRemoveStoreApps.Size = new System.Drawing.Size(210, 17);
            this.chkRemoveStoreApps.TabIndex = 0;
            this.chkRemoveStoreApps.Tag = "RemoveStoreApps";
            this.chkRemoveStoreApps.Text = "Remove Installed Windows Store Apps";
            this.chkRemoveStoreApps.UseVisualStyleBackColor = true;
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.Location = new System.Drawing.Point(12, 139);
            this.txtResults.MaxLength = 65535;
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(750, 280);
            this.txtResults.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 431);
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
        private System.Windows.Forms.CheckBox chkRemoveStoreApps;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.CheckBox chkDeprovisionStoreApps;
        private System.Windows.Forms.CheckBox chkInternetExplorer;
    }
}