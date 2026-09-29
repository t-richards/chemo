using Chemo.Settings;
using System.Collections.Generic;

namespace Chemo.Treatment.Start
{
    class SuggestedApps : SettingsTreatment
    {
        private const string ContentDeliveryManager = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager";

        public override string Name()
        {
            return "Turn Off App Recommendations";
        }

        public override string Tooltip()
        {
            return "Stops Windows from recommending apps in the Start menu and from quietly installing promoted apps.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                // Consumer features policy. Only Enterprise and Education honor it; the values below cover Pro.
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableWindowsConsumerFeatures", 1),

                // Start menu suggestions
                new RegistryValue(ContentDeliveryManager, "SystemPaneSuggestionsEnabled", 0),
                new RegistryValue(ContentDeliveryManager, "SubscribedContent-338388Enabled", 0),

                // Promoted apps that install themselves
                new RegistryValue(ContentDeliveryManager, "SilentInstalledAppsEnabled", 0),
                new RegistryValue(ContentDeliveryManager, "PreInstalledAppsEnabled", 0),
                new RegistryValue(ContentDeliveryManager, "OemPreInstalledAppsEnabled", 0),
            };
        }
    }
}
