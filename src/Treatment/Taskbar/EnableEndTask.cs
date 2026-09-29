using Chemo.Settings;

namespace Chemo.Treatment.Taskbar
{
    internal sealed class EnableEndTask : SettingsTreatment
    {
        public override string Name => "Add End task to the taskbar";

        public override string Description => "Adds End task to the menu when you right-click an app on the taskbar, so you can close a frozen app without opening Task Manager.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\TaskbarDeveloperSettings", "TaskbarEndTask", 1),
            ];
        }
    }
}
