using Chemo.Settings;

namespace Chemo.Treatment.Devices
{
    internal sealed class PreventDeviceCompanionApps : SettingsTreatment
    {
        public override string Name()
        {
            return "Prevent Device Companion Apps";
        }

        public override string Tooltip()
        {
            return "Stops Windows from downloading manufacturer apps and info when you plug in a device, such as a monitor or mouse.";
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
