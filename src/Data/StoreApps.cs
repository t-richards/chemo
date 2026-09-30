namespace Chemo.Data
{
    internal static class StoreApps
    {
        // Pre-installed apps on a typical Windows 11 Pro install that are ads, upsells, or discontinued.
        // Built-in utilities (Calculator, Clock, Camera, Media Player, Photos, Snipping Tool, etc.) and
        // anything other apps depend on (App Installer, Store, Xbox identity, codecs) are deliberately kept.
        private static readonly HashSet<string> AppsToRemove = new(StringComparer.OrdinalIgnoreCase)
        {
            // Bing & MSN content
            "Microsoft.BingNews",                       // News
            "Microsoft.BingSearch",                     // Bing Search
            "Microsoft.BingWeather",                    // Weather
            "Microsoft.Copilot",                        // Copilot

            // Upsells & promotions
            "Clipchamp.Clipchamp",                      // Clipchamp
            "Microsoft.Getstarted",                     // Tips
            "Microsoft.MicrosoftOfficeHub",             // Microsoft 365 Copilot
            "Microsoft.MicrosoftSolitaireCollection",   // Solitaire & Casual Games
            "Microsoft.PowerAutomateDesktop",           // Power Automate
            "Microsoft.WindowsFeedbackHub",             // Feedback Hub

            // Teams
            "MicrosoftTeams",                           // Teams (personal), Windows 11 23H2 and earlier
            "MSTeams",                                  // Teams

            // Discontinued
            "Microsoft.549981C3F5F10",                  // Cortana
            "Microsoft.Windows.DevHome",                // Dev Home
            "Microsoft.windowscommunicationsapps",      // Mail and Calendar
            "Microsoft.WindowsMaps",                    // Maps
        };

        public static bool ShouldRemove(string packageName)
        {
            return AppsToRemove.Contains(packageName);
        }
    }
}
