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
                    logger.Log("Deprovisioning package: {0}", package.DisplayName);
                    DismApi.RemoveProvisionedAppxPackage(session, package.PackageName);
                    packageCount += 1;
                }
            }

            if (packageCount < 1)
            {
                logger.Log("No Windows Store packages were deprovisioned.");
            }
        }
    }
}
