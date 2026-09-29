using Chemo.Controls;

namespace Chemo.Test
{
    public class DarkThemeTest
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ItFollowsWindowsByDefault(bool windowsIsDark)
        {
            Assert.Equal(windowsIsDark, DarkTheme.UseDark(ThemeChoice.System, windowsIsDark, highContrast: false));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ItIgnoresWindowsWhenLightOrDarkIsPicked(bool windowsIsDark)
        {
            Assert.False(DarkTheme.UseDark(ThemeChoice.Light, windowsIsDark, highContrast: false));
            Assert.True(DarkTheme.UseDark(ThemeChoice.Dark, windowsIsDark, highContrast: false));
        }

        [Fact]
        public void ItStaysLightInHighContrast()
        {
            Assert.All(
                Enum.GetValues(typeof(ThemeChoice)).Cast<ThemeChoice>(),
                choice => Assert.False(DarkTheme.UseDark(choice, windowsIsDark: true, highContrast: true)));
        }
    }
}
