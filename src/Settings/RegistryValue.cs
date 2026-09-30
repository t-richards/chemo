using Microsoft.Win32;

namespace Chemo.Settings
{
    /// <summary>
    /// A registry value that should hold a specific value.
    /// </summary>
    internal sealed class RegistryValue : ISetting
    {
        public string KeyName { get; }
        public string ValueName { get; }
        public object Value { get; }
        public RegistryValueKind Kind { get; }

        /// <param name="keyName">The full key path, starting with HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE.</param>
        /// <param name="valueName">The value's name, or "" for the key's default value.</param>
        /// <param name="value">The data it should hold.</param>
        /// <param name="kind">The data's type. Most Windows settings are DWORDs.</param>
        public RegistryValue(string keyName, string valueName, object value, RegistryValueKind kind = RegistryValueKind.DWord)
        {
            KeyName = keyName;
            ValueName = valueName;
            Value = value;
            Kind = kind;
        }

        public bool IsApplied()
        {
            return Equals(Registry.GetValue(KeyName, ValueName, null), Value);
        }

        public void Apply()
        {
            Registry.SetValue(KeyName, ValueName, Value, Kind);
        }

        public override string ToString()
        {
            string valueName = ValueName.Length == 0 ? "(Default)" : ValueName;
            return $@"{KeyName}\{valueName} = {Value}";
        }
    }
}
