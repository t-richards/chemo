using Chemo.Utilities;
using Microsoft.Win32;

namespace Chemo.Controls
{
    /// <summary>
    /// Which theme Chemo uses: the one picked for apps in Windows, or always light or dark.
    /// </summary>
    internal enum ThemeChoice
    {
        System,
        Light,
        Dark,
    }

    /// <summary>
    /// Chemo's dark mode. Controls that Windows draws get the dark look File Explorer uses, and the rest get colors
    /// from the WinUI dark theme, the same source as <see cref="FluentIcons"/>. Light mode is the Windows Forms default.
    /// </summary>
    internal static class DarkTheme
    {
        private const string Personalize = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        public static readonly Color Background = Color.FromArgb(0x20, 0x20, 0x20);
        public static readonly Color Surface = Color.FromArgb(0x19, 0x19, 0x19);
        public static readonly Color Text = Color.White;
        public static readonly Color DisabledText = Color.FromArgb(0x96, 0x96, 0x96);

        // The border Windows draws around a dark list, so the details box can match it.
        public static readonly Color Border = Color.FromArgb(0x82, 0x87, 0x90);

        public static readonly Color MenuBackground = Color.FromArgb(0x2B, 0x2B, 0x2B);
        public static readonly Color MenuHover = Color.FromArgb(0x3D, 0x3D, 0x3D);
        public static readonly Color MenuBorder = Color.FromArgb(0x45, 0x45, 0x45);
        public static readonly Color ProgressBar = Color.FromArgb(0x6C, 0xCB, 0x5F);
        public static readonly Color ProgressTrack = Color.FromArgb(0x3A, 0x3A, 0x3A);

        /// <summary>
        /// Works out whether Chemo should be dark. High contrast always wins, so its colors are never covered up.
        /// </summary>
        public static bool UseDark(ThemeChoice choice, bool windowsIsDark, bool highContrast)
        {
            if (highContrast)
            {
                return false;
            }

            return choice == ThemeChoice.Dark || (choice == ThemeChoice.System && windowsIsDark);
        }

        /// <summary>
        /// Whether Dark is picked for apps in Settings > Personalization > Colors.
        /// </summary>
        public static bool WindowsIsDark()
        {
            return Registry.GetValue(Personalize, "AppsUseLightTheme", null) is 0;
        }

        /// <summary>
        /// Gives a window Windows' dark look, the one File Explorer uses, or puts back its default look.
        /// </summary>
        public static void SetWindowTheme(IntPtr handle, bool dark)
        {
            _ = UnsafeNativeMethods.SetWindowTheme(handle, dark ? "DarkMode_Explorer" : null, null);
        }

        /// <summary>
        /// Makes a window's title bar dark or light.
        /// </summary>
        public static void SetTitleBar(IntPtr handle, bool dark)
        {
            int value = dark ? 1 : 0;
            _ = UnsafeNativeMethods.DwmSetWindowAttribute(handle, UnsafeNativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE, ref value, sizeof(int));
        }

        /// <summary>
        /// Makes the native popup menus Windows draws for Chemo, like a text box's right-click menu and the window
        /// menu, dark or light. The functions for this are undocumented, so if a future Windows drops them the menus
        /// stay light.
        /// </summary>
        public static void SetMenuMode(bool dark)
        {
            try
            {
                _ = UnsafeNativeMethods.SetPreferredAppMode(dark ? PreferredAppMode.ForceDark : PreferredAppMode.ForceLight);
                UnsafeNativeMethods.FlushMenuThemes();
            }
            catch (EntryPointNotFoundException)
            {
                // Menus keep the light look.
            }
        }
    }
}
