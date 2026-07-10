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
        /// S-Box(substitution box)
        /// </summary>
        static readonly byte[] SBox =
        {
            0x63,0x7C,0x77,0x7B,0xF2,0x6B,0x6F,0xC5,
            0x30,0x01,0x67,0x2B,0xFE,0xD7,0xAB,0x76,

            0xCA,0x82,0xC9,0x7D,0xFA,0x59,0x47,0xF0,
            0xAD,0xD4,0xA2,0xAF,0x9C,0xA4,0x72,0xC0,

            0xB7,0xFD,0x93,0x26,0x36,0x3F,0xF7,0xCC,
            0x34,0xA5,0xE5,0xF1,0x71,0xD8,0x31,0x15,

            0x04,0xC7,0x23,0xC3,0x18,0x96,0x05,0x9A,
            0x07,0x12,0x80,0xE2,0xEB,0x27,0xB2,0x75,

            0x09,0x83,0x2C,0x1A,0x1B,0x6E,0x5A,0xA0,
            0x52,0x3B,0xD6,0xB3,0x29,0xE3,0x2F,0x84,

            0x53,0xD1,0x00,0xED,0x20,0xFC,0xB1,0x5B,
            0x6A,0xCB,0xBE,0x39,0x4A,0x4C,0x58,0xCF,

            0xD0,0xEF,0xAA,0xFB,0x43,0x4D,0x33,0x85,
            0x45,0xF9,0x02,0x7F,0x50,0x3C,0x9F,0xA8,

            0x51,0xA3,0x40,0x8F,0x92,0x9D,0x38,0xF5,
            0xBC,0xB6,0xDA,0x21,0x10,0xFF,0xF3,0xD2,

            0xCD,0x0C,0x13,0xEC,0x5F,0x97,0x44,0x17,
            0xC4,0xA7,0x7E,0x3D,0x64,0x5D,0x19,0x73,

            0x60,0x81,0x4F,0xDC,0x22,0x2A,0x90,0x88,
            0x46,0xEE,0xB8,0x14,0xDE,0x5E,0x0B,0xDB,

            0xE0,0x32,0x3A,0x0A,0x49,0x06,0x24,0x5C,
            0xC2,0xD3,0xAC,0x62,0x91,0x95,0xE4,0x79,

            0xE7,0xC8,0x37,0x6D,0x8D,0xD5,0x4E,0xA9,
            0x6C,0x56,0xF4,0xEA,0x65,0x7A,0xAE,0x08,

            0xBA,0x78,0x25,0x2E,0x1C,0xA6,0xB4,0xC6,
            0xE8,0xDD,0x74,0x1F,0x4B,0xBD,0x8B,0x8A,

            0x70,0x3E,0xB5,0x66,0x48,0x03,0xF6,0x0E,
            0x61,0x35,0x57,0xB9,0x86,0xC1,0x1D,0x9E,

            0xE1,0xF8,0x98,0x11,0x69,0xD9,0x8E,0x94,
            0x9B,0x1E,0x87,0xE9,0xCE,0x55,0x28,0xDF,

            0x8C,0xA1,0x89,0x0D,0xBF,0xE6,0x42,0x68,
            0x41,0x99,0x2D,0x0F,0xB0,0x54,0xBB,0x16
        };
        static Aes128()
        {
            // S-Boxの初期化
            //for (int i = 0; i < 256; i++)
            //{
            //    byte x = (byte)i;
            //    byte y = (byte)(x ^ (x << 1) ^ (x << 2) ^ (x << 3) ^ (x << 4));
            //    SBox[i] = (byte)(y ^ ((y >> 8) & 0xFF) ^ 0x63);
            //}
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
        public static byte Multiply(byte a, byte b)
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
        public static uint RotWord(uint word)
        {
            return (word << 8) | (word >> 24);
        }
        /// <summary>
        /// S-Boxを使ってワードの各バイトを置換する
        /// </summary>
        public static uint SubWord(uint word)
        {
            return
                ((uint)SBox[(word >> 24) & 0xFF] << 24) |
                ((uint)SBox[(word >> 16) & 0xFF] << 16) |
                ((uint)SBox[(word >> 8) & 0xFF] << 8) |
                SBox[word & 0xFF];
        }
        public static uint SubWord(ReadOnlySpan<byte> word)
        {
            return 
                (uint)SBox[word[3]] << 24 |
                (uint)SBox[word[2]] << 16 |
                (uint)SBox[word[1]] << 8 |
                (uint)SBox[word[0]];
        }
        #endregion RoundKey
    }
}
