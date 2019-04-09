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
        private static ImageList imageList;
        private int progressPercent = 0;
        private int progressIncrement = 0;

        public frmMain()
        {
            InitializeComponent();

            // Other init stuff here
            DismApi.InitializeEx(DismLogLevel.LogErrors);

            // Treatments
            InitTreatments();

            // Icons
            treeViewTreatments.ImageList = StateIcons;
            treeViewTreatments.ImageKey = "NotStarted";
            lstResults.SmallImageList = StateIcons;
        }

        public void InitTreatments()
        {
            treeViewTreatments.Nodes.Clear();

            // root node
            TreeNode root = new TreeNode("All Treatments")
            {
                Checked = true
            };

            // apps
            TreeNode apps = new TreeNode("Apps", new TreeNode[] {
                new TreatmentNode(typeof(Treatment.Apps.RemoveStoreApps)),
                new TreatmentNode(typeof(Treatment.Apps.DeprovisionStoreApps)),
                new TreatmentNode(typeof(Treatment.Apps.OneDrive)),
                new TreatmentNode(typeof(Treatment.Apps.DisableCortana)),
            })
            {
                Checked = true,
                ToolTipText = "Treatments related to store apps or other apps."
            };

            // config
            TreeNode config = new TreeNode("Config", new TreeNode[] {
                new TreatmentNode(typeof(Treatment.Config.WindowsUpdateReboot)),
                new TreatmentNode(typeof(Treatment.Config.RequireCtrlAltDel)),
                new TreatmentNode(typeof(Treatment.Config.DisableInternetSearchResults)),
                new TreatmentNode(typeof(Treatment.Config.SetClockUTC)),
                new TreatmentNode(typeof(Treatment.Config.SuggestedApps)),
                new TreatmentNode(typeof(Treatment.Config.GameBar)),
            })
            {
                Checked = true,
                ToolTipText = "Opinionated configuration changes."
            };

            // features
            TreeNode features = new TreeNode("Features", new TreeNode[]{
                new TreatmentNode(typeof(Treatment.Features.InternetExplorer))
            })
            {
                Checked = true,
                ToolTipText = "Windows Feature toggles."
            };

            // add root node children, and add node to tree
            root.Nodes.AddRange(new TreeNode[]
            {
                apps,
                config,
                features
            });
            treeViewTreatments.Nodes.Add(root);

            treeViewTreatments.ExpandAll();
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
            List<TreatmentNode> performNodes = CollectTreatments();
            ApplyTreatments(performNodes);
        }

        private void ApplyTreatments(List<TreatmentNode> treeNodes)
        {
            foreach (TreatmentNode node in treeNodes)
            {
                var treatment = node.Treatment;

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var result = treatment.PerformTreatment();
                stopWatch.Stop();

                var listItem = new ListViewItem(treatment.Name());
                if (result)
                {
                    listItem.ImageKey = "Ok";
                }
                else
                {
                    listItem.ImageKey = "Error";
                }
                listItem.SubItems.Add(stopWatch.Elapsed.ToString());
                listItem.ToolTipText = treatment.logger.ToString();
                lstResults.Items.Add(listItem);
                IncrementProgress();
            }

            SetProgress(100);
        }

        private List<TreatmentNode> CollectTreatments()
        {
            List<TreatmentNode> selectedTreatments = new List<TreatmentNode>();

            foreach (TreeNode treeNode in treeViewTreatments.Nodes.All())
            {
                if (treeNode.Checked && treeNode.GetType() == typeof(TreatmentNode))
                {
                    selectedTreatments.Add((TreatmentNode)treeNode);
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

            foreach (var treeNode in treeViewTreatments.Nodes.All())
            {
                treeNode.ImageKey = "NotStarted";
            }
        }

        private void IncrementProgress()
        {
            progressPercent += progressIncrement;
            lblProgressPercent.Text = string.Format("{0}%", progressPercent);
            lblProgressPercent.Refresh();
            prgTreatmentApplication.Value = progressPercent;
        }

        private void SetProgress(int value)
        {
            progressPercent = value;
            lblProgressPercent.Text = string.Format("{0}%", progressPercent);
            lblProgressPercent.Refresh();
            prgTreatmentApplication.Value = progressPercent;
        }

        private void TreeViewTreatments_AfterCheck(object sender, TreeViewEventArgs e)
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

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new frmAbout();
            aboutForm.ShowDialog();
        }

        private void BtnAnalyze_Click(object sender, EventArgs e)
        {
            Reset();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<TreatmentNode> selectedTreatments = CollectTreatments();
            List<TreatmentNode> performTreatments = selectedTreatments.Where(tr => tr.Treatment.ShouldPerformTreatment()).ToList();
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

            foreach (var node in selectedTreatments)
            {
                var treatment = node.Treatment;
                ListViewItem detail = new ListViewItem(treatment.Name());
                detail.SubItems.Add("Should be applied");
                lstResults.Items.Add(detail);
            }
        }
    }
}
