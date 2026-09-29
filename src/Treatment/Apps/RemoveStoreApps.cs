using Chemo.Data;
using Chemo.Settings;

namespace Chemo.Treatment.Apps
{
    internal sealed class RemoveStoreApps : SettingsTreatment
    {
        public override string Name()
        {
            return "Remove Windows Store Apps";
        }

        public override string Tooltip()
        {
            return "Removes pre-installed ads, upsells, and discontinued apps such as News, Teams, and Solitaire for all users.";
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
