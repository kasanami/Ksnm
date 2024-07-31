using Ksnm.ExtensionMethods.System.Complex;
using Ksnm.ExtensionMethods.System.Decimal;
using Ksnm.ExtensionMethods.System.Double;
using Ksnm.ExtensionMethods.System.Single;
using System;
using bigint = System.Numerics.BigInteger;
using complex = System.Numerics.Complex;

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
        /// <summary>
        /// BigInteger で初期化
        /// </summary>
        public Numeric(bigint value)
        {
            Value = value;
        }
        /// <summary>
        /// 複素数で初期化
        /// </summary>
        public Numeric(complex value)
        {
            Value = value;
        }
        #endregion コンストラクタ

        #region メソッド
        /// <summary>
        /// 最適化
        /// 内部の値型を最適な型へ変換します。
        /// </summary>
        public void Optimize()
        {
            if (IsInteger())
            {
                if (Value >= int.MinValue && Value <= int.MaxValue)
                {
                    Value = (int)Value;
                }
                else if (Value >= uint.MinValue && Value <= uint.MaxValue)
                {
                    Value = (uint)Value;
                }
                else if (Value >= long.MinValue && Value <= long.MaxValue)
                {
                    Value = (long)Value;
                }
                else if (Value >= ulong.MinValue && Value <= ulong.MaxValue)
                {
                    Value = (ulong)Value;
                }
                else
                {
                    // そのまま
                }
            }
        }
        /// <summary>
        /// 整数なら true を返します。
        /// </summary>
        /// <returns>true:整数　false:非整数</returns>
        public bool IsInteger()
        {
            if (Value is sbyte || Value is byte ||
                Value is short || Value is ushort ||
                Value is int || Value is uint ||
                Value is long || Value is ulong ||
                Value is bigint)
            {
                return true;
            }
            else if (Value is float)
            {
                return ((float)Value).IsInteger();
            }
            else if (Value is double)
            {
                return ((double)Value).IsInteger();
            }
            else if (Value is decimal)
            {
                return ((decimal)Value).IsInteger();
            }
            else if (Value is complex)
            {
                return ((complex)Value).IsInteger();
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
        /// uint からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(uint value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// long からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(long value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// ulong からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(ulong value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// float からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(float value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// double からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(double value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// decimal からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(decimal value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// BigInteger からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(bigint value)
        {
            return new Numeric(value);
        }
        /// <summary>
        /// Complex からの暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Numeric(complex value)
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
            else if (value.Value is bigint)
            {
                return (T)value.Value;
            }
            else if (value.Value is complex)
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
        /// <summary>
        /// BigInteger への明示的な変換を定義します。
        /// </summary>
        public static explicit operator bigint(in Numeric value)
        {
            return Cast<bigint>(value);
        }
        /// <summary>
        /// Complex への明示的な変換を定義します。
        /// </summary>
        public static explicit operator complex(in Numeric value)
        {
            return Cast<complex>(value);
        }
        #endregion Numeric→他の型
        #endregion 型変換

    }
}