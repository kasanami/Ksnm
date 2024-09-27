using Ksnm;
using Ksnm.Numerics;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

using Fraction32 = Ksnm.Numerics.Fraction<System.Int16>;
using Fraction64 = Ksnm.Numerics.Fraction<System.Int32>;

namespace ConsoleApp
{
    internal class NumericsTest
    {
        public static void Run()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

            ExtendedDoubleTest();
            ExtendedDecimalTest();
            CubedDividedNumber8Test();
            FractionTest();


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
        }

        public static void ExtendedDoubleTest()
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

            Console.WriteLine();
        }

        public static void ExtendedDecimalTest()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

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

            Console.WriteLine();
        }

        public static void CubedDividedNumber8Test()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

            CubedDividedNumber8 cdn = new CubedDividedNumber8();
            for (int i = 1; i <= 128; i += 8)
            {
                CubedDividedNumber8.Divisor = i * i * i;
                Console.WriteLine($"Divisor={CubedDividedNumber8.Divisor}:{CubedDividedNumber8.MinValue}～{CubedDividedNumber8.MaxValue}");
            }
            CubedDividedNumber8.Divisor = 0x8000;
            for (int i = sbyte.MinValue; i <= sbyte.MaxValue; i++)
            {
                cdn.Value = (sbyte)i;
                double d = (double)cdn;
                Console.WriteLine($"{cdn.Value}:{d}");
            }
            for (double d = -2; d <= 2; d += 0.01)
            {
                cdn = (CubedDividedNumber8)d;
                Console.WriteLine($"{cdn.Value}:{d}");
            }
            for (double d = (double)CubedDividedNumber8.MinValue; d <= (double)CubedDividedNumber8.MaxValue; d += 1)
            {
                cdn = (CubedDividedNumber8)d;
                Console.WriteLine($"{d}→{cdn.Value}→{(double)cdn}");
            }

            Console.WriteLine();
        }

        public static void FractionTest()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());


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

            Console.WriteLine();
        }
    }
}
