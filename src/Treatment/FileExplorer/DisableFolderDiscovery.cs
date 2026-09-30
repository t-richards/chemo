using Chemo.Settings;
using Chemo.Utilities;
using Microsoft.Win32;

namespace Chemo.Treatment.FileExplorer
{
    internal sealed class DisableFolderDiscovery : SettingsTreatment
    {
        public override string Name => "Use the same view for every folder";

        public override string Description =>
            "Stops File Explorer from guessing what's in each folder, which makes big folders slow to open. " +
            "Every folder uses the same general view, and any views you've set up are reset. Chemo restarts File Explorer " +
            "to finish, which closes open folder windows.";

        // Folders File Explorer has already opened can keep their old view until it restarts.
        public override bool NeedsExplorerRestart => true;

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new GenericFolderViews(),
            ];
        }

        private sealed class GenericFolderViews : ISetting
        {
            private const string ShellKey = @"Software\Classes\Local Settings\Software\Microsoft\Windows\Shell";
            private const string AllFoldersKey = @"HKEY_CURRENT_USER\" + ShellKey + @"\Bags\AllFolders\Shell";

            public bool IsApplied()
            {
                return RegistryUtils.StringEquals(AllFoldersKey, "FolderType", "NotSpecified");
            }

            public void Apply()
            {
                // Forget the folder types Explorer already detected, then make every folder generic.
                Registry.CurrentUser.DeleteSubKeyTree(ShellKey + @"\Bags", false);
                Registry.CurrentUser.DeleteSubKeyTree(ShellKey + @"\BagMRU", false);
                Registry.SetValue(AllFoldersKey, "FolderType", "NotSpecified", RegistryValueKind.String);
            }

            public override string ToString()
            {
                return "Every folder uses the generic view";
            }
        }
    }
}
