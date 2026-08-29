namespace Ksnm.Cryptography.Enigma;

/// <summary>
/// 8bitのリフレクターを表すクラス。
/// </summary>
public sealed class Reflector8
{
    private const int Size = 256;
    private readonly byte[] _wiring;
    /// <summary>
    /// 交換なしのリフレクターを生成する。
    /// </summary>
    public Reflector8()
    {
        var wiring = new byte[Size];
        for (int i = 0; i < Size; i++)
        {
            wiring[i] = (byte)i;
        }
        _wiring = ParseWiring(wiring);
    }

    public Reflector8(ReadOnlySpan<byte> wiring)
    {
        if (wiring.Length != Size)
            throw new ArgumentException($"リフレクター配線は{Size}である必要があります。", nameof(wiring));

        _wiring = ParseWiring(wiring);
    }

    public Reflector8(Random random)
    {
        var wiring = new byte[Size];
        for (int i = 0; i < Size; i++)
        {
            wiring[i] = (byte)i;
        }
        // ランダムなインデックス配列を作成する
        var randomIndexes = new byte[Size];
        for (int i = 0; i < Size; i++)
        {
            randomIndexes[i] = (byte)i;
        }
        random.Shuffle(randomIndexes);
        // ランダムにペアを入れ替える
        // ただし、同じ文字が複数回使われないようにする
        bool[] used = new bool[Size];
        for (int i = 0; i < Size; i++)
        {
            if (used[i]) continue;
            var j = randomIndexes[i];
            if (used[j]) continue;
            (wiring[i], wiring[j]) = (wiring[j], wiring[i]);
            used[i] = true;
            used[j] = true;
        }
        _wiring = ParseWiring(wiring);
    }

    public byte Reflect(byte input)
    {
        return _wiring[input];
    }

    private static byte[] ParseWiring(ReadOnlySpan<byte> wiring)
    {
        byte[] result = new byte[Size];
        bool[] used = new bool[Size];

        for (int i = 0; i < Size; i++)
        {
            byte value = wiring[i];

            if (used[value])
                throw new ArgumentException("リフレクター配線に同じ文字が複数存在します。", nameof(wiring));

            used[value] = true;
            result[i] = value;
        }

        // Reflector8は必ず自己逆写像でなければならない
        for (int i = 0; i < Size; i++)
        {
            if (result[result[i]] != i)
                throw new ArgumentException(
                    "リフレクター配線が自己逆写像になっていません。",
                    nameof(wiring));
        }

        return result;
    }
}