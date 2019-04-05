using Microsoft.Dism;
using System;

namespace Chemo.Treatment
{
    class InternetExplorer : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        public static readonly string IE32BitName = "Internet-Explorer-Optional-x86";
        public static readonly string IE64BitName = "Internet-Explorer-Optional-amd64";
        protected string resolvedFeatureName = "";

        public InternetExplorer()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                resolvedFeatureName = IE64BitName;
            }
            else
            {
                resolvedFeatureName = IE32BitName;
            }
        }

        public bool ShouldPerformTreatment()
        {
            try
            {
                using (DismSession session = DismApi.OpenOnlineSession())
                {
                    DismFeatureInfo info = DismApi.GetFeatureInfo(session, resolvedFeatureName);
                    if (
                        info.FeatureState == DismPackageFeatureState.NotPresent ||
                        info.FeatureState == DismPackageFeatureState.Removed ||
                        info.FeatureState == DismPackageFeatureState.Staged ||
                        info.FeatureState == DismPackageFeatureState.UninstallPending)
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool PerformTreatment()
        {
            try
            {
                using (DismSession session = DismApi.OpenOnlineSession())
                {
                    try
                    {
                        logger.Log("Disabling Internet Explorer 11...");
                        DismApi.DisableFeature(session, resolvedFeatureName, null, true);
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
