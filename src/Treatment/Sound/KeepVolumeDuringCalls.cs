using Chemo.Settings;

namespace Chemo.Treatment.Sound
{
    class KeepVolumeDuringCalls : SettingsTreatment
    {
        public override string Name()
        {
            return "Don't Lower Other Sounds During Calls";
        }

        public override string Tooltip()
        {
            return "Stops Windows from turning down music and other sounds when it detects a call, such as in Teams or Discord.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // The Sound control panel's Communications tab: 0 mutes, 1 lowers by 80%, 2 lowers by 50%, and 3 does nothing.
            return new ISetting[]
            {
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Multimedia\Audio", "UserDuckingPreference", 3),
            };
        }
    }
}
