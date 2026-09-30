using Microsoft.Win32;
using System.Diagnostics;

namespace Chemo.Utilities
{
    internal static class ExplorerShell
    {
        private const string Winlogon = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
        private static readonly TimeSpan RestartTimeout = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Restarts the File Explorer behind the desktop and taskbar, so it picks up settings it only reads when it
        /// starts. Open File Explorer windows close.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// File Explorer isn't running the desktop, or Windows is set not to restart it.
        /// </exception>
        public static void Restart()
        {
            // Windows starts File Explorer again whenever it stops unexpectedly, as the signed-in user rather than as an
            // administrator like Chemo. If that's been turned off, stopping File Explorer would leave the PC without a
            // taskbar.
            if (Registry.GetValue(Winlogon, "AutoRestartShell", null) is not int autoRestart || autoRestart == 0)
            {
                throw new InvalidOperationException("Windows is set not to restart File Explorer.");
            }

            int oldId = ShellProcessId();
            if (oldId == 0)
            {
                throw new InvalidOperationException("File Explorer isn't running.");
            }

            using (Process shell = Process.GetProcessById(oldId))
            {
                if (!string.Equals(shell.ProcessName, "explorer", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException($"{shell.ProcessName} is running the desktop instead of File Explorer.");
                }

                shell.Kill();
                _ = shell.WaitForExit((int)RestartTimeout.TotalMilliseconds);
            }

            Stopwatch waiting = Stopwatch.StartNew();
            while (waiting.Elapsed < RestartTimeout)
            {
                int newId = ShellProcessId();
                if (newId != 0 && newId != oldId)
                {
                    return;
                }

                Thread.Sleep(250);
            }

            // Windows didn't bring it back, so start it rather than leave the PC without a taskbar.
            Process.Start("explorer.exe")?.Dispose();
        }

        /// <summary>
        /// Finds the process behind this session's desktop, so the File Explorer behind anyone else's desktop on this
        /// PC is left alone.
        /// </summary>
        /// <returns>The process's ID, or 0 if nothing is running the desktop.</returns>
        private static int ShellProcessId()
        {
            IntPtr desktop = UnsafeNativeMethods.GetShellWindow();
            if (desktop == IntPtr.Zero)
            {
                return 0;
            }

            _ = UnsafeNativeMethods.GetWindowThreadProcessId(desktop, out uint processId);
            return (int)processId;
        }
    }
}
