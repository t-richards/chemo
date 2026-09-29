using Chemo.Data;
using Chemo.Settings;
using Chemo.Utilities;
using Microsoft.Dism;

namespace Chemo.Treatment.Apps
{
    internal sealed class DeprovisionStoreApps : SettingsTreatment
    {
        public override string Name()
        {
            return "Keep removed apps from coming back";
        }

        public override string Tooltip()
        {
            return "Stops Windows from reinstalling the same apps when someone new signs in to this PC or when a big Windows update installs.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new StoreAppsDeprovisioned(Logger),
            ];
        }

        /// <summary>
        /// Windows installs provisioned apps for each new user, and reinstalls them during feature updates.
        /// </summary>
        private sealed class StoreAppsDeprovisioned : ISetting
        {
            private readonly MemoryLogger logger;

            public StoreAppsDeprovisioned(MemoryLogger logger)
            {
                this.logger = logger;
            }

            private static List<DismAppxPackage> FindPackages(DismSession session)
            {
                return DismApi.GetProvisionedAppxPackages(session)
                    .Where(package => StoreApps.ShouldRemove(package.DisplayName))
                    .ToList();
            }

            public bool IsApplied()
            {
                using DismSession session = DismApi.OpenOnlineSession();
                List<DismAppxPackage> packages = FindPackages(session);

                foreach (DismAppxPackage package in packages)
                {
                    logger.Log("{0} is provisioned", package.PackageName);
                }

                return packages.Count == 0;
            }

            public void Apply()
            {
                using DismSession session = DismApi.OpenOnlineSession();
                List<string> failed = [];

                foreach (DismAppxPackage package in FindPackages(session))
                {
                    try
                    {
                        DismApi.RemoveProvisionedAppxPackage(session, package.PackageName);
                        logger.Log("Deprovisioned {0}.", package.DisplayName);
                    }
                    catch (DismRebootRequiredException)
                    {
                        logger.Log("Deprovisioned {0}. Windows finishes at the next restart.", package.DisplayName);
                    }
                    catch (DismException ex)
                    {
                        logger.Log("Could not deprovision {0}: {1}", package.DisplayName, ex.Message);
                        failed.Add(package.DisplayName);
                    }
                }

                if (failed.Count > 0)
                {
                    throw new InvalidOperationException($"Could not deprovision {string.Join(", ", failed)}.");
                }
            }

            public override string ToString()
            {
                return "Pre-installed Store apps are deprovisioned";
            }
        }
    }
}
