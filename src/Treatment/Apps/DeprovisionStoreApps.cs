using Chemo.Data;
using Microsoft.Dism;

namespace Chemo.Treatment.Apps
{
    internal sealed class DeprovisionStoreApps : BaseTreatment
    {
        public override string Name()
        {
            return "Deprovision Windows Store Apps";
        }

        public override string Tooltip()
        {
            return "Deprovisions the same apps so they don't return when a new user is created or a feature update is applied.";
        }

        public override bool ShouldPerformTreatment()
        {
            int packageCount = 0;

            // A DISM failure is reported as an analysis error rather than as nothing to deprovision.
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismAppxPackageCollection dismAppxPackages = DismApi.GetProvisionedAppxPackages(session);
                foreach (DismAppxPackage package in dismAppxPackages)
                {
                    if (StoreApps.ShouldRemove(package.DisplayName))
                    {
                        Logger.Log("Would deprovision {0}", package.DisplayName);
                        packageCount += 1;
                    }
                    else
                    {
                        Logger.Log("Not deprovisioning {0}", package.DisplayName);
                    }
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
            int removedPackageCount = 0;

            try
            {
                using DismSession session = DismApi.OpenOnlineSession();
                DismAppxPackageCollection dismAppxPackages = DismApi.GetProvisionedAppxPackages(session);
                foreach (DismAppxPackage package in dismAppxPackages)
                {
                    try
                    {
                        if (StoreApps.ShouldRemove(package.DisplayName))
                        {
                            DismApi.RemoveProvisionedAppxPackage(session, package.PackageName);
                            Logger.Log("Successfully deprovisioned {0}", package.DisplayName);
                            removedPackageCount += 1;
                        }
                        else
                        {
                            Logger.Log("Not deprovisioning {0}", package.DisplayName);
                        }

                    }
                    catch (DismRebootRequiredException ex)
                    {
                        Logger.Log("Successfully deprovisioned {0}: {1}", package.DisplayName, ex.Message);
                        removedPackageCount += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("An error occurred while deprovisioning packages: {0}", ex.Message);
                return false;
            }

            if (removedPackageCount <= 0)
            {
                Logger.Log("No Windows Store packages were deprovisioned.");
            }

            return true;
        }
    }
}
