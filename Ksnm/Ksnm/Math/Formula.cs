using static System.Math;

namespace Ksnm
{
    public static partial class Math
    {
        /// <summary>
        /// 公式
        /// </summary>
        public static class Formula
        {
            /// <summary>
            /// ライプニッツの公式
            /// </summary>
            /// <returns>PI/4(円周率の4分の1)</returns>
            public static double Leibniz(int cout)
            {
                double sum = 0.0;
                for (var i = 0; i < cout; i++)
                {
                    if (IsEven(i))
                    {
                        sum += 1.0 / (2.0 * i + 1);
                    }
                    else
                    {
                        sum -= 1.0 / (2.0 * i + 1);
                    }
                }
                return sum;
            }
        }
    }
}
