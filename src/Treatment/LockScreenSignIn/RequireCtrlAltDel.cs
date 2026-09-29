using Chemo.Settings;

namespace Chemo.Treatment.LockScreenSignIn
{
    internal sealed class RequireCtrlAltDel : SettingsTreatment
    {
        public override string Name => "Require Ctrl+Alt+Del to sign in";

        public override string Description =>
            "Makes you press Ctrl+Alt+Del before signing in. " +
            "Only Windows can respond to that key combination, so a fake sign-in screen can't trick you into typing your password.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "DisableCAD", 0),
            ];
        }
    }
}
