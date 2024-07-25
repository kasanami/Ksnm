using System.Numerics;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 10進数型インターフェイス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IDecimal<T> where T : INumber<T>
    {
    }
}