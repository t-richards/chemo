using Chemo.Settings;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Chemo.Treatment.Start
{
    class DisableStoreSearchResults : SettingsTreatment
    {
        public override string Name()
        {
            return "Disable Store Results in Search";
        }

        public override string Tooltip()
        {
            return "Stops the Start menu from recommending Microsoft Store apps when you search for an app.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return new ISetting[]
            {
                new StoreDatabaseLocked(),
            };
        }

        /// <summary>
        /// Start menu search reads its app recommendations from the Store's local database. Denying
        /// everyone access to that file leaves search with nothing to recommend.
        /// </summary>
        private sealed class StoreDatabaseLocked : ISetting
        {
            private static readonly SecurityIdentifier Everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

            private readonly string LocalStatePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Packages", "Microsoft.WindowsStore_8wekyb3d8bbwe", "LocalState"
            );

            private string DatabasePath => Path.Combine(LocalStatePath, "store.db");

            public bool IsApplied()
            {
                // Without the Store there are no Store results to hide.
                if (!Directory.Exists(LocalStatePath))
                {
                    return true;
                }

                FileInfo database = new FileInfo(DatabasePath);
                if (!database.Exists)
                {
                    return false;
                }

                return database.GetAccessControl()
                    .GetAccessRules(true, false, typeof(SecurityIdentifier))
                    .Cast<FileSystemAccessRule>()
                    .Any(rule =>
                        rule.AccessControlType == AccessControlType.Deny &&
                        rule.IdentityReference.Equals(Everyone) &&
                        rule.FileSystemRights.HasFlag(FileSystemRights.FullControl));
            }

            public void Apply()
            {
                FileInfo database = new FileInfo(DatabasePath);
                if (!database.Exists)
                {
                    // A locked, empty database keeps the Store from creating a real one later.
                    database.Create().Dispose();
                }

                FileSecurity security = database.GetAccessControl();
                security.AddAccessRule(new FileSystemAccessRule(Everyone, FileSystemRights.FullControl, AccessControlType.Deny));
                database.SetAccessControl(security);
            }

            public override string ToString()
            {
                return $"{DatabasePath} is locked";
            }
        }
    }
}
