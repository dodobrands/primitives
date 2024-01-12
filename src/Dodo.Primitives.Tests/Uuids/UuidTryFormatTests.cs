using System;
using System.Text;
using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public unsafe class UuidTryFormatTests
{
    #region TryFormat

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void TryFormatNullFormat(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringN(correctBytes);
        char* bufferPtr = stackalloc char[32];
        var spanBuffer = new Span<char>(bufferPtr, 32);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten));
        Assert.AreEqual(32, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 32));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void TryFormatEmptyFormat(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringN(correctBytes);
        char* bufferPtr = stackalloc char[32];
        var spanBuffer = new Span<char>(bufferPtr, 32);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, ReadOnlySpan<char>.Empty));
        Assert.AreEqual(32, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 32));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void TryFormatIncorrectFormat(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        Span<char> buffer = stackalloc char[68];
        Assert.False(uuid.TryFormat(buffer, out int charsWritten, "Ъ".AsSpan()));
        Assert.AreEqual(0, charsWritten);
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void TryFormatTooLongFormat(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        Span<char> buffer = stackalloc char[68];
        Assert.False(uuid.TryFormat(buffer, out int charsWritten, "ЪЪ".AsSpan()));
        Assert.AreEqual(0, charsWritten);
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void TryFormatNCorrect(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringN(correctBytes);
        char* bufferPtr = stackalloc char[32];
        var spanBuffer = new Span<char>(bufferPtr, 32);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'N' })));
        Assert.AreEqual(32, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 32));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void TryFormatDCorrect(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringD(correctBytes);
        char* bufferPtr = stackalloc char[36];
        var spanBuffer = new Span<char>(bufferPtr, 36);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'D' })));
        Assert.AreEqual(36, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 36));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void TryFormatBCorrect(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringB(correctBytes);
        char* bufferPtr = stackalloc char[38];
        var spanBuffer = new Span<char>(bufferPtr, 38);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'B' })));
        Assert.AreEqual(38, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 38));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void TryFormatPCorrect(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringP(correctBytes);
        char* bufferPtr = stackalloc char[38];
        var spanBuffer = new Span<char>(bufferPtr, 38);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'P' })));
        Assert.AreEqual(38, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 38));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void TryFormatXCorrect(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringX(correctBytes);
        char* bufferPtr = stackalloc char[68];
        var spanBuffer = new Span<char>(bufferPtr, 68);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'X' })));
        Assert.AreEqual(68, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 68));
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
                Assert.False(uuid.TryFormat(buffer, out int charsWritten, new ReadOnlySpan<char>(new[] { format })));
                Assert.AreEqual(0, charsWritten);
            }
        });
    }

    #endregion

    #region ISpanFormattable.TryFormat

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void SpanFormattableTryFormatEmptyFormat(byte[] correctBytes)
    {
        ISpanFormattable uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringN(correctBytes);
        char* bufferPtr = stackalloc char[32];
        var spanBuffer = new Span<char>(bufferPtr, 32);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, ReadOnlySpan<char>.Empty, null));
        Assert.AreEqual(32, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 32));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void SpanFormattableTryFormatIncorrectFormat(byte[] correctBytes)
    {
        ISpanFormattable uuid = new Uuid(correctBytes);
        Span<char> buffer = stackalloc char[68];
        Assert.False(uuid.TryFormat(buffer, out int charsWritten, "Ъ".AsSpan(), null));
        Assert.AreEqual(0, charsWritten);
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void SpanFormattableTryFormatTooLongFormat(byte[] correctBytes)
    {
        ISpanFormattable uuid = new Uuid(correctBytes);
        Span<char> buffer = stackalloc char[68];
        Assert.False(uuid.TryFormat(buffer, out int charsWritten, "ЪЪ".AsSpan(), null));
        Assert.AreEqual(0, charsWritten);
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void SpanFormattableTryFormatNCorrect(byte[] correctBytes)
    {
        ISpanFormattable uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringN(correctBytes);
        char* bufferPtr = stackalloc char[32];
        var spanBuffer = new Span<char>(bufferPtr, 32);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'N' }), null));
        Assert.AreEqual(32, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 32));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void SpanFormattableTryFormatDCorrect(byte[] correctBytes)
    {
        ISpanFormattable uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringD(correctBytes);
        char* bufferPtr = stackalloc char[36];
        var spanBuffer = new Span<char>(bufferPtr, 36);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'D' }), null));
        Assert.AreEqual(36, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 36));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void SpanFormattableTryFormatBCorrect(byte[] correctBytes)
    {
        ISpanFormattable uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringB(correctBytes);
        char* bufferPtr = stackalloc char[38];
        var spanBuffer = new Span<char>(bufferPtr, 38);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'B' }), null));
        Assert.AreEqual(38, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 38));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void SpanFormattableTryFormatPCorrect(byte[] correctBytes)
    {
        ISpanFormattable uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringP(correctBytes);
        char* bufferPtr = stackalloc char[38];
        var spanBuffer = new Span<char>(bufferPtr, 38);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'P' }), null));
        Assert.AreEqual(38, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 38));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void SpanFormattableTryFormatXCorrect(byte[] correctBytes)
    {
        ISpanFormattable uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringX(correctBytes);
        char* bufferPtr = stackalloc char[68];
        var spanBuffer = new Span<char>(bufferPtr, 68);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'X' }), null));
        Assert.AreEqual(68, charsWritten);
        Assert.AreEqual(expectedString, new string(bufferPtr, 0, 68));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void SpanFormattableTryFormatSmallDestination(byte[] correctBytes)
    {
        Assert.Multiple(() =>
        {
            ISpanFormattable uuid = new Uuid(correctBytes);
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
                Assert.False(uuid.TryFormat(buffer, out int charsWritten, new ReadOnlySpan<char>(new[] { format }), null));
                Assert.AreEqual(0, charsWritten);
            }
        });
    }

    #endregion

    #region IUtf8SpanFormattable.TryFormat

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void Utf8SpanFormattableTryFormatEmptyFormat(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringN(correctBytes);
        byte* bufferPtr = stackalloc byte[32];
        var spanBuffer = new Span<byte>(bufferPtr, 32);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, ReadOnlySpan<char>.Empty, null));
        Assert.AreEqual(32, charsWritten);
        Assert.AreEqual(expectedString, Encoding.UTF8.GetString(bufferPtr, charsWritten));
    }


    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void Utf8SpanFormattableTryFormatIncorrectFormat(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        Span<byte> buffer = stackalloc byte[68];
        Assert.False(uuid.TryFormat(buffer, out int charsWritten, "Ъ".AsSpan(), null));
        Assert.AreEqual(0, charsWritten);
    }


    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void Utf8SpanFormattableTryFormatTooLongFormat(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        Span<byte> buffer = stackalloc byte[68];
        Assert.False(uuid.TryFormat(buffer, out int charsWritten, "ЪЪ".AsSpan(), null));
        Assert.AreEqual(0, charsWritten);
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void Utf8SpanFormattableTryFormatNCorrect(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringN(correctBytes);
        byte* bufferPtr = stackalloc byte[32];
        var spanBuffer = new Span<byte>(bufferPtr, 32);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'N' }), null));
        Assert.AreEqual(32, charsWritten);
        Assert.AreEqual(expectedString, Encoding.UTF8.GetString(bufferPtr, charsWritten));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void Utf8SpanFormattableTryFormatDCorrect(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringD(correctBytes);
        byte* bufferPtr = stackalloc byte[36];
        var spanBuffer = new Span<byte>(bufferPtr, 36);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'D' }), null));
        Assert.AreEqual(36, charsWritten);
        Assert.AreEqual(expectedString, Encoding.UTF8.GetString(bufferPtr, charsWritten));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void Utf8SpanFormattableTryFormatBCorrect(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringB(correctBytes);
        byte* bufferPtr = stackalloc byte[38];
        var spanBuffer = new Span<byte>(bufferPtr, 38);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'B' }), null));
        Assert.AreEqual(38, charsWritten);
        Assert.AreEqual(expectedString, Encoding.UTF8.GetString(bufferPtr, charsWritten));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void Utf8SpanFormattableTryFormatPCorrect(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringP(correctBytes);
        byte* bufferPtr = stackalloc byte[38];
        var spanBuffer = new Span<byte>(bufferPtr, 38);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'P' }), null));
        Assert.AreEqual(38, charsWritten);
        Assert.AreEqual(expectedString, Encoding.UTF8.GetString(bufferPtr, charsWritten));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void Utf8SpanFormattableTryFormatXCorrect(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringX(correctBytes);
        byte* bufferPtr = stackalloc byte[68];
        var spanBuffer = new Span<byte>(bufferPtr, 68);
        Assert.True(uuid.TryFormat(spanBuffer, out int charsWritten, new ReadOnlySpan<char>(new[] { 'X' }), null));
        Assert.AreEqual(68, charsWritten);
        Assert.AreEqual(expectedString, Encoding.UTF8.GetString(bufferPtr, charsWritten));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void Utf8SpanFormattableTryFormatSmallDestination(byte[] correctBytes)
    {
        Assert.Multiple(() =>
        {
            var uuid = new Uuid(correctBytes);
            Span<byte> buffer = stackalloc byte[10];
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
                Assert.False(uuid.TryFormat(buffer, out int charsWritten, new ReadOnlySpan<char>(new[] { format }), null));
                Assert.AreEqual(0, charsWritten);
            }
        });
    }

    #endregion
}
