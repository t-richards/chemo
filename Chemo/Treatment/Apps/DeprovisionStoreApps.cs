using Chemo.Data;
using Microsoft.Dism;
using System;

namespace Chemo.Treatment.Apps
{
    class DeprovisionStoreApps : BaseTreatment
    {
        public override string Name()
        {
            return "Deprovision Windows Store Apps";
        }

        public override string Tooltip()
        {
            return "Deprovisions all packages.This prevents Windows Store application from re-appearing when a new user is created, or when a feature update is applied.";
        }

        public override bool ShouldPerformTreatment()
        {
            int packageCount = 0;
            try
            {
                using (DismSession session = DismApi.OpenOnlineSession())
                {
                    DismAppxPackageCollection dismAppxPackages = DismApi.GetProvisionedAppxPackages(session);
                    foreach (var package in dismAppxPackages)
                    {
                        if (StoreApps.shouldRemove(package.DisplayName))
                        {
                            packageCount += 1;
                        }
                    }
                }
            }
            catch
            {
                return false;
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
                using (DismSession session = DismApi.OpenOnlineSession())
                {
                    DismAppxPackageCollection dismAppxPackages = DismApi.GetProvisionedAppxPackages(session);
                    foreach (var package in dismAppxPackages)
                    {
                        try
                        {
                            if (StoreApps.shouldRemove(package.DisplayName))
                            {
                                DismApi.RemoveProvisionedAppxPackage(session, package.PackageName);
                                logger.Log("Successfully deprovisioned {0}", package.DisplayName);
                                removedPackageCount += 1;
                            }
                            else
                            {
                                logger.Log("Not deprovisioning {0}", package.DisplayName);
                            }

                        }
                        catch (DismRebootRequiredException ex)
                        {
                            logger.Log("Successfully deprovisioned {0}: {1}", package.DisplayName, ex.Message);
                            removedPackageCount += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log("An error occurred while deprovisioning packages: {0}", ex.Message);
                return false;
            }

            if (removedPackageCount <= 0)
            {
                logger.Log("No Windows Store packages were deprovisioned.");
            }

            return true;
        }
    }
}
