using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Ksnm.Algorithm.Tests
{
    [TestClass()]
    public class Crc16Tests
    {
        [TestMethod()]
        public void ComputeHashTest()
        {
            var data = Encoding.UTF8.GetBytes("1234");
            Crc16Polynomial polynomial = Crc16Polynomial.IbmReversed;
            // CRC-16-IBM
            {
                var crc16 = new Crc16(polynomial, 0, 0);
                var hash = crc16.ComputeHash(data);
                Assert.AreEqual(0x14BA, hash, Debug.GetFilePathAndLineNumber());
            }
            {
                var crc16 = new Crc16(polynomial, 0xFFFF, 0);
                var hash = crc16.ComputeHash(data);
                Assert.AreEqual(0x30BA, hash, Debug.GetFilePathAndLineNumber());
            }
            {
                var crc16 = new Crc16(polynomial, 0, 0xFFFF);
                var hash = crc16.ComputeHash(data);
                Assert.AreEqual(0xEB45, hash, Debug.GetFilePathAndLineNumber());
            }
            {
                var crc16 = new Crc16(polynomial, 0xFFFF, 0xFFFF);
                var hash = crc16.ComputeHash(data);
                Assert.AreEqual(0xCF45, hash, Debug.GetFilePathAndLineNumber());
            }
            // CRC-16-CCITT
            polynomial = Crc16Polynomial.CcittReversed;
            {
                var crc16 = new Crc16(polynomial, 0, 0);
                var hash = crc16.ComputeHash(data);
                Assert.AreEqual(0x8832, hash, Debug.GetFilePathAndLineNumber());
            }
            {
                var crc16 = new Crc16(polynomial, 0xFFFF, 0);
                var hash = crc16.ComputeHash(data);
                Assert.AreEqual(0x8B13, hash, Debug.GetFilePathAndLineNumber());
            }
            {
                var crc16 = new Crc16(polynomial, 0, 0xFFFF);
                var hash = crc16.ComputeHash(data);
                Assert.AreEqual(0x77CD, hash, Debug.GetFilePathAndLineNumber());
            }
            {
                var crc16 = new Crc16(polynomial, 0xFFFF, 0xFFFF);
                var hash = crc16.ComputeHash(data);
                Assert.AreEqual(0x74EC, hash, Debug.GetFilePathAndLineNumber());
            }
        }
    }
}
