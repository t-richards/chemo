using Chemo.Settings;

namespace Chemo.Treatment.Taskbar
{
    internal sealed class RemoveWidgets : SettingsTreatment
    {
        private static readonly string[] PackageNames = ["Microsoft.WidgetsPlatformRuntime", "MicrosoftWindows.Client.WebExperience"];

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
            return
            [
                // Removal can fail while the widgets are running.
                new AppPackagesRemoved("Widgets packages", name => PackageNames.Contains(name, StringComparer.OrdinalIgnoreCase), Logger, "Widgets", "WidgetService"),
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Dsh", "AllowNewsAndInterests", 0),
            ];
        }
    }
}
