using System.Numerics;

namespace Ksnm2.Numerics
{
    /// <summary>
    /// 10進数型インターフェイス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IDecimal<T> where T : INumber<T>
    {
    }
}