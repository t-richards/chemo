using Chemo.Treatment;
using Chemo.Utilities;

namespace Chemo.Controls
{
    /// <summary>
    /// A treatment's row in the treatment list, showing its status and how long it took.
    /// </summary>
    internal sealed class TreatmentItem : ListViewItem
    {
        private const int StatusColumn = 1;
        private const int TimeColumn = 2;

        public BaseTreatment Treatment { get; }

        public CategoryItem Category { get; }

        public TreatmentStatus Status { get; private set; }

        public TreatmentItem(BaseTreatment treatment, CategoryItem category)
        {
            Treatment = treatment;
            Category = category;
            Text = treatment.Name;
            ToolTipText = treatment.Description;
            Checked = true;
            IndentCount = 1;

            SubItems.Add("");
            SubItems.Add("");
        }

        /// <summary>
        /// Shows a status in the list. The item must already be in a <see cref="TreatmentListView"/>.
        /// </summary>
        public void SetStatus(TreatmentStatus status, string text, TimeSpan? duration = null)
        {
            Status = status;
            SubItems[StatusColumn].Text = text;
            SubItems[TimeColumn].Text = duration.HasValue ? Durations.Humanize(duration.Value) : "";
            ((TreatmentListView)ListView).SetSubItemImage(this, StatusColumn, status.ToString());
        }
    }
}
