using System.Collections.Generic;
using System.Windows.Forms;

namespace Chemo.CoreExt
{
    public static class TreeNodeExt
    {
        public static IEnumerable<TreeNode> All(this TreeNodeCollection nodes)
        {
            foreach (TreeNode n in nodes)
            {
                yield return n;
                
                foreach (TreeNode child in n.Nodes.All())
                {
                    yield return child;
                }
            }
        }
    }
}
