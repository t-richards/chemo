using Chemo.Data;
using Chemo.Utilities;

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

            foreach (AppPackage package in AppPackages.FindForAllUsers())
            {
                if (StoreApps.ShouldRemove(package.Name))
                {
                    Logger.Log("Would remove {0}", package.Name);
                    packageCount += 1;
                }
                else
                {
                    Logger.Log("Not removing {0}", package.Name);
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
            List<AppPackage> packagesToRemove = new List<AppPackage>();

            foreach (AppPackage package in AppPackages.FindForAllUsers())
            {
                if (StoreApps.ShouldRemove(package.Name))
                {
                    packagesToRemove.Add(package);
                }
                else
                {
                    Logger.Log("Not removing {0}", package.Name);
                }
            }

            if (packagesToRemove.Count <= 0)
            {
                Logger.Log("No Windows Store applications were uninstalled.");
            }

            AppPackages.RemoveForAllUsers(packagesToRemove, Logger);
            Logger.Log("");

            return true;
        }
    }
}
