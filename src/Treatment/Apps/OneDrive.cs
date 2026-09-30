using Chemo.Settings;
using Chemo.Utilities;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;

namespace Chemo.Treatment.Apps
{
    internal sealed class OneDrive : SettingsTreatment
    {
        private const string ClassKey = @"HKEY_CLASSES_ROOT\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}";

        private static readonly string[] ProcessNames = ["OneDrive", "FileCoAuth"];

        // %SystemRoot%\System32\OneDriveSetup.exe
        private static readonly string UninstallerPath = Path.Combine(Environment.SystemDirectory, "OneDriveSetup.exe");

        // %OneDrive%, usually %USERPROFILE%\OneDrive
        private static readonly string UserDataPath = Environment.GetEnvironmentVariable("OneDrive") is { Length: > 0 } oneDrive
            ? oneDrive
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "OneDrive");

        // %APPDATA%\Microsoft\Windows\Start Menu\Programs\OneDrive.lnk
        private static readonly string ShortcutPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Microsoft", "Windows", "Start Menu", "Programs", "OneDrive.lnk");

        // %LOCALAPPDATA%\Microsoft\OneDrive
        private static readonly string LocalAppDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "OneDrive");

        // %PROGRAMDATA%\Microsoft OneDrive
        private static readonly string ProgramDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft OneDrive");

        // %PROGRAMFILES%\Microsoft OneDrive, where OneDrive runs from when it's installed for all users
        private static readonly string ProgramFilesPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Microsoft OneDrive");

        public override string Name => "Remove OneDrive";

        public override string Description => "Uninstalls OneDrive and stops it from running for everyone on this PC. Files already in your OneDrive folder stay where they are.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new Uninstalled(Logger),

                // The uninstaller can leave helpers like OneDrive's sync service running, and their files can't be
                // deleted until they stop.
                new NotRunning(Logger),

                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OneDrive", "DisableFileSyncNGSC", 1),
                new DeletedRegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "OneDrive"),
                new UnpinnedFromExplorer(),
                new DeletedRegistryKey(@"HKEY_CLASSES_ROOT\Wow6432Node\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}"),
                new LeftoversDeleted(Logger),
            ];
        }

        public override bool RestartPending()
        {
            HashSet<string> pendingDeletes = PendingDeletes.Read();
            return Leftovers().Exists(pendingDeletes.Contains);
        }

        /// <summary>
        /// Finds OneDrive and its helpers, such as its sync service, by name and by where they run from.
        /// </summary>
        private static List<Process> FindProcesses()
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
            catch (Exception ex) when (ex is Win32Exception or InvalidOperationException)
            {
                // Windows doesn't let us inspect its own protected processes, and none of them are OneDrive.
                return false;
            }
        }

        private static void StopProcesses(MemoryLogger logger)
        {
            foreach (Process process in FindProcesses())
            {
                using (process)
                {
                    try
                    {
                        process.Kill();
                        process.WaitForExit(5000);
                        logger.Log("Stopped {0} ({1}).", process.ProcessName, process.Id);
                    }
                    catch (Exception ex) when (ex is Win32Exception or InvalidOperationException)
                    {
                        logger.Log("Could not stop {0} ({1}): {2}", process.ProcessName, process.Id, ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// The user's OneDrive folder is only removed when nothing but its desktop.ini remains.
        /// </summary>
        private static bool UserFolderIsEmpty()
        {
            return Directory.Exists(UserDataPath) && Directory.EnumerateFileSystemEntries(UserDataPath)
                .All(entry => Path.GetFileName(entry).Equals("desktop.ini", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Leftover OneDrive files and folders that still exist, including the user's OneDrive folder if it's empty.
        /// </summary>
        private static List<string> Leftovers()
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

        private sealed class Uninstalled : ISetting
        {
            private readonly MemoryLogger logger;

            public Uninstalled(MemoryLogger logger)
            {
                this.logger = logger;
            }

            public bool IsApplied()
            {
                string executable = Path.Combine(LocalAppDataPath, "OneDrive.exe");
                return !File.Exists(executable) || PendingDeletes.Read().Contains(executable);
            }

            public void Apply()
            {
                StopProcesses(logger);

                // Without the uninstaller, deleting the leftovers still removes OneDrive.
                if (!File.Exists(UninstallerPath))
                {
                    logger.Log("OneDrive uninstaller not found at {0}.", UninstallerPath);
                    return;
                }

                using Process process = Process.Start(new ProcessStartInfo(UninstallerPath, "/uninstall") { UseShellExecute = false });
                process.WaitForExit();
                logger.Log(process.ExitCode == 0
                    ? "OneDrive uninstaller finished."
                    : $"OneDrive uninstaller exited with code 0x{process.ExitCode:X8}.");
            }

            public override string ToString()
            {
                return "OneDrive is uninstalled";
            }
        }

        private sealed class NotRunning : ISetting
        {
            private readonly MemoryLogger logger;

            public NotRunning(MemoryLogger logger)
            {
                this.logger = logger;
            }

            /// <returns>The name and ID of each OneDrive process that's running.</returns>
            private static List<string> RunningProcesses()
            {
                List<string> running = [];

                foreach (Process process in FindProcesses())
                {
                    using (process)
                    {
                        running.Add($"{process.ProcessName} ({process.Id})");
                    }
                }

                return running;
            }

            public bool IsApplied()
            {
                List<string> running = RunningProcesses();

                foreach (string process in running)
                {
                    logger.Log("{0} is running", process);
                }

                return running.Count == 0;
            }

            public void Apply()
            {
                StopProcesses(logger);

                List<string> running = RunningProcesses();
                if (running.Count > 0)
                {
                    throw new InvalidOperationException($"Could not stop {string.Join(", ", running)}.");
                }
            }

            public override string ToString()
            {
                return "OneDrive isn't running";
            }
        }

        /// <summary>
        /// Only changes the value while OneDrive is still registered, so the key isn't created for nothing.
        /// </summary>
        private sealed class UnpinnedFromExplorer : ISetting
        {
            public bool IsApplied()
            {
                return !(Registry.GetValue(ClassKey, "System.IsPinnedToNameSpaceTree", null) is int pinned && pinned != 0);
            }

            public void Apply()
            {
                Registry.SetValue(ClassKey, "System.IsPinnedToNameSpaceTree", 0, RegistryValueKind.DWord);
            }

            public override string ToString()
            {
                return "OneDrive isn't pinned to File Explorer's navigation pane";
            }
        }

        private sealed class LeftoversDeleted : ISetting
        {
            private readonly MemoryLogger logger;

            public LeftoversDeleted(MemoryLogger logger)
            {
                this.logger = logger;
            }

            // Leftovers Windows deletes at the next restart count as deleted, and RestartPending reports them.
            public bool IsApplied()
            {
                HashSet<string> pendingDeletes = PendingDeletes.Read();
                bool applied = true;

                foreach (string path in Leftovers())
                {
                    if (pendingDeletes.Contains(path))
                    {
                        logger.Log("{0} will be deleted when Windows restarts.", path);
                    }
                    else
                    {
                        logger.Log("{0} is left over.", path);
                        applied = false;
                    }
                }

                return applied;
            }

            public void Apply()
            {
                DeleteDirectory(LocalAppDataPath);
                DeleteDirectory(ProgramDataPath);
                DeleteFile(ShortcutPath);
                DeleteUserFolderIfEmpty();
            }

            public override string ToString()
            {
                return "Leftover OneDrive files and folders are deleted";
            }

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
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
                {
                    // Usually a file inside is in use and waiting to be deleted at restart. Windows deletes the
                    // folder at restart too, once it's empty.
                    logger.Log("{0} will be deleted when Windows restarts, after the files in it.", path);
                    DeleteOnReboot(path);
                }
                catch (Exception ex)
                {
                    logger.Log("Could not delete {0}: {1}", path, ex.Message);
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
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
                {
                    // Files that are in use, like OneDrive's File Explorer extension, can only be deleted at restart.
                    logger.Log("{0} is in use and will be deleted when Windows restarts.", path);
                    DeleteOnReboot(path);
                }
                catch (Exception ex)
                {
                    logger.Log("Could not delete {0}: {1}", path, ex.Message);
                }
            }

            private void DeleteOnReboot(string path)
            {
                if (!UnsafeNativeMethods.MoveFileEx(path, null, MoveFileFlags.DelayUntilReboot))
                {
                    logger.Log("Could not schedule {0} to be deleted at restart: {1}", path, new Win32Exception().Message);
                }
            }

            private void DeleteUserFolderIfEmpty()
            {
                if (!Directory.Exists(UserDataPath))
                {
                    return;
                }

                if (!UserFolderIsEmpty())
                {
                    logger.Log("Keeping {0} because it still contains files.", UserDataPath);
                    return;
                }

                DeleteDirectory(UserDataPath);
                if (!Directory.Exists(UserDataPath))
                {
                    Environment.SetEnvironmentVariable("OneDrive", null, EnvironmentVariableTarget.User);
                    logger.Log("Removed empty folder {0}.", UserDataPath);
                }
            }
        }
    }
}
