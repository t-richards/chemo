using Chemo.Settings;

namespace Chemo.Treatment.FileExplorer
{
    internal sealed class TurnOnAdvancedSettings : SettingsTreatment
    {
        private const string Advanced = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";

        public override string Name => "Show file extensions, hidden files, and more";

        public override string Description =>
            "Shows file extensions, hidden files, the full folder path in the title bar, and empty " +
            "drives, and adds Run as different user to Start. Sign out to finish.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(Advanced, "HideFileExt", 0),

                // 1 shows hidden files. Protected operating system files, like the desktop.ini in every folder,
                // stay hidden (ShowSuperHidden).
                new RegistryValue(Advanced, "Hidden", 1),

                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CabinetState", "FullPath", 1),
                new RegistryValue(Advanced, "HideDrivesWithNoMedia", 0),

                // Settings sets this through the policy editor, since it's a policy.
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "ShowRunAsDifferentUserInStart", 1),
            ];
        }
    }
}
