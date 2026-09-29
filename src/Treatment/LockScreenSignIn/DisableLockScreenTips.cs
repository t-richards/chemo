using Chemo.Settings;

namespace Chemo.Treatment.LockScreenSignIn
{
    internal sealed class DisableLockScreenTips : SettingsTreatment
    {
        private const string ContentDeliveryManager = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager";

        public override string Name()
        {
            return "Turn off lock screen tips";
        }

        public override string Tooltip()
        {
            return "Stops Windows from showing fun facts, tips, and ads on the lock screen. Your lock screen picture stays the same.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // The "Get fun facts, tips, tricks, and more on your lock screen" setting.
            return
            [
                new RegistryValue(ContentDeliveryManager, "RotatingLockScreenOverlayEnabled", 0),
                new RegistryValue(ContentDeliveryManager, "SubscribedContent-338387Enabled", 0),
            ];
        }
    }
}
