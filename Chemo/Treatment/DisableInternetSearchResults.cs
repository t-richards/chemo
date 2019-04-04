using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class DisableInternetSearchResults : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static readonly string SearchKey = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Search";
        private static readonly int DesiredValue = 0;

        public bool ShouldPerformTreatment()
        {
            if (!(
                RegistryUtils.IntEquals(SearchKey, "BingSearchEnabled", 0) &&
                RegistryUtils.IntEquals(SearchKey, "AllowSearchToUseLocation", 0) &&
                RegistryUtils.IntEquals(SearchKey, "CortanaConsent", 0)
            ))
            {
                return true;
            }

            return false;
        }

        public bool PerformTreatment()
        {
            try
            {
                Registry.SetValue(SearchKey, "BingSearchEnabled", DesiredValue, RegistryValueKind.DWord);
                Registry.SetValue(SearchKey, "AllowSearchToUseLocation", DesiredValue, RegistryValueKind.DWord);
                Registry.SetValue(SearchKey, "CortanaConsent", DesiredValue, RegistryValueKind.DWord);
                logger.Log("Successfully disabled internet search results in the start menu.");
                return true;
            }
            catch (Exception ex)
            {
                logger.Log("Could not disable internet search results in the start menu: {0}", ex.Message);
            }

            return false;
        }
    }
}
