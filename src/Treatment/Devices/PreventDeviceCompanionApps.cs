using Chemo.Settings;
using System.Collections.Generic;

namespace Chemo.Treatment.Devices
{
    class PreventDeviceCompanionApps : SettingsTreatment
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
            return new ISetting[]
            {
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Device Metadata", "PreventDeviceMetadataFromNetwork", 1),
            };
        }
    }
}
