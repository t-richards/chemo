using Chemo.Settings;
using Microsoft.Win32;

namespace Chemo.Treatment.Privacy
{
    internal sealed class DisableLocationTracking : SettingsTreatment
    {
        public override string Name()
        {
            return "Turn off location tracking";
        }

        public override string Tooltip()
        {
            return "Turns off location for every app and everyone on this PC. " +
                "Apps like Weather and Find my device, and setting your time zone automatically, won't be able to find where you are.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location", "Value", "Deny", RegistryValueKind.String),
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Sensor\Overrides\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}", "SensorPermissionState", 0),
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SYSTEM\Maps", "AutoUpdateEnabled", 0),
                new ServiceStartup("lfsvc", ServiceStartType.Disabled),
            ];
        }
    }
}
