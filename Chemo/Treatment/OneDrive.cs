using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace Chemo.Treatment
{
    class OneDrive : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;

        private void KillProcesses()
        {
            Process[] procs = Process.GetProcessesByName("OneDrive");
            foreach (var proc in procs)
            {
                if (proc != null)
                {
                    proc.Kill();
                }
            }
        }

        private void DeleteRegistryKeys()
        {
            RegistryKey reg;
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run", "OneDrive", "");

            if (Environment.Is64BitOperatingSystem)
            {
                reg = Registry.ClassesRoot.OpenSubKey("Wow6432Node\\CLSID", true);
                if (reg != null)
                {
                    reg.DeleteSubKeyTree("{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
                }
                return;
            }

            reg = Registry.ClassesRoot.OpenSubKey("CLSID", true);
            if (reg != null)
            {
                reg.DeleteSubKeyTree("{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
            }
        }

        public bool MaybeDelete(string path)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }

            try
            {
                Directory.Delete(path, true);
                return true;
            }
            catch (Exception ex)
            {
                logger.Log("Could not delete {0}: {1}", path, ex.Message);
            }

            return false;
        }

        public void DeleteFolders()
        {
            // %USERPROFILE%\OneDrive
            string userData = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "OneDrive"
            );
            MaybeDelete(userData);

            // %LOCALAPPDATA%\Microsoft\OneDrive
            string localAppData = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Local", "Microsoft", "OneDrive"
            );
            MaybeDelete(localAppData);

            // %PROGRAMDATA%\Microsoft OneDrive
            string programData = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "Microsoft OneDrive"
            );
            MaybeDelete(programData);
        }

        public bool ShouldPerformTreatment()
        {
            return true;
        }

        public bool PerformTreatment()
        {
            bool retval = true;

            try
            {
                logger.Log("Terminating any running OneDrive processes...");
                KillProcesses();
                logger.Log("Completed termination of running OneDrive processes");
            }
            catch (Exception ex)
            {
                logger.Log("Could not kill running OneDrive processes: {0}", ex.Message);
                retval = false;
            }

            try
            {
                logger.Log("Removing OneDrive keys from registry...");
                DeleteRegistryKeys();
                logger.Log("Completed removal of OneDrive keys from registry");
            }
            catch (Exception ex)
            {
                logger.Log("Could not remove OneDrive keys from registry: {0}", ex.Message);
                retval = false;
            }

            try
            {
                logger.Log("Deleting OneDrive folders completely...");
                DeleteFolders();
                logger.Log("Completed removal of OneDrive folders.");
            }
            catch (Exception ex)
            {
                logger.Log("Could not delete OneDrive folders: {0}", ex.Message);
                retval = false;
            }

            return retval;
        }
    }
}
