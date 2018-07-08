using Chemo.Treatment;
using Microsoft.Dism;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chemo
{
    // Size: 870x600
    public partial class frmMain : Form
    {
        private static readonly Logger logger = Logger.Instance;

        public frmMain()
        {
            InitializeComponent();

            // Other init stuff here
            treeViewTreatments.ExpandAll();
            logger.SetTarget(txtResults);
            DismApi.Initialize(DismLogLevel.LogErrors);
        }

        private void BtnInitiateTreatment_Click(object sender, EventArgs e)
        {
            txtResults.Clear();
            txtResults.Refresh();

            // Recursively walk the tree and apply treatments as we come to them
            WalkNodes(treeViewTreatments.Nodes);
        }

        private async void WalkNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode treeNode in nodes)
            {
                if (treeNode.Checked && treeNode.Tag != null)
                {
                    string tag = treeNode.Tag.ToString();
                    logger.Log("\r");
                    logger.Log("== Applying Treatment: {0} ==", treeNode.Text);

                    // Create treatment instance based on checkbox tag
                    string typeStr = "Chemo.Treatment." + tag;
                    Type componentType = Type.GetType(typeStr);
                    ITreatment tr = (ITreatment)Activator.CreateInstance(componentType);

                    // Perform treatment work in the background to not lock up the UI
                    // Only run one task at a time because funny things happen when they run in parallel
                    await Task.Run(() =>
                        tr.PerformTreatment()
                    );
                }

                WalkNodes(treeNode.Nodes);
            }
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
    }
}
