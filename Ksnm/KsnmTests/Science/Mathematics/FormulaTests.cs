﻿using Ksnm.Numerics;
using Ksnm.Science.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

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
                var pi = Formula.MachinsFormula<double>() * 4;
                Assert.AreEqual(3.14159265358979, pi, 0.000001);
            }
            // BigDecimal型
            {
                BigDecimal.DefaultMinExponent = -102;// 四捨五入のため-100に更に-2
                var tolerance = new BigDecimal(1, -100);
                var pi = Formula.MachinsFormula<BigDecimal>(tolerance) * 4;
                pi.SetMinExponentAndRound(-100);
                Assert.AreEqual(Pi100, pi.ToString());
            }
        }

        [TestMethod()]
        public void ChudnovskySeriesTest()
        {
            // double型
            {
                var pi = 1 / Formula.ChudnovskySeries<double>();
                Assert.AreEqual(3.14159265358979, pi, 0.00000_00000_1);
            }
            // decimal型
            {
                var pi = 1 / Formula.ChudnovskySeries<decimal>(0.00000_00000_00000_1m);
                Assert.AreEqual(3.14159265358979m, pi, 0.00000_00000_1m);
            }
            // BigDecimal型
            if (false)
            {
                BigDecimal.DefaultMinExponent = -102;// 四捨五入のため-100に更に-2
                var tolerance = new BigDecimal(1, -100);
                var pi = 1 / Formula.ChudnovskySeries<BigDecimal>(tolerance);
                pi.SetMinExponentAndRound(-100);
                Assert.AreEqual(Pi100, pi.ToString());
            }
        }
    }
}