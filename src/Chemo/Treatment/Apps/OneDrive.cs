using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace Chemo.Treatment.Apps
{
    class OneDrive : BaseTreatment
    {
        private const string Clsid = "{018D5C66-4533-4307-9B53-224DE2ED1FE6}";
        private const string AutoRunKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run";
        private const string ClassKey = @"HKEY_CLASSES_ROOT\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}";

        private readonly string UserDataPath;
        private readonly string ShortcutPath;
        private readonly string LocalAppDataPath;
        private readonly string ProgramDataPath;

        public override string Name()
        {
            return "Remove OneDrive";
        }

        public override string Tooltip()
        {
            return "Completely removes OneDrive including ALL ONEDRIVE DATA.";
        }

        public OneDrive()
        {
            // %USERPROFILE%\OneDrive
            UserDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "OneDrive"
            );

            // %APPDATA%\Microsoft\Windows\StartMenu\Programs\OneDrive
            ShortcutPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Microsoft", "Windows", "Start Menu", "Programs", "OneDrive.lnk"
            );

            // %LOCALAPPDATA%\Microsoft\OneDrive
            LocalAppDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Microsoft", "OneDrive"
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
        private bool DeleteDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                foreach (string file in files)
                {
                    DeleteFile(file);
                }

                foreach (string dir in directories)
                {
                    DeleteDirectory(dir);
                }

                File.SetAttributes(path, FileAttributes.Normal);
                Directory.Delete(path, false);
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.Log("Deleting {0} on reboot: {1}", path, ex.Message);
                return DeleteOnReboot(path);
            }
            catch (Exception ex)
            {
                Logger.Log("Could not delete {0}: {1}", path, ex.Message);
            }

            return false;
        }

        private bool DeleteFile(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            try
            {
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.Log("Deleting {0} on reboot: {1}", path, ex.Message);
                return DeleteOnReboot(path);
            }
            catch (Exception ex)
            {
                Logger.Log("Could not delete {0}: {1}", path, ex.Message);
            }

            return false;
        }

        private static bool DeleteOnReboot(string path)
        {
            return UnsafeNativeMethods.MoveFileEx(path, null, MoveFileFlags.DelayUntilReboot);
        }

        private bool FoldersExist()
        {
            return (
                Directory.Exists(UserDataPath) ||
                Directory.Exists(LocalAppDataPath) ||
                Directory.Exists(ProgramDataPath) ||
                File.Exists(ShortcutPath)
            );
        }

        private void DeleteFoldersAndFiles()
        {
            DeleteDirectory(UserDataPath);
            DeleteDirectory(LocalAppDataPath);
            DeleteDirectory(ProgramDataPath);
            DeleteFile(ShortcutPath);
        }
        #endregion

        public override bool ShouldPerformTreatment()
        {
            bool retval = false;

            if (ProcessesRunning())
            {
                Logger.Log("Would kill one or more running OneDrive processes.");
                retval = true;
            }
            else
            {
                Logger.Log("No OneDrive processes are running.");
            }

            if (RegistryKeysExist())
            {
                Logger.Log("Would remove one or more OneDrive registry keys.");
                retval = true;
            }
            else
            {
                Logger.Log("No OneDrive registry keys are present.");
            }

            if (FoldersExist())
            {
                Logger.Log("Would delete one or more OneDrive folders.");
                retval = true;
            }
            else
            {
                Logger.Log("No OneDrive folders are present.");
            }

            return retval;
        }

        public override bool PerformTreatment()
        {
            bool retval = true;

            try
            {
                Logger.Log("Terminating any running OneDrive processes...");
                KillProcesses();
                Logger.Log("Completed termination of running OneDrive processes");
            }
            catch (Exception ex)
            {
                Logger.Log("Could not kill running OneDrive processes: {0}", ex.Message);
                retval = false;
            }

            try
            {
                Logger.Log("Removing OneDrive keys from registry...");
                DeleteRegistryKeys();
                Logger.Log("Completed removal of OneDrive keys from registry");
            }
            catch (Exception ex)
            {
                Logger.Log("Could not remove OneDrive keys from registry: {0}", ex.Message);
                retval = false;
            }

            try
            {
                Logger.Log("Deleting OneDrive folders completely...");
                DeleteFoldersAndFiles();
                Logger.Log("Completed removal of OneDrive folders.");
            }
            catch (Exception ex)
            {
                Logger.Log("Could not delete OneDrive folders: {0}", ex.Message);
                retval = false;
            }

            return retval;
        }
    }
}
