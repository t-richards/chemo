using Chemo.Data;
using Chemo.Settings;

namespace Chemo.Treatment.Apps
{
    internal sealed class RemoveStoreApps : SettingsTreatment
    {
        public override string Name()
        {
            return "Remove preinstalled apps";
        }

        public override string Tooltip()
        {
            return "Uninstalls the ads, upsells, and abandoned apps Windows comes with, like News, Solitaire, and Teams, for everyone on this PC. " +
                "Useful apps like Calculator, Photos, and the Microsoft Store stay.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new AppPackagesRemoved("Pre-installed Store apps", StoreApps.ShouldRemove, Logger),
            ];
        }
    }
}
