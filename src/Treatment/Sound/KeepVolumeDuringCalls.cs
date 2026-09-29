using Chemo.Settings;

namespace Chemo.Treatment.Sound
{
    internal sealed class KeepVolumeDuringCalls : SettingsTreatment
    {
        public override string Name()
        {
            return "Don't lower other sounds during calls";
        }

        public override string Tooltip()
        {
            return "Stops Windows from turning down music and other sounds when it thinks you're on a call, like in Teams or Discord.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // The Sound control panel's Communications tab: 0 mutes, 1 lowers by 80%, 2 lowers by 50%, and 3 does nothing.
            return
            [
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Multimedia\Audio", "UserDuckingPreference", 3),
            ];
        }
    }
}
