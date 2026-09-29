using Chemo.Settings;
using System.Collections.Generic;

namespace Chemo.Treatment.Appearance
{
    internal class DarkMode : SettingsTreatment
    {
        public override string Name()
        {
            return "Dark Mode";
        }

        public override string Tooltip()
        {
            return "Sets windows and applications to use the dark mode theme.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new RegistryValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 0),
            };
        }
    }
}
