using Microsoft.Dism;

namespace Chemo.Treatment
{
    class DeprovisionStoreApps : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;

        public void PerformTreatment()
        {
            int packageCount = 0;

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

            if (packageCount <= 0)
            {
                logger.Log("No Windows Store packages were deprovisioned.");
            }
        }
    }
}
