using Chemo.Treatment;
using Microsoft.Dism;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Chemo
{
    public partial class frmMain : Form
    {
        private static readonly Logger logger = Logger.Instance;

        public frmMain()
        {
            InitializeComponent();

            // Other init stuff here
            logger.SetTarget(txtResults);
            DismApi.Initialize(DismLogLevel.LogErrors);
        }

        private void BtnInitiateTreatment_Click(object sender, EventArgs e)
        {
            txtResults.Clear();

            foreach (CheckBox c in grpTreatments.Controls.OfType<CheckBox>())
            {
                logger.Log("Component {0} value: {1}", c.Text, c.Checked.ToString());
                if (c.Checked && c.Tag != null)
                {
                    Type componentType = Type.GetType("Chemo.Treatment." + c.Tag.ToString());
                    ITreatment tr = (ITreatment)Activator.CreateInstance(componentType);
                    tr.PerformTreatment();
                }
            }
        }
    }
}
