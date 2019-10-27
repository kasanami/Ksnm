using Ksnm.ExtensionMethods.System.Span;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ksnm.ExtensionMethods.System.Span.Tests
{
    [TestClass()]
    public class SpanExtensionsTests
    {
        [TestMethod()]
        public void IndexOfTest()
        {
            // 0～9の配列を作成
            var span = Enumerable.Range(0, 10).ToArray().AsSpan();
            int[] find = new int[] { 1, 2, 3 };
            Assert.AreEqual(1, span.IndexOf(find, 0));
            Assert.AreEqual(1, span.IndexOf(find, 1));
            Assert.AreEqual(-1, span.IndexOf(find, 2));
            Assert.AreEqual(-1, span.IndexOf(find, 3));
            find = new int[] { 4, 5, 6 };
            Assert.AreEqual(4, span.IndexOf(find, 0));
            Assert.AreEqual(4, span.IndexOf(find, 1));
            Assert.AreEqual(4, span.IndexOf(find, 2));
            Assert.AreEqual(4, span.IndexOf(find, 3));
            Assert.AreEqual(4, span.IndexOf(find, 4));
            Assert.AreEqual(-1, span.IndexOf(find, 5));
            Assert.AreEqual(-1, span.IndexOf(find, 6));
            find = new int[] { 7, 8, 9 };
            Assert.AreEqual(7, span.IndexOf(find, 0));
            Assert.AreEqual(7, span.IndexOf(find, 7));
            Assert.AreEqual(-1, span.IndexOf(find, 8));
            Assert.AreEqual(-1, span.IndexOf(find, 9));
            Assert.AreEqual(-1, span.IndexOf(find, 10));
        }

        [TestMethod()]
        public void SliceTest()
        {
            var str = "abcdefg".AsSpan();
            var subStr = str.Slice("a", "f").ToString();
            Assert.AreEqual(subStr, "bcde");
            subStr = str.Slice("a", "f", true, true).ToString();
            Assert.AreEqual(subStr, "abcdef");
            subStr = str.Slice("b", "f").ToString();
            Assert.AreEqual(subStr, "cde");
            subStr = str.Slice("b", "f", false, true).ToString();
            Assert.AreEqual(subStr, "cdef");
            subStr = str.Slice("b", "g").ToString();
            Assert.AreEqual(subStr, "cdef");
            subStr = str.Slice("b", "g", true, true).ToString();
            Assert.AreEqual(subStr, "bcdefg");
            subStr = str.Slice(null, "f").ToString();
            Assert.AreEqual(subStr, "abcde");
            subStr = str.Slice(null, "f", true, true).ToString();
            Assert.AreEqual(subStr, "abcdef");
            subStr = str.Slice("a", null).ToString();
            Assert.AreEqual(subStr, "bcdefg");
            subStr = str.Slice("a", null, true, true).ToString();
            Assert.AreEqual(subStr, "abcdefg");
            // 全角でテスト
            str = "あいうえお".AsSpan();
            subStr = str.Slice("あ", "お").ToString();
            Assert.AreEqual(subStr, "いうえ");
            subStr = str.Slice("あ", "お", true, true).ToString();
            Assert.AreEqual(subStr, "あいうえお");
        }
    }
}
