using Microsoft.Dism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chemo.Treatment
{
    class InternetExplorer : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        public const string IEFeatureName = "Internet-Explorer-Optional-amd64";

        public void PerformTreatment()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismFeatureInfo info = DismApi.GetFeatureInfo(session, IEFeatureName);
                if (info.FeatureState == DismPackageFeatureState.NotPresent || info.FeatureState == DismPackageFeatureState.Removed)
                {
                    logger.Log("Internet Explorer 11 is not present on the system.");
                    return;
                }

                try
                {
                    logger.Log("Disabling Internet Explorer 11...");
                    DismApi.DisableFeature(session, IEFeatureName, null, true);
                }
                catch (DismRebootRequiredException ex)
                {
                    logger.Log("Successfully disabled Internet Explorer 11. {0}", ex.Message);
                    return;
                }

                logger.Log("Successfully disabled Internet Explorer 11.");
            }
        }
    }
}
