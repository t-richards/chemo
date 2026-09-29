using Chemo.Settings;

namespace Chemo.Treatment.Start
{
    internal sealed class SuggestedApps : SettingsTreatment
    {
        private const string ContentDeliveryManager = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager";

        public override string Name()
        {
            return "Turn Off App Recommendations";
        }

        public override string Tooltip()
        {
            return "Stops the Start menu from recommending apps, tips, and shortcuts, and stops Windows from quietly installing promoted apps.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                // Consumer features policy. Only Enterprise and Education honor it; the values below cover Pro.
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableWindowsConsumerFeatures", 1),

                // Start menu suggestions, including "Show recommendations for tips, shortcuts, new apps, and more"
                new RegistryValue(ContentDeliveryManager, "SystemPaneSuggestionsEnabled", 0),
                new RegistryValue(ContentDeliveryManager, "SubscribedContent-338388Enabled", 0),
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "Start_IrisRecommendations", 0),

                // Promoted apps that install themselves
                new RegistryValue(ContentDeliveryManager, "SilentInstalledAppsEnabled", 0),
                new RegistryValue(ContentDeliveryManager, "PreInstalledAppsEnabled", 0),
                new RegistryValue(ContentDeliveryManager, "OemPreInstalledAppsEnabled", 0),
            ];
        }
    }
}
