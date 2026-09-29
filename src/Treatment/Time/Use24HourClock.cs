using Chemo.Settings;
using Chemo.Utilities;
using System.Collections.Generic;

namespace Chemo.Treatment.Time
{
    class Use24HourClock : SettingsTreatment
    {
        public override string Name()
        {
            return "Use a 24-Hour Clock";
        }

        public override string Tooltip()
        {
            return "Shows times like 17:30 instead of 5:30 PM, including the clock on the taskbar.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new LocaleFormat("Short time format", UnsafeNativeMethods.LOCALE_SSHORTTIME, "HH:mm"),
                new LocaleFormat("Long time format", UnsafeNativeMethods.LOCALE_STIMEFORMAT, "HH:mm:ss"),
            };
        }

        protected override void OnSettingsChanged()
        {
            SettingChange.Broadcast("intl");
        }
    }
}
