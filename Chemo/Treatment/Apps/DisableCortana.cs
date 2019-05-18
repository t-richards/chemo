using Microsoft.Win32;
using System;

namespace Chemo.Treatment.Apps
{
    class DisableCortana : BaseTreatment
    {
        private const string CortanaKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search";
        private const int DesiredValue = 0;

        public override string Name()
        {
            return "Disable Cortana";
        }

        public override string Tooltip()
        {
            return "Prevents Cortana from appearing in the taskbar. A sign out is required to complete this operation.";
        }

        public override bool ShouldPerformTreatment()
        {
            var value = Registry.GetValue(CortanaKey, "AllowCortana", 0);
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
