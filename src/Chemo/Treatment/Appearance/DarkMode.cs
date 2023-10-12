using Microsoft.Win32;
using System;

namespace Chemo.Treatment.Appearance
{
    internal class DarkMode : BaseTreatment
    {
        private const string HKCUPersonalize = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string AppsUseLightTheme = "AppsUseLightTheme";
        private const int LightThemeValue = 0;

        public override string Name()
        {
            return "Dark Mode";
        }

        public override bool PerformTreatment()
        {
            try
            {
                Registry.SetValue(HKCUPersonalize, AppsUseLightTheme, LightThemeValue, RegistryValueKind.DWord);
                Logger.Log("Applied dark theme to windows and apps.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Could not apply dark mode: {0}", ex.Message);
                return false;
            }
        }

        public override bool ShouldPerformTreatment()
        {
            return RegistryUtils.IntEquals(HKCUPersonalize, AppsUseLightTheme, LightThemeValue);
        }

        public override string Tooltip()
        {
            return "Sets windows and applications to use the dark mode theme.";
        }
    }
}
