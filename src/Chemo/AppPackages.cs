using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Management.Deployment;

namespace Chemo
{
    internal static class AppPackages
    {
        private static readonly PackageManager packageManager = new PackageManager();

        /// <summary>
        /// Finds the app packages installed for any user, listing each package once.
        /// </summary>
        public static IEnumerable<Package> FindForAllUsers()
        {
            return packageManager.FindPackages().DistinctBy(p => p.Id.FullName);
        }

        /// <summary>
        /// Removes a package for every user on the PC.
        /// </summary>
        /// <returns>Returns true if the package was removed, false otherwise.</returns>
        public static bool RemoveForAllUsers(Package package, MemoryLogger logger)
        {
            IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> deploymentOperation =
                packageManager.RemovePackageAsync(package.Id.FullName, RemovalOptions.RemoveForAllUsers);

            using (ManualResetEvent opCompletedEvent = new ManualResetEvent(false))
            {
                deploymentOperation.Completed = (result, progress) => opCompletedEvent.Set();
                opCompletedEvent.WaitOne();
            }

            logger.Log("Removal operation {1}: {0}", package.Id.Name, deploymentOperation.Status);
            if (deploymentOperation.Status == AsyncStatus.Error)
            {
                DeploymentResult deploymentResult = deploymentOperation.GetResults();
                logger.Log("Error code: {0}", deploymentOperation.ErrorCode);
                logger.Log("Error text: {0}", deploymentResult.ErrorText);
                return false;
            }

            return true;
        }
    }
}
