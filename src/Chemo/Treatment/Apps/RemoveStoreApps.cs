using Chemo.Data;
using System.Collections.Generic;
using System.Threading;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Management.Deployment;

namespace Chemo.Treatment.Apps
{
    class RemoveStoreApps : BaseTreatment
    {
        private static PackageManager packageManager = new PackageManager();

        public override string Name()
        {
            return "Remove Windows Store Apps";
        }

        public override string Tooltip()
        {
            return "Removes most pre-installed Windows Store apps.";
        }

        public override bool ShouldPerformTreatment()
        {
            int packageCount = 0;
            IEnumerable<Package> packages = packageManager.FindPackages();

            foreach (Package package in packages)
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
            IEnumerable<Package> packages = packageManager.FindPackages();

            foreach (var package in packages)
            {
                if (StoreApps.ShouldRemove(package.Id.Name))
                {
                    RemovePackage(package);
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

        private void RemovePackage(Package package)
        {
            IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> deploymentOperation =
                packageManager.RemovePackageAsync(package.Id.FullName);

            ManualResetEvent opCompletedEvent = new ManualResetEvent(false);

            deploymentOperation.Completed = (result, progress) =>
            {
                Logger.Log("Removal operation {1}: {0}", package.Id.Name, result.Status);
                if (result.Status == AsyncStatus.Error)
                {
                    DeploymentResult deploymentResult = deploymentOperation.GetResults();
                    Logger.Log("Error code: {0}", deploymentOperation.ErrorCode);
                    Logger.Log("Error text: {0}", deploymentResult.ErrorText);
                }
                opCompletedEvent.Set();
            };

            opCompletedEvent.WaitOne();
            opCompletedEvent.Dispose();
        }
    }
}
