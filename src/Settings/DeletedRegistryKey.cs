using Chemo.Utilities;

namespace Chemo.Settings
{
    /// <summary>
    /// A registry key that should not exist, along with everything in it.
    /// </summary>
    internal sealed class DeletedRegistryKey : ISetting
    {
        public string KeyName { get; }

        /// <param name="keyName">The full key path, starting with HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, or HKEY_CLASSES_ROOT.</param>
        public DeletedRegistryKey(string keyName)
        {
            KeyName = keyName;
        }

        public bool IsApplied()
        {
            return !RegistryUtils.KeyExists(KeyName);
        }

        public void Apply()
        {
            RegistryUtils.DeleteKey(KeyName);
        }

        public override string ToString()
        {
            return $"{KeyName} is removed";
        }
    }
}
