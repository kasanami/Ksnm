using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Science.Mathematics
{
    /// <summary>
    /// 数学の予想
    /// </summary>
    public static class Conjecture
    {
        /// <summary>
        /// ゴールドバッハの予想
        /// 指定した数の範囲の偶数が、2つの素数の和として表すことができるか判定する。
        /// このとき、2つの素数は同じであってもよい。
        /// </summary>
        /// <param name="maxValue">テストする最大値</param>
        /// <returns>ゴールドバッハの予想が成り立つならtrue</returns>
        public static bool GoldbachsConjecture(int minValue, int maxValue)
        {
            // 奇数なら1足す
            if (int.IsOddInteger(minValue))
            {
                minValue++;
            }
            for (int i = minValue; i <= maxValue; i += 2)
            {
                if (GoldbachsConjecture(i) == false)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 指定した数値が、2つの素数の和として表すことができるか判定する。
        /// </summary>
        /// <param name="value"></param>
        /// <returns>true:表すことができる</returns>
        public static bool GoldbachsConjecture(int value)
        {
            for (int p = 2; p < value; p++)
            {
                if (IsPrime(p) == false)
                {
                    continue;
                }
                var value2 = value - p;
                if (IsPrime(value2))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 素数一覧
        /// </summary>
        static List<int> PrimeNumbers = new List<int>();
        /// <summary>
        /// 素数か判定する
        /// </summary>
        static bool IsPrime(int value)
        {
            if(PrimeNumbers.Contains(value))
            {
                return true;
            }
            if(Math.IsPrime(value))
            {
                PrimeNumbers.Add(value);
                return true;
            }
            return false;
        }
    }
}
