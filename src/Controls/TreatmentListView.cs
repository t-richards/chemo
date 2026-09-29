using Chemo.Utilities;

namespace Chemo.Controls
{
    /// <summary>
    /// A details list view that can show images in any column, not just the first, and whose last column
    /// fills the remaining width.
    /// </summary>
    class TreatmentListView : ListView
    {
        // The image shown in each column after the first, keyed by item and column.
        private readonly Dictionary<(ListViewItem Item, int Column), string> subItemImages = new Dictionary<(ListViewItem, int), string>();

        public TreatmentListView()
        {
            // Windows Forms turns this into the list view's own double buffering, which stops rows flickering as the
            // pointer moves over them and tooltips come and go.
            DoubleBuffered = true;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Use File Explorer's look instead of the classic one Windows Forms defaults to.
            UnsafeNativeMethods.SetWindowTheme(Handle, "Explorer", null);

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
            UnsafeNativeMethods.LVITEMW lvItem = new UnsafeNativeMethods.LVITEMW
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
