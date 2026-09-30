using Chemo.Settings;

namespace Chemo.Treatment.Start
{
    internal sealed class HideRecentItems : SettingsTreatment
    {
        private const string ExplorerPolicies = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Explorer";

        public override string Name => "Hide recent items in Start";

        public override string Description =>
            "Hides the Recent section in Start, and stops Start from listing apps you recently installed, apps you use most, " +
            "and files you recently opened. Recent files also disappear from File Explorer's Home and from jump lists.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                // Settings' switch for the whole Recent section, which Windows 11 added in its August 2026 update
                // (KB5120998). Earlier versions ignore it.
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Start", "ShowRecentSection", 0),

                // "Show recommended files in Start, recent files in File Explorer, and items in Jump Lists"
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "Start_TrackDocs", 0),

                // For every user. 2 hides the Most used list.
                new RegistryValue(ExplorerPolicies, "HideRecentlyAddedApps", 1),
                new RegistryValue(ExplorerPolicies, "ShowOrHideMostUsedApps", 2),
            ];
        }
    }
}
