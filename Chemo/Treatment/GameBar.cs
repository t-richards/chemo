using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class GameBar : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static readonly string GameDVR = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\GameDVR";
        private static readonly string GameConfigStore = "HKEY_CURRENT_USER\\System\\GameConfigStore";
        private static readonly int DesiredValue = 0;

        public bool ShouldPerformTreatment()
        {
            return true;
        }

        public bool PerformTreatment()
        {
            try
            {
                Registry.SetValue(GameDVR, "AppCaptureEnabled", 0, RegistryValueKind.DWord);
                Registry.SetValue(GameConfigStore, "GameDVR_Enabled", 0, RegistryValueKind.DWord);
                logger.Log("Successfully turned off the game bar.");
                return true;
            }
            catch (Exception ex)
            {
                logger.Log("Could not turn off the game bar: {0}", ex.Message);
            }

            return false;
        }
    }
}
