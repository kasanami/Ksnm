namespace Ksnm.Cryptography.Enigma;

/// <summary>
/// 8bitのプラグボードを表すクラス。
/// </summary>
public sealed class Plugboard8
{
    private const int Size = 256;
    private readonly byte[] _wiring;

    /// <summary>
    /// 交換なしのプラグボードを生成する。
    /// </summary>
    public Plugboard8()
    {
        _wiring = new byte[Size];

        for (int i = 0; i < Size; i++)
        {
            _wiring[i] = (byte)i;
        }
    }

    /// <summary>
    /// ランダムな接続でプラグボードを生成する。
    /// </summary>
    public Plugboard8(Random random)
    {
        _wiring = new byte[Size];
        for (int i = 0; i < Size; i++)
        {
            _wiring[i] = (byte)i;
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
            (_wiring[i], _wiring[j]) = (_wiring[j], _wiring[i]);
            used[i] = true;
            used[j] = true;
        }
    }

    /// <summary>
    /// 指定したペアでプラグボードを生成する。
    /// 例: [A,B, C,D, E,F, G,H]
    /// </summary>
    public Plugboard8(byte[] pairs)
        : this()
    {
        SetPairs(pairs);
    }

    /// <summary>
    /// 文字をプラグボードに通す。
    /// </summary>
    public byte Transform(byte input)
    {
        return _wiring[input];
    }

    /// <summary>
    /// プラグボードの接続を設定する。
    /// 例: [A,B, C,D, E,F, G,H]
    /// </summary>
    public void SetPairs(byte[] pairs)
    {
        if (pairs is null)
            throw new ArgumentNullException(nameof(pairs));

        if (pairs.Length % 2 != 0)
            throw new ArgumentException("プラグボードの接続はペアで指定する必要があります。", nameof(pairs));

        byte[][] tokens = pairs.Chunk(2).ToArray();

        foreach (byte[] token in tokens)
        {
            if (token.Length != 2)
            {
                throw new ArgumentException(
                    $"不正なプラグボード設定です: {token}",
                    nameof(pairs));
            }

            byte a = token[0];
            byte b = token[1];

            if (a == b)
            {
                throw new ArgumentException($"同じ値同士は接続できません: {token}", nameof(pairs));
            }

            // すでに別の文字と接続されている場合
            if (_wiring[a] != a)
            {
                throw new ArgumentException($"{a} は既に接続されています。", nameof(pairs));
            }

            if (_wiring[b] != b)
            {
                throw new ArgumentException($"{b} は既に接続されています。", nameof(pairs));
            }

            _wiring[a] = b;
            _wiring[b] = a;
        }
    }
}