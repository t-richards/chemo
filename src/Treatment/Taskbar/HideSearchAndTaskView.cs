using Chemo.Settings;
using Chemo.Utilities;

namespace Chemo.Treatment.Taskbar
{
    internal sealed class HideSearchAndTaskView : SettingsTreatment
    {
        public override string Name()
        {
            return "Hide Search and Task View";
        }

        public override string Tooltip()
        {
            return "Removes the search box and the Task View button from the taskbar. You can still search by opening Start and typing, " +
                "and open Task View with Windows+Tab.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                // 0 is Hide in Settings > Personalization > Taskbar > Search.
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "SearchboxTaskbarMode", 0),
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowTaskViewButton", 0),
            ];
        }

        protected override void OnSettingsChanged()
        {
            SettingChange.Broadcast("TraySettings");
        }
    }
}
