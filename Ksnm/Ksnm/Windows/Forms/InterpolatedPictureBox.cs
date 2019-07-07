using System;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Ksnm.Windows.Forms
{
    /// <summary>
    /// 補完モードを設定可能にしたPictureBox
    /// </summary>
    public class InterpolatedPictureBox : PictureBox
    {
        private InterpolationMode interpolation = InterpolationMode.Default;
        /// <summary>
        /// 補完モード
        /// </summary>
        public InterpolationMode Interpolation
        {
            get { return interpolation; }
            set
            {
                if (interpolation != value)
                {
                    interpolation = value;
                    // 補完モードが変更されたので再描画
                    Invalidate();
                }
            }
        }
        /// <summary>
        /// 補完モードを設定して描画
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.InterpolationMode = interpolation;
            pe.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            base.OnPaint(pe);
        }
    }
}
