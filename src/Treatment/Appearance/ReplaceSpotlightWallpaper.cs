using Chemo.Settings;
using Chemo.Utilities;
using Microsoft.Win32;
using System.ComponentModel;

namespace Chemo.Treatment.Appearance
{
    internal sealed class ReplaceSpotlightWallpaper : SettingsTreatment
    {
        public override string Name => "Replace the Spotlight wallpaper";

        public override string Description =>
            "If your desktop background is Windows Spotlight, which downloads a new picture every day and adds a \"Learn about this picture\" " +
            "icon to the desktop, switches it to the standard Windows wallpaper. A picture you picked yourself is left alone.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new SpotlightOff(),
            ];
        }

        /// <summary>
        /// Choosing Windows Spotlight in Settings > Personalization > Background sets both of these values, and choosing
        /// Picture clears them.
        /// </summary>
        private sealed class SpotlightOff : ISetting
        {
            private const string Wallpapers = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers";
            private const string SpotlightSettings = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\DesktopSpotlight\Settings";
            private const int SpotlightBackground = 3;
            private const int PictureBackground = 0;

            public bool IsApplied()
            {
                return !RegistryUtils.IntEquals(Wallpapers, "BackgroundType", SpotlightBackground) &&
                    !RegistryUtils.IntEquals(SpotlightSettings, "EnabledState", 1);
            }

            public void Apply()
            {
                // %SystemRoot%\Web\Wallpaper\Windows\img0.jpg, the first picture Settings offers.
                string wallpaper = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Web", "Wallpaper", "Windows", "img0.jpg");
                if (!File.Exists(wallpaper))
                {
                    throw new FileNotFoundException("The default Windows wallpaper is missing.", wallpaper);
                }

                Registry.SetValue(SpotlightSettings, "EnabledState", 0, RegistryValueKind.DWord);
                Registry.SetValue(Wallpapers, "BackgroundType", PictureBackground, RegistryValueKind.DWord);

                const uint UpdateAndNotify = UnsafeNativeMethods.SPIF_UPDATEINIFILE | UnsafeNativeMethods.SPIF_SENDCHANGE;
                if (!UnsafeNativeMethods.SystemParametersInfo(UnsafeNativeMethods.SPI_SETDESKWALLPAPER, 0, wallpaper, UpdateAndNotify))
                {
                    throw new Win32Exception();
                }
            }

            public override string ToString()
            {
                return "Desktop background isn't Windows Spotlight";
            }
        }
    }
}
