using Chemo.Data;
using Windows.ApplicationModel;

namespace Chemo.Treatment.Apps
{
    class RemoveStoreApps : BaseTreatment
    {
        public override string Name()
        {
            return "Remove Windows Store Apps";
        }

        public override string Tooltip()
        {
            return "Removes pre-installed ads, upsells, and discontinued apps such as News, Teams, and Solitaire for all users.";
        }

        public override bool ShouldPerformTreatment()
        {
            int packageCount = 0;

            foreach (Package package in AppPackages.FindForAllUsers())
            {
                if (StoreApps.ShouldRemove(package.Id.Name))
                {
                    Logger.Log("Would remove {0}", package.Id.Name);
                    packageCount += 1;
                }
                else
                {
                    Logger.Log("Not removing {0}", package.Id.Name);
                }
            }

            if (packageCount > 0)
            {
                return true;
            }

            return false;
        }

        public override bool PerformTreatment()
        {
            int packageCount = 0;

            foreach (Package package in AppPackages.FindForAllUsers())
            {
                if (StoreApps.ShouldRemove(package.Id.Name))
                {
                    AppPackages.RemoveForAllUsers(package, Logger);
                    packageCount += 1;
                }
                else
                {
                    Logger.Log("Not removing {0}", package.Id.Name);
                }
            }

            if (packageCount <= 0)
            {
                Logger.Log("No Windows Store applications were uninstalled.");
            }
            Logger.Log("");

            return true;
        }
    }
}
