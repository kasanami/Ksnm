using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 分数型インターフェイス
    /// </summary>
    interface IFraction<TSelf, TNumerator, TDenominator> :
        IFloatingPointIeee754<TSelf>
        where TSelf : INumber<TSelf>, IFloatingPointIeee754<TSelf>
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
        /// <summary>
        /// 逆数を取得する
        /// </summary>
        public TSelf Reciprocal { get; }
        /// <summary>
        /// 約分する
        /// </summary>
        public void Reduce();
        /// <summary>
        /// 可約ならtrueを返す。
        /// </summary>
        public bool IsReducible();
    }
}