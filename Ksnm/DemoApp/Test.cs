using System;
using System.Diagnostics;
using Numeric = Ksnm.Numerics.Numeric;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using Ksnm.ExtensionMethods.System.Single;
using Ksnm.ExtensionMethods.System.Double;
using System.Numerics;
using Ksnm.Numerics;

namespace DemoApp
{
    public class Test
    {
        static Stopwatch stopwatch = new Stopwatch();

        public static void RunAll()
        {
            //SingleTest();
            //DoubleTest();
            //DecimalTest();
            //BigIntegerTest();
            //BigDecimalTest();
            //GreatestCommonDivisorWeightTest();
            //PowWeightTest();
            //PowTest();
            /*
            Numeric num = new Numeric(100m);
            num.Normalize();
            */
        }
        public static void SingleTest()
        {
            Console.WriteLine($"SingleTest");
            var stopwatch = new Stopwatch();
            for (int j = 0; j < 5; j++)
            {
                stopwatch.Restart();
                float sample = 1.1f;
                for (int i = 0; i < 10000000; i++)
                {
                    sample.IsInteger();
                }
                stopwatch.Stop();
                Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ミリ秒");
            }
        }
        public static void DoubleTest()
        {
            Console.WriteLine($"DoubleTest");
            var stopwatch = new Stopwatch();

            {
                for (int i = 0; i < 3; i++)
                {
                    var value = i * i;
                    Console.WriteLine($"{i} => { Math.Sqrt(value)}");
                }
            }

#if true
            {
                double baseValue = 0.00000001;
                for (int i = 0; i < 3; i++)
                {
                    var value = (float)(baseValue + i);
                    Console.WriteLine($"{baseValue + i} => {value}");
                }
            }
#endif

            Console.WriteLine($"IsPositive test");
            for (int j = 0; j < 5; j++)
            {
                stopwatch.Restart();
                double sample = 1.1;
                for (int i = 0; i < 100_000_000; i++)
                {
                    sample.IsPositive();
                }
                stopwatch.Stop();
                Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ミリ秒");
            }

            Console.WriteLine($"IsInteger test");
            for (int j = 0; j < 5; j++)
            {
                stopwatch.Restart();
                double sample = 1.1;
                for (int i = 0; i < 10000000; i++)
                {
                    sample.IsInteger();
                }
                stopwatch.Stop();
                Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ミリ秒");
            }
        }

        public static void DecimalTest()
        {
            Console.WriteLine("DecimalTest()");

            decimal pi = 3.14159_26535_89793_23846_26433_83279_50288m;
            pi = decimal.Parse("3.14159265358979323846264338327950288");


            {
                decimal baseValue = 0.001m;
                for (int i = 0; i < 3; i++)
                {
                    var value = (int)(baseValue + i);
                    Console.WriteLine($"{baseValue + i} => {value}");
                }
            }
#if true
            {
                decimal baseValue = 0.001m;
                for (int i = 0; i < 3; i++)
                {
                    var value = (int)(baseValue + i);
                    Console.WriteLine($"{baseValue + i} => {value}");
                }
            }
#endif
#if false
            {
                decimal baseValue = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    var value = (int)(baseValue + i);
                    Console.WriteLine($"{baseValue + i} => {value}");
                }
            }
#endif

#if false
            {
                var baseValue = BigInteger.Parse("79228162514264337593543950335");
                for (int i = 0; i < 3; i++)
                {
                    decimal d = (decimal)(baseValue + i);
                    Console.WriteLine($"{baseValue + i} => {d}");
                }
            }
#endif

            {
                decimal d1 = 0.0000000000000000000000000001m;
                decimal d2 = 2m;

                decimal d3 = d1 / d2;
                Console.WriteLine($"{d1} / {d2} = {d3}");
            }

            for (decimal d1 = 0; d1 <= 20; d1++)
            {
                for (decimal d2 = 1; d2 <= 20; d2++)
                {
                    decimal d3 = d1 / d2;
                    Console.WriteLine($"{d1} / {d2} = {d3}");
                }
            }

            Console.WriteLine();
        }

        public static void BigIntegerTest()
        {
            Console.WriteLine("BigIntegerTest()");

            Console.WriteLine($"BigInteger.Pow(10, e);");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"e={i}");
                stopwatch.Restart();
                for (int j = 1; j < 5_000_000; j++)
                {
                    BigInteger.Pow(10, i);
                }
                stopwatch.Stop();
                Console.WriteLine($"{stopwatch.Elapsed}");
            }
            Console.WriteLine();

            BigInteger sample = 0x0123_4567_89AB_CDEF;
            var bytes = sample.ToByteArray();
            Console.WriteLine(sample);
            Console.WriteLine(bytes.ToDebugString("X2", null, false));
        }

        public static void BigDecimalTest()
        {
            Console.WriteLine("BigDecimalTest()");

            Console.WriteLine($"BigDecimal.Sqrt();");
            for (int i = 0; i < 10; i++)
            {
                var value = BigDecimal.Sqrt(i,105);
                Console.WriteLine($"√{i}={value}");
            }
            Console.WriteLine();

            Console.WriteLine($"BigDecimal.Pow10(e);");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"e={i}");
                stopwatch.Restart();
                for (int j = 1; j < 5_000_000; j++)
                {
                    BigDecimal.Pow10(i);
                }
                stopwatch.Stop();
                Console.WriteLine($"{stopwatch.Elapsed}");
            }
            Console.WriteLine();

            for (decimal i = -10; i < 10; i++)
            {
                var sample = new BigDecimal(i);
                Console.WriteLine(i);
                Console.WriteLine(sample.ToDecimal());
            }

            {
                decimal sourceDecimal = 0x0123_4567_89AB_CDEF;
                BigDecimal sample = new BigDecimal(sourceDecimal);
                var sampleDecimal = sample.ToDecimal();
                Console.WriteLine(sourceDecimal);
                Console.WriteLine(sampleDecimal);
                Console.WriteLine(sourceDecimal == sampleDecimal);
            }
        }
        public static void GreatestCommonDivisorWeightTest()
        {
            const int count = 2000;

            Console.WriteLine($"Math.GreatestCommonDivisor(int)");
            stopwatch.Restart();
            for (int m = 1; m <= count; m++)
            {
                for (int n = 1; n <= count; n++)
                {
                    Ksnm.Math.GreatestCommonDivisor(m, n);
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");

            Console.WriteLine($"Math.GreatestCommonDivisor(uint)");
            stopwatch.Restart();
            for (uint m = 1; m <= count; m++)
            {
                for (uint n = 1; n <= count; n++)
                {
                    Ksnm.Math.GreatestCommonDivisor(m, n);
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");
        }
        public static void PowWeightTest()
        {
            const int count = 10000;
            stopwatch.Restart();
            Console.WriteLine($"System.Math.Pow");
            for (int i = 0; i < count; i++)
            {
                for (int m = 0; m <= 10; m++)
                {
                    for (int n = 0; n <= 10; n++)
                    {
                        Math.Pow(m, n);
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");

            stopwatch.Restart();
            Console.WriteLine($"Ksnm.Math.Pow(int)");
            for (int i = 0; i < count; i++)
            {
                for (int m = 0; m <= 10; m++)
                {
                    for (int n = 0; n <= 10; n++)
                    {
                        Ksnm.Math.Pow(m, n);
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");

            stopwatch.Restart();
            Console.WriteLine($"Ksnm.Math.Pow(uint)");
            for (int i = 0; i < count; i++)
            {
                for (uint m = 0; m <= 10; m++)
                {
                    for (uint n = 0; n <= 10; n++)
                    {
                        Ksnm.Math.Pow(m, n);
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");
        }
        public static void PowTest()
        {
            for (int e = -9; e <= 9; e++)
            {
                for (int n = -10; n < 10; n++)
                {
                    Console.WriteLine($"{n}^{e}={Math.Pow(n, e)}");
                }
                Console.WriteLine();
            }
            for (int n = -10; n < 10; n++)
            {
                for (int e = -9; e <= 9; e++)
                {
                    Console.WriteLine($"{n}^{e}={Math.Pow(n, e)}");
                }
                Console.WriteLine();
            }
        }
    }
}
