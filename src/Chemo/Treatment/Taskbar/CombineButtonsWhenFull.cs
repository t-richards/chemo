using Chemo.Settings;
using System.Collections.Generic;

namespace Chemo.Treatment.Taskbar
{
    class CombineButtonsWhenFull : SettingsTreatment
    {
        private const string Advanced = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";

        public override string Name()
        {
            return "Combine Taskbar Buttons Only When Full";
        }

        public override string Tooltip()
        {
            return "Shows each open window as its own labeled taskbar button, and only groups them when the taskbar runs out of room.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // 0 always combines, 1 combines when the taskbar is full, and 2 never combines.
            return new ISetting[]
            {
                new RegistryValue(Advanced, "TaskbarGlomLevel", 1),
                new RegistryValue(Advanced, "MMTaskbarGlomLevel", 1),
            };
        }

        protected override void OnSettingsChanged()
        {
            SettingChange.Broadcast("TraySettings");
        }
    }
}
