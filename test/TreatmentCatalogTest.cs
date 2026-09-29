using Chemo.Settings;
using Chemo.Treatment;
using System.Reflection;

namespace Chemo.Test
{
    public class TreatmentCatalogTest
    {
        private static IEnumerable<SettingsTreatment> Treatments => TreatmentCatalog.Categories.SelectMany(c => c.Treatments);

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
            Type[] treatmentTypes = typeof(SettingsTreatment).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(SettingsTreatment)) && !t.IsAbstract)
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

        [Fact]
        public void ItGivesEveryTreatmentADifferentName()
        {
            Assert.Distinct(Treatments.Select(t => t.Name), StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void ItNamesTreatmentsWithLabelsAndDescribesThemInSentences()
        {
            Assert.All(Treatments, t =>
            {
                Assert.False(string.IsNullOrWhiteSpace(t.Name));
                Assert.False(t.Name.EndsWith(".", StringComparison.Ordinal), $"{t.Name} ends with a period.");
                Assert.EndsWith(".", t.Description, StringComparison.Ordinal);
                Assert.DoesNotContain("  ", t.Description, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void ItDescribesEverySetting()
        {
            // The details pane logs each setting by its description, which shouldn't fall back to the type's name.
            MethodInfo settings = typeof(SettingsTreatment).GetMethod("Settings", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.All(Treatments, t =>
            {
                ISetting[] treatmentSettings = ((IEnumerable<ISetting>)settings.Invoke(t, null)).ToArray();

                Assert.NotEmpty(treatmentSettings);
                Assert.All(treatmentSettings, s => Assert.NotEqual(s.GetType().ToString(), s.ToString(), StringComparer.Ordinal));
            });
        }

        [Fact]
        public void ItMatchesTheTreatmentsDoc()
        {
            // treatments.md has a heading for each category with a heading for each of its treatments under it, in the
            // same order as the app. Anything else there is free text.
            List<string> expected = [];
            foreach (Category category in TreatmentCatalog.Categories)
            {
                expected.Add($"## {category.Name}");
                expected.AddRange(category.Treatments.Select(t => $"### {t.Name}"));
            }

            List<string> actual = File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "treatments.md"))
                .Where(line => line.StartsWith("## ", StringComparison.Ordinal) || line.StartsWith("### ", StringComparison.Ordinal))
                .ToList();

            Assert.Equal(expected, actual);
        }
    }
}
