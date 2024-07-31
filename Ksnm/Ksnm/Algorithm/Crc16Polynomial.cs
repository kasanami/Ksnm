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
namespace Ksnm.Algorithm
{
    /// <summary>
    /// CRC16の多項式
    /// </summary>
    public enum Crc16Polynomial : ushort
    {
        /// <summary>
        /// CRC-16-IBM
        /// </summary>
        Ibm = 0x8005,
        /// <summary>
        /// CRC-16-IBMの反転
        /// </summary>
        IbmReversed = 0xA001,
        /// <summary>
        /// CRC-16-CCITT
        /// </summary>
        Ccitt = 0x1021,
        /// <summary>
        /// CRC-16-CCITTの反転
        /// </summary>
        CcittReversed = 0x8408,
    }
}
