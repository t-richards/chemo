using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class GameBar : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static readonly string gameDvr = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\GameDVR";
        private static readonly string gameConfigStore = "HKEY_CURRENT_USER\\System\\GameConfigStore";

        public void PerformTreatment()
        {
            try
            {
                Registry.SetValue(gameDvr, "AppCaptureEnabled", 0, RegistryValueKind.DWord);
                Registry.SetValue(gameConfigStore, "GameDVR_Enabled", 0, RegistryValueKind.DWord);
                logger.Log("Successfully turned off the game bar.");
            }
            catch (Exception ex)
            {
                logger.Log("Could not turn off the game bar: {0}", ex.Message);
            }
        }
    }
}
