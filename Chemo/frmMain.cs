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
        private static ImageList imageList;
        private int progressPercent = 0;
        private int progressIncrement = 0;

        public frmMain()
        {
            InitializeComponent();

            // Other init stuff here
            treeViewTreatments.ExpandAll();
            logger.SetTarget(txtResults);
            DismApi.InitializeEx(DismLogLevel.LogErrors);

            // Icons
            treeViewTreatments.ImageList = TreeIcons;
            treeViewTreatments.ImageKey = "NotStarted";
        }

        public static ImageList TreeIcons
        {
            get
            {
                if (imageList == null)
                {
                    imageList = new ImageList();
                    imageList.Images.Add("NotStarted", Properties.Resources.StatusNotStarted_16x);
                    imageList.Images.Add("Ok", Properties.Resources.StatusOK_16x);
                    imageList.Images.Add("Info", Properties.Resources.StatusInformation_16x);
                    imageList.Images.Add("Warning", Properties.Resources.StatusWarning_16x);
                    imageList.Images.Add("Error", Properties.Resources.StatusCriticalError_16x);
                }

                return imageList;
            }
        }

        private void BtnInitiateTreatment_Click(object sender, EventArgs e)
        {
            Reset();
            List<ITreatment> shouldPerformTreatments = CollectTreatments().Where(tr => tr.ShouldPerformTreatment()).ToList();
            ApplyTreatments(shouldPerformTreatments);
        }

        private void ApplyTreatments(List<ITreatment> treatments)
        {
            foreach (var treatment in treatments)
            {
                logger.Log("=== Applying: {0} ===", treatment.GetType().ToString());
                var result = treatment.PerformTreatment();
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
                    treeNode.ImageKey = "Ok";
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
            List<ITreatment> selectedTreatments = CollectTreatments();
            List<ITreatment> performTreatments = selectedTreatments.Where(tr => tr.ShouldPerformTreatment()).ToList();
            stopWatch.Stop();

            logger.Log("Analysis Complete: {0}", stopWatch.Elapsed);
            logger.Log("Selected {0} treatments.", selectedTreatments.Count);
            logger.Log("{0} treatments need to be applied.", performTreatments.Count);
            logger.Log("{0} treatments already applied.", selectedTreatments.Count - performTreatments.Count);
            logger.Log("");

            if (performTreatments.Count > 0)
            {
                logger.Log("Details of treatments to be applied:");
                foreach (var treatment in performTreatments)
                {
                    logger.Log(" - {0}", treatment.GetType().ToString());
                }
            }
            else
            {
                logger.Log("No treatments need to be applied!");
            }

        }
    }
}
