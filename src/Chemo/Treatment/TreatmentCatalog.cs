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

            new Category("Sign-in", "Change how you sign in to Windows.",
                new SignIn.RequireCtrlAltDel()),

            new Category("Start", "Clean up the Start menu and search.",
                new Start.DisableInternetSearchResults(),
                new Start.SuggestedApps()),

            new Category("Time", "Change how Windows keeps time.",
                new Time.SetClockUTC()),

            new Category("Updates", "Control when Windows Update interrupts you.",
                new Updates.WindowsUpdateReboot()),
        };
    }
}
