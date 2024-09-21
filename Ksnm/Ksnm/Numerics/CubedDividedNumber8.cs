using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 三乗＆除算で実数を表現する数値型
    /// </summary>
    public struct CubedDividedNumber8
    {
        #region フィールド
        sbyte _value;
        #endregion フィールド

        #region プロパティ
        public sbyte Value { get => _value; set => _value = value; }
        /// <summary>
        /// 三乗した値
        /// </summary>
        public int CubedValue => (_value * _value * _value);
        /// <summary>
        /// 除数
        /// </summary>
        public static int Divisor = 16 * 16 * 16;

        public static CubedDividedNumber8 MinValue => new CubedDividedNumber8() { _value = sbyte.MinValue };
        public static CubedDividedNumber8 MaxValue => new CubedDividedNumber8() { _value = sbyte.MaxValue };
        #endregion プロパティ

        #region コンストラクタ
        public CubedDividedNumber8() { }
        public CubedDividedNumber8(double d)
        {
            d *= Divisor;
            d = double.Cbrt(d);
            Value = sbyte.CreateSaturating(d);
        }
        #endregion コンストラクタ


        #region 型変換
        public static CubedDividedNumber8 ConvertFrom<TOther>(TOther value) where TOther : INumber<TOther>
        {
            if (TOther.IsNaN(value) ||
                TOther.IsPositiveInfinity(value) ||
                TOther.IsNegativeInfinity(value))
            {
                throw new InvalidCastException($"{nameof(value)}={value}");
            }
            else if (typeof(TOther) == typeof(Half))
            {
                var fValue = (Half)(object)value;
                return new CubedDividedNumber8((double)fValue);
            }
            else if (typeof(TOther) == typeof(float))
            {
                var fValue = (float)(object)value;
                return new CubedDividedNumber8((double)fValue);
            }
            else if (typeof(TOther) == typeof(double))
            {
                var fValue = (double)(object)value;
                return new CubedDividedNumber8((double)fValue);
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                var fValue = (decimal)(object)value;
                return new CubedDividedNumber8((double)fValue);
            }
            else if (typeof(TOther) == typeof(int))
            {
                var iValue = (int)(object)value;
                return new CubedDividedNumber8(iValue);
            }
            else if (typeof(TOther) == typeof(uint))
            {
                var iValue = (uint)(object)value;
                return new CubedDividedNumber8(iValue);
            }
            else if (typeof(TOther) == typeof(sbyte))
            {
                var iValue = (sbyte)(object)value;
                return new CubedDividedNumber8(iValue);
            }
            else if (typeof(TOther) == typeof(byte))
            {
                var iValue = (byte)(object)value;
                return new CubedDividedNumber8(iValue);
            }
            else if (typeof(TOther) == typeof(short))
            {
                var iValue = (short)(object)value;
                return new CubedDividedNumber8(iValue);
            }
            else if (typeof(TOther) == typeof(ushort))
            {
                var iValue = (ushort)(object)value;
                return new CubedDividedNumber8(iValue);
            }
            else if (typeof(TOther) == typeof(long))
            {
                var iValue = (long)(object)value;
                return new CubedDividedNumber8(iValue);
            }
            else if (typeof(TOther) == typeof(ulong))
            {
                var iValue = (ulong)(object)value;
                return new CubedDividedNumber8(iValue);
            }
            throw new NotSupportedException($"Type={value.GetType()}");
        }
        public static explicit operator CubedDividedNumber8(byte value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(sbyte value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(short value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(ushort value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(int value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(uint value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(long value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(ulong value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(Int128 value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(UInt128 value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(Half value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(float value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(double value) => ConvertFrom(value);
        public static explicit operator CubedDividedNumber8(decimal value) => ConvertFrom(value);

        public static double ConvertToDouble(CubedDividedNumber8 value)
        {
            return value.CubedValue / (double)Divisor;
        }
        public static TOther ConvertTo<TOther>(CubedDividedNumber8 value) where TOther : INumber<TOther>
        {
            return TOther.CreateChecked(ConvertToDouble(value));
        }
        public static explicit operator byte(CubedDividedNumber8 value) => ConvertTo<byte>(value);
        public static explicit operator sbyte(CubedDividedNumber8 value) => ConvertTo<sbyte>(value);
        public static explicit operator short(CubedDividedNumber8 value) => ConvertTo<short>(value);
        public static explicit operator ushort(CubedDividedNumber8 value) => ConvertTo<ushort>(value);
        public static explicit operator int(CubedDividedNumber8 value) => ConvertTo<int>(value);
        public static explicit operator uint(CubedDividedNumber8 value) => ConvertTo<uint>(value);
        public static explicit operator long(CubedDividedNumber8 value) => ConvertTo<long>(value);
        public static explicit operator ulong(CubedDividedNumber8 value) => ConvertTo<ulong>(value);
        public static explicit operator Int128(CubedDividedNumber8 value) => ConvertTo<Int128>(value);
        public static explicit operator UInt128(CubedDividedNumber8 value) => ConvertTo<UInt128>(value);
        public static explicit operator Half(CubedDividedNumber8 value) => ConvertTo<Half>(value);
        public static explicit operator float(CubedDividedNumber8 value) => ConvertTo<float>(value);
        public static explicit operator double(CubedDividedNumber8 value) => ConvertTo<double>(value);
        public static explicit operator decimal(CubedDividedNumber8 value) => ConvertTo<decimal>(value);
        #endregion 型変換

        #region object
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
        public override string ToString()
        {
            var d = (double)this;
            return d.ToString();
        }
        #endregion object
    }
}