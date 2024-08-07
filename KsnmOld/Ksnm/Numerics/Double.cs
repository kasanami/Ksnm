﻿using System;

namespace Ksnm.Numerics
{
    /// <summary>
    /// System.Double のラッパー
    /// </summary>
    public struct Double : IMath<Double>, IEquatable<Double>
    {
        #region プロパティ
        /// <summary>
        /// 値
        /// </summary>
        public double Value;
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 指定した値で初期化する
        /// </summary>
        public Double(double value)
        {
            Value = value;
        }
        #endregion コンストラクタ
        /// <summary>
        /// 指定された 2 つのインスタンスが同じ値を表しているかどうかを示す値を返します。
        /// </summary>
        /// <param name="d1">比較する最初の値です。</param>
        /// <param name="d2">比較する 2 番目の値です。</param>
        /// <returns>d1 と d2 が等しい場合は true。それ以外の場合は false。</returns>
        public static bool Equals(Double d1, Double d2)
        {
            return d1.Value == d2.Value;
        }
        #region 型変換
        /// <summary>
        /// byte から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Double(double value)
        {
            return new Double(value);
        }
        /// <summary>
        /// byte から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator double(Double value)
        {
            return value.Value;
        }
        #endregion 型変換
        #region IMath
        public Double Zero => 0.0;

        public Double One => 1.0;

        public Double MinusOne => -1.0;

        public int Sign => Math.Sign(Value);

        public bool IsZero => Value == 0;

        public bool IsOne => Value == 1;

        public Double Abs()
        {
            return System.Math.Abs(Value);
        }

        public Double Add(Double addend)
        {
            return Value + addend;
        }

        public Double Add(int addend)
        {
            return Value + addend;
        }

        public Double Add(double addend)
        {
            return Value + addend;
        }

        public Double Add(decimal addend)
        {
            return Value + (double)addend;
        }

        public Double Ceiling()
        {
            return System.Math.Ceiling(Value);
        }

        public int Compare(Double other)
        {
            if (Value > other)
            {
                return +1;
            }
            else if (Value < other)
            {
                return -1;
            }
            return 0;
        }

        public Double Divide(Double divisor)
        {
            return Value / divisor;
        }

        public Double Divide(int divisor)
        {
            return Value / divisor;
        }

        public Double Divide(double divisor)
        {
            return Value / divisor;
        }

        public Double Divide(decimal divisor)
        {
            return Value / (double)divisor;
        }
#if false
        public Double DivRem(Double divisor, out Double remainder)
        {
            throw new NotImplementedException();
            //return System.Math.DivRem(Value, divisor, out remainder);
        }
#endif
        public Double Floor()
        {
            return System.Math.Floor(Value);
        }

        public Double From(int value)
        {
            return value;
        }

        public Double From(double value)
        {
            return value;
        }

        public Double From(decimal value)
        {
            return (double)value;
        }

        public double Log(double baseValue)
        {
            return System.Math.Log(Value, baseValue);
        }

        public double Log()
        {
            return System.Math.Log(Value);
        }

        public double Log10()
        {
            return System.Math.Log10(Value);
        }

        public Double Max(Double other)
        {
            return System.Math.Max(Value, other);
        }

        public Double Min(Double other)
        {
            return System.Math.Min(Value, other);
        }

        public Double Multiply(Double multiplier)
        {
            return Value * multiplier;
        }

        public Double Multiply(int multiplier)
        {
            return Value * multiplier;
        }

        public Double Multiply(double multiplier)
        {
            return Value * multiplier;
        }

        public Double Multiply(decimal multiplier)
        {
            return Value * (double)multiplier;
        }

        public Double Negate()
        {
            return -Value;
        }

        public Double Pow(int exponent)
        {
            return System.Math.Pow(Value, exponent);
        }

        public Double Exp()
        {
            return System.Math.Exp(Value);
        }

        public Double Remainder(Double divisor)
        {
            return Value % divisor;
        }

        public Double Sqrt()
        {
            return System.Math.Sqrt(Value);
        }

        public Double Subtract(Double substrahend)
        {
            return Value - substrahend;
        }

        public Double Subtract(int substrahend)
        {
            return Value - substrahend;
        }

        public Double Subtract(double substrahend)
        {
            return Value - substrahend;
        }

        public Double Subtract(decimal substrahend)
        {
            return Value - (double)substrahend;
        }

        public Double Truncate()
        {
            return System.Math.Truncate(Value);
        }
        #region 三角関数
        /// <summary>
        /// 指定された角度のサインを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Double Sin()
        {
            return System.Math.Sin(Value);
        }
        /// <summary>
        /// 指定された角度のコサインを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Double Cos()
        {
            return System.Math.Cos(Value);
        }
        /// <summary>
        /// 指定された角度のタンジェントを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Double Tan()
        {
            return System.Math.Tan(Value);
        }
        /// <summary>
        /// コサインが指定数となる角度を返します。
        /// this:コサインを表す数で、d が -1 以上 1 以下である必要があります。
        /// </summary>
        /// <returns>0 ≤θ≤π の、ラジアンで表した角度 θ。 または d < -1 または d > 1、あるいは d が System.Double.NaN と等しい場合は、System.Double.NaN。</returns>
        public Double Acos()
        {
            return System.Math.Acos(Value);
        }
        /// <summary>
        /// サインが指定数となる角度を返します。
        /// this:サインを表す数で、d が -1 以上 1 以下である必要があります。
        /// </summary>
        /// <returns>-π/2 ≤θ≤π/2 の、ラジアンで表した角度 θ。 または d < -1 または d > 1、あるいは d が System.Double.NaN と等しい場合は、System.Double.NaN。</returns>
        public Double Asin()
        {
            return System.Math.Asin(Value);
        }
        /// <summary>
        /// タンジェントが指定数となる角度を返します。
        /// this:タンジェントを表す数。
        /// </summary>
        /// <returns>-π/2 ≤θ≤π/2 の、ラジアンで表した角度 θ。 または d が System.Double.NaN に等しい場合は System.Double.NaN。d
        ///     が System.Double.NegativeInfinity に等しい場合は、倍精度に丸めた -π/2 (-1.5707963267949)。d が
        ///     System.Double.PositiveInfinity に等しい場合は、倍精度に丸めた π/2 (1.5707963267949)。</returns>
        public Double Atan()
        {
            return System.Math.Atan(Value);
        }
        /// <summary>
        /// タンジェントが 2 つの指定された数の商である角度を返します。
        /// </summary>
        /// <param name="y">点の y 座標。</param>
        /// <param name="x">点の x 座標。</param>
        /// <returns>-π≤θ≤π および tan(θ) = y / x の、ラジアンで示した角度 θ。(x, y) は、デカルト座標の点を示します。 次の点に注意してください。
        ///     クワドラント 1 の (x, y) の場合は、0 < θ < π/2。 クワドラント 2 の (x, y) の場合は、π/2 < θ≤π。 クワドラント
        ///     3 の (x, y) の場合は、-π < θ < -π/2。 クワドラント 4 の (x, y) の場合は、-π/2 < θ < 0。 クワドラント間の境界上にある点の場合は、次の戻り値になります。
        ///     y が 0 で x が負数でない場合は、θ = 0。 y が 0 で x が負の場合は、θ = π。 y が正で x が 0 の場合は、θ = π/2。
        ///     y が負数で x が 0 の場合は、θ = -π/2。 y が 0 かつ x が 0 の場合は、θ = 0。 x または y が System.Double.NaN
        ///     であるか、x または y が System.Double.PositiveInfinity または System.Double.NegativeInfinity
        ///     のいずれである場合、メソッドは System.Double.NaN を返します。</returns>
        public Double Atan2(Double y, Double x)
        {
            return System.Math.Atan2(y.Value, x.Value);
        }
        #endregion 三角関数
        #region 双曲線関数
        /// <summary>
        /// 指定された角度のハイパーボリック サインを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Double Sinh()
        {
            return System.Math.Sinh(Value);
        }
        /// <summary>
        /// 指定された角度のハイパーボリック コサインを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Double Cosh()
        {
            return System.Math.Cosh(Value);
        }
        /// <summary>
        /// 指定された角度のハイパーボリック タンジェントを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Double Tanh()
        {
            return System.Math.Tanh(Value);
        }
        #endregion 双曲線関数
        #endregion IMath
        #region object
        /// <summary>
        /// 現在のインスタンスの値と指定されたオブジェクトの値が等しいかどうかを示す値を返します。
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is Double)
            {
                return Equals((Double)obj);
            }
            if (obj is double)
            {
                return Equals((Double)obj);
            }
            return false;
        }
        /// <summary>
        /// このインスタンスのハッシュ コードを返します。
        /// </summary>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        /// <summary>
        /// このインスタンスの数値を、それと等価な文字列形式に変換します。
        /// </summary>
        public override string ToString()
        {
            return Value.ToString();
        }
        #endregion object

        #region IEquatable
        /// <summary>
        /// このインスタンスが指定した値と等しいかどうかを示す値を返します。
        /// </summary>
        public bool Equals(Double other)
        {
            return Equals(this, other);
        }
        #endregion IEquatable
    }
}