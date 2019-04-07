using Chemo.Treatment;
using System.Windows.Forms;

namespace Chemo
{
    class TreatmentNode : TreeNode
    {
        public BaseTreatment Treatment { get; }

        public TreatmentNode(string text, BaseTreatment treatment) : base(text)
        {
            Treatment = treatment;
            ImageKey = "NotStarted";
        }
    }
}
