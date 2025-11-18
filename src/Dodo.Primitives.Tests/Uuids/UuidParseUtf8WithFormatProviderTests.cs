using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Dodo.Primitives.Tests.Uuids.Data;
using Dodo.Primitives.Tests.Uuids.Data.Models;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidParseUtf8WithFormatProviderTests
{
    private static readonly byte[]? NullString = null;

    [SuppressMessage("ReSharper", "RedundantCast")]
    public static IEnumerable GetFormatProviders()
    {
        yield return (IFormatProvider?) CultureInfo.InvariantCulture;
        yield return (IFormatProvider?) new CultureInfo("en-US");
        yield return (IFormatProvider?) null!;
    }

    [Test]
    public void ParseNullUtf8StringShouldThrows([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Throws<FormatException>(() =>
        {
#nullable disable
            Uuid _ = Uuid.Parse(NullString!, formatProvider);
#nullable restore
        });
    }

    [Test]
    public void ParseEmptyUtf8StringShouldThrows([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Throws<FormatException>(() =>
        {
            Uuid _ = Uuid.Parse(Array.Empty<byte>(), formatProvider);
        });
    }

    #region ParseN

    [Test]
    public void ParseUtf8CorrectNSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8CorrectSpan(UuidTestData.CorrectNStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8NIncorrectLargeSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.LargeNStrings, formatProvider);
    }

    [Test]
    public void ParseNIncorrectSmallSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.SmallNStrings, formatProvider);
    }

    [Test]
    public void ParseIncorrectNSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.BrokenNStrings, formatProvider);
    }

    #endregion

    #region ParseD

    [Test]
    public void ParseUtf8CorrectDSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8CorrectSpan(UuidTestData.CorrectDStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8DIncorrectLargeSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.LargeDStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8DIncorrectSmallSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.SmallDStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8IncorrectDSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.BrokenDStrings, formatProvider);
    }

    #endregion

    #region ParseB

    [Test]
    public void ParseUtf8CorrectBSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8CorrectSpan(UuidTestData.CorrectBStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8BIncorrectLargeSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.LargeBStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8BIncorrectSmallSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.SmallBStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8IncorrectBSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.BrokenBStrings, formatProvider);
    }

    #endregion

    #region ParseP

    [Test]
    public void ParseUtf8CorrectPSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8CorrectSpan(UuidTestData.CorrectPStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8PIncorrectLargeSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.LargePStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8PIncorrectSmallSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.SmallPStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8IncorrectPSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.BrokenPStrings, formatProvider);
    }

    #endregion

    #region ParseX

    [Test]
    public void ParseUtf8CorrectXSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8CorrectSpan(UuidTestData.CorrectXStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8XIncorrectLargeSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.LargeXStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8XIncorrectSmallSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.SmallXStrings, formatProvider);
    }

    [Test]
    public void ParseUtf8IncorrectXSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        ParseUtf8IncorrectSpan(UuidTestData.BrokenXStrings, formatProvider);
    }

    #endregion

    #region Helpers

    private unsafe void ParseUtf8CorrectSpan(UuidStringWithBytes[] correctStrings, IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            Span<byte> utf8Buffer = stackalloc byte[8192];
            foreach (UuidStringWithBytes correctString in correctStrings)
            {
                utf8Buffer.Clear();
                int utf8Chars = GetUtf8BytesSpanFromString(correctString.String, utf8Buffer);
                Span<byte> spanToParse = utf8Buffer[..utf8Chars];
                byte[] expectedBytes = correctString.Bytes;

                Uuid uuid = Uuid.Parse(spanToParse, formatProvider);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = uuid;
                }

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    private void ParseUtf8IncorrectSpan(string[] incorrectLargeStrings, IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            Span<byte> utf8Buffer = stackalloc byte[8192];
            foreach (string largeString in incorrectLargeStrings)
            {
                int utf8Chars = GetUtf8BytesSpanFromString(largeString, utf8Buffer);
                byte[] spanToParse = utf8Buffer[..utf8Chars].ToArray();
                Assert.Throws<FormatException>(() =>
                {
                    Uuid.Parse(spanToParse.AsSpan(), formatProvider);
                });
            }
        });
    }

    private static int GetUtf8BytesSpanFromString(string uuidString, Span<byte> result)
    {
        byte[] resultBytes = Encoding.UTF8.GetBytes(uuidString);
        if (resultBytes.Length > result.Length)
        {
            throw new Exception("Utf8 bytes larger than provided buffer");
        }

        for (var i = 0; i < resultBytes.Length; i++)
        {
            result[i] = resultBytes[i];
        }

        return resultBytes.Length;
    }

    #endregion
}
