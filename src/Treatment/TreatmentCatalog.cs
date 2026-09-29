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
    internal static class TreatmentCatalog
    {
        public static IReadOnlyList<Category> Categories { get; } =
        [
            new Category("Appearance", "Change how Windows looks.",
                new Appearance.DarkMode(),
                new Appearance.DisableTransparency(),
                new Appearance.DisableAnimations(),
                new Appearance.ReplaceSpotlightWallpaper()),

            new Category("Apps", "Remove apps you didn't ask for.",
                new Apps.RemoveStoreApps(),
                new Apps.DeprovisionStoreApps(),
                new Apps.OneDrive()),

            new Category("Copilot & AI", "Remove Copilot and turn off the AI features built into Windows.",
                new CopilotAI.RemoveWindowsAI()),

            new Category("Devices", "Stop PC and device makers from installing their apps without asking.",
                new Devices.PreventDeviceCompanionApps(),
                new Devices.DisableWpbt()),

            new Category("File Explorer", "Change how File Explorer shows your files.",
                new FileExplorer.TurnOnAdvancedSettings(),
                new FileExplorer.RemoveHomeAndGallery(),
                new FileExplorer.RestorePreviousRightClickMenu(),
                new FileExplorer.DisableFolderDiscovery()),

            new Category("Keyboard & mouse", "Stop the keyboard and mouse from doing things you didn't mean.",
                new KeyboardMouse.DisableStickyKeysShortcut(),
                new KeyboardMouse.DisableMouseAcceleration()),

            new Category("Lock screen & sign-in", "Change the lock screen, how you sign in, and when your PC locks.",
                new LockScreenSignIn.LockWhenIdle(),
                new LockScreenSignIn.RequireCtrlAltDel(),
                new LockScreenSignIn.SkipLockScreen(),
                new LockScreenSignIn.DisableLockScreenTips()),

            new Category("Network", "Change how your PC treats the networks it connects to.",
                new Network.SetNetworksPrivate()),

            new Category("Performance", "Reduce what Windows runs in the background.",
                new Performance.SetServicesToManual(),
                new Performance.DisableBackgroundApps()),

            new Category("Power", "Change how your PC uses power, sleeps, and shuts down.",
                new Power.UseHighPerformancePlan(),
                new Power.DisableHibernation()),

            new Category("Privacy", "Limit what Windows collects and shares about you.",
                new Privacy.DisableTelemetry(),
                new Privacy.DisableLocationTracking()),

            new Category("Sound", "Quiet the sounds Windows makes on its own.",
                new Sound.DisableSystemSounds(),
                new Sound.KeepVolumeDuringCalls()),

            new Category("Start", "Clean up Start and search.",
                new Start.DisableInternetSearchResults(),
                new Start.DisableStoreSearchResults(),
                new Start.SuggestedApps(),
                new Start.UnpinStartApps(),
                new Start.HideRecentItems(),
                new Start.RestorePreviousStartMenu()),

            new Category("Taskbar", "Clean up the taskbar.",
                new Taskbar.RemoveWidgets(),
                new Taskbar.HideSearchAndTaskView(),
                new Taskbar.UnpinTaskbarApps(),
                new Taskbar.AlignTaskbarLeft(),
                new Taskbar.CombineButtonsWhenFull(),
                new Taskbar.ShowAllTrayIcons(),
                new Taskbar.EnableEndTask()),

            new Category("Time", "Change how Windows keeps and shows the time.",
                new Time.Use24HourClock(),
                new Time.UseIsoDates(),
                new Time.SetClockUtc()),

            new Category("Updates", "Control when Windows Update interrupts you.",
                new Updates.DelayUpdateRestarts()),
        ];
    }
}
