using Microsoft.Win32;
using System;
using System.Threading.Tasks;

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

        public override Task<bool> ShouldPerformTreatment()
        {
            var value = Registry.GetValue(CortanaKey, "AllowCortana", 0);
            if (value == null || (int)value != DesiredValue)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public override Task<bool> PerformTreatment()
        {
            try
            {
                Registry.SetValue(CortanaKey, "AllowCortana", DesiredValue, RegistryValueKind.DWord);
                Logger.Log("Successfully disabled Cortana.");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Logger.Log("Could not disable Cortana: {0}", ex.Message);
            }

            return Task.FromResult(false);
        }
    }
}
