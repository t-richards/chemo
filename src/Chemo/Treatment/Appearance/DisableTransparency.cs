using Microsoft.Win32;

namespace Chemo.Treatment.Appearance
{
    internal class DisableTransparency : BaseTreatment
    {
        private const string keyName = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string valueName = "EnableTransparency";
        private const int value = 0;

        public override string Name()
        {
            return "Disable Transparency";
        }

        public override bool PerformTreatment()
        {
            try
            {
                Registry.SetValue(keyName, valueName, value, RegistryValueKind.DWord);
                Logger.Log("Disabled transparency.");
                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Log("Could not disable transparency: {0}", ex.Message);
                return false;
            }
        }

        public override bool ShouldPerformTreatment()
        {
            return RegistryUtils.IntEquals(keyName, valueName, value);
        }

        public override string Tooltip()
        {
            return "Disables transparency in windows and applications.";
        }
    }
}