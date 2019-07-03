using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Randoms.Tests
{
    [TestClass()]
    public class SampleTests : Base
    {
        [TestMethod()]
        public void NextTest()
        {
            var random = new Sample();
            NextTest(random);
        }

        [TestMethod()]
        public void NextUInt32Test()
        {
            var random = new Sample();
            NextUInt32Test(random);
        }

        [TestMethod()]
        public void NextInt64Test()
        {
            var random = new Sample();
            NextInt64Test(random);
        }

        [TestMethod()]
        public void NextUInt64Test()
        {
            var random = new Sample();
            NextUInt64Test(random);
        }

        [TestMethod()]
        public void NextDoubleTest()
        {
            var random = new Sample();
            NextDoubleTest(random);
        }
    }
}