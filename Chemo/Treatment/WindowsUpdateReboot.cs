using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class WindowsUpdateReboot : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static readonly string AutoUpdateKey = "HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU";
        private static readonly int DesiredValue = 2;

        public bool ShouldPerformTreatment()
        {
            var value = Registry.GetValue(AutoUpdateKey, "AUOptions", 0);
            if (value == null || (int)value != DesiredValue)
            {
                return true;
            }

            return false;
        }

        public void PerformTreatment()
        {
            try
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU", "AUOptions", DesiredValue, RegistryValueKind.DWord);
                logger.Log("Successfully disabled automatic reboot for Windows Update.");
            }
            catch (Exception ex)
            {
                logger.Log("Could not disable automatic reboot for Windows Update: {0}", ex.Message);
            }
        }
    }
}
