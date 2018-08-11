using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class SetClockUTC : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static readonly string timezoneInfo = "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\TimeZoneInformation";

        public void PerformTreatment()
        {
            try
            {
                Registry.SetValue(timezoneInfo, "RealTimeIsUniversal", 1, RegistryValueKind.DWord);
                logger.Log("Successfully set system clock to UTC.");
            }
            catch (Exception ex)
            {
                logger.Log("Could not set system clock to UTC: {0}", ex.Message);
            }
        }
    }
}
