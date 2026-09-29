using Chemo.Settings;

namespace Chemo.Treatment.Start
{
    internal sealed class RestorePreviousStartMenu : SettingsTreatment
    {
        public override string Name()
        {
            return "Restore Previous Start Menu Layout";
        }

        public override string Tooltip()
        {
            return "Brings back the Start menu layout from before the Windows 11 25H2 redesign. Has no effect on newer builds where the old layout is gone. A restart is required.";
        }

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
