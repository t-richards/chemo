using Chemo.Settings;

namespace Chemo.Treatment.Devices
{
    internal sealed class PreventDeviceCompanionApps : SettingsTreatment
    {
        public override string Name()
        {
            return "Stop devices from installing their own apps";
        }

        public override string Tooltip()
        {
            return "Stops Windows from downloading the maker's app and info when you plug in a device, like a mouse, keyboard, or monitor.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Device Metadata", "PreventDeviceMetadataFromNetwork", 1),
            ];
        }
    }
}
