using Ksnm.Numerics;

namespace Ksnm.Graphics.Colors
{
    /// <summary>
    /// 色
    /// </summary>
    /// <param name="R">赤色</param>
    /// <param name="G">緑色</param>
    /// <param name="B">青色</param>
    public record struct Rgb888(ScaledNumber8 R, ScaledNumber8 G, ScaledNumber8 B)
    {
        #region 定数,static
        public static readonly Rgb888 Red = new(1.0, 0.0, 0.0);
        public static readonly Rgb888 Green = new(0.0, 1.0, 0.0);
        public static readonly Rgb888 Blue = new(0.0, 0.0, 1.0);
        #endregion 定数,static

        #region コンストラクタ
        public Rgb888() : this(ScaledNumber8.Zero, ScaledNumber8.Zero, ScaledNumber8.Zero)
        {
        }
        public Rgb888(double r, double g, double b) :
            this((ScaledNumber8)Math.Clamp(r, 0, 1), (ScaledNumber8)Math.Clamp(g, 0, 1), (ScaledNumber8)Math.Clamp(b, 0, 1))
        {
        }
        #endregion コンストラクタ

        public RgbFFF ToRgbFFF()
        {
            return new((float)R, (float)G, (float)B);
        }
    }
}