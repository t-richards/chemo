using Chemo.Settings;
using Chemo.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Chemo.Treatment.Taskbar
{
    class RemoveWidgets : SettingsTreatment
    {
        public override string Name()
        {
            return "Remove Widgets";
        }

        public override string Tooltip()
        {
            return "Removes the Widgets board and its news feed from the taskbar for all users, and turns Widgets off so it stays off if Windows reinstalls it. Sign out to finish.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new WidgetPackagesRemoved(Logger),
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Dsh", "AllowNewsAndInterests", 0),
            };
        }

        private sealed class WidgetPackagesRemoved : ISetting
        {
            private static readonly string[] PackageNames = { "Microsoft.WidgetsPlatformRuntime", "MicrosoftWindows.Client.WebExperience" };
            private static readonly string[] ProcessNames = { "Widgets", "WidgetService" };

            private readonly MemoryLogger Logger;

            public WidgetPackagesRemoved(MemoryLogger logger)
            {
                Logger = logger;
            }

            private static List<AppPackage> FindPackages()
            {
                return AppPackages.FindForAllUsers()
                    .Where(p => PackageNames.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                    .ToList();
            }

            public bool IsApplied()
            {
                return FindPackages().Count == 0;
            }

            public void Apply()
            {
                // Removal can fail while the widgets are running.
                foreach (Process process in ProcessNames.SelectMany(Process.GetProcessesByName))
                {
                    process.Kill();
                    process.WaitForExit(5000);
                }

                List<AppPackage> failed = AppPackages.RemoveForAllUsers(FindPackages(), Logger);

                if (failed.Count > 0)
                {
                    throw new InvalidOperationException($"Could not remove {string.Join(", ", failed.Select(p => p.Name))}.");
                }
            }

            public override string ToString()
            {
                return "Widgets packages are removed";
            }
        }
    }
}
