using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Cryptography
{
    [StructLayout(LayoutKind.Explicit)]
    public class Aes128
    {
        /// <summary>
        /// S-Box
        /// </summary>
        static byte[] SBox = new byte[256];
        static Aes128()
        {
            // S-Boxの初期化
            for (int i = 0; i < 256; i++)
            {
                byte x = (byte)i;
                byte y = (byte)(x ^ (x << 1) ^ (x << 2) ^ (x << 3) ^ (x << 4));
                SBox[i] = (byte)(y ^ ((y >> 8) & 0xFF) ^ 0x63);
            }
        }
        /// <summary>
        /// 状態行列
        /// - 初期状態では平文が入る
        /// - Stateは毎ラウンド書き換えられる作業領域
        /// </summary>
        [FieldOffset(0)]
        public byte[,] state = new byte[4, 4];
        /// <summary>
        /// 状態行列を1次元配列として扱う場合の作業領域
        /// </summary>
        [FieldOffset(0)]
        public byte[] stateArray = new byte[4 * 4];
        /// <summary>
        /// AES-128の暗号化を行います。
        /// </summary>
        public byte[] Encrypt(byte[] plainBytes, byte[] key)
        {
            var roundKeys = KeyExpansion(key);
            SetState(plainBytes);
            AddRoundKey(roundKeys.AsSpan(0, 16));

            // 9回繰り返し
            for (int i = 1; i <= 9; i++)
            {
                SubBytes();
                ShiftRows();
                MixColumns();
                AddRoundKey(roundKeys.AsSpan(i * 16, 16));
            }
            //最後
            {
                SubBytes();
                ShiftRows();
                AddRoundKey(roundKeys.AsSpan(10 * 16, 16));
            }
            return stateArray;
        }
        /// <summary>
        /// AES-128の復号化を行います。
        /// </summary>
        public byte[] Decrypt(byte[] cipherBytes, byte[] key)
        {
            var roundKeys = KeyExpansion(key);
            SetState(cipherBytes);
            AddRoundKey(roundKeys.AsSpan(0, 16));
            // 9回繰り返し
            for (int i = 9; i >= 1; i--)
            {
                // 逆の操作を行う
                // InverseShiftRows();
                // InverseSubBytes();
                AddRoundKey(roundKeys.AsSpan(i * 16, 16));
                // InverseMixColumns();
            }
            //最後
            {
                // InverseShiftRows();
                // InverseSubBytes();
                AddRoundKey(roundKeys.AsSpan(0, 16));
            }
            return stateArray;
        }
        /// <summary>
        /// 状態行列に平文をセットします。
        /// </summary>
        /// <param name="input"></param>
        public void SetState(byte[] input)
        {
            ///メモリーをコピーする
            Array.Copy(input, stateArray, stateArray.Length);
        }
        /// <summary>
        /// 状態とラウンド鍵をXORします。
        /// </summary>
        public void AddRoundKey(Span<byte> roundKey)
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    state[r, c] ^= roundKey[r * 4 + c];
                }
            }
        }
        static byte XTime(byte x)
        {
            return (byte)(
                (x & 0x80) != 0
                    ? ((x << 1) ^ 0x1B)
                    : (x << 1));
        }
        /// <summary>
        /// 状態行列の各バイトをS-Boxで置換します。
        /// </summary>
        public void SubBytes()
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    state[r, c] = SBox[state[r, c]];
                }
            }
        }
        /// <summary>
        /// 状態行列の各列をGalois Field GF(2^8)で乗算します。
        /// </summary>
        static byte Multiply(byte a, byte b)
        {
            byte result = 0;

            while (b != 0)
            {
                if ((b & 1) != 0)
                    result ^= a;

                bool carry = (a & 0x80) != 0;

                a <<= 1;

                if (carry)
                    a ^= 0x1B;

                b >>= 1;
            }

            return result;
        }

        public void ShiftRows()
        {
            for (int row = 1; row < 4; row++)
            {
                byte[] temp = new byte[4];

                for (int col = 0; col < 4; col++)
                    temp[col] = state[row, (col + row) % 4];

                for (int col = 0; col < 4; col++)
                    state[row, col] = temp[col];
            }
        }
        public byte Mul3(byte x)
        {
            return (byte)(XTime(x) ^ x);
        }
        public void MixColumns()
        {
            for (int c = 0; c < 4; c++)
            {
                byte s0 = state[0, c];
                byte s1 = state[1, c];
                byte s2 = state[2, c];
                byte s3 = state[3, c];

                state[0, c] = (byte)(XTime(s0) ^ Mul3(s1) ^ s2 ^ s3);

                state[1, c] = (byte)(s0 ^ XTime(s1) ^ Mul3(s2) ^ s3);

                state[2, c] = (byte)(s0 ^ s1 ^ XTime(s2) ^ Mul3(s3));

                state[3, c] = (byte)(Mul3(s0) ^ s1 ^ s2 ^ XTime(s3));
            }
        }
        #region RoundKey
        /// <summary>
        /// ラウンド定数
        /// </summary>
        static readonly byte[] Rcon =
        {
            0x01, 0x02, 0x04, 0x08,
            0x10, 0x20, 0x40, 0x80,
            0x1B, 0x36
        };
        static byte[] KeyExpansion(byte[] key)
        {
            uint[] w = new uint[44]; // 4 * (Nr + 1) words
            const int Nk = 4; // 128-bit key
            const int RoundsCount = 10; // Number of rounds
            const int Nb = 4; // Block size in words
            for (int i = 0; i < Nk; i++)
            {
                w[i] = BitConverter.ToUInt32(key, i * 4);
            }
            for (int i = Nk; i < Nb * (RoundsCount + 1); i++)
            {
                uint temp = w[i - 1];
                if (i % Nk == 0)
                {
                    temp = SubWord(RotWord(temp)) ^ ((uint)Rcon[i / Nk - 1] << 24);
                }
                w[i] = w[i - Nk] ^ temp;
            }
            return w.SelectMany(BitConverter.GetBytes).ToArray();
        }
        /// <summary>
        /// ワードを左に1バイト回転させる
        /// </summary>
        static uint RotWord(uint word)
        {
            return (word << 8) | (word >> 24);
        }
        /// <summary>
        /// S-Boxを使ってワードの各バイトを置換する
        /// </summary>
        static uint SubWord(uint word)
        {
            return
                ((uint)SBox[(word >> 24) & 0xFF] << 24) |
                ((uint)SBox[(word >> 16) & 0xFF] << 16) |
                ((uint)SBox[(word >> 8) & 0xFF] << 8) |
                SBox[word & 0xFF];
        }
        #endregion RoundKey
    }
}
