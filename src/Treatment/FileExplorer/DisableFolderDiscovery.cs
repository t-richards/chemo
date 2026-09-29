using Chemo.Settings;
using Chemo.Utilities;
using Microsoft.Win32;

namespace Chemo.Treatment.FileExplorer
{
    class DisableFolderDiscovery : SettingsTreatment
    {
        public override string Name()
        {
            return "Disable Automatic Folder Type Discovery";
        }

        public override string Tooltip()
        {
            return "Stops File Explorer from guessing each folder's type from its contents, which slows down browsing large folders. " +
                "Every folder uses the same generic view, and saved folder views and grouping are reset. Sign out to finish.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new GenericFolderViews(),
            };
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
