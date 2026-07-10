using Ksnm.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Cryptography.Tests
{
    [TestClass()]
    public class Aes128Tests
    {
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
    }
}
