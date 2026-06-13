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
        public void EncryptTest()
        {
            //Rsa64 rsa64 = new Rsa64(61, 53);
            //Rsa64 rsa64 = new Rsa64(211, 269);
            //Rsa64 rsa64 = new Rsa64(1009, 8009);
            Rsa64 rsa64 = new Rsa64(9967, 8009);

            var plainValue = 12345;
            var encryptedValue = rsa64.Encrypt(plainValue);
            var decryptedValue = rsa64.Decrypt(encryptedValue);
            Assert.AreEqual(plainValue, decryptedValue);
        }
    }
}
