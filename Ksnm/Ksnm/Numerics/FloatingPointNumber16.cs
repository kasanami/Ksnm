using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitsType = System.Int32;// 半精度浮動小数点数 全体のビットを表す型

namespace Ksnm.Numerics
{
    /// <summary>
    /// 半精度浮動小数点数型
    /// </summary>
    public class FloatingPointNumber16
    {
        public static double ToDouble(ushort half)
        {
            ulong doubleBits = 0;
            // 符号部
            if ((half & 0x8000) != 0)
            {
                doubleBits |= 0x8000_0000_0000_0000;
            }
            // 指数部
            {
                int exponent = (half & 0b01111100_00000000) >> 10;
                exponent -= 15;
                exponent += 1023;
                doubleBits |= (ulong)exponent << (52);
            }
            // 仮数部
            {
                ulong mantissa = (ulong)half & 0b00000011_11111111;
                doubleBits |= mantissa << (52 - 10);
            }
            return BitConverter.Int64BitsToDouble((long)doubleBits);
        }
    }
}
