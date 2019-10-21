using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Utilities.Tests
{
    [TestClass()]
    public class CsvLoggerTests
    {
        [TestMethod()]
        public void AppendLineTest()
        {
            var filePath = "hoge.csv";
            var csvLogger = new CsvLogger(filePath);
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
