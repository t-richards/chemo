using Chemo.Settings;
using Microsoft.Win32;

namespace Chemo.Treatment.Sound
{
    class DisableSystemSounds : SettingsTreatment
    {
        public override string Name()
        {
            return "Turn Off System Sounds";
        }

        public override string Tooltip()
        {
            return "Switches to the No Sounds scheme and turns off the startup sound, silencing Windows' notification, error, and other event sounds. " +
                "Alarms, ringtones for calls, music, and videos aren't affected.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new NoSoundsScheme(),

                // Unchecking "Play Windows Startup sound" in the Sound control panel sets both of these.
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\BootAnimation", "DisableStartupSound", 1),
                new RegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\EditionOverrides", "UserSetting_DisableStartupSound", 1),
            };
        }

        /// <summary>
        /// Windows plays the sound in each event's .Current key. Choosing a scheme in the Sound control panel
        /// copies the scheme's sound into .Current for every event the scheme has an entry for, and leaves other
        /// events, like alarms and ringtones, alone. This does the same for the No Sounds scheme, .None.
        /// </summary>
        private sealed class NoSoundsScheme : ISetting
        {
            private const string Schemes = @"AppEvents\Schemes";
            private const string Scheme = ".None";

            public bool IsApplied()
            {
                using (RegistryKey schemes = Registry.CurrentUser.OpenSubKey(Schemes))
                {
                    if (schemes == null)
                    {
                        return true;
                    }

                    // The scheme is a key name and the sounds are file paths, so neither is case-sensitive.
                    if (!string.Equals(schemes.GetValue("") as string, Scheme, StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }

                foreach (RegistryKey eventKey in SchemeEvents(writable: false))
                {
                    using (eventKey)
                    {
                        if (!string.Equals(Sound(eventKey, ".Current"), Sound(eventKey, Scheme), StringComparison.OrdinalIgnoreCase))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            public void Apply()
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\" + Schemes, "", Scheme, RegistryValueKind.String);

                foreach (RegistryKey eventKey in SchemeEvents(writable: true))
                {
                    using (eventKey)
                    using (RegistryKey current = eventKey.CreateSubKey(".Current"))
                    {
                        current.SetValue("", Sound(eventKey, Scheme), RegistryValueKind.String);
                    }
                }
            }

            private static string Sound(RegistryKey eventKey, string scheme)
            {
                using (RegistryKey sound = eventKey.OpenSubKey(scheme))
                {
                    return sound?.GetValue("") as string ?? "";
                }
            }

            /// <summary>
            /// Opens AppEvents\Schemes\Apps\{app}\{event} for each event the scheme has an entry for. The caller
            /// disposes the keys.
            /// </summary>
            private static IEnumerable<RegistryKey> SchemeEvents(bool writable)
            {
                using (RegistryKey apps = Registry.CurrentUser.OpenSubKey(Schemes + @"\Apps"))
                {
                    if (apps == null)
                    {
                        yield break;
                    }

                    foreach (string app in apps.GetSubKeyNames())
                    {
                        using (RegistryKey appKey = apps.OpenSubKey(app))
                        {
                            foreach (string eventName in appKey.GetSubKeyNames())
                            {
                                RegistryKey eventKey = appKey.OpenSubKey(eventName, writable);
                                if (eventKey == null)
                                {
                                    continue;
                                }

                                if (eventKey.GetSubKeyNames().Contains(Scheme, StringComparer.OrdinalIgnoreCase))
                                {
                                    yield return eventKey;
                                }
                                else
                                {
                                    eventKey.Dispose();
                                }
                            }
                        }
                    }
                }
            }

            public override string ToString()
            {
                return "Sound scheme is No Sounds";
            }
        }
    }
}
