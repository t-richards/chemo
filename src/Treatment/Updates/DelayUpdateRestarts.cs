using Chemo.Settings;

namespace Chemo.Treatment.Updates
{
    internal sealed class DelayUpdateRestarts : SettingsTreatment
    {
        private const string WindowsUpdatePolicies = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate";

        public override string Name()
        {
            return "Delay restarts after updates";
        }

        public override string Tooltip()
        {
            return "Stops Windows from restarting on its own after updates for as long as it allows. Updates still download and install right " +
                "away, and Windows reminds you to restart when it suits you. It only forces a restart 30 days after finding an update, or 7 " +
                "days after installing it if that's later, and warns you 15 minutes before. Windows Home ignores this.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // "Specify deadline for automatic updates and restarts", Microsoft's way to control restarts on Windows 11, at its
            // longest deadline and grace period, with no automatic restarts before the deadline. The older "No auto-restart with
            // logged on users" policy isn't reliable on Windows 11.
            return
            [
                // Monthly quality updates
                new RegistryValue(WindowsUpdatePolicies, "SetComplianceDeadlineForQU", 1),
                new RegistryValue(WindowsUpdatePolicies, "ConfigureDeadlineForQualityUpdates", 30),
                new RegistryValue(WindowsUpdatePolicies, "ConfigureDeadlineGracePeriod", 7),
                new RegistryValue(WindowsUpdatePolicies, "ConfigureDeadlineNoAutoRebootForQualityUpdates", 1),

                // Yearly feature updates
                new RegistryValue(WindowsUpdatePolicies, "SetComplianceDeadlineForFU", 1),
                new RegistryValue(WindowsUpdatePolicies, "ConfigureDeadlineForFeatureUpdates", 30),
                new RegistryValue(WindowsUpdatePolicies, "ConfigureDeadlineGracePeriodForFeatureUpdates", 7),
                new RegistryValue(WindowsUpdatePolicies, "ConfigureDeadlineNoAutoRebootForFeatureUpdates", 1),
            ];
        }
    }
}
