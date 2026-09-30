using Microsoft.Win32;

namespace Chemo.Utilities
{
    internal static class RegistryUtils
    {
        // A value of another type, such as a DWORD stored as a string, doesn't match rather than throwing.
        public static bool IntEquals(string keyName, string valueName, int expectedValue)
        {
            return Registry.GetValue(keyName, valueName, null) is int value && value == expectedValue;
        }

        public static bool StringEquals(string keyName, string valueName, string expectedValue)
        {
            return Registry.GetValue(keyName, valueName, null) is string value && string.Equals(value, expectedValue, StringComparison.Ordinal);
        }

        /// <summary>
        /// Deletes a value if it exists. The key name is a full path, like the ones Registry.GetValue takes.
        /// </summary>
        public static void DeleteValue(string keyName, string valueName)
        {
            RegistryKey root = Root(keyName, out string subKeyName);
            using RegistryKey key = root.OpenSubKey(subKeyName, true);
            key?.DeleteValue(valueName, false);
        }

        /// <summary>
        /// Determines whether a key exists. The key name is a full path, like the ones Registry.GetValue takes.
        /// </summary>
        public static bool KeyExists(string keyName)
        {
            RegistryKey root = Root(keyName, out string subKeyName);
            using RegistryKey? key = root.OpenSubKey(subKeyName);
            return key != null;
        }

        /// <summary>
        /// Deletes a key and everything in it, if it exists. The key name is a full path, like the ones
        /// Registry.GetValue takes.
        /// </summary>
        public static void DeleteKey(string keyName)
        {
            RegistryKey root = Root(keyName, out string subKeyName);
            root.DeleteSubKeyTree(subKeyName, false);
        }

        private static RegistryKey Root(string keyName, out string subKeyName)
        {
            int separator = keyName.IndexOf('\\');
            subKeyName = keyName.Substring(separator + 1);

            return keyName.Substring(0, separator) switch
            {
                "HKEY_CURRENT_USER" => Registry.CurrentUser,
                "HKEY_LOCAL_MACHINE" => Registry.LocalMachine,
                "HKEY_CLASSES_ROOT" => Registry.ClassesRoot,
                _ => throw new ArgumentException($"Unsupported registry root in {keyName}.", nameof(keyName)),
            };
        }
    }
}
