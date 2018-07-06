using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class WindowsUpdateReboot : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;

        public void PerformTreatment()
        {
            try
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU", "AUOptions", 2, RegistryValueKind.DWord);
                logger.Log("Successfully disabled automatic reboot for Windows Update.");
            }
            catch (Exception ex)
            {
                logger.Log("Could not disable automatic reboot for Windows Update: {0}", ex.Message);
            }
        }
    }
}
