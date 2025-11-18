using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Dodo.Primitives.Tests.Uuids.Data;
using Dodo.Primitives.Tests.Uuids.Data.Models;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidTryParseUtf8WithFormatProviderTests
{
    public static IEnumerable GetFormatProviders()
    {
        foreach (IFormatProvider? nullableFormatProvider in GetNullableFormatProviders())
        {
            yield return nullableFormatProvider;
        }
    }

    [SuppressMessage("ReSharper", "RedundantCast")]
    public static IEnumerable<IFormatProvider?> GetNullableFormatProviders()
    {
        yield return (IFormatProvider?) CultureInfo.InvariantCulture;
        yield return (IFormatProvider?) new CultureInfo("en-US");
        yield return (IFormatProvider?) null!;
    }

    [Test]
    public void TryParseNullUtf8StringShouldFalse([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        byte[]? valueToParse = null;
        bool parsed = Uuid.TryParse(valueToParse, formatProvider, out Uuid uuid);
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Is.False);
            Assert.That(uuid, Is.EqualTo(Uuid.Empty));
        });
    }

    [Test]
    public void TryParseEmptyUtf8StringShouldFalse([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        byte[] valueToParse = Array.Empty<byte>();
        bool parsed = Uuid.TryParse(valueToParse, formatProvider, out Uuid uuid);
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Is.False);
            Assert.That(uuid, Is.EqualTo(Uuid.Empty));
        });
    }

    #region TryParseN

    [Test]
    public void TryParseUtf8CorrectNSpan()
    {
        TryParseUtf8CorrectSpan(UuidTestData.CorrectNStrings);
    }

    [Test]
    public void TryParseUtf8NIncorrectLargeSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.LargeNStrings);
    }

    [Test]
    public void TryParseUtf8NIncorrectSmallSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.SmallNStrings);
    }

    [Test]
    public void TryParseUtf8IncorrectNSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.BrokenNStrings);
    }

    #endregion

    #region TryParseD

    [Test]
    public void TryParseUtf8CorrectDSpan()
    {
        TryParseUtf8CorrectSpan(UuidTestData.CorrectDStrings);
    }

    [Test]
    public void TryParseUtf8DIncorrectLargeSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.LargeDStrings);
    }


    [Test]
    public void TryParseUtf8DIncorrectSmallSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.SmallDStrings);
    }

    [Test]
    public void TryParseUtf8IncorrectDSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.BrokenDStrings);
    }

    #endregion

    #region TryParseB

    [Test]
    public void TryParseUtf8CorrectBSpan()
    {
        TryParseUtf8CorrectSpan(UuidTestData.CorrectBStrings);
    }

    [Test]
    public void TryParseUtf8BIncorrectLargeSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.LargeBStrings);
    }

    [Test]
    public void TryParseUtf8BIncorrectSmallSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.SmallBStrings);
    }

    [Test]
    public void TryParseUtf8IncorrectBSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.BrokenBStrings);
    }

    #endregion

    #region TryParseP

    [Test]
    public void TryParseUtf8CorrectPSpan()
    {
        TryParseUtf8CorrectSpan(UuidTestData.CorrectPStrings);
    }

    [Test]
    public void TryParseUtf8PIncorrectLargeSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.LargePStrings);
    }

    [Test]
    public void TryParseUtf8PIncorrectSmallSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.SmallPStrings);
    }

    [Test]
    public void TryParseUtf8IncorrectPSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.BrokenPStrings);
    }

    #endregion

    #region TryParseX

    [Test]
    public void TryParseUtf8CorrectXSpan()
    {
        TryParseUtf8CorrectSpan(UuidTestData.CorrectXStrings);
    }

    [Test]
    public void TryParseUtf8XIncorrectLargeSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.LargeXStrings);
    }

    [Test]
    public void TryParseUtf8XIncorrectSmallSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.SmallXStrings);
    }

    [Test]
    public void TryParseUtf8IncorrectXSpan()
    {
        TryParseUtf8IncorrectSpan(UuidTestData.BrokenXStrings);
    }

    #endregion

    #region Helpers

    private unsafe void TryParseUtf8CorrectSpan(UuidStringWithBytes[] correctStrings)
    {
        Assert.Multiple(() =>
        {
            foreach (IFormatProvider? formatProvider in GetNullableFormatProviders())
            {
                foreach (UuidStringWithBytes correctString in correctStrings)
                {
                    byte[] spanToParse = Encoding.UTF8.GetBytes(correctString.String);
                    byte[] expectedBytes = correctString.Bytes;

                    bool parsed = Uuid.TryParse(spanToParse, formatProvider, out Uuid uuid);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = uuid;
                    }

                    Assert.That(parsed);
                    Assert.That(actualBytes, Is.EqualTo(expectedBytes));
                }
            }
        });
    }

    private void TryParseUtf8IncorrectSpan(string[] incorrectLargeStrings)
    {
        Assert.Multiple(() =>
        {
            foreach (IFormatProvider? formatProvider in GetNullableFormatProviders())
            {
                foreach (string largeString in incorrectLargeStrings)
                {
                    byte[] spanToParse = Encoding.UTF8.GetBytes(largeString);
                    Assert.That(Uuid.TryParse(spanToParse, formatProvider, out _), Is.False);
                }
            }
        });
    }

    #endregion
}
