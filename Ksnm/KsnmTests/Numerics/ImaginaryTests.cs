﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class ImaginaryTests
    {

        [TestMethod()]
        public void OperatorTest()
        {
            // Complexの計算結果と比較
            for (int i = -10; i <= 10; i++)
            {
                var i1 = new Imaginary(i);
                var c1 = new Complex(0, i);
                for (int j = -10; j <= 10; j++)
                {
                    var i2 = new Imaginary(j);
                    var c2 = new Complex(0, j);
                    // 加算
                    var i3 = i1 + i2;
                    var c3 = c1 + c2;
                    Assert.AreEqual(c3.Imaginary, i3.Value, $"i={i} j={j}");
                    // 減算
                    i3 = i1 - i2;
                    c3 = c1 - c2;
                    Assert.AreEqual(c3.Imaginary, i3.Value, $"i={i} j={j}");
                    // 乗算
                    var real = i1 * i2;
                    c3 = c1 * c2;
                    Assert.AreEqual(c3.Real, real, $"i={i} j={j}");
                    // 除算
                    if (j != 0)
                    {
                        real = i1 / i2;
                        c3 = c1 / c2;
                        Assert.AreEqual(c3.Real, real, $"i={i} j={j}");
                    }
                }
            }
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var i1 = new Imaginary(123);
            var i2 = new Imaginary(123);
            Assert.AreEqual(i1.GetHashCode(), i2.GetHashCode());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var i = new Imaginary(123);
            Assert.AreEqual("123i", i.ToString());
        }
    }
}