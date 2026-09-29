using Chemo.Settings;

namespace Chemo.Treatment.LockScreenSignIn
{
    internal sealed class RequireCtrlAltDel : SettingsTreatment
    {
        public override string Name()
        {
            return "Require Ctrl+Alt+Del at Sign In";
        }

        public override string Tooltip()
        {
            return "Requires the user to press Ctrl+Alt+Del at the sign in screen for security reasons.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "DisableCAD", 0),
            ];
        }
    }
}
