using Microsoft.Win32;
using System;

namespace Chemo.Treatment.Config
{
    class GameBar : BaseTreatment
    {
        private const string GameDVR = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\GameDVR";
        private const string GameConfigStore = @"HKEY_CURRENT_USER\System\GameConfigStore";
        private const int DesiredValue = 0;

        public override string Name()
        {
            return "Turn Off Game Bar";
        }

        public override string Tooltip()
        {
            return "Turns off the game bar for both apps and games.";
        }

        public override bool ShouldPerformTreatment()
        {
            return !(
                RegistryUtils.IntEquals(GameDVR, "AppCaptureEnabled", DesiredValue) &&
                RegistryUtils.IntEquals(GameConfigStore, "GameDVR_Enabled", DesiredValue)
            );
        }

        public override bool PerformTreatment()
        {
            try
            {
                Registry.SetValue(GameDVR, "AppCaptureEnabled", DesiredValue, RegistryValueKind.DWord);
                Registry.SetValue(GameConfigStore, "GameDVR_Enabled", DesiredValue, RegistryValueKind.DWord);
                Logger.Log("Successfully turned off the game bar.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Could not turn off the game bar: {0}", ex.Message);
            }

            return false;
        }
    }
}
