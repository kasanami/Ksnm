namespace Ksnm.Numerics.Tests
{
    using Vector = Vector<double>;
    [TestClass()]
    public class VectorTests
    {
        [TestMethod()]
        public void OperationsTest()
        {
            // +
            {
                Vector vector1 = [1, 2, 3, 4];
                Vector vector2 = [1, 2, 3, 4];
                Vector expected = [2, 4, 6, 8];
                Vector actual = vector1 + vector2;
                Assert.AreEqual(expected, actual);
            }
            {
                Vector vector1 = [1, 2, 3, 4];
                Vector vector2 = [1, 2, 3, 4];
                Vector expected = vector1 + vector2;
                Vector actual = vector1;
                actual.AddAssign(vector2);
                Assert.AreEqual(expected, actual);
            }
            // -
            {
                Vector vector1 = [1, 2, 3, 4];
                Vector vector2 = [1, 2, 3, 4];
                Vector expected = [0, 0, 0, 0];
                Vector actual = vector1 - vector2;
                Assert.AreEqual(expected, actual);
            }
            {
                Vector vector1 = [1, 2, 3, 4];
                Vector vector2 = [1, 2, 3, 4];
                Vector expected = vector1 - vector2;
                Vector actual = vector1;
                actual.SubtractAssign(vector2);
                Assert.AreEqual(expected, actual);
            }
            // *
            {
                Vector vector1 = [1, 2, 3, 4];
                Vector vector2 = [1, 2, 3, 4];
                Vector expected = [1, 4, 9, 16];
                Vector actual = vector1 * vector2;
                Assert.AreEqual(expected, actual);
            }
            {
                Vector vector1 = [1, 2, 3, 4];
                Vector vector2 = [1, 2, 3, 4];
                Vector expected = vector1 * vector2;
                Vector actual = vector1;
                actual.MultiplyAssign(vector2);
                Assert.AreEqual(expected, actual);
            }
            // /
            {
                Vector vector1 = [1, 2, 3, 4];
                Vector vector2 = [1, 2, 3, 4];
                Vector expected = [1, 1, 1, 1];
                Vector actual = vector1 / vector2;
                Assert.AreEqual(expected, actual);
            }
            {
                Vector vector1 = [1, 2, 3, 4];
                Vector vector2 = [1, 2, 3, 4];
                Vector expected = vector1 / vector2;
                Vector actual = vector1;
                actual.DivideAssign(vector2);
                Assert.AreEqual(expected, actual);
            }
            // %
            {
                Vector vector1 = [1, 2, 3, 4];
                Vector vector2 = [1, 2, 3, 4];
                Vector expected = [0, 0, 0, 0];
                Vector actual = vector1 % vector2;
                Assert.AreEqual(expected, actual);
            }
            {
                Vector vector1 = [1, 2, 3, 4];
                Vector vector2 = [1, 2, 3, 4];
                Vector expected = vector1 % vector2;
                Vector actual = vector1;
                actual.ModulusAssign(vector2);
                Assert.AreEqual(expected, actual);
            }
        }
    }
}