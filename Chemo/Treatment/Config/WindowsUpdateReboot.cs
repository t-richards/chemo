using Microsoft.Win32;
using System;
using System.Threading.Tasks;

namespace Chemo.Treatment.Config
{
    class WindowsUpdateReboot : BaseTreatment
    {
        private const string AutoUpdateKey = @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\WindowsUpdate\AU";
        private const int DesiredValue = 2;

        public override string Name()
        {
            return "Disable Force-Reboot After Windows Update";
        }

        public override string Tooltip()
        {
            return "Prevents Windows from automatically rebooting after applying updates.";
        }

        public override Task<bool> ShouldPerformTreatment()
        {
            var value = Registry.GetValue(AutoUpdateKey, "AUOptions", 0);
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
                Registry.SetValue(AutoUpdateKey, "AUOptions", DesiredValue, RegistryValueKind.DWord);
                Logger.Log("Successfully disabled automatic reboot for Windows Update.");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Logger.Log("Could not disable automatic reboot for Windows Update: {0}", ex.Message);
            }

            return Task.FromResult(false);
        }
    }
}
