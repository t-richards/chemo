using Microsoft.Win32;

namespace Chemo.Settings
{
    /// <summary>
    /// A registry value that should hold a specific value.
    /// </summary>
    public sealed class RegistryValue : ISetting
    {
        public string KeyName { get; }
        public string ValueName { get; }
        public object Value { get; }
        public RegistryValueKind Kind { get; }

        /// <param name="keyName">The full key path, starting with HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE.</param>
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
            return $@"{KeyName}\{ValueName} = {Value}";
        }
    }
}
