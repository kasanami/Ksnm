using Ksnm.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class CryptographyTest
    {
        public static void Run()
        {
            Console.WriteLine("RSA暗号化方式のテスト");
            TestRsa(191, 109);
            TestRsa64();
            TestRsaU16();
        }

        static void TestRsa(BigInteger p, BigInteger q)
        {
            Console.WriteLine($"TestRsa(p={p}, q={q})");

            // n = p * q
            BigInteger n = p * q;

            // φ(n)
            BigInteger phi = (p - 1) * (q - 1);

            // 公開指数
            BigInteger e = 17;

            // 秘密指数
            BigInteger d = ModInverse(e, phi);

            Console.WriteLine($"phi={phi}");
            Console.WriteLine($"公開鍵 (n={n}, e={e})");
            Console.WriteLine($"秘密鍵 (n={n}, d={d})");

            BigInteger message = 123;

            // 暗号化
            BigInteger cipher = BigInteger.ModPow(message, e, n);

            // 復号
            BigInteger decrypted = BigInteger.ModPow(cipher, d, n);

            Console.WriteLine($"平文     : {message}");
            Console.WriteLine($"暗号文   : {cipher}");
            Console.WriteLine($"復号結果 : {decrypted}");
        }
        /// <summary>
        /// 拡張ユークリッド互除法
        /// </summary>
        static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger x0 = 0;
            BigInteger x1 = 1;

            while (a > 1)
            {
                BigInteger q = a / m;

                (a, m) = (m, a % m);
                (x0, x1) = (x1 - q * x0, x0);
                Console.WriteLine($"a={a}, m={m}, x0={x0}, x1={x1}");
            }

            if (x1 < 0)
                x1 += m0;

            return x1;
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
                var rsa = new Rsa64(prime1[i], prime2[i]);
                Console.WriteLine($"Prime1         :{rsa.Prime1}");
                Console.WriteLine($"Prime2         :{rsa.Prime2}");
                Console.WriteLine($"PublicExponent :{rsa.PublicExponent}");
                Console.WriteLine($"PublicSemiprime:{rsa.PublicSemiprime}");
                Console.WriteLine($"SecretExponent :{rsa.SecretExponent}");

                var encryptedValue = rsa.Encrypt(plainValue);
                var decryptedValue = rsa.Decrypt(encryptedValue);
                Console.WriteLine($"plainValue    :{plainValue}");
                Console.WriteLine($"encryptedValue:{encryptedValue}");
                Console.WriteLine($"decryptedValue:{decryptedValue}");
            }
            {
                Console.WriteLine($"テスト");
                var rsa = new Rsa64();
                Console.WriteLine($"Prime1         :{rsa.Prime1}");
                Console.WriteLine($"Prime2         :{rsa.Prime2}");
                Console.WriteLine($"PublicExponent :{rsa.PublicExponent}");
                Console.WriteLine($"PublicSemiprime:{rsa.PublicSemiprime}");
                Console.WriteLine($"SecretExponent :{rsa.SecretExponent}");

                var encryptedValue = rsa.Encrypt(plainValue);
                var decryptedValue = rsa.Decrypt(encryptedValue);
                Console.WriteLine($"plainValue    :{plainValue}");
                Console.WriteLine($"encryptedValue:{encryptedValue}");
                Console.WriteLine($"decryptedValue:{decryptedValue}");
            }
        }

        static void TestRsaU16()
        {
            Console.WriteLine("TestRsaU16");
            byte plainValue = 123;
            {
                Console.WriteLine($"テスト");
                var rsa = new RsaU16(191, 109);
                Console.WriteLine($"Prime1         :{rsa.Prime1}");
                Console.WriteLine($"Prime2         :{rsa.Prime2}");
                Console.WriteLine($"PublicExponent :{rsa.PublicExponent}");
                Console.WriteLine($"PublicSemiprime:{rsa.PublicSemiprime}");
                Console.WriteLine($"SecretExponent :{rsa.SecretExponent}");

                var encryptedValue = rsa.Encrypt(plainValue);
                var decryptedValue = rsa.Decrypt(encryptedValue);
                Console.WriteLine($"plainValue    :{plainValue}");
                Console.WriteLine($"encryptedValue:{encryptedValue}");
                Console.WriteLine($"decryptedValue:{decryptedValue}");
            }
            int[] prime1 = [37, 67, 89, 131, 163, 181];
            int[] prime2 = [29, 59, 79, 157, 173, 191];
            for (int i = 0; i < prime1.Length; i++)
            {
                Console.WriteLine($"テスト{i + 1}");
                var rsa = new RsaU16((byte)prime1[i], (byte)prime2[i]);
                Console.WriteLine($"Prime1         :{rsa.Prime1}");
                Console.WriteLine($"Prime2         :{rsa.Prime2}");
                Console.WriteLine($"PublicExponent :{rsa.PublicExponent}");
                Console.WriteLine($"PublicSemiprime:{rsa.PublicSemiprime}");
                Console.WriteLine($"SecretExponent :{rsa.SecretExponent}");

                var encryptedValue = rsa.Encrypt(plainValue);
                var decryptedValue = rsa.Decrypt(encryptedValue);
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
                Console.WriteLine($"SecretExponent :{rsa.SecretExponent}");

                var encryptedValue = rsa.Encrypt(plainValue);
                var decryptedValue = rsa.Decrypt(encryptedValue);
                Console.WriteLine($"plainValue    :{plainValue}");
                Console.WriteLine($"encryptedValue:{encryptedValue}");
                Console.WriteLine($"decryptedValue:{decryptedValue}");
            }
        }
    }
}

