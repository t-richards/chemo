using Chemo.Settings;

namespace Chemo.Treatment.LockScreenSignIn
{
    internal sealed class SkipLockScreen : SettingsTreatment
    {
        public override string Name()
        {
            return "Skip the Lock Screen";
        }

        public override string Tooltip()
        {
            return "Goes straight to the password or PIN box when you start, wake, or lock your PC, instead of a lock screen you have to click or swipe away first. " +
                "If Ctrl+Alt+Del is required, you press that instead.";
        }

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
