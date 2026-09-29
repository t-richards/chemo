using Chemo.Settings;

namespace Chemo.Treatment.Time
{
    internal sealed class SetClockUTC : SettingsTreatment
    {
        public override string Name()
        {
            return "Keep the hardware clock in UTC";
        }

        public override string Tooltip()
        {
            return "Makes Windows keep your PC's hardware clock in UTC instead of local time, the way Linux does. " +
                "This stops the clock from being off by hours when you switch between Windows and Linux on the same PC.";
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
