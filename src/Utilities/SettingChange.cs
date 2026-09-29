using System;

namespace Chemo.Utilities
{
    internal static class SettingChange
    {
        /// <summary>
        /// Tells the taskbar and open apps that a setting changed, as the Settings app does, so they update
        /// now instead of after the next sign-in.
        /// </summary>
        /// <param name="area">The area that changed, such as "ImmersiveColorSet" for the theme or "intl" for regional formats.</param>
        public static void Broadcast(string area)
        {
            UnsafeNativeMethods.SendMessageTimeout(
                UnsafeNativeMethods.HWND_BROADCAST,
                UnsafeNativeMethods.WM_SETTINGCHANGE,
                IntPtr.Zero,
                area,
                UnsafeNativeMethods.SMTO_ABORTIFHUNG,
                100,
                out _);
        }
    }
}
