using System.Numerics;
using static Ksnm.Math;
using static Ksnm.Science.Mathematics.Formula;

namespace ConsoleApp
{
    internal class MathTest
    {
        public static void Run()
        {
            GammaTest();
            StirlingsFormulaTest();
        }
        public static void GammaTest()
        {
            //Console.WriteLine("GammaTest");
            //for (double i = 0; i <= 10; i += 0.25)
            //{
            //    var x = Gamma(i);
            //    Console.WriteLine($"({i:0.0000})={x:0.0000}");
            //}
        }
        public static void StirlingsFormulaTest()
        {
            Console.WriteLine("StirlingsFormulaTest");
            for (double i = 0; i <= 10; i += 0.25)
            {
                var x = StirlingsFormula(i);
                Console.WriteLine($"({i:0.0000})={x:0.0000}");
            }
        }
    }
}