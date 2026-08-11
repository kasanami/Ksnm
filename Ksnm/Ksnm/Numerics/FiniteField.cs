using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics;

/// <summary>
/// 有限体を表す構造体です。
/// ・要素数が任意のため、ジェネリック型パラメータ T を使用します。
/// ・要素数は任意のためテスト用。
/// </summary>
public readonly struct FiniteField<T> : IEquatable<FiniteField<T>>,
    IAdditionOperators<FiniteField<T>, FiniteField<T>, FiniteField<T>>,
    ISubtractionOperators<FiniteField<T>, FiniteField<T>, FiniteField<T>>,
    IMultiplyOperators<FiniteField<T>, FiniteField<T>, FiniteField<T>>,
    IDivisionOperators<FiniteField<T>, FiniteField<T>, FiniteField<T>>
    where T : struct, INumber<T>,
    IAdditionOperators<T, T, T>,
    ISubtractionOperators<T, T, T>,
    IMultiplyOperators<T, T, T>,
    IDivisionOperators<T, T, T>
{
    /// <summary>
    /// 有限体の要素を表します。
    /// </summary>
    public T Value { get; }
    /// <summary>
    /// 有限体の要素数を表します。
    /// </summary>
    public T Quantity { get; }
    /// <summary>
    /// 有限体の要素を初期化します。
    /// </summary>
    public FiniteField(T value, T quantity)
    {
        Value = value;
        Quantity = quantity;
    }
    #region 演算子
    public static FiniteField<T> operator +(FiniteField<T> a, FiniteField<T> b)
    {
        if(!a.Quantity.Equals(b.Quantity))
            throw new InvalidOperationException("Quantityが異なる有限体同士の演算はできません。");
        return new((a.Value + b.Value) % a.Quantity, a.Quantity);
    }

    public static FiniteField<T> operator -(FiniteField<T> a, FiniteField<T> b){
        if(!a.Quantity.Equals(b.Quantity))
            throw new InvalidOperationException("Quantityが異なる有限体同士の演算はできません。");
        return new(((a.Value + a.Quantity - b.Value) % a.Quantity), a.Quantity);
    }

    public static FiniteField<T> operator *(FiniteField<T> a, FiniteField<T> b)
    {
        if (!a.Quantity.Equals(b.Quantity))
            throw new InvalidOperationException("Quantityが異なる有限体同士の演算はできません。");
        return new(((a.Value * b.Value) % a.Quantity), a.Quantity);
    }

    public static FiniteField<T> operator /(FiniteField<T> a, FiniteField<T> b)
    {
        if (!a.Quantity.Equals(b.Quantity))
            throw new InvalidOperationException("Quantityが異なる有限体同士の演算はできません。");
        if (b.Value == T.Zero)
            throw new DivideByZeroException();

        return new(((a.Value * MultiplicativeInverse(b)) % a.Quantity), a.Quantity);
    }
    public static FiniteField<T> operator -(FiniteField<T> value)
    {
        return new(((value.Quantity - value.Value) % value.Quantity), value.Quantity);
    }
    #endregion 演算子
    /// <summary>
    /// 有限体における乗法逆元を計算します。
    /// </summary>
    private static T MultiplicativeInverse(FiniteField<T> value)
    {
        // 拓張ユークリッドの互除法を使用して、valueの乗法逆元を計算します。
        T a = value.Value;
        T b = value.Quantity;
        T x0 = T.One, x1 = T.Zero;
        while (a > T.One)
        {
            T q = a / b;
            (a, b) = (b, a % b);
            (x0, x1) = (x1, x0 - q * x1);
        }
        if (x0 < T.Zero)
            x0 += value.Quantity;
        return x0;
    }

    public bool Equals(FiniteField<T> other)
    {
        return Value.Equals(other.Value) && Quantity.Equals(other.Quantity);
    }

    public override bool Equals(object? obj)
        => obj is FiniteField<T> other && Equals(other);

    public override int GetHashCode()
        => Value.GetHashCode() ^ Quantity.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(FiniteField<T> left, FiniteField<T> right)
        => left.Value == right.Value;

    public static bool operator !=(FiniteField<T> left, FiniteField<T> right)
        => left.Value != right.Value;

    public static implicit operator T(FiniteField<T> value)
        => value.Value;
}
