using Chemo.Data;
using Microsoft.Dism;
using System;

namespace Chemo.Treatment
{
    class DeprovisionStoreApps : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;

        public void PerformTreatment()
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
            }

            if (removedPackageCount <= 0)
            {
                logger.Log("No Windows Store packages were deprovisioned.");
            }
        }
    }
}
