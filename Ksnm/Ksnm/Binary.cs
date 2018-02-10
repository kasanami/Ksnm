/*
The zlib License

Copyright (c) 2017-2018 Takahiro Kasanami

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.

2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.

3. This notice may not be removed or altered from any source distribution.
*/

namespace Ksnm
{
    /// <summary>
    /// 2進数処理
    /// </summary>
    public static class Binary
    {
        /// <summary>
        /// 各ビット数で表現可能な値の数
        /// </summary>
        public static ulong[] MaxValues = new ulong[]
        {
            0,
            (1UL<<1 )-1,(1UL<<2 )-1,(1UL<<3 )-1,(1UL<<4 )-1,(1UL<<5 )-1,(1UL<<6 )-1,(1UL<<7 )-1,(1UL<<8 )-1,
            (1UL<<9 )-1,(1UL<<10)-1,(1UL<<11)-1,(1UL<<12)-1,(1UL<<13)-1,(1UL<<14)-1,(1UL<<15)-1,(1UL<<16)-1,
            (1UL<<17)-1,(1UL<<18)-1,(1UL<<19)-1,(1UL<<20)-1,(1UL<<21)-1,(1UL<<22)-1,(1UL<<23)-1,(1UL<<24)-1,
            (1UL<<25)-1,(1UL<<26)-1,(1UL<<27)-1,(1UL<<28)-1,(1UL<<29)-1,(1UL<<30)-1,(1UL<<31)-1,(1UL<<32)-1,
            (1UL<<33)-1,(1UL<<34)-1,(1UL<<35)-1,(1UL<<36)-1,(1UL<<37)-1,(1UL<<38)-1,(1UL<<39)-1,(1UL<<40)-1,
            (1UL<<41)-1,(1UL<<42)-1,(1UL<<43)-1,(1UL<<44)-1,(1UL<<45)-1,(1UL<<46)-1,(1UL<<47)-1,(1UL<<48)-1,
            (1UL<<49)-1,(1UL<<50)-1,(1UL<<51)-1,(1UL<<52)-1,(1UL<<53)-1,(1UL<<54)-1,(1UL<<55)-1,(1UL<<56)-1,
            (1UL<<57)-1,(1UL<<58)-1,(1UL<<59)-1,(1UL<<60)-1,(1UL<<61)-1,(1UL<<62)-1,(1UL<<63)-1,0xFFFFFFFFFFFFFFFF,
        };

        #region FillOne

        /// <summary>
        /// 最上位ビットに近い1から、最下位ビット方向を1で埋める
        /// </summary>
        /// <returns></returns>
        public static uint FillOneFromLeadingOneToLSB(uint bits)
        {
            bits = bits | (bits >> 1);
            bits = bits | (bits >> 2);
            bits = bits | (bits >> 4);
            bits = bits | (bits >> 8);
            bits = bits | (bits >> 16);
            return bits;
        }

        /// <summary>
        /// 最上位ビットに近い1から、最下位ビット方向を1で埋める
        /// </summary>
        /// <returns></returns>
        public static ulong FillOneFromLeadingOneToLSB(ulong bits)
        {
            bits = bits | (bits >> 1);
            bits = bits | (bits >> 2);
            bits = bits | (bits >> 4);
            bits = bits | (bits >> 8);
            bits = bits | (bits >> 16);
            bits = bits | (bits >> 32);
            return bits;
        }

        #endregion FillOne

        #region CountOne

        /// <summary>
        ///  1 のビットを数える
        /// </summary>
        public static int CountOne(uint bits)
        {
            bits = (bits & 0x55555555) + (bits >> 1 & 0x55555555);
            bits = (bits & 0x33333333) + (bits >> 2 & 0x33333333);
            bits = (bits & 0x0f0f0f0f) + (bits >> 4 & 0x0f0f0f0f);
            bits = (bits & 0x00ff00ff) + (bits >> 8 & 0x00ff00ff);
            bits = (bits & 0x0000ffff) + (bits >> 16 & 0x0000ffff);
            return (int)bits;
        }

        /// <summary>
        ///  1 のビットを数える
        /// </summary>
        public static int CountOne(int bits)
        {
            return CountOne((uint)bits);
        }

        /// <summary>
        ///  1 のビットを数える
        /// </summary>
        public static int CountOne(ulong bits)
        {
            bits = (bits & 0x5555555555555555) + (bits >> 1 & 0x5555555555555555);
            bits = (bits & 0x3333333333333333) + (bits >> 2 & 0x3333333333333333);
            bits = (bits & 0x0f0f0f0f0f0f0f0f) + (bits >> 4 & 0x0f0f0f0f0f0f0f0f);
            bits = (bits & 0x00ff00ff00ff00ff) + (bits >> 8 & 0x00ff00ff00ff00ff);
            bits = (bits & 0x0000ffff0000ffff) + (bits >> 16 & 0x0000ffff0000ffff);
            bits = (bits & 0x00000000ffffffff) + (bits >> 32 & 0x00000000ffffffff);
            return (int)bits;
        }

        /// <summary>
        ///  1 のビットを数える
        /// </summary>
        public static int CountOne(long bits)
        {
            return CountOne((ulong)bits);
        }

        #endregion CountOne

        #region CountLeadingZero

        /// <summary>
        /// 最上位ビットから連続している0の数
        /// </summary>
        /// <returns></returns>
        public static int CountLeadingZero(byte bits)
        {
            bits = (byte)(bits | (bits >> 1));
            bits = (byte)(bits | (bits >> 2));
            bits = (byte)(bits | (bits >> 4));
            return CountOne((byte)~bits);
        }

        /// <summary>
        /// 最上位ビットから連続している0の数
        /// </summary>
        /// <returns></returns>
        public static int CountLeadingZero(sbyte bits)
        {
            return CountLeadingZero((byte)bits);
        }

        /// <summary>
        /// 最上位ビットから連続している0の数
        /// </summary>
        /// <returns></returns>
        public static int CountLeadingZero(ushort bits)
        {
            bits = (ushort)(bits | (bits >> 1));
            bits = (ushort)(bits | (bits >> 2));
            bits = (ushort)(bits | (bits >> 4));
            bits = (ushort)(bits | (bits >> 8));
            return CountOne((ushort)~bits);
        }

        /// <summary>
        /// 最上位ビットから連続している0の数
        /// </summary>
        /// <returns></returns>
        public static int CountLeadingZero(short bits)
        {
            return CountLeadingZero((ushort)bits);
        }

        /// <summary>
        /// 最上位ビットから連続している0の数
        /// </summary>
        /// <returns></returns>
        public static int CountLeadingZero(uint bits)
        {
            bits = bits | (bits >> 1);
            bits = bits | (bits >> 2);
            bits = bits | (bits >> 4);
            bits = bits | (bits >> 8);
            bits = bits | (bits >> 16);
            return CountOne(~bits);
        }

        /// <summary>
        /// 最上位ビットから連続している0の数
        /// </summary>
        /// <returns></returns>
        public static int CountLeadingZero(int bits)
        {
            return CountLeadingZero((uint)bits);
        }

        /// <summary>
        /// 最上位ビットから連続している0の数
        /// </summary>
        /// <returns></returns>
        public static int CountLeadingZero(ulong bits)
        {
            bits = bits | (bits >> 1);
            bits = bits | (bits >> 2);
            bits = bits | (bits >> 4);
            bits = bits | (bits >> 8);
            bits = bits | (bits >> 16);
            bits = bits | (bits >> 32);
            return CountOne(~bits);
        }

        /// <summary>
        /// 最上位ビットから連続している0の数
        /// </summary>
        /// <returns></returns>
        public static int CountLeadingZero(long bits)
        {
            return CountLeadingZero((ulong)bits);
        }

        #endregion CountLeadingZero

        #region CountTrainingZero

        /// <summary>
        /// 最下位ビットから連続している0の数
        /// </summary>
        public static int CountTrainingZero(uint bits)
        {
            return CountOne((~bits) & (bits - 1));
        }

        /// <summary>
        /// 最下位ビットから連続している0の数
        /// </summary>
        public static int CountTrainingZero(int bits)
        {
            return CountOne((~bits) & (bits - 1));
        }

        /// <summary>
        /// 最下位ビットから連続している0の数
        /// </summary>
        public static int CountTrainingZero(ulong bits)
        {
            return CountOne((~bits) & (bits - 1));
        }

        /// <summary>
        /// 最下位ビットから連続している0の数
        /// </summary>
        public static int CountTrainingZero(long bits)
        {
            return CountOne((~bits) & (bits - 1));
        }

        #endregion CountTrainingZero

        #region IsPowerOf2

        /// <summary>
        /// 2の累乗ならtrueを返す
        /// </summary>
        public static bool IsPowerOf2(int bits)
        {
            return (bits & (bits - 1)) == 0;
        }

        /// <summary>
        /// 2の累乗ならtrueを返す
        /// </summary>
        public static bool IsPowerOf2(uint bits)
        {
            return (bits & (bits - 1)) == 0;
        }

        /// <summary>
        /// 2の累乗ならtrueを返す
        /// </summary>
        public static bool IsPowerOf2(long bits)
        {
            return (bits & (bits - 1)) == 0;
        }

        /// <summary>
        /// 2の累乗ならtrueを返す
        /// </summary>
        public static bool IsPowerOf2(ulong bits)
        {
            return (bits & (bits - 1)) == 0;
        }

        #endregion IsPowerOf2

        #region FloorPowerOf2

        /// <summary>
        /// 床関数
        /// </summary>
        public static uint FloorPowerOf2(uint bits)
        {
            bits = bits | (bits >> 1);
            bits = bits | (bits >> 2);
            bits = bits | (bits >> 4);
            bits = bits | (bits >> 8);
            bits = bits | (bits >> 16);
            return bits - (bits >> 1);
        }

        /// <summary>
        /// 床関数
        /// </summary>
        public static int FloorPowerOf2(int bits)
        {
            return (int)FloorPowerOf2((uint)bits);
        }

        /// <summary>
        /// 床関数
        /// </summary>
        public static ulong FloorPowerOf2(ulong bits)
        {
            bits = bits | (bits >> 1);
            bits = bits | (bits >> 2);
            bits = bits | (bits >> 4);
            bits = bits | (bits >> 8);
            bits = bits | (bits >> 16);
            bits = bits | (bits >> 32);
            return bits - (bits >> 1);
        }

        /// <summary>
        /// 床関数
        /// </summary>
        public static long FloorPowerOf2(long bits)
        {
            return (long)FloorPowerOf2((ulong)bits);
        }

        #endregion FloorPowerOf2

        #region CeilingPowerOf2

        /// <summary>
        /// 天井関数
        /// </summary>
        public static int CeilingPowerOf2(int bits)
        {
            bits = bits - 1;
            bits = bits | (bits >> 1);
            bits = bits | (bits >> 2);
            bits = bits | (bits >> 4);
            bits = bits | (bits >> 8);
            bits = bits | (bits >> 16);
            return bits + 1;
        }

        /// <summary>
        /// 天井関数
        /// </summary>
        public static long CeilingPowerOf2(long bits)
        {
            bits = bits - 1;
            bits = bits | (bits >> 1);
            bits = bits | (bits >> 2);
            bits = bits | (bits >> 4);
            bits = bits | (bits >> 8);
            bits = bits | (bits >> 16);
            bits = bits | (bits >> 32);
            return bits + 1;
        }

        #endregion CeilingPowerOf2

        #region ToUInt16

        /// <summary>
        /// 指定された値から変換された 16 ビット符号なし整数を返します。
        /// </summary>
        /// <param name="left">上位8ビットを指定する値</param>
        /// <param name="right">下位8ビットを指定する値</param>
        public static ushort ToUInt16(byte left, byte right)
        {
            return (ushort)((left << 8) | right);
        }

        /// <summary>
        /// 指定された値から変換された 16 ビット符号なし整数を返します。
        /// </summary>
        /// <param name="left">上位8ビットを指定する値</param>
        /// <param name="right">下位8ビットを指定する値</param>
        public static ushort ToUInt16(sbyte left, sbyte right)
        {
            return ToUInt16((byte)left, (byte)right);
        }

        #endregion ToUInt16

        #region ToUInt32

        /// <summary>
        /// 指定された値から変換された 32 ビット符号なし整数を返します。
        /// </summary>
        /// <param name="left">上位16ビットを指定する値</param>
        /// <param name="right">下位16ビットを指定する値</param>
        public static uint ToUInt32(ushort left, ushort right)
        {
            return ((uint)left << 16) | right;
        }

        /// <summary>
        /// 指定された値から変換された 32 ビット符号なし整数を返します。
        /// </summary>
        /// <param name="left">上位16ビットを指定する値</param>
        /// <param name="right">下位16ビットを指定する値</param>
        public static uint ToUInt32(short left, short right)
        {
            return ToUInt32((ushort)left, (ushort)right);
        }

        #endregion ToUInt32

        #region ToUInt64

        /// <summary>
        /// 指定された値から変換された 64 ビット符号なし整数を返します。
        /// </summary>
        /// <param name="left">上位32ビットを指定する値</param>
        /// <param name="right">下位32ビットを指定する値</param>
        public static ulong ToUInt64(uint left, uint right)
        {
            return ((ulong)left << 32) | right;
        }

        /// <summary>
        /// 指定された値から変換された 64 ビット符号なし整数を返します。
        /// </summary>
        /// <param name="left">上位32ビットを指定する値</param>
        /// <param name="right">下位32ビットを指定する値</param>
        public static ulong ToUInt64(int left, int right)
        {
            return ToUInt64((uint)left, (uint)right);
        }

        #endregion ToUInt64

        /// <summary>
        /// 指定された値から変換された 16 ビット符号なし整数を返します。
        /// </summary>
        public static ushort ToUInt16ByExpand(byte bits)
        {
            return ToUInt16(bits, bits);
        }
        /// <summary>
        /// 指定された値から変換された 32 ビット符号なし整数を返します。
        /// </summary>
        public static uint ToUInt32ByExpand(byte bits)
        {
            ushort bits2 = ToUInt16ByExpand(bits);
            return ToUInt32(bits2, bits2);
        }
        /// <summary>
        /// 指定された値から変換された 32 ビット符号なし整数を返します。
        /// </summary>
        public static uint ToUInt32ByExpand(ushort bits)
        {
            return ToUInt32(bits, bits);
        }
        /// <summary>
        /// 指定された値から変換された 32 ビット符号なし整数を返します。
        /// </summary>
        public static ulong ToUInt64ByExpand(byte bits)
        {
            uint bits2 = ToUInt32ByExpand(bits);
            return ToUInt64(bits2, bits2);
        }
        /// <summary>
        /// 指定された値から変換された 32 ビット符号なし整数を返します。
        /// </summary>
        public static ulong ToUInt64ByExpand(ushort bits)
        {
            uint bits2 = ToUInt32ByExpand(bits);
            return ToUInt64(bits2, bits2);
        }
        /// <summary>
        /// 指定された値から変換された 32 ビット符号なし整数を返します。
        /// </summary>
        public static ulong ToUInt64ByExpand(uint bits)
        {
            return ToUInt64(bits, bits);
        }

        /// <summary>
        /// ビット数を変更し、値の大きさはビット数に応じて拡縮される。
        /// ビット数を減らした場合、下位ビットは消失する。（不可逆圧縮）
        /// ビット数を増やした場合、下位ビットを補間します。
        /// 8→16ビット例：0xFF(8ビットの最大値)→0xFFFF(16ビットの最大値)
        /// </summary>
        public static ulong Scale(ulong originalBits, int originalBitsCount, int destBitsCount)
        {
            if (originalBitsCount > destBitsCount)
            {
                var diff = originalBitsCount - destBitsCount;
                return originalBits >> diff;
            }
            else if (destBitsCount > originalBitsCount)
            {
                var diff = destBitsCount - originalBitsCount;
                ulong result = 0;
                for (int i = diff; i > -originalBitsCount; i -= originalBitsCount)
                {
                    if (i > 0)
                    {
                        result |= originalBits << i;
                    }
                    else if (i < 0)
                    {
                        result |= originalBits >> -i;
                    }
                    else
                    {
                        result |= originalBits;
                    }
                }
                return result;
            }
            return originalBits;
        }

        // doubleにキャストしてから割り算したほうが速いので無効化
#if false
        /// <summary>
        /// 指定した 64 ビット符号無し整数を、0 以上 1 未満の倍精度浮動小数点数に変換します。
        /// <para>精度の都合上、下位のビットは無視されます。</para>
        /// </summary>
        public static double ToRateDouble(ulong bits)
        {
            if (bits == 0)
            {
                return 0;
            }
            var leadingZeroCount = CountLeadingZero(bits);
            // 指数部
            ulong exponent = (uint)(1023 - leadingZeroCount) & 0x7FF;
            // 仮数部
            ulong fraction = bits >> (12 - leadingZeroCount - 1);// 最上位ビットの0と1を一つ消す
            fraction &= 0x000F_FFFF_FFFF_FFFF;
            // 合成
            ulong uint64Bits = (exponent << 52) | fraction;
            return BitConverter.Int64BitsToDouble((long)uint64Bits);
        }

        /// <summary>
        /// 指定した 32 ビット符号無し整数を、0 以上 1 未満の倍精度浮動小数点数に変換します。
        /// <para>精度の都合上の不足のビットは、bits をシフトし補います。</para>
        /// </summary>
        public static double ToRateDouble(uint bits)
        {
            ulong uint64Bits = ToUInt64(bits, bits);
            return ToRateDouble(uint64Bits);
        }
#endif
    }
}
