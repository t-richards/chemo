using Chemo.Data;
using Xunit;

namespace Chemo.Test
{
    public class StoreAppsTest
    {
        [Theory]
        [InlineData("Microsoft.BingNews")]
        [InlineData("MSTeams")]
        [InlineData("MicrosoftTeams")]
        [InlineData("Microsoft.MicrosoftSolitaireCollection")]
        public void ItRemovesJunkApps(string packageName)
        {
            Assert.True(StoreApps.ShouldRemove(packageName));
        }

        [Fact]
        public void ItIgnoresCase()
        {
            Assert.True(StoreApps.ShouldRemove("microsoft.bingnews"));
        }

        [Theory]
        [InlineData("Microsoft.DesktopAppInstaller")]
        [InlineData("Microsoft.WindowsStore")]
        [InlineData("Microsoft.WindowsCalculator")]
        [InlineData("Microsoft.ZuneMusic")]
        [InlineData("Microsoft.XboxIdentityProvider")]
        [InlineData("Microsoft.GetHelp")]
        public void ItKeepsUsefulApps(string packageName)
        {
            Assert.False(StoreApps.ShouldRemove(packageName));
        }

        [Fact]
        public void ItDoesNotRemoveMediaExtensions()
        {
            Assert.False(StoreApps.ShouldRemove("Microsoft.WebpImageExtension"));
            Assert.False(StoreApps.ShouldRemove("Microsoft.VP9VideoExtensions"));
            Assert.False(StoreApps.ShouldRemove("Microsoft.HEIFImageExtension"));
        }

        [Fact]
        public void ItSkipsJunkPackages()
        {
            Assert.False(StoreApps.ShouldRemove("foobar"));
        }
    }
}
