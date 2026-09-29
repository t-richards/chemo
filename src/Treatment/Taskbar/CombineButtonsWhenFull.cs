using Chemo.Settings;
using Chemo.Utilities;

namespace Chemo.Treatment.Taskbar
{
    internal sealed class CombineButtonsWhenFull : SettingsTreatment
    {
        private const string Advanced = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";

        public override string Name()
        {
            return "Combine taskbar buttons only when it's full";
        }

        public override string Tooltip()
        {
            return "Shows each open window as its own labeled taskbar button, and only groups them when the taskbar runs out of room.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // 0 always combines, 1 combines when the taskbar is full, and 2 never combines.
            return
            [
                new RegistryValue(Advanced, "TaskbarGlomLevel", 1),
                new RegistryValue(Advanced, "MMTaskbarGlomLevel", 1),
            ];
        }

        protected override void OnSettingsChanged()
        {
            SettingChange.Broadcast("TraySettings");
        }
    }
}
