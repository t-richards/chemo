using Chemo.Settings;

namespace Chemo.Treatment.LockScreenSignIn
{
    internal sealed class SkipLockScreen : SettingsTreatment
    {
        public override string Name => "Skip the lock screen";

        public override string Description =>
            "Goes straight to the password or PIN box when you start, wake, or lock your PC, without a lock " +
            "screen to click or swipe away first. If Ctrl+Alt+Del is required, you press that instead.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                // "Do not display the lock screen"
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "NoLockScreen", 1),
            ];
        }
    }
}
