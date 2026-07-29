using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Cryptography
{
    using Int8 = SByte;
    using UInt8 = Byte;
    /// <summary>
    /// RSA暗号化方式を実装するクラス。（RsaU16の16は公開合成数のビット数を意味する）
    /// - 平文は UInt8 型
    /// - 暗号文は UInt16 型
    /// - 素数は UInt8 型
    /// - 公開合成数は UInt16 型
    /// </summary>
    public class RsaU16
    {
        /// <summary>
        /// 秘密鍵を生成するための素数1
        /// </summary>
        public UInt8 Prime1 { get; }
        /// <summary>
        /// 秘密鍵を生成するための素数2
        /// </summary>
        public UInt8 Prime2 { get; }
        /// <summary>
        /// 公開半素数
        /// </summary>
        public UInt16 PublicSemiprime { get; }
        /// <summary>
        /// 公開指数(素数)
        /// </summary>
        public UInt8 PublicExponent { get; }
        /// <summary>
        /// 秘密指数
        /// </summary>
        public UInt16 SecretExponent { get; }
        /// <summary>
        /// 公開指数のデフォルト値。素数でなければならない。
        /// </summary>
        const UInt8 DefaultPublicExponent = 17;
        /// <summary>
        /// RSA暗号化方式のインスタンスを生成する。素数はランダムに生成される。
        /// </summary>
        public RsaU16() : this(GeneratePrime(), GeneratePrime(), DefaultPublicExponent)
        {
        }
        /// <summary>
        /// RSA暗号化方式のインスタンスを生成する。素数は引数で指定する。
        /// </summary>
        /// <param name="prime1">秘密鍵を生成するための素数1</param>
        /// <param name="prime2">秘密鍵を生成するための素数2</param>
        /// <param name="publicExponent">公開指数</param>
        /// <exception cref="ArgumentException"></exception>
        public RsaU16(UInt8 prime1, UInt8 prime2, UInt8 publicExponent = DefaultPublicExponent)
        {
            checked
            {
                if (prime1 > UInt8.MaxValue)
                {
                    throw new ArgumentException($"{nameof(prime1)}は{UInt8.MaxValue}以下でなければならない。");
                }
                if (prime2 > UInt8.MaxValue)
                {
                    throw new ArgumentException($"{nameof(prime2)}は{UInt8.MaxValue}以下でなければならない。");
                }
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
                PublicSemiprime = (UInt16)(Prime1 * Prime2);
                if (PublicSemiprime <= UInt8.MaxValue)
                {
                    throw new ArgumentException($"{nameof(PublicSemiprime)}は{UInt8.MaxValue}より大きくなければならない。");
                }
                PublicExponent = publicExponent;
                // オイラーのトーシェント関数の値
                UInt16 phi = (UInt16)((Prime1 - 1) * (Prime2 - 1));
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
        public UInt16 Encrypt(UInt8 plainValue)
        {
            return ModPow(plainValue, PublicExponent, PublicSemiprime);
        }
        /// <summary>
        /// 平文を暗号化する。
        /// </summary>
        /// <param name="plainValues">平文</param>
        /// <returns>暗号文</returns>
        public UInt16[] Encrypt(ReadOnlySpan<UInt8> plainValues)
        {
            var encryptedValues = new UInt16[plainValues.Length];
            for (int i = 0; i < plainValues.Length; i++)
            {
                encryptedValues[i] = Encrypt(plainValues[i]);
            }
            return encryptedValues;
        }
        /// <summary>
        /// 暗号文を復号する。
        /// </summary>
        /// <param name="encryptedValue">暗号文</param>
        /// <returns>平文</returns>
        public UInt8 Decrypt(UInt16 encryptedValue)
        {
            return (UInt8)ModPow(encryptedValue, SecretExponent, PublicSemiprime);
        }
        /// <summary>
        /// 暗号文を復号する。
        /// </summary>
        /// <param name="encryptedValues">暗号文</param>
        /// <returns>平文</returns>
        public UInt8[] Decrypt(ReadOnlySpan<UInt16> encryptedValues)
        {
            var decryptedValues = new UInt8[encryptedValues.Length];
            for (int i = 0; i < encryptedValues.Length; i++)
            {
                decryptedValues[i] = Decrypt(encryptedValues[i]);
            }
            return decryptedValues;
        }

        #region Utility
        /// <summary>
        /// 素数をランダムに生成する。
        /// </summary>
        static UInt8 GeneratePrime() => (UInt8)Math.GeneratePrime(17, UInt8.MaxValue);
        /// <summary>
        /// value が素数であるかどうかを判定するアルゴリズム。
        /// </summary>
        static bool IsPrime(UInt8 value) => Math.IsPrime(value);
        /// <summary>
        /// a と m が互いに素であるとき、a の m に関する逆元を計算するアルゴリズム。
        /// ※ 一時的にマイナス値になるため、int を使用して計算する。
        /// </summary>
        static UInt16 ModInverse(UInt16 a, UInt16 m) => (UInt16)Math.ModInverse<int>(a, m);
        /// <summary>
        /// value^exponent mod modulus を計算する。
        /// ※ 一時的に大きい値になるため、BigInteger を使用して計算する。
        /// </summary>
        static UInt16 ModPow(UInt16 value, UInt16 exponent, UInt16 modulus) => (UInt16)Math.ModPow<BigInteger>(value, exponent, modulus);
        /// <summary>
        /// a と b の最大公約数を計算する
        /// </summary>
        public static int Gcd(int a, int b) => Math.GreatestCommonDivisor(a, b);
        #endregion Utility
    }
}
