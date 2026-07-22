using Ksnm.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Ksnm.Cryptography.Aes128;

namespace Ksnm.Cryptography.Tests
{
    [TestClass()]
    public class Aes128Tests
    {
        [TestMethod()]
        public void AddRoundKeyTest()
        {
            byte[] key = new byte[16]
            {
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,
                0x08,0x09,0x0A,0x0B,0x0C,0x0D,0x0E,0x0F
            };

            var aes = new Aes(key);
        }
        [TestMethod()]
        public void ExpandKeyTest()
        {

        }
        [TestMethod()]
        public void StateTest()
        {
            Aes128.State state = new Aes128.State([
                0x00, 0x01, 0x02, 0x03,
                0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B,
                0x0C, 0x0D, 0x0E, 0x0F
            ]);
            // 
            Assert.AreEqual(00, state[0, 0]);
            Assert.AreEqual(01, state[1, 0]);
            Assert.AreEqual(02, state[2, 0]);
            Assert.AreEqual(03, state[3, 0]);
            Assert.AreEqual(04, state[0, 1]);
            Assert.AreEqual(05, state[1, 1]);
            Assert.AreEqual(06, state[2, 1]);
            Assert.AreEqual(07, state[3, 1]);
            Assert.AreEqual(08, state[0, 2]);
            Assert.AreEqual(09, state[1, 2]);
            Assert.AreEqual(10, state[2, 2]);
            Assert.AreEqual(11, state[3, 2]);
            Assert.AreEqual(12, state[0, 3]);
            Assert.AreEqual(13, state[1, 3]);
            Assert.AreEqual(14, state[2, 3]);
            Assert.AreEqual(15, state[3, 3]);

            for (int i = 0; i < state.Array.Length; i++)
            {
                state.Array[i] = (byte)i;
            }
            Assert.AreEqual(00, state[0, 0]);
            Assert.AreEqual(01, state[0, 1]);
            Assert.AreEqual(02, state[0, 2]);
            Assert.AreEqual(03, state[0, 3]);
            Assert.AreEqual(04, state[1, 0]);
            Assert.AreEqual(05, state[1, 1]);
            Assert.AreEqual(06, state[1, 2]);
            Assert.AreEqual(07, state[1, 3]);
            Assert.AreEqual(08, state[2, 0]);
            Assert.AreEqual(09, state[2, 1]);
            Assert.AreEqual(10, state[2, 2]);
            Assert.AreEqual(11, state[2, 3]);
            Assert.AreEqual(12, state[3, 0]);
            Assert.AreEqual(13, state[3, 1]);
            Assert.AreEqual(14, state[3, 2]);
            Assert.AreEqual(15, state[3, 3]);

            Assert.AreEqual<uint>(0x03020100, state.Words[0]);
            Assert.AreEqual<uint>(0x07060504, state.Words[1]);
            Assert.AreEqual<uint>(0x0B0A0908, state.Words[2]);
            Assert.AreEqual<uint>(0x0F0E0D0C, state.Words[3]);
        }
        [TestMethod()]
        public void ConstructorTest()
        {
        }
        [TestMethod()]
        public void EncryptTest()
        {
        }
        [TestMethod()]
        public void MultiplyTest()
        {
            Assert.AreEqual(0xFE, Aes128.Multiply(0x57, 0x13));
        }
        [TestMethod()]
        public void RotWordTest()
        {
            Assert.AreEqual(0xCF4F3C09, Aes128.RotWord(0x09CF4F3C));
        }
        [TestMethod()]
        public void SubWordTest()
        {
            var word = Aes128.SubWord(0x01020304);
            var word2 = Aes128.SubWord(new byte[] { 0x04, 0x03, 0x02, 0x01 });
            Assert.AreEqual<uint>(0x7C777BF2, word2);
        }
        [TestMethod()]
        public void SBoxTest()
        {
            Assert.AreEqual<uint>(0xD4, Aes128.SBox[0x19]);
            Assert.AreEqual<uint>(0xE0, Aes128.SBox[0xA0]);
            Assert.AreEqual<uint>(0xB8, Aes128.SBox[0x9A]);
            Assert.AreEqual<uint>(0x1E, Aes128.SBox[0xE9]);
        }
        [TestMethod()]
        public void SubBytesTest()
        {
            Aes128.State state = new Aes128.State(new byte[]
            {
                0x19, 0xA0, 0x9A, 0xE9,
                0x3D, 0xF4, 0xC6, 0xF8,
                0xE3, 0xE2, 0x8D, 0x48,
                0xBE, 0x2B, 0x2A, 0x08
            });
            Aes128.SubBytes(ref state);

            var expected = new byte[]
            {
                0xD4, 0x27, 0x11, 0xAE,
                0xE0, 0xBF, 0x98, 0xF1,
                0xB8, 0xB4, 0x5D, 0xE5,
                0x1E, 0x41, 0x52, 0x30,
            };
            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual<uint>(expected[i], state.Array[i]);
            }
        }
        [TestMethod()]
        public void ShiftRowsTest()
        {
            Aes128.State state = new Aes128.State(new byte[]
            {
                0x19, 0xA0, 0x9A, 0xE9,
                0x3D, 0xF4, 0xC6, 0xF8,
                0xE3, 0xE2, 0x8D, 0x48,
                0xBE, 0x2B, 0x2A, 0x08
            });
            Aes128.SubBytes(ref state);
            Aes128.ShiftRows(ref state);

            var expected = new byte[]
            {
                0xD4, 0xBF, 0x5D, 0x30,
                0xE0, 0xB4, 0x52, 0xAE,
                0xB8, 0x41, 0x11, 0xF1,
                0x1E, 0x27, 0x98, 0xE5,
            };
            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual<uint>(expected[i], state.Array[i]);
            }
        }
        [TestMethod()]
        public void MixColumnsTest()
        {
            Aes128.State state = new Aes128.State(new byte[]
            {
                0x19, 0xA0, 0x9A, 0xE9,
                0x3D, 0xF4, 0xC6, 0xF8,
                0xE3, 0xE2, 0x8D, 0x48,
                0xBE, 0x2B, 0x2A, 0x08
            });
            Aes128.SubBytes(ref state);
            Aes128.ShiftRows(ref state);
            Aes128.MixColumns(ref state);

            var expected = new byte[]
            {
                0x04, 0xE0, 0x48, 0x28,
                0x66, 0xCB, 0xF8, 0x06,
                0x81, 0x19, 0xD3, 0x26,
                0xE5, 0x9A, 0x7A, 0x4C,
            };
            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual<uint>(expected[i], state.Array[i]);
            }
        }
        [TestMethod()]
        public void KeyExpansionTest()
        {
            byte[] key = [0x2B, 0x7E, 0x15, 0x16, 0x28, 0xAE, 0xD2, 0xA6, 0xAB, 0xF7, 0x15, 0x88, 0x09, 0xCF, 0x4F, 0x3C];
            var words1 = Aes128.KeyExpansion(key);
            var words2 = Aes.KeyExpansion(key,4,10);

            Assert.AreEqual<uint>(words2[0], words1[0]);
            Assert.AreEqual<uint>(words2[1], words1[1]);
            Assert.AreEqual<uint>(words2[2], words1[2]);
            Assert.AreEqual<uint>(words2[3], words1[3]);
            Assert.AreEqual<uint>(words2[4], words1[4]);
            Assert.AreEqual<uint>(words2[5], words1[5]);
            Assert.AreEqual<uint>(words2[6], words1[6]);
            Assert.AreEqual<uint>(words2[7], words1[7]);

            Assert.AreEqual<uint>(words2[8], words1[8]);
            Assert.AreEqual<uint>(words2[9], words1[9]);
            Assert.AreEqual<uint>(words2[10], words1[10]);
            Assert.AreEqual<uint>(words2[11], words1[11]);
            Assert.AreEqual<uint>(words2[12], words1[12]);
            Assert.AreEqual<uint>(words2[13], words1[13]);
            Assert.AreEqual<uint>(words2[14], words1[14]);
            Assert.AreEqual<uint>(words2[15], words1[15]);

        }
    }
}
