using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MemoryStream = global::System.IO.MemoryStream;

namespace Ksnm.ExtensionMethods.System.IO.Stream.Tests
{
    [TestClass()]
    public class StreamExtensionsTests
    {
        byte[] sampleBuffer = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        [TestMethod()]
        public void ReadInt8Test()
        {
            using (var memoryStream = new MemoryStream(sampleBuffer))
            {
                Assert.AreEqual<sbyte>(1, memoryStream.ReadInt8());
                Assert.AreEqual<sbyte>(2, memoryStream.ReadInt8());
            }
        }

        [TestMethod()]
        public void ReadUInt8Test()
        {
            using (var memoryStream = new MemoryStream(sampleBuffer))
            {
                Assert.AreEqual<byte>(1, memoryStream.ReadUInt8());
                Assert.AreEqual<byte>(2, memoryStream.ReadUInt8());
            }
        }

        [TestMethod()]
        public void ReadInt16Test()
        {
            using (var memoryStream = new MemoryStream(sampleBuffer))
            {
                Assert.AreEqual<Int16>(0x0201, memoryStream.ReadInt16());
                Assert.AreEqual<Int16>(0x0403, memoryStream.ReadInt16());
            }
        }

        [TestMethod()]
        public void ReadUInt16Test()
        {
            using (var memoryStream = new MemoryStream(sampleBuffer))
            {
                Assert.AreEqual<UInt16>(0x0201, memoryStream.ReadUInt16());
                Assert.AreEqual<UInt16>(0x0403, memoryStream.ReadUInt16());
            }
        }

        [TestMethod()]
        public void ReadInt32Test()
        {
            using (var memoryStream = new MemoryStream(sampleBuffer))
            {
                Assert.AreEqual<Int32>(0x04030201, memoryStream.ReadInt32());
                Assert.AreEqual<Int32>(0x08070605, memoryStream.ReadInt32());
            }
        }

        [TestMethod()]
        public void ReadUInt32Test()
        {
            using (var memoryStream = new MemoryStream(sampleBuffer))
            {
                Assert.AreEqual<UInt32>(0x04030201, memoryStream.ReadUInt32());
                Assert.AreEqual<UInt32>(0x08070605, memoryStream.ReadUInt32());
            }
        }

        [TestMethod()]
        public void ReadInt64Test()
        {
            using (var memoryStream = new MemoryStream(sampleBuffer))
            {
                Assert.AreEqual<Int64>(0x08070605_04030201, memoryStream.ReadInt64());
            }
        }

        [TestMethod()]
        public void ReadUInt64Test()
        {
            using (var memoryStream = new MemoryStream(sampleBuffer))
            {
                Assert.AreEqual<UInt64>(0x08070605_04030201, memoryStream.ReadUInt64());
            }
        }
    }
}
