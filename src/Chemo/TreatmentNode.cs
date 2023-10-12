using Chemo.Treatment;
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

        public TreatmentNode Unchecked()
        {
            Checked = false;
            return this;
        }
    }
}
