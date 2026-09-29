using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Chemo.Treatment.Apps
{
    class OneDrive : BaseTreatment
    {
        private const string Clsid = "{018D5C66-4533-4307-9B53-224DE2ED1FE6}";
        private const string AutoRunKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run";
        private const string ClassKey = @"HKEY_CLASSES_ROOT\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}";
        private const string PolicyKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OneDrive";
        private const string PolicyValueName = "DisableFileSyncNGSC";
        private const int PolicyValue = 1;

        private static readonly string[] ProcessNames = { "OneDrive", "FileCoAuth" };

        private readonly string UninstallerPath;
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
            return "Uninstalls OneDrive and prevents it from running for all users. Files in your OneDrive folder are kept.";
        }

        public OneDrive()
        {
            // %SystemRoot%\System32\OneDriveSetup.exe
            UninstallerPath = Path.Combine(Environment.SystemDirectory, "OneDriveSetup.exe");

            // %OneDrive%, usually %USERPROFILE%\OneDrive
            UserDataPath = Environment.GetEnvironmentVariable("OneDrive");
            if (string.IsNullOrEmpty(UserDataPath))
            {
                UserDataPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "OneDrive"
                );
            }

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

        #region Uninstaller
        private bool IsInstalled()
        {
            return File.Exists(Path.Combine(LocalAppDataPath, "OneDrive.exe"));
        }

        private void Uninstall()
        {
            if (!File.Exists(UninstallerPath))
            {
                Logger.Log("OneDrive uninstaller not found at {0}.", UninstallerPath);
                return;
            }

            using (Process process = Process.Start(new ProcessStartInfo(UninstallerPath, "/uninstall") { UseShellExecute = false }))
            {
                process.WaitForExit();
                Logger.Log("OneDrive uninstaller exited with code {0}.", process.ExitCode);
            }
        }
        #endregion

        #region Processes
        private static bool ProcessesRunning()
        {
            return ProcessNames.Any(name => Process.GetProcessesByName(name).Length > 0);
        }

        private static void KillProcesses()
        {
            foreach (string name in ProcessNames)
            {
                foreach (Process proc in Process.GetProcessesByName(name))
                {
                    proc.Kill();
                    proc.WaitForExit(5000);
                }
            }
        }
        #endregion

        #region Policy
        private static bool PolicyApplied()
        {
            return RegistryUtils.IntEquals(PolicyKey, PolicyValueName, PolicyValue);
        }

        private static void ApplyPolicy()
        {
            Registry.SetValue(PolicyKey, PolicyValueName, PolicyValue, RegistryValueKind.DWord);
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

        /// <summary>
        /// The user's OneDrive folder is only removed when nothing but its desktop.ini remains.
        /// </summary>
        private bool UserFolderIsEmpty()
        {
            return Directory.Exists(UserDataPath) && Directory.EnumerateFileSystemEntries(UserDataPath)
                .All(entry => Path.GetFileName(entry).Equals("desktop.ini", StringComparison.OrdinalIgnoreCase));
        }

        private void DeleteUserFolderIfEmpty()
        {
            if (!Directory.Exists(UserDataPath))
            {
                return;
            }

            if (!UserFolderIsEmpty())
            {
                Logger.Log("Keeping {0} because it still contains files.", UserDataPath);
                return;
            }

            DeleteDirectory(UserDataPath);
            if (!Directory.Exists(UserDataPath))
            {
                Environment.SetEnvironmentVariable("OneDrive", null, EnvironmentVariableTarget.User);
                Logger.Log("Removed empty folder {0}.", UserDataPath);
            }
        }

        private bool FoldersExist()
        {
            return (
                UserFolderIsEmpty() ||
                Directory.Exists(LocalAppDataPath) ||
                Directory.Exists(ProgramDataPath) ||
                File.Exists(ShortcutPath)
            );
        }

        private void DeleteFoldersAndFiles()
        {
            DeleteDirectory(LocalAppDataPath);
            DeleteDirectory(ProgramDataPath);
            DeleteFile(ShortcutPath);
            DeleteUserFolderIfEmpty();
        }
        #endregion

        public override bool ShouldPerformTreatment()
        {
            bool retval = false;

            if (IsInstalled())
            {
                Logger.Log("Would uninstall OneDrive.");
                retval = true;
            }
            else
            {
                Logger.Log("OneDrive is not installed.");
            }

            if (ProcessesRunning())
            {
                Logger.Log("Would kill one or more running OneDrive processes.");
                retval = true;
            }
            else
            {
                Logger.Log("No OneDrive processes are running.");
            }

            if (!PolicyApplied())
            {
                Logger.Log("Would set the policy that prevents OneDrive from running.");
                retval = true;
            }
            else
            {
                Logger.Log("OneDrive is already prevented from running by policy.");
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
                Logger.Log("Would delete one or more leftover OneDrive folders.");
                retval = true;
            }
            else
            {
                Logger.Log("No leftover OneDrive folders are present.");
            }

            return retval;
        }

        public override bool PerformTreatment()
        {
            bool retval = true;

            try
            {
                Logger.Log("Running the OneDrive uninstaller...");
                Uninstall();
            }
            catch (Exception ex)
            {
                Logger.Log("Could not run the OneDrive uninstaller: {0}", ex.Message);
                retval = false;
            }

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
                Logger.Log("Setting the policy that prevents OneDrive from running...");
                ApplyPolicy();
                Logger.Log("Completed setting the OneDrive policy.");
            }
            catch (Exception ex)
            {
                Logger.Log("Could not set the OneDrive policy: {0}", ex.Message);
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
                Logger.Log("Deleting leftover OneDrive folders...");
                DeleteFoldersAndFiles();
                Logger.Log("Completed removal of leftover OneDrive folders.");
            }
            catch (Exception ex)
            {
                Logger.Log("Could not delete leftover OneDrive folders: {0}", ex.Message);
                retval = false;
            }

            return retval;
        }
    }
}
