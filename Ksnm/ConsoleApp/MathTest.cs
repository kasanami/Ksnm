using Ksnm.Numerics;
using Ksnm.Science.Mathematics;
using System.Numerics;
using static Ksnm.Science.Mathematics.Formula;

namespace ConsoleApp
{
    internal class MathTest
    {
        public static void Run()
        {
            Console.WriteLine("Exp");
            {
                Console.WriteLine($"double  :{Math.Exp<double>(1.0, double.Epsilon)}");
                Console.WriteLine($"decimal :{Math.Exp<decimal>(decimal.One, Math.DecimalEpsilon)}");
                Console.WriteLine($"Fixed16 :{Math.Exp<Fixed16>(Fixed16.One, Fixed16.Epsilon)}");
            }

            FastInverseSqrtTest();

            // ネイピア数を計算
            Console.WriteLine("CalculateE");
            {
                Console.WriteLine($"BFloat16:{Math.CalculateE<BFloat16>()}");
                Console.WriteLine($"Half    :{Math.CalculateE<Half>()}");
                Console.WriteLine($"float   :{Math.CalculateE<float>()}");
                Console.WriteLine($"double  :{Math.CalculateE<double>()}");
                Console.WriteLine($"decimal :{Math.CalculateE<decimal>(Math.DecimalEpsilon)}");
                Console.WriteLine($"Fixed64 :{Math.CalculateE<Fixed64>()}");
            }

            // 円周率を計算
            Console.WriteLine("CalculatePi");
            {
                Console.WriteLine($"BFloat16:{Math.CalculatePi<BFloat16>()}");
                Console.WriteLine($"Half    :{Math.CalculatePi<Half>()}");
                Console.WriteLine($"float   :{Math.CalculatePi<float>()}");
                Console.WriteLine($"double  :{Math.CalculatePi<double>()}");
                Console.WriteLine($"decimal :{Math.CalculatePi<decimal>(Math.DecimalEpsilon)}");
                Console.WriteLine($"Fixed64 :{Math.CalculatePi<Fixed64>()}");
            }

            // Log2
            Console.WriteLine("Log2");
            {
                Console.WriteLine($"double.Log2:{double.Log2(int.MaxValue)}");
                Console.WriteLine($"int   .Log2:{int.Log2(int.MaxValue)}");
                Console.WriteLine($"Math  .Log2:{Math.Log2<double>(int.MaxValue)}");
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
            Ksnm.Debug.WriteLineCallerInfo();

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
            Ksnm.Debug.WriteLineCallerInfo();

            for (double i = 0; i <= 10; i += 0.25)
            {
                var x = StirlingsFormula(i);
                Console.WriteLine($"({i:0.0000})={x:0.0000}");
            }

            Console.WriteLine();
        }

        public static void FastInverseSqrtTest()
        {
            Ksnm.Debug.WriteLineCallerInfo();

            Console.WriteLine("float");
            for (float f = 1; f <= 9; f += 0.5f)
            {
                var a = Ksnm.Math.InverseSqrt(f);
                var b = Ksnm.Math.FastInverseSqrt(f);
                Console.WriteLine($"{a:0.00000000}:{b:0.00000000} ({a - b:0.00000000})");
            }

            Console.WriteLine("double");
            for (double f = 1; f <= 9; f += 0.5)
            {
                var a = Ksnm.Math.InverseSqrt(f);
                var b = Ksnm.Math.FastInverseSqrt(f);
                Console.WriteLine($"{a:0.00000000}:{b:0.00000000} ({a - b:0.00000000})");
            }

            Console.WriteLine();
        }
    }
}