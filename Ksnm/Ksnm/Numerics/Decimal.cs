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
        public decimal Value;
        #endregion プロパティ
        #region コンストラクタ
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

        public Decimal DivRem(Decimal divisor, out Decimal remainder)
        {
            throw new NotImplementedException();
            //return System.Math.DivRem(Value, divisor, out remainder);
        }

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
            if (Value < 0)
            {
                return -Value;
            }
            return Value;
        }

        public Decimal Pow(int exponent)
        {
            return 0;// Pow(Value, exponent);
        }

        public Decimal Remainder(Decimal divisor)
        {
            return Value % divisor;
        }

        public Decimal Sqrt()
        {
            return 0;// System.Math.Sqrt(Value);
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