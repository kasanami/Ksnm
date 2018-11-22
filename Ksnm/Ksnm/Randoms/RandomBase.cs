/*
The zlib License

Copyright (c) 2018 Takahiro Kasanami

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
namespace Ksnm.Randoms
{
    /// <summary>
    /// 乱数生成器の基底クラス
    /// </summary>
    public abstract class RandomBase : System.Random
    {
        /// <summary>
        /// 0 以上で 0xFFFFFFFF 以下の乱数を返します。
        /// </summary>
        /// <returns>0 以上で 0xFFFFFFFF 以下の32 ビット符号無し整数。</returns>
        public abstract uint GenerateUInt32();

        /// <summary>
        /// 0 以上で 0xFFFFFFFFFFFFFFFF 以下の乱数を返します。
        /// </summary>
        /// <returns>0 以上で 0xFFFFFFFFFFFFFFFF 以下の64 ビット符号無し整数。</returns>
        public abstract ulong GenerateUInt64();

        /// <summary>
        /// 0 以上で System.UInt32.MaxValue より小さい乱数を返します。
        /// </summary>
        /// <returns>0 以上で System.UInt32.MaxValue より小さい 64 ビット符号なし整数。</returns>
        public virtual uint NextUInt32()
        {
            var temp = GenerateUInt32();
            return temp % uint.MaxValue;
        }

        /// <summary>
        /// 指定した最大値より小さい 0 以上の乱数を返します。
        /// </summary>
        /// <param name="maxValue">生成される乱数の排他的上限値。maxValue は 0 以上にする必要があります。</param>
        /// <returns>0 以上で maxValue 未満の 32 ビット符号なし整数。
        /// つまり、通常は戻り値の範囲に 0 は含まれますが、maxValue は含まれません。
        /// ただし、maxValue が 0 の場合は、0 が返されます。
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">maxValue が 0 未満です。</exception>
        public virtual uint NextUInt32(uint maxValue)
        {
            if (maxValue < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (maxValue == 0)
            {
                return 0;
            }
            var temp = NextUInt32();
            return (temp % maxValue);
        }

        /// <summary>
        /// 0 以上で System.Int64.MaxValue より小さい乱数を返します。
        /// </summary>
        /// <returns>0 以上で System.Int64.MaxValue より小さい 64 ビット符号付整数。</returns>
        public virtual long NextInt64()
        {
            var temp = GenerateUInt64() & long.MaxValue;
            return (long)(temp % long.MaxValue);
        }

        /// <summary>
        /// 指定した最大値より小さい 0 以上の乱数を返します。
        /// </summary>
        /// <param name="maxValue">生成される乱数の排他的上限値。maxValue は 0 以上にする必要があります。</param>
        /// <returns>0 以上で maxValue 未満の 64 ビット符号付き整数。
        /// つまり、通常は戻り値の範囲に 0 は含まれますが、maxValue は含まれません。
        /// ただし、maxValue が 0 の場合は、0 が返されます。
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">maxValue が 0 未満です。</exception>
        public virtual long NextInt64(long maxValue)
        {
            if (maxValue < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (maxValue == 0)
            {
                return 0;
            }
            var temp = NextInt64();
            return (temp % maxValue);
        }

        /// <summary>
        /// 0 以上で System.UInt64.MaxValue より小さい乱数を返します。
        /// </summary>
        /// <returns>0 以上で System.UInt64.MaxValue より小さい 64 ビット符号なし整数。</returns>
        public virtual ulong NextUInt64()
        {
            var temp = GenerateUInt64() & ulong.MaxValue;
            return temp % ulong.MaxValue;
        }

        /// <summary>
        /// 指定した最大値より小さい 0 以上の乱数を返します。
        /// </summary>
        /// <param name="maxValue">生成される乱数の排他的上限値。maxValue は 0 以上にする必要があります。</param>
        /// <returns>0 以上で maxValue 未満の 64 ビット符号なし整数。
        /// つまり、通常は戻り値の範囲に 0 は含まれますが、maxValue は含まれません。
        /// ただし、maxValue が 0 の場合は、0 が返されます。
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">maxValue が 0 未満です。</exception>
        public virtual ulong NextUInt64(ulong maxValue)
        {
            if (maxValue < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (maxValue == 0)
            {
                return 0;
            }
            var temp = GenerateUInt64();
            return (temp % maxValue);
        }

        #region override for System.Random

        /// <summary>
        /// 0 以上で System.Int32.MaxValue より小さい乱数を返します。
        /// </summary>
        /// <returns>0 以上で System.Int32.MaxValue より小さい 32 ビット符号付整数。</returns>
        public override int Next()
        {
            uint temp = GenerateUInt32();
            return (int)(temp % int.MaxValue);
        }

        /// <summary>
        /// 指定した最大値より小さい 0 以上の乱数を返します。
        /// </summary>
        /// <param name="maxValue">生成される乱数の排他的上限値。maxValue は 0 以上にする必要があります。</param>
        /// <returns>0 以上で maxValue 未満の 32 ビット符号付き整数。
        /// つまり、通常は戻り値の範囲に 0 は含まれますが、maxValue は含まれません。
        /// ただし、maxValue が 0 の場合は、0 が返されます。
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">maxValue が 0 未満です。</exception>
        public override int Next(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (maxValue == 0)
            {
                return 0;
            }
            uint temp = GenerateUInt32();
            return (int)(temp % maxValue);
        }

        /// <summary>
        /// 指定した範囲内のランダムな整数を返します。
        /// </summary>
        /// <param name="minValue">返される乱数の包括的下限値。</param>
        /// <param name="maxValue">返される乱数の排他的上限値。maxValue は minValue 以上である必要があります。</param>
        /// <returns>minValue 以上で maxValue 未満の 32 ビット符号付整数。つまり、戻り値の範囲に minValue は含まれますが maxValue は含まれません。minValueが maxValue と等しい場合は、minValue が返されます。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">minValue が maxValue より大きくなっています。</exception>
        public override int Next(int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            return minValue + Next(maxValue - minValue);
        }

        /// <summary>
        /// 指定したバイト配列の要素に乱数を格納します。
        /// </summary>
        /// <param name="buffer">乱数を格納するバイト配列。</param>
        /// <exception cref="System.ArgumentNullException">buffer が null</exception>
        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new System.ArgumentNullException();
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)GenerateUInt32();
            }
        }

        /// <summary>
        /// 0.0 と 1.0 の間の乱数を返します。
        /// </summary>
        /// <returns>0.0 以上 1.0 未満の倍精度浮動小数点数。</returns>
        protected override double Sample()
        {
#if true
            // doubleにキャストしてもビットが失われない値の最大は 0x20000000000000
            // 0x20000000000000 は分母に使うので 1 つ小さい値でマスクする。
            ulong max = 0x1FFFFFFFFFFFFF;
            double sample = GenerateUInt64() & max;
            // 出力されるdoubleの最大値は、0x3FEFFFFFFFFFFFFFなので 1.0 未満
            return sample / (max + 1);
            // NOTE
            // long→double→longにキャストしたときの、値の変化前と変化後
            // 0x001FFFFFFFFFFFFF → 0x001FFFFFFFFFFFFF
            // 0x0020000000000000 → 0x0020000000000000
            // 0x0020000000000001 → 0x0020000000000000
#elif true
            // TODO:約 0.00000000023283064365387 刻みでしか値が変化しない問題を抱えている。
            // 実用上は問題ないと思われる。
            var sample = GenerateUInt32();
            return sample / ((double)uint.MaxValue + 1);
#elif true
            // ulong.MaxValueをdoubleにキャストすると、失われるビットがある
            // そのため、計算結果が、1.0以上になる場合があるのでボツ
            ulong sample = GenerateUInt64();
            return sample / ((double)ulong.MaxValue + 1);
#endif
        }

        #endregion override for System.Random
    }
}
