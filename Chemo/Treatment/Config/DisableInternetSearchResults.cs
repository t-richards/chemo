using Microsoft.Win32;
using System;

namespace Chemo.Treatment.Config
{
    class DisableInternetSearchResults : BaseTreatment
    {
        private const string SearchKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search";
        private const int DesiredValue = 0;

        public override string Name()
        {
            return "Disable Internet Search Results";
        }

        public override string Tooltip()
        {
            return "Prevents internet junk from appearing when searching apps, files, etc. in the start menu.";
        }

        public override bool ShouldPerformTreatment()
        {
            return !(
                RegistryUtils.IntEquals(SearchKey, "BingSearchEnabled", 0) &&
                RegistryUtils.IntEquals(SearchKey, "AllowSearchToUseLocation", 0) &&
                RegistryUtils.IntEquals(SearchKey, "CortanaConsent", 0)
            );
        }

        public override bool PerformTreatment()
        {
            try
            {
                Registry.SetValue(SearchKey, "BingSearchEnabled", DesiredValue, RegistryValueKind.DWord);
                Registry.SetValue(SearchKey, "AllowSearchToUseLocation", DesiredValue, RegistryValueKind.DWord);
                Registry.SetValue(SearchKey, "CortanaConsent", DesiredValue, RegistryValueKind.DWord);
                Logger.Log("Successfully disabled internet search results in the start menu.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Could not disable internet search results in the start menu: {0}", ex.Message);
            }

            return false;
        }
    }
}
