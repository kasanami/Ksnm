using System;

namespace Ksnm.Numerics
{
    /// <summary>
    /// System.Double のラッパー
    /// </summary>
    public struct Double : IMath<Double>, IEquatable<Double>
    {
        #region プロパティ
        public double Value;
        #endregion プロパティ
        #region コンストラクタ
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
            if (Value < 0)
            {
                return -Value;
            }
            return Value;
        }

        public Double Pow(int exponent)
        {
            return System.Math.Pow(Value, exponent);
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