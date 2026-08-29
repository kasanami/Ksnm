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
    }
    /// <summary>
    /// すべてのローター、リフレクター、プラグボードをランダムに生成する。
    /// </summary>
    /// <param name="random"></param>
    public EnigmaMachine8(Random random)
    {
        _left = new Rotor8(random);
        _middle = new Rotor8(random);
        _right = new Rotor8(random);
        _reflector = new Reflector8(random);
        _plugboard = new Plugboard8(random);
    }
    /// <summary>
    /// ローターの位置を設定する。
    /// ※初期位置は変更されません。ローターの位置を変更するだけです。
    /// </summary>
    /// <param name="positions"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void SetRotors(byte left, byte middle, byte right)
    {
        _left.SetPosition(left);
        _middle.SetPosition(middle);
        _right.SetPosition(right);
    }
    /// <summary>
    /// ローターの位置を初期状態にリセットする。
    /// </summary>
    public void ResetRotors()
    {
        _left.Reset();
        _middle.Reset();
        _right.Reset();
    }
    /// <summary>
    /// ローターを回転させる。
    /// </summary>
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
    /// <summary>
    /// 複数バイトの値を暗号化する。
    /// </summary>
    /// <param name="input"></param>
    /// <param name="resetRotors"></param>
    /// <returns></returns>
    public byte[] Encrypt(ReadOnlySpan<byte> input, bool resetRotors = true)
    {
        if (resetRotors)
        {
            ResetRotors();
        }

        byte[] result = new byte[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            result[i] = Encrypt(input[i]);
        }

        return result;
    }
    /// <summary>
    /// 文字列を暗号化する。
    /// EncodingはUTF-8を使用する。
    /// </summary>
    /// <param name="text"></param>
    /// <param name="resetRotors"></param>
    /// <returns></returns>
    public byte[] Encrypt(string text, bool resetRotors = true)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        return Encrypt(bytes, resetRotors);
    }
    /// <summary>
    /// 復号する。
    /// </summary>
    public byte Decrypt(byte input)
    {
        // Enigma暗号機は自己逆写像なので、暗号化と復号化は同じ処理で行える。
        return Encrypt(input);
    }
    /// <summary>
    /// 復号する。
    /// </summary>
    public byte[] Decrypt(ReadOnlySpan<byte> input, bool resetRotors = true)
    {
        // Enigma暗号機は自己逆写像なので、暗号化と復号化は同じ処理で行える。
        return Encrypt(input);
    }
    /// <summary>
    /// 復号して文字列に変換する。
    /// EncodingはUTF-8を使用する。
    /// </summary>
    public string DecryptToText(ReadOnlySpan<byte> input, bool resetRotors = true)
    {
        var bytes = Encrypt(input, resetRotors);
        return Encoding.UTF8.GetString(bytes);
    }
}