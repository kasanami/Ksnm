using System;
using System.Diagnostics;
using Numeric = Ksnm.Numerics.Numeric;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using Ksnm.ExtensionMethods.System.Collections.Generic.Dictionary;
using Ksnm.ExtensionMethods.System.Single;
using Ksnm.ExtensionMethods.System.Double;
using System.Numerics;
using Ksnm.Numerics;
using static System.Math;
using System.Collections.Generic;
using DemoApp.Conjectures;

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
            //ExpTest();
            //LogTest();
            //SqrtTest();
            //SinTest();
            //AsinTest();
            //AtanTest();
            /*
            Numeric num = new Numeric(100m);
            num.Normalize();
            */
            //AbcConjectureTest();
            CollatzProblem();
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
                    Console.WriteLine($"{i} => {Math.Sqrt(value)}");
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
                var value = BigDecimal.Sqrt(i, 105);
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
        public static void ExpTest()
        {
            Console.WriteLine($"ExpTest()");
            Console.WriteLine($"System.Math");
            for (int n = -10; n < 10; n++)
            {
                Console.WriteLine($"e^{n}={Exp(n)}");
            }
            Console.WriteLine($"Ksnm.Math");
            for (int n = -10; n < 10; n++)
            {
                Console.WriteLine($"e^{n}={Ksnm.Math.Exp(n)}");
            }
            // 計算回数を替えて試す
            for (int n = 0; n < 10; n++)
            {
                Console.WriteLine($"e^10 {n}={Ksnm.Math.Exp(10, n)}");
            }
        }
        public static void LogTest()
        {

        }
        public static void SqrtTest()
        {
            Console.WriteLine($"SqrtTest()");

            var countCount = new Dictionary<int, int>();

            {
                decimal sqrt;
                sqrt = Ksnm.Math.Sqrt(400m);
                sqrt = Ksnm.Math.Sqrt(16m);
                sqrt = Ksnm.Math.Sqrt(5m);
                sqrt = Ksnm.Math.Sqrt(31m, 30);
            }

            for (decimal i = 0; i < 100000; i++)
            {
                var sqrt = Ksnm.Math.Sqrt(i, 30);
                Console.WriteLine($"sqrt({i})={sqrt}");
            }

            countCount = countCount.OrderByKey();
        }
        public static void SinTest()
        {
            Console.WriteLine($"SinTest()");
            Console.WriteLine($"System.Sin");
            for (int n = -10; n < 10; n++)
            {
                Console.WriteLine($"Sin {n}={Sin(n)}");
            }
            Console.WriteLine($"Ksnm.Math");
            for (int n = -10; n < 10; n++)
            {
                Console.WriteLine($"Sin {n}={Ksnm.Math.Sin(n)}");
            }
            // 計算回数を替えて試す
            Console.WriteLine();
            for (int n = -10; n < 10; n++)
            {
                for (int c = 0; c < 30; c++)
                {
                    Console.WriteLine($"Sin {n} {c}={Ksnm.Math.Sin(n, c)}");
                }
            }
        }
        public static void AsinTest()
        {
            Console.WriteLine($"AsinTest()");

            Console.WriteLine($"π/2={Ksnm.Math.PI_Decimal / 2}");
            Console.WriteLine($"π/4={Ksnm.Math.PI_Decimal / 4}");

            Console.WriteLine($"System.Math");
            for (double x = -1; x <= 1; x += 0.125)
            {
                Console.WriteLine($"Asin({x})={Math.Asin(x)}");
            }

            // 計算回数を替えて試す
            Console.WriteLine();
            for (decimal x = -1; x <= 1; x += 0.125m)
            {
                for (int c = 1; c <= 50; c++)
                {
                    try
                    {
                        Console.WriteLine($"Asin({x}, {c})={Ksnm.Math.Asin(x, c)}");
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }
        public static void AtanTest()
        {
            Console.WriteLine($"AtanTest()");

            Console.WriteLine($"π/2={Ksnm.Math.PI_Decimal / 2}");
            Console.WriteLine($"π/4={Ksnm.Math.PI_Decimal / 4}");

            Console.WriteLine($"System.Math");
            for (double x = -1; x <= 1; x += 0.125)
            {
                Console.WriteLine($"Atan({x})={Math.Atan(x)}");
            }

            {
                decimal x = 1;
                int c = 100000;
                Console.WriteLine($"Atan({x}, {c})={Ksnm.Math.Atan(x, c)}");
            }

            // 計算回数を替えて試す
            Console.WriteLine();
            for (decimal x = -1; x <= 1; x += 0.125m)
            {
                for (int c = 1; c <= 1; c++)
                {
                    try
                    {
                        Console.WriteLine($"Atan({x}, {c})={Ksnm.Math.Atan(x, c)}");
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// ABC予想を用いたテスト
        /// </summary>
        public static void AbcConjectureTest()
        {
            {
                int a = 1;
                int b = 8;
                int c = a + b;
                int d = AbcConjecture.Radical(a * b * c);
            }
            var trueCount = 0;
            var random = new Random();
            for (int j = 1; j < 100; j++)
            {
                for (int i = 1; i < 100; i++)
                {
                    int a = i;
                    int b = j;
                    int c = a + b;
                    if (Ksnm.Math.GreatestCommonDivisor(a, b, c) == 1)
                    {
                        // 互いに素なら比較
                        int d = AbcConjecture.Radical(a * b * c);
                        Console.WriteLine($"{a},{b}\tc < d={c < d}");
                        if (c < d)
                        {
                            trueCount++;
                        }
                    }
                }
            }
            Console.WriteLine($"{nameof(trueCount)}={trueCount}");
        }
        /// <summary>
        /// コラッツの問題
        /// </summary>
        public static void CollatzProblem()
        {
            // この数以下は1になる
            int min = 1;
            // どれくらいの数値までテストするか
            for (int i = min + 1; i < 10000; i++)
            {
                var value = i;
                // 試行回数
                for (int j = 0; j < 100000; j++)
                {
                    if ((value & 1) == 0)
                    {
                        // 偶数
                        value /= 2;
                        // 1になったか？
                        if (value <= min)
                        {
                            min = i;
                            break;
                        }
                    }
                    else
                    {
                        // 奇数
                        value = value * 3 + 1;
                    }
                }
            }
        }
    }
}
