using Ksnm.Cryptography;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ksnm.Cryptography.Tests
{
    [TestClass()]
    public class AesTests
    {
        byte[] key = [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F];
        [TestMethod()]
        public void EcbTest()
        {
            var aes = new Aes(key);
            byte[] planeBytes =
            [
                01, 02, 03, 04,
                05, 06, 07, 08,
                09, 10, 11, 12,
                13, 14, 15, 16
            ];
            var encryptedBytes = aes.EncryptEcb(planeBytes);
            var decryptedBytes = aes.DecryptEcb(encryptedBytes);
            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual<byte>(planeBytes[i], decryptedBytes[i]);
            }
        }
        [TestMethod()]
        public void AddRoundKeyTest()
        {
            var aes = new Aes(key);
            byte[] state =
            [
                01, 02, 03, 04,
                05, 06, 07, 08,
                09, 10, 11, 12,
                13, 14, 15, 16
            ];
            Aes.AddRoundKey(state, 0, aes.RoundKeys);

            byte[] expected = [1, 3, 1, 7, 1, 3, 1, 15, 1, 3, 1, 7, 1, 3, 1, 31];
            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual<byte>(expected[i], state[i]);
            }
        }
        [TestMethod()]
        public void SubBytesTest()
        {
            var aes = new Aes(key);
            byte[] state =
            [
                01, 02, 03, 04,
                05, 06, 07, 08,
                09, 10, 11, 12,
                13, 14, 15, 16
            ];
            Aes.SubBytes(state);

            byte[] expected = [124, 119, 123, 242, 107, 111, 197, 48, 1, 103, 43, 254, 215, 171, 118, 202];
            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual<byte>(expected[i], state[i]);
            }
        }
        [TestMethod()]
        public void InvSubBytesTest()
        {
            var aes = new Aes(key);
            byte[] state =
            [
                01, 02, 03, 04,
                05, 06, 07, 08,
                09, 10, 11, 12,
                13, 14, 15, 16
            ];
            Aes.SubBytes(state);
            Aes.InvSubBytes(state);

            byte[] expected =
            [
                01, 02, 03, 04,
                05, 06, 07, 08,
                09, 10, 11, 12,
                13, 14, 15, 16
            ];
            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual<byte>(expected[i], state[i]);
            }
        }
        [TestMethod()]
        public void ShiftRowsTest()
        {
            var aes = new Aes(key);
            byte[] state =
            [
                01, 02, 03, 04,
                05, 06, 07, 08,
                09, 10, 11, 12,
                13, 14, 15, 16
            ];
            Aes.ShiftRows(state);

            byte[] expected =
            [
                01, 06, 11, 16,
                05, 10, 15, 04,
                09, 14, 03, 08,
                13, 02, 07, 12,
            ];
            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual<byte>(expected[i], state[i]);
            }
        }
        [TestMethod()]
        public void InvShiftRowsTest()
        {
            var aes = new Aes(key);
            byte[] state =
            [
                01, 02, 03, 04,
                05, 06, 07, 08,
                09, 10, 11, 12,
                13, 14, 15, 16
            ];
            Aes.ShiftRows(state);
            Aes.InvShiftRows(state);

            byte[] expected =
            [
                01, 02, 03, 04,
                05, 06, 07, 08,
                09, 10, 11, 12,
                13, 14, 15, 16
            ];
            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual<byte>(expected[i], state[i]);
            }
        }
        [TestMethod()]
        public void MixColumnsTest()
        {
            var aes = new Aes(key);
            byte[] state =
            [
                01, 02, 03, 04,
                05, 06, 07, 08,
                09, 10, 11, 12,
                13, 14, 15, 16
            ];
            Aes.MixColumns(state);

            byte[] expected = [3, 4, 9, 10, 15, 8, 21, 30, 11, 12, 1, 2, 23, 16, 45, 54];
            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual<byte>(expected[i], state[i]);
            }
        }
        [TestMethod()]
        public void InvMixColumnsTest()
        {
            var aes = new Aes(key);
            byte[] state =
            [
                01, 02, 03, 04,
                05, 06, 07, 08,
                09, 10, 11, 12,
                13, 14, 15, 16
            ];
            Aes.MixColumns(state);
            Aes.InvMixColumns(state);

            byte[] expected =
            [
                01, 02, 03, 04,
                05, 06, 07, 08,
                09, 10, 11, 12,
                13, 14, 15, 16
            ];
            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual<byte>(expected[i], state[i]);
            }
        }
    }
}
