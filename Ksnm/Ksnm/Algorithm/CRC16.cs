/*
The zlib License

Copyright (c) 2019 Takahiro Kasanami

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.

2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.

3. This notice may not be removed or altered from any source distribution.
*/
using System;

namespace Ksnm.Algorithm
{
    /// <summary>
    /// 巡回冗長検査 CRC-16
    /// </summary>
    public class CRC16
    {
        ushort[] table = new ushort[256];
        const CRC16Polynomial DefaultPolynomial = CRC16Polynomial.IBM_Reversed;
        /// <summary>
        /// 多項式
        /// </summary>
        public CRC16Polynomial Polynomial { get; private set; } = DefaultPolynomial;
        /// <summary>
        /// 初期値
        /// </summary>
        public ushort InitialValue { get; private set; } = 0;
        /// <summary>
        /// 出力時のXOR
        /// </summary>
        public ushort FinalXor { get; private set; } = 0;
        /// <summary>
        /// ハッシュ値を計算する。
        /// </summary>
        /// <param name="bytes">計算対象のバイト列</param>
        /// <returns>ハッシュ値</returns>
        public ushort ComputeHash(byte[] bytes)
        {
            ushort hash = InitialValue;
            for (int i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(hash ^ bytes[i]);
                hash = (ushort)((hash >> 8) ^ table[index]);
            }
            return (ushort)(hash ^ FinalXor);
        }
        /// <summary>
        /// ハッシュ値を計算し、バイト列で返す。
        /// </summary>
        /// <param name="bytes">計算対象のバイト列</param>
        /// <returns>ハッシュ値のバイト列</returns>
        public byte[] ComputeHashBytes(byte[] bytes)
        {
            var hash = ComputeHash(bytes);
            return BitConverter.GetBytes(hash);
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        /// <param name="polynomial">多項式</param>
        /// <param name="initialValue">初期値</param>
        /// <param name="finalXor">最終XOR</param>
        public CRC16(CRC16Polynomial polynomial = DefaultPolynomial, ushort initialValue = 0, ushort finalXor = 0)
        {
            Polynomial = polynomial;
            InitialValue = initialValue;
            FinalXor = finalXor;

            ushort value;
            ushort temp;
            for (ushort i = 0; i < table.Length; ++i)
            {
                value = 0;
                temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ (ushort)polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                table[i] = value;
            }
        }
    }
}
