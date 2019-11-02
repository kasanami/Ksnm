using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitsType = System.UInt16;// 半精度浮動小数点数 全体のビットを表す型

namespace Ksnm.Numerics
{
    /// <summary>
    /// 半精度浮動小数点数型
    /// </summary>
    public struct FloatingPointNumber16
    {
        #region 定数
        /// <summary>
        /// 数値 0 を表します。
        /// </summary>
        public const BitsType ZeroBits = 0;
        /// <summary>
        /// 数値 1 を表します。
        /// </summary>
        public const BitsType OneBits = 0x3C00;
        /// <summary>
        /// 負の 1 (-1) を表します。
        /// </summary>
        public const BitsType MinusOneBits = 0xBC00;
        /// <summary>
        /// 最小有効値を表します。
        /// </summary>
        public const BitsType MinValueBits = 0xFBFF;
        /// <summary>
        /// 最大有効値を表します。
        /// </summary>
        public const BitsType MaxValueBits = 0x7BFF;
        /// <summary>
        /// ゼロより大きい最小の値を表します。
        /// </summary>
        public const BitsType EpsilonBits = 0x0001;
        /// <summary>
        /// 負の無限大を表します。
        /// </summary>
        public const BitsType NegativeInfinityBits = 0xFC00;
        /// <summary>
        /// 正の無限大を表します。
        /// </summary>
        public const BitsType PositiveInfinityBits = 0x7C00;
        /// <summary>
        /// 非数 (NaN) の値を表します。
        /// </summary>
        public const BitsType NaNBits = 0b1_11111_10_0000_0000;

        /// <summary>
        /// 数値 0 を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 Zero = new FloatingPointNumber16 { Bits = ZeroBits };
        /// <summary>
        /// 数値 1 を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 One = new FloatingPointNumber16 { Bits = OneBits };
        /// <summary>
        /// 負の 1 (-1) を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 MinusOne = new FloatingPointNumber16 { Bits = MinusOneBits };
        /// <summary>
        /// 最小有効値を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 MinValue = new FloatingPointNumber16 { Bits = MinValueBits };
        /// <summary>
        /// 最大有効値を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 MaxValue = new FloatingPointNumber16 { Bits = MaxValueBits };
        /// <summary>
        /// ゼロより大きい最小の値を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 Epsilon = new FloatingPointNumber16 { Bits = EpsilonBits };
        /// <summary>
        /// 負の無限大を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 NegativeInfinity = new FloatingPointNumber16 { Bits = NegativeInfinityBits };
        /// <summary>
        /// 正の無限大を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 PositiveInfinity = new FloatingPointNumber16 { Bits = PositiveInfinityBits };
        /// <summary>
        /// 非数 (NaN) の値を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 NaN = new FloatingPointNumber16 { Bits = NaNBits };
        #endregion 定数

        #region プロパティ
        /// <summary>
        /// 全体のビット
        /// </summary>
        public BitsType Bits { get; private set; }
        /// <summary>
        /// 符号部ビット
        /// 負数ならば1になる。
        /// </summary>
        public BitsType SignBits
        {
            get => (BitsType)((Bits & 0x8000) >> 15);
            set
            {
                value &= 1;
                if (value == 1)
                {
                    Bits |= 0x8000;
                }
                else
                {
                    Bits &= 0x7FFF;
                }
            }
        }
        /// <summary>
        /// 指数
        /// </summary>
        public int Exponent
        {
            get => ExponentBits - 15;
            set => ExponentBits = (ushort)(value + 15);
        }
        /// <summary>
        /// 指数部ビット
        /// </summary>
        public BitsType ExponentBits
        {
            get => (BitsType)((Bits & 0b01111100_00000000) >> 10);
            set => Bits ^= (BitsType)((value & 0b11111) << 10);
        }
        /// <summary>
        /// 仮数部ビット
        /// </summary>
        public BitsType MantissaBits
        {
            get => (BitsType)(Bits & 0b00000011_11111111);
            set => Bits ^= (BitsType)(value & 0b00000011_11111111);
        }
        #endregion プロパティ

        /// <summary>
        /// 倍精度浮動小数点数に変換する
        /// </summary>
        public double ToDouble()
        {
            ulong doubleBits = 0;
            // 符号部
            var sign = SignBits != 0;
            // 指数部
            int exponentBits = ExponentBits;
            // 仮数部
            ulong mantissaBits = MantissaBits;
            if (exponentBits == 0x1F)
            {
                if (mantissaBits == 0)
                {
                    if (sign)
                    {
                        return double.NegativeInfinity;
                    }
                    else
                    {
                        return double.PositiveInfinity;
                    }
                }
                else
                {
                    return double.NaN;
                }
            }
            else if (exponentBits == 0)
            {
                doubleBits = 0;
            }
            else
            {
                exponentBits -= 15;
                exponentBits += 1023;
                doubleBits |= (ulong)exponentBits << (52);
            }
            // 符号部
            if (sign)
            {
                doubleBits |= 0x8000_0000_0000_0000;
            }
            // 仮数部
            {
                doubleBits |= mantissaBits << (52 - 10);
            }
            return BitConverter.Int64BitsToDouble((long)doubleBits);
        }
    }
}
