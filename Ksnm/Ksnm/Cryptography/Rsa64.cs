using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Cryptography
{
    /// <summary>
    /// RSA暗号化方式を実装するクラス。（Rsa64の64は公開合成数のビット数を意味する）
    /// - 平文はint型とする
    /// - 素数はint型とする
    /// - 公開合成数はlong型とする
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
        /// 公開半素数
        /// - int型の素数2つの積はlong型に収まるため、long型を使用する。
        /// - 平文はint型であるため、公開合成数は0xFFFFFFFFより大きくなければならない。
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
        /// 公開指数のデフォルト値。素数でなければならない。一般的には65537が使用されるが、257もよく使用される。
        /// </summary>
        const int DefaultPublicExponent = 257;
        /// <summary>
        /// RSA暗号化方式のインスタンスを生成する。素数はランダムに生成される。
        /// </summary>
        public Rsa64() : this(GeneratePrime(), GeneratePrime(), DefaultPublicExponent)
        {
        }
        /// <summary>
        /// RSA暗号化方式のインスタンスを生成する。素数は引数で指定する。
        /// </summary>
        /// <param name="prime1">秘密鍵を生成するための素数1</param>
        /// <param name="prime2">秘密鍵を生成するための素数2</param>
        /// <param name="publicExponent">公開指数</param>
        /// <exception cref="ArgumentException"></exception>
        public Rsa64(int prime1, int prime2, int publicExponent = DefaultPublicExponent)
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
                PublicSemiprime = (long)Prime1 * (long)Prime2;
                if (PublicSemiprime <= int.MaxValue)
                {
                    throw new ArgumentException($"{nameof(PublicSemiprime)}は{int.MaxValue}より大きくなければならない。");
                }
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
        /// 素数をランダムに生成する。
        /// </summary>
        static int GeneratePrime() => Math.GeneratePrime(46_340, int.MaxValue);
        /// <summary>
        /// value が素数であるかどうかを判定するアルゴリズム。
        /// </summary>
        static bool IsPrime(long value) => Math.IsPrime(value);
        /// <summary>
        /// a と m が互いに素であるとき、a の m に関する逆元を計算するアルゴリズム。
        /// </summary>
        static long ModInverse(long a, long m) => Math.ModInverse(a, m);
        /// <summary>
        /// value^exponent mod modulus を計算する。
        /// ※ 一時的に大きい値になるため、BigInteger を使用して計算する。
        /// </summary>
        static long ModPow(long value, long exponent, long modulus) => (long)Math.ModPow<BigInteger>(value, exponent, modulus);
        /// <summary>
        /// a と b の最大公約数を計算する
        /// </summary>
        public static long Gcd(long a, long b) => Math.GreatestCommonDivisor(a, b);
        #endregion Utility
    }
}
