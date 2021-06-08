using System.Numerics;

namespace Ksnm.Science.Mathematics
{
    /// <summary>
    /// 数学の定理
    /// </summary>
    public class Theorem
    {
        /// <summary>
        /// ウィルソンの定理
        /// p が大きくなるにつれて計算量が膨大になるため、素数を判定するために用いるには実用的ではない。
        /// ※ 1 は例外で true となる。
        /// </summary>
        /// <returns>p が素数ならば true</returns>
        public static bool Wilsons(int p)
        {
            return (Math.Factorial((BigInteger)p - 1) + 1) % p == 0;
        }
    }
}