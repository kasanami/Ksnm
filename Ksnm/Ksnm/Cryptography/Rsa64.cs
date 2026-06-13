using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Cryptography
{
    /// <summary>
    /// RSA暗号化方式を実装するクラス。
    /// </summary>
    public class Rsa64
    {
        /// <summary>
        /// 秘密鍵を生成するための素数1
        /// </summary>
        public long Prime1 { get; }
        /// <summary>
        /// 秘密鍵を生成するための素数2
        /// </summary>
        public long Prime2 { get; }
        /// <summary>
        /// 公開合成数
        /// </summary>
        public long PublicSemiprime { get; }
        /// <summary>
        /// 公開指数(素数)
        /// </summary>
        public long PublicExponent { get; } = 17;
        /// <summary>
        /// 秘密指数
        /// </summary>
        public long SecretExponent { get; }
        /// <summary>
        /// RSA暗号化方式のインスタンスを生成する。素数はランダムに生成される。
        /// </summary>
        public Rsa64() : this(GeneratePrimeInt32(), GeneratePrimeInt32())
        {
        }
        /// <summary>
        /// RSA暗号化方式のインスタンスを生成する。素数は引数で指定する。
        /// </summary>
        /// <param name="prime1">秘密鍵を生成するための素数1</param>
        /// <param name="prime2">秘密鍵を生成するための素数2</param>
        /// <exception cref="ArgumentException"></exception>
        public Rsa64(int prime1, int prime2)
        {
            checked
            {
                if (IsPrime(prime1) == false)
                {
                    throw new ArgumentException($"{nameof(prime1)}は素数でなければならない。");
                }
                if (IsPrime(prime2) == false)
                {
                    throw new ArgumentException($"{nameof(prime2)}は素数でなければならない。");
                }
                Prime1 = prime1;
                Prime2 = prime2;
                PublicSemiprime = Prime1 * Prime2;
                // オイラーのトーシェント関数の値
                long phi = (Prime1 - 1) * (Prime2 - 1);
                SecretExponent = ModInverse(PublicExponent, phi);
            }
        }
        /// <summary>
        /// 平文を暗号化する。
        /// </summary>
        /// <param name="plainValue">平文</param>
        /// <returns>暗号文</returns>
        public long Encrypt(long plainValue)
        {
            return ModPow(plainValue, PublicExponent, PublicSemiprime);
        }
        /// <summary>
        /// 暗号文を復号する。
        /// </summary>
        /// <param name="encryptedValue">暗号文</param>
        /// <returns>平文</returns>
        public long Decrypt(long encryptedValue)
        {
            return ModPow(encryptedValue, SecretExponent, PublicSemiprime);
        }

        #region Utility
        /// <summary>
        /// 32ビット整数の素数をランダムに生成する。生成される素数は1000以上である。
        /// </summary>
        static int GeneratePrimeInt32()
        {
            while (true)
            {
                int value = RandomNumberGenerator.GetInt32(
                    1_000,
                    int.MaxValue);

                // 偶数を避ける
                value |= 1;

                if (IsPrime(value))
                    return value;
            }
        }
        /// <summary>
        /// value が素数であるかどうかを判定するアルゴリズム。
        /// </summary>
        static bool IsPrime(long value)
        {
            if (value < 2)
                return false;

            if (value == 2)
                return true;

            if ((value & 1) == 0)
                return false;

            long limit = (long)Math.Sqrt(value);

            for (long i = 3; i <= limit; i += 2)
            {
                if (value % i == 0)
                    return false;
            }

            return true;
        }
        /// <summary>
        /// a と m が互いに素であるとき、a の m に関する逆元を計算するアルゴリズム。
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        static long ModInverse(long a, long m)
        {
            long m0 = m;
            long x0 = 0;
            long x1 = 1;

            while (a > 1)
            {
                long q = a / m;

                (a, m) = (m, a % m);
                (x0, x1) = (x1 - q * x0, x0);
            }

            if (x1 < 0)
            {
                x1 += m0;
            }

            return x1;
        }
        /// <summary>
        /// value^exponent mod modulus を効率的に計算するアルゴリズム。
        /// </summary>
        static long ModPow(long value, long exponent, long modulus)
        {
            checked
            {
                long result = 1;

                value %= modulus;

                while (exponent > 0)
                {
                    if ((exponent & 1) == 1)
                    {
                        result = (result * value) % modulus;
                    }
                    exponent >>= 1;
                    value = (value * value) % modulus;
                }

                return result;
            }
        }
        #endregion Utility
    }
}
