using Chemo.Data;
using System.Collections.Generic;
using System.Threading;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Management.Deployment;

namespace Chemo.Treatment
{
    class RemoveStoreApps : BaseTreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static PackageManager packageManager = new PackageManager();

        public new static string Name { get => "Remove Windows Store Apps"; }

        public override bool ShouldPerformTreatment()
        {
            int packageCount = 0;
            IEnumerable<Package> packages = packageManager.FindPackages();

            foreach (var package in packages)
            {
                // Don't remove frameworks or system packages
                if (
                    package.IsFramework ||
                    package.SignatureKind == PackageSignatureKind.System
                )
                {
                    continue;
                }

                if (StoreApps.shouldRemove(package.Id.Name))
                {
                    packageCount += 1;
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
            IEnumerable<Package> packages = packageManager.FindPackages();

            foreach (var package in packages)
            {
                // Don't remove frameworks or system packages
                if (
                    package.IsFramework ||
                    package.SignatureKind == PackageSignatureKind.System
                )
                {
                    continue;
                }

                if (StoreApps.shouldRemove(package.Id.Name))
                {
                    RemovePackage(package);
                    packageCount += 1;
                }
                else
                {
                    logger.Log("Not removing {0}", package.Id.Name);
                }
            }

            if (packageCount <= 0)
            {
                logger.Log("No Windows Store applications were uninstalled.");
            }
            logger.Log("");

            return true;
        }

        private static void RemovePackage(Package package)
        {
            IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> deploymentOperation =
                packageManager.RemovePackageAsync(package.Id.FullName);

            ManualResetEvent opCompletedEvent = new ManualResetEvent(false);

            deploymentOperation.Completed = (result, progress) =>
            {
                logger.Log("Removal operation {1}: {0}", package.Id.Name, result.Status);
                if (result.Status == AsyncStatus.Error)
                {
                    DeploymentResult deploymentResult = deploymentOperation.GetResults();
                    logger.Log("Error code: {0}", deploymentOperation.ErrorCode);
                    logger.Log("Error text: {0}", deploymentResult.ErrorText);
                }
                opCompletedEvent.Set();
            };

            opCompletedEvent.WaitOne();
        }
    }
}
