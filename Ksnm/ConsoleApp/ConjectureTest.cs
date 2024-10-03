using Ksnm.Science.Mathematics;

namespace ConsoleApp
{
    /// <summary>
    /// 予想をテスト
    /// </summary>
    public class ConjectureTest
    {
        public static void Run()
        {

            Console.WriteLine($"ゴールドバッハ予想 {Conjecture.GoldbachsConjecture(1000)}");
            
        }
    }
}
