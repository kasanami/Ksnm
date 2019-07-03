using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Randoms.Tests
{
    [TestClass()]
    public class PrototypeTests : Base
    {
        [TestMethod()]
        public void NextTest()
        {
            var random = new Prototype();
            NextTest(random);
        }

        [TestMethod()]
        public void NextUInt32Test()
        {
            var random = new Prototype();
            NextUInt32Test(random);
        }

        [TestMethod()]
        public void NextInt64Test()
        {
            var random = new Prototype();
            NextInt64Test(random);
        }

        [TestMethod()]
        public void NextUInt64Test()
        {
            var random = new Prototype();
            NextUInt64Test(random);
        }

        [TestMethod()]
        public void NextDoubleTest()
        {
            var random = new Prototype();
            NextDoubleTest(random);
        }
    }
}