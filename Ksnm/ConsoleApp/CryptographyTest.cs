using Ksnm.Cryptography;
using Ksnm.Cryptography.Ecc;
using Ksnm.Cryptography.Enigma;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using System.Numerics;
using System.Text;

namespace ConsoleApp
{
    internal class CryptographyTest
    {
        public static void Run()
        {
            {
                Console.WriteLine("エニグマテスト");
                TestEnigma();
                TestEnigma8();
                TestEnigma8_2();
            }
            {
                var curve = new EllipticCurve(
                    a: 2,
                    b: 2,
                    p: 17);

                var g = new EcPoint(5, 1);

                Console.WriteLine(curve.IsOnCurve(g));

                var p = curve.Multiply(g, 2);
                var q = curve.Multiply(g, 7);

                Console.WriteLine($"2G = {p}");
                Console.WriteLine($"7G = {q}");
            }
            if (false)
            {
                Console.WriteLine("AES暗号化方式のテスト");
                TestAes128();
            }
            if (false)
            {
                Console.WriteLine("RSA暗号化方式のテスト");
                TestRsa(191, 109);
                //TestRsa64();
                //TestRsaU16();
            }
        }

        static void TestEnigma()
        {
            Console.WriteLine("TestEnigma()");

            var rotorI = new Rotor(
                "EKMFLGDQVZNTOWYHXUSPAIBRCJ",
                'Q');

            var rotorII = new Rotor(
                "AJDKSIRUXBLHWTMCQGZNPYFVOE",
                'E');

            var rotorIII = new Rotor(
                "BDFHJLCPRTXVZNYEIWGAKMUSQO",
                'V');

            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
            var plugboard = new Plugboard("AB CD EF");

            var machine = new EnigmaMachine(
                rotorI,
                rotorII,
                rotorIII,
                reflector,
                plugboard);

            machine.SetPositions("AAA");

            Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");

            string encrypted = machine.Encrypt("HELLOWORLD");

            Console.WriteLine(encrypted);
            Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");

            machine.Reset();

            Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");

            string encryptedAgain = machine.Encrypt("HELLOWORLD");

            Console.WriteLine(encryptedAgain);
            Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");

            string decrypted = machine.Decrypt("HELLOWORLD");

            Console.WriteLine(decrypted);
            Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");
        }

        static void TestEnigma8()
        {
            Console.WriteLine("TestEnigma8()");

            Random random = new Random(123456);
            var machine = new EnigmaMachine8(random);

            {
                Console.WriteLine($"Encrypt");
                machine.SetRotors(0, 0, 0);
                Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");
                var encrypted = machine.Encrypt("HELLOWORLD", false);
                Console.WriteLine($"encrypted:{encrypted.ToDebugString()}");
                Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");

                Console.WriteLine($"Decrypt");
                machine.SetRotors(0, 0, 0);
                Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");
                var decrypted = machine.DecryptToText(encrypted, false);
                Console.WriteLine($"decrypted:{decrypted}");
                Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");
            }

            {
                Console.WriteLine($"Encrypt");
                machine.ResetRotors();
                Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");
                var encrypted = machine.Encrypt("HELLOWORLD", false);
                Console.WriteLine($"encrypted:{encrypted.ToDebugString()}");
                Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");

                Console.WriteLine($"Decrypt");
                machine.ResetRotors();
                Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");
                var decrypted = machine.DecryptToText(encrypted, false);
                Console.WriteLine($"decrypted:{decrypted}");
                Console.WriteLine($"Rotor Positions: {machine.RotorPositions}");
            }
        }

        static void TestEnigma8_2()
        {
            Console.WriteLine("TestEnigma8_2()");

            byte[] planeBytes = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];

            {
                Console.WriteLine("交換なしのエニグマ");
                var machine = new EnigmaMachine8(
                    new Rotor8(),
                    new Rotor8(),
                    new Rotor8(),
                    new Reflector8(),
                    new Plugboard8()
                    );

                var encrypted = machine.Encrypt(planeBytes);
                var decrypted = machine.Decrypt(encrypted);
                Console.WriteLine($"encrypted:{encrypted.ToDebugString()}");
                Console.WriteLine($"decrypted:{decrypted.ToDebugString()}");
            }

            {
                Console.WriteLine("弱いエニグマ(リフレクターはずらしただけ)");
                var wiring = new byte[Rotor8.Size];
                for (int i = 0; i < Rotor8.Size; i++)
                {
                    wiring[i] = (byte)((i + 128) % Rotor8.Size);
                }
                var machine = new EnigmaMachine8(
                    new Rotor8(),
                    new Rotor8(),
                    new Rotor8(),
                    new Reflector8(wiring),
                    new Plugboard8()
                    );

                var encrypted = machine.Encrypt(planeBytes);
                var decrypted = machine.Decrypt(encrypted);
                Console.WriteLine($"encrypted:{encrypted.ToDebugString()}");
                Console.WriteLine($"decrypted:{decrypted.ToDebugString()}");
            }

            {
                Console.WriteLine("弱いエニグマ(リフレクターはずらしただけ+ローターずらしただけ)");
                Random random = new Random(123456);
                var wiring1 = new byte[Rotor8.Size];
                for (int i = 0; i < Rotor8.Size; i++)
                {
                    wiring1[i] = (byte)((i + 1) % Rotor8.Size);
                }
                var wiring2 = new byte[Rotor8.Size];
                for (int i = 0; i < Rotor8.Size; i++)
                {
                    wiring2[i] = (byte)((i + 2) % Rotor8.Size);
                }
                var wiring3 = new byte[Rotor8.Size];
                for (int i = 0; i < Rotor8.Size; i++)
                {
                    wiring3[i] = (byte)((i + 3) % Rotor8.Size);
                }
                var wiring128 = new byte[Rotor8.Size];
                for (int i = 0; i < Rotor8.Size; i++)
                {
                    wiring128[i] = (byte)((i + 128) % Rotor8.Size);
                }
                var machine = new EnigmaMachine8(
                    new Rotor8(wiring1, 1),
                    new Rotor8(wiring2, 2),
                    new Rotor8(wiring3, 3),
                    new Reflector8(wiring128),
                    new Plugboard8()
                    );

                var encrypted = machine.Encrypt(planeBytes);
                var decrypted = machine.Decrypt(encrypted);
                Console.WriteLine($"encrypted:{encrypted.ToDebugString()}");
                Console.WriteLine($"decrypted:{decrypted.ToDebugString()}");
            }

            {
                Console.WriteLine("弱いエニグマ(リフレクターはずらしただけ+ローターはランダム)");
                Random random = new Random(123456);
                var wiring128 = new byte[Rotor8.Size];
                for (int i = 0; i < Rotor8.Size; i++)
                {
                    wiring128[i] = (byte)((i + 128) % Rotor8.Size);
                }
                var machine = new EnigmaMachine8(
                    new Rotor8(random),
                    new Rotor8(random),
                    new Rotor8(random),
                    new Reflector8(wiring128),
                    new Plugboard8()
                    );

                var encrypted = machine.Encrypt(planeBytes);
                var decrypted = machine.Decrypt(encrypted);
                Console.WriteLine($"encrypted:{encrypted.ToDebugString()}");
                Console.WriteLine($"decrypted:{decrypted.ToDebugString()}");
            }
        }

        #region AES
        static void TestAes128()
        {
            Console.WriteLine("AES-128暗号化方式のテスト");

            byte[] key = new byte[16]
            {
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,
                0x08,0x09,0x0A,0x0B,0x0C,0x0D,0x0E,0x0F
            };

            byte[] iv = new byte[16]
            {
                0x10,0x11,0x12,0x13,0x14,0x15,0x16,0x17,
                0x18,0x19,0x1A,0x1B,0x1C,0x1D,0x1E,0x1F
            };

            byte[] plaintext = System.Text.Encoding.UTF8.GetBytes("Hello AES from scratch!");

            var aes = new Aes(key);
            byte[] ciphertext = aes.EncryptCbc(plaintext, iv);
            byte[] recovered = aes.DecryptCbc(ciphertext, iv);

            Console.WriteLine($"Plain : {System.Text.Encoding.UTF8.GetString(plaintext)}");
            Console.WriteLine($"Cipher: {Convert.ToHexString(ciphertext)}");
            Console.WriteLine($"Back  : {System.Text.Encoding.UTF8.GetString(recovered)}");

            //D7DFEF9C698D98D8880C76FADFFFF1DFC818364AA95EB16AE9449D9DEADF3BD7
        }
        #endregion AES

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
            {
                Console.WriteLine($"テスト 文字列");
                var plainText = "Hello, World!";
                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                var rsa = new RsaU16();
                Console.WriteLine($"Prime1         :{rsa.Prime1}");
                Console.WriteLine($"Prime2         :{rsa.Prime2}");
                Console.WriteLine($"PublicExponent :{rsa.PublicExponent}");
                Console.WriteLine($"PublicSemiprime:{rsa.PublicSemiprime}");
                Console.WriteLine($"SecretExponent :{rsa.SecretExponent}");

                var encryptedValues = rsa.Encrypt(plainBytes);
                var decryptedValues = rsa.Decrypt(encryptedValues);
                Console.WriteLine($"plainText      :{plainText}");
                Console.WriteLine($"encryptedValues:{encryptedValues.ToDebugString()}");
                Console.WriteLine($"decryptedText  :{Encoding.UTF8.GetString(decryptedValues)}");
            }
        }
    }
}
