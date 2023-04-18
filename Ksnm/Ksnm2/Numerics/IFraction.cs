using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm2.Numerics
{
    /// <summary>
    /// 分数型インターフェイス
    /// </summary>
    interface IFraction<TSelf, TNumerator, TDenominator>
        where TSelf : INumber<TSelf>
        where TNumerator : INumber<TNumerator>
        where TDenominator : INumber<TDenominator>
    {
        /// <summary>
        /// 分子
        /// </summary>
        public TNumerator Numerator { get; set; }
        /// <summary>
        /// 分母
        /// </summary>
        public TDenominator Denominator { get; set; }
    }
}