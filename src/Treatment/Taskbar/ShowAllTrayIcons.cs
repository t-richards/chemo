using Chemo.Settings;
using Microsoft.Win32;

namespace Chemo.Treatment.Taskbar
{
    internal sealed class ShowAllTrayIcons : SettingsTreatment
    {
        public override string Name => "Show all tray icons";

        public override string Description =>
            "Shows every app's icon next to the clock instead of hiding some behind the ^ arrow. " +
            "Apps you install later start out hidden, so run this again to show them.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new AllIconsPromoted(),
            ];
        }

        /// <summary>
        /// Windows 11 keeps a key for each app that has shown a tray icon, and IsPromoted is the switch for it
        /// under Settings > Personalization > Taskbar > Other system tray icons.
        /// </summary>
        private sealed class AllIconsPromoted : ISetting
        {
            private const string NotifyIconSettings = @"Control Panel\NotifyIconSettings";

            public bool IsApplied()
            {
                using RegistryKey icons = Registry.CurrentUser.OpenSubKey(NotifyIconSettings);
                if (icons == null)
                {
                    return true;
                }

                foreach (string name in icons.GetSubKeyNames())
                {
                    using RegistryKey icon = icons.OpenSubKey(name);
                    if (!(icon?.GetValue("IsPromoted") is int promoted && promoted == 1))
                    {
                        return false;
                    }
                }

                return true;
            }

            public void Apply()
            {
                using RegistryKey icons = Registry.CurrentUser.OpenSubKey(NotifyIconSettings);
                foreach (string name in icons.GetSubKeyNames())
                {
                    using RegistryKey icon = icons.OpenSubKey(name, true);
                    icon.SetValue("IsPromoted", 1, RegistryValueKind.DWord);
                }
            }

            public override string ToString()
            {
                return "Every tray icon is shown on the taskbar";
            }
        }
    }
}
