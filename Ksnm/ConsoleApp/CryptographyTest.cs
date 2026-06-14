using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ksnm.Cryptography;

namespace ConsoleApp
{
    internal class CryptographyTest
    {
        public static void Run()
        {
            Console.WriteLine("RSA暗号化方式のテスト");
            {
                //Ksnm.Cryptography.Rsa64 rsa64 = new Ksnm.Cryptography.Rsa64(61, 53);
                //Ksnm.Cryptography.Rsa64 rsa64 = new Ksnm.Cryptography.Rsa64(211, 269);
                //Ksnm.Cryptography.Rsa64 rsa64 = new Ksnm.Cryptography.Rsa64(1009, 8009);
                Rsa64 rsa64 = new Rsa64(9967, 8009, 61);

                var plainValue = 12345;
                var encryptedValue = rsa64.Encrypt(plainValue);
                var decryptedValue = rsa64.Decrypt(encryptedValue);
                Console.WriteLine($"plainValue    :{plainValue}");
                Console.WriteLine($"encryptedValue:{encryptedValue}");
                Console.WriteLine($"decryptedValue:{decryptedValue}");
            }
        }
    }
}
