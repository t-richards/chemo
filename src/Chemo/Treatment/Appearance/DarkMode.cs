using Chemo.Settings;
using System;
using System.Collections.Generic;

namespace Chemo.Treatment.Appearance
{
    internal class DarkMode : SettingsTreatment
    {
        private const string Personalize = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        public override string Name()
        {
            return "Dark Mode";
        }

        public override string Tooltip()
        {
            return "Switches Windows (the taskbar and Start menu) and apps to dark mode, like choosing Dark in Settings > Personalization > Colors.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // The Settings app's single mode choice sets both of these; its Custom mode sets them separately.
            return new ISetting[]
            {
                new RegistryValue(Personalize, "SystemUsesLightTheme", 0),
                new RegistryValue(Personalize, "AppsUseLightTheme", 0),
            };
        }

        protected override void OnSettingsChanged()
        {
            // Tell the taskbar, Start menu, and open apps that the theme changed, as the Settings app does,
            // so they switch now instead of after the next sign-in.
            UnsafeNativeMethods.SendMessageTimeout(
                UnsafeNativeMethods.HWND_BROADCAST,
                UnsafeNativeMethods.WM_SETTINGCHANGE,
                IntPtr.Zero,
                "ImmersiveColorSet",
                UnsafeNativeMethods.SMTO_ABORTIFHUNG,
                100,
                out _);
            Logger.Log("Notified open windows of the theme change.");
        }
    }
}
