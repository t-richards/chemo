using Chemo.Settings;

namespace Chemo.Treatment.Devices
{
    internal sealed class DisableWpbt : SettingsTreatment
    {
        public override string Name()
        {
            return "Block apps hidden in your PC's firmware";
        }

        public override string Tooltip()
        {
            return "Stops your PC's firmware from installing the manufacturer's software every time Windows starts. " +
                "Some PC makers use this to put their apps back after you remove them.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager", "DisableWpbtExecution", 1),
            ];
        }
    }
}
