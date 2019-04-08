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
            BaseTreatment treatment = (BaseTreatment)Activator.CreateInstance(treatmentType);
            Text = treatment.Name();
            ToolTipText = treatment.Tooltip();
            ImageKey = "NotStarted";
        }
    }
}
