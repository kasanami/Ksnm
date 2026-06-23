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
            //TestRsa64();
            TestRsaU16();
        }

        static void TestRsa64()
        {
            Console.WriteLine("TestRsa64");
            int plainValue = 1234567890;//
            int[] prime1 = [72707, 9266503, 1089419417];
            int[] prime2 = [79699, 625697, 2132019979];
            for (int i = 0; i < prime1.Length; i++)
            {
                Console.WriteLine($"テスト{i + 1}");
                var rsa64 = new Rsa64(prime1[i], prime2[i]);
                Console.WriteLine($"Prime1         :{rsa64.Prime1}");
                Console.WriteLine($"Prime2         :{rsa64.Prime2}");
                Console.WriteLine($"PublicExponent :{rsa64.PublicExponent}");
                Console.WriteLine($"PublicSemiprime:{rsa64.PublicSemiprime}");

                var encryptedValue = rsa64.Encrypt(plainValue);
                var decryptedValue = rsa64.Decrypt(encryptedValue);
                Console.WriteLine($"plainValue    :{plainValue}");
                Console.WriteLine($"encryptedValue:{encryptedValue}");
                Console.WriteLine($"decryptedValue:{decryptedValue}");
            }
            {
                Console.WriteLine($"テスト");
                var rsa64 = new Rsa64();
                Console.WriteLine($"Prime1         :{rsa64.Prime1}");
                Console.WriteLine($"Prime2         :{rsa64.Prime2}");
                Console.WriteLine($"PublicExponent :{rsa64.PublicExponent}");
                Console.WriteLine($"PublicSemiprime:{rsa64.PublicSemiprime}");

                var encryptedValue = rsa64.Encrypt(plainValue);
                var decryptedValue = rsa64.Decrypt(encryptedValue);
                Console.WriteLine($"plainValue    :{plainValue}");
                Console.WriteLine($"encryptedValue:{encryptedValue}");
                Console.WriteLine($"decryptedValue:{decryptedValue}");
            }
        }

        static void TestRsaU16()
        {
            Console.WriteLine("TestRsaU16");
            byte plainValue = 123;
            int[] prime1 = [79, 89, 131, 173];
            int[] prime2 = [67, 59, 163, 181];
            for (int i = 0; i < prime1.Length; i++)
            {
                Console.WriteLine($"テスト{i + 1}");
                var rsaU16 = new RsaU16((byte)prime1[i], (byte)prime2[i]);
                Console.WriteLine($"Prime1         :{rsaU16.Prime1}");
                Console.WriteLine($"Prime2         :{rsaU16.Prime2}");
                Console.WriteLine($"PublicExponent :{rsaU16.PublicExponent}");
                Console.WriteLine($"PublicSemiprime:{rsaU16.PublicSemiprime}");

                var encryptedValue = rsaU16.Encrypt(plainValue);
                var decryptedValue = rsaU16.Decrypt(encryptedValue);
                Console.WriteLine($"plainValue    :{plainValue}");
                Console.WriteLine($"encryptedValue:{encryptedValue}");
                Console.WriteLine($"decryptedValue:{decryptedValue}");
            }
            {
                Console.WriteLine($"テスト");
                var rsa = new RsaU16();
                Console.WriteLine($"Prime1         :{rsa.Prime1}");
                Console.WriteLine($"Prime2         :{rsa.Prime2}");
                Console.WriteLine($"PublicExponent :{rsa.PublicExponent}");
                Console.WriteLine($"PublicSemiprime:{rsa.PublicSemiprime}");

                var encryptedValue = rsa.Encrypt(plainValue);
                var decryptedValue = rsa.Decrypt(encryptedValue);
                Console.WriteLine($"plainValue    :{plainValue}");
                Console.WriteLine($"encryptedValue:{encryptedValue}");
                Console.WriteLine($"decryptedValue:{decryptedValue}");
            }
        }
    }
}

