using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Units.SI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ksnm.Numerics;

namespace Ksnm.Units.SI.Tests
{
    [TestClass()]
    public class MetreTests
    {
        [TestMethod()]
        public void OperationsTest()
        {
            var len1 = new Metre<Double>(3);
            var len2 = new Metre<Double>(4);
            var area = len1 * len2;
            Assert.AreEqual("12m^2", area.ToString());
        }
    }
}