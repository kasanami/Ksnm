using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Microsoft.ApplicationInsights;

namespace Ksnm.Numerics.Tests
{
    using Matrix = Matrix<double>;
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void ConstantTest()
        {
        }
        [TestMethod()]
        public void ConstructorTest()
        {
            Matrix expected = new Matrix(2, 2);
            Matrix actual = new Matrix(expected);
            Assert.AreEqual(expected, actual);

            expected[0, 0] = 1;
            actual[0, 0] = 1;
            Assert.AreEqual(actual, expected);

            expected[1, 1] = 1;
            actual[1, 1] = 1;
            Assert.AreEqual(actual, expected);
        }
        [TestMethod()]
        public void CastTest()
        {
        }

        [TestMethod()]
        public void OperationsTest1()
        {
            // +
            {
                Matrix actual = new double[3, 3]
                {
                    { 1,2,3 },
                    { 4,5,6 },
                    { 7,8,9 },
                };
                Matrix expected = new double[3, 3]
                {
                    { 1,2,3 },
                    { 4,5,6 },
                    { 7,8,9 },
                };
                Assert.AreEqual(expected, +actual);
            }
            // -
            {
                Matrix actual = new double[3, 3]
                {
                    { 1,2,3 },
                    { 4,5,6 },
                    { 7,8,9 },
                };
                Matrix expected = new double[3, 3]
                {
                    { -1,-2,-3 },
                    { -4,-5,-6 },
                    { -7,-8,-9 },
                };
                Assert.AreEqual(expected, -actual);
            }
        }

        [TestMethod()]
        public void OperationsTest2()
        {
            Matrix expected = new Matrix(2, 2);
            Matrix actual = new Matrix(expected);
            Matrix a = new Matrix(expected);
            Matrix b = new Matrix(expected);
            // +
            {
                expected[0, 0] = 34;
                expected[0, 1] = 68;
                expected[1, 0] = 136;
                expected[1, 1] = 272;

                a[0, 0] = 2;
                a[0, 1] = 4;
                a[1, 0] = 8;
                a[1, 1] = 16;

                b[0, 0] = 32;
                b[0, 1] = 64;
                b[1, 0] = 128;
                b[1, 1] = 256;
                actual = a + b;
                Assert.AreEqual(expected, actual);
            }
            {
                a = new double[2, 2]
                {
                    { 1, 2 },
                    { 3, 4 },
                };
                actual = a + Matrix.One;
                expected = new double[2, 2]
                {
                    { 2, 3 },
                    { 4, 5 },
                };
                Assert.AreEqual(expected, actual);
            }
            // -
            {
                expected[0, 0] = 30;
                expected[0, 1] = 60;
                expected[1, 0] = 120;
                expected[1, 1] = 240;

                a[0, 0] = 2;
                a[0, 1] = 4;
                a[1, 0] = 8;
                a[1, 1] = 16;

                b[0, 0] = 32;
                b[0, 1] = 64;
                b[1, 0] = 128;
                b[1, 1] = 256;
                actual = b - a;
                Assert.AreEqual(expected, actual);
            }
            {
                a = new double[2, 2]
                {
                    { 2, 3 },
                    { 4, 5 },
                };
                actual = a - Matrix.One;
                expected = new double[2, 2]
                {
                    { 1, 2 },
                    { 3, 4 },
                };
                Assert.AreEqual(expected, actual);
            }
            // *
            {
                expected[0, 0] = 32;
                expected[0, 1] = 51;
                expected[1, 0] = 46;
                expected[1, 1] = 75;

                a[0, 0] = 2;
                a[0, 1] = 5;
                a[1, 0] = 4;
                a[1, 1] = 7;

                b[0, 0] = 1;
                b[0, 1] = 3;
                b[1, 0] = 6;
                b[1, 1] = 9;
                actual = a * b;
                Assert.AreEqual(expected, actual);
            }
            // *
            {
                expected = new Matrix(1, 2);
                actual = new Matrix(expected);
                a = new Matrix(1, 3);
                b = new Matrix(3, 2);

                expected[0, 0] = 62;
                expected[0, 1] = 38;

                a[0, 0] = 2;
                a[0, 1] = 4;
                a[0, 2] = 6;

                b[0, 0] = 7;
                b[0, 1] = 5;
                b[1, 0] = 3;
                b[1, 1] = 4;
                b[2, 0] = 6;
                b[2, 1] = 2;
                actual = a * b;
                Assert.AreEqual(expected, actual);
            }
            // ==
            {
                a = new double[,]
                {
                    { 1, 2 },
                    { 3, 4 },
                };
                b = new double[,]
                {
                    { 1, 2 },
                    { 3, 4 },
                };
                Assert.IsTrue(a == b);
                b = new double[,]
                {
                    { 1, 2 },
                    { 3, 1 },
                };
                Assert.IsFalse(a == b);
            }
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            for (int i = 0; i < 10; i++)
            {
                var a = new Matrix(i, i);
                var b = new Matrix(i, i);
                Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            }
        }

        [TestMethod()]
        public void AbsTest()
        {
            Matrix actual = new double[3, 3]
            {
                { +1,-1,0 },
                { 0,-1,0 },
                { -1,0,-1 },
            };
            Matrix expected = new double[3, 3]
            {
                { +1,+1,0 },
                { 0,+1,0 },
                { +1,0,+1 },
            };
            Assert.AreEqual(expected, Matrix.Abs(actual));
        }

        [TestMethod()]
        public void IsZeroTest()
        {
            Matrix notZero = new double[3, 3]
            {
                { 0,0,0 },
                { 0,0,0 },
                { 0,0,-1 },
            };
            Matrix zero = new double[3, 3]
            {
                { 0,0,0 },
                { 0,0,0 },
                { 0,0,0 },
            };
            Assert.IsFalse(Matrix.IsZero(notZero));
            Assert.IsTrue(Matrix.IsZero(zero));
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Matrix a = new double[3, 3]
            {
                { 0,0,0 },
                { 0,0,0 },
                { 0,0,-1 },
            };
            Matrix b = new double[3, 3]
            {
                { 0,0,0 },
                { 0,0,0 },
                { 0,0,0 },
            };
            Matrix c = new double[3, 3]
            {
                { 0,0,0 },
                { 0,0,0 },
                { 0,0,1 },
            };
            Assert.AreEqual(-1, a.CompareTo(b));
            Assert.AreEqual(1, b.CompareTo(a));
            Assert.AreEqual(-1, b.CompareTo(c));
            Assert.AreEqual(1, c.CompareTo(a));
        }

        [TestMethod()]
        public void SigmoidTest()
        {
            Matrix a = new double[3, 3]
            {
                { -2, -1, -0.5 }, { -0.25, 0, +0.25 }, { +0.5, +1, +2 }
            };
            Matrix expected = new double[3, 3]
            {
                { 0.11920292202211755, 0.2689414213699951, 0.3775406687981454 },
                { 0.43782349911420193, 0.5, 0.5621765008857981 },
                { 0.6224593312018546, 0.7310585786300049, 0.8807970779778823 }
            };
            Assert.AreEqual(expected, Matrix.Sigmoid(a, 1.0));
        }
    }
}