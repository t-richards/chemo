using Chemo.Settings;
using Chemo.Utilities;

namespace Chemo.Treatment.Taskbar
{
    internal sealed class AlignTaskbarLeft : SettingsTreatment
    {
        public override string Name => "Move taskbar icons to the left";

        public override string Description => "Moves the Start button and app icons to the left end of the taskbar, where they were before Windows 11.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarAl", 0),
            ];
        }

        protected override void OnSettingsChanged()
        {
            SettingChange.Broadcast("TraySettings");
        }
    }
}
