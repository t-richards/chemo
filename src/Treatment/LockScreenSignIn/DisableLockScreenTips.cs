using Chemo.Settings;

namespace Chemo.Treatment.LockScreenSignIn
{
    class DisableLockScreenTips : SettingsTreatment
    {
        private const string ContentDeliveryManager = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager";

        public override string Name()
        {
            return "Turn Off Lock Screen Tips";
        }

        public override string Tooltip()
        {
            return "Stops Windows from showing fun facts, tips, and promotions on the lock screen. The lock screen picture isn't changed.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // The "Get fun facts, tips, tricks, and more on your lock screen" setting.
            return new ISetting[]
            {
                new RegistryValue(ContentDeliveryManager, "RotatingLockScreenOverlayEnabled", 0),
                new RegistryValue(ContentDeliveryManager, "SubscribedContent-338387Enabled", 0),
            };
        }
    }
}
