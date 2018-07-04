using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.Management.Deployment;

namespace Chemo.Treatment
{
    class RemoveStoreApps : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;

        public readonly IDictionary<string, bool> AppsToRemove = new Dictionary<string, bool>
        {
            { "Microsoft.BingWeather", true },
            { "Microsoft.GetHelp", true },
            { "Microsoft.Getstarted", true },
            { "Microsoft.Microsoft3DViewer", true },
            { "Microsoft.MicrosoftStickyNotes", true },
            { "Microsoft.OneConnect", true },
            { "Microsoft.People", true },
            { "Microsoft.Print3D", true },
            { "Microsoft.SkypeApp", true },
            { "Microsoft.Wallet", true },
            { "Microsoft.WindowsAlarms", true },
            { "Microsoft.WindowsFeedbackHub", true },
            { "Microsoft.WindowsMaps", true },
            { "Microsoft.Xbox.TCUI", true },
            { "Microsoft.XboxApp", true },
            { "Microsoft.XboxGameOverlay", true },
            { "Microsoft.XboxSpeechToTextOverlay", true },
            { "Microsoft.ZuneMusic", true },
            { "Microsoft.ZuneVideo", true },
            { "Microsoft.MicrosoftOfficeHub", true }
        };

        public void PerformTreatment()
        {
            PackageManager packageManager = new PackageManager();

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

                logger.Log("Should remove? {0}", shouldRemove);
                DisplayPackageInfo(package);
                logger.Log("");
            }
        }

        private static void DisplayPackageInfo(Package package)
        {
            logger.Log("Name: {0}", package.Id.Name);
            logger.Log("Full Name: {0}", package.Id.FullName);

            try
            {
                logger.Log("Installed Location: {0}", package.InstalledLocation.Path);

            }
            catch (Exception ex)
            {
                logger.Log("Could not determine installed location: {0}", ex.Message);
            }
        }
    }
}
