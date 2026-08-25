namespace Ksnm.Cryptography.Enigma;

/// <summary>
/// Enigma暗号機のローターを表すクラス。
/// </summary>
public sealed class Rotor
{
    private const int AlphabetSize = 26;

    /// <summary>
    /// ローターの配線。
    /// wiring[i] は、ローター内部で入力 i がどの出力へ接続されるかを表す。
    /// </summary>
    private readonly int[] _wiring;

    /// <summary>
    /// 逆方向の配線。
    /// </summary>
    private readonly int[] _inverseWiring;

    /// <summary>
    /// ノッチ位置。
    /// この位置で次のローターを進める。
    /// </summary>
    private readonly int _notch;

    /// <summary>
    /// ローターの現在位置。
    /// 0 = A, 1 = B, ..., 25 = Z
    /// </summary>
    public int Position { get; private set; }

    /// <summary>
    /// リング設定。
    /// 0 = A, 1 = B, ..., 25 = Z
    /// </summary>
    public int RingSetting { get; }

    /// <summary>
    /// ローターの現在位置を文字で取得する。
    /// </summary>
    public char PositionLetter => (char)('A' + Position);

    public char RingSettingLetter =>
        (char)('A' + RingSetting);

    /// <summary>
    /// 現在ノッチ位置にあるか。
    /// </summary>
    public bool AtNotch =>
        Position == _notch;

    /// <summary>
    /// ローターを生成する。
    /// </summary>
    /// <param name="wiring">
    /// AlphabetSize文字のローター配線。
    /// 例: "EKMFLGDQVZNTOWYHXUSPAIBRCJ"
    /// </param>
    /// <param name="notch">
    /// ノッチ位置。
    /// </param>
    /// <param name="position">
    /// 初期ローター位置。
    /// 0=A ～ 25=Z
    /// </param>
    /// <param name="ringSetting">
    /// リング設定。
    /// 0=A ～ 25=Z
    /// </param>
    public Rotor(
        string wiring,
        char notch,
        char position = 'A',
        char ringSetting = 'A')
    {
        if (wiring.Length != AlphabetSize)
            throw new ArgumentException($"ローター配線は{AlphabetSize}文字である必要があります。", nameof(wiring));

        _wiring = ParseWiring(wiring);
        _inverseWiring = CreateInverseWiring(_wiring);

        _notch = ToIndex(notch);

        Position = ToIndex(position);
        RingSetting = ToIndex(ringSetting);
    }

    /// <summary>
    /// ローターを1ステップ回転させる。
    /// </summary>
    public void Step()
    {
        Position = (Position + 1) % AlphabetSize;
    }

    /// <summary>
    /// ローターを順方向に通過する。
    /// </summary>
    /// <param name="input">入力値 0～25</param>
    /// <returns>出力値 0～25</returns>
    public int Forward(int input)
    {
        ValidateInput(input);

        // ローター位置とリング設定を考慮してローター内部の位置へ変換
        int shiftedInput =
            Mod26(input + Position - RingSetting);

        // ローター配線を通す
        int wired = _wiring[shiftedInput];

        // 元の位置体系へ戻す
        int output =
            Mod26(wired - Position + RingSetting);

        return output;
    }

    /// <summary>
    /// ローターを逆方向に通過する。
    /// </summary>
    /// <param name="input">入力値 0～25</param>
    /// <returns>出力値 0～25</returns>
    public int Backward(int input)
    {
        ValidateInput(input);

        int shiftedInput =
            Mod26(input + Position - RingSetting);

        // 逆方向の配線を通す
        int wired = _inverseWiring[shiftedInput];

        int output =
            Mod26(wired - Position + RingSetting);

        return output;
    }

    /// <summary>
    /// ローター位置を設定する。
    /// </summary>
    public void SetPosition(int position)
    {
        if (position is < 0 or > 25)
            throw new ArgumentOutOfRangeException(nameof(position));

        Position = position;
    }

    /// <summary>
    /// ローター位置を設定する。
    /// </summary>
    public void SetPosition(char position)
    {
        Position = ToIndex(position);
    }

    private static int[] ParseWiring(string wiring)
    {
        if (wiring is null)
            throw new ArgumentNullException(nameof(wiring));

        if (wiring.Length != AlphabetSize)
            throw new ArgumentException(
                "ローター配線は26文字である必要があります。",
                nameof(wiring));

        int[] result = new int[AlphabetSize];
        bool[] used = new bool[AlphabetSize];

        for (int i = 0; i < AlphabetSize; i++)
        {
            int value = ToIndex(wiring[i]);

            if (used[value])
                throw new ArgumentException(
                    "ローター配線に同じ文字が複数存在します。",
                    nameof(wiring));

            used[value] = true;
            result[i] = value;
        }

        return result;
    }

    private static int[] CreateInverseWiring(int[] wiring)
    {
        int[] inverse = new int[AlphabetSize];

        for (int i = 0; i < AlphabetSize; i++)
        {
            inverse[wiring[i]] = i;
        }

        return inverse;
    }

    private static int ToIndex(char c)
    {
        c = char.ToUpperInvariant(c);

        if (c is < 'A' or > 'Z')
        {
            throw new ArgumentOutOfRangeException(
                nameof(c),
                "A～Zの範囲で指定してください。");
        }

        return c - 'A';
    }

    private static int Mod26(int value)
    {
        value %= AlphabetSize;

        if (value < 0)
            value += AlphabetSize;

        return value;
    }

    private static void ValidateInput(int input)
    {
        if (input is < 0 or >= AlphabetSize)
            throw new ArgumentOutOfRangeException(nameof(input));
    }
}