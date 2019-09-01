using System.Reflection;
using System.Windows.Forms;

namespace Chemo.Treatment
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, System.EventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly();

            txtVersion.Text = $"Version: {assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}";
        }

        private void OnGithubLabelClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/t-richards/chemo");
        }
    }
}
