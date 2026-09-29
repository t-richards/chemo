using Chemo.Settings;

namespace Chemo.Treatment.Start
{
    internal sealed class DisableInternetSearchResults : SettingsTreatment
    {
        private const string SearchKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search";

        public override string Name()
        {
            return "Disable Internet Search Results";
        }

        public override string Tooltip()
        {
            return "Prevents internet junk from appearing when searching apps, files, etc. in the start menu.";
        }

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
