using Ksnm.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ksnm.Cryptography.Tests
{
    [TestClass()]
    public class Aes128Tests
    {
        [TestMethod()]
        public void StateTest()
        {
            Aes128.State state = new Aes128.State();
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

            Assert.AreEqual<uint>(0xD4, state.Array[0]);
            Assert.AreEqual<uint>(0xE0, state.Array[1]);
            Assert.AreEqual<uint>(0xB8, state.Array[2]);
            Assert.AreEqual<uint>(0x1E, state.Array[3]);
            Assert.AreEqual<uint>(0x27, state.Array[4]);
            Assert.AreEqual<uint>(0xBF, state.Array[5]);
            Assert.AreEqual<uint>(0xB4, state.Array[6]);
            Assert.AreEqual<uint>(0x41, state.Array[7]);
            Assert.AreEqual<uint>(0x11, state.Array[8]);
            Assert.AreEqual<uint>(0x98, state.Array[9]);
            Assert.AreEqual<uint>(0x5D, state.Array[10]);
            Assert.AreEqual<uint>(0x52, state.Array[11]);
            Assert.AreEqual<uint>(0xAE, state.Array[12]);
            Assert.AreEqual<uint>(0xF1, state.Array[13]);
            Assert.AreEqual<uint>(0xE5, state.Array[14]);
            Assert.AreEqual<uint>(0x30, state.Array[15]);
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

            Assert.AreEqual<uint>(0xD4, state.Array[0]);
            Assert.AreEqual<uint>(0xE0, state.Array[1]);
            Assert.AreEqual<uint>(0xB8, state.Array[2]);
            Assert.AreEqual<uint>(0x1E, state.Array[3]);
            Assert.AreEqual<uint>(0xBF, state.Array[4]);
            Assert.AreEqual<uint>(0xB4, state.Array[5]);
            Assert.AreEqual<uint>(0x41, state.Array[6]);
            Assert.AreEqual<uint>(0x27, state.Array[7]);
            Assert.AreEqual<uint>(0x5D, state.Array[8]);
            Assert.AreEqual<uint>(0x52, state.Array[9]);
            Assert.AreEqual<uint>(0x11, state.Array[10]);
            Assert.AreEqual<uint>(0x98, state.Array[11]);
            Assert.AreEqual<uint>(0x30, state.Array[12]);
            Assert.AreEqual<uint>(0xAE, state.Array[13]);
            Assert.AreEqual<uint>(0xF1, state.Array[14]);
            Assert.AreEqual<uint>(0xE5, state.Array[15]);
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

            Assert.AreEqual<uint>(0x04, state.Array[0]);
            Assert.AreEqual<uint>(0xE0, state.Array[1]);
            Assert.AreEqual<uint>(0x48, state.Array[2]);
            Assert.AreEqual<uint>(0x28, state.Array[3]);
            Assert.AreEqual<uint>(0x66, state.Array[4]);
            Assert.AreEqual<uint>(0xCB, state.Array[5]);
            Assert.AreEqual<uint>(0xF8, state.Array[6]);
            Assert.AreEqual<uint>(0x06, state.Array[7]);
            Assert.AreEqual<uint>(0x81, state.Array[8]);
            Assert.AreEqual<uint>(0x19, state.Array[9]);
            Assert.AreEqual<uint>(0xD3, state.Array[10]);
            Assert.AreEqual<uint>(0x26, state.Array[11]);
            Assert.AreEqual<uint>(0xE5, state.Array[12]);
            Assert.AreEqual<uint>(0x9A, state.Array[13]);
            Assert.AreEqual<uint>(0x7A, state.Array[14]);
            Assert.AreEqual<uint>(0x4C, state.Array[15]);
        }
    }
}
