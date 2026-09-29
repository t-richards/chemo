using Chemo.Settings;
using Chemo.Utilities;

namespace Chemo.Treatment.Time
{
    internal sealed class Use24HourClock : SettingsTreatment
    {
        public override string Name => "Use a 24-hour clock";

        public override string Description => "Shows times like 17:30 instead of 5:30 PM, including the clock on the taskbar.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new LocaleFormat("Short time format", UnsafeNativeMethods.LOCALE_SSHORTTIME, "HH:mm"),
                new LocaleFormat("Long time format", UnsafeNativeMethods.LOCALE_STIMEFORMAT, "HH:mm:ss"),
            ];
        }

        protected override void OnSettingsChanged()
        {
            SettingChange.Broadcast("intl");
        }
    }
}
