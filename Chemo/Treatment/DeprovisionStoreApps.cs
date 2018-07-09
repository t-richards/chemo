using Microsoft.Dism;
using System;

namespace Chemo.Treatment
{
    class DeprovisionStoreApps : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;

        public void PerformTreatment()
        {
            int packageCount = 0;

            try
            {
                using (DismSession session = DismApi.OpenOnlineSession())
                {
                    DismAppxPackageCollection dismAppxPackages = DismApi.GetProvisionedAppxPackages(session);
                    foreach (var package in dismAppxPackages)
                    {
                        try
                        {
                            DismApi.RemoveProvisionedAppxPackage(session, package.PackageName);
                            logger.Log("Successfully deprovisioned {0}", package.DisplayName);
                        }
                        catch (DismRebootRequiredException ex)
                        {
                            logger.Log("Successfully deprovisioned {0}: {1}", package.DisplayName, ex.Message);
                        }

                        packageCount += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log("An error occurred while deprovisioning packages: {0}", ex.Message);
            }

            if (packageCount <= 0)
            {
                logger.Log("No Windows Store packages were deprovisioned.");
            }
        }
    }
}
