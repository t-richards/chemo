using Chemo.Settings;

namespace Chemo.Treatment.Updates
{
    internal sealed class WindowsUpdateReboot : SettingsTreatment
    {
        public override string Name()
        {
            return "Disable Force-Reboot After Windows Update";
        }

        public override string Tooltip()
        {
            return "Prevents Windows from automatically rebooting after applying updates.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\WindowsUpdate\AU", "AUOptions", 2),
            ];
        }
    }
}
