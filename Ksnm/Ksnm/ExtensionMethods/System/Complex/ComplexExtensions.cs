using Ksnm.ExtensionMethods.System.Double;
using complex = System.Numerics.Complex;// 拡張元の型

namespace Ksnm.ExtensionMethods.System.Complex
{
    /// <summary>
    /// Complexの拡張メソッド
    /// </summary>
    public static class ComplexExtensions
    {
        #region Is*
        /// <summary>
        /// 正数なら true を返します。
        /// </summary>
        public static bool IsPositive(this complex value)
        {
            return value.Real > 0;
        }
        /// <summary>
        /// 負数なら true を返します。
        /// </summary>
        public static bool IsNegative(this complex value)
        {
            return value.Real < 0;
        }
        /// <summary>
        /// 負または正の無限大なら true を返します。
        /// </summary>
        public static bool IsInfinity(this complex value)
        {
            return value.Real.IsInfinity();
        }
        /// <summary>
        /// 整数なら true を返します。
        /// </summary>
        public static bool IsInteger(this complex value)
        {
            return value.Real.IsInteger();
        }
        #endregion Is*
    }
}
