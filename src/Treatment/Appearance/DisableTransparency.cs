using Chemo.Settings;
using System.Collections.Generic;

namespace Chemo.Treatment.Appearance
{
    internal class DisableTransparency : SettingsTreatment
    {
        public override string Name()
        {
            return "Disable Transparency";
        }

        public override string Tooltip()
        {
            return "Disables transparency in windows and applications.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", 0),
            };
        }
    }
}
