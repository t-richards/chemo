using Microsoft.Win32;
using System;

namespace Chemo.Treatment
{
    class RequireCtrlAltDel : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;

        public bool ShouldPerformTreatment()
        {
            return true;
        }

        public bool PerformTreatment()
        {
            try
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "DisableCAD", 0, RegistryValueKind.DWord);
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
