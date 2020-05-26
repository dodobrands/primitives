using System;
using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids
{
    public class UuidTryWriteBytesTests
    {
        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public unsafe void ToByteArrayExactOutputSize(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            byte* buffer = stackalloc byte[16];
            var output = new Span<byte>(buffer, 16);

            bool wasWritten = uuid.TryWriteBytes(output);

            var outputBytes = output.ToArray();

            Assert.True(wasWritten);
            Assert.AreEqual(correctBytes, outputBytes);
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public unsafe void ToByteArrayLargeOutputSize(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            byte* buffer = stackalloc byte[512];
            var output = new Span<byte>(buffer, 512);

            bool wasWritten = uuid.TryWriteBytes(output);

            var outputBytes = output.Slice(0, 16).ToArray();

            Assert.True(wasWritten);
            Assert.AreEqual(correctBytes, outputBytes);
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public unsafe void ToByteArraySmallOutputSize(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            byte* buffer = stackalloc byte[4];
            var output = new Span<byte>(buffer, 4);

            bool wasWritten = uuid.TryWriteBytes(output);

            var outputBytes = output.ToArray();

            Assert.False(wasWritten);
            Assert.AreNotEqual(correctBytes, outputBytes);
        }
    }
}
