using Chemo.Settings;

namespace Chemo.Treatment.Devices
{
    internal sealed class DisableWpbt : SettingsTreatment
    {
        public override string Name()
        {
            return "Block PC Maker Software From Firmware";
        }

        public override string Tooltip()
        {
            return "Stops your PC's firmware from installing the manufacturer's software every time Windows starts (Windows Platform Binary Table).";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager", "DisableWpbtExecution", 1),
            ];
        }
    }
}
