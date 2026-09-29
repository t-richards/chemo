using Chemo.Settings;
using Chemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chemo.Treatment.CopilotAI
{
    class RemoveWindowsAI : SettingsTreatment
    {
        private const string WindowsAIPolicies = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsAI";
        private const string PaintPolicies = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Paint";

        // Windows AI Components Host
        private static readonly ServiceStartup AIService = new ServiceStartup("WSAIFabricSvc", ServiceStartType.Disabled);

        public override string Name()
        {
            return "Remove Windows AI";
        }

        public override string Tooltip()
        {
            return "Removes the Copilot apps and Click to Do for all users, and turns off Recall, the AI features in Paint and Notepad, " +
                "and the Windows AI service so they stay off. Restart to finish.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new AIPackagesRemoved(Logger),

                // Recall. Windows removes the feature and any saved snapshots at the next restart.
                new RegistryValue(WindowsAIPolicies, "AllowRecallEnablement", 0),
                new RegistryValue(WindowsAIPolicies, "DisableAIDataAnalysis", 1),

                // Click to Do, and the AI search in Settings, which only Enterprise and Education let you turn off.
                new RegistryValue(WindowsAIPolicies, "DisableClickToDo", 1),
                new RegistryValue(WindowsAIPolicies, "DisableSettingsAgent", 1),

                // The Copilot pane in Windows 11 23H2. It also stops upgrades from installing the Copilot app in its place.
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\WindowsCopilot", "TurnOffWindowsCopilot", 1),

                // Paint and Notepad
                new RegistryValue(PaintPolicies, "DisableCocreator", 1),
                new RegistryValue(PaintPolicies, "DisableGenerativeFill", 1),
                new RegistryValue(PaintPolicies, "DisableImageCreator", 1),
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\WindowsNotepad", "DisableAIFeatures", 1),

                AIService,
            };
        }

        public override bool RestartPending()
        {
            // Recall is also removed at restart, but Windows doesn't show whether that's still to come.
            return AIService.IsApplied() && AIService.IsRunning();
        }

        private sealed class AIPackagesRemoved : ISetting
        {
            // Copilot, Microsoft 365 Copilot, and the system app behind Click to Do.
            private static readonly string[] PackageNames = { "Microsoft.Copilot", "Microsoft.MicrosoftOfficeHub", "MicrosoftWindows.Client.CoreAI" };

            private readonly MemoryLogger Logger;

            public AIPackagesRemoved(MemoryLogger logger)
            {
                Logger = logger;
            }

            private static List<AppPackage> FindPackages()
            {
                return AppPackages.FindForAllUsers()
                    .Where(p => PackageNames.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                    .Where(p => AppPackages.FindInstalledUsers(p).Count > 0)
                    .ToList();
            }

            public bool IsApplied()
            {
                List<AppPackage> packages = FindPackages();

                foreach (AppPackage package in packages)
                {
                    Logger.Log("{0} is installed for {1}", package.PackageFullName, string.Join(", ", AppPackages.FindInstalledUsers(package)));
                }

                return packages.Count == 0;
            }

            public void Apply()
            {
                List<AppPackage> packages = FindPackages();

                foreach (AppPackage package in packages.Where(p => p.IsSystemApp))
                {
                    AppPackages.RetireSystemApp(package);
                    Logger.Log("Retired {0} so it can be removed.", package.Name);
                }

                List<AppPackage> failed = AppPackages.RemoveForAllUsers(packages, Logger);

                if (failed.Count > 0)
                {
                    throw new InvalidOperationException($"Could not remove {string.Join(", ", failed.Select(p => p.Name))}.");
                }
            }

            public override string ToString()
            {
                return "Copilot and Click to Do apps are removed";
            }
        }
    }
}
