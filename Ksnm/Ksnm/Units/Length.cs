using Ksnm.Numerics;

namespace Ksnm.Units
{
    /// <summary>
    /// 長さの単位
    /// </summary>
    public class Length<T> : Quantity<T> where T : IMath<T>
    {
        /// <summary>
        /// 乗算し面積を計算する
        /// </summary>
        public static Area<T> operator *(Length<T> valueL, Length<T> valueR)
        {
            return new Area<T>(valueL, valueR);
        }
    }
}