using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Science.Electromagnetism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Science.Electromagnetism.Tests
{
    [TestClass()]
    public class ElectricalResistanceTests
    {
        [TestMethod()]
        public void SeriesConnectionTest()
        {
            var actual = ElectricalResistance.SeriesConnection(1);
            Assert.AreEqual(1, actual);

            actual = ElectricalResistance.SeriesConnection(1, 2);
            Assert.AreEqual(3, actual);

            actual = ElectricalResistance.SeriesConnection(1, 2, 3);
            Assert.AreEqual(6, actual);

            actual = ElectricalResistance.SeriesConnection(1, 2, 3, 4);
            Assert.AreEqual(10, actual);
        }

        [TestMethod()]
        public void ParallelConnectionTest()
        {
            var actual = ElectricalResistance.ParallelConnection(1);
            Assert.AreEqual(1, actual);

            actual = ElectricalResistance.ParallelConnection(2, 2);
            Assert.AreEqual(1, actual);

            actual = ElectricalResistance.ParallelConnection(2, 2, 1);
            Assert.AreEqual(0.5, actual);
        }
    }
}