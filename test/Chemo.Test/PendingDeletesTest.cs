using Xunit;

namespace Chemo.Test
{
    public class PendingDeletesTest
    {
        [Fact]
        public void ItFindsDeletesAndSkipsMoves()
        {
            string[] operations =
            {
                @"\??\C:\Users\Test\AppData\Local\Microsoft\OneDrive\FileSyncShell64.dll", "",
                @"\??\C:\Temp\old.txt", @"!\??\C:\Temp\new.txt",
                @"\??\C:\Users\Test\AppData\Local\Microsoft\OneDrive", "",
            };

            var paths = PendingDeletes.Parse(operations);

            Assert.Equal(2, paths.Count);
            Assert.Contains(@"C:\Users\Test\AppData\Local\Microsoft\OneDrive", paths);
            Assert.Contains(@"c:\users\test\appdata\local\microsoft\onedrive\filesyncshell64.dll", paths);
            Assert.DoesNotContain(@"C:\Temp\old.txt", paths);
        }

        [Fact]
        public void ItHandlesNoOperations()
        {
            Assert.Empty(PendingDeletes.Parse(null));
        }
    }
}
