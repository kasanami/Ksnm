using Ksnm.ExtensionMethods.System.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SBigInteger = System.Numerics.BigInteger;
using SComplex = System.Numerics.Complex;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 数値全般を扱う型
    /// 実態はdynamic型
    /// </summary>
    public class Numeric
    {
        /// <summary>
        /// 
        /// </summary>
        public dynamic Value { get; private set; } = 0;

        #region コンストラクタ
        /// <summary>
        /// 符号あり整数型で初期化
        /// </summary>
        public Numeric(int value)
        {
            Value = value;
        }
        /// <summary>
        /// 符号なし整数型で初期化
        /// </summary>
        public Numeric(uint value)
        {
            Value = value;
        }
        /// <summary>
        /// 符号あり整数型で初期化
        /// </summary>
        public Numeric(long value)
        {
            Value = value;
        }
        /// <summary>
        /// 符号なし整数型で初期化
        /// </summary>
        public Numeric(ulong value)
        {
            Value = value;
        }
        /// <summary>
        /// 単精度浮動小数点数型で初期化
        /// </summary>
        public Numeric(float value)
        {
            Value = value;
        }
        /// <summary>
        /// 倍精度浮動小数点数型で初期化
        /// </summary>
        public Numeric(double value)
        {
            Value = value;
        }
        /// <summary>
        /// 10進数浮動小数点数型で初期化
        /// </summary>
        public Numeric(decimal value)
        {
            Value = value;
        }
        #endregion コンストラクタ

        #region メソッド
        /// <summary>
        /// 正規化
        /// 内部の値型を最適な型へ変換します。
        /// </summary>
        public void Normalize()
        {
            if(IsInteger())
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsInteger()
        {
            if (Value is sbyte || Value is byte ||
                Value is short || Value is ushort ||
                Value is int || Value is uint ||
                Value is long || Value is ulong ||
                Value is SBigInteger)
            {
                return true;
            }
            else if (Value is double)
            {
                return ((double)Value).IsInteger();
            }
            throw new Exception($"{nameof(Value)}が非対応の型です。");
        }
        #endregion メソッド

        #region 型変換
        #region 他の型→Numeric
        /// <summary>
        /// byte からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(byte value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// sbyte からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(sbyte value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// short からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(short value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// ushort からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(ushort value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// int からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(int value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// uint からの明示的な変換を定義します。
        /// </summary>
        public static explicit operator Numeric(uint value)
        {
            return new Numeric((int)value);
        }
        /// <summary>
        /// long からの明示的な変換を定義します。
        /// </summary>
        public static explicit operator Numeric(long value)
        {
            return new Numeric((int)value);
        }
        /// <summary>
        /// ulong からの明示的な変換を定義します。
        /// </summary>
        public static explicit operator Numeric(ulong value)
        {
            return new Numeric((int)value);
        }
        /// <summary>
        /// float からの明示的な変換を定義します。
        /// </summary>
        public static explicit operator Numeric(float value)
        {
            return (Numeric)(double)value;
        }
        /// <summary>
        /// double からの明示的な変換を定義します。
        /// </summary>
        public static explicit operator Numeric(double value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// decimal からの明示的な変換を定義します。
        /// </summary>
        public static explicit operator Numeric(decimal value)
        {
            return new Numeric(value);
        }
        #endregion 他の型→Numeric
        #region Numeric→他の型
        /// <summary>
        /// 
        /// </summary>
        public static T Cast<T>(in Numeric value)
        {
            if (value.Value is byte)
            {
                return (T)value.Value;
            }
            else if (value.Value is sbyte)
            {
                return (T)value.Value;
            }
            else if (value.Value is short)
            {
                return (T)value.Value;
            }
            else if (value.Value is ushort)
            {
                return (T)value.Value;
            }
            else if (value.Value is int)
            {
                return (T)value.Value;
            }
            else if (value.Value is uint)
            {
                return (T)value.Value;
            }
            else if (value.Value is long)
            {
                return (T)value.Value;
            }
            else if (value.Value is ulong)
            {
                return (T)value.Value;
            }
            else if (value.Value is float)
            {
                return (T)value.Value;
            }
            else if (value.Value is double)
            {
                return (T)value.Value;
            }
            else if (value.Value is decimal)
            {
                return (T)value.Value;
            }
            else if (value.Value is SBigInteger)
            {
                return (T)value.Value;
            }
            else if (value.Value is SComplex)
            {
                return (T)value.Value;
            }
            throw new Exception($"{nameof(Value)}が非対応の型です。");
        }
        /// <summary>
        /// byte への明示的な変換を定義します。
        /// </summary>
        public static explicit operator byte(in Numeric value)
        {
            return Cast<byte>(value);
        }
        /// <summary>
        /// sbyte への明示的な変換を定義します。
        /// </summary>
        public static explicit operator sbyte(in Numeric value)
        {
            return Cast<sbyte>(value);
        }
        /// <summary>
        /// short への明示的な変換を定義します。
        /// </summary>
        public static explicit operator short(in Numeric value)
        {
            return Cast<short>(value);
        }
        /// <summary>
        /// ushort への明示的な変換を定義します。
        /// </summary>
        public static explicit operator ushort(in Numeric value)
        {
            return Cast<ushort>(value);
        }
        /// <summary>
        /// int への明示的な変換を定義します。
        /// </summary>
        public static explicit operator int(in Numeric value)
        {
            return Cast<int>(value);
        }
        /// <summary>
        /// uint への明示的な変換を定義します。
        /// </summary>
        public static explicit operator uint(in Numeric value)
        {
            return Cast<uint>(value);
        }
        /// <summary>
        /// long への明示的な変換を定義します。
        /// </summary>
        public static explicit operator long(in Numeric value)
        {
            return Cast<long>(value);
        }
        /// <summary>
        /// ulong への明示的な変換を定義します。
        /// </summary>
        public static explicit operator ulong(in Numeric value)
        {
            return Cast<ulong>(value);
        }
        /// <summary>
        /// float への明示的な変換を定義します。
        /// </summary>
        public static explicit operator float(in Numeric value)
        {
            return Cast<float>(value);
        }
        /// <summary>
        /// double への明示的な変換を定義します。
        /// </summary>
        public static explicit operator double(in Numeric value)
        {
            return Cast<double>(value);
        }
        /// <summary>
        /// decimal への明示的な変換を定義します。
        /// </summary>
        public static explicit operator decimal(in Numeric value)
        {
            return Cast<decimal>(value);
        }
        #endregion Numeric→他の型
        #endregion 型変換

    }
}
