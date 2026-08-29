namespace Ksnm.Cryptography.Enigma;

/// <summary>
/// Enigma暗号機のリフレクターを表すクラス。
/// </summary>
public sealed class Reflector
{
    private readonly int[] _wiring;
    /// <summary>
    /// 交換なしのリフレクターを生成する。
    /// </summary>
    public Reflector()
    {
        _wiring = new int[26];
        for (int i = 0; i < 26; i++)
        {
            _wiring[i] = i;
        }
    }
    public Reflector(string wiring)
    {
        if (wiring.Length != 26)
            throw new ArgumentException(
                "リフレクター配線は26文字である必要があります。",
                nameof(wiring));

        _wiring = ParseWiring(wiring);
    }

    public int Reflect(int input)
    {
        if (input is < 0 or > 25)
            throw new ArgumentOutOfRangeException(nameof(input));

        return _wiring[input];
    }

    private static int[] ParseWiring(string wiring)
    {
        int[] result = new int[26];
        bool[] used = new bool[26];

        for (int i = 0; i < 26; i++)
        {
            char c = char.ToUpperInvariant(wiring[i]);

            if (c is < 'A' or > 'Z')
                throw new ArgumentException(
                    "リフレクター配線にはA～Zのみ使用できます。",
                    nameof(wiring));

            int value = c - 'A';

            if (used[value])
                throw new ArgumentException(
                    "リフレクター配線に同じ文字が複数存在します。",
                    nameof(wiring));

            used[value] = true;
            result[i] = value;
        }

        // Reflectorは必ず自己逆写像でなければならない
        for (int i = 0; i < 26; i++)
        {
            if (result[result[i]] != i)
                throw new ArgumentException(
                    "リフレクター配線が自己逆写像になっていません。",
                    nameof(wiring));
        }

        return result;
    }
}