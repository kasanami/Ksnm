using System;
using System.Diagnostics;

namespace DemoApp
{
    public class Test
    {
        static Stopwatch stopwatch = new Stopwatch();

        public static void RunAll()
        {
            //GreatestCommonDivisorWeightTest();
            //PowWeightTest();
            //PowTest();
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
