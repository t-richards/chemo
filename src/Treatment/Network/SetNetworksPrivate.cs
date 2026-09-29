using Chemo.Settings;

namespace Chemo.Treatment.Network
{
    internal sealed class SetNetworksPrivate : SettingsTreatment
    {
        public override string Name()
        {
            return "Make your current network private";
        }

        public override string Tooltip()
        {
            return "Marks the networks you're connected to right now as private, so this PC can see and share with printers and other PCs on them. " +
                "Networks you join later aren't changed. Don't use this on public Wi-Fi.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new ConnectedNetworksPrivate(),
            ];
        }

        private sealed class ConnectedNetworksPrivate : ISetting
        {
            // Windows' Network List Manager, which the Settings app and Set-NetConnectionProfile use.
            private static readonly Guid NetworkListManager = new("DCB00C01-570F-4A9B-8D69-199FDBA5723B");
            private const int ConnectedNetworks = 1;
            private const int PublicCategory = 0;
            private const int PrivateCategory = 1;

            /// <summary>
            /// Finds connected public networks. Domain networks, managed by a workplace, are left alone.
            /// </summary>
            private static List<dynamic> PublicNetworks()
            {
                dynamic manager = Activator.CreateInstance(Type.GetTypeFromCLSID(NetworkListManager));
                List<dynamic> networks = [];

                foreach (dynamic network in manager.GetNetworks(ConnectedNetworks))
                {
                    if ((int)network.GetCategory() == PublicCategory)
                    {
                        networks.Add(network);
                    }
                }

                return networks;
            }

            public bool IsApplied()
            {
                return PublicNetworks().Count == 0;
            }

            public void Apply()
            {
                foreach (dynamic network in PublicNetworks())
                {
                    network.SetCategory(PrivateCategory);
                }
            }

            public override string ToString()
            {
                return "Connected networks are private";
            }
        }
    }
}
