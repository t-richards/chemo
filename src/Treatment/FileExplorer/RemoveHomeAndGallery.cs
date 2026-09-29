using Chemo.Settings;

namespace Chemo.Treatment.FileExplorer
{
    internal sealed class RemoveHomeAndGallery : SettingsTreatment
    {
        private const string HomeClsid = @"HKEY_CURRENT_USER\Software\Classes\CLSID\{f874310e-b6b7-47dc-bc84-b9e6b38f5903}";
        private const string GalleryClsid = @"HKEY_CURRENT_USER\Software\Classes\CLSID\{e88865ea-0e1c-4e20-9aa6-edcd0212c87c}";

        public override string Name()
        {
            return "Remove Home and Gallery";
        }

        public override string Tooltip()
        {
            return "Removes Home and Gallery from the left side of File Explorer and opens File Explorer to This PC instead. Sign out to finish.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                // Unpins them from the navigation pane for this user, the same way OneDrive is unpinned.
                new RegistryValue(HomeClsid, "System.IsPinnedToNameSpaceTree", 0),
                new RegistryValue(GalleryClsid, "System.IsPinnedToNameSpaceTree", 0),

                // Otherwise File Explorer still opens to Home. 1 is This PC.
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "LaunchTo", 1),
            ];
        }
    }
}
