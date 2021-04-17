using System;

namespace Ksnm.Numerics
{
    /// <summary>
    /// System.Double のラッパー
    /// </summary>
    public struct Double : IMath<Double>
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

        public Double DivRem(Double divisor, out Double remainder)
        {
            throw new NotImplementedException();
            //return System.Math.DivRem(Value, divisor, out remainder);
        }

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

        public Double Truncate()
        {
            return System.Math.Truncate(Value);
        }
        #endregion IMath
        #region object
        public override string ToString()
        {
            return Value.ToString();
        }
        #endregion object
    }
}