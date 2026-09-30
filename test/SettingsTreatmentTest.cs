using Chemo.Settings;
using Chemo.Treatment;

namespace Chemo.Test
{
    public class SettingsTreatmentTest
    {
        private sealed class FakeSetting : ISetting
        {
            public bool Applied { get; set; }

            public bool Fails { get; set; }

            public bool IsApplied()
            {
                return Applied;
            }

            public void Apply()
            {
                if (Fails)
                {
                    throw new InvalidOperationException("Broken");
                }

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

            Assert.True(treatment.NeedsApplying());
            Assert.True(treatment.Apply());
            Assert.True(missing.Applied);
            Assert.True(treatment.Changed);
            Assert.Equal(1, treatment.ChangeNotifications);
            Assert.False(treatment.NeedsApplying());
        }

        [Fact]
        public void ItDoesNotNotifyWhenNothingChanged()
        {
            FakeTreatment treatment = new(new FakeSetting { Applied = true });

            Assert.False(treatment.NeedsApplying());
            Assert.True(treatment.Apply());
            Assert.False(treatment.Changed);
            Assert.Equal(0, treatment.ChangeNotifications);
        }

        [Fact]
        public void ItOnlyReportsChangesFromTheLastApply()
        {
            FakeTreatment treatment = new(new FakeSetting());

            Assert.True(treatment.Apply());
            Assert.True(treatment.Changed);
            Assert.True(treatment.Apply());
            Assert.False(treatment.Changed);
        }

        [Fact]
        public void ItKeepsGoingPastASettingThatFails()
        {
            FakeSetting broken = new() { Fails = true };
            FakeSetting next = new();
            FakeTreatment treatment = new(broken, next);

            Assert.False(treatment.Apply());
            Assert.True(next.Applied);
            Assert.Contains("Could not set", treatment.Logger.ToString(), StringComparison.Ordinal);
            Assert.True(treatment.NeedsApplying());
        }
    }
}
