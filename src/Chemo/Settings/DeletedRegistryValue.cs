using Microsoft.Win32;

namespace Chemo.Settings
{
    /// <summary>
    /// A registry value that should not exist.
    /// </summary>
    public sealed class DeletedRegistryValue : ISetting
    {
        public string KeyName { get; }
        public string ValueName { get; }

        /// <param name="keyName">The full key path, starting with HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE.</param>
        public DeletedRegistryValue(string keyName, string valueName)
        {
            KeyName = keyName;
            ValueName = valueName;
        }

        public bool IsApplied()
        {
            return Registry.GetValue(KeyName, ValueName, null) == null;
        }

        public void Apply()
        {
            RegistryUtils.DeleteValue(KeyName, ValueName);
        }

        public override string ToString()
        {
            return $@"{KeyName}\{ValueName} is removed";
        }
    }
}
