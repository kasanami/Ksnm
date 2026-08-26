using System.Reflection.PortableExecutable;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Ksnm.Cryptography.Enigma;

/// <summary>
/// 8bitのEnigma暗号機を表すクラス。
/// </summary>
public sealed class EnigmaMachine8
{
    private readonly Rotor8 _left;
    private readonly Rotor8 _middle;
    private readonly Rotor8 _right;
    private readonly Reflector8 _reflector;
    private readonly Plugboard8 _plugboard;

    private readonly int _initialLeftPosition;
    private readonly int _initialMiddlePosition;
    private readonly int _initialRightPosition;

    public string RotorPositions => $"{_left.Position}_{_middle.Position}_{_right.Position}";

    public EnigmaMachine8(
        Rotor8 left,
        Rotor8 middle,
        Rotor8 right,
        Reflector8 reflector,
        Plugboard8 plugboard)
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
    public void SetPositions(byte[] positions)
    {
        if (positions is null)
            throw new ArgumentNullException(nameof(positions));

        if (positions.Length != 3)
            throw new ArgumentException("ローター位置は3つで指定してください。", nameof(positions));

        _left.SetPosition(positions[0]);
        _middle.SetPosition(positions[1]);
        _right.SetPosition(positions[2]);
    }
    /// <summary>
    /// ローターの位置を初期状態にリセットする。
    /// </summary>
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
    /// <summary>
    /// 1バイトの値を暗号化する。
    /// </summary>
    public byte Encrypt(byte input)
    {
        // 暗号化する前にローターを回転
        StepRotors();

        byte value = input;

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

        return value;
    }
    public byte[] Encrypt(ReadOnlySpan<byte> input)
    {
        byte[] result = new byte[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            result[i] = Encrypt(input[i]);
        }

        return result;
    }
    public byte[] Encrypt(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        return Encrypt(bytes);
    }
    /// <summary>
    /// 復号する。Enigma暗号機は自己逆写像なので、暗号化と復号化は同じ処理で行える。
    /// </summary>
    public byte Decrypt(byte input)
    {
        return Encrypt(input);
    }
    /// <summary>
    /// 復号する。Enigma暗号機は自己逆写像なので、暗号化と復号化は同じ処理で行える。
    /// </summary>
    public byte[] Decrypt(ReadOnlySpan<byte> input)
    {
        return Encrypt(input);
    }
    public string DecryptToText(ReadOnlySpan<byte> input)
    {
        var bytes = Encrypt(input);
        return Encoding.UTF8.GetString(bytes);
    }
}