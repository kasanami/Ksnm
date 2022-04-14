using Ksnm.Numerics;
using Ksnm.Science.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Science.Mathematics.Tests
{
    [TestClass()]
    public class FormulaTests
    {
        /// <summary>
        /// 小数点以下100桁の円周率の文字列
        /// </summary>
        static readonly string Pi100 = "3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170680";

        [TestMethod()]
        public void LeibnizTest()
        {
            {
                var pi = Formula.Leibniz(10000000) * 4;
                Assert.AreEqual(System.Math.PI, pi, 0.000001);
            }
            {
                var pi = Formula.LeibnizForDecimal(100_000) * 4;
                pi = decimal.Round(pi, 21, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(3.141582653589793488463m, pi);

                var pi2 = Formula.LeibnizForBigDecimal(100_000, 30) * 4;
                pi2 = BigDecimal.Round(pi2, 21, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(pi.ToString(), pi2.ToString());
            }
        }

        [TestMethod()]
        public void WallisProductTest()
        {
            {
                var pi = Formula.WallisProduct(10000000) * 2;
                Assert.AreEqual(System.Math.PI, pi, 0.000001);
            }
            {
                var pi = Formula.WallisProductForDecimal(100_000) * 2;
                pi = decimal.Round(pi, 21, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(3.141584799657246379213m, pi);

                var pi2 = Formula.WallisProductForBigDecimal(100_000, 30) * 2;
                pi2 = BigDecimal.Round(pi2, 21, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(pi.ToString(), pi2.ToString());
            }
        }

        [TestMethod()]
        public void PIByRamanujanTest()
        {
            var pi = 1 / Formula.PIByRamanujan(13, 105);
            pi.SetMinExponentAndRound(-100);
            Assert.AreEqual(Pi100, pi.ToString());
        }

        [TestMethod()]
        public void MachinsFormulaTest()
        {
            // double型
            {
                var pi = Formula.MachinsFormula(10) * 4;
                Assert.AreEqual(3.14159265358979, pi, 0.000001);
            }
            // BigDecimal型
            {
                var pi = Formula.MachinsFormula(71, 100) * 4;
                Assert.AreEqual(Pi100, pi.ToString());
            }
        }

        
    }
}