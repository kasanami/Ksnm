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
        public abstract uint SampleUInt();
        /// <summary>
        /// 0.0 と 1.0 の間の乱数を返します。
        /// </summary>
        /// <returns>0.0 以上 1.0 未満の倍精度浮動小数点数。</returns>
        protected override double Sample()
        {
#if true
            // TODO:約 0.00000000023283064365387 刻みでしか値が変化しない問題を抱えている。
            // 実用上は問題ないと思われる。
            var sample = SampleUInt();
            return sample / ((double)uint.MaxValue + 1);
#elif true
            // ulong.MaxValueをdoubleにキャストすると、失われるビットがある
            // そのため、計算結果が、1.0以上になる場合があるのでボツ
            ulong sample = Binary.ToUInt64(SampleUInt(), SampleUInt());
            return sample / ((double)ulong.MaxValue + 1);
#else
            // ビットレベルで変換　処理が重いのでボツ
            var sample = SampleUInt();
            return Binary.ToRateDouble(sample);
#endif
        }
    }
}
