using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Chemo.Utilities
{
    /// <summary>
    /// Finds and unpins taskbar pins through the "Unpin from taskbar" command on each app in Start's All apps list.
    /// Windows 11 no longer lets programs pin apps to the taskbar, but this command still unpins them.
    /// </summary>
    internal static class TaskbarPins
    {
        // The command's name in every language
        private const string UnpinVerb = "taskbarunpin";

        private static readonly Guid BHID_EnumItems = new Guid("94f60519-2850-4924-aa5a-d15e84868039");
        private static readonly Guid BHID_SFUIObject = new Guid("3981e225-f559-11d3-8e3a-00c04f6837d5");
        private const uint SIGDN_PARENTRELATIVEPARSING = 0x80018001;
        private const uint CMF_NORMAL = 0x0;
        private const uint GCS_VERBW = 0x4;
        private const uint CMIC_MASK_FLAG_NO_UI = 0x400;
        private const int SW_SHOWNORMAL = 1;
        private const uint FirstCommand = 1;
        private const uint LastCommand = 0x7FFF;

        [ComImport, Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellItem
        {
            void BindToHandler(IntPtr pbc, [MarshalAs(UnmanagedType.LPStruct)] Guid bhid, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);
            void GetParent(out IShellItem ppsi);
            void GetDisplayName(uint sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
        }

        [ComImport, Guid("70629033-e363-4a28-a567-0db78006e6d7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IEnumShellItems
        {
            [PreserveSig]
            int Next(uint celt, out IShellItem rgelt, out uint pceltFetched);
        }

        [ComImport, Guid("000214e4-0000-0000-c000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IContextMenu
        {
            [PreserveSig]
            int QueryContextMenu(IntPtr hmenu, uint indexMenu, uint idCmdFirst, uint idCmdLast, uint uFlags);

            void InvokeCommand(ref CMINVOKECOMMANDINFO pici);

            [PreserveSig]
            int GetCommandString(UIntPtr idCmd, uint uType, IntPtr pReserved, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, uint cchMax);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CMINVOKECOMMANDINFO
        {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            public IntPtr lpVerb;
            public IntPtr lpParameters;
            public IntPtr lpDirectory;
            public int nShow;
            public uint dwHotKey;
            public IntPtr hIcon;
        }

        /// <summary>
        /// Finds the apps pinned to the taskbar.
        /// </summary>
        /// <returns>Their app IDs, such as Microsoft.Windows.Explorer for File Explorer.</returns>
        public static List<string> FindPinnedApps()
        {
            return OnStaThread(() =>
            {
                List<string> pinned = new List<string>();
                VisitPinnedApps((appId, unpin) => pinned.Add(appId));
                return pinned;
            });
        }

        /// <summary>
        /// Unpins the apps pinned to the taskbar that match.
        /// </summary>
        /// <returns>The app IDs of the apps that were unpinned.</returns>
        public static List<string> UnpinApps(Predicate<string> shouldUnpin)
        {
            return OnStaThread(() =>
            {
                List<string> unpinned = new List<string>();
                VisitPinnedApps((appId, unpin) =>
                {
                    if (shouldUnpin(appId))
                    {
                        unpin();
                        unpinned.Add(appId);
                    }
                });
                return unpinned;
            });
        }

        /// <summary>
        /// Calls visit with the app ID of each app pinned to the taskbar, and an action that unpins it.
        /// </summary>
        private static void VisitPinnedApps(Action<string, Action> visit)
        {
            UnsafeNativeMethods.SHCreateItemFromParsingName("shell:AppsFolder", IntPtr.Zero, typeof(IShellItem).GUID, out object folderObject);
            IShellItem appsFolder = (IShellItem)folderObject;

            try
            {
                appsFolder.BindToHandler(IntPtr.Zero, BHID_EnumItems, typeof(IEnumShellItems).GUID, out object itemsObject);
                IEnumShellItems items = (IEnumShellItems)itemsObject;

                try
                {
                    while (items.Next(1, out IShellItem item, out uint fetched) == 0 && fetched == 1)
                    {
                        try
                        {
                            VisitIfPinned(item, visit);
                        }
                        finally
                        {
                            Marshal.ReleaseComObject(item);
                        }
                    }
                }
                finally
                {
                    Marshal.ReleaseComObject(items);
                }
            }
            finally
            {
                Marshal.ReleaseComObject(appsFolder);
            }
        }

        private static void VisitIfPinned(IShellItem item, Action<string, Action> visit)
        {
            IContextMenu menu;
            try
            {
                item.BindToHandler(IntPtr.Zero, BHID_SFUIObject, typeof(IContextMenu).GUID, out object menuObject);
                menu = (IContextMenu)menuObject;
            }
            catch (COMException)
            {
                // Some entries have no menu, and nothing to unpin.
                return;
            }

            IntPtr hmenu = UnsafeNativeMethods.CreatePopupMenu();
            try
            {
                menu.QueryContextMenu(hmenu, 0, FirstCommand, LastCommand, CMF_NORMAL);
                uint command = FindCommand(menu, hmenu, UnpinVerb);
                if (command == 0)
                {
                    return;
                }

                item.GetDisplayName(SIGDN_PARENTRELATIVEPARSING, out string appId);
                visit(appId, () =>
                {
                    CMINVOKECOMMANDINFO info = new CMINVOKECOMMANDINFO
                    {
                        cbSize = Marshal.SizeOf(typeof(CMINVOKECOMMANDINFO)),
                        fMask = CMIC_MASK_FLAG_NO_UI,
                        lpVerb = (IntPtr)(command - FirstCommand),
                        nShow = SW_SHOWNORMAL,
                    };
                    menu.InvokeCommand(ref info);
                });
            }
            finally
            {
                UnsafeNativeMethods.DestroyMenu(hmenu);
                Marshal.ReleaseComObject(menu);
            }
        }

        /// <returns>The menu's command with the given language-independent name, or 0 if it has none.</returns>
        private static uint FindCommand(IContextMenu menu, IntPtr hmenu, string verb)
        {
            int count = UnsafeNativeMethods.GetMenuItemCount(hmenu);
            for (int position = 0; position < count; position++)
            {
                // Separators and submenus fall outside the range of commands.
                uint command = UnsafeNativeMethods.GetMenuItemID(hmenu, position);
                if (command < FirstCommand || command > LastCommand)
                {
                    continue;
                }

                StringBuilder name = new StringBuilder(64);
                if (menu.GetCommandString((UIntPtr)(command - FirstCommand), GCS_VERBW, IntPtr.Zero, name, (uint)name.Capacity) == 0 &&
                    string.Equals(name.ToString(), verb, StringComparison.OrdinalIgnoreCase))
                {
                    return command;
                }
            }

            return 0;
        }

        /// <summary>
        /// Context menu handlers expect to run on a single-threaded apartment, which treatments don't run on.
        /// </summary>
        private static T OnStaThread<T>(Func<T> func)
        {
            T result = default!;
            Exception? error = null;

            Thread thread = new Thread(() =>
            {
                try
                {
                    result = func();
                }
                catch (Exception ex)
                {
                    error = ex;
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (error != null)
            {
                ExceptionDispatchInfo.Capture(error).Throw();
            }

            return result;
        }
    }
}
