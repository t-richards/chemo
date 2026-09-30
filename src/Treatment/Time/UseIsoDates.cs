using Chemo.Settings;
using Chemo.Utilities;

namespace Chemo.Treatment.Time
{
    internal sealed class UseIsoDates : SettingsTreatment
    {
        public override string Name => "Use year-month-day dates";

        public override string Description => "Shows dates like 2026-09-28, which mean the same thing in every country and sort in order, including on the taskbar.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new LocaleFormat("Short date format", UnsafeNativeMethods.LOCALE_SSHORTDATE, "yyyy-MM-dd"),
            ];
        }

        protected override void OnSettingsChanged()
        {
            SettingChange.Broadcast("intl");
        }
    }
}
