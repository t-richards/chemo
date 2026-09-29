using Chemo.Utilities;

namespace Chemo.Test
{
    public class PendingDeletesTest
    {
        [Fact]
        public void ItFindsDeletesAndSkipsMoves()
        {
            // Entries as Windows 11 writes them.
            string[] operations =
            [
                @"*1\??\C:\Users\Test\AppData\Local\Microsoft\OneDrive\FileSyncShell64.dll", "",
                @"*1\??\C:\Temp\old.txt", @"!*1\??\C:\Temp\new.txt",
                @"*1\??\C:\Users\Test\AppData\Local\Microsoft\OneDrive", "",
            ];

            HashSet<string> paths = PendingDeletes.Parse(operations);

            Assert.Equal(2, paths.Count);
            Assert.Contains(@"C:\Users\Test\AppData\Local\Microsoft\OneDrive", paths);
            Assert.Contains(@"c:\users\test\appdata\local\microsoft\onedrive\filesyncshell64.dll", paths);
            Assert.DoesNotContain(@"C:\Temp\old.txt", paths);
        }

        [Fact]
        public void ItHandlesEntriesWithoutAPrefix()
        {
            HashSet<string> paths = PendingDeletes.Parse([@"\??\C:\Temp\old.txt", ""]);

            Assert.Contains(@"C:\Temp\old.txt", paths);
        }

        [Fact]
        public void ItHandlesNoOperations()
        {
            Assert.Empty(PendingDeletes.Parse(null));
        }
    }
}
