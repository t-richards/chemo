using Chemo.CoreExt;
using Chemo.Treatment;
using Microsoft.Dism;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
                new TreatmentNode(new Treatment.Apps.RemoveStoreApps()),
                new TreatmentNode(new Treatment.Apps.DeprovisionStoreApps()),
                new TreatmentNode(new Treatment.Apps.OneDrive()),
                new TreatmentNode(new Treatment.Apps.DisableCortana()),
            })
            {
                Checked = true,
                ToolTipText = "Treatments related to store apps or other apps."
            };

            // config
            TreeNode config = new TreeNode("Config", new TreeNode[] {
                new TreatmentNode(new Treatment.Config.WindowsUpdateReboot()),
                new TreatmentNode(new Treatment.Config.RequireCtrlAltDel()),
                new TreatmentNode(new Treatment.Config.DisableInternetSearchResults()),
                new TreatmentNode(new Treatment.Config.SetClockUTC()),
                new TreatmentNode(new Treatment.Config.SuggestedApps()),
                new TreatmentNode(new Treatment.Config.GameBar()),
            })
            {
                Checked = true,
                ToolTipText = "Opinionated configuration changes."
            };

            // features
            TreeNode features = new TreeNode("Features", new TreeNode[]{
                new TreatmentNode(new Treatment.Features.InternetExplorer())
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
            List<TreatmentNode> performNodes = CollectTreatmentNodes();
            ApplyTreatments(performNodes);
        }

        private async void ApplyTreatments(List<TreatmentNode> treeNodes)
        {
            foreach (TreatmentNode node in treeNodes)
            {
                var treatment = node.Treatment;
                ConfiguredTaskAwaitable<bool> performTask = Task<bool>.Factory.StartNew(() => treatment.PerformTreatment()).ConfigureAwait(true);
                Stopwatch stopWatch = new Stopwatch();

                stopWatch.Start();
                var result = await performTask;
                stopWatch.Stop();

                var listItem = new ListViewItem(treatment.Name());
                if (result)
                {
                    listItem.ImageKey = "Ok";
                    listItem.SubItems.Add("Successfully applied");
                }
                else
                {
                    listItem.ImageKey = "Error";
                    listItem.SubItems.Add("Error, right click for detail");
                }
                listItem.SubItems.Add(stopWatch.Elapsed.ToString());
                lstResults.Items.Add(listItem);
                IncrementProgress();
            }

            SetProgress(100);
        }

        private List<TreatmentNode> CollectTreatmentNodes()
        {
            List<TreatmentNode> selectedTreatments = new List<TreatmentNode>();

            foreach (TreeNode treeNode in treeViewTreatments.Nodes.All())
            {
                if (treeNode.Checked && treeNode.GetType() == typeof(TreatmentNode))
                {
                    selectedTreatments.Add((TreatmentNode)treeNode);
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
                if (treeNode.GetType() == typeof(TreatmentNode))
                {
                    var treatment = ((TreatmentNode)treeNode).Treatment;
                    treatment.Logger.Reset();
                }
            }
        }

        private void IncrementProgress()
        {
            progressPercent += progressIncrement;
            lblProgressPercent.Text = $"{progressPercent}%";
            lblProgressPercent.Refresh();
            prgTreatmentApplication.Value = progressPercent;
        }

        private void SetProgress(int value)
        {
            progressPercent = value;
            lblProgressPercent.Text = $"{progressPercent}%";
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
            aboutForm.Dispose();
        }

        private async void BtnAnalyze_Click(object sender, EventArgs e)
        {
            Reset();

            int performTreatmentCount = 0;

            Stopwatch overallAnalysisTime = new Stopwatch();
            overallAnalysisTime.Start();

            List<TreatmentNode> selectedTreatments = CollectTreatmentNodes();

            foreach (TreatmentNode node in selectedTreatments)
            {
                var treatment = node.Treatment;
                ListViewItem detail = new ListViewItem(treatment.Name());
                ConfiguredTaskAwaitable<bool> analyzeTask = Task<bool>.Factory.StartNew(() => treatment.ShouldPerformTreatment()).ConfigureAwait(true);
                Stopwatch itemTime = new Stopwatch();

                itemTime.Start();
                bool shouldPerform = await analyzeTask;
                itemTime.Stop();

                node.ImageKey = "Ok";

                if (shouldPerform)
                {
                    detail.SubItems.Add("Should be applied");
                    performTreatmentCount += 1;
                }
                else
                {
                    detail.SubItems.Add("Already applied");
                }

                detail.Tag = treatment;
                detail.ImageKey = "Ok";
                detail.SubItems.Add(itemTime.Elapsed.ToString());
                lstResults.Items.Add(detail);
            }

            overallAnalysisTime.Stop();

            // Analysis top item
            ListViewItem analysis = new ListViewItem("Analysis Complete", "OK");
            analysis.SubItems.Add("");
            analysis.SubItems.Add(overallAnalysisTime.Elapsed.ToString());

            StringBuilder tooltip = new StringBuilder();
            tooltip.Append($"Selected {selectedTreatments.Count} treatments.\r\n");
            tooltip.Append($"{performTreatmentCount} treatments need to be applied.\r\n");
            tooltip.Append($"{selectedTreatments.Count - performTreatmentCount} treatments already applied.\r\n");
            analysis.ToolTipText = tooltip.ToString();
            lstResults.Items.Insert(0, analysis);
        }

        private void LstResults_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            Point clickPoint = new Point(e.X, e.Y);
            ListViewHitTestInfo hitTestInfo = lstResults.HitTest(clickPoint);

            if (hitTestInfo.Item == null || hitTestInfo.Item.Tag == null)
            {
                return;
            }

            string text = $"Show Details for {hitTestInfo.Item.Text}";
            MenuItem[] menuItems = new MenuItem[]
            {
                new MenuItem(text, LstResults_OnContextMenuClick)
            };
            lstResults.ContextMenu = new ContextMenu(menuItems);
            lstResults.ContextMenu.Tag = hitTestInfo.Item.Tag;
        }

        private void LstResults_OnContextMenuClick(object sender, EventArgs e)
        {
            MenuItem senderItem = (MenuItem)sender;
            BaseTreatment treatment = (BaseTreatment)senderItem.Parent.Tag;
            string message = treatment.Logger.ToString();

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            MessageBox.Show(message, treatment.Name());
        }
    }
}
