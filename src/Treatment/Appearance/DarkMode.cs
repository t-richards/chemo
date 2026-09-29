using Chemo.Settings;
using Chemo.Utilities;

namespace Chemo.Treatment.Appearance
{
    internal sealed class DarkMode : SettingsTreatment
    {
        private const string Personalize = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        public override string Name()
        {
            return "Use dark mode";
        }

        public override string Tooltip()
        {
            return "Switches Windows and your apps to dark mode, the same as picking Dark in Settings > Personalization > Colors.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // The Settings app's single mode choice sets both of these; its Custom mode sets them separately.
            return
            [
                new RegistryValue(Personalize, "SystemUsesLightTheme", 0),
                new RegistryValue(Personalize, "AppsUseLightTheme", 0),
            ];
        }

        protected override void OnSettingsChanged()
        {
            SettingChange.Broadcast("ImmersiveColorSet");
            Logger.Log("Notified open windows of the theme change.");
        }
    }
}
