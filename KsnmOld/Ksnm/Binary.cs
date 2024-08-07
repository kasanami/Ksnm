﻿/*
The zlib License

Copyright (c) 2017-2019 Takahiro Kasanami

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
using System;
using System.Collections.ObjectModel;

namespace Ksnm
{
    /// <summary>
    /// 2進数処理
    /// </summary>
    public static class Binary
    {
        /// <summary>
        /// 各ビット数で表現可能な最大値
        /// </summary>
        public static readonly ReadOnlyCollection<ulong> MaxValues = Array.AsReadOnly(new ulong[]
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
        });

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

        #region IsPowerOfTwo

        /// <summary>
        /// 2の累乗ならtrueを返す
        /// </summary>
        public static bool IsPowerOfTwo(int bits)
        {
            return (bits & (bits - 1)) == 0;
        }

        /// <summary>
        /// 2の累乗ならtrueを返す
        /// </summary>
        public static bool IsPowerOfTwo(uint bits)
        {
            return (bits & (bits - 1)) == 0;
        }

        /// <summary>
        /// 2の累乗ならtrueを返す
        /// </summary>
        public static bool IsPowerOfTwo(long bits)
        {
            return (bits & (bits - 1)) == 0;
        }

        /// <summary>
        /// 2の累乗ならtrueを返す
        /// </summary>
        public static bool IsPowerOfTwo(ulong bits)
        {
            return (bits & (bits - 1)) == 0;
        }

        #endregion IsPowerOfTwo

        #region FloorPowerOfTwo

        /// <summary>
        /// 床関数
        /// </summary>
        public static uint FloorPowerOfTwo(uint bits)
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
        public static int FloorPowerOfTwo(int bits)
        {
            return (int)FloorPowerOfTwo((uint)bits);
        }

        /// <summary>
        /// 床関数
        /// </summary>
        public static ulong FloorPowerOfTwo(ulong bits)
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
        public static long FloorPowerOfTwo(long bits)
        {
            return (long)FloorPowerOfTwo((ulong)bits);
        }

        #endregion FloorPowerOfTwo

        #region CeilingPowerOfTwo

        /// <summary>
        /// 天井関数
        /// </summary>
        public static int CeilingPowerOfTwo(int bits)
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
        public static uint CeilingPowerOfTwo(uint bits)
        {
            return (uint)CeilingPowerOfTwo((int)bits);
        }

        /// <summary>
        /// 天井関数
        /// </summary>
        public static long CeilingPowerOfTwo(long bits)
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

        /// <summary>
        /// 天井関数
        /// </summary>
        public static ulong CeilingPowerOfTwo(ulong bits)
        {
            return (ulong)CeilingPowerOfTwo((long)bits);
        }

        #endregion CeilingPowerOfTwo

        #region ToInt16

        /// <summary>
        /// 指定された値から変換された 16 ビット符号付整数を返します。
        /// </summary>
        /// <param name="left">上位8ビットを指定する値</param>
        /// <param name="right">下位8ビットを指定する値</param>
        public static short ToInt16(byte left, byte right)
        {
            return (short)((left << 8) | right);
        }

        /// <summary>
        /// 指定された値から変換された 16 ビット符号付整数を返します。
        /// </summary>
        /// <param name="left">上位8ビットを指定する値</param>
        /// <param name="right">下位8ビットを指定する値</param>
        public static short ToInt16(sbyte left, sbyte right)
        {
            return ToInt16((byte)left, (byte)right);
        }

        #endregion ToInt16

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

        #region ToInt32

        /// <summary>
        /// 指定された値から変換された 32 ビット符号付整数を返します。
        /// </summary>
        /// <param name="left">上位16ビットを指定する値</param>
        /// <param name="right">下位16ビットを指定する値</param>
        public static int ToInt32(ushort left, ushort right)
        {
            return ((int)left << 16) | right;
        }

        /// <summary>
        /// 指定された値から変換された 32 ビット符号付整数を返します。
        /// </summary>
        /// <param name="left">上位16ビットを指定する値</param>
        /// <param name="right">下位16ビットを指定する値</param>
        public static int ToInt32(short left, short right)
        {
            return ToInt32((ushort)left, (ushort)right);
        }

        #endregion ToInt32

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

        #region ToInt64

        /// <summary>
        /// 指定された値から変換された 64 ビット符号付整数を返します。
        /// </summary>
        /// <param name="left">上位32ビットを指定する値</param>
        /// <param name="right">下位32ビットを指定する値</param>
        public static long ToInt64(uint left, uint right)
        {
            return ((long)left << 32) | right;
        }

        /// <summary>
        /// 指定された値から変換された 64 ビット符号付整数を返します。
        /// </summary>
        /// <param name="left">上位32ビットを指定する値</param>
        /// <param name="right">下位32ビットを指定する値</param>
        public static long ToInt64(int left, int right)
        {
            return ToInt64((uint)left, (uint)right);
        }

        #endregion ToInt64

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

        /// <summary>
        /// 指定した 64 ビット符号無し整数を、0 以上 1 未満の倍精度浮動小数点数に変換します。
        /// <para>例：0→0.0</para>
        /// <para>例：ulong.MaxValue→0.9999999…</para>
        /// <para>※精度の都合上、下位のビットは無視されます。</para>
        /// </summary>
        public static double ToScaledDouble(ulong bits)
        {
#if true
            // doubleにキャストしてもビットが失われない値の最大は 0x20_0000_0000_0000
            // bits の最大値を 0x1F_FFFF_FFFF_FFFF にするため 11 ビットシフトする
            const double Max = 0x0020_0000_0000_0000;
            const int RBitShift = 11;
            bits >>= RBitShift;
            // 出力されるdoubleの最大値は、0x3FEFFFFFFFFFFFFFなので 1.0 未満
            return bits / Max;
            // NOTE
            // long→double→longにキャストしたときの、値の変化前と変化後
            // 0x001FFFFFFFFFFFFF → 0x001FFFFFFFFFFFFF
            // 0x0020000000000000 → 0x0020000000000000
            // 0x0020000000000001 → 0x0020000000000000
#elif false
            // TODO:約 0.00000000023283064365387 刻みでしか値が変化しない問題を抱えている。
            // 実用上は問題ないと思われる。
            var sample = GenerateUInt32();
            return sample / ((double)uint.MaxValue + 1);
#elif false
            // ulongの最大値はdoubleでは下位の桁が無視され、+1しても変化がない。
            // そのため、計算結果が、1.0になる場合がある。
            ulong sample = GenerateUInt64();
            return sample / ((double)ulong.MaxValue + 1);
#elif false
            // doubleにキャストしてから割り算したほうが速いので無効化
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
#endif
        }

        /// <summary>
        /// 指定した 32 ビット符号無し整数を、0 以上 1 未満の倍精度浮動小数点数に変換します。
        /// <para>精度の都合上の不足のビットは、bits をシフトし補います。</para>
        /// </summary>
        public static double ToScaledDouble(uint bits)
        {
            ulong uint64Bits = ToUInt64(bits, bits);
            return ToScaledDouble(uint64Bits);
        }

        #region Rotate
        /// <summary>
        /// 左ローテート
        /// </summary>
        /// <param name="bits">操作する値</param>
        /// <param name="shift">シフトするビット数</param>
        /// <returns>ローテートした値</returns>
        public static uint RotateLeft(uint bits, int shift)
        {
            return ((bits >> (~shift)) >> 1) | (bits << shift);
        }
        /// <summary>
        /// 左ローテート
        /// </summary>
        /// <param name="bits">操作する値</param>
        /// <param name="shift">シフトするビット数</param>
        /// <returns>ローテートした値</returns>
        public static ulong RotateLeft(ulong bits, int shift)
        {
            return ((bits >> (~shift)) >> 1) | (bits << shift);
        }
        /// <summary>
        /// 右ローテート
        /// </summary>
        /// <param name="bits">操作する値</param>
        /// <param name="shift">シフトするビット数</param>
        /// <returns>ローテートした値</returns>
        public static uint RotateRight(uint bits, int shift)
        {
            return ((bits << (~shift)) << 1) | (bits >> shift);
        }
        /// <summary>
        /// 右ローテート
        /// </summary>
        /// <param name="bits">操作する値</param>
        /// <param name="shift">シフトするビット数</param>
        /// <returns>ローテートした値</returns>
        public static ulong RotateRight(ulong bits, int shift)
        {
            return ((bits << (~shift)) << 1) | (bits >> shift);
        }
        #endregion Rotate

        #region BitShift
        /// <summary>
        /// 配列をまたいで右ビットシフトする。
        /// bits[0]の最上位ビットの隣は、bits[1]の最下位ビットとする。
        /// </summary>
        /// <param name="bits">ビット配列</param>
        /// <param name="shift">シフトするビット数</param>
        public static void RightShift(int[] bits, int shift)
        {
            if (shift == 0)
            {
                return;
            }
            // shiftが32を超える場合2つ次の要素からビット値をコピーする
            int offset = shift / 32;
            shift %= 32;
            if (shift > 0)
            {
                // ビットシフト実施
                for (int i = 0; i < bits.Length; i++)
                {
                    // もとのshiftが32未満の場合は、その要素をシフトする。
                    // 32を超える場合は、次の要素をシフトしてコピーする。
                    var sourceIndex = i + offset;
                    if (sourceIndex < bits.Length)
                    {
                        bits[i] = LogicalRightShift(bits[sourceIndex], shift);
                    }
                    else
                    {
                        bits[i] = 0;
                    }
                    // 次の要素のビット値を上位ビットにコピー
                    sourceIndex += 1;
                    if (sourceIndex < bits.Length)
                    {
                        bits[i] |= bits[sourceIndex] << (32 - shift);
                    }
                }
            }
            else
            {
                // もとのshiftが32の倍数の場合、次の要素からそのままコピーする
                // 最後の要素は0にする。
                for (int i = 0; i < bits.Length; i++)
                {
                    // 次の要素のビット値を上位ビットにコピー
                    var nextIndex = i + offset;
                    if (nextIndex < bits.Length)
                    {
                        bits[i] = bits[nextIndex];
                    }
                    else
                    {
                        bits[i] = 0;
                    }
                }
            }
        }
        /// <summary>
        /// 符号付きの整数を論理右シフトする。
        /// </summary>
        /// <param name="bits">シフトする値</param>
        /// <param name="shift">シフトするビット数</param>
        /// <returns>シフト後の値</returns>
        public static int LogicalRightShift(int bits, int shift)
        {
            return (int)((uint)bits >> shift);
        }
        #endregion BitShift
    }
}
