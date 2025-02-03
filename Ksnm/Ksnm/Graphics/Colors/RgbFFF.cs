using Ksnm.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Graphics.Colors
{
    /// <summary>
    /// 色
    /// </summary>
    /// <param name="R">赤色(0～1)</param>
    /// <param name="G">緑色(0～1)</param>
    /// <param name="B">青色(0～1)</param>
    public record struct RgbFFF(float R, float G, float B)
    {
        #region 定数,static
        public static readonly RgbFFF Red = new(1.0, 0.0, 0.0);
        public static readonly RgbFFF Green = new(0.0, 1.0, 0.0);
        public static readonly RgbFFF Blue = new(0.0, 0.0, 1.0);
        #endregion 定数,static

        #region コンストラクタ
        public RgbFFF() : this(0, 0, 0)
        {
        }
        public RgbFFF(double r, double g, double b) :
            this((float)r, (float)g, (float)b)
        {
        }
        #endregion コンストラクタ
        public Rgb888 ToRgb888()
        {
            return new(
                (ScaledNumber8)Math.Clamp(R, 0, 1),
                (ScaledNumber8)Math.Clamp(G, 0, 1),
                (ScaledNumber8)Math.Clamp(B, 0, 1));
        }
    }
}