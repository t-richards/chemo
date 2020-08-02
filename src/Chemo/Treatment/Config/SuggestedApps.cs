using Microsoft.Win32;
using System;

namespace Chemo.Treatment.Config
{
    class SuggestedApps : BaseTreatment
    {
        // Tiles
        private const string CloudContent = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Cloud Content";
        private const string ConsumerFeaturesKey = "DisableWindowsConsumerFeatures";
        private const int ConsumerFeaturesValue = 1;

        // Suggestions v0
        private const string ContentDeliveryManager = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager";
        private const string SuggestionsKey = "SystemPaneSuggestionsEnabled";
        private const int SuggestionsValue = 0;

        // Suggestions v1
        private const string SubscribedContent = "SubscribedContent-338388Enabled";
        private const int SubscribedContentValue = 0;

        public override string Name()
        {
            return "Turn Off App Recommendations";
        }

        public override string Tooltip()
        {
            return @"Prevents 'recommended' applications from displaying on the start menu.";
        }

        public override bool ShouldPerformTreatment()
        {
            return !(
                RegistryUtils.IntEquals(CloudContent, ConsumerFeaturesKey, ConsumerFeaturesValue) &&
                RegistryUtils.IntEquals(ContentDeliveryManager, SuggestionsKey, SuggestionsValue) &&
                RegistryUtils.IntEquals(ContentDeliveryManager, SubscribedContent, SubscribedContentValue)
            );
        }

        public override bool PerformTreatment()
        {
            try
            {
                Registry.SetValue(CloudContent, ConsumerFeaturesKey, ConsumerFeaturesValue, RegistryValueKind.DWord);
                Registry.SetValue(ContentDeliveryManager, SuggestionsKey, SuggestionsValue, RegistryValueKind.DWord);
                Registry.SetValue(ContentDeliveryManager, SubscribedContent, SubscribedContentValue, RegistryValueKind.DWord);
                Logger.Log("Successfully turned off suggested apps.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Could not turn off suggested apps: {0}", ex.Message);
            }

            return false;
        }
    }
}
