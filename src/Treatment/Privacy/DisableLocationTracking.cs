using Chemo.Settings;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Chemo.Treatment.Privacy
{
    class DisableLocationTracking : SettingsTreatment
    {
        public override string Name()
        {
            return "Disable Location Tracking";
        }

        public override string Tooltip()
        {
            return "Turns off location services for every app and user. Apps like Weather, Find my device, and setting the time zone automatically won't be able to find your location.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location", "Value", "Deny", RegistryValueKind.String),
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Sensor\Overrides\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}", "SensorPermissionState", 0),
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SYSTEM\Maps", "AutoUpdateEnabled", 0),
                new ServiceStartup("lfsvc", ServiceStartType.Disabled),
            };
        }
    }
}
