using Chemo.Settings;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace Chemo.Treatment.Start
{
    class UnpinStartApps : SettingsTreatment
    {
        private const string ExplorerPolicies = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Explorer";

        // %ProgramData%\Chemo\StartPins.json
        private static readonly string LayoutPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Chemo", "StartPins.json");

        public override string Name()
        {
            return "Unpin Everything From Start";
        }

        public override string Tooltip()
        {
            return "Clears the Pinned section of Start for every user, including pins for apps that aren't installed yet, like LinkedIn and WhatsApp. " +
                "Apps you pin afterwards stay pinned. Sign out to finish.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            // The Configure Start Pins policy, which Windows 11 24H2 supports from the July 2025 update (KB5062660).
            // Its JSON setting is the path to a layout file.
            return new ISetting[]
            {
                new EmptyLayoutFile(),
                new RegistryValue(ExplorerPolicies, "ConfigureStartPins", 1),
                new RegistryValue(ExplorerPolicies, "ConfigureStartPinsJSON", LayoutPath, RegistryValueKind.ExpandString),
            };
        }

        /// <summary>
        /// A layout with nothing pinned. applyOnce clears each user's pins at their next sign-in and then leaves
        /// Start alone, where without it Windows would clear the pins at every sign-in. The single empty pin is
        /// how winutil empties Start.
        /// </summary>
        private sealed class EmptyLayoutFile : ISetting
        {
            private const string Layout = "{\"applyOnce\":true,\"pinnedList\":[{}]}";

            public bool IsApplied()
            {
                return File.Exists(LayoutPath) && File.ReadAllText(LayoutPath) == Layout;
            }

            public void Apply()
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LayoutPath));
                File.WriteAllText(LayoutPath, Layout);
            }

            public override string ToString()
            {
                return $"{LayoutPath} pins nothing to Start";
            }
        }
    }
}
