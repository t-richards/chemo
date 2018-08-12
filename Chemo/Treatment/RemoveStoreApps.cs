using System;
using System.Collections.Generic;
using System.Threading;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Management.Deployment;

namespace Chemo.Treatment
{
    class RemoveStoreApps : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;
        private static PackageManager packageManager = new PackageManager();

        public readonly IDictionary<string, bool> AppsToRemove = new Dictionary<string, bool>
        {
            { "46928bounde.EclipseManager", true },
            { "828B5831.HiddenCityMysteryofShadows", true },
            { "A278AB0D.DragonManiaLegends", true },
            { "ActiproSoftwareLLC.562882FEEB491", true },
            { "AdobeSystemsIncorporated.AdobePhotoshopExpress", true },
            { "D5EA27B7.Duolingo-LearnLanguagesforFree", true},
            { "king.com.BubbleWitch3Saga", true },
            { "king.com.CandyCrushSaga", true },
            { "king.com.CandyCrushSodaSaga", true },
            { "Microsoft.3DBuilder", true },
            { "Microsoft.BingFinance", true },
            { "Microsoft.BingFoodAndDrink", true },
            { "Microsoft.BingHealthAndFitness", true },
            { "Microsoft.BingNews", true },
            { "Microsoft.BingSports", true },
            { "Microsoft.BingTravel", true },
            { "Microsoft.BingWeather", true },
            { "Microsoft.DesktopAppInstaller", true },
            { "Microsoft.FreshPaint", true },
            { "Microsoft.GetHelp", true },
            { "Microsoft.Getstarted", true },
            { "Microsoft.Messaging", true },
            { "Microsoft.Microsoft3DViewer", true },
            { "Microsoft.MicrosoftOfficeHub", true },
            { "Microsoft.MicrosoftPowerBIForWindows", true },
            { "Microsoft.MicrosoftSolitaireCollection", true },
            { "Microsoft.MicrosoftStickyNotes", true },
            { "Microsoft.MinecraftUWP", true },
            { "Microsoft.MSPaint", true },
            { "Microsoft.NetworkSpeedTest", true },
            { "Microsoft.Office.OneNote", true },
            { "Microsoft.Office.Sway", true },
            { "Microsoft.OneConnect", true },
            { "Microsoft.People", true },
            { "Microsoft.Print3D", true },
            { "Microsoft.SkypeApp", true },
            { "Microsoft.Wallet", true },
            { "Microsoft.WindowsAlarms", true },
            { "Microsoft.WindowsCamera", true },
            { "microsoft.windowscommunicationsapps", true },
            { "Microsoft.WindowsFeedbackHub", true },
            { "Microsoft.WindowsMaps", true },
            { "Microsoft.WindowsSoundRecorder", true },
            { "Microsoft.Xbox.TCUI", true },
            { "Microsoft.XboxApp", true },
            { "Microsoft.XboxGameOverlay", true },
            { "Microsoft.XboxGamingOverlay", true },
            { "Microsoft.XboxIdentityProvider", true },
            { "Microsoft.XboxSpeechToTextOverlay", true },
            { "Microsoft.ZuneMusic", true },
            { "Microsoft.ZuneVideo", true },
            { "Nordcurrent.CookingFever", true },
            { "PandoraMediaInc.29680B314EFC2", true },
        };

        public void PerformTreatment()
        {
            int packageCount = 0;
            IEnumerable<Package> packages = packageManager.FindPackages();

            foreach (var package in packages)
            {
                // Don't remove frameworks
                if (package.IsFramework)
                {
                    continue;
                }

                // Don't remove system packages
                if (package.SignatureKind == PackageSignatureKind.System)
                {
                    continue;
                }

                AppsToRemove.TryGetValue(package.Id.Name, out bool shouldRemove);
                if (shouldRemove)
                {
                    RemovePackage(package);
                    packageCount += 1;
                }
                else
                {
                    logger.Log("Not removing {0}", package.Id.Name);
                }
            }

            if (packageCount <= 0)
            {
                logger.Log("No Windows Store applications were uninstalled.");
            }
            logger.Log("");
        }

        private static void RemovePackage(Package package)
        {
            IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> deploymentOperation =
                packageManager.RemovePackageAsync(package.Id.FullName);

            ManualResetEvent opCompletedEvent = new ManualResetEvent(false);

            deploymentOperation.Completed = (result, progress) => {
                logger.Log("Removal operation {1}: {0}", package.Id.Name, result.Status);
                if (result.Status == AsyncStatus.Error)
                {
                    DeploymentResult deploymentResult = deploymentOperation.GetResults();
                    logger.Log("Error code: {0}", deploymentOperation.ErrorCode);
                    logger.Log("Error text: {0}", deploymentResult.ErrorText);
                }
                opCompletedEvent.Set();
            };

            opCompletedEvent.WaitOne();
        }
    }
}
