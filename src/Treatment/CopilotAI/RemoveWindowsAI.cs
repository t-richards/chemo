using Chemo.Settings;
using Chemo.Utilities;

namespace Chemo.Treatment.CopilotAI
{
    internal sealed class RemoveWindowsAI : SettingsTreatment
    {
        private const string WindowsAIPolicies = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsAI";
        private const string PaintPolicies = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Paint";

        // Copilot, Microsoft 365 Copilot, and the system app behind Click to Do.
        private static readonly string[] PackageNames = ["Microsoft.Copilot", "Microsoft.MicrosoftOfficeHub", "MicrosoftWindows.Client.CoreAI"];

        // Windows AI Components Host
        private static readonly ServiceStartup AIService = new("WSAIFabricSvc", ServiceStartType.Disabled);

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
            return
            [
                new AppPackagesRemoved("Copilot and Click to Do apps", name => PackageNames.Contains(name, StringComparer.OrdinalIgnoreCase), Logger),

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
            ];
        }

        public override bool RestartPending()
        {
            // Recall is also removed at restart, but Windows doesn't show whether that's still to come.
            return AIService.IsApplied() && AIService.IsRunning();
        }
    }
}
