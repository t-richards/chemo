using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace Chemo.Treatment
{
    class OneDrive : BaseTreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static readonly string Clsid = "{018D5C66-4533-4307-9B53-224DE2ED1FE6}";
        private static readonly string AutoRunKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run";
        private static readonly string ClassKey = @"HKEY_CLASSES_ROOT\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}";

        private string UserDataPath;
        private string LocalAppDataPath;
        private string ProgramDataPath;

        public OneDrive()
        {
            // %USERPROFILE%\OneDrive
            UserDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "OneDrive"
            );

            // %LOCALAPPDATA%\Microsoft\OneDrive
            LocalAppDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Local", "Microsoft", "OneDrive"
            );

            // %PROGRAMDATA%\Microsoft OneDrive
            ProgramDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "Microsoft OneDrive"
            );
        }
        #region Processes
        private bool ProcessesRunning()
        {
            Process[] procs = Process.GetProcessesByName("OneDrive");
            return (procs.Length > 0);
        }

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
        #endregion

        #region Registry Keys
        private bool RegistryKeysExist()
        {
            if (!RegistryUtils.StringEquals(AutoRunKey, "OneDrive", ""))
            {
                return true;
            }

            if (!RegistryUtils.IntEquals(ClassKey, "System.IsPinnedToNameSpaceTree", 0))
            {
                return true;
            }

            if (Environment.Is64BitOperatingSystem)
            {
                using (RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(@"Wow6432Node\CLSID\", true))
                {
                    if (regKey.OpenSubKey(Clsid) != null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                using (RegistryKey regKey = Registry.ClassesRoot.OpenSubKey("CLSID", true))
                {
                    if (regKey.OpenSubKey(Clsid) != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void DeleteRegistryKeys()
        {
            Registry.SetValue(ClassKey, "System.IsPinnedToNameSpaceTree", 0, RegistryValueKind.DWord);
            Registry.SetValue(AutoRunKey, "OneDrive", "", RegistryValueKind.String);

            if (Environment.Is64BitOperatingSystem)
            {
                using (RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(@"Wow6432Node\CLSID\", true))
                {
                    if (regKey.OpenSubKey(Clsid) != null)
                    {
                        regKey.DeleteSubKeyTree(Clsid);
                    }
                }
            }
            else
            {
                using (RegistryKey regKey = Registry.ClassesRoot.OpenSubKey("CLSID", true))
                {
                    if (regKey.OpenSubKey(Clsid) != null)
                    {
                        regKey.DeleteSubKeyTree(Clsid);
                    }
                }
            }
        }
        #endregion

        #region Folders
        private bool MaybeDelete(string path)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }

            try
            {
                var di = new DirectoryInfo(path);
                di.Attributes &= ~FileAttributes.ReadOnly;
                Directory.Delete(path, true);
                return true;
            }
            catch (Exception ex)
            {
                logger.Log("Could not delete {0}: {1}", path, ex.Message);
            }

            return false;
        }

        private bool FoldersExist()
        {
            return (
                Directory.Exists(UserDataPath) ||
                Directory.Exists(LocalAppDataPath) ||
                Directory.Exists(ProgramDataPath)
            );
        }

        private void DeleteFolders()
        {
            MaybeDelete(UserDataPath);
            MaybeDelete(LocalAppDataPath);
            MaybeDelete(ProgramDataPath);
        }
        #endregion

        public override bool ShouldPerformTreatment()
        {
            return (
                ProcessesRunning() ||
                RegistryKeysExist() ||
                FoldersExist()
            );
        }

        public override bool PerformTreatment()
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
