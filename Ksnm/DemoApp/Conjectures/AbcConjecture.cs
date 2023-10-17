using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Conjectures
{
    /// <summary>
    /// ABC予想に関する関数
    /// </summary>
    public class AbcConjecture
    {
        /// <summary>
        /// 根基 
        /// </summary>
        /// <param name="value">任意の自然数</param>
        /// <returns>互いに異なる素因数の積</returns>
        public static int Radical(int value)
        {
            var values = Ksnm.Math.PrimeFactorization(value);
            return Radical(values);
        }
        /// <summary>
        /// 根基 
        /// </summary>
        /// <param name="values">素因数</param>
        /// <returns>互いに異なる素因数の積</returns>
        public static int Radical(IEnumerable<int> values)
        {
            values = values.Distinct();// 重複を削除
            return values.Aggregate((accumulator, item) => accumulator * item);// すべての要素の積
        }
    }
}
