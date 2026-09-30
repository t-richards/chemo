using Chemo.Settings;

namespace Chemo.Treatment.FileExplorer
{
    internal sealed class TurnOnAdvancedSettings : SettingsTreatment
    {
        private const string Advanced = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";

        public override string Name => "Show file extensions, hidden files, and more";

        public override string Description =>
            "Shows file extensions, hidden files, and empty drives, and adds Run as different user to desktop apps in Start. " +
            "Folder tabs show the full path, except for special folders like Documents. Chemo restarts File Explorer to finish, " +
            "which closes open folder windows.";

        // File Explorer reads the full path setting when it starts.
        public override bool NeedsExplorerRestart => true;

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(Advanced, "HideFileExt", 0),

                // 1 shows hidden files. Protected operating system files, like the desktop.ini in every folder,
                // stay hidden (ShowSuperHidden).
                new RegistryValue(Advanced, "Hidden", 1),

                // Folder Options also sets a flag in the binary Settings value next to this one, but Windows goes by
                // this value when the two differ.
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CabinetState", "FullPath", 1),
                new RegistryValue(Advanced, "HideDrivesWithNoMedia", 0),

                // Settings sets this through the policy editor, since it's a policy.
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "ShowRunAsDifferentUserInStart", 1),
            ];
        }
    }
}
