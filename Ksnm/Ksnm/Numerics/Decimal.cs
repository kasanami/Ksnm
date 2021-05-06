using System;
using Ksnm.ExtensionMethods.System.Decimal;

namespace Ksnm.Numerics
{
    /// <summary>
    /// System.Decimal のラッパー
    /// </summary>
    public struct Decimal : IMath<Decimal>, IEquatable<Decimal>
    {
        #region プロパティ
        /// <summary>
        /// 値
        /// </summary>
        public decimal Value;
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 指定した値で初期化する
        /// </summary>
        public Decimal(decimal value)
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
        public static bool Equals(Decimal d1, Decimal d2)
        {
            return d1.Value == d2.Value;
        }
        #region 型変換
        /// <summary>
        /// byte から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Decimal(decimal value)
        {
            return new Decimal(value);
        }
        /// <summary>
        /// byte から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator decimal(Decimal value)
        {
            return value.Value;
        }
        #endregion 型変換
        #region IMath
        public Decimal Zero => 0;

        public Decimal One => 1;

        public Decimal MinusOne => -1;

        public int Sign => Value.GetSign();

        public bool IsZero => Value == 0;

        public bool IsOne => Value == 1;

        public Decimal Abs()
        {
            return System.Math.Abs(Value);
        }

        public Decimal Add(Decimal addend)
        {
            return Value + addend;
        }

        public Decimal Add(int addend)
        {
            return Value + addend;
        }

        public Decimal Add(double addend)
        {
            return Value + (decimal)addend;
        }

        public Decimal Add(decimal addend)
        {
            return Value + addend;
        }

        public Decimal Ceiling()
        {
            return System.Math.Ceiling(Value);
        }

        public int Compare(Decimal other)
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

        public Decimal Divide(Decimal divisor)
        {
            return Value / divisor;
        }

        public Decimal Divide(int divisor)
        {
            return Value / divisor;
        }

        public Decimal Divide(double divisor)
        {
            return Value / (decimal)divisor;
        }

        public Decimal Divide(decimal divisor)
        {
            return Value / divisor;
        }
#if false
        public Decimal DivRem(Decimal divisor, out Decimal remainder)
        {
            throw new NotImplementedException();
            //return System.Math.DivRem(Value, divisor, out remainder);
        }
#endif
        public Decimal Floor()
        {
            return System.Math.Floor(Value);
        }

        public Decimal From(int value)
        {
            return value;
        }

        public Decimal From(double value)
        {
            return (decimal)value;
        }

        public Decimal From(decimal value)
        {
            return value;
        }
#if false
        public double Log(double baseValue)
        {
            return -1;// System.Math.Log(Value, baseValue);
        }

        public double Log()
        {
            return (double)Value.Log(100000);
        }

        public double Log10()
        {
            return -1;//System.Math.Log10(Value);
        }
#endif
        public Decimal Max(Decimal other)
        {
            return System.Math.Max(Value, other);
        }

        public Decimal Min(Decimal other)
        {
            return System.Math.Min(Value, other);
        }

        public Decimal Multiply(Decimal multiplier)
        {
            return Value * multiplier;
        }

        public Decimal Multiply(int multiplier)
        {
            return Value * multiplier;
        }

        public Decimal Multiply(double multiplier)
        {
            return Value * (decimal)multiplier;
        }

        public Decimal Multiply(decimal multiplier)
        {
            return Value * multiplier;
        }

        public Decimal Negate()
        {
            return -Value;
        }

        public Decimal Pow(int exponent)
        {
            return Math.Pow(Value, exponent);
        }

        public Decimal Exp()
        {
            return Math.Exp(Value);
        }

        public Decimal Remainder(Decimal divisor)
        {
            return Value % divisor;
        }

        public Decimal Sqrt()
        {
            return Math.Sqrt(Value);
        }

        public Decimal Subtract(Decimal substrahend)
        {
            return Value - substrahend;
        }

        public Decimal Subtract(int substrahend)
        {
            return Value - substrahend;
        }

        public Decimal Subtract(double substrahend)
        {
            return Value - (decimal)substrahend;
        }

        public Decimal Subtract(decimal substrahend)
        {
            return Value - substrahend;
        }

        public Decimal Truncate()
        {
            return System.Math.Truncate(Value);
        }
        #region 三角関数
        /// <summary>
        /// 指定された角度のサインを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Decimal Sin()
        {
            return Math.Sin(Value);
        }
        /// <summary>
        /// 指定された角度のコサインを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Decimal Cos()
        {
            return Math.Cos(Value);
        }
        /// <summary>
        /// 指定された角度のタンジェントを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Decimal Tan()
        {
            return Sin().Divide(Cos());
        }
        /// <summary>
        /// コサインが指定数となる角度を返します。
        /// this:コサインを表す数で、d が -1 以上 1 以下である必要があります。
        /// </summary>
        /// <returns>0 ≤θ≤π の、ラジアンで表した角度 θ。 または d < -1 または d > 1、あるいは d が System.Decimal.NaN と等しい場合は、System.Decimal.NaN。</returns>
        public Decimal Acos()
        {
            return Math.Acos(Value);
        }
        /// <summary>
        /// サインが指定数となる角度を返します。
        /// this:サインを表す数で、d が -1 以上 1 以下である必要があります。
        /// </summary>
        /// <returns>-π/2 ≤θ≤π/2 の、ラジアンで表した角度 θ。 または d < -1 または d > 1、あるいは d が System.Decimal.NaN と等しい場合は、System.Decimal.NaN。</returns>
        public Decimal Asin()
        {
            return Math.Asin(Value);
        }
        /// <summary>
        /// タンジェントが指定数となる角度を返します。
        /// this:タンジェントを表す数。
        /// </summary>
        /// <returns>-π/2 ≤θ≤π/2 の、ラジアンで表した角度 θ。 または d が System.Decimal.NaN に等しい場合は System.Decimal.NaN。d
        ///     が System.Decimal.NegativeInfinity に等しい場合は、倍精度に丸めた -π/2 (-1.5707963267949)。d が
        ///     System.Decimal.PositiveInfinity に等しい場合は、倍精度に丸めた π/2 (1.5707963267949)。</returns>
        public Decimal Atan()
        {
            return Math.Atan(Value);
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
        ///     y が負数で x が 0 の場合は、θ = -π/2。 y が 0 かつ x が 0 の場合は、θ = 0。 x または y が System.Decimal.NaN
        ///     であるか、x または y が System.Decimal.PositiveInfinity または System.Decimal.NegativeInfinity
        ///     のいずれである場合、メソッドは System.Decimal.NaN を返します。</returns>
        public Decimal Atan2(Decimal y, Decimal x)
        {
            return Math.Atan2(y.Value, x.Value);
        }
        #endregion 三角関数
        #region 双曲線関数
        /// <summary>
        /// 指定された角度のハイパーボリック サインを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Decimal Sinh()
        {
            //     (e^x)  -      ( e^-x         ) / 2
            return Exp().Subtract(Negate().Exp()).Divide(2);
        }
        /// <summary>
        /// 指定された角度のハイパーボリック コサインを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Decimal Cosh()
        {
            //     (e^x)  + ( e^-x         ) / 2
            return Exp().Add(Negate().Exp()).Divide(2);
        }
        /// <summary>
        /// 指定された角度のハイパーボリック タンジェントを返します。
        /// this:ラジアンで表した角度。
        /// </summary>
        public Decimal Tanh()
        {
            return Sinh().Divide(Cosh());
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
            if (obj is Decimal)
            {
                return Equals((Decimal)obj);
            }
            if (obj is decimal)
            {
                return Equals((Decimal)obj);
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
        public bool Equals(Decimal other)
        {
            return Equals(this, other);
        }
        #endregion IEquatable
    }
}