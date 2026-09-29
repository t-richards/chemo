using Chemo.Settings;
using Chemo.Treatment;

namespace Chemo.Test
{
    public class SettingsTreatmentTest
    {
        private sealed class FakeSetting : ISetting
        {
            public bool Applied { get; set; }

            public bool IsApplied()
            {
                return Applied;
            }

            public void Apply()
            {
                Applied = true;
            }
        }

        private sealed class FakeTreatment : SettingsTreatment
        {
            private readonly ISetting[] settings;

            public int ChangeNotifications { get; private set; }

            public FakeTreatment(params ISetting[] settings)
            {
                this.settings = settings;
            }

            public override string Name => "Fake";

            public override string Description => "Fake";

            protected override IEnumerable<ISetting> Settings()
            {
                return settings;
            }

            protected override void OnSettingsChanged()
            {
                ChangeNotifications += 1;
            }
        }

        [Fact]
        public void ItAppliesMissingSettingsAndNotifiesOnce()
        {
            FakeSetting missing = new();
            FakeTreatment treatment = new(missing, new FakeSetting(), new FakeSetting { Applied = true });

            Assert.True(treatment.ShouldPerformTreatment());
            Assert.True(treatment.PerformTreatment());
            Assert.True(missing.Applied);
            Assert.Equal(1, treatment.ChangeNotifications);
            Assert.False(treatment.ShouldPerformTreatment());
        }

        [Fact]
        public void ItDoesNotNotifyWhenNothingChanged()
        {
            FakeTreatment treatment = new(new FakeSetting { Applied = true });

            Assert.False(treatment.ShouldPerformTreatment());
            Assert.True(treatment.PerformTreatment());
            Assert.Equal(0, treatment.ChangeNotifications);
        }
    }
}
