using Chemo.Settings;

namespace Chemo.Treatment.Taskbar
{
    internal sealed class RemoveWidgets : SettingsTreatment
    {
        private static readonly string[] PackageNames = ["Microsoft.WidgetsPlatformRuntime", "MicrosoftWindows.Client.WebExperience"];

        public override string Name => "Remove Widgets";

        public override string Description =>
            "Removes Widgets and its news feed from the taskbar for everyone on this PC. " +
            "If a Windows update brings it back, run this again.";

        protected override IEnumerable<ISetting> Settings()
        {
            // The AllowNewsAndInterests policy would keep Widgets off, but Windows' UserChoice Protection Driver
            // (UCPD.sys) only lets Microsoft's own programs set it, even for administrators.
            return
            [
                // Removal can fail while the widgets are running.
                new AppPackagesRemoved("Widgets packages", name => PackageNames.Contains(name, StringComparer.OrdinalIgnoreCase), Logger, "Widgets", "WidgetService"),
            ];
        }
    }
}
