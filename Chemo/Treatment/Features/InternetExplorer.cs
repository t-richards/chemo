using Microsoft.Dism;
using System;
using System.Threading.Tasks;

namespace Chemo.Treatment.Features
{
    class InternetExplorer : BaseTreatment
    {
        public const string IE32BitName = "Internet-Explorer-Optional-x86";
        public const string IE64BitName = "Internet-Explorer-Optional-amd64";
        protected string resolvedFeatureName = "";

        public override string Name()
        {
            return "Disable Internet Explorer";
        }

        public override string Tooltip()
        {
            return "Disables Internet Explorer 11. A system reboot is required to complete this operation.";
        }

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

        public override Task<bool> ShouldPerformTreatment()
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
                        return Task.FromResult(false);
                    }
                }
            }
            catch
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public override Task<bool> PerformTreatment()
        {
            try
            {
                using (DismSession session = DismApi.OpenOnlineSession())
                {
                    try
                    {
                        DismApi.DisableFeature(session, resolvedFeatureName, null, true);
                    }
                    catch (DismRebootRequiredException ex)
                    {
                        Logger.Log("Successfully disabled Internet Explorer 11. {0}", ex.Message);
                        return Task.FromResult(true);
                    }

                    Logger.Log("Successfully disabled Internet Explorer 11.");
                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("An error occurred while disabling Internet Explorer: {0}", ex.Message);
            }

            return Task.FromResult(false);
        }
    }
}
