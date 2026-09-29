using Chemo.Settings;

namespace Chemo.Treatment.Performance
{
    internal sealed class SetServicesToManual : SettingsTreatment
    {
        public override string Name => "Trim background services";

        public override string Description =>
            "Turns off Offline Files and the diagnostic data service, and lets the Maps " +
            "and Storage services start only when something needs them.";

        protected override IEnumerable<ISetting> Settings()
        {
            // Internet Connection Sharing (SharedAccess) is left alone because Mobile Hotspot and
            // the Hyper-V Default Switch depend on it.
            return
            [
                new ServiceStartup("CscService", ServiceStartType.Disabled),
                new ServiceStartup("DiagTrack", ServiceStartType.Disabled),
                new ServiceStartup("MapsBroker", ServiceStartType.Manual),
                new ServiceStartup("StorSvc", ServiceStartType.Manual),
            ];
        }
    }
}
