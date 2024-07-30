using Ksnm.ExtensionMethods.System.Array;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Ksnm.ExtensionMethods.System.Array.Tests
{
    [TestClass()]
    public class ArrayExtensionsTests
    {
        [TestMethod()]
        public void CompareTest()
        {
            var arrayA = new[] { 1, 2, 3 };
            var arrayB = new[] { 1, 2, 3 };
            Assert.AreEqual(arrayA.Compare(arrayB), 0);

            arrayA = new[] { 1, 2, 3 };
            arrayB = new[] { 1, 2, 2 };
            Assert.AreEqual(arrayA.Compare(arrayB), +1);

            arrayA = new[] { 1, 2, 3, 4 };
            arrayB = new[] { 1, 2, 3 };
            Assert.AreEqual(arrayA.Compare(arrayB), +1);

            arrayA = new[] { 1, 2, 2 };
            arrayB = new[] { 1, 2, 3 };
            Assert.AreEqual(arrayA.Compare(arrayB), -1);

            arrayA = new[] { 1, 2, 3 };
            arrayB = new[] { 1, 2, 3, 4 };
            Assert.AreEqual(arrayA.Compare(arrayB), -1);
        }

        [TestMethod()]
        public void GetLastTest()
        {
            var array = new[] { 1, 2, 3 };
            Assert.AreEqual(array.GetLast(), 3);
        }

        [TestMethod()]
        public void GetCenterValueTest()
        {
            var array1 = new[] { 1, 2, 3 };
            Assert.AreEqual(array1.GetCenterValue(), 2);

            var array2 = new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
            };
            Assert.AreEqual(array2.GetCenterValue(), 5);

            var array3 = new[, ,]
            {
                {
                    { 11, 12, 13 },
                    { 14, 15, 16 },
                    { 17, 18, 19 },
                },
                {
                    { 21, 22, 23 },
                    { 24, 25, 26 },
                    { 27, 28, 29 },
                },
                {
                    { 31, 32, 33 },
                    { 34, 35, 36 },
                    { 37, 38, 39 },
                }
            };
            Assert.AreEqual(array3.GetCenterValue(), 25);
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            {
                var ary = new[] { 0, 1, 2, 3 };
                for (int i = 0; i < ary.Length; i++)
                {
                    Assert.AreEqual(i, ary.IndexOf(i));
                }
            }
            {
                var ary = new[] { 0, 1, 2, 3 };
                for (int i = 0; i < ary.Length; i++)
                {
                    Assert.AreEqual(i, ary.IndexOf(i, 0));
                }
            }
            {
                var ary = new[] { "0", "1", "2", "3" };
                for (int i = 0; i < ary.Length; i++)
                {
                    Assert.AreEqual(i, ary.IndexOf(i.ToString()));
                }
            }
            {
                var ary = new[] { "0", "1", "2", "3" };
                for (int i = 0; i < ary.Length; i++)
                {
                    Assert.AreEqual(i, ary.IndexOf(i.ToString(), 0));
                }
            }
        }

        [TestMethod()]
        public void CopyFromTest()
        {
            {
                var ary1 = new int[] { 0, 0, 0, 0 };
                var ary2 = new int[] { 1, 2, 3, 4 };
                ary1.CopyFrom(ary2);
                Assert.IsTrue(ary1.SequenceEqual(ary2));
            }
            {
                var ary1 = new double[] { 0, 0, 0, 0 };
                var ary2 = new double[] { 1, 2, 3, 4 };
                ary1.CopyFrom(ary2);
                Assert.IsTrue(ary1.SequenceEqual(ary2));
            }
            var expected = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            {
                var ary1 = new double[12];
                var ary2 = new double[,] { { 0, 1, 2 }, { 3, 4, 5, }, { 6, 7, 8 }, { 9, 10, 11 } };
                ary1.CopyFrom(ary2);
                Assert.IsTrue(ary1.SequenceEqual(expected));
            }
            {
                var ary1 = new double[12];
                var ary2 = new double[,,] { { { 0 }, { 1 }, { 2 } }, { { 3 }, { 4 }, { 5 }, }, { { 6 }, { 7 }, { 8 } }, { { 9 }, { 10 }, { 11 } } };
                ary1.CopyFrom(ary2);
                Assert.IsTrue(ary1.SequenceEqual(expected));
            }
        }
    }
}