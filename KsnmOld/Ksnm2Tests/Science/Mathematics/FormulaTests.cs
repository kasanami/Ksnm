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
        public void LeibnizFormulaTest()
        {
            {
                var pi = Formula.LeibnizFormula(10000000) * 4;
                Assert.AreEqual(System.Math.PI, pi, 0.000001);
            }
            {
                var pi = Formula.LeibnizFormulaForDecimal(100_000) * 4;
                pi = decimal.Round(pi, 21, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(3.141582653589793488463m, pi);

                var pi2 = Formula.LeibnizFormula(100_000, 30) * 4;
                pi2 = BigDecimal.Round(pi2, 21, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(pi.ToString(), pi2.ToString());
            }
        }

        [TestMethod()]
        public void WallisFormulaTest()
        {
            {
                var pi = Formula.WallisFormula(10000000) * 2;
                Assert.AreEqual(System.Math.PI, pi, 0.000001);
            }
            {
                var pi = Formula.WallisFormulaForDecimal(100_000) * 2;
                pi = decimal.Round(pi, 21, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(3.141584799657246379213m, pi);

                var pi2 = Formula.WallisFormula(100_000, 30) * 2;
                pi2 = BigDecimal.Round(pi2, 21, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(pi.ToString(), pi2.ToString());
            }
        }

        [TestMethod()]
        public void RamanujansPiFormulaTest()
        {
            var pi = 1 / Formula.RamanujansPiFormula(13, 105);
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
                var pi = Formula.MachinsFormula(71, 105) * 4;
                pi.SetMinExponentAndRound(-100);
                Assert.AreEqual(Pi100, pi.ToString());
            }
        }

        
    }
}