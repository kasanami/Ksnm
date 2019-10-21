/*
The zlib License

Copyright (c) 2014-2018 Takahiro Kasanami

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
/*
    このクラスは次のアルゴリズムを使用しています。「Xorshift RNGs」
    「Xorshift RNGs」は、ジョージ・マーサグリアが2003年に開発しました。

    This class uses the following algorithm. "Xorshift RNGs"
    The "Xorshift RNGs", George Marsaglia was developed in 2003.
 */
namespace Ksnm.Randoms
{
    /// <summary>
    /// Xorshift RNGsをSystem.Randomを継承して実装したクラス
    /// </summary>
    public class Xorshift128 : RandomBase
    {
        /// <summary>
        /// 内部パラメータ
        /// </summary>
        public uint w, x, y, z;

        /// <summary>
        /// 時間に応じて決定される既定のシード値を使用し、新しいインスタンスを初期化します。
        /// </summary>
        public Xorshift128() : this((uint)System.DateTime.Now.Ticks) { }

        /// <summary>
        /// 指定したシード値を使用して 新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。負数を指定した場合、その数値の絶対値が使用されます。</param>
        /// <exception cref="System.OverflowException">seed が System.Int32.MinValue です。これは、絶対値が計算されるときにオーバーフローの原因となります。</exception>
        public Xorshift128(int seed) : this((uint)System.Math.Abs(seed)) { }

        /// <summary>
        /// 指定したシード値を使用して 新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。</param>
        public Xorshift128(uint seed)
            : this(123456789, 362436069, 521288629, 88675123)
        {
            seed = (uint)(seed * 2685821657736338717);
            x ^= seed;
            y ^= seed;
            z ^= seed;
            w ^= seed;
        }

        /// <summary>
        /// 指定した値を使用して 新しいインスタンスを初期化します。
        /// </summary>
        public Xorshift128(uint x, uint y, uint z, uint w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// 0 以上で 0xFFFFFFFF 以下の乱数を返します。
        /// </summary>
        /// <returns>0 以上で 0xFFFFFFFF 以下の32 ビット符号無し整数。</returns>
        public override uint GenerateUInt32()
        {
            uint t = (x ^ (x << 11));
            x = y;
            y = z;
            z = w;
            w = (w ^ (w >> 19)) ^ (t ^ (t >> 8));
            return w;
        }

        /// <summary>
        /// 0 以上で 0xFFFFFFFFFFFFFFFF 以下の乱数を返します。
        /// </summary>
        /// <returns>0 以上で 0xFFFFFFFFFFFFFFFF 以下の64 ビット符号無し整数。</returns>
        public override ulong GenerateUInt64()
        {
            return Binary.ToUInt64(GenerateUInt32(), GenerateUInt32());
        }
    }
}
