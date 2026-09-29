using Microsoft.Win32;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Management.Deployment;

namespace Chemo.Utilities
{
    /// <summary>
    /// An installed app package, such as a Microsoft Store app.
    /// </summary>
    internal sealed class AppPackage
    {
        public string Name { get; }

        public string PackageFullName { get; }

        public string PackageFamilyName { get; }

        /// <summary>
        /// Whether the package is part of Windows, which Windows refuses to remove until it's retired.
        /// </summary>
        public bool IsSystemApp { get; }

        public AppPackage(string name, string packageFullName, string packageFamilyName, bool isSystemApp)
        {
            Name = name;
            PackageFullName = packageFullName;
            PackageFamilyName = packageFamilyName;
            IsSystemApp = isSystemApp;
        }
    }

    /// <summary>
    /// Finds and removes app packages with Windows' PackageManager API, which .NET Framework can call directly.
    /// </summary>
    internal static class AppPackages
    {
        private const string AllUserStore = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Appx\AppxAllUserStore";
        private const string SystemSid = "S-1-5-18";

        // SYSTEM, LOCAL SERVICE, and NETWORK SERVICE, which nobody signs in with.
        private static readonly string[] ServiceSids = { SystemSid, "S-1-5-19", "S-1-5-20" };

        private static readonly PackageManager packageManager = new PackageManager();

        /// <summary>
        /// Finds the app packages installed for any user, listing each package once.
        /// </summary>
        public static List<AppPackage> FindForAllUsers()
        {
            return packageManager.FindPackages()
                .Select(package => new AppPackage(
                    package.Id.Name,
                    package.Id.FullName,
                    package.Id.FamilyName,
                    package.SignatureKind == PackageSignatureKind.System))
                .GroupBy(package => package.PackageFullName)
                .Select(group => group.First())
                .ToList();
        }

        /// <summary>
        /// Finds the people who have the package installed, rather than only staged. Service accounts are
        /// skipped, because Windows leaves a removed system app installed for SYSTEM, pending a removal that
        /// doesn't finish.
        /// </summary>
        /// <returns>The security IDs of those users.</returns>
        public static List<string> FindInstalledUsers(AppPackage package)
        {
            return packageManager.FindUsers(package.PackageFullName)
                .Where(user => user.InstallState == PackageInstallState.Installed && !ServiceSids.Contains(user.UserSecurityId))
                .Select(user => user.UserSecurityId)
                .ToList();
        }

        /// <summary>
        /// Retires a system app for everyone who has it, the way Windows retires the system apps it replaces, so
        /// that it can be removed. Also keeps it from being installed for new users.
        /// </summary>
        public static void RetireSystemApp(AppPackage package)
        {
            // Other removal tools retire it for the system account as well.
            IEnumerable<string> sids = packageManager.FindUsers(package.PackageFullName)
                .Select(user => user.UserSecurityId)
                .Append(SystemSid);

            foreach (string sid in sids)
            {
                Registry.LocalMachine.CreateSubKey($@"{AllUserStore}\EndOfLife\{sid}\{package.PackageFullName}").Dispose();
            }

            Registry.LocalMachine.CreateSubKey($@"{AllUserStore}\Deprovisioned\{package.PackageFamilyName}").Dispose();
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
