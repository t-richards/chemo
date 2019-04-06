using Chemo.CoreExt;
using Chemo.Treatment;
using Microsoft.Dism;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            DismApi.InitializeEx(DismLogLevel.LogErrors);

            // Icons
            treeViewTreatments.ImageList = StateIcons;
            treeViewTreatments.ImageKey = "NotStarted";
            lstResults.SmallImageList = StateIcons;
        }

        public static ImageList StateIcons
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
            List<BaseTreatment> shouldPerformTreatments = CollectTreatments().Where(tr => tr.ShouldPerformTreatment()).ToList();
            ApplyTreatments(shouldPerformTreatments);
        }

        private void ApplyTreatments(List<BaseTreatment> treatments)
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

        private List<BaseTreatment> CollectTreatments()
        {
            List<BaseTreatment> selectedTreatments = new List<BaseTreatment>();

            foreach (var treeNode in treeViewTreatments.Nodes.All())
            {
                if (treeNode.Checked && treeNode.Tag != null)
                {
                    string tag = treeNode.Tag.ToString();
                    string typeStr = "Chemo.Treatment." + tag;
                    Type componentType = Type.GetType(typeStr);

                    // Create treatment instance based on checkbox tag
                    BaseTreatment tr = (BaseTreatment)Activator.CreateInstance(componentType);

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
            lstResults.Items.Clear();
            lstResults.Refresh();
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
            List<BaseTreatment> selectedTreatments = CollectTreatments();
            List<BaseTreatment> performTreatments = selectedTreatments.Where(tr => tr.ShouldPerformTreatment()).ToList();
            stopWatch.Stop();

            // Analysis top item
            var analysis = new ListViewItem("Analysis Complete", "OK");
            analysis.SubItems.Add(new ListViewItem.ListViewSubItem(analysis, stopWatch.Elapsed.ToString()));

            var tooltip = new StringBuilder();
            tooltip.AppendFormat("Selected {0} treatments.\r\n", selectedTreatments.Count);
            tooltip.AppendFormat("{0} treatments need to be applied.\r\n", performTreatments.Count);
            tooltip.AppendFormat("{0} treatments already applied.\r\n", selectedTreatments.Count - performTreatments.Count);
            analysis.ToolTipText = tooltip.ToString();

            

            lstResults.Items.Add(analysis);

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
