using System.Collections.Generic;

namespace Chemo.Treatment
{
    /// <summary>
    /// Every treatment, grouped the way it appears in the treatment tree.
    /// </summary>
    /// <remarks>
    /// Categories are named after the part of Windows a person would recognize, not how the treatment
    /// works, and are listed alphabetically. Each category's treatments live in the matching folder
    /// and namespace under Treatment.
    /// </remarks>
    public static class TreatmentCatalog
    {
        public static IReadOnlyList<Category> Categories { get; } = new[]
        {
            new Category("Appearance", "Change how Windows looks.",
                new Appearance.DarkMode(),
                new Appearance.DisableTransparency()),

            new Category("Apps", "Remove apps you didn't ask for.",
                new Apps.RemoveStoreApps(),
                new Apps.DeprovisionStoreApps(),
                new Apps.OneDrive()),

            new Category("Devices", "Stop PC and device makers from installing software on their own.",
                new Devices.PreventDeviceCompanionApps(),
                new Devices.DisableWpbt()),

            new Category("File Explorer", "Change how File Explorer shows your files.",
                new FileExplorer.DisableFolderDiscovery()),

            new Category("Performance", "Reduce what Windows runs in the background.",
                new Performance.SetServicesToManual()),

            new Category("Power", "Change how your PC sleeps and shuts down.",
                new Power.DisableHibernation()),

            new Category("Privacy", "Limit what Windows collects and shares about you.",
                new Privacy.DisableTelemetry(),
                new Privacy.DisableLocationTracking()),

            new Category("Sign-in", "Change how you sign in to Windows.",
                new SignIn.RequireCtrlAltDel()),

            new Category("Start", "Clean up the Start menu and search.",
                new Start.DisableInternetSearchResults(),
                new Start.DisableStoreSearchResults(),
                new Start.SuggestedApps(),
                new Start.RestorePreviousStartMenu()),

            new Category("Taskbar", "Clean up the taskbar.",
                new Taskbar.RemoveWidgets(),
                new Taskbar.EnableEndTask()),

            new Category("Time", "Change how Windows keeps time.",
                new Time.SetClockUTC()),

            new Category("Updates", "Control when Windows Update interrupts you.",
                new Updates.WindowsUpdateReboot()),
        };
    }
}
