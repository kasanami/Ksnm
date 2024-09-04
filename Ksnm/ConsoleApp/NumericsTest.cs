using Ksnm;
using Ksnm.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class NumericsTest
    {
        public static void Run()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

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
