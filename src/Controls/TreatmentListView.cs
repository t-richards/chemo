using Chemo.Utilities;

namespace Chemo.Controls
{
    /// <summary>
    /// A details list view that can show images in any column, not just the first, whose last column fills the
    /// remaining width, and that can switch to Windows' dark look.
    /// </summary>
    internal sealed class TreatmentListView : ListView
    {
        // The image shown in each column after the first, keyed by item and column.
        private readonly Dictionary<(ListViewItem Item, int Column), string> subItemImages = [];

        public TreatmentListView()
        {
            // Windows Forms turns this into the list view's own double buffering, which stops rows flickering as the
            // pointer moves over them and tooltips come and go.
            DoubleBuffered = true;
        }

        /// <summary>
        /// Whether the list uses Windows' dark look, with status icons drawn for a dark background.
        /// </summary>
        public bool Dark
        {
            get;
            set
            {
                field = value;

                if (value)
                {
                    BackColor = DarkTheme.Surface;
                    ForeColor = DarkTheme.Text;
                }
                else
                {
                    ResetBackColor();
                    ResetForeColor();
                }

                // Disposing an image list detaches it from the list, so the new icons go in first.
                ImageList? oldIcons = SmallImageList;
                SmallImageList = FluentIcons.CreateStatusIcons(LogicalToDeviceUnits(16), value);
                oldIcons?.Dispose();

                if (IsHandleCreated)
                {
                    SetWindowThemes();
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            SetWindowThemes();

            // Windows supports images in any column, but Windows Forms doesn't expose it.
            UnsafeNativeMethods.SendMessage(
                Handle,
                UnsafeNativeMethods.LVM_SETEXTENDEDLISTVIEWSTYLE,
                new IntPtr(UnsafeNativeMethods.LVS_EX_SUBITEMIMAGES),
                new IntPtr(UnsafeNativeMethods.LVS_EX_SUBITEMIMAGES));

            // Items are added back when the handle is recreated, but their column images aren't.
            foreach (KeyValuePair<(ListViewItem Item, int Column), string> image in subItemImages)
            {
                SendSubItemImage(image.Key.Item, image.Key.Column, image.Value);
            }
        }

        /// <summary>
        /// Gives the list, its column headers and its tooltips the light or dark look. In light mode the list uses
        /// File Explorer's look instead of the classic one Windows Forms defaults to. Windows' dark list has no dark
        /// column headers, so they use the dark look of File Explorer's folder view instead. If any of this fails, the
        /// list keeps the classic look.
        /// </summary>
        private void SetWindowThemes()
        {
            _ = UnsafeNativeMethods.SetWindowTheme(Handle, Dark ? "DarkMode_Explorer" : "Explorer", null);
            _ = UnsafeNativeMethods.SetWindowTheme(HeaderHandle(), Dark ? "DarkMode_ItemsView" : null, null);

            IntPtr toolTipHandle = UnsafeNativeMethods.SendMessage(Handle, UnsafeNativeMethods.LVM_GETTOOLTIPS, IntPtr.Zero, IntPtr.Zero);
            if (toolTipHandle != IntPtr.Zero)
            {
                DarkTheme.SetWindowTheme(toolTipHandle, Dark);
            }
        }

        private IntPtr HeaderHandle()
        {
            return UnsafeNativeMethods.SendMessage(Handle, UnsafeNativeMethods.LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
        }

        protected override void WndProc(ref Message m)
        {
            if (Dark && m.Msg == UnsafeNativeMethods.WM_NOTIFY && DrawHeaderText(ref m))
            {
                return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Windows' dark column headers draw their text in the light look's color, so this sets the dark look's text
        /// color as each header is drawn.
        /// </summary>
        /// <returns>Returns true if the message was the header asking how to draw, false otherwise.</returns>
        private bool DrawHeaderText(ref Message m)
        {
            UnsafeNativeMethods.NMHDR header = (UnsafeNativeMethods.NMHDR)m.GetLParam(typeof(UnsafeNativeMethods.NMHDR));
            if (header.code != UnsafeNativeMethods.NM_CUSTOMDRAW || header.hwndFrom != HeaderHandle())
            {
                return false;
            }

            UnsafeNativeMethods.NMCUSTOMDRAW draw = (UnsafeNativeMethods.NMCUSTOMDRAW)m.GetLParam(typeof(UnsafeNativeMethods.NMCUSTOMDRAW));
            switch (draw.dwDrawStage)
            {
                case UnsafeNativeMethods.CDDS_PREPAINT:
                    m.Result = new IntPtr(UnsafeNativeMethods.CDRF_NOTIFYITEMDRAW);
                    return true;

                case UnsafeNativeMethods.CDDS_ITEMPREPAINT:
                    _ = UnsafeNativeMethods.SetTextColor(draw.hdc, ColorTranslator.ToWin32(ForeColor));
                    m.Result = new IntPtr(UnsafeNativeMethods.CDRF_DODEFAULT);
                    return true;

                default:
                    return false;
            }
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);

            if (View == View.Details && Columns.Count > 0)
            {
                // A width of -2 sizes the last column to fill the rest of the list.
                Columns[Columns.Count - 1].Width = -2;
            }
        }

        /// <summary>
        /// Shows an image from the small image list next to the text in a column other than the first.
        /// </summary>
        public void SetSubItemImage(ListViewItem item, int column, string imageKey)
        {
            subItemImages[(item, column)] = imageKey;

            if (IsHandleCreated)
            {
                SendSubItemImage(item, column, imageKey);
            }
        }

        private void SendSubItemImage(ListViewItem item, int column, string imageKey)
        {
            UnsafeNativeMethods.LVITEMW lvItem = new()
            {
                mask = UnsafeNativeMethods.LVIF_IMAGE,
                iItem = item.Index,
                iSubItem = column,
                iImage = SmallImageList.Images.IndexOfKey(imageKey),
            };

            UnsafeNativeMethods.SendMessage(Handle, UnsafeNativeMethods.LVM_SETITEMW, IntPtr.Zero, ref lvItem);
        }
    }
}
