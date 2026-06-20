using Ksnm.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Cryptography.Tests
{
    [TestClass()]
    public class Rsa64Tests
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            try
            {
                Rsa64 rsa64 = new Rsa64(4, 4, 4);
                Assert.Fail("例外が発生しませんでした。");
            }
            catch (ArgumentException e)
            {
                // 期待される例外
            }
            try
            {
                Rsa64 rsa64 = new Rsa64(61, 53, 3);
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
            Rsa64 rsa64 = new Rsa64();

            var plainValues = new int[] { 12345, 67890, 54321, int.MaxValue, int.MinValue };
            foreach (var plainValue in plainValues)
            {
                var encryptedValue = rsa64.Encrypt(plainValue);
                var decryptedValue = rsa64.Decrypt(encryptedValue);
                Assert.AreEqual(plainValue, decryptedValue);
            }
        }
    }
}
