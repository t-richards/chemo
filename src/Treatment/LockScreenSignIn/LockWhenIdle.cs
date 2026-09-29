using Chemo.Settings;
using Chemo.Utilities;
using Microsoft.Win32;
using System.ComponentModel;

namespace Chemo.Treatment.LockScreenSignIn
{
    class LockWhenIdle : SettingsTreatment
    {
        public override string Name()
        {
            return "Lock After 15 Minutes Away";
        }

        public override string Tooltip()
        {
            return "Blanks the screen after 15 minutes without use, and asks you to sign in again when you come back.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new BlankScreenSaver(),
                new ScreenSaverParameter("Screen saver is on", UnsafeNativeMethods.SPI_GETSCREENSAVEACTIVE, UnsafeNativeMethods.SPI_SETSCREENSAVEACTIVE, 1),
                new ScreenSaverParameter("Screen saver starts after 900 seconds", UnsafeNativeMethods.SPI_GETSCREENSAVETIMEOUT, UnsafeNativeMethods.SPI_SETSCREENSAVETIMEOUT, 900),
                new ScreenSaverParameter("Sign-in is required after the screen saver", UnsafeNativeMethods.SPI_GETSCREENSAVESECURE, UnsafeNativeMethods.SPI_SETSCREENSAVESECURE, 1),
            };
        }

        /// <summary>
        /// Windows' built-in Blank screen saver. There's no system parameter for which screen saver to use,
        /// so it's set in the registry like the Screen Saver Settings dialog does.
        /// </summary>
        private sealed class BlankScreenSaver : ISetting
        {
            private const string Desktop = @"HKEY_CURRENT_USER\Control Panel\Desktop";
            private readonly string path = Path.Combine(Environment.SystemDirectory, "scrnsave.scr");

            public bool IsApplied()
            {
                return Registry.GetValue(Desktop, "SCRNSAVE.EXE", null) is string current
                    && current.Equals(path, StringComparison.OrdinalIgnoreCase);
            }

            public void Apply()
            {
                Registry.SetValue(Desktop, "SCRNSAVE.EXE", path, RegistryValueKind.String);
            }

            public override string ToString()
            {
                return $"Screen saver is {path}";
            }
        }

        /// <summary>
        /// A screen saver system parameter, which is read into pvParam and set from uiParam.
        /// </summary>
        private sealed class ScreenSaverParameter : ISetting
        {
            private readonly string description;
            private readonly uint getAction;
            private readonly uint setAction;
            private readonly int value;

            public ScreenSaverParameter(string description, uint getAction, uint setAction, int value)
            {
                this.description = description;
                this.getAction = getAction;
                this.setAction = setAction;
                this.value = value;
            }

            public bool IsApplied()
            {
                int current = 0;
                if (!UnsafeNativeMethods.SystemParametersInfo(getAction, 0, ref current, 0))
                {
                    throw new Win32Exception();
                }

                return current == value;
            }

            public void Apply()
            {
                const uint UpdateAndNotify = UnsafeNativeMethods.SPIF_UPDATEINIFILE | UnsafeNativeMethods.SPIF_SENDCHANGE;
                if (!UnsafeNativeMethods.SystemParametersInfo(setAction, (uint)value, IntPtr.Zero, UpdateAndNotify))
                {
                    throw new Win32Exception();
                }
            }

            public override string ToString()
            {
                return description;
            }
        }
    }
}
