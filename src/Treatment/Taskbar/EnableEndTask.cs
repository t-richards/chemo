using Chemo.Settings;

namespace Chemo.Treatment.Taskbar
{
    class EnableEndTask : SettingsTreatment
    {
        public override string Name()
        {
            return "Enable End Task on Right Click";
        }

        public override string Tooltip()
        {
            return "Adds 'End task' to the menu when you right-click an app on the taskbar, so you can close a frozen app without Task Manager.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\TaskbarDeveloperSettings", "TaskbarEndTask", 1),
            };
        }
    }
}
