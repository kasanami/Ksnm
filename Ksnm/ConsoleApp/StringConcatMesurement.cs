using BenchmarkDotNet.Attributes;
using System.Text;

namespace ConsoleApp
{
    /// <summary>
    /// 文字列を接続する速度を測定
    /// （BenchmarkDotNetのサンプル）
    /// </summary>
    [MemoryDiagnoser]
    [MinColumn, MaxColumn]
    public class StringConcatMesurement
    {
        private int NumberOfItems = 2000;
        [Benchmark]
        public string WithStringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < NumberOfItems; i++)
            {
                sb.Append("1");
            }
            return sb.ToString();
        }
        [Benchmark]
        public string WithStringType()
        {
            string s = string.Empty;
            for (int i = 0; i < NumberOfItems; i++)
            {
                s += "1";
            }
            return s;
        }
    }
}
