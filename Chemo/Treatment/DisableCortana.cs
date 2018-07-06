using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class DisableCortana : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;

        public void PerformTreatment()
        {
            try
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search", "AllowCortana", 0, RegistryValueKind.DWord);
                logger.Log("Successfully disabled Cortana.");
            }
            catch (Exception ex)
            {
                logger.Log("Could not disable Cortana: {0}", ex.Message);
            }
        }
    }
}
