using Chemo.CoreExt;
using Chemo.Treatment;
using Microsoft.Dism;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Chemo
{
    // Size: 870x600
    public partial class frmMain : Form
    {
        private static readonly Logger logger = Logger.Instance;
        private int progressPercent = 0;
        private int progressIncrement = 0;

        public frmMain()
        {
            InitializeComponent();

            // Other init stuff here
            treeViewTreatments.ExpandAll();
            logger.SetTarget(txtResults);
            DismApi.InitializeEx(DismLogLevel.LogErrors);
        }

        private void BtnInitiateTreatment_Click(object sender, EventArgs e)
        {
            Reset();
            List<ITreatment> selectedTreatments = CollectTreatments();
            ApplyTreatments(selectedTreatments);
        }

        private void ApplyTreatments(List<ITreatment> treatments)
        {
            foreach (var treatment in treatments)
            {
                logger.Log("=== Applying: {0} ===", treatment.GetType().ToString());
                treatment.PerformTreatment();
                IncrementProgress();
                logger.Log("");
            }

            SetProgress(100);
        }

        private List<ITreatment> CollectTreatments()
        {
            List<ITreatment> selectedTreatments = new List<ITreatment>();

            foreach (var treeNode in treeViewTreatments.Nodes.All())
            {
                if (treeNode.Checked && treeNode.Tag != null)
                {
                    string tag = treeNode.Tag.ToString();
                    string typeStr = "Chemo.Treatment." + tag;
                    Type componentType = Type.GetType(typeStr);

                    // Create treatment instance based on checkbox tag
                    ITreatment tr = (ITreatment)Activator.CreateInstance(componentType);

                    selectedTreatments.Add(tr);
                }
            }

            progressIncrement = (int)Math.Floor(100.0f / selectedTreatments.Count);

            return selectedTreatments;
        }

        private void Reset()
        {
            // State
            progressPercent = 0;
            progressIncrement = 0;

            // Components
            txtResults.Clear();
            txtResults.Refresh();
            lblProgressPercent.Text = "";
            lblProgressPercent.Refresh();
            prgTreatmentApplication.Value = 0;
        }

        private void IncrementProgress()
        {
            progressPercent += progressIncrement;
            lblProgressPercent.Text = String.Format("{0}%", progressPercent);
            lblProgressPercent.Refresh();
            prgTreatmentApplication.Value = progressPercent;
        }

        private void SetProgress(int value)
        {
            progressPercent = value;
            lblProgressPercent.Text = String.Format("{0}%", progressPercent);
            lblProgressPercent.Refresh();
            prgTreatmentApplication.Value = progressPercent;
        }

        private void treeViewTreatments_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown)
            {
                return;
            }

            try
            {
                CheckTreeViewNodes(e.Node, e.Node.Checked);
            }
            finally
            {
                e.Node.TreeView.EndUpdate();
            }
        }

        private void CheckTreeViewNodes(TreeNode node, bool isChecked)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = isChecked;

                if (child.Nodes.Count > 0)
                {
                    CheckTreeViewNodes(child, isChecked);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new frmAbout();
            aboutForm.ShowDialog();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            Reset();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<ITreatment> selectedTreatments = CollectTreatments().Where(tr => tr.ShouldPerformTreatment()).ToList();
            stopWatch.Stop();
            logger.Log("Analysis Complete: {0}", stopWatch.Elapsed);

            if (selectedTreatments.Count <= 0)
            {
                logger.Log("No treatments to apply.");
                return;
            }

            logger.Log("Details of treatments to be applied:");
            foreach (var treatment in selectedTreatments)
            {
                if (treatment.ShouldPerformTreatment())
                {
                    logger.Log(" - {0}", treatment.GetType().ToString());
                }
            }
        }
    }
}
