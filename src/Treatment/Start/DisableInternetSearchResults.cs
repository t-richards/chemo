using Chemo.Settings;

namespace Chemo.Treatment.Start
{
    internal sealed class DisableInternetSearchResults : SettingsTreatment
    {
        private const string SearchKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search";

        public override string Name => "Turn off web results in search";

        public override string Description =>
            "Stops Start search from showing Bing web results, so searching only shows what's on your PC. " +
            "Also stops search from using your location.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(SearchKey, "BingSearchEnabled", 0),
                new RegistryValue(SearchKey, "AllowSearchToUseLocation", 0),
                new RegistryValue(SearchKey, "CortanaConsent", 0),
            ];
        }
    }
}
