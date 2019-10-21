using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Ksnm.ExtensionMethods.System.String.Tests
{
    [TestClass()]
    public class StringExtensionsTests
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
            var sample = "あいうえおアイウエオ";
            Assert.AreEqual("アイウエオアイウエオ", sample.HiraganaToKatakana());
        }

        [TestMethod()]
        public void KatakanaToHiraganaTest()
        {
            var sample = "あいうえおアイウエオ";
            Assert.AreEqual("あいうえおあいうえお", sample.KatakanaToHiragana());
        }

        [TestMethod()]
        public void ToWideTest()
        {
            var sample = "123abcABC";
            Assert.AreEqual("１２３ａｂｃＡＢＣ", sample.ToWide());
        }

        [TestMethod()]
        public void RemoveControlCharTest()
        {
            var sample = " 123abc\a\b\f\r\n\t\v";
            Assert.AreEqual(" 123abc", sample.RemoveControlChar());
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
            // 全角でテスト
            str = "あいうえお";
            subStr = str.Substring("あ", "お");
            Assert.AreEqual(subStr, "いうえ");
            subStr = str.Substring("あ", "お", true, true);
            Assert.AreEqual(subStr, "あいうえお");
        }

        [TestMethod()]
        public void ReplaceTest()
        {
            var str = "abcdefg";
            var str2 = str.Replace("aceg", '-');
            Assert.AreEqual(str2, "-b-d-f-");

            str2 = str.Replace(new[] { "a", "c", "e", "g" }, "-");
            Assert.AreEqual(str2, "-b-d-f-");

            str = "あいうえお";
            str2 = str.Replace("あうお", '★');
            Assert.AreEqual(str2, "★い★え★");

            str2 = str.Replace(new[] { "あ", "う", "お" }, "★");
            Assert.AreEqual(str2, "★い★え★");
        }

        [TestMethod()]
        public void SplitTest()
        {
            var str = "abcdefg";
            var subStrs = str.Split(2, 2, 3).ToArray();
            Assert.AreEqual(3, subStrs.Count());
            Assert.AreEqual("ab", subStrs[0]);
            Assert.AreEqual("cd", subStrs[1]);
            Assert.AreEqual("efg", subStrs[2]);

            subStrs = str.Split(2, 3).ToArray();
            Assert.AreEqual(2, subStrs.Count());
            Assert.AreEqual("ab", subStrs[0]);
            Assert.AreEqual("cde", subStrs[1]);

            subStrs = str.Split(3, 3, 3).ToArray();
            Assert.AreEqual(3, subStrs.Count());
            Assert.AreEqual("abc", subStrs[0]);
            Assert.AreEqual("def", subStrs[1]);
            Assert.AreEqual("g", subStrs[2]);

            subStrs = str.Split(4, 4, 4).ToArray();
            Assert.AreEqual(3, subStrs.Count());
            Assert.AreEqual("abcd", subStrs[0]);
            Assert.AreEqual("efg", subStrs[1]);
            Assert.AreEqual("", subStrs[2]);
        }
    }
}
