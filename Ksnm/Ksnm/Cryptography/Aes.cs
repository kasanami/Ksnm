using System;
using System.Buffers.Binary;
using System.IO;

namespace Ksnm.Cryptography
{
    /// <summary>
    /// ECB/CBCヘルパーとPKCS7パディングを用いたAESの実装。
    /// 
    /// Notes:
    /// - 128/192/256ビットのキーをサポートします。
    /// - 学習、相互運用性、および管理された環境を目的としています。
    /// - セキュリティ上重要な用途では、検証済みのライブラリやプラットフォームの暗号化APIを使用することを推奨します。
    /// </summary>
    public sealed class Aes
    {
        #region 定数
        /// <summary>
        /// AESブロックサイズ（バイト単位）。AESは128ビット（16バイト）のブロックを使用します。
        /// </summary>
        private const int BlockSize = 16;
        /// <summary>
        /// AES S-Box（Substitution Box）。AESのSubBytesステップで使用される非線形置換テーブルです。
        /// </summary>
        private static readonly byte[] SBox = new byte[256]
        {
            0x63,0x7C,0x77,0x7B,0xF2,0x6B,0x6F,0xC5,0x30,0x01,0x67,0x2B,0xFE,0xD7,0xAB,0x76,
            0xCA,0x82,0xC9,0x7D,0xFA,0x59,0x47,0xF0,0xAD,0xD4,0xA2,0xAF,0x9C,0xA4,0x72,0xC0,
            0xB7,0xFD,0x93,0x26,0x36,0x3F,0xF7,0xCC,0x34,0xA5,0xE5,0xF1,0x71,0xD8,0x31,0x15,
            0x04,0xC7,0x23,0xC3,0x18,0x96,0x05,0x9A,0x07,0x12,0x80,0xE2,0xEB,0x27,0xB2,0x75,
            0x09,0x83,0x2C,0x1A,0x1B,0x6E,0x5A,0xA0,0x52,0x3B,0xD6,0xB3,0x29,0xE3,0x2F,0x84,
            0x53,0xD1,0x00,0xED,0x20,0xFC,0xB1,0x5B,0x6A,0xCB,0xBE,0x39,0x4A,0x4C,0x58,0xCF,
            0xD0,0xEF,0xAA,0xFB,0x43,0x4D,0x33,0x85,0x45,0xF9,0x02,0x7F,0x50,0x3C,0x9F,0xA8,
            0x51,0xA3,0x40,0x8F,0x92,0x9D,0x38,0xF5,0xBC,0xB6,0xDA,0x21,0x10,0xFF,0xF3,0xD2,
            0xCD,0x0C,0x13,0xEC,0x5F,0x97,0x44,0x17,0xC4,0xA7,0x7E,0x3D,0x64,0x5D,0x19,0x73,
            0x60,0x81,0x4F,0xDC,0x22,0x2A,0x90,0x88,0x46,0xEE,0xB8,0x14,0xDE,0x5E,0x0B,0xDB,
            0xE0,0x32,0x3A,0x0A,0x49,0x06,0x24,0x5C,0xC2,0xD3,0xAC,0x62,0x91,0x95,0xE4,0x79,
            0xE7,0xC8,0x37,0x6D,0x8D,0xD5,0x4E,0xA9,0x6C,0x56,0xF4,0xEA,0x65,0x7A,0xAE,0x08,
            0xBA,0x78,0x25,0x2E,0x1C,0xA6,0xB4,0xC6,0xE8,0xDD,0x74,0x1F,0x4B,0xBD,0x8B,0x8A,
            0x70,0x3E,0xB5,0x66,0x48,0x03,0xF6,0x0E,0x61,0x35,0x57,0xB9,0x86,0xC1,0x1D,0x9E,
            0xE1,0xF8,0x98,0x11,0x69,0xD9,0x8E,0x94,0x9B,0x1E,0x87,0xE9,0xCE,0x55,0x28,0xDF,
            0x8C,0xA1,0x89,0x0D,0xBF,0xE6,0x42,0x68,0x41,0x99,0x2D,0x0F,0xB0,0x54,0xBB,0x16
        };
        /// <summary>
        /// AES逆S-Box（Inverse Substitution Box）。AESのInvSubBytesステップで使用される非線形置換テーブルです。
        /// </summary>
        private static readonly byte[] InvSBox = new byte[256]
        {
            0x52,0x09,0x6A,0xD5,0x30,0x36,0xA5,0x38,0xBF,0x40,0xA3,0x9E,0x81,0xF3,0xD7,0xFB,
            0x7C,0xE3,0x39,0x82,0x9B,0x2F,0xFF,0x87,0x34,0x8E,0x43,0x44,0xC4,0xDE,0xE9,0xCB,
            0x54,0x7B,0x94,0x32,0xA6,0xC2,0x23,0x3D,0xEE,0x4C,0x95,0x0B,0x42,0xFA,0xC3,0x4E,
            0x08,0x2E,0xA1,0x66,0x28,0xD9,0x24,0xB2,0x76,0x5B,0xA2,0x49,0x6D,0x8B,0xD1,0x25,
            0x72,0xF8,0xF6,0x64,0x86,0x68,0x98,0x16,0xD4,0xA4,0x5C,0xCC,0x5D,0x65,0xB6,0x92,
            0x6C,0x70,0x48,0x50,0xFD,0xED,0xB9,0xDA,0x5E,0x15,0x46,0x57,0xA7,0x8D,0x9D,0x84,
            0x90,0xD8,0xAB,0x00,0x8C,0xBC,0xD3,0x0A,0xF7,0xE4,0x58,0x05,0xB8,0xB3,0x45,0x06,
            0xD0,0x2C,0x1E,0x8F,0xCA,0x3F,0x0F,0x02,0xC1,0xAF,0xBD,0x03,0x01,0x13,0x8A,0x6B,
            0x3A,0x91,0x11,0x41,0x4F,0x67,0xDC,0xEA,0x97,0xF2,0xCF,0xCE,0xF0,0xB4,0xE6,0x73,
            0x96,0xAC,0x74,0x22,0xE7,0xAD,0x35,0x85,0xE2,0xF9,0x37,0xE8,0x1C,0x75,0xDF,0x6E,
            0x47,0xF1,0x1A,0x71,0x1D,0x29,0xC5,0x89,0x6F,0xB7,0x62,0x0E,0xAA,0x18,0xBE,0x1B,
            0xFC,0x56,0x3E,0x4B,0xC6,0xD2,0x79,0x20,0x9A,0xDB,0xC0,0xFE,0x78,0xCD,0x5A,0xF4,
            0x1F,0xDD,0xA8,0x33,0x88,0x07,0xC7,0x31,0xB1,0x12,0x10,0x59,0x27,0x80,0xEC,0x5F,
            0x60,0x51,0x7F,0xA9,0x19,0xB5,0x4A,0x0D,0x2D,0xE5,0x7A,0x9F,0x93,0xC9,0x9C,0xEF,
            0xA0,0xE0,0x3B,0x4D,0xAE,0x2A,0xF5,0xB0,0xC8,0xEB,0xBB,0x3C,0x83,0x53,0x99,0x61,
            0x17,0x2B,0x04,0x7E,0xBA,0x77,0xD6,0x26,0xE1,0x69,0x14,0x63,0x55,0x21,0x0C,0x7D
        };
        /// <summary>
        /// AESのラウンド定数（Rcon）。キー拡張で使用される定数配列です。
        /// </summary>
        private static readonly byte[] Rcon = new byte[]
        {
            0x00, // unused
            0x01,0x02,0x04,0x08,0x10,0x20,0x40,0x80,0x1B,0x36,
            0x6C,0xD8,0xAB,0x4D,0x9A
        };
        #endregion 定数
        #region フィールド
        /// <summary>
        /// AESキーのワード数（32ビット単位）。AES-128では4、AES-192では6、AES-256では8です。
        /// </summary>
        private readonly int _KeyWordsCount;
        private readonly int _RoundsCount;
        private readonly uint[] _roundKeys; // expanded key schedule words
        #endregion フィールド
        /// <summary>
        /// AES暗号化/復号化のインスタンスを初期化します。
        /// </summary>
        /// <param name="key">AESキー</param>
        /// <exception cref="ArgumentException"></exception>
        public Aes(ReadOnlySpan<byte> key)
        {
            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            {
                throw new ArgumentException($"{nameof(key)}の長さは、16、24、または32バイトでなければなりません。", nameof(key));
            }

            _KeyWordsCount = key.Length / 4;
            _RoundsCount = _KeyWordsCount + 6;
            _roundKeys = ExpandKey(key);
        }
        /// <summary>
        /// AESをECBモードで暗号化します。必要に応じてPKCS7パディングを適用します。
        /// </summary>
        /// <param name="plainBytes">暗号化するデータ</param>
        /// <param name="usePkcs7Padding">PKCS7パディングを使用するかどうか</param>
        /// <returns>暗号化されたデータ</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public byte[] EncryptEcb(byte[] plainBytes, bool usePkcs7Padding = true)
        {
            if (plainBytes is null)
            {
                throw new ArgumentNullException(nameof(plainBytes));
            }
            byte[] input = usePkcs7Padding ? ApplyPkcs7Padding(plainBytes) : RequireBlockAlignedCopy(plainBytes);
            byte[] output = new byte[input.Length];

            Span<byte> blockIn = stackalloc byte[BlockSize];
            Span<byte> blockOut = stackalloc byte[BlockSize];

            for (int offset = 0; offset < input.Length; offset += BlockSize)
            {
                input.AsSpan(offset, BlockSize).CopyTo(blockIn);
                EncryptBlock(blockIn, blockOut);
                blockOut.CopyTo(output.AsSpan(offset, BlockSize));
            }

            return output;
        }
        /// <summary>
        /// AESをECBモードで復号します。必要に応じてPKCS7パディングを削除します。
        /// </summary>
        /// <param name="encryptedBytes">復号する暗号データ</param>
        /// <param name="usePkcs7Padding">PKCS7パディングを使用するかどうか</param>
        /// <returns>復号されたデータ</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public byte[] DecryptEcb(byte[] encryptedBytes, bool usePkcs7Padding = true)
        {
            if (encryptedBytes is null)
            {
                throw new ArgumentNullException(nameof(encryptedBytes));
            }
            if (encryptedBytes.Length % BlockSize != 0)
            {
                throw new ArgumentException("暗号データの長さは、16バイトの倍数でなければなりません。", nameof(encryptedBytes));
            }

            byte[] output = new byte[encryptedBytes.Length];
            Span<byte> blockIn = stackalloc byte[BlockSize];
            Span<byte> blockOut = stackalloc byte[BlockSize];

            for (int offset = 0; offset < encryptedBytes.Length; offset += BlockSize)
            {
                encryptedBytes.AsSpan(offset, BlockSize).CopyTo(blockIn);
                DecryptBlock(blockIn, blockOut);
                blockOut.CopyTo(output.AsSpan(offset, BlockSize));
            }

            return usePkcs7Padding ? RemovePkcs7Padding(output) : output;
        }
        /// <summary>
        /// AESをCBCモードで暗号化します。必要に応じてPKCS7パディングを適用します。
        /// </summary>
        /// <param name="plainBytes">暗号化するデータ</param>
        /// <param name="iv">初期化ベクトル</param>
        /// <param name="usePkcs7Padding">PKCS7パディングを使用するかどうか</param>
        /// <returns>暗号化されたデータ</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public byte[] EncryptCbc(byte[] plainBytes, byte[] iv, bool usePkcs7Padding = true)
        {
            if (plainBytes is null)
            {
                throw new ArgumentNullException(nameof(plainBytes));
            }
            ValidateIv(iv);

            byte[] input = usePkcs7Padding ? ApplyPkcs7Padding(plainBytes) : RequireBlockAlignedCopy(plainBytes);
            byte[] output = new byte[input.Length];

            Span<byte> prev = stackalloc byte[BlockSize];
            iv.AsSpan(0, BlockSize).CopyTo(prev);

            Span<byte> block = stackalloc byte[BlockSize];
            Span<byte> enc = stackalloc byte[BlockSize];

            for (int offset = 0; offset < input.Length; offset += BlockSize)
            {
                input.AsSpan(offset, BlockSize).CopyTo(block);
                XorInPlace(block, prev);
                EncryptBlock(block, enc);
                enc.CopyTo(output.AsSpan(offset, BlockSize));
                enc.CopyTo(prev);
            }

            return output;
        }
        /// <summary>
        /// AESをCBCモードで復号します。必要に応じてPKCS7パディングを削除します。
        /// </summary>
        /// <param name="encryptedBytes">復号する暗号データ</param>
        /// <param name="iv">初期化ベクトル</param>
        /// <param name="usePkcs7Padding">PKCS7パディングを使用するかどうか</param>
        /// <returns>復号されたデータ</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public byte[] DecryptCbc(byte[] encryptedBytes, byte[] iv, bool usePkcs7Padding = true)
        {
            if (encryptedBytes is null)
            {
                throw new ArgumentNullException(nameof(encryptedBytes));
            }
            if (encryptedBytes.Length % BlockSize != 0)
            {
                throw new ArgumentException("Encrypted bytes length must be a multiple of 16 bytes.", nameof(encryptedBytes));
            }
            ValidateIv(iv);

            byte[] output = new byte[encryptedBytes.Length];
            Span<byte> prev = stackalloc byte[BlockSize];
            iv.AsSpan(0, BlockSize).CopyTo(prev);

            Span<byte> block = stackalloc byte[BlockSize];
            Span<byte> dec = stackalloc byte[BlockSize];
            Span<byte> currentCipher = stackalloc byte[BlockSize];

            for (int offset = 0; offset < encryptedBytes.Length; offset += BlockSize)
            {
                encryptedBytes.AsSpan(offset, BlockSize).CopyTo(currentCipher);
                currentCipher.CopyTo(block);
                DecryptBlock(block, dec);
                XorInPlace(dec, prev);
                dec.CopyTo(output.AsSpan(offset, BlockSize));
                currentCipher.CopyTo(prev);
            }

            return usePkcs7Padding ? RemovePkcs7Padding(output) : output;
        }
        /// <summary>
        /// AESの1ブロック（16バイト）を暗号化します。入力と出力のバッファは16バイトである必要があります。
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <exception cref="ArgumentException"></exception>
        public void EncryptBlock(ReadOnlySpan<byte> input, Span<byte> output)
        {
            if (input.Length < BlockSize)
            {
                throw new ArgumentException("入力ブロックのサイズは16バイトでなければなりません。", nameof(input));
            }
            if (output.Length < BlockSize)
            {
                throw new ArgumentException("出力ブロックのサイズは16バイトでなければなりません。", nameof(output));
            }

            Span<byte> state = stackalloc byte[BlockSize];
            input.Slice(0, BlockSize).CopyTo(state);

            AddRoundKey(state, 0);

            for (int round = 1; round < _RoundsCount; round++)
            {
                SubBytes(state);
                ShiftRows(state);
                MixColumns(state);
                AddRoundKey(state, round);
            }

            SubBytes(state);
            ShiftRows(state);
            AddRoundKey(state, _RoundsCount);

            state.CopyTo(output);
        }
        /// <summary>
        /// AESの1ブロック（16バイト）を復号します。入力と出力のバッファは16バイトである必要があります。
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <exception cref="ArgumentException"></exception>
        public void DecryptBlock(ReadOnlySpan<byte> input, Span<byte> output)
        {
            if (input.Length < BlockSize)
            {
                throw new ArgumentException("入力ブロックのサイズは16バイトでなければなりません。", nameof(input));
            }
            if (output.Length < BlockSize)
            {
                throw new ArgumentException("出力ブロックのサイズは16バイトでなければなりません。", nameof(output));
            }

            Span<byte> state = stackalloc byte[BlockSize];
            input.Slice(0, BlockSize).CopyTo(state);

            AddRoundKey(state, _RoundsCount);

            for (int round = _RoundsCount - 1; round >= 1; round--)
            {
                InvShiftRows(state);
                InvSubBytes(state);
                AddRoundKey(state, round);
                InvMixColumns(state);
            }

            InvShiftRows(state);
            InvSubBytes(state);
            AddRoundKey(state, 0);

            state.CopyTo(output);
        }
        /// <summary>
        /// AESキーを拡張してラウンドキーを生成します。キー拡張アルゴリズムに従って、元のキーから必要なラウンドキーを計算します。
        /// </summary>
        private uint[] ExpandKey(ReadOnlySpan<byte> key)
        {
            int wordsLength = 4 * (_RoundsCount + 1);
            uint[] words = new uint[wordsLength];

            for (int i = 0; i < _KeyWordsCount; i++)
            {
                words[i] = BinaryPrimitives.ReadUInt32BigEndian(key.Slice(i * 4, 4));
            }

            for (int i = _KeyWordsCount; i < wordsLength; i++)
            {
                uint temp = words[i - 1];

                if (i % _KeyWordsCount == 0)
                {
                    temp = SubWord(RotWord(temp)) ^ ((uint)Rcon[i / _KeyWordsCount] << 24);
                }
                else if (_KeyWordsCount > 6 && i % _KeyWordsCount == 4)
                {
                    temp = SubWord(temp);
                }

                words[i] = words[i - _KeyWordsCount] ^ temp;
            }

            return words;
        }
        /// <summary>
        /// 指定されたラウンドのラウンドキーを状態に加えます。
        /// AddRoundKeyステップは、状態とラウンドキーのビットごとのXORを行います。
        /// </summary>
        /// <param name="state"></param>
        /// <param name="round"></param>
        private void AddRoundKey(Span<byte> state, int round)
        {
            int wordOffset = round * 4;
            for (int c = 0; c < 4; c++)
            {
                uint k = _roundKeys[wordOffset + c];
                state[c * 4 + 0] ^= (byte)(k >> 24);
                state[c * 4 + 1] ^= (byte)(k >> 16);
                state[c * 4 + 2] ^= (byte)(k >> 8);
                state[c * 4 + 3] ^= (byte)k;
            }
        }
        /// <summary>
        /// AESのSubBytesステップを実行します。
        /// 状態の各バイトをS-Boxを使用して置換します。
        /// </summary>
        /// <param name="state"></param>
        private static void SubBytes(Span<byte> state)
        {
            for (int i = 0; i < BlockSize; i++)
                state[i] = SBox[state[i]];
        }
        /// <summary>
        /// AESのInvSubBytesステップを実行します。
        /// </summary>
        /// <param name="state"></param>
        private static void InvSubBytes(Span<byte> state)
        {
            for (int i = 0; i < BlockSize; i++)
                state[i] = InvSBox[state[i]];
        }
        /// <summary>
        /// AESのShiftRowsステップを実行します。
        /// 状態の各行を左にシフトします。
        /// ※AESのステートレイアウトは列優先です。
        /// </summary>
        /// <param name="state"></param>
        private static void ShiftRows(Span<byte> state)
        {
            Span<byte> t = stackalloc byte[BlockSize];
            state.CopyTo(t);

            t[0] = state[00]; t[4] = state[04]; t[08] = state[08]; t[12] = state[12];
            t[1] = state[05]; t[5] = state[09]; t[09] = state[13]; t[13] = state[01];
            t[2] = state[10]; t[6] = state[14]; t[10] = state[02]; t[14] = state[06];
            t[3] = state[15]; t[7] = state[03]; t[11] = state[07]; t[15] = state[11];

            t.CopyTo(state);
        }
        /// <summary>
        /// AESのInvShiftRowsステップを実行します。
        /// 状態の各行を右にシフトします。
        /// </summary>
        /// <param name="state"></param>
        private static void InvShiftRows(Span<byte> state)
        {
            Span<byte> t = stackalloc byte[BlockSize];
            state.CopyTo(t);

            t[0] = state[00]; t[4] = state[04]; t[08] = state[08]; t[12] = state[12];
            t[1] = state[13]; t[5] = state[01]; t[09] = state[05]; t[13] = state[09];
            t[2] = state[10]; t[6] = state[14]; t[10] = state[02]; t[14] = state[06];
            t[3] = state[07]; t[7] = state[11]; t[11] = state[15]; t[15] = state[03];

            t.CopyTo(state);
        }
        /// <summary>
        /// AESのMixColumnsステップを実行します。
        /// </summary>
        /// <param name="state"></param>
        private static void MixColumns(Span<byte> state)
        {
            for (int c = 0; c < 4; c++)
            {
                int i = c * 4;
                byte a0 = state[i + 0];
                byte a1 = state[i + 1];
                byte a2 = state[i + 2];
                byte a3 = state[i + 3];

                state[i + 0] = (byte)(Mul2(a0) ^ Mul3(a1) ^ a2 ^ a3);
                state[i + 1] = (byte)(a0 ^ Mul2(a1) ^ Mul3(a2) ^ a3);
                state[i + 2] = (byte)(a0 ^ a1 ^ Mul2(a2) ^ Mul3(a3));
                state[i + 3] = (byte)(Mul3(a0) ^ a1 ^ a2 ^ Mul2(a3));
            }
        }
        /// <summary>
        /// AESのInvMixColumnsステップを実行します。
        /// </summary>
        /// <param name="state"></param>
        private static void InvMixColumns(Span<byte> state)
        {
            for (int c = 0; c < 4; c++)
            {
                int i = c * 4;
                byte a0 = state[i + 0];
                byte a1 = state[i + 1];
                byte a2 = state[i + 2];
                byte a3 = state[i + 3];

                state[i + 0] = (byte)(Mul14(a0) ^ Mul11(a1) ^ Mul13(a2) ^ Mul9(a3));
                state[i + 1] = (byte)(Mul9(a0) ^ Mul14(a1) ^ Mul11(a2) ^ Mul13(a3));
                state[i + 2] = (byte)(Mul13(a0) ^ Mul9(a1) ^ Mul14(a2) ^ Mul11(a3));
                state[i + 3] = (byte)(Mul11(a0) ^ Mul13(a1) ^ Mul9(a2) ^ Mul14(a3));
            }
        }
        /// <summary>
        /// 指定された32ビットワードの各バイトをS-Boxを使用して置換します。
        /// キー拡張で使用されます。
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static uint SubWord(uint x)
        {
            return (uint)(SBox[(x >> 24) & 0xFF] << 24 |
                          SBox[(x >> 16) & 0xFF] << 16 |
                          SBox[(x >> 8) & 0xFF] << 8 |
                          SBox[x & 0xFF]);
        }
        /// <summary>
        /// 指定された32ビットワードを左に8ビット（1バイト）回転させます。
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static uint RotWord(uint x) => (x << 8) | (x >> 24);
        /// <summary>
        /// 指定されたバイト配列aに対して、バイト配列bをビットごとにXORします。
        /// 結果はaに格納されます。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private static void XorInPlace(Span<byte> a, ReadOnlySpan<byte> b)
        {
            for (int i = 0; i < BlockSize; i++)
            {
                a[i] ^= b[i];
            }
        }
        /// <summary>
        /// IV（初期化ベクトル）の長さを検証します。IVは16バイトである必要があります。
        /// </summary>
        /// <param name="iv">初期化ベクトル</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static void ValidateIv(byte[] iv)
        {
            if (iv is null)
            {
                throw new ArgumentNullException(nameof(iv));
            }
            if (iv.Length != BlockSize)
            {
                throw new ArgumentException("IV must be 16 bytes.", nameof(iv));
            }
        }
        /// <summary>
        /// データの長さが16バイトの倍数であることを確認し、コピーを作成します。
        /// パディングが無効な場合に使用されます。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static byte[] RequireBlockAlignedCopy(byte[] data)
        {
            if (data.Length % BlockSize != 0)
            {
                throw new ArgumentException("パディングが無効になっている場合、データの長さは16バイトの倍数でなければなりません。", nameof(data));
            }
            return (byte[])data.Clone();
        }
        /// <summary>
        /// PKCS7パディングを適用します。データの長さが16バイトの倍数になるように、必要なバイト数を追加します。
        /// </summary>
        /// <param name="data">パディングを適用するデータ</param>
        /// <returns>パディングが適用されたデータ</returns>
        private static byte[] ApplyPkcs7Padding(byte[] data)
        {
            int pad = BlockSize - (data.Length % BlockSize);
            if (pad == 0) pad = BlockSize;

            byte[] result = new byte[data.Length + pad];
            Buffer.BlockCopy(data, 0, result, 0, data.Length);
            for (int i = data.Length; i < result.Length; i++)
            {
                result[i] = (byte)pad;
            }
            return result;
        }
        /// <summary>
        /// PKCS7パディングを削除します。データの最後のバイトを確認し、その値に基づいてパディングを削除します。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="CryptographicException"></exception>
        private static byte[] RemovePkcs7Padding(byte[] data)
        {
            if (data.Length == 0 || data.Length % BlockSize != 0)
            {
                throw new CryptographicException("Invalid PKCS7 padding.");
            }

            int pad = data[^1];
            if (pad < 1 || pad > BlockSize)
            {
                throw new CryptographicException("Invalid PKCS7 padding.");
            }

            for (int i = data.Length - pad; i < data.Length; i++)
            {
                if (data[i] != pad)
                {
                    throw new CryptographicException("Invalid PKCS7 padding.");
                }
            }

            byte[] result = new byte[data.Length - pad];
            Buffer.BlockCopy(data, 0, result, 0, result.Length);
            return result;
        }
        /// <summary>
        /// GF(2^8)上での乗算を行います。
        /// AESのMixColumnsステップで使用されます。
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static byte Mul2(byte x)
        {
            int r = x << 1;
            if ((x & 0x80) != 0) r ^= 0x1B;
            return (byte)r;
        }

        private static byte Mul3(byte x) => (byte)(Mul2(x) ^ x);
        private static byte Mul9(byte x) => (byte)(Mul2(Mul2(Mul2(x))) ^ x);
        private static byte Mul11(byte x) => (byte)(Mul2(Mul2(Mul2(x))) ^ Mul2(x) ^ x);
        private static byte Mul13(byte x) => (byte)(Mul2(Mul2(Mul2(x))) ^ Mul2(Mul2(x)) ^ x);
        private static byte Mul14(byte x) => (byte)(Mul2(Mul2(Mul2(x))) ^ Mul2(Mul2(x)) ^ Mul2(x));
    }

    /// <summary>
    /// System.Security.Cryptography への依存関係を追加しないための例外型。
    /// </summary>
    public sealed class CryptographicException : Exception
    {
        public CryptographicException(string message) : base(message) { }
    }
}
