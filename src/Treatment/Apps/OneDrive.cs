using Chemo.Utilities;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;

namespace Chemo.Treatment.Apps
{
    internal sealed class OneDrive : BaseTreatment
    {
        private const string Clsid = "{018D5C66-4533-4307-9B53-224DE2ED1FE6}";
        private const string AutoRunKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run";
        private const string ClassKey = @"HKEY_CLASSES_ROOT\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}";
        private const string PolicyKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OneDrive";
        private const string PolicyValueName = "DisableFileSyncNGSC";
        private const int PolicyValue = 1;

        private static readonly string[] ProcessNames = ["OneDrive", "FileCoAuth"];

        private readonly string UninstallerPath;
        private readonly string UserDataPath;
        private readonly string ShortcutPath;
        private readonly string LocalAppDataPath;
        private readonly string ProgramDataPath;
        private readonly string ProgramFilesPath;

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

            // %PROGRAMFILES%\Microsoft OneDrive, where OneDrive runs from when it's installed for all users
            ProgramFilesPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "Microsoft OneDrive"
            );
        }

        #region Uninstaller
        private bool IsInstalled()
        {
            string executable = Path.Combine(LocalAppDataPath, "OneDrive.exe");
            return File.Exists(executable) && !PendingDeletes.Read().Contains(executable);
        }

        private void Uninstall()
        {
            if (!File.Exists(UninstallerPath))
            {
                Logger.Log("OneDrive uninstaller not found at {0}.", UninstallerPath);
                return;
            }

            using Process process = Process.Start(new ProcessStartInfo(UninstallerPath, "/uninstall") { UseShellExecute = false });
            process.WaitForExit();
            Logger.Log(process.ExitCode == 0
                ? "OneDrive uninstaller finished."
                : $"OneDrive uninstaller exited with code 0x{process.ExitCode:X8}.");
        }
        #endregion

        #region Processes
        /// <summary>
        /// Finds OneDrive and its helpers, such as its sync service, by name and by where they run from.
        /// </summary>
        private List<Process> FindProcesses()
        {
            string[] folders = [LocalAppDataPath, ProgramFilesPath];
            List<Process> found = [];

            foreach (Process process in Process.GetProcesses())
            {
                if (ProcessNames.Contains(process.ProcessName, StringComparer.OrdinalIgnoreCase) || IsRunningFrom(process, folders))
                {
                    found.Add(process);
                }
                else
                {
                    process.Dispose();
                }
            }

            return found;
        }

        private static bool IsRunningFrom(Process process, string[] folders)
        {
            try
            {
                string? path = process.MainModule?.FileName;
                return path != null && folders.Any(folder => path.StartsWith(folder + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex) when (ex is Win32Exception || ex is InvalidOperationException)
            {
                // Windows doesn't let us inspect its own protected processes, and none of them are OneDrive.
                return false;
            }
        }

        private bool ProcessesRunning()
        {
            List<Process> processes = FindProcesses();
            foreach (Process process in processes)
            {
                Logger.Log("Would stop {0} ({1}).", process.ProcessName, process.Id);
                process.Dispose();
            }

            return processes.Count > 0;
        }

        private void KillProcesses()
        {
            foreach (Process process in FindProcesses())
            {
                using (process)
                {
                    try
                    {
                        process.Kill();
                        process.WaitForExit(5000);
                        Logger.Log("Stopped {0} ({1}).", process.ProcessName, process.Id);
                    }
                    catch (Exception ex) when (ex is Win32Exception || ex is InvalidOperationException)
                    {
                        Logger.Log("Could not stop {0} ({1}): {2}", process.ProcessName, process.Id, ex.Message);
                    }
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
        private static bool RegistryKeysExist()
        {
            // The uninstaller deletes these values, so a missing value counts as removed.
            if (Registry.GetValue(AutoRunKey, "OneDrive", null) is string autoRun && autoRun.Length > 0)
            {
                return true;
            }

            if (Registry.GetValue(ClassKey, "System.IsPinnedToNameSpaceTree", null) is int pinned && pinned != 0)
            {
                return true;
            }

            if (Environment.Is64BitOperatingSystem)
            {
                using RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(@"Wow6432Node\CLSID\");
                if (regKey.OpenSubKey(Clsid) != null)
                {
                    return true;
                }
            }
            else
            {
                using RegistryKey regKey = Registry.ClassesRoot.OpenSubKey("CLSID");
                if (regKey.OpenSubKey(Clsid) != null)
                {
                    return true;
                }
            }

            return false;
        }

        private void DeleteRegistryKeys()
        {
            // Only unpin OneDrive from File Explorer's navigation pane if it's still registered there.
            if (Registry.GetValue(ClassKey, "System.IsPinnedToNameSpaceTree", null) != null)
            {
                Registry.SetValue(ClassKey, "System.IsPinnedToNameSpaceTree", 0, RegistryValueKind.DWord);
            }
            RegistryUtils.DeleteValue(AutoRunKey, "OneDrive");

            if (Environment.Is64BitOperatingSystem)
            {
                using RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(@"Wow6432Node\CLSID\", true);
                if (regKey.OpenSubKey(Clsid) != null)
                {
                    regKey.DeleteSubKeyTree(Clsid);
                }
            }
            else
            {
                using RegistryKey regKey = Registry.ClassesRoot.OpenSubKey("CLSID", true);
                if (regKey.OpenSubKey(Clsid) != null)
                {
                    regKey.DeleteSubKeyTree(Clsid);
                }
            }
        }
        #endregion

        #region Folders
        private void DeleteDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                return;
            }

            try
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    DeleteFile(file);
                }

                foreach (string dir in Directory.GetDirectories(path))
                {
                    DeleteDirectory(dir);
                }

                File.SetAttributes(path, FileAttributes.Normal);
                Directory.Delete(path, false);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                // Usually a file inside is in use and waiting to be deleted at restart. Windows deletes the
                // folder at restart too, once it's empty.
                Logger.Log("{0} will be deleted when Windows restarts, after the files in it.", path);
                DeleteOnReboot(path);
            }
            catch (Exception ex)
            {
                Logger.Log("Could not delete {0}: {1}", path, ex.Message);
            }
        }

        private void DeleteFile(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                // Files that are in use, like OneDrive's File Explorer extension, can only be deleted at restart.
                Logger.Log("{0} is in use and will be deleted when Windows restarts.", path);
                DeleteOnReboot(path);
            }
            catch (Exception ex)
            {
                Logger.Log("Could not delete {0}: {1}", path, ex.Message);
            }
        }

        private void DeleteOnReboot(string path)
        {
            if (!UnsafeNativeMethods.MoveFileEx(path, null, MoveFileFlags.DelayUntilReboot))
            {
                Logger.Log("Could not schedule {0} to be deleted at restart: {1}", path, new Win32Exception().Message);
            }
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

        /// <summary>
        /// Leftover OneDrive files and folders that still exist, including the user's OneDrive folder if it's empty.
        /// </summary>
        private List<string> Leftovers()
        {
            List<string> leftovers = new[] { LocalAppDataPath, ProgramDataPath }.Where(Directory.Exists).ToList();

            if (File.Exists(ShortcutPath))
            {
                leftovers.Add(ShortcutPath);
            }

            if (UserFolderIsEmpty())
            {
                leftovers.Add(UserDataPath);
            }

            return leftovers;
        }

        public override bool RestartPending()
        {
            HashSet<string> pendingDeletes = PendingDeletes.Read();
            return Leftovers().Any(pendingDeletes.Contains);
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

            List<string> leftovers = Leftovers();
            HashSet<string> pendingDeletes = PendingDeletes.Read();
            foreach (string path in leftovers)
            {
                if (pendingDeletes.Contains(path))
                {
                    Logger.Log("{0} will be deleted when Windows restarts.", path);
                }
                else
                {
                    Logger.Log("Would delete {0}.", path);
                    retval = true;
                }
            }

            if (leftovers.Count == 0)
            {
                Logger.Log("No leftover OneDrive folders are present.");
            }

            return retval;
        }

        public override bool PerformTreatment()
        {
            // OneDrive is stopped again after the uninstaller runs, because the uninstaller can leave helpers like its
            // sync service running, and their files can't be deleted until they stop.
            bool retval = true;
            retval &= Step("Stopping OneDrive", KillProcesses);
            retval &= Step("Running the OneDrive uninstaller", Uninstall);
            retval &= Step("Stopping anything the uninstaller left running", KillProcesses);
            retval &= Step("Setting the policy that prevents OneDrive from running", ApplyPolicy);
            retval &= Step("Removing OneDrive keys from the registry", DeleteRegistryKeys);
            retval &= Step("Deleting leftover OneDrive folders", DeleteFoldersAndFiles);
            return retval;
        }

        /// <summary>
        /// Logs and runs one part of the treatment, logging any failure so the remaining parts still run.
        /// </summary>
        private bool Step(string description, Action action)
        {
            Logger.Log("{0}...", description);

            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Failed: {0}", ex.Message);
                return false;
            }
        }
    }
}
