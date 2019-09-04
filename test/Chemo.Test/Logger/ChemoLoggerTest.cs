using Chemo.Logger;
using Xunit;

namespace Chemo.Test.Logger
{
    public class ChemoLoggerTest
    {
        private ChemoLogger logger;

        public ChemoLoggerTest()
        {
            logger = ChemoLogger.Instance;
            logger.Reset();
        }

        [Fact]
        public void ItIsInitializable()
        {
            Assert.NotNull(ChemoLogger.Instance);
        }

        [Fact]
        public void ItIsASingleton()
        {
            var instance1 = ChemoLogger.Instance;
            var instance2 = ChemoLogger.Instance;
            Assert.StrictEqual(instance1, instance2);
        }

        [Fact]
        public void ItStartsWithNoEvents()
        {
            Assert.Empty(logger.Events);
        }

        [Fact]
        public void ItCanLogThings()
        {
            logger.Log("hello {0}", "foo");

            Assert.NotEmpty(logger.Events);
        }
    }
}
