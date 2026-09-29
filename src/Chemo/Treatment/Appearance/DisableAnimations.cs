using Chemo.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Chemo.Treatment.Appearance
{
    internal class DisableAnimations : SettingsTreatment
    {
        private const uint UpdateAndNotify = UnsafeNativeMethods.SPIF_UPDATEINIFILE | UnsafeNativeMethods.SPIF_SENDCHANGE;

        public override string Name()
        {
            return "Turn Off Animations";
        }

        public override string Tooltip()
        {
            return "Turns off the animations when windows open, close, minimize, and maximize, and in apps and the taskbar, so Windows feels snappier.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new AppAnimationsOff(),
                new WindowAnimationsOff(),
                new RegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarAnimations", 0),
            };
        }

        private sealed class AppAnimationsOff : ISetting
        {
            public bool IsApplied()
            {
                int enabled = 0;
                if (!UnsafeNativeMethods.SystemParametersInfo(UnsafeNativeMethods.SPI_GETCLIENTAREAANIMATION, 0, ref enabled, 0))
                {
                    throw new Win32Exception();
                }

                return enabled == 0;
            }

            public void Apply()
            {
                // The new value goes in pvParam: IntPtr.Zero means off.
                if (!UnsafeNativeMethods.SystemParametersInfo(UnsafeNativeMethods.SPI_SETCLIENTAREAANIMATION, 0, IntPtr.Zero, UpdateAndNotify))
                {
                    throw new Win32Exception();
                }
            }

            public override string ToString()
            {
                return "Animations in apps are off";
            }
        }

        private sealed class WindowAnimationsOff : ISetting
        {
            private static UnsafeNativeMethods.ANIMATIONINFO Read()
            {
                UnsafeNativeMethods.ANIMATIONINFO info = new UnsafeNativeMethods.ANIMATIONINFO
                {
                    cbSize = (uint)Marshal.SizeOf<UnsafeNativeMethods.ANIMATIONINFO>()
                };

                if (!UnsafeNativeMethods.SystemParametersInfo(UnsafeNativeMethods.SPI_GETANIMATION, info.cbSize, ref info, 0))
                {
                    throw new Win32Exception();
                }

                return info;
            }

            public bool IsApplied()
            {
                return Read().iMinAnimate == 0;
            }

            public void Apply()
            {
                UnsafeNativeMethods.ANIMATIONINFO info = Read();
                info.iMinAnimate = 0;

                if (!UnsafeNativeMethods.SystemParametersInfo(UnsafeNativeMethods.SPI_SETANIMATION, info.cbSize, ref info, UpdateAndNotify))
                {
                    throw new Win32Exception();
                }
            }

            public override string ToString()
            {
                return "Minimize and maximize animations are off";
            }
        }
    }
}
