using Chemo.Treatment;
using Microsoft.Dism;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Chemo
{
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

        private void WalkNodes(TreeNodeCollection nodes)
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

                    // Perform work in the background to not lock up the UI
                    Thread treatmentThread = new Thread(tr.PerformTreatment);
                    treatmentThread.Start();
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
    }
}
