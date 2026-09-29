using Chemo.Settings;

namespace Chemo.Treatment.Time
{
    internal sealed class SetClockUTC : SettingsTreatment
    {
        public override string Name()
        {
            return "Set System Clock to UTC";
        }

        public override string Tooltip()
        {
            return "Sets the system's hardware clock to Coordinated Universal Time (UTC). The Windows default is localtime.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\TimeZoneInformation", "RealTimeIsUniversal", 1),
            ];
        }
    }
}
