using Chemo.Settings;
using Chemo.Utilities;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Chemo.Treatment.KeyboardMouse
{
    class DisableStickyKeysShortcut : SettingsTreatment
    {
        public override string Name()
        {
            return "Turn Off the Sticky Keys Shortcut";
        }

        public override string Tooltip()
        {
            return "Stops pressing Shift five times from turning on Sticky Keys or asking whether to, which is easy to do by accident in games.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new ShortcutOff(),
            };
        }

        private sealed class ShortcutOff : ISetting
        {
            private static UnsafeNativeMethods.STICKYKEYS Read()
            {
                UnsafeNativeMethods.STICKYKEYS stickyKeys = new UnsafeNativeMethods.STICKYKEYS
                {
                    cbSize = (uint)Marshal.SizeOf<UnsafeNativeMethods.STICKYKEYS>()
                };

                if (!UnsafeNativeMethods.SystemParametersInfo(UnsafeNativeMethods.SPI_GETSTICKYKEYS, stickyKeys.cbSize, ref stickyKeys, 0))
                {
                    throw new Win32Exception();
                }

                return stickyKeys;
            }

            public bool IsApplied()
            {
                return (Read().dwFlags & UnsafeNativeMethods.SKF_HOTKEYACTIVE) == 0;
            }

            public void Apply()
            {
                // Only the shortcut is turned off. Sticky Keys itself, and its other options, are left as they are.
                UnsafeNativeMethods.STICKYKEYS stickyKeys = Read();
                stickyKeys.dwFlags &= ~UnsafeNativeMethods.SKF_HOTKEYACTIVE;

                const uint UpdateAndNotify = UnsafeNativeMethods.SPIF_UPDATEINIFILE | UnsafeNativeMethods.SPIF_SENDCHANGE;
                if (!UnsafeNativeMethods.SystemParametersInfo(UnsafeNativeMethods.SPI_SETSTICKYKEYS, stickyKeys.cbSize, ref stickyKeys, UpdateAndNotify))
                {
                    throw new Win32Exception();
                }
            }

            public override string ToString()
            {
                return "Pressing Shift five times doesn't turn on Sticky Keys";
            }
        }
    }
}
