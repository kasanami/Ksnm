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
        }
        public static void GreatestCommonDivisorWeightTest()
        {
            const int count = 2000;
            stopwatch.Restart();
            Console.WriteLine($"Math.GreatestCommonDivisor");
            for (int m = 1; m <= count; m++)
            {
                for (int n = 1; n <= count; n++)
                {
                    Ksnm.Math.GreatestCommonDivisor(m, n);
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");
        }
    }
}
