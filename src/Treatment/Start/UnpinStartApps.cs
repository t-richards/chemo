using Chemo.Settings;
using Chemo.Utilities;
using Microsoft.Win32;
using System.Diagnostics;

namespace Chemo.Treatment.Start
{
    internal sealed class UnpinStartApps : SettingsTreatment
    {
        public override string Name()
        {
            return "Unpin everything from Start";
        }

        public override string Tooltip()
        {
            return "Unpins every app from Start for your account, including pins for apps that aren't even installed yet, like LinkedIn and WhatsApp. " +
                "If you pin apps later, running this again unpins those too.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new NothingPinned(),
            ];
        }

        /// <summary>
        /// Windows has no way to unpin apps for someone, so this briefly sets the Configure Start Pins policy
        /// (Windows 11 24H2 with KB5062660 and later) to an empty layout, waits for Start to apply it, and then
        /// removes the policy and the layout file. The single empty pin is how winutil empties Start.
        /// </summary>
        private sealed class NothingPinned : ISetting
        {
            private const string ExplorerPolicies = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Explorer";
            private const string Layout = /*lang=json,strict*/ "{\"applyOnce\":true,\"pinnedList\":[{}]}";
            private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);

            public bool IsApplied()
            {
                return StartLayout.CountPins() == 0;
            }

            public void Apply()
            {
                // ProgramData is where Start has been tested reading the layout from.
                string layoutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Chemo-StartPins.json");

                try
                {
                    File.WriteAllText(layoutPath, Layout);
                    Registry.SetValue(ExplorerPolicies, "ConfigureStartPins", 1, RegistryValueKind.DWord);
                    Registry.SetValue(ExplorerPolicies, "ConfigureStartPinsJSON", layoutPath, RegistryValueKind.ExpandString);

                    Stopwatch waited = Stopwatch.StartNew();
                    while (StartLayout.CountPins() > 0)
                    {
                        if (waited.Elapsed > Timeout)
                        {
                            throw new TimeoutException($"Start didn't apply the empty layout within {Timeout.TotalSeconds} seconds.");
                        }

                        Thread.Sleep(500);
                    }
                }
                finally
                {
                    RegistryUtils.DeleteValue(ExplorerPolicies, "ConfigureStartPins");
                    RegistryUtils.DeleteValue(ExplorerPolicies, "ConfigureStartPinsJSON");
                    File.Delete(layoutPath);
                }
            }

            public override string ToString()
            {
                return "Nothing is pinned to Start";
            }
        }
    }
}
