using Chemo.Settings;
using System.Collections.Generic;

namespace Chemo.Treatment.Start
{
    class HideRecentItems : SettingsTreatment
    {
        private const string ExplorerPolicies = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Explorer";

        public override string Name()
        {
            return "Hide Recent Items in Start";
        }

        public override string Tooltip()
        {
            return "Stops Start from listing recently added apps, your most used apps, and files you opened recently. " +
                "Recent files also disappear from File Explorer's Home and from jump lists. Restart to finish.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                // "Show recommended files in Start, recent files in File Explorer, and items in Jump Lists"
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "Start_TrackDocs", 0),

                // For every user. 2 hides the Most used list.
                new RegistryValue(ExplorerPolicies, "HideRecentlyAddedApps", 1),
                new RegistryValue(ExplorerPolicies, "ShowOrHideMostUsedApps", 2),
            };
        }
    }
}
