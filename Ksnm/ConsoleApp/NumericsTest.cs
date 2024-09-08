using Ksnm;
using Ksnm.Numerics;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp
{
    internal class NumericsTest
    {
        public static void Run()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

            Console.WriteLine($"ExtendedDouble");
            for (double i = -10; i <= 10; i += 0.5)
            {
                ExtendedDouble d = i;
                Console.WriteLine($"Value   :{d.Value}");
                Console.WriteLine($"Bits    :{d.Bits:X}");
                Console.WriteLine($"Sign    :{d.Sign}");
                Console.WriteLine($"Mantissa:{d.Mantissa}");
                Console.WriteLine($"Exponent:{d.Exponent}");
                Console.WriteLine($"Scale   :{d.Scale}");
                var value = d.Mantissa * d.Scale * d.Sign;
                Console.WriteLine($"{value}");
                Console.WriteLine();
            }

            Console.WriteLine($"ExtendedDecimal");
            for (decimal i = -10; i <= 10; i += 0.5m)
            {
                ExtendedDecimal d = i;
                Console.WriteLine($"Value   :{d.Value}");
                Console.WriteLine($"Bits    :{d.Bits:X}");
                Console.WriteLine($"Sign    :{d.Sign}");
                Console.WriteLine($"Mantissa:{d.Mantissa}");
                Console.WriteLine($"Exponent:{d.Exponent}");
                Console.WriteLine($"Scale   :{d.Scale}");
                var value = (double)d.Mantissa * d.Scale * d.Sign;
                Console.WriteLine($"{value}");
                Console.WriteLine();
            }

            Console.WriteLine($"double → Fraction");
            for (double i = -10; i <= 10; i++)
            {
                Fraction<long> fraction = new Fraction<long>(i);
                fraction.Reduce();
                Console.WriteLine($"{i} → {fraction}");
            }
            Console.WriteLine($"decimal → Fraction");
            for (decimal i = -10; i <= 10; i++)
            {
                Fraction<long> fraction = new Fraction<long>(i);
                fraction.Reduce();
                Console.WriteLine($"{i} → {fraction}");
            }
            // NaNはキャスト可能？→可能
            if (false)
            {
                Half f16 = Half.NaN;
                Console.WriteLine($"{f16} {BitConverter.HalfToUInt16Bits(f16):X4}");
                float f32 = float.NaN;
                Console.WriteLine($"{f32} {BitConverter.SingleToUInt32Bits(f32):X8}");
                double f64 = double.NaN;
                Console.WriteLine($"{f64} {BitConverter.DoubleToUInt64Bits(f64):X16}");

                f64 = (double)f16;
                Console.WriteLine($"{f64} {BitConverter.DoubleToUInt64Bits(f64):X16}");

                f64 = (double)f32;
                Console.WriteLine($"{f64} {BitConverter.DoubleToUInt64Bits(f64):X16}");
            }
            // 数学定数を計算
            if (false)
            {
                var exp = (double)Ksnm.Math.CalculateE<Fraction32>(Fraction32.Epsilon, 5);
                var pi = (double)Ksnm.Math.CalculatePi<Fraction32>();
            }

            {
                var bigFraction = new BigFraction(1, 2);// 1/2
                Console.WriteLine(bigFraction.ToString());
            }

            {
                var bigFraction = new BigFraction(1, 3);// 1/3
                Console.WriteLine(bigFraction.ToString());
            }
        }
    }
}
