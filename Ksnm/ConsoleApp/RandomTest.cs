namespace ConsoleApp;

internal class RandomTest
{
    public static void Run()
    {
        var rng = new Ksnm.Randoms.Xorshift32(123456);
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine(rng.Next(0, 101));
        }
    }
}