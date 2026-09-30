using Chemo.Treatment;

namespace Chemo.Controls
{
    /// <summary>
    /// A bold row in the treatment list whose checkbox checks or unchecks every treatment in its category.
    /// </summary>
    internal sealed class CategoryItem : ListViewItem
    {
        public Category Category { get; }

        public List<TreatmentItem> Treatments { get; } = [];

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
