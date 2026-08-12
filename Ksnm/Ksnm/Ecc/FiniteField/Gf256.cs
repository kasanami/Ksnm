namespace Ksnm.Ecc.FiniteField;

/// <summary>
/// GF(2^8)の有限体を表す構造体です。
/// </summary>
public readonly struct Gf256
{
    #region 定数
    /// <summary>
    /// GF(2^8)の指数表
    /// </summary>
    private static readonly byte[] ExpTable;
    /// <summary>
    /// GF(2^8)の対数表
    /// </summary>
    private static readonly byte[] LogTable;
    /// <summary>
    /// 静的コンストラクタ
    /// 指数表と対数表を初期化します。
    /// </summary>
    static Gf256()
    {
        ExpTable = CreateExpTable();
        LogTable = CreateLogTable(ExpTable);
    }
    private static byte[] CreateExpTable()
    {
        var table = new byte[512];

        // α^0 = 1
        byte value = 1;

        for (int i = 0; i < 255; i++)
        {
            table[i] = value;

            // 次のαへ
            value = MultiplyByAlpha(value);
        }

        // 255以降は繰り返しコピー
        for (int i = 255; i < table.Length; i++)
        {
            table[i] = table[i - 255];
        }

        return table;
    }
    private static byte MultiplyByAlpha(byte value)
    {
        int result = value << 1;

        if ((result & 0x100) != 0)
        {
            result ^= 0x11D;
        }

        return (byte)result;
    }
    private static byte[] CreateLogTable(byte[] expTable)
    {
        var table = new byte[256];

        for (int i = 0; i < 255; i++)
        {
            table[expTable[i]] = (byte)i;
        }

        return table;
    }
    #endregion 定数

    public byte Value { get; }

    public Gf256(byte value)
    {
        Value = value;
    }

    public override string ToString()
        => Value.ToString();
    #region 演算子
    public static implicit operator Gf256(byte value)
        => new(value);

    public static implicit operator byte(Gf256 value)
        => value.Value;

    public static Gf256 operator +(Gf256 left, Gf256 right)
    {
        return new((byte)(left.Value ^ right.Value));
    }
    public static Gf256 operator -(Gf256 left, Gf256 right)
    {
        return left + right;
    }
    public static Gf256 operator *(Gf256 left, Gf256 right)
    {
        if (left.Value == 0 || right.Value == 0)
        {
            return new Gf256(0);
        }

        int log = LogTable[left.Value] + LogTable[right.Value];

        return new Gf256(ExpTable[log]);
    }
    #endregion 演算子
}