using Ksnm.Numerics;
using Ksnm.Units;
using KsnmTests.Numerics;
using System.Numerics;

using Fraction32 = Ksnm.Numerics.Fraction<System.Int16>;

namespace ConsoleApp
{
    internal class NumericsTest
    {
        public static void Run()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

            {
                Console.WriteLine($"{nameof(Ksnm.Numerics.Vector<double>)}");

                Ksnm.Numerics.Vector<double> vector = new(3);

                vector[0] = 1;
                vector[1] = 1;
                vector[2] = 1;

                Console.WriteLine($"{nameof(vector)}={vector}");
                Console.WriteLine($"{nameof(vector.Magnitude)}={vector.Magnitude}");
                Console.WriteLine($"{nameof(vector.MagnitudePow2)}={vector.MagnitudePow2}");

                vector[1] = -1;
                vector[2] = -1;
                Console.WriteLine($"{nameof(vector)}={vector}");
                Console.WriteLine($"{nameof(vector.Magnitude)}={vector.Magnitude}");
                Console.WriteLine($"{nameof(vector.MagnitudePow2)}={vector.MagnitudePow2}");
            }

            {
                Console.WriteLine($"{nameof(Int128)}");
                Console.WriteLine($"{nameof(Int128.MaxValue)}={Int128.MaxValue}");
                Console.WriteLine($"{nameof(Int128.MinValue)}={Int128.MinValue}");
                Console.WriteLine($"{nameof(Int128.Zero)}={Int128.Zero}");
                Console.WriteLine($"{nameof(Int128.One)}={Int128.One}");
                Console.WriteLine($"{nameof(Int128.NegativeOne)}={Int128.NegativeOne}");
            }

            //ExtendedSingleTest();
            //ExtendedDoubleTest();
            //ExtendedDecimalTest();
            //CubedDividedNumber8Test();
            //FractionTest();
            //BrainFloatingPoint16Test();
            FloatingPointNumber8S1E4M3B7Test();

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

        public static void ExtendedSingleTest()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

            Console.WriteLine($"ExtendedSingle");
            for (float i = -10; i <= 10; i += 0.1f)
            {
                ExtendedSingle value = i;
                var reconstruction = value.Coefficient * value.Scale * value.Sign;
                Console.WriteLine($"Value      :{value.Value}");
                Console.WriteLine($"Bits       :0x{value.Bits:X}");
                Console.WriteLine($"UpperBits  :0x{value.UpperBits:X}");
                Console.WriteLine($"LowerBits  :0x{value.LowerBits:X}");
                Console.WriteLine($"Sign       :{value.Sign}");
                Console.WriteLine($"Exponent   :{value.Exponent}");
                Console.WriteLine($"Mantissa   :0x{value.Mantissa:X}");
                Console.WriteLine($"Scale      :{value.Scale}");
                Console.WriteLine($"Coefficient:{value.Coefficient}");
                Console.WriteLine($"復元       :{reconstruction}");
                Console.WriteLine();
            }

            {
                ExtendedSingle f = 1;
                f.Sign = +1;
                ConsoleWriteLine(f);
                f.Sign = -1;
                ConsoleWriteLine(f);

                f.Exponent = +1;
                ConsoleWriteLine(f);
                f.Exponent = -1;
                ConsoleWriteLine(f);
                f.Exponent = 0;
                ConsoleWriteLine(f);

                f.Mantissa = 1;
                ConsoleWriteLine(f);
            }

            {
                ExtendedSingle f;
                ExtendedDouble d;
                f = 0.125f;
                d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, UInt32>(f);
                ConsoleWriteLine(f);
                ConsoleWriteLine(d);
                f = 2;
                d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, UInt32>(f);
                ConsoleWriteLine(f);
                ConsoleWriteLine(d);
            }

            Console.WriteLine();
        }

        public static void ExtendedDoubleTest()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

            Console.WriteLine($"ExtendedDouble");
            for (double i = -10; i <= 10; i += 0.1)
            {
                ExtendedDouble value = i;
                var reconstruction = value.Coefficient * value.Scale * value.Sign;
                Console.WriteLine($"Value      :{value.Value}");
                Console.WriteLine($"Bits       :0x{value.Bits:X}");
                Console.WriteLine($"UpperBits  :0x{value.UpperBits:X}");
                Console.WriteLine($"LowerBits  :0x{value.LowerBits:X}");
                Console.WriteLine($"Sign       :{value.Sign}");
                Console.WriteLine($"Exponent   :{value.Exponent}");
                Console.WriteLine($"Mantissa   :0x{value.Mantissa:X}");
                Console.WriteLine($"Scale      :{value.Scale}");
                Console.WriteLine($"Coefficient:{value.Coefficient}");
                Console.WriteLine($"復元       :{reconstruction}");
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
                ConsoleWriteLine(d);
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

            {
                Console.WriteLine($"{nameof(Fraction<Int128>)}");
                Fraction<Int128> fraction = new();

                fraction = (Fraction<Int128>)10.0;
                Console.WriteLine($"10.0  = {fraction}");
                fraction = (Fraction<Int128>)12.3;
                Console.WriteLine($"12.3  = {fraction}");

                fraction = (Fraction<Int128>)10.0m;
                Console.WriteLine($"10.0m = {fraction}");
                fraction = (Fraction<Int128>)12.3m;
                Console.WriteLine($"12.3m = {fraction}");

                fraction = (Fraction<Int128>)Int128.Parse("123456789012345678901234567890123456789");
                Console.WriteLine($"123456789012345678901234567890123456789 = {fraction}");
                fraction /= (Fraction<Int128>)Int128.Parse("100000000000000000000000000000000000000");
                Console.WriteLine($"{fraction}={(double)fraction}");

                fraction = (Fraction<Int128>)Int128.Parse("123");
                fraction /= (Fraction<Int128>)Int128.Parse("100000000000000000000000000000000000000");
                Console.WriteLine($"{fraction}={(double)fraction}");
            }

            Console.WriteLine();
        }

        public static void BrainFloatingPoint16Test()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());
            Console.WriteLine($"{nameof(BrainFloatingPoint16)} Test");

            BrainFloatingPoint16 bfp = new();

            for (float i = -10; i <= 10; i += 0.5f)
            {
                bfp = (BrainFloatingPoint16)i;
                ConsoleWriteLine(bfp);
                Console.WriteLine();
            }

            for (double d = -10; d <= +10; d += 0.01)
            {
                bfp = (BrainFloatingPoint16)d;
                Console.WriteLine($"{d}→{bfp}→{(double)bfp}");
            }

            Console.WriteLine();
        }

        public static void FloatingPointNumber8S1E4M3B7Test()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());
            Console.WriteLine($"{nameof(FloatingPointNumber8S1E4M3B7)} Test");

            FloatingPointNumber8S1E4M3B7 fp8 = new();

            for (int i = 0; i < 0xFF; i++)
            {
                fp8.Bits = (byte)i;
                ConsoleWriteLine(fp8);
            }
            for (int i = 0; i < 0xFF; i++)
            {
                fp8.Bits = (byte)i;
                Console.WriteLine($"{i}\t{fp8}");
            }

            for (float i = -2; i <= 2; i += 0.5f)
            {
                fp8 = (FloatingPointNumber8S1E4M3B7)i;
                ConsoleWriteLine(fp8);
                Console.WriteLine();
            }

            for (double d = -2; d <= +2; d += 0.01)
            {
                fp8 = (FloatingPointNumber8S1E4M3B7)d;
                Console.WriteLine($"{d}→{fp8}→{(double)fp8}");
            }

            Console.WriteLine();
        }

        static void ConsoleWriteLine<TBits>(IFloatingPointProperties<TBits> value)
            where TBits : INumber<TBits>
        {
            var reconstruction = value.Coefficient * value.Scale * value.Sign;
            Console.WriteLine($"Value       :{value}");
            Console.WriteLine($"Bits        :0x{value.Bits:X}");
            Console.WriteLine($"SignBit     :{value.SignBit}");
            Console.WriteLine($"ExponentBits:0x{value.ExponentBits:X}");
            Console.WriteLine($"MantissaBits:0x{value.MantissaBits:X}");
            Console.WriteLine($"Sign        :{value.Sign}");
            Console.WriteLine($"Exponent    :{value.Exponent}");
            Console.WriteLine($"Mantissa    :0x{value.Mantissa:X}");
            Console.WriteLine($"Scale       :{value.Scale}");
            Console.WriteLine($"Coefficient :{value.Coefficient}");
            Console.WriteLine($"復元        :{reconstruction}");
            Console.WriteLine();
        }
    }
}