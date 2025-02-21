using Ksnm.Units;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using static System.Formats.Asn1.AsnWriter;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 分数型
    /// </summary>
    public struct Fraction<T> :
        IFraction<Fraction<T>, T, T>,
        INumber<Fraction<T>>
        where T : INumber<T>, IMinMaxValue<T>
    {
        #region 定数・静的変数
        public static Fraction<T> One => new Fraction<T>(T.One);

        public static int Radix => T.Radix;

        public static Fraction<T> Zero => new Fraction<T>(T.Zero);

        public static Fraction<T> AdditiveIdentity => new Fraction<T>(T.AdditiveIdentity);

        public static Fraction<T> MultiplicativeIdentity => new Fraction<T>(T.MultiplicativeIdentity);

        public static Fraction<T> Epsilon => new Fraction<T>(T.One, T.MaxValue);

        public static Fraction<T> NaN => new Fraction<T>(T.Zero, T.Zero);

        public static Fraction<T> NegativeInfinity => new Fraction<T>(-T.One, T.Zero);

        public static Fraction<T> NegativeZero => new Fraction<T>(-T.Zero);

        public static Fraction<T> PositiveInfinity => new Fraction<T>(T.One, T.Zero);

        public static Fraction<T> NegativeOne => new Fraction<T>(-T.One);

        public static Fraction<T> E => throw new NotImplementedException();

        public static Fraction<T> Pi => throw new NotImplementedException();

        public static Fraction<T> Tau => throw new NotImplementedException();
        /// <summary>
        /// 扱える最大の指数
        /// </summary>
        static int _MaxExponent2 = int.CreateSaturating(System.Math.Log2(double.CreateSaturating(T.MaxValue)));
        static int _MinExponent2 = -_MaxExponent2;
        static int _MaxExponent10 = int.CreateSaturating(System.Math.Log10(double.CreateSaturating(T.MaxValue)));
        static int _MinExponent10 = -_MaxExponent10;
        #endregion 定数・静的変数

        #region プロパティ
        /// <summary>
        /// 分子
        /// </summary>
        public T Numerator { get; set; }
        /// <summary>
        /// 分母
        /// </summary>
        public T Denominator { get; set; }
        /// <summary>
        /// 逆数を返す。
        /// </summary>
        public Fraction<T> Reciprocal => new Fraction<T>(Denominator, Numerator);
        #endregion プロパティ

        #region コンストラクタ
        public Fraction(T numerator, T denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        public Fraction(T numerator)
        {
            Numerator = numerator;
            Denominator = T.One;
        }
        public Fraction(ExtendedDouble value)
        {
            Numerator = T.Zero;// 分子
            Denominator = T.One;// 分母
            if (value == 0)
            {
                return;
            }
            int shift = Binary.CountTrainingZero(value.Mantissa);
            var mantissa = value.Mantissa >> shift;
            var exponent = value.Exponent + shift;
            ulong denominator = 1;
            const ulong Radix = 2;
            int MaxExponent = _MaxExponent2;
            int MinExponent = _MinExponent2;
            // 指数が範囲外ならクリップする
            if (exponent > MaxExponent)
            {
                // 分子
                if (value.SignBit == 1)
                {
                    Numerator = -T.One;
                }
                else
                {
                    Numerator = T.One;
                }
                // 分母
                Denominator = T.Zero;
                return;
            }
            else if (exponent < MinExponent)
            {
                Numerator = T.Zero;// 分子
                Denominator = T.One;// 分母
                return;
            }
            // 指数が正なら分子を大きくする
            if (exponent > 0)
            {
                var scale = Math.Pow(Radix, exponent);
                mantissa *= scale;
            }
            else if (exponent < 0)
            {
                var scale = Math.Pow(Radix, -exponent);
                denominator = scale;
            }
            // 分子が範囲外なら無限大にする
            try
            {
                Numerator = T.CreateChecked(mantissa);
            }
            catch (OverflowException)
            {
                if (value.SignBit == 1)
                {
                    Numerator = -T.One;
                }
                else
                {
                    Numerator = T.One;
                }
                Denominator = T.Zero;
                return;
            }
            // 分母が範囲外なら0にする
            try
            {
                Denominator = T.CreateChecked(denominator);
            }
            catch (OverflowException)
            {
                Numerator = T.Zero;
                Denominator = T.One;
                return;
            }
            // 符号
            if (value.SignBit == 1)
            {
                Numerator = -Numerator;
            }
        }
        public Fraction(ExtendedDecimal value)
        {
            Numerator = T.Zero;// 分子
            Denominator = T.One;// 分母
            if (value == 0)
            {
                return;
            }
            var mantissa = value.Mantissa;
            var exponent = value.Exponent;
            Int128 denominator = 1;
            const ulong Radix = 10;
            int MaxExponent = _MaxExponent10;
            int MinExponent = _MinExponent10;
            // 指数が範囲外ならクリップする
            if (exponent > MaxExponent)
            {
                // 分子
                if (value.SignBit == 1)
                {
                    Numerator = -T.One;
                }
                else
                {
                    Numerator = T.One;
                }
                // 分母
                Denominator = T.Zero;
                return;
            }
            else if (exponent < MinExponent)
            {
                Numerator = T.Zero;// 分子
                Denominator = T.One;// 分母
                return;
            }
            // 指数が正なら分子を大きくする
            if (exponent > 0)
            {
                var scale = Math.Pow(Radix, exponent);
                mantissa *= scale;
            }
            else if (exponent < 0)
            {
                var scale = Math.Pow(Radix, -exponent);
                denominator = scale;
            }
            // 分子が範囲外なら無限大にする
            try
            {
                Numerator = T.CreateChecked(mantissa);
            }
            catch (OverflowException)
            {
                if (value.SignBit == 1)
                {
                    Numerator = -T.One;
                }
                else
                {
                    Numerator = T.One;
                }
                Denominator = T.Zero;
                return;
            }
            // 分母が範囲外なら0にする
            try
            {
                Denominator = T.CreateChecked(denominator);
            }
            catch (OverflowException)
            {
                Numerator = T.Zero;
                Denominator = T.One;
                return;
            }
            // 符号
            if (value.SignBit == 1)
            {
                Numerator = -Numerator;
            }
        }
        #endregion コンストラクタ

        #region 型変換
        public static Fraction<T> ConvertFrom<TOther>(TOther value) where TOther : INumber<TOther>
        {
            if (TOther.IsNaN(value))
            {
                return NaN;
            }
            else if (TOther.IsPositiveInfinity(value))
            {
                return PositiveInfinity;
            }
            else if (TOther.IsNegativeInfinity(value))
            {
                return NegativeInfinity;
            }
            else if (typeof(TOther) == typeof(Half))
            {
                var fValue = (Half)(object)value;
                return new Fraction<T>((double)fValue);
            }
            else if (typeof(TOther) == typeof(float))
            {
                var fValue = (float)(object)value;
                return new Fraction<T>(fValue);
            }
            else if (typeof(TOther) == typeof(double))
            {
                var fValue = (double)(object)value;
                return new Fraction<T>(fValue);
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                var fValue = (decimal)(object)value;
                return new Fraction<T>(fValue);
            }
            else if (TOther.IsInteger(value))
            {
                return new Fraction<T>(T.CreateChecked(value));
            }
            throw new InvalidCastException($"{nameof(value)}={value}");
        }
        public static explicit operator Fraction<T>(byte value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(sbyte value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(short value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(ushort value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(int value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(uint value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(long value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(ulong value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(Int128 value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(UInt128 value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(Half value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(float value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(double value) => ConvertFrom(value);
        public static explicit operator Fraction<T>(decimal value) => ConvertFrom(value);
        //public static implicit operator Fraction<T>(T value)
        //{
        //    return new Fraction<T>(value);
        //}

        public static TOther ConvertTo<TOther>(Fraction<T> value) where TOther : INumber<TOther>
        {
            TOther numeratorOther = TOther.CreateSaturating(value.Numerator);
            TOther denominatorOther = TOther.CreateSaturating(value.Denominator);
            return numeratorOther / denominatorOther;
        }
        public static explicit operator byte(Fraction<T> value) => ConvertTo<byte>(value);
        public static explicit operator sbyte(Fraction<T> value) => ConvertTo<sbyte>(value);
        public static explicit operator short(Fraction<T> value) => ConvertTo<short>(value);
        public static explicit operator ushort(Fraction<T> value) => ConvertTo<ushort>(value);
        public static explicit operator int(Fraction<T> value) => ConvertTo<int>(value);
        public static explicit operator uint(Fraction<T> value) => ConvertTo<uint>(value);
        public static explicit operator long(Fraction<T> value) => ConvertTo<long>(value);
        public static explicit operator ulong(Fraction<T> value) => ConvertTo<ulong>(value);
        public static explicit operator Int128(Fraction<T> value) => ConvertTo<Int128>(value);
        public static explicit operator UInt128(Fraction<T> value) => ConvertTo<UInt128>(value);
        public static explicit operator Half(Fraction<T> value) => ConvertTo<Half>(value);
        public static explicit operator float(Fraction<T> value) => ConvertTo<float>(value);
        public static explicit operator double(Fraction<T> value) => ConvertTo<double>(value);
        public static explicit operator decimal(Fraction<T> value) => ConvertTo<decimal>(value);
        #endregion 型変換

        /// <summary>
        /// 分数の簡約化
        /// </summary>
        public void Simplify()
        {
            // 約分する
            Reduce();
            // 分母を正の数にする
            if (Denominator < T.Zero)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }
        }

        #region Reduce 約分
        /// <summary>
        /// 約分する。
        /// <para>可約でない場合は何もしません。</para>
        /// </summary>
        public void Reduce()
        {
            var gcd = Math.GreatestCommonDivisor(Numerator, Denominator);
            if (gcd > T.One)
            {
                Numerator /= gcd;
                Denominator /= gcd;
            }
        }
        /// <summary>
        /// 可約ならtrueを返す。
        /// <para>判定後にReduce()を呼び出すより、Reduce()単体で使用したほうが効率的です。</para>
        /// </summary>
        public bool IsReducible()
        {
            var gcd = Math.GreatestCommonDivisor(Numerator, Denominator);
            return gcd > T.One;
        }
        #endregion Reduce 約分

        public static Fraction<T> Abs(Fraction<T> value)
        {
            return new Fraction<T>(T.Abs(value.Numerator), T.Abs(value.Denominator));
        }

        public static bool IsCanonical(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsComplexNumber(Fraction<T> value) => false;

        public static bool IsEvenInteger(Fraction<T> value)
        {
            if (IsInteger(value))
            {
                return T.IsEvenInteger(value.Numerator);
            }
            return false;
        }
        /// <summary>
        /// 有限値ならtrueを返す。
        /// </summary>
        public static bool IsFinite(Fraction<T> value)
        {
            return value.Denominator != T.Zero;
        }

        public static bool IsImaginaryNumber(Fraction<T> value) => false;

        public static bool IsInfinity(Fraction<T> value)
        {
            return value.Numerator != T.Zero && value.Denominator == T.Zero;
        }

        public static bool IsInteger(Fraction<T> value)
        {
            return value.Denominator == T.One;
        }

        public static bool IsNaN(Fraction<T> value)
        {
            return value.Numerator == T.Zero && value.Denominator == T.Zero;
        }

        public static bool IsNegative(Fraction<T> value)
        {
            return T.IsNegative(value.Numerator);
        }

        public static bool IsNegativeInfinity(Fraction<T> value)
        {
            return value.Numerator < T.Zero && value.Denominator == T.Zero;
        }

        public static bool IsNormal(Fraction<T> value)
        {
            return true;
        }

        public static bool IsOddInteger(Fraction<T> value)
        {
            if (IsInteger(value))
            {
                return T.IsOddInteger(value.Numerator);
            }
            return false;
        }

        public static bool IsPositive(Fraction<T> value)
        {
            return T.IsPositive(value.Numerator);
        }

        public static bool IsPositiveInfinity(Fraction<T> value)
        {
            return value.Numerator > T.Zero && value.Denominator == T.Zero;
        }

        public static bool IsRealNumber(Fraction<T> value)
        {
            if (value.Denominator == T.Zero)
            {
                return false;
            }
            return true;
        }

        public static bool IsSubnormal(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsZero(Fraction<T> value)
        {
            if (value.Denominator == T.Zero)
            {
                return false;
            }
            return value.Numerator == T.Zero;
        }

        public static Fraction<T> MaxMagnitude(Fraction<T> x, Fraction<T> y)
        {
            T xNumerator = x.Numerator * y.Denominator;
            T yNumerator = y.Numerator * x.Denominator;
            if (xNumerator > yNumerator)
            {
                return x;
            }
            return y;
        }

        public static Fraction<T> MaxMagnitudeNumber(Fraction<T> x, Fraction<T> y)
        {
            return MaxMagnitude(x, y);
        }

        public static Fraction<T> MinMagnitude(Fraction<T> x, Fraction<T> y)
        {
            T xNumerator = x.Numerator * y.Denominator;
            T yNumerator = y.Numerator * x.Denominator;
            if (xNumerator < yNumerator)
            {
                return x;
            }
            return y;
        }

        public static Fraction<T> MinMagnitudeNumber(Fraction<T> x, Fraction<T> y)
        {
            return MinMagnitude(x, y);
        }

        public static Fraction<T> Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            var slashIndex = s.IndexOf('/');
            if (slashIndex == -1)
            {
                return new(T.Parse(s, style, provider));
            }
            else
            {
                return new(
                    T.Parse(s[..slashIndex], style, provider),
                    T.Parse(s[(slashIndex + 1)..], style, provider));
            }
        }

        public static Fraction<T> Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            return Parse(s.AsSpan(), style, provider);
        }

        public static Fraction<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            var slashIndex = s.IndexOf('/');
            if (slashIndex == -1)
            {
                return new(T.Parse(s, provider));
            }
            else
            {
                return new(
                    T.Parse(s[..slashIndex], provider),
                    T.Parse(s[(slashIndex + 1)..], provider));
            }
        }

        public static Fraction<T> Parse(string s, IFormatProvider? provider)
        {
            return Parse(s.AsSpan(), provider);
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction<T> result)
        {
            var slashIndex = s.IndexOf('/');

            if (slashIndex == -1)
            {
                if (T.TryParse(s, style, provider, out var numerator))
                {
                    result = new(numerator);
                    return true;
                }
            }
            else if (
                T.TryParse(s[..slashIndex], style, provider, out var numerator) &&
                T.TryParse(s[(slashIndex + 1)..], style, provider, out var denominator))
            {
                result = new(numerator, denominator);
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction<T> result)
        {
            return TryParse(s.AsSpan(), style, provider, out result);
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction<T> result)
        {
            var slashIndex = s.IndexOf('/');

            if (slashIndex == -1)
            {
                if (T.TryParse(s, provider, out var numerator))
                {
                    result = new(numerator);
                    return true;
                }
            }
            else if (
                T.TryParse(s[..slashIndex], provider, out var numerator) &&
                T.TryParse(s[(slashIndex + 1)..], provider, out var denominator))
            {
                result = new(numerator, denominator);
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction<T> result)
        {
            return TryParse(s.AsSpan(), provider, out result);
        }

        static bool TryConvertFromChecked<TOther>(TOther value, out Fraction<T> result)
            where TOther : INumber<TOther>
        {
            result = default;
            if (typeof(TOther) == typeof(Fraction<T>))
            {
                var actualValue = (Fraction<T>)(object)value;
                result.Numerator = actualValue.Numerator;
                result.Denominator = actualValue.Denominator;
                return true;
            }
            T numerator;
            if (T.TryConvertFromChecked<TOther>(value, out numerator) == false)
            {
                return false;
            }
            result.Numerator = numerator;
            result.Denominator = T.One;
            return true;
        }

        static bool TryConvertFromSaturating<TOther>(TOther value, out Fraction<T> result)
            where TOther : INumber<TOther>
        {
            result = default;
            if (typeof(TOther) == typeof(Fraction<T>))
            {
                var actualValue = (Fraction<T>)(object)value;
                result.Numerator = actualValue.Numerator;
                result.Denominator = actualValue.Denominator;
                return true;
            }
            T numerator;
            if (T.TryConvertFromSaturating<TOther>(value, out numerator) == false)
            {
                return false;
            }
            result.Numerator = numerator;
            result.Denominator = T.One;
            return true;
        }

        static bool TryConvertFromTruncating<TOther>(TOther value, out Fraction<T> result)
            where TOther : INumber<TOther>
        {
            result = default;
            if (typeof(TOther) == typeof(Fraction<T>))
            {
                var actualValue = (Fraction<T>)(object)value;
                result.Numerator = actualValue.Numerator;
                result.Denominator = actualValue.Denominator;
                return true;
            }
            T numerator;
            if (T.TryConvertFromTruncating<TOther>(value, out numerator) == false)
            {
                return false;
            }
            result.Numerator = numerator;
            result.Denominator = T.One;
            return true;
        }

        static bool TryConvertToChecked<TOther>(Fraction<T> value, out TOther result)
            where TOther : INumber<TOther>
        {
            result = default;
            TOther numeratorOther;
            TOther denominatorOther;
            if (T.TryConvertToChecked<TOther>(value.Numerator, out numeratorOther) == false)
            {
                return false;
            }
            if (T.TryConvertToChecked<TOther>(value.Denominator, out denominatorOther) == false)
            {
                return false;
            }
            result = numeratorOther / denominatorOther;
            return true;
        }

        static bool TryConvertToSaturating<TOther>(Fraction<T> value, out TOther result)
            where TOther : INumber<TOther>
        {
            result = default;
            TOther numeratorOther;
            TOther denominatorOther;
            if (T.TryConvertToSaturating<TOther>(value.Numerator, out numeratorOther) == false)
            {
                return false;
            }
            if (T.TryConvertToSaturating<TOther>(value.Denominator, out denominatorOther) == false)
            {
                return false;
            }
            result = numeratorOther / denominatorOther;
            return true;
        }

        static bool TryConvertToTruncating<TOther>(Fraction<T> value, out TOther result)
            where TOther : INumber<TOther>
        {
            result = default;
            TOther numeratorOther;
            TOther denominatorOther;
            if (T.TryConvertToTruncating<TOther>(value.Numerator, out numeratorOther) == false)
            {
                return false;
            }
            if (T.TryConvertToTruncating<TOther>(value.Denominator, out denominatorOther) == false)
            {
                return false;
            }
            result = numeratorOther / denominatorOther;
            return true;
        }

        #region IComparable
        /// <summary>
        /// 比較し、これらの相対値を示す値を返します。
        /// </summary>
        /// <param name="obj">比較するオブジェクト</param>
        /// <returns>0の場合等価です。0 より大きい値の場合 obj よりも大きいです。0 より小さい値の場合 obj よりも小さいです。</returns>
        public int CompareTo(object? obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (obj is Fraction<T>)
            {
                return CompareTo((Fraction<T>)obj);
            }
            return 1;
        }
        public int CompareTo(Fraction<T>? other)
        {
            if (other == null)
            {
                return 1;
            }
            if (other.HasValue)
            {
                return CompareTo(other.Value);
            }
            return 1;
        }
        public int CompareTo(Fraction<T> other)
        {
            return (Numerator * other.Denominator).CompareTo(other.Numerator * Denominator);
        }
        #endregion IComparable

        #region IEquatable
        public bool Equals(Fraction<T>? other)
        {
            if (other == null)
            {
                return false;
            }
            if (other.HasValue)
            {
                return Equals(other.Value);
            }
            return false;
        }
        public bool Equals(Fraction<T> other)
        {
            return CompareTo(other) == 0;
        }
        #endregion IEquatable

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            charsWritten = 0;
            int start = 0;
            int numeratorCharsWritten;
            int denominatorCharsWritten;
            // 分子
            if (Numerator.TryFormat(destination, out numeratorCharsWritten, format, provider) == false)
            {
                return false;
            }
            // /記号
            start = numeratorCharsWritten;
            if (start < destination.Length)
            {
                destination[start] = '/';
                start += 1;
            }
            else
            {
                return false;
            }
            // 分母
            if (start < destination.Length)
            {
                var destination2 = destination.Slice(start, destination.Length - start);
                if (Denominator.TryFormat(destination2,
                    out denominatorCharsWritten, format, provider) == false)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            charsWritten = numeratorCharsWritten + 1 + denominatorCharsWritten;
            return true;
        }
        #region オペレーター

        #region 単項演算子
        /// <summary>
        /// 符号維持
        /// </summary>
        public static Fraction<T> operator +(Fraction<T> value) => value;
        /// <summary>
        /// 符号反転
        /// <para>変更されるのは Numerator</para>
        /// </summary>
        public static Fraction<T> operator -(Fraction<T> value)
        {
            return new Fraction<T>((T)(-value.Numerator), value.Denominator);
        }
        public static Fraction<T> operator --(Fraction<T> value)
        {
            return value - One;
        }
        public static Fraction<T> operator ++(Fraction<T> value)
        {
            return value + One;
        }
        #endregion 単項演算子

        #region 二項演算子
        /// <summary>
        /// 加算
        /// </summary>
        public static Fraction<T> operator +(Fraction<T> left, Fraction<T> right)
        {
            var temp = new Fraction<T>();
            if (right.Denominator == left.Denominator)
            {
                temp.Numerator = T.CreateChecked(left.Numerator + right.Numerator);
                temp.Denominator = left.Denominator;
            }
            else
            {
                temp.Numerator = T.CreateChecked(left.Numerator * right.Denominator + right.Numerator * left.Denominator);
                temp.Denominator = T.CreateChecked(left.Denominator * right.Denominator);
            }
            temp.Reduce();
            return temp;
        }
        /// <summary>
        /// 減算
        /// </summary>
        public static Fraction<T> operator -(Fraction<T> left, Fraction<T> right)
        {
            var temp = new Fraction<T>();
            if (right.Denominator == left.Denominator)
            {
                temp.Numerator = T.CreateChecked(left.Numerator - right.Numerator);
                temp.Denominator = left.Denominator;
            }
            else
            {
                temp.Numerator = T.CreateChecked(left.Numerator * right.Denominator - right.Numerator * left.Denominator);
                temp.Denominator = T.CreateChecked(left.Denominator * right.Denominator);
            }
            temp.Reduce();
            return temp;
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Fraction<T> operator *(Fraction<T> left, Fraction<T> right)
        {
            var temp = new Fraction<T>();
            temp.Numerator = T.CreateChecked(left.Numerator * right.Numerator);
            temp.Denominator = T.CreateChecked(left.Denominator * right.Denominator);
            temp.Reduce();
            return temp;
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static Fraction<T> operator /(Fraction<T> left, Fraction<T> right)
        {
            var temp = new Fraction<T>();
            temp.Numerator = T.CreateChecked(left.Numerator * right.Denominator);
            temp.Denominator = T.CreateChecked(left.Denominator * right.Numerator);
            temp.Reduce();
            return temp;
        }
        /// <summary>
        /// 剰余
        /// </summary>
        public static Fraction<T> operator %(Fraction<T> left, Fraction<T> right)
        {
            // 分母の最小公倍数
            T lcm = Math.LeastCommonMultiple(left.Denominator, right.Denominator);

            // 両方の分数を分母がLCMの形に変換
            T aNumerator = left.Numerator * (lcm / left.Denominator);
            T bNumerator = right.Numerator * (lcm / right.Denominator);

            // 剰余を計算
            T modNumerator = aNumerator % bNumerator;

            return new Fraction<T>(modNumerator, lcm);
        }
        #endregion 2項演算子

        #region 比較演算子
        public static bool operator ==(Fraction<T>? left, Fraction<T>? right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return left.Value == right.Value;
        }
        public static bool operator ==(Fraction<T> left, Fraction<T> right)
        {
            left.Reduce();
            right.Reduce();
            return left.Numerator == right.Numerator && left.Denominator == right.Denominator;
        }
        public static bool operator !=(Fraction<T>? left, Fraction<T>? right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return left.Value != right.Value;
        }
        public static bool operator !=(Fraction<T> left, Fraction<T> right)
        {
            left.Reduce();
            right.Reduce();
            return left.Numerator != right.Numerator || left.Denominator != right.Denominator;
        }
        /// <summary>
        /// 大なり演算子
        /// </summary>
        public static bool operator >(Fraction<T> left, Fraction<T> right)
        {
            return left.Numerator * right.Denominator > right.Numerator * left.Denominator;
        }
        /// <summary>
        /// 小なり演算子
        /// </summary>
        public static bool operator <(Fraction<T> left, Fraction<T> right)
        {
            return left.Numerator * right.Denominator < right.Numerator * left.Denominator;
        }
        /// <summary>
        /// 以上演算子
        /// </summary>
        public static bool operator >=(Fraction<T> left, Fraction<T> right)
        {
            return left.Numerator * right.Denominator >= right.Numerator * left.Denominator;
        }
        /// <summary>
        /// 以下演算子
        /// </summary>
        public static bool operator <=(Fraction<T> left, Fraction<T> right)
        {
            return left.Numerator * right.Denominator <= right.Numerator * left.Denominator;
        }
        #endregion 比較演算子

        #endregion オペレーター


        #region object
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is Fraction<T>)
            {
                return Equals((Fraction<T>)obj);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Numerator.GetHashCode() ^ Denominator.GetHashCode();
        }
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
        #endregion object

        public static Fraction<T> Atan2(Fraction<T> y, Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Atan2Pi(Fraction<T> y, Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> BitDecrement(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> BitIncrement(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> FusedMultiplyAdd(Fraction<T> left, Fraction<T> right, Fraction<T> addend)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Ieee754Remainder(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public static int ILogB(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> ScaleB(Fraction<T> x, int n)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Exp(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Exp10(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Exp2(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public int GetExponentByteCount()
        {
            throw new NotImplementedException();
        }

        public int GetExponentShortestBitLength()
        {
            throw new NotImplementedException();
        }

        public int GetSignificandBitLength()
        {
            throw new NotImplementedException();
        }

        public int GetSignificandByteCount()
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Round(Fraction<T> x, int digits, MidpointRounding mode)
        {
            throw new NotImplementedException();
        }

        public bool TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public bool TryWriteExponentLittleEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public bool TryWriteSignificandBigEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public bool TryWriteSignificandLittleEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Acosh(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Asinh(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Atanh(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Cosh(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Sinh(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Tanh(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Log(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Log(Fraction<T> x, Fraction<T> newBase)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Log10(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Log2(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Pow(Fraction<T> x, Fraction<T> y)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Cbrt(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Hypot(Fraction<T> x, Fraction<T> y)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> RootN(Fraction<T> x, int n)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Sqrt(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Acos(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> AcosPi(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Asin(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> AsinPi(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Atan(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> AtanPi(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Cos(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> CosPi(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Sin(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static (Fraction<T> Sin, Fraction<T> Cos) SinCos(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static (Fraction<T> SinPi, Fraction<T> CosPi) SinCosPi(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> SinPi(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> Tan(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> TanPi(Fraction<T> x)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertFromChecked<TOther>(TOther value, out Fraction<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertFromSaturating<TOther>(TOther value, out Fraction<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertFromTruncating<TOther>(TOther value, out Fraction<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertToChecked<TOther>(Fraction<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertToSaturating<TOther>(Fraction<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertToTruncating<TOther>(Fraction<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        #region オペレーター

        static bool IComparisonOperators<Fraction<T>, Fraction<T>, bool>.operator >(Fraction<T> left, Fraction<T> right)
            => left > right;

        static bool IComparisonOperators<Fraction<T>, Fraction<T>, bool>.operator >=(Fraction<T> left, Fraction<T> right)
            => left >= right;

        static bool IComparisonOperators<Fraction<T>, Fraction<T>, bool>.operator <(Fraction<T> left, Fraction<T> right)
            => left < right;

        static bool IComparisonOperators<Fraction<T>, Fraction<T>, bool>.operator <=(Fraction<T> left, Fraction<T> right)
            => left <= right;

        static Fraction<T> IModulusOperators<Fraction<T>, Fraction<T>, Fraction<T>>.operator %(Fraction<T> left, Fraction<T> right)
            => left % right;

        static Fraction<T> IAdditionOperators<Fraction<T>, Fraction<T>, Fraction<T>>.operator +(Fraction<T> left, Fraction<T> right)
            => left + right;

        static Fraction<T> IDecrementOperators<Fraction<T>>.operator --(Fraction<T> value)
            => value--;

        static Fraction<T> IDivisionOperators<Fraction<T>, Fraction<T>, Fraction<T>>.operator /(Fraction<T> left, Fraction<T> right)
            => left / right;

        static bool IEqualityOperators<Fraction<T>, Fraction<T>, bool>.operator ==(Fraction<T> left, Fraction<T> right)
            => left == right;

        static bool IEqualityOperators<Fraction<T>, Fraction<T>, bool>.operator !=(Fraction<T> left, Fraction<T> right)
            => left != right;

        static Fraction<T> IIncrementOperators<Fraction<T>>.operator ++(Fraction<T> value)
            => value++;

        static Fraction<T> IMultiplyOperators<Fraction<T>, Fraction<T>, Fraction<T>>.operator *(Fraction<T> left, Fraction<T> right)
            => left * right;

        static Fraction<T> ISubtractionOperators<Fraction<T>, Fraction<T>, Fraction<T>>.operator -(Fraction<T> left, Fraction<T> right)
            => left - right;

        static Fraction<T> IUnaryNegationOperators<Fraction<T>, Fraction<T>>.operator -(Fraction<T> value)
            => -value;

        static Fraction<T> IUnaryPlusOperators<Fraction<T>, Fraction<T>>.operator +(Fraction<T> value)
            => +value;
        #endregion オペレーター
    }
}
