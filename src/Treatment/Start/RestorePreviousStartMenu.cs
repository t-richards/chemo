using Chemo.Settings;

namespace Chemo.Treatment.Start
{
    internal sealed class RestorePreviousStartMenu : SettingsTreatment
    {
        public override string Name => "Bring back the previous Start menu";

        public override string Description =>
            "Brings back the Start menu from before the Windows 11 25H2 redesign. " +
            "Newer versions of Windows no longer have the old Start menu, so this does nothing there. Restart to finish.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                // Feature override that turns off the redesigned Start menu, as set by ViVeTool.
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FeatureManagement\Overrides\8\3036241548", "EnabledState", 1),
            ];
        }
    }
}
