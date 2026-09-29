using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Windows.Foundation;
using Windows.Management.Deployment;

namespace Chemo.Utilities
{
    /// <summary>
    /// An installed app package, such as a Microsoft Store app.
    /// </summary>
    internal sealed class AppPackage
    {
        public string Name { get; set; }

        public string PackageFullName { get; set; }
    }

    /// <summary>
    /// Finds and removes app packages with Windows' PackageManager API, which .NET Framework can call directly.
    /// </summary>
    internal static class AppPackages
    {
        private static readonly PackageManager packageManager = new PackageManager();

        /// <summary>
        /// Finds the app packages installed for any user, listing each package once.
        /// </summary>
        public static List<AppPackage> FindForAllUsers()
        {
            return packageManager.FindPackages()
                .Select(package => new AppPackage { Name = package.Id.Name, PackageFullName = package.Id.FullName })
                .GroupBy(package => package.PackageFullName)
                .Select(group => group.First())
                .ToList();
        }

        /// <summary>
        /// Removes packages for every user on the PC, logging the result for each one.
        /// </summary>
        /// <returns>The packages that couldn't be removed.</returns>
        public static List<AppPackage> RemoveForAllUsers(IReadOnlyCollection<AppPackage> packages, MemoryLogger logger)
        {
            List<AppPackage> failed = new List<AppPackage>();

            foreach (AppPackage package in packages)
            {
                IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> deploymentOperation =
                    packageManager.RemovePackageAsync(package.PackageFullName, RemovalOptions.RemoveForAllUsers);

                using (ManualResetEvent opCompletedEvent = new ManualResetEvent(false))
                {
                    deploymentOperation.Completed = (result, progress) => opCompletedEvent.Set();
                    opCompletedEvent.WaitOne();
                }

                if (deploymentOperation.Status == AsyncStatus.Error)
                {
                    logger.Log("Could not remove {0}: {1}", package.Name, deploymentOperation.GetResults().ErrorText);
                    failed.Add(package);
                }
                else
                {
                    logger.Log("Removed {0}.", package.Name);
                }
            }

            return failed;
        }
    }
}
