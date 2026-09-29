using Chemo.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Chemo.Treatment.Power
{
    class DisableHibernation : SettingsTreatment
    {
        public override string Name()
        {
            return "Disable Hibernation";
        }

        public override string Tooltip()
        {
            return "Turns off hibernation and Fast Startup and deletes the hibernation file, freeing disk space. " +
                "Laptops will shut down instead of hibernating when the battery runs out.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new HibernationOff(),
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FlyoutMenuSettings", "ShowHibernateOption", 0),
            };
        }

        private sealed class HibernationOff : ISetting
        {
            // powercfg records the setting here. Session Manager\Power, which some guides use, only holds Fast Startup's setting.
            private const string PowerKey = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power";

            public bool IsApplied()
            {
                return RegistryUtils.IntEquals(PowerKey, "HibernateEnabled", 0);
            }

            public void Apply()
            {
                // powercfg also deletes hiberfil.sys, which setting HibernateEnabled alone doesn't.
                ProcessStartInfo startInfo = new ProcessStartInfo(Path.Combine(Environment.SystemDirectory, "powercfg.exe"), "/hibernate off")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        throw new InvalidOperationException($"powercfg exited with code {process.ExitCode}.");
                    }
                }
            }

            public override string ToString()
            {
                return "Hibernation is off";
            }
        }
    }
}
