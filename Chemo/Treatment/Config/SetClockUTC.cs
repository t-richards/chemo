using Microsoft.Win32;
using System;

namespace Chemo.Treatment.Config
{
    class SetClockUTC : BaseTreatment
    {
        private static readonly string TimezoneKey = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\TimeZoneInformation";
        private static readonly int DesiredValue = 1;

        public override string Name()
        {
            return "Set System Clock to UTC";
        }

        public override string Tooltip()
        {
            return @"Sets the system's hardware clock to Coordinated Universal Time (UTC). The Windows default is localtime.";
        }

        public override bool ShouldPerformTreatment()
        {
            var value = Registry.GetValue(TimezoneKey, "RealTimeIsUniversal", 0);
            if (value == null || (int)value != DesiredValue)
            {
                return true;
            }

            return false;
        }

        public override bool PerformTreatment()
        {
            try
            {
                Registry.SetValue(TimezoneKey, "RealTimeIsUniversal", 1, RegistryValueKind.DWord);
                logger.Log("Successfully set system clock to UTC.");
                return true;
            }
            catch (Exception ex)
            {
                logger.Log("Could not set system clock to UTC: {0}", ex.Message);
            }

            return false;
        }
    }
}
