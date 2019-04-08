using Microsoft.Win32;
using System;

namespace Chemo.Treatment.Config
{
    class RequireCtrlAltDel : BaseTreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static readonly string WinLogon = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
        private static readonly int DesiredValue = 0;

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
                logger.Log("Successfully required Ctrl-Alt-Delete for user login.");
                return true;
            }
            catch (Exception ex)
            {
                logger.Log("Could not require Ctrl-Alt-Delete for user login: {0}", ex.Message);
            }

            return false;
        }
    }
}
