using System.Runtime.InteropServices;

// Chemo runs as an administrator, often from the Downloads folder. Loading the Windows DLLs it calls only from
// System32 stops a planted DLL of the same name next to Chemo.exe from running with those rights.
[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

namespace Chemo.Utilities
{
    [Flags]
    internal enum MoveFileFlags
    {
        None = 0,
        ReplaceExisting = 1,
        CopyAllowed = 2,
        DelayUntilReboot = 4,
        WriteThrough = 8,
        CreateHardlink = 16,
        FailIfNotTrackable = 32,
    }

    internal static class UnsafeNativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool MoveFileEx(string lpExistingFileName, string? lpNewFileName, MoveFileFlags dwFlags);

        public const uint SC_MANAGER_CONNECT = 0x0001;
        public const uint SERVICE_CHANGE_CONFIG = 0x0002;
        public const uint SERVICE_NO_CHANGE = 0xFFFFFFFF;

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "OpenSCManagerW")]
        public static extern IntPtr OpenSCManager(string? lpMachineName, string? lpDatabaseName, uint dwDesiredAccess);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "OpenServiceW")]
        public static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "ChangeServiceConfigW")]
        public static extern bool ChangeServiceConfig(
            IntPtr hService,
            uint dwServiceType,
            uint dwStartType,
            uint dwErrorControl,
            string? lpBinaryPathName,
            string? lpLoadOrderGroup,
            IntPtr lpdwTagId,
            string? lpDependencies,
            string? lpServiceStartName,
            string? lpPassword,
            string? lpDisplayName);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool CloseServiceHandle(IntPtr hSCObject);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, string? pszSubAppName, string? pszSubIdList);

        public static readonly IntPtr HWND_BROADCAST = new(0xFFFF);
        public const int WM_SETTINGCHANGE = 0x001A;
        public const uint SMTO_ABORTIFHUNG = 0x0002;

        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SendMessageTimeoutW")]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, int msg, IntPtr wParam, string lParam, uint fuFlags, uint uTimeout, out IntPtr lpdwResult);

        // SystemParametersInfo actions and flags
        public const uint SPIF_UPDATEINIFILE = 0x01;
        public const uint SPIF_SENDCHANGE = 0x02;
        public const uint SPI_GETMOUSE = 0x0003;
        public const uint SPI_SETMOUSE = 0x0004;
        public const uint SPI_GETSCREENSAVETIMEOUT = 0x000E;
        public const uint SPI_SETSCREENSAVETIMEOUT = 0x000F;
        public const uint SPI_GETSCREENSAVEACTIVE = 0x0010;
        public const uint SPI_SETSCREENSAVEACTIVE = 0x0011;
        public const uint SPI_SETDESKWALLPAPER = 0x0014;
        public const uint SPI_GETSTICKYKEYS = 0x003A;
        public const uint SPI_SETSTICKYKEYS = 0x003B;
        public const uint SPI_GETANIMATION = 0x0048;
        public const uint SPI_SETANIMATION = 0x0049;
        public const uint SPI_GETSCREENSAVESECURE = 0x0076;
        public const uint SPI_SETSCREENSAVESECURE = 0x0077;
        public const uint SPI_GETCLIENTAREAANIMATION = 0x1042;
        public const uint SPI_SETCLIENTAREAANIMATION = 0x1043;
        public const uint SKF_HOTKEYACTIVE = 0x00000004;

        [StructLayout(LayoutKind.Sequential)]
        internal struct ANIMATIONINFO
        {
            public uint cbSize;
            public int iMinAnimate;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct STICKYKEYS
        {
            public uint cbSize;
            public uint dwFlags;
        }

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SystemParametersInfoW")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref int pvParam, uint fWinIni);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SystemParametersInfoW")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SystemParametersInfoW")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, int[] pvParam, uint fWinIni);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SystemParametersInfoW")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref ANIMATIONINFO pvParam, uint fWinIni);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SystemParametersInfoW")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref STICKYKEYS pvParam, uint fWinIni);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "SystemParametersInfoW")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

        // Locale information for the current user
        public const uint LOCALE_USER_DEFAULT = 0x0400;
        public const uint LOCALE_SSHORTDATE = 0x0000001F;
        public const uint LOCALE_SSHORTTIME = 0x00000079;
        public const uint LOCALE_STIMEFORMAT = 0x00001003;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int GetLocaleInfoEx(string? lpLocaleName, uint LCType, char[] lpLCData, int cchData);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "SetLocaleInfoW")]
        public static extern bool SetLocaleInfo(uint Locale, uint LCType, string lpLCData);

        // Power schemes
        [DllImport("powrprof.dll")]
        public static extern uint PowerGetActiveScheme(IntPtr UserRootPowerKey, out IntPtr ActivePolicyGuid);

        [DllImport("powrprof.dll")]
        public static extern uint PowerSetActiveScheme(IntPtr UserRootPowerKey, ref Guid SchemeGuid);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LocalFree(IntPtr hMem);

        // Shell items and their context menus
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void SHCreateItemFromParsingName(string pszPath, IntPtr pbc, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CreatePopupMenu();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyMenu(IntPtr hMenu);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern uint GetMenuItemID(IntPtr hMenu, int nPos);

        public const int LVM_SETEXTENDEDLISTVIEWSTYLE = 0x1000 + 54;
        public const int LVM_SETITEMW = 0x1000 + 76;
        public const int LVS_EX_SUBITEMIMAGES = 0x0002;
        public const uint LVIF_IMAGE = 0x0002;

        [StructLayout(LayoutKind.Sequential)]
        internal struct LVITEMW
        {
            public uint mask;
            public int iItem;
            public int iSubItem;
            public uint state;
            public uint stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
            public int iGroupId;
            public uint cColumns;
            public IntPtr puColumns;
            public IntPtr piColFmt;
            public int iGroup;
        }

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref LVITEMW lParam);
    }
}
