using Chemo.Settings;
using System.Collections.Generic;

namespace Chemo.Treatment.Performance
{
    class SetServicesToManual : SettingsTreatment
    {
        public override string Name()
        {
            return "Trim Background Services";
        }

        public override string Tooltip()
        {
            return "Turns off Offline Files and the diagnostic tracking service, and lets the Maps and Storage services start only when they're needed.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // Internet Connection Sharing (SharedAccess) is left alone because Mobile Hotspot and
            // the Hyper-V Default Switch depend on it.
            return new ISetting[]
            {
                new ServiceStartup("CscService", ServiceStartType.Disabled),
                new ServiceStartup("DiagTrack", ServiceStartType.Disabled),
                new ServiceStartup("MapsBroker", ServiceStartType.Manual),
                new ServiceStartup("StorSvc", ServiceStartType.Manual),
            };
        }
    }
}
