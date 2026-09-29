using Chemo.Treatment;

namespace Chemo.Test
{
    public class TreatmentCatalogTest
    {
        [Fact]
        public void ItListsCategoriesAlphabetically()
        {
            string[] names = TreatmentCatalog.Categories.Select(c => c.Name).ToArray();

            Assert.Equal(names.OrderBy(name => name, StringComparer.OrdinalIgnoreCase), names, StringComparer.Ordinal);
        }

        [Fact]
        public void ItHasNoEmptyCategories()
        {
            Assert.All(TreatmentCatalog.Categories, c => Assert.NotEmpty(c.Treatments));
        }

        [Fact]
        public void ItIncludesEveryTreatmentOnce()
        {
            Type[] treatmentTypes = typeof(BaseTreatment).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BaseTreatment)) && !t.IsAbstract)
                .ToArray();
            Type[] catalogTypes = TreatmentCatalog.Categories
                .SelectMany(c => c.Treatments)
                .Select(t => t.GetType())
                .ToArray();

            Assert.Equal(treatmentTypes.OrderBy(t => t.FullName, StringComparer.Ordinal), catalogTypes.OrderBy(t => t.FullName, StringComparer.Ordinal));
        }

        [Fact]
        public void ItKeepsTreatmentsInTheirCategoryNamespace()
        {
            foreach (Category category in TreatmentCatalog.Categories)
            {
                string expectedNamespace = "Chemo.Treatment." + string.Concat(category.Name.Where(char.IsLetterOrDigit));

                Assert.All(category.Treatments, t =>
                    Assert.Equal(expectedNamespace, t.GetType().Namespace, ignoreCase: true));
            }
        }
    }
}
