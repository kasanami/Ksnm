using Ksnm.Numerics;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Ksnm.Graphics.Colors
{
    /// <summary>
    /// 色
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Rgb888 :
        IEquatable<Rgb888>,
        IAdditionOperators<Rgb888, Rgb888, Rgb888>,
        ISubtractionOperators<Rgb888, Rgb888, Rgb888>
    {
        #region 定数,static
        public static readonly Rgb888 Red = new(1.0, 0.0, 0.0);
        public static readonly Rgb888 Green = new(0.0, 1.0, 0.0);
        public static readonly Rgb888 Blue = new(0.0, 0.0, 1.0);
        #endregion 定数,static

        #region フィールド,プロパティ
        [FieldOffset(0)]
        public ScaledNumber8 R;
        [FieldOffset(1)]
        public ScaledNumber8 G;
        [FieldOffset(2)]
        public ScaledNumber8 B;
        #endregion フィールド,プロパティ

        #region コンストラクタ
        public Rgb888() : this(ScaledNumber8.Zero, ScaledNumber8.Zero, ScaledNumber8.Zero)
        {
        }
        public Rgb888(ScaledNumber8 r, ScaledNumber8 g, ScaledNumber8 b)
        {
            R = r;
            G = g;
            B = b;
        }
        public Rgb888(double r, double g, double b) :
            this((ScaledNumber8)Math.Clamp(r, 0, 1), (ScaledNumber8)Math.Clamp(g, 0, 1), (ScaledNumber8)Math.Clamp(b, 0, 1))
        {
        }
        #endregion コンストラクタ

        #region IEquatable
        public override bool Equals(object? obj)
        {
            return obj is Rgb888 rgb && Equals(rgb);
        }

        public bool Equals(Rgb888 other)
        {
            return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B);
        }
        #endregion IEquatable

        public RgbFFF ToRgbFFF()
        {
            return new((float)R, (float)G, (float)B);
        }

        #region operator
        public static bool operator ==(Rgb888 left, Rgb888 right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Rgb888 left, Rgb888 right)
        {
            return !(left == right);
        }
        public static Rgb888 operator +(Rgb888 left, Rgb888 right)
        {
            return new(
                left.R + right.R,
                left.G + right.G,
                left.B + right.B);
        }
        public static Rgb888 operator -(Rgb888 left, Rgb888 right)
        {
            return new(
                left.R - right.R,
                left.G - right.G,
                left.B - right.B);
        }
        #endregion operator
    }
}