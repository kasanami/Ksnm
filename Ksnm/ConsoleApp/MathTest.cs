using Ksnm.Numerics;
using Ksnm.Science.Mathematics;
using System.Numerics;
using static Ksnm.Math;
using static Ksnm.Science.Mathematics.Formula;

namespace ConsoleApp
{
    internal class MathTest
    {
        public static void Run()
        {
            FastInverseSqrtTest();

            // Log2
            if (false)
            {
                int maxValueLog2 = Ksnm.Math.Log2<int>(int.MaxValue, 1);
                int minValueLog2 = Ksnm.Math.Log2<int>(int.MinValue, 1);
            }

            Console.WriteLine("ゴールドバッハ予想");
            if (Conjecture.GoldbachsConjecture(4, 100))
            {
                Console.WriteLine("→4～100の範囲では成り立つ");
            }
            Console.WriteLine("ガウス＝ルジャンドルのアルゴリズム");
            {
                Console.WriteLine("double");
                var pi = Algorithm.GaussLegendre<double>(3);
                Console.WriteLine($"→{pi}");
            }
            {
                Console.WriteLine("BigDecimal");
                BigDecimal pi;
                Console.WriteLine($"DefaultMinExponent={BigDecimal.DefaultMinExponent}");
                for (var count = 0; count <= 8; count++)
                {
                    pi = Algorithm.GaussLegendre<BigDecimal>(0, count, BigDecimal.Sqrt);
                    Console.WriteLine($"{count}:{pi}");
                }
            }
            GammaTest();
            StirlingsFormulaTest();
        }
        public static void GammaTest()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

            //Console.WriteLine("GammaTest");
            //for (double i = 0; i <= 10; i += 0.25)
            //{
            //    var x = Gamma(i);
            //    Console.WriteLine($"({i:0.0000})={x:0.0000}");
            //}

            Console.WriteLine();
        }
        public static void StirlingsFormulaTest()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

            for (double i = 0; i <= 10; i += 0.25)
            {
                var x = StirlingsFormula(i);
                Console.WriteLine($"({i:0.0000})={x:0.0000}");
            }

            Console.WriteLine();
        }

        public static void FastInverseSqrtTest()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

            for (float f = 1; f <= 9; f += 0.5f)
            {
                var a = Ksnm.Math.InverseSqrt(f);
                var b = Ksnm.Math.FastInverseSqrt(f);
                Console.WriteLine($"{a:0.00000000}:{b:0.00000000} ({a - b:0.00000000})");
            }

            Console.WriteLine();
        }
    }
}