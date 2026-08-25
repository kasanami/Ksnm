namespace Ksnm.Cryptography.Enigma;

/// <summary>
/// Enigma暗号機を表すクラス。
/// </summary>
public sealed class EnigmaMachine
{
    private readonly Rotor _left;
    private readonly Rotor _middle;
    private readonly Rotor _right;
    private readonly Reflector _reflector;
    private readonly Plugboard _plugboard;

    private readonly int _initialLeftPosition;
    private readonly int _initialMiddlePosition;
    private readonly int _initialRightPosition;

    public string RotorPositions => $"{_left.PositionLetter}{_middle.PositionLetter}{_right.PositionLetter}";

    public EnigmaMachine(
        Rotor left,
        Rotor middle,
        Rotor right,
        Reflector reflector,
        Plugboard plugboard)
    {
        _left = left;
        _middle = middle;
        _right = right;
        _reflector = reflector;
        _plugboard = plugboard;

        _initialLeftPosition = left.Position;
        _initialMiddlePosition = middle.Position;
        _initialRightPosition = right.Position;
    }
    public void SetPositions(string positions)
    {
        if (positions is null)
            throw new ArgumentNullException(nameof(positions));

        if (positions.Length != 3)
            throw new ArgumentException(
                "ローター位置は3文字で指定してください。",
                nameof(positions));

        _left.SetPosition(positions[0]);
        _middle.SetPosition(positions[1]);
        _right.SetPosition(positions[2]);
    }

    public void Reset()
    {
        _left.SetPosition(_initialLeftPosition);
        _middle.SetPosition(_initialMiddlePosition);
        _right.SetPosition(_initialRightPosition);
    }

    private void StepRotors()
    {
        bool middleAtNotch = _middle.AtNotch;
        bool rightAtNotch = _right.AtNotch;

        // ダブルステッピング
        if (middleAtNotch)
        {
            _left.Step();
            _middle.Step();
        }
        else if (rightAtNotch)
        {
            _middle.Step();
        }

        // 右ローターは毎回必ず回転
        _right.Step();
    }

    public char Encrypt(char input)
    {
        input = char.ToUpperInvariant(input);

        if (input is < 'A' or > 'Z')
            throw new ArgumentException(
                "入力にはA～Zのみ使用できます。",
                nameof(input));

        // 暗号化する前にローターを回転
        StepRotors();

        int value = input - 'A';

        // プラグボード
        value = _plugboard.Transform(value);

        // 順方向
        value = _right.Forward(value);
        value = _middle.Forward(value);
        value = _left.Forward(value);

        // リフレクター
        value = _reflector.Reflect(value);

        // 逆方向
        value = _left.Backward(value);
        value = _middle.Backward(value);
        value = _right.Backward(value);

        // プラグボード
        value = _plugboard.Transform(value);

        return (char)('A' + value);
    }

    public string Encrypt(string text)
    {
        char[] result = new char[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (c is >= 'a' and <= 'z')
                c = char.ToUpperInvariant(c);

            if (c is < 'A' or > 'Z')
            {
                result[i] = c;
                continue;
            }

            result[i] = Encrypt(c);
        }

        return new string(result);
    }
}