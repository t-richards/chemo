using Chemo.Settings;

namespace Chemo.Treatment.Appearance
{
    internal sealed class DisableTransparency : SettingsTreatment
    {
        public override string Name()
        {
            return "Turn off transparency";
        }

        public override string Tooltip()
        {
            return "Makes the taskbar, Start, and window backgrounds solid instead of " +
                "see-through, the same as turning off Transparency effects in Settings.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", 0),
            ];
        }
    }
}
