using System;
using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids
{
    public class UuidTryFormatTests
    {
        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void TryFormatNullFormat(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            var expectedString = UuidTestsUtils.GetStringN(correctBytes);
            Span<char> buffer = stackalloc char[32];
            Assert.True(uuid.TryFormat(buffer, out int charsWritten));
            Assert.AreEqual(32, charsWritten);
            Assert.AreEqual(expectedString, new string(buffer));
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void TryFormatEmptyFormat(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            var expectedString = UuidTestsUtils.GetStringN(correctBytes);
            Span<char> buffer = stackalloc char[32];
            Assert.True(uuid.TryFormat(buffer, out int charsWritten, ReadOnlySpan<char>.Empty));
            Assert.AreEqual(32, charsWritten);
            Assert.AreEqual(expectedString, new string(buffer));
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void TryFormatIncorrectFormat(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            Span<char> buffer = stackalloc char[68];
            Assert.False(uuid.TryFormat(buffer, out int charsWritten, "Ъ"));
            Assert.AreEqual(0, charsWritten);
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void TryFormatTooLongFormat(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            Span<char> buffer = stackalloc char[68];
            Assert.False(uuid.TryFormat(buffer, out int charsWritten, "ЪЪ"));
            Assert.AreEqual(0, charsWritten);
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void TryFormatNCorrect(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            var expectedString = UuidTestsUtils.GetStringN(correctBytes);
            Span<char> buffer = stackalloc char[32];
            Assert.True(uuid.TryFormat(buffer, out int charsWritten, new ReadOnlySpan<char>(new[] {'N'})));
            Assert.AreEqual(32, charsWritten);
            Assert.AreEqual(expectedString, new string(buffer));
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void TryFormatDCorrect(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            var expectedString = UuidTestsUtils.GetStringD(correctBytes);
            Span<char> buffer = stackalloc char[36];
            Assert.True(uuid.TryFormat(buffer, out int charsWritten, new ReadOnlySpan<char>(new[] {'D'})));
            Assert.AreEqual(36, charsWritten);
            Assert.AreEqual(expectedString, new string(buffer));
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void TryFormatBCorrect(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            var expectedString = UuidTestsUtils.GetStringB(correctBytes);
            Span<char> buffer = stackalloc char[38];
            Assert.True(uuid.TryFormat(buffer, out int charsWritten, new ReadOnlySpan<char>(new[] {'B'})));
            Assert.AreEqual(38, charsWritten);
            Assert.AreEqual(expectedString, new string(buffer));
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void TryFormatPCorrect(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            var expectedString = UuidTestsUtils.GetStringP(correctBytes);
            Span<char> buffer = stackalloc char[38];
            Assert.True(uuid.TryFormat(buffer, out int charsWritten, new ReadOnlySpan<char>(new[] {'P'})));
            Assert.AreEqual(38, charsWritten);
            Assert.AreEqual(expectedString, new string(buffer));
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void TryFormatXCorrect(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            var expectedString = UuidTestsUtils.GetStringX(correctBytes);
            Span<char> buffer = stackalloc char[68];
            Assert.True(uuid.TryFormat(buffer, out int charsWritten, new ReadOnlySpan<char>(new[] {'X'})));
            Assert.AreEqual(68, charsWritten);
            Assert.AreEqual(expectedString, new string(buffer));
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void TryFormatSmallDestination(byte[] correctBytes)
        {
            Assert.Multiple(() =>
            {
                var uuid = new Uuid(correctBytes);
                Span<char> buffer = stackalloc char[10];
                var formats = new[]
                {
                    'N',
                    'n',
                    'D',
                    'd',
                    'B',
                    'b',
                    'P',
                    'p',
                    'X',
                    'x'
                };
                foreach (char format in formats)
                {
                    Assert.False(uuid.TryFormat(buffer, out int charsWritten, new ReadOnlySpan<char>(new[] {format})));
                    Assert.AreEqual(0, charsWritten);
                }
            });
        }
    }
}
