using Chemo.Settings;
using Chemo.Utilities;

namespace Chemo.Treatment.Taskbar
{
    class AlignTaskbarLeft : SettingsTreatment
    {
        public override string Name()
        {
            return "Align Taskbar to the Left";
        }

        public override string Tooltip()
        {
            return "Moves the Start button and app icons to the left edge of the taskbar, where they were before Windows 11.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarAl", 0),
            };
        }

        protected override void OnSettingsChanged()
        {
            SettingChange.Broadcast("TraySettings");
        }
    }
}
