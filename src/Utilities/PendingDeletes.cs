using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace Chemo.Utilities
{
    /// <summary>
    /// The files and folders Windows will delete the next time it restarts.
    /// </summary>
    public static class PendingDeletes
    {
        private const string SessionManagerKey = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager";

        /// <summary>
        /// Reads the paths scheduled for deletion, such as with MoveFileEx and MOVEFILE_DELAY_UNTIL_REBOOT.
        /// </summary>
        public static HashSet<string> Read()
        {
            HashSet<string> paths = Parse(Registry.GetValue(SessionManagerKey, "PendingFileRenameOperations", null) as string[]);
            paths.UnionWith(Parse(Registry.GetValue(SessionManagerKey, "PendingFileRenameOperations2", null) as string[]));
            return paths;
        }

        /// <summary>
        /// The registry lists pairs of NT paths: the file to move, then where to move it. An empty destination
        /// means the file is deleted. Windows 11 writes paths like "*1\??\C:\path", with a prefix before the "\??\".
        /// </summary>
        public static HashSet<string> Parse(string[] operations)
        {
            HashSet<string> paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (operations == null)
            {
                return paths;
            }

            for (int i = 0; i + 1 < operations.Length; i += 2)
            {
                if (operations[i + 1].Length == 0)
                {
                    string source = operations[i];
                    int prefixEnd = source.IndexOf(@"\??\", StringComparison.Ordinal);
                    paths.Add(prefixEnd >= 0 ? source.Substring(prefixEnd + 4) : source);
                }
            }

            return paths;
        }
    }
}
