using Microsoft.Win32;
using System;
using System.Threading.Tasks;

namespace Chemo.Treatment.Config
{
    class SuggestedApps : BaseTreatment
    {
        private const string CloudContent = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Cloud Content";
        private const int DesiredValue = 1;

        public override string Name()
        {
            return "Turn Off App Recommendations";
        }

        public override string Tooltip()
        {
            return @"Prevents 'recommended' applications from displaying on the start menu.";
        }

        public override Task<bool> ShouldPerformTreatment()
        {
            return Task.FromResult(
                !RegistryUtils.IntEquals(CloudContent, "DisableWindowsConsumerFeatures", DesiredValue)
            );
        }

        public override Task<bool> PerformTreatment()
        {
            try
            {
                Registry.SetValue(CloudContent, "DisableWindowsConsumerFeatures", DesiredValue, RegistryValueKind.DWord);
                Logger.Log("Successfully turned off suggested apps.");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Logger.Log("Could not turn off suggested apps: {0}", ex.Message);
            }

            return Task.FromResult(false);
        }
    }
}
