﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Utilities.Tests
{
    [TestClass()]
    public class CSVLoggerTests
    {
        [TestMethod()]
        public void AppendLineTest()
        {
            var filePath = "hoge.csv";
            var csvLogger = new CSVLogger(filePath);
            csvLogger.AppendLine("A", "B", "C");
            csvLogger.AppendLine("1", "2", "3");
            csvLogger.AppendLine(",", "\"", "\n");
            var expected =
                "A,B,C\r\n" +
                "1,2,3\r\n" +
                "\",\",\"\"\",\"\r\n\"\r\n";
            var actual = System.IO.File.ReadAllText(filePath);
            System.IO.File.Delete(filePath);
            Assert.AreEqual(expected, actual);
        }
    }
}
