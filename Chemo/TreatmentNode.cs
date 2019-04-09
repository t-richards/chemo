using Chemo.Treatment;
using System;
using System.Windows.Forms;

namespace Chemo
{
    class TreatmentNode : TreeNode
    {
        public BaseTreatment Treatment { get; }

        public TreatmentNode(Type treatmentType)
        {
            Treatment = (BaseTreatment)Activator.CreateInstance(treatmentType);
            Text = Treatment.Name();
            ToolTipText = Treatment.Tooltip();
            Checked = true;
            ImageKey = "NotStarted";
        }
    }
}
