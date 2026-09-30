using Chemo.Settings;
using Microsoft.Win32;

namespace Chemo.Treatment.FileExplorer
{
    internal sealed class RestorePreviousRightClickMenu : SettingsTreatment
    {
        public override string Name => "Bring back the full right-click menu";

        public override string Description =>
            "Brings back the Windows 10 right-click menu in File Explorer and on the desktop, so you don't have to click " +
            "Show more options every time. Chemo restarts File Explorer to finish, which closes open folder windows.";

        // File Explorer loads the menu once, when it starts.
        public override bool NeedsExplorerRestart => true;

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                // An empty per-user server for the Windows 11 menu's COM class stops it from loading, and Explorer
                // falls back to the previous menu. The value must be an empty string, not missing.
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32", "", "", RegistryValueKind.String),
            ];
        }
    }
}
