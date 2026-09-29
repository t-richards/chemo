using Chemo.Treatment;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Chemo.Controls
{
    /// <summary>
    /// A bold row in the treatment list whose checkbox checks or unchecks every treatment in its category.
    /// </summary>
    class CategoryItem : ListViewItem
    {
        public Category Category { get; }

        public List<TreatmentItem> Treatments { get; } = new List<TreatmentItem>();

        public CategoryItem(Category category, Font font)
        {
            Category = category;
            Text = category.Name;
            ToolTipText = category.Description;
            Font = font;
            Checked = true;

            SubItems.Add("");
            SubItems.Add("");
        }
    }
}
