using Chemo.Data;
using Xunit;

namespace Chemo.Test
{
    public class StoreAppsTest
    {
        [Fact]
        public void ItRemovesCandyCrush()
        {
            Assert.True(StoreApps.ShouldRemove("king.com.CandyCrushSaga"));
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