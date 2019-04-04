using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class DisableInternetSearchResults : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static readonly string windowsSearch = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Search";

        public void PerformTreatment()
        {
            try
            {
                Registry.SetValue(windowsSearch, "BingSearchEnabled", 0, RegistryValueKind.DWord);
                Registry.SetValue(windowsSearch, "AllowSearchToUseLocation", 0, RegistryValueKind.DWord);
                Registry.SetValue(windowsSearch, "CortanaConsent", 0, RegistryValueKind.DWord);
                logger.Log("Successfully disabled internet search results in the start menu.");
            }
            catch (Exception ex)
            {
                logger.Log("Could not disable internet search results in the start menu: {0}", ex.Message);
            }
        }
    }
}
