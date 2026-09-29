using Chemo.Utilities;
using System.Diagnostics;

namespace Chemo.Settings
{
    /// <summary>
    /// App packages that no one on the PC should have installed.
    /// </summary>
    internal sealed class AppPackagesRemoved : ISetting
    {
        private readonly string description;
        private readonly Func<string, bool> shouldRemove;
        private readonly MemoryLogger logger;
        private readonly string[] blockingProcesses;

        /// <param name="description">What the packages are, such as "Widgets packages".</param>
        /// <param name="shouldRemove">Whether to remove a package, given its name.</param>
        /// <param name="logger">Where to log each package that's still installed, and each removal.</param>
        /// <param name="blockingProcesses">Processes to stop first, because removing their package fails while they run.</param>
        public AppPackagesRemoved(string description, Func<string, bool> shouldRemove, MemoryLogger logger, params string[] blockingProcesses)
        {
            this.description = description;
            this.shouldRemove = shouldRemove;
            this.logger = logger;
            this.blockingProcesses = blockingProcesses;
        }

        private List<AppPackage> FindPackages()
        {
            // Names first, since checking who has a package installed is slower.
            return AppPackages.FindForAllUsers()
                .Where(p => shouldRemove(p.Name) && AppPackages.FindInstalledUsers(p).Count > 0)
                .ToList();
        }

        public bool IsApplied()
        {
            List<AppPackage> packages = FindPackages();

            foreach (AppPackage package in packages)
            {
                logger.Log("{0} is installed for {1}", package.PackageFullName, string.Join(", ", AppPackages.FindInstalledUsers(package)));
            }

            return packages.Count == 0;
        }

        public void Apply()
        {
            foreach (Process process in blockingProcesses.SelectMany(Process.GetProcessesByName))
            {
                using (process)
                {
                    process.Kill();
                    process.WaitForExit(5000);
                }
            }

            List<AppPackage> packages = FindPackages();

            // Windows won't remove its own apps until they're retired.
            foreach (AppPackage package in packages.Where(p => p.IsSystemApp))
            {
                AppPackages.RetireSystemApp(package);
                logger.Log("Retired {0} so it can be removed.", package.Name);
            }

            List<AppPackage> failed = AppPackages.RemoveForAllUsers(packages, logger);

            if (failed.Count > 0)
            {
                throw new InvalidOperationException($"Could not remove {string.Join(", ", failed.Select(p => p.Name))}.");
            }
        }

        public override string ToString()
        {
            return $"{description} are removed";
        }
    }
}
