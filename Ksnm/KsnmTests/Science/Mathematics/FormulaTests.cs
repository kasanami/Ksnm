using Ksnm.Numerics;
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
        static readonly string Pi100 = "3.141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117068";

        [TestMethod()]
        public void LeibnizFormulaTest()
        {
            {
                var pi = Formula.LeibnizFormula<double>() * 4;
                Assert.AreEqual(System.Math.PI, pi, 0.0000001);
            }
            {
                var pi = Formula.LeibnizFormula<decimal>(0.00000_00000_00000_00000_1m) * 4;
                Assert.AreEqual(3.14159265358979323846m, pi, 0.00001m);
            }
            if(false)
            {
                var tolerance = new BigDecimal(1, -105);
                BigDecimal.DefaultMinExponent = -105;
                var pi = Formula.LeibnizFormula<BigDecimal>(tolerance) * 4;
                pi = BigDecimal.Round(pi, 100, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(Pi100, pi.ToString());
            }
        }

        [TestMethod()]
        public void WallisFormulaTest()
        {
            {
                var pi = Formula.WallisFormula<double>(100000) * 2;
                Assert.AreEqual(System.Math.PI, pi, 0.0001);
            }
            {
                var pi = Formula.WallisFormula<decimal>(100000) * 2;
                Assert.AreEqual(3.14159265358979323846m, pi, 0.0001m);
            }
            if(false)
            {
                BigDecimal.DefaultMinExponent = -105;
                var pi = Formula.WallisFormula<BigDecimal>(100000) * 2;
                pi = BigDecimal.Round(pi, 100, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(3.141592m, pi.ToDecimal(), 0.0001m);
            }
        }

        [TestMethod()]
        public void RamanujansPiFormulaTest()
        {
            {
                var pi = 1 / Formula.RamanujansPiFormula<double>();
                Assert.AreEqual(System.Math.PI, pi, 0.000000000000001);
            }
            {
                // オーバーフローするため制度が悪い
                var pi = 1 / Formula.RamanujansPiFormula<decimal>(0.00000_00000_00000_1m, 3);
                Assert.AreEqual(3.14159265358979323846m, pi, 0.0001m);
            }
            {
                BigDecimal.DefaultMinExponent = -105;// 四捨五入のため調整
                var tolerance = new BigDecimal(1, -100);
                var pi = 1 / Formula.RamanujansPiFormula<BigDecimal>(tolerance);
                pi = BigDecimal.Round(pi, 100, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(Pi100, pi.ToString());
            }
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
                BigDecimal.DefaultMinExponent = -105;// 四捨五入のため調整
                var tolerance = new BigDecimal(1, -100);
                var pi = Formula.MachinsFormula<BigDecimal>(tolerance) * 4;
                pi = BigDecimal.Round(pi, 100, System.MidpointRounding.AwayFromZero);
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
                var pi = 1 / Formula.ChudnovskySeries<decimal>(0.00000_00000_00000_1m, 1);
                Assert.AreEqual(3.14159265358979m, pi, 0.00000_00000_1m);
            }
            // BigDecimal型
            if (false)
            {
                BigDecimal.DefaultMinExponent = -105;// 四捨五入のため-100に更に-5
                var tolerance = new BigDecimal(1, -100);
                var pi = 1 / Formula.ChudnovskySeries<BigDecimal>(tolerance);
                pi = BigDecimal.Round(pi, 100, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(Pi100, pi.ToString());
            }
        }
    }
}