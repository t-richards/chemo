using Chemo.Settings;
using System.Collections.Generic;
using System.ComponentModel;

namespace Chemo.Treatment.KeyboardMouse
{
    class DisableMouseAcceleration : SettingsTreatment
    {
        public override string Name()
        {
            return "Turn Off Mouse Acceleration";
        }

        public override string Tooltip()
        {
            return "Turns off Enhance pointer precision, so the pointer always moves the same distance for the same mouse movement, however fast you move it.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new PointerPrecisionOff(),
            };
        }

        /// <summary>
        /// The mouse settings are two speed thresholds and an acceleration level. An acceleration of 0 is what
        /// unchecking Enhance pointer precision does.
        /// </summary>
        private sealed class PointerPrecisionOff : ISetting
        {
            public bool IsApplied()
            {
                int[] mouse = new int[3];
                if (!UnsafeNativeMethods.SystemParametersInfo(UnsafeNativeMethods.SPI_GETMOUSE, 0, mouse, 0))
                {
                    throw new Win32Exception();
                }

                return mouse[2] == 0;
            }

            public void Apply()
            {
                const uint UpdateAndNotify = UnsafeNativeMethods.SPIF_UPDATEINIFILE | UnsafeNativeMethods.SPIF_SENDCHANGE;
                if (!UnsafeNativeMethods.SystemParametersInfo(UnsafeNativeMethods.SPI_SETMOUSE, 0, new[] { 0, 0, 0 }, UpdateAndNotify))
                {
                    throw new Win32Exception();
                }
            }

            public override string ToString()
            {
                return "Enhance pointer precision is off";
            }
        }
    }
}
