using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dism;

namespace Chemo.Treatment
{
    class DeprovisionStoreApps : ITreatment
    {
        private static readonly Logger logger = Logger.Instance;

        public void PerformTreatment()
        {
            logger.Log("== Deprovision ==");

            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismAppxPackageCollection dismAppxPackages = DismApi.GetProvisionedAppxPackages(session);
                foreach (var package in dismAppxPackages)
                {
                    logger.Log("DISM package display name: {0}", package.DisplayName);
                }
            }
        }
    }
}
