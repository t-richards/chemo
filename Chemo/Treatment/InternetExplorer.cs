using Microsoft.Dism;
using System;

namespace Chemo.Treatment
{
    class InternetExplorer : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        public const string IE32BitName = "Internet-Explorer-Optional-x86";
        public const string IE64BitName = "Internet-Explorer-Optional-amd64";

        public bool ShouldPerformTreatment()
        {
            return true;
        }

        public bool PerformTreatment()
        {
            string featureName;
            if (Environment.Is64BitOperatingSystem)
            {
                featureName = IE64BitName;
            }
            else
            {
                featureName = IE32BitName;
            }

            try
            {
                using (DismSession session = DismApi.OpenOnlineSession())
                {
                    DismFeatureInfo info = DismApi.GetFeatureInfo(session, featureName);
                    if (
                        info.FeatureState == DismPackageFeatureState.NotPresent ||
                        info.FeatureState == DismPackageFeatureState.Removed ||
                        info.FeatureState == DismPackageFeatureState.Staged)
                    {
                        logger.Log("Internet Explorer 11 is not present on the system.");
                        return true;
                    }

                    if (info.FeatureState == DismPackageFeatureState.UninstallPending)
                    {
                        logger.Log("Internet Explorer 11 is already pending uninstall.");
                        return true;
                    }

                    try
                    {
                        logger.Log("Disabling Internet Explorer 11...");
                        DismApi.DisableFeature(session, featureName, null, true);
                    }
                    catch (DismRebootRequiredException ex)
                    {
                        logger.Log("Successfully disabled Internet Explorer 11. {0}", ex.Message);
                        return true;
                    }

                    logger.Log("Successfully disabled Internet Explorer 11.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Log("An error occurred while disabling Internet Explorer: {0}", ex.Message);
            }

            return false;
        }
    }
}
