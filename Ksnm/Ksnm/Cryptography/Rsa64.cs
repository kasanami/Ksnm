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
        public int Prime1 { get; }
        /// <summary>
        /// 秘密鍵を生成するための素数2
        /// </summary>
        public int Prime2 { get; }
        /// <summary>
        /// 公開合成数
        /// </summary>
        public long PublicSemiprime { get; }
        /// <summary>
        /// 公開指数(素数)
        /// </summary>
        public int PublicExponent { get; }
        /// <summary>
        /// 秘密指数
        /// </summary>
        public long SecretExponent { get; }
        /// <summary>
        /// RSA暗号化方式のインスタンスを生成する。素数はランダムに生成される。
        /// </summary>
        public Rsa64() : this(GeneratePrimeInt32(), GeneratePrimeInt32(), 17)
        {
        }
        /// <summary>
        /// RSA暗号化方式のインスタンスを生成する。素数は引数で指定する。
        /// </summary>
        /// <param name="prime1">秘密鍵を生成するための素数1</param>
        /// <param name="prime2">秘密鍵を生成するための素数2</param>
        /// <param name="publicExponent">公開指数</param>
        /// <exception cref="ArgumentException"></exception>
        public Rsa64(int prime1, int prime2, int publicExponent = 17)
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
                if (IsPrime(publicExponent) == false)
                {
                    throw new ArgumentException($"{nameof(publicExponent)}は素数でなければならない。");
                }
                Prime1 = prime1;
                Prime2 = prime2;
                PublicSemiprime = Prime1 * Prime2;
                PublicExponent = publicExponent;
                // オイラーのトーシェント関数の値
                long phi = (long)(Prime1 - 1) * (long)(Prime2 - 1);
                if (Gcd(PublicExponent, phi) != 1)
                {
                    throw new ArgumentException($"{nameof(publicExponent)}は{phi}と互いに素でなければならない。");
                }
                SecretExponent = ModInverse(PublicExponent, phi);
            }
        }
        /// <summary>
        /// 平文を暗号化する。
        /// </summary>
        /// <param name="plainValue">平文</param>
        /// <returns>暗号文</returns>
        public long Encrypt(int plainValue)
        {
            return ModPow(plainValue, PublicExponent, PublicSemiprime);
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
        /// </summary>
        static long ModInverse(long a, long m) => Math.ModInverse(a, m);
        /// <summary>
        /// value^exponent mod modulus を効率的に計算するアルゴリズム。
        /// </summary>
        static long ModPow(long value, long exponent, long modulus) => Math.ModPow(value, exponent, modulus);
        /// <summary>
        /// a と b の最大公約数を計算する
        /// </summary>
        public static long Gcd(long a, long b) => Math.GreatestCommonDivisor(a, b);
        #endregion Utility
    }
}
