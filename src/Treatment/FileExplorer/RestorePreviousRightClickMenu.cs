using Chemo.Settings;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Chemo.Treatment.FileExplorer
{
    class RestorePreviousRightClickMenu : SettingsTreatment
    {
        public override string Name()
        {
            return "Restore Previous Right-Click Menu";
        }

        public override string Tooltip()
        {
            return "Brings back the full right-click menu from Windows 10 in File Explorer and on the desktop, so you don't have to click 'Show more options'. Sign out to finish.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                // An empty per-user server for the Windows 11 menu's COM class stops it from loading, and Explorer
                // falls back to the previous menu. The value must be an empty string, not missing.
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32", "", "", RegistryValueKind.String),
            };
        }
    }
}
