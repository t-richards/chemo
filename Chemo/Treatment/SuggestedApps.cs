using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class SuggestedApps : BaseTreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static readonly string CloudContent = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Cloud Content";
        private static readonly int DesiredValue = 1;

        public override bool ShouldPerformTreatment()
        {
            return !RegistryUtils.IntEquals(CloudContent, "DisableWindowsConsumerFeatures", DesiredValue);
        }

        public override bool PerformTreatment()
        {
            try
            {
                Registry.SetValue(CloudContent, "DisableWindowsConsumerFeatures", DesiredValue, RegistryValueKind.DWord);
                logger.Log("Successfully turned off suggested apps.");
                return true;
            }
            catch (Exception ex)
            {
                logger.Log("Could not turn off suggested apps: {0}", ex.Message);
            }

            return false;
        }
    }
}
