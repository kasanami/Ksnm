using Ksnm.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Cryptography.Tests
{
    [TestClass()]
    public class RsaU16Tests
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            try
            {
                RsaU16 rsa = new RsaU16(4, 4, 4);
                Assert.Fail("例外が発生しませんでした。");
            }
            catch (ArgumentException e)
            {
                // 期待される例外
            }
            try
            {
                RsaU16 rsa = new RsaU16(61, 53, 3);
                Assert.Fail("例外が発生しませんでした。");
            }
            catch (ArgumentException e)
            {
                // 期待される例外
            }
        }
        [TestMethod()]
        public void EncryptTest()
        {
            RsaU16 rsa = new RsaU16(79, 67);

            for (int plainValue = 0; plainValue < 256; plainValue++)
            {
                var encryptedValue = rsa.Encrypt((byte)plainValue);
                var decryptedValue = rsa.Decrypt(encryptedValue);
                Assert.AreEqual(plainValue, decryptedValue);
            }
        }
    }
}
