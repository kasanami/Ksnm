namespace Ksnm.Cryptography.Enigma;

/// <summary>
/// Enigma暗号機のプラグボードを表すクラス。
/// </summary>
public sealed class Plugboard
{
    private readonly int[] _wiring;

    /// <summary>
    /// 交換なしのプラグボードを生成する。
    /// </summary>
    public Plugboard()
    {
        _wiring = new int[26];

        for (int i = 0; i < 26; i++)
        {
            _wiring[i] = i;
        }
    }

    /// <summary>
    /// 指定したペアでプラグボードを生成する。
    /// 例: "AB CD EF"
    /// </summary>
    public Plugboard(string pairs)
        : this()
    {
        SetPairs(pairs);
    }

    /// <summary>
    /// 文字をプラグボードに通す。
    /// </summary>
    public int Transform(int input)
    {
        if (input is < 0 or > 25)
            throw new ArgumentOutOfRangeException(nameof(input));

        return _wiring[input];
    }

    /// <summary>
    /// プラグボードの接続を設定する。
    /// 例: "AB CD EF GH"
    /// </summary>
    public void SetPairs(string pairs)
    {
        if (pairs is null)
            throw new ArgumentNullException(nameof(pairs));

        string[] tokens = pairs
            .Split(
                new[] { ' ', '\t', ',', ';' },
                StringSplitOptions.RemoveEmptyEntries);

        foreach (string token in tokens)
        {
            if (token.Length != 2)
            {
                throw new ArgumentException(
                    $"不正なプラグボード設定です: {token}",
                    nameof(pairs));
            }

            char a = char.ToUpperInvariant(token[0]);
            char b = char.ToUpperInvariant(token[1]);

            if (a is < 'A' or > 'Z' ||
                b is < 'A' or > 'Z')
            {
                throw new ArgumentException(
                    $"A～Z以外の文字が含まれています: {token}",
                    nameof(pairs));
            }

            if (a == b)
            {
                throw new ArgumentException(
                    $"同じ文字同士は接続できません: {token}",
                    nameof(pairs));
            }

            int ia = a - 'A';
            int ib = b - 'A';

            // すでに別の文字と接続されている場合
            if (_wiring[ia] != ia)
            {
                throw new ArgumentException(
                    $"文字 {a} は既に接続されています。",
                    nameof(pairs));
            }

            if (_wiring[ib] != ib)
            {
                throw new ArgumentException(
                    $"文字 {b} は既に接続されています。",
                    nameof(pairs));
            }

            _wiring[ia] = ib;
            _wiring[ib] = ia;
        }
    }
}