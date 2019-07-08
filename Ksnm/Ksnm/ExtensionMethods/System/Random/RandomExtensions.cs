/*
The zlib License

Copyright (c) 2014-2019 Takahiro Kasanami

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
using Original = System;

namespace Ksnm.ExtensionMethods.System.Random
{
    /// <summary>
    /// Randomの拡張メソッド
    /// </summary>
    public static class RandomExtensions
    {

        /// <summary>
        /// false と true の乱数を返します。
        /// </summary>
        public static bool NextBool(this Original.Random random)
        {
            return random.Next(2) == 1;
        }

        /// <summary>
        /// 0 以上のランダムな整数を返します。
        /// </summary>
        /// <returns>0 以上で System.Int64.MaxValue より小さい 64 ビット符号付整数。</returns>
        public static long NextLong(this Original.Random random)
        {
            return random.NextLong(long.MaxValue);
        }

        /// <summary>
        /// 0 以上のランダムな整数を返します。
        /// </summary>
        /// <returns>0 以上で System.Int64.MaxValue より小さい 64 ビット符号付整数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">maxValue が 0 未満です。</exception>
        public static long NextLong(this Original.Random random, long minValue, long maxValue)
        {
            return minValue + random.NextLong(maxValue - minValue);
        }

        /// <summary>
        /// 指定した最大値より小さい 0 以上のランダムな整数を返します。
        /// </summary>
        /// <param name="maxValue">生成される乱数の排他的上限値。maxValue は 0 以上にする必要があります。</param>
        /// <returns>0 以上で maxValue 未満の 64 ビット符号付整数。
        /// つまり、通常は戻り値の範囲に 0 は含まれますが、maxValue は含まれません。
        /// ただし、maxValue が 0 の場合は、0 が返されます。
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">maxValue が 0 未満です。</exception>
        public static long NextLong(this Original.Random random, long maxValue)
        {
            if (maxValue < 0)
            {
                throw new global::System.ArgumentOutOfRangeException();
            }
            if (maxValue == 0)
            {
                return 0;
            }
            // メモ:random.Next()の32ビット整数は最上位ビットが常に0なので使用できない。
            var buffer = new byte[sizeof(long)];
            random.NextBytes(buffer);
            long result = Original.BitConverter.ToInt64(buffer, 0);
            return (result & long.MaxValue) % maxValue;
        }

        /// <summary>
        /// 0.0 以上 1.0 以下のランダムな浮動小数点数を返します。
        /// </summary>
        /// <returns>0.0 以上 1.0 以下の倍精度浮動小数点数。</returns>
        public static double UnitInterval(this Original.Random random)
        {
            var buffer = new byte[sizeof(ulong)];
            random.NextBytes(buffer);
            double value = Original.BitConverter.ToUInt64(buffer, 0);
            return value / ulong.MaxValue;
        }

        /// <summary>
        /// 指定した範囲内のランダムな整数を返します。
        /// </summary>
        /// <param name="min">返される乱数の包括的下限値。</param>
        /// <param name="max">返される乱数の包括的上限値。</param>
        public static int Range(this Original.Random random, int min, int max)
        {
            return min + random.Next(max - min + 1);
        }

        /// <summary>
        /// 指定した範囲内のランダムな浮動小数点数を返します。
        /// </summary>
        /// <param name="min">返される乱数の包括的下限値。</param>
        /// <param name="max">返される乱数の包括的上限値。</param>
        public static float Range(this Original.Random random, float min, float max)
        {
            return (float)random.Range((double)min, (double)max);
        }

        /// <summary>
        /// 指定した範囲内のランダムな浮動小数点数を返します。
        /// </summary>
        /// <param name="min">返される乱数の包括的下限値。</param>
        /// <param name="max">返される乱数の包括的上限値。</param>
        public static double Range(this Original.Random random, double min, double max)
        {
            // t=0.0以上1.0以下の値
            // メモ:NextDouble()は1.0"未満"なので使用できない。
            var t = random.UnitInterval();
            return Math.Lerp(min, max, t);
        }
    }
}
