using Chemo.Settings;
using Chemo.Utilities;

namespace Chemo.Treatment.Taskbar
{
    internal sealed class UnpinTaskbarApps : SettingsTreatment
    {
        public override string Name()
        {
            return "Unpin Apps From Taskbar";
        }

        public override string Tooltip()
        {
            return "Unpins everything from the taskbar except File Explorer, such as Microsoft Edge and the Microsoft Store. " +
                "If you pin apps later, running this again unpins them too.";
        }

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new OnlyFileExplorerPinned(Logger),
            ];
        }

        private sealed class OnlyFileExplorerPinned : ISetting
        {
            private const string FileExplorer = "Microsoft.Windows.Explorer";

            private readonly MemoryLogger Logger;

            public OnlyFileExplorerPinned(MemoryLogger logger)
            {
                Logger = logger;
            }

            private static bool IsFileExplorer(string appId)
            {
                return string.Equals(appId, FileExplorer, StringComparison.OrdinalIgnoreCase);
            }

            public bool IsApplied()
            {
                List<string> pinned = TaskbarPins.FindPinnedApps().Where(appId => !IsFileExplorer(appId)).ToList();

                foreach (string appId in pinned)
                {
                    Logger.Log("{0} is pinned to the taskbar", appId);
                }

                return pinned.Count == 0;
            }

            public void Apply()
            {
                foreach (string appId in TaskbarPins.UnpinApps(appId => !IsFileExplorer(appId)))
                {
                    Logger.Log("Unpinned {0} from the taskbar.", appId);
                }
            }

            public override string ToString()
            {
                return "Only File Explorer is pinned to the taskbar";
            }
        }
    }
}
