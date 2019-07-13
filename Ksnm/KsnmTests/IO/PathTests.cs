using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.IO.Tests
{
    [TestClass()]
    public class PathTests
    {
        [TestMethod()]
        public void ToSafeFileNameTest()
        {
            var sample = "\n\r\t\\/:*?\"<>|";
            Assert.AreEqual("＼／：＊？”＜＞｜", Path.ToSafeFileName(sample));
        }
    }
}
