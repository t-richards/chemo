using Chemo.Settings;
using Chemo.Utilities;
using System.Collections.Generic;

namespace Chemo.Treatment.Time
{
    class UseIsoDates : SettingsTreatment
    {
        public override string Name()
        {
            return "Use Year-Month-Day Dates";
        }

        public override string Tooltip()
        {
            return "Shows dates like 2026-09-28, which read the same in every country and sort in order, including on the taskbar.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new LocaleFormat("Short date format", UnsafeNativeMethods.LOCALE_SSHORTDATE, "yyyy-MM-dd"),
            };
        }

        protected override void OnSettingsChanged()
        {
            SettingChange.Broadcast("intl");
        }
    }
}
