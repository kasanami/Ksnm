using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Ksnm.LitJson.Tests
{
    [TestClass()]
    public class JsonMapperTests
    {
        /// <summary>
        /// テスト用データ
        /// </summary>
        public class TestData
        {
            public bool boolMember = true;
            public int intMember = 1234567890;
            public decimal decimalMember = 123456789.123m;
            public string stringMember = "abcあいう";
            public bool boolProperty { get; set; } = false;
            public int intProperty { get; set; } = 1234567890;
            public decimal decimalProperty { get; set; } = 123456789.123m;
            public string stringProperty { get; set; } = "abcあいう";

            public int[] arrayMember = { 0, 1, 2, 3 };
            public List<int> listMember = new List<int> { 0, 1, 2, 3 };

            public Dictionary<string, string> dictionaryMember = new Dictionary<string, string>
            {
                {"key0","value0" },
                {"key1","value1" },
                {"key2","value2" },
            };
            public Dictionary<int, string> dictionaryMember2 = new Dictionary<int, string>
            {
                {0,"value0" },
                {1,"value1" },
                {2,"value2" },
            };

            public override bool Equals(object obj)
            {
                if (obj is TestData == false)
                {
                    return false;
                }

                var testData = obj as TestData;
                if (boolMember != testData.boolMember) { return false; }
                if (intMember != testData.intMember) { return false; }
                if (decimalMember != testData.decimalMember) { return false; }
                if (stringMember != testData.stringMember) { return false; }
                if (boolProperty != testData.boolProperty) { return false; }
                if (intProperty != testData.intProperty) { return false; }
                if (decimalProperty != testData.decimalProperty) { return false; }
                if (stringProperty != testData.stringProperty) { return false; }

                if (arrayMember.SequenceEqual(testData.arrayMember) == false) { return false; }
                if (listMember.SequenceEqual(testData.listMember) == false) { return false; }

                if (dictionaryMember.SequenceEqual(testData.dictionaryMember) == false) { return false; }
                if (dictionaryMember2.SequenceEqual(testData.dictionaryMember2) == false) { return false; }

                return true;
            }
            public override int GetHashCode()
            {
                return intMember;
            }
        }

        [TestMethod()]
        public void ToJsonTest()
        {
            var testData = new TestData();
            var json = JsonMapper.ToJson(testData);
            var testData2 = JsonMapper.ToObject<TestData>(json);
            if (testData.Equals(testData2) == false)
            {
                Assert.Fail();
            }
        }
    }
}
