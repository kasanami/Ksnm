namespace Ksnm.Cryptography.Enigma;

/// <summary>
/// 8bit版のローター
/// </summary>
public sealed class Rotor8
{
    private const int Size = 256;

    /// <summary>
    /// ローターの配線。
    /// wiring[i] は、ローター内部で入力 i がどの出力へ接続されるかを表す。
    /// </summary>
    private readonly byte[] _wiring;

    /// <summary>
    /// 逆方向の配線。
    /// </summary>
    private readonly byte[] _inverseWiring;

    /// <summary>
    /// ノッチ位置。
    /// この位置で次のローターを進める。
    /// </summary>
    private readonly int _notch;

    /// <summary>
    /// ローターの現在位置。
    /// </summary>
    public int Position { get; private set; }

    /// <summary>
    /// リング設定。
    /// </summary>
    public int RingSetting { get; }

    /// <summary>
    /// 現在ノッチ位置にあるか。
    /// </summary>
    public bool AtNotch => Position == _notch;

    /// <summary>
    /// ローターを生成する。
    /// </summary>
    /// <param name="wiring">ローター配線。</param>
    /// <param name="notch">ノッチ位置。</param>
    /// <param name="position">初期ローター位置。</param>
    /// <param name="ringSetting">リング設定。</param>
    public Rotor8(
        ReadOnlySpan<byte> wiring,
        byte notch,
        byte position = 0,
        byte ringSetting = 0)
    {
        if (wiring.Length != Size)
            throw new ArgumentException($"ローター配線は{Size}文字である必要があります。", nameof(wiring));

        _wiring = ParseWiring(wiring);
        _inverseWiring = CreateInverseWiring(_wiring);

        _notch = notch;

        Position = position;
        RingSetting = ringSetting;
    }
    /// <summary>
    /// ランダムなローターを生成する。
    /// </summary>
    public Rotor8(Random random)
    {
        _wiring = new byte[Size];
        
        for (int i = 0; i < Size; i++)
        {
            _wiring[i] = (byte)random.Next(Size);
        }

        _inverseWiring = CreateInverseWiring(_wiring);
        _notch = random.Next(Size);
        Position = random.Next(Size);
        RingSetting = random.Next(Size);
    }

    /// <summary>
    /// ローターを1ステップ回転させる。
    /// </summary>
    public void Step()
    {
        Position = (Position + 1) % Size;
    }

    /// <summary>
    /// ローターを順方向に通過する。
    /// </summary>
    /// <param name="input">入力値 0～255</param>
    /// <returns>出力値 0～255</returns>
    public byte Forward(byte input)
    {
        // ローター位置とリング設定を考慮してローター内部の位置へ変換
        byte shiftedInput = Mod256(input + Position - RingSetting);

        // ローター配線を通す
        byte wired = _wiring[shiftedInput];

        // 元の位置体系へ戻す
        byte output = Mod256(wired - Position + RingSetting);

        return output;
    }

    /// <summary>
    /// ローターを逆方向に通過する。
    /// </summary>
    /// <param name="input">入力値 0～255</param>
    /// <returns>出力値 0～255</returns>
    public byte Backward(byte input)
    {
        byte shiftedInput = Mod256(input + Position - RingSetting);

        // 逆方向の配線を通す
        byte wired = _inverseWiring[shiftedInput];

        byte output = Mod256(wired - Position + RingSetting);

        return output;
    }

    /// <summary>
    /// ローター位置を設定する。
    /// </summary>
    public void SetPosition(int position)
    {
        if (position is < 0 or >= Size)
            throw new ArgumentOutOfRangeException(nameof(position));

        Position = position;
    }

    /// <summary>
    /// ローター位置を設定する。
    /// </summary>
    public void SetPosition(byte position)
    {
        Position = position;
    }

    private static byte[] ParseWiring(ReadOnlySpan<byte> wiring)
    {
        if (wiring.IsEmpty)
            throw new ArgumentNullException(nameof(wiring));

        if (wiring.Length != Size)
            throw new ArgumentException($"ローター配線は{Size}文字である必要があります。", nameof(wiring));

        byte[] result = new byte[Size];
        bool[] used = new bool[Size];

        for (int i = 0; i < Size; i++)
        {
            byte value = wiring[i];

            if (used[value])
                throw new ArgumentException("ローター配線に同じ文字が複数存在します。", nameof(wiring));

            used[value] = true;
            result[i] = value;
        }

        return result;
    }

    private static byte[] CreateInverseWiring(byte[] wiring)
    {
        byte[] inverse = new byte[Size];

        for (int i = 0; i < Size; i++)
        {
            inverse[wiring[i]] = (byte)i;
        }

        return inverse;
    }

    private static byte Mod256(int value)
    {
        value %= Size;

        if (value < 0)
            value += Size;

        return (byte)value;
    }
}