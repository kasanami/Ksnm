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
namespace Ksnm.Randoms
{
    /// <summary>
    /// 数値を単純にインクリメントし出力する。
    /// 乱数としては使えないが、テストに使用する事を想定している。
    /// </summary>
    public class IncrementRandom : RandomBase
    {
        /// <summary>
        /// 現在の内部数値
        /// </summary>
        uint current;
        /// <summary>
        /// 現在の内部数値
        /// </summary>
        public uint Current
        {
            get { return current; }
            set
            {
                current = value % Cycle;
            }
        }
        /// <summary>
        /// 周期（内部数値の最大値+1）
        /// </summary>
        uint cycle = uint.MaxValue;
        /// <summary>
        /// 周期（内部数値の最大値+1）
        /// </summary>
        public uint Cycle
        {
            get { return cycle; }
            set
            {
                cycle = System.Math.Max(value, 1);
            }
        }


        /// <summary>
        /// 時間に応じて決定される既定のシード値を使用し、新しいインスタンスを初期化します。
        /// </summary>
        public IncrementRandom() : this((uint)System.DateTime.Now.Ticks) { }

        /// <summary>
        /// 指定したシード値を使用して 新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値。負数を指定した場合、その数値の絶対値が使用されます。</param>
        public IncrementRandom(int seed) : this((uint)System.Math.Abs(seed)) { }

        /// <summary>
        /// 指定したシード値を使用して 新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値。</param>
        public IncrementRandom(uint seed)
        {
            current = seed;
        }

        /// <summary>
        /// 指定したシード値を使用して 新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値。</param>
        /// <param name="cycle">周期</param>
        public IncrementRandom(uint seed, uint cycle)
        {
            current = seed;
            Cycle = cycle;
        }

        /// <summary>
        /// 内部数値を更新
        /// </summary>
        /// <returns>更新後の内部数値</returns>
        protected uint Update()
        {
            current++;
            if (current >= cycle)
            {
                current = 0;
            }
            return current;
        }

        /// <summary>
        /// 0 以上で System.Int32.MaxValue より小さい乱数を返します。
        /// </summary>
        /// <returns>0 以上で System.Int32.MaxValue より小さい 32 ビット符号付整数。</returns>
        public override int Next()
        {
            return (int)(Sample() * int.MaxValue);
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
                throw new System.ArgumentOutOfRangeException();
            if (maxValue == 0)
                return 0;
            return (int)(Sample() * maxValue);
        }

        /// <summary>
        /// 0 以上で 0xFFFFFFFF 以下の乱数を返します。
        /// </summary>
        /// <returns>0 以上で 0xFFFFFFFF 以下の32 ビット符号無し整数。</returns>
        public override uint GenerateUInt32()
        {
            return Update();
        }

        /// <summary>
        /// 0 以上で 0xFFFFFFFFFFFFFFFF 以下の乱数を返します。
        /// </summary>
        /// <returns>0 以上で 0xFFFFFFFFFFFFFFFF 以下の64 ビット符号無し整数。</returns>
        public override ulong GenerateUInt64()
        {
            return Update();
        }

        /// <summary>
        /// 0.0 と 1.0 の間の乱数を返します。
        /// </summary>
        /// <returns>0.0 以上 1.0 未満の倍精度浮動小数点数。</returns>
        protected override double Sample()
        {
            Update();
            return (double)current / (double)cycle;
        }
    }
}
