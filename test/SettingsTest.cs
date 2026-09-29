using Chemo.Settings;
using Microsoft.Win32;

namespace Chemo.Test
{
    public sealed class SettingsTest : IDisposable
    {
        private readonly string subKey = @"Software\Chemo.Test\" + Guid.NewGuid();
        private string KeyName => @"HKEY_CURRENT_USER\" + subKey;

        public void Dispose()
        {
            Registry.CurrentUser.DeleteSubKeyTree(subKey, false);
        }

        [Fact]
        public void RegistryValueAppliesDwords()
        {
            RegistryValue setting = new(KeyName, "Value", 1);

            Assert.False(setting.IsApplied());
            setting.Apply();
            Assert.True(setting.IsApplied());
            Assert.Equal(RegistryValueKind.DWord, Registry.CurrentUser.OpenSubKey(subKey).GetValueKind("Value"));
        }

        [Fact]
        public void RegistryValueDetectsOtherValues()
        {
            Registry.SetValue(KeyName, "Value", 0, RegistryValueKind.DWord);

            Assert.False(new RegistryValue(KeyName, "Value", 1).IsApplied());
            Assert.False(new RegistryValue(KeyName, "Value", "0", RegistryValueKind.String).IsApplied());
        }

        [Fact]
        public void RegistryValueTellsEmptyDefaultValuesFromMissingOnes()
        {
            RegistryValue setting = new(KeyName, "", "", RegistryValueKind.String);
            Registry.CurrentUser.CreateSubKey(subKey).Dispose();

            Assert.False(setting.IsApplied());
            setting.Apply();
            Assert.True(setting.IsApplied());
            Assert.Equal("", Registry.CurrentUser.OpenSubKey(subKey).GetValue(null));
        }

        [Fact]
        public void DeletedRegistryValueRemovesValues()
        {
            Registry.SetValue(KeyName, "Value", 1, RegistryValueKind.DWord);
            DeletedRegistryValue setting = new(KeyName, "Value");

            Assert.False(setting.IsApplied());
            setting.Apply();
            Assert.True(setting.IsApplied());
        }

        [Fact]
        public void DeletedRegistryValueIgnoresMissingKeys()
        {
            DeletedRegistryValue setting = new(KeyName + @"\Missing", "Value");

            Assert.True(setting.IsApplied());
            setting.Apply();
        }

        [Fact]
        public void DeletedRegistryKeyRemovesKeysAndWhatsInThem()
        {
            Registry.SetValue(KeyName + @"\Parent\Child", "Value", 1, RegistryValueKind.DWord);
            DeletedRegistryKey setting = new(KeyName + @"\Parent");

            Assert.False(setting.IsApplied());
            setting.Apply();
            Assert.True(setting.IsApplied());
            Assert.Null(Registry.CurrentUser.OpenSubKey(subKey + @"\Parent"));
        }

        [Fact]
        public void DeletedRegistryKeyIgnoresMissingKeys()
        {
            DeletedRegistryKey setting = new(KeyName + @"\Missing");

            Assert.True(setting.IsApplied());
            setting.Apply();
        }

        [Fact]
        public void ServiceStartupReadsStartType()
        {
            // The event log service always starts automatically.
            Assert.True(new ServiceStartup("EventLog", ServiceStartType.Automatic).IsApplied());
            Assert.False(new ServiceStartup("EventLog", ServiceStartType.Disabled).IsApplied());
        }

        [Fact]
        public void ServiceStartupIgnoresMissingServices()
        {
            Assert.True(new ServiceStartup("Chemo.Test.Missing", ServiceStartType.Disabled).IsApplied());
        }

        [Fact]
        public void ServiceStartupReadsWhetherServicesAreRunning()
        {
            Assert.True(new ServiceStartup("EventLog", ServiceStartType.Automatic).IsRunning());
            Assert.False(new ServiceStartup("Chemo.Test.Missing", ServiceStartType.Disabled).IsRunning());
        }
    }
}
