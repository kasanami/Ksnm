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
using Original = System.IO;

namespace Ksnm.ExtensionMethods.System.IO.Stream
{
    /// <summary>
    /// Streamの拡張メソッド
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 現在のストリームから値を読み取る。
        /// </summary>
        /// <returns>読み取った値</returns>
        /// <exception cref="System.IO.EndOfStreamException">Readの戻り値が読み込む型のサイズと異なる。</exception>
        public static SByte ReadInt8(this Original.Stream self)
        {
            var buffer = new byte[sizeof(SByte)];
            var readSize = self.Read(buffer, 0, buffer.Length);
            if (readSize != buffer.Length)
            {
                throw new Original.EndOfStreamException($"readSize = {readSize}");
            }
            return (SByte)buffer[0];
        }
        /// <summary>
        /// 現在のストリームから値を読み取る。
        /// </summary>
        /// <returns>読み取った値</returns>
        /// <exception cref="System.IO.EndOfStreamException">Readの戻り値が読み込む型のサイズと異なる。</exception>
        public static Byte ReadUInt8(this Original.Stream self)
        {
            var buffer = new byte[sizeof(Byte)];
            var readSize = self.Read(buffer, 0, buffer.Length);
            if (readSize != buffer.Length)
            {
                throw new Original.EndOfStreamException($"readSize = {readSize}");
            }
            return (Byte)buffer[0];
        }
        /// <summary>
        /// 現在のストリームから値を読み取る。
        /// </summary>
        /// <returns>読み取った値</returns>
        /// <exception cref="System.IO.EndOfStreamException">Readの戻り値が読み込む型のサイズと異なる。</exception>
        public static Int16 ReadInt16(this Original.Stream self)
        {
            var buffer = new byte[sizeof(Int16)];
            var readSize = self.Read(buffer, 0, buffer.Length);
            if (readSize != buffer.Length)
            {
                throw new Original.EndOfStreamException($"readSize = {readSize}");
            }
            return BitConverter.ToInt16(buffer, 0);
        }
        /// <summary>
        /// 現在のストリームから値を読み取る。
        /// </summary>
        /// <returns>読み取った値</returns>
        /// <exception cref="System.IO.EndOfStreamException">Readの戻り値が読み込む型のサイズと異なる。</exception>
        public static UInt16 ReadUInt16(this Original.Stream self)
        {
            var buffer = new byte[sizeof(UInt16)];
            var readSize = self.Read(buffer, 0, buffer.Length);
            if (readSize != buffer.Length)
            {
                throw new Original.EndOfStreamException($"readSize = {readSize}");
            }
            return BitConverter.ToUInt16(buffer, 0);
        }
        /// <summary>
        /// 現在のストリームから値を読み取る。
        /// </summary>
        /// <returns>読み取った値</returns>
        /// <exception cref="System.IO.EndOfStreamException">Readの戻り値が読み込む型のサイズと異なる。</exception>
        public static Int32 ReadInt32(this Original.Stream self)
        {
            var buffer = new byte[sizeof(Int32)];
            var readSize = self.Read(buffer, 0, buffer.Length);
            if (readSize != buffer.Length)
            {
                throw new Original.EndOfStreamException($"readSize = {readSize}");
            }
            return BitConverter.ToInt32(buffer, 0);
        }
        /// <summary>
        /// 現在のストリームから値を読み取る。
        /// </summary>
        /// <returns>読み取った値</returns>
        /// <exception cref="System.IO.EndOfStreamException">Readの戻り値が読み込む型のサイズと異なる。</exception>
        public static UInt32 ReadUInt32(this Original.Stream self)
        {
            var buffer = new byte[sizeof(UInt32)];
            var readSize = self.Read(buffer, 0, buffer.Length);
            if (readSize != buffer.Length)
            {
                throw new Original.EndOfStreamException($"readSize = {readSize}");
            }
            return BitConverter.ToUInt32(buffer, 0);
        }
        /// <summary>
        /// 現在のストリームから値を読み取る。
        /// </summary>
        /// <returns>読み取った値</returns>
        /// <exception cref="System.IO.EndOfStreamException">Readの戻り値が読み込む型のサイズと異なる。</exception>
        public static Int64 ReadInt64(this Original.Stream self)
        {
            var buffer = new byte[sizeof(Int64)];
            var readSize = self.Read(buffer, 0, buffer.Length);
            if (readSize != buffer.Length)
            {
                throw new Original.EndOfStreamException($"readSize = {readSize}");
            }
            return BitConverter.ToInt64(buffer, 0);
        }
        /// <summary>
        /// 現在のストリームから値を読み取る。
        /// </summary>
        /// <returns>読み取った値</returns>
        /// <exception cref="System.IO.EndOfStreamException">Readの戻り値が読み込む型のサイズと異なる。</exception>
        public static UInt64 ReadUInt64(this Original.Stream self)
        {
            var buffer = new byte[sizeof(UInt64)];
            var readSize = self.Read(buffer, 0, buffer.Length);
            if (readSize != buffer.Length)
            {
                throw new Original.EndOfStreamException($"readSize = {readSize}");
            }
            return BitConverter.ToUInt64(buffer, 0);
        }
    }
}
