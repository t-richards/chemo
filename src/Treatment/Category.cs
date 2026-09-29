namespace Chemo.Treatment
{
    internal sealed class Category
    {
        /// <summary>
        /// The category name shown in the treatment tree.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A plain-language description of what the category covers, shown as a tooltip.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The treatments in this category, in the order they are applied.
        /// </summary>
        public IReadOnlyList<SettingsTreatment> Treatments { get; }

        public Category(string name, string description, params SettingsTreatment[] treatments)
        {
            Name = name;
            Description = description;
            Treatments = treatments;
        }
    }
}
