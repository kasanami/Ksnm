using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ksnm.Randoms
{
    /// <summary>
    /// 試作
    /// </summary>
    public class Prototype : Random
    {
        uint seed;

        /// <summary>
        /// 時間に応じて決定される既定のシード値を使用し、新しいインスタンスを初期化します。
        /// </summary>
        public Prototype() : this((uint)System.DateTime.Now.Ticks) { }

        /// <summary>
        /// 指定したシード値を使用して 新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。負数を指定した場合、その数値の絶対値が使用されます。</param>
        /// <exception cref="System.OverflowException">seed が System.Int32.MinValue です。これは、絶対値が計算されるときにオーバーフローの原因となります。</exception>
        public Prototype(int seed) : this((uint)System.Math.Abs(seed)) { }

        /// <summary>
        /// 指定したシード値を使用して 新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。</param>
        public Prototype(uint seed)
        {
            this.seed = seed;
        }

        /// <summary>
        /// 0 以上で 0xFFFFFFFF 以下の乱数を返します。
        /// </summary>
        /// <returns>0 以上で 0xFFFFFFFF 以下の32 ビット符号無し整数。</returns>
        public uint SampleUInt()
        {
            seed = seed * 12345 + 123;
            return seed;
        }

        /// <summary>
        /// 0.0 と 1.0 の間の乱数を返します。
        /// </summary>
        /// <returns>0.0 以上 1.0 未満の倍精度浮動小数点数。</returns>
        protected override double Sample()
        {
            ulong sample = Binary.ToUInt64(SampleUInt(), SampleUInt());
            return sample / (double)ulong.MaxValue;
        }
    }
}
