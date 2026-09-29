using Chemo.Settings;

namespace Chemo.Treatment.Performance
{
    internal sealed class DisableBackgroundApps : SettingsTreatment
    {
        public override string Name()
        {
            return "Disable Background Apps";
        }

        public override string Tooltip()
        {
            return "Stops Microsoft Store apps from running in the background when you aren't using them. " +
                "Closed apps, such as Phone Link, can't sync or show notifications until you open them. Sign out to finish.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // Windows 11 only offers this per app in Settings. Windows 10's single "Let apps run in the
            // background" switch set both of these, and Windows 11 still honors them.
            return
            [
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications", "GlobalUserDisabled", 1),
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "BackgroundAppGlobalToggle", 0),
            ];
        }
    }
}
