using System.Windows.Forms;

namespace Chemo
{
    class ChemoTreeView : TreeView
    {
        // https://docs.microsoft.com/en-us/windows/desktop/inputdev/wm-lbuttondblclk
        private const int WM_LBUTTONDBLCLK = 0x0203;

        protected override void WndProc(ref Message m)
        {
            // Filter double-click event
            if (m.Msg != WM_LBUTTONDBLCLK)
            {
                base.WndProc(ref m);
            }
        }
    }
}
