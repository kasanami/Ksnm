using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.ExtensionMethods.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.ExtensionMethods.System.Tests
{
    [TestClass()]
    public class String_Tests
    {
        [TestMethod()]
        public void GetLastTest()
        {
            var str = "abc";
            Assert.AreEqual(str.GetLast(), 'c');
        }
 
        [TestMethod()]
        public void HiraganaToKatakanaTest()
        {
        }

        [TestMethod()]
        public void KatakanaToHiraganaTest()
        {
        }

        [TestMethod()]
        public void ToWideTest()
        {
        }

        [TestMethod()]
        public void IsNullOrEmptyTest()
        {
            string str = null;
            Assert.IsTrue(str.IsNullOrEmpty());
            str = "";
            Assert.IsTrue(str.IsNullOrEmpty());
            str = " ";
            Assert.IsFalse(str.IsNullOrEmpty());
        }

        [TestMethod()]
        public void IsNullOrWhiteSpaceTest()
        {
            string str = null;
            Assert.IsTrue(str.IsNullOrWhiteSpace());
            str = "";
            Assert.IsTrue(str.IsNullOrWhiteSpace());
            str = " ";
            Assert.IsTrue(str.IsNullOrWhiteSpace());
            str = "　";
            Assert.IsTrue(str.IsNullOrWhiteSpace());
            str = "\n";
            Assert.IsTrue(str.IsNullOrWhiteSpace());
            str = "\t";
            Assert.IsTrue(str.IsNullOrWhiteSpace());
        }

        [TestMethod()]
        public void SubstringTest()
        {
            var str = "abcdefg";
            var subStr = str.Substring("a", "f");
            Assert.AreEqual(subStr, "bcde");
            subStr = str.Substring("a", "f", true, true);
            Assert.AreEqual(subStr, "abcdef");
            subStr = str.Substring("b", "f");
            Assert.AreEqual(subStr, "cde");
            subStr = str.Substring("b", "f", false, true);
            Assert.AreEqual(subStr, "cdef");
            subStr = str.Substring("b", "g");
            Assert.AreEqual(subStr, "cdef");
            subStr = str.Substring("b", "g", true, true);
            Assert.AreEqual(subStr, "bcdefg");
            subStr = str.Substring(null, "f");
            Assert.AreEqual(subStr, "abcde");
            subStr = str.Substring(null, "f", true, true);
            Assert.AreEqual(subStr, "abcdef");
            subStr = str.Substring("a", null);
            Assert.AreEqual(subStr, "bcdefg");
            subStr = str.Substring("a", null, true, true);
            Assert.AreEqual(subStr, "abcdefg");
            subStr = str.Substring(null, null);
            Assert.AreEqual(subStr, "abcdefg");
        }
    }
}