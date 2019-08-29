using Chemo.Data;
using Microsoft.Dism;
using System;
using System.Threading.Tasks;

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

        public override Task<bool> ShouldPerformTreatment()
        {
            int packageCount = 0;
            try
            {
                using (DismSession session = DismApi.OpenOnlineSession())
                {
                    DismAppxPackageCollection dismAppxPackages = DismApi.GetProvisionedAppxPackages(session);
                    foreach (var package in dismAppxPackages)
                    {
                        if (StoreApps.ShouldRemove(package.DisplayName))
                        {
                            Logger.Log("Would deprovision {0}", package.DisplayName);
                            packageCount += 1;
                        }
                    }
                }
            }
            catch
            {
                return Task.FromResult(false);
            }

            if (packageCount > 0)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public override Task<bool> PerformTreatment()
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
            }
            catch (Exception ex)
            {
                Logger.Log("An error occurred while deprovisioning packages: {0}", ex.Message);
                return Task.FromResult(false);
            }

            if (removedPackageCount <= 0)
            {
                Logger.Log("No Windows Store packages were deprovisioned.");
            }

            return Task.FromResult(true);
        }
    }
}
