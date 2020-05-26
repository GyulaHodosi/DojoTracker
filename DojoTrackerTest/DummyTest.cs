using NUnit.Framework;

namespace TestDojoTracker
{
    [TestFixture]
    public class DummyTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DummyTestPassing()
        {
            Assert.Pass();
        }
    }
}