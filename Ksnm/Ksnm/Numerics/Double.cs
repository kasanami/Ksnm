using System;

namespace Ksnm.Numerics
{
    /// <summary>
    /// System.Double のラッパー
    /// </summary>
    public struct Double : IMath<double>
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
        public double Zero => 0.0;

        public double One => 1.0;

        public double MinusOne => -1.0;

        public int Sign => Math.Sign(Value);

        public bool IsZero => Value == 0;

        public bool IsOne => Value == 1;

        public double Abs()
        {
            return System.Math.Abs(Value);
        }

        public double Add(double addend)
        {
            return Value + addend;
        }

        public double Ceiling()
        {
            return System.Math.Ceiling(Value);
        }

        public int Compare(double other)
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

        public double Divide(double divisor)
        {
            return Value / divisor;
        }

        public double DivRem(double divisor, out double remainder)
        {
            throw new NotImplementedException();
            //return System.Math.DivRem(Value, divisor, out remainder);
        }

        public double Floor()
        {
            return System.Math.Floor(Value);
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

        public double Max(double other)
        {
            return System.Math.Max(Value, other);
        }

        public double Min(double other)
        {
            return System.Math.Min(Value, other);
        }

        public double Multiply(double multiplier)
        {
            return Value * multiplier;
        }

        public double Negate()
        {
            if (Value < 0)
            {
                return -Value;
            }
            return Value;
        }

        public double Pow(int exponent)
        {
            return System.Math.Pow(Value, exponent);
        }

        public double Remainder(double divisor)
        {
            return Value % divisor;
        }

        public double Sqrt()
        {
            return System.Math.Sqrt(Value);
        }

        public double Subtract(double substrahend)
        {
            return Value - substrahend;
        }

        public double Truncate()
        {
            return System.Math.Truncate(Value);
        }
        #endregion IMath
    }
}