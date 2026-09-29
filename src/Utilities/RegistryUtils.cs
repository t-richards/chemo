using Microsoft.Win32;
using System;

namespace Chemo.Utilities
{
    class RegistryUtils
    {
        public static bool IntEquals(string keyName, string valueName, int expectedValue)
        {
            var value = Registry.GetValue(keyName, valueName, null);
            return (value != null && (int)value == expectedValue);
        }

        public static bool StringEquals(string keyName, string valueName, string expectedValue)
        {
            var value = Registry.GetValue(keyName, valueName, null);
            return (value != null && (string)value == expectedValue);
        }

        /// <summary>
        /// Deletes a value if it exists. The key name is a full path, like the ones Registry.GetValue takes.
        /// </summary>
        public static void DeleteValue(string keyName, string valueName)
        {
            int separator = keyName.IndexOf('\\');
            RegistryKey root = keyName.Substring(0, separator) switch
            {
                "HKEY_CURRENT_USER" => Registry.CurrentUser,
                "HKEY_LOCAL_MACHINE" => Registry.LocalMachine,
                _ => throw new ArgumentException($"Unsupported registry root in {keyName}.", nameof(keyName)),
            };

            using (RegistryKey key = root.OpenSubKey(keyName.Substring(separator + 1), true))
            {
                key?.DeleteValue(valueName, false);
            }
        }
    }
}
