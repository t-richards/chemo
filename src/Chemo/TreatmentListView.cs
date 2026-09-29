using System;
using System.Windows.Forms;

namespace Chemo
{
    /// <summary>
    /// A details list view that can show images in any column, not just the first, and whose last column
    /// fills the remaining width.
    /// </summary>
    class TreatmentListView : ListView
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Windows Forms themes the list for dark mode itself, but leaves light mode with the classic look.
            if (!Application.IsDarkModeEnabled)
            {
                UnsafeNativeMethods.SetWindowTheme(Handle, "Explorer", null);
            }

            // Windows supports images in any column, but Windows Forms doesn't expose it.
            UnsafeNativeMethods.SendMessage(
                Handle,
                UnsafeNativeMethods.LVM_SETEXTENDEDLISTVIEWSTYLE,
                UnsafeNativeMethods.LVS_EX_SUBITEMIMAGES,
                UnsafeNativeMethods.LVS_EX_SUBITEMIMAGES);

            // Items are added back when the handle is recreated, but their column images aren't.
            foreach (ListViewItem item in Items)
            {
                for (int column = 1; column < item.SubItems.Count; column++)
                {
                    if (item.SubItems[column].Tag is string imageKey)
                    {
                        SendSubItemImage(item, column, imageKey);
                    }
                }
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
            item.SubItems[column].Tag = imageKey;

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
