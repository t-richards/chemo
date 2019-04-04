using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class SuggestedApps : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static readonly string policiesWindows = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Cloud Content";

        public void PerformTreatment()
        {
            try
            {
                Registry.SetValue(policiesWindows, "DisableWindowsConsumerFeatures", 1, RegistryValueKind.DWord);
                logger.Log("Successfully turned off suggested apps.");
            }
            catch (Exception ex)
            {
                logger.Log("Could not turn off suggested apps: {0}", ex.Message);
            }
        }
    }
}
