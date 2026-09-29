using Chemo.Settings;
using Chemo.Utilities;
using System.Collections.Generic;

namespace Chemo.Treatment.Appearance
{
    internal class DarkMode : SettingsTreatment
    {
        private const string Personalize = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        public override string Name()
        {
            return "Dark Mode";
        }

        public override string Tooltip()
        {
            return "Switches Windows (the taskbar and Start menu) and apps to dark mode, like choosing Dark in Settings > Personalization > Colors.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // The Settings app's single mode choice sets both of these; its Custom mode sets them separately.
            return new ISetting[]
            {
                new RegistryValue(Personalize, "SystemUsesLightTheme", 0),
                new RegistryValue(Personalize, "AppsUseLightTheme", 0),
            };
        }

        protected override void OnSettingsChanged()
        {
            SettingChange.Broadcast("ImmersiveColorSet");
            Logger.Log("Notified open windows of the theme change.");
        }
    }
}
