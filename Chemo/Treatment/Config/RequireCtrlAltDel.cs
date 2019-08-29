using Microsoft.Win32;
using System;

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

        public override bool ShouldPerformTreatment()
        {
            return !RegistryUtils.IntEquals(WinLogon, "DisableCAD", DesiredValue);
        }

        public override bool PerformTreatment()
        {
            try
            {
                Registry.SetValue(WinLogon, "DisableCAD", 0, RegistryValueKind.DWord);
                Logger.Log("Successfully required Ctrl-Alt-Delete for user login.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Could not require Ctrl-Alt-Delete for user login: {0}", ex.Message);
            }

            return false;
        }
    }
}
