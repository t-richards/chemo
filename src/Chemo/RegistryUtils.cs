using Microsoft.Win32;

namespace Chemo
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
    }
}
