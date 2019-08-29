using Chemo.Treatment;
using System;
using System.Windows.Forms;

namespace Chemo
{
    class TreatmentNode : TreeNode
    {
        public BaseTreatment Treatment { get; }

        public TreatmentNode(BaseTreatment treatment)
        {
            Treatment = treatment;
            Text = Treatment.Name();
            ToolTipText = Treatment.Tooltip();
            Checked = true;
            ImageKey = "NotStarted";
        }
    }
}
