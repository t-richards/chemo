using Chemo.Settings;

namespace Chemo.Treatment.Privacy
{
    internal sealed class DisableTelemetry : SettingsTreatment
    {
        private const string HKCU = @"HKEY_CURRENT_USER\Software\Microsoft";

        public override string Name()
        {
            return "Turn off telemetry";
        }

        public override string Tooltip()
        {
            return "Sends Microsoft the least diagnostic data Windows allows, and turns off the advertising ID, tips " +
                "and ads based on your diagnostic data, online speech recognition, typing and handwriting data " +
                "collection, activity history, feedback requests, and PowerShell's telemetry.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                // Diagnostic data. Pro treats 0 (off) as 1 (required), the lowest level it supports.
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\DataCollection", "AllowTelemetry", 0),
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", "AllowTelemetry", 0),
                new ServiceStartup("DiagTrack", ServiceStartType.Disabled),

                // Advertising and tailored experiences
                new RegistryValue(HKCU + @"\Windows\CurrentVersion\AdvertisingInfo", "Enabled", 0),
                new RegistryValue(HKCU + @"\Windows\CurrentVersion\Privacy", "TailoredExperiencesWithDiagnosticDataEnabled", 0),

                // Speech, typing, and inking
                new RegistryValue(HKCU + @"\Speech_OneCore\Settings\OnlineSpeechPrivacy", "HasAccepted", 0),
                new RegistryValue(HKCU + @"\Input\TIPC", "Enabled", 0),
                new RegistryValue(HKCU + @"\InputPersonalization", "RestrictImplicitInkCollection", 1),
                new RegistryValue(HKCU + @"\InputPersonalization", "RestrictImplicitTextCollection", 1),
                new RegistryValue(HKCU + @"\InputPersonalization\TrainedDataStore", "HarvestContacts", 0),
                new RegistryValue(HKCU + @"\Personalization\Settings", "AcceptedPrivacyPolicy", 0),

                // Activity history and app launch tracking
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "PublishUserActivities", 0),
                new RegistryValue(HKCU + @"\Windows\CurrentVersion\Explorer\Advanced", "Start_TrackProgs", 0),

                // Feedback prompts
                new RegistryValue(HKCU + @"\Siuf\Rules", "NumberOfSIUFInPeriod", 0),
                new DeletedRegistryValue(HKCU + @"\Siuf\Rules", "PeriodInNanoSeconds"),

                // PowerShell 7
                new MachineEnvironmentVariable("POWERSHELL_TELEMETRY_OPTOUT", "1"),
            ];
        }
    }
}
