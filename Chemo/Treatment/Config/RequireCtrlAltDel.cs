using Microsoft.Win32;
using System;
using System.Threading.Tasks;

namespace Chemo.Treatment.Config
{
    class RequireCtrlAltDel : BaseTreatment
    {
        private const string WinLogon = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
        private const int DesiredValue = 0;

        public override string Name()
        {
            return "Require Ctrl+Alt+Del at Sign In";
        }

        public override string Tooltip()
        {
            return "Requires the user to press Ctrl+Alt+Del at the sign in screen for security reasons.";
        }

        public override Task<bool> ShouldPerformTreatment()
        {
            return Task.FromResult(
                !RegistryUtils.IntEquals(WinLogon, "DisableCAD", DesiredValue)
            );
        }

        public override Task<bool> PerformTreatment()
        {
            try
            {
                Registry.SetValue(WinLogon, "DisableCAD", 0, RegistryValueKind.DWord);
                Logger.Log("Successfully required Ctrl-Alt-Delete for user login.");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Logger.Log("Could not require Ctrl-Alt-Delete for user login: {0}", ex.Message);
            }

            return Task.FromResult(false);
        }
    }
}
