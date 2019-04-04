using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class DisableCortana : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static readonly string CortanaKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search";
        private static readonly int DesiredValue = 0;

        public bool ShouldPerformTreatment()
        {
            var value = Registry.GetValue(CortanaKey, "AllowCortana", 0);
            if (value == null || (int)value != DesiredValue)
            {
                return true;
            }

            return false;
        }

        public bool PerformTreatment()
        {
            try
            {
                Registry.SetValue(CortanaKey, "AllowCortana", DesiredValue, RegistryValueKind.DWord);
                logger.Log("Successfully disabled Cortana.");
                return true;
            }
            catch (Exception ex)
            {
                logger.Log("Could not disable Cortana: {0}", ex.Message);
            }

            return false;
        }
    }
}
