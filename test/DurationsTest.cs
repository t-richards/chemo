using Chemo.Utilities;
using System;
using Xunit;

namespace Chemo.Test
{
    public class DurationsTest
    {
        [Theory]
        [InlineData(0, "under a second")]
        [InlineData(0.4, "under a second")]
        [InlineData(1, "1 second")]
        [InlineData(1.6, "2 seconds")]
        [InlineData(59.4, "59 seconds")]
        [InlineData(59.6, "1 minute")]
        [InlineData(65, "1 minute 5 seconds")]
        [InlineData(120, "2 minutes")]
        [InlineData(3600, "1 hour")]
        [InlineData(3720, "1 hour 2 minutes")]
        public void ItDescribesDurations(double seconds, string expected)
        {
            Assert.Equal(expected, Durations.Humanize(TimeSpan.FromSeconds(seconds)));
        }
    }
}
