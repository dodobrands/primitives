using System;
using Dodo.Primitives.Tests.Uuids.Data;
using Dodo.Primitives.Tests.Uuids.Data.Models;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidTryParseTests
{
    [Test]
    public void TryParseNullStringShouldFalse()
    {
        bool parsed = Uuid.TryParse((string?) null, out Uuid uuid);
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Is.False);
            Assert.That(uuid, Is.EqualTo(Uuid.Empty));
        });
    }

    [Test]
    public void TryParseEmptyStringShouldFalse()
    {
        bool parsed = Uuid.TryParse(string.Empty, out Uuid uuid);
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Is.False);
            Assert.That(uuid, Is.EqualTo(Uuid.Empty));
        });
    }

    [Test]
    public void TryParseEmptySpanShouldFalse()
    {
        bool parsed = Uuid.TryParse(new ReadOnlySpan<char>(new char[] { }), out Uuid uuid);
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Is.False);
            Assert.That(uuid, Is.EqualTo(Uuid.Empty));
        });
    }

    #region TryParseN

    [Test]
    public void TryParseCorrectNString()
    {
        TryParseCorrectString(UuidTestData.CorrectNStrings);
    }

    [Test]
    public void TryParseCorrectNSpan()
    {
        TryParseCorrectSpan(UuidTestData.CorrectNStrings);
    }

    [Test]
    public void TryParseNIncorrectLargeString()
    {
        TryParseIncorrectString(UuidTestData.LargeNStrings);
    }

    [Test]
    public void ParseNIncorrectLargeSpan()
    {
        TryParseIncorrectSpan(UuidTestData.LargeNStrings);
    }

    [Test]
    public void ParseNIncorrectSmallString()
    {
        TryParseIncorrectString(UuidTestData.SmallNStrings);
    }

    [Test]
    public void ParseNIncorrectSmallSpan()
    {
        TryParseIncorrectSpan(UuidTestData.SmallNStrings);
    }

    [Test]
    public void ParseIncorrectNString()
    {
        TryParseIncorrectString(UuidTestData.BrokenNStrings);
    }

    [Test]
    public void ParseIncorrectNSpan()
    {
        TryParseIncorrectSpan(UuidTestData.BrokenNStrings);
    }

    #endregion

    #region TryParseD

    [Test]
    public void TryParseCorrectDString()
    {
        TryParseCorrectString(UuidTestData.CorrectDStrings);
    }

    [Test]
    public void TryParseCorrectDSpan()
    {
        TryParseCorrectSpan(UuidTestData.CorrectDStrings);
    }

    [Test]
    public void TryParseDIncorrectLargeString()
    {
        TryParseIncorrectString(UuidTestData.LargeDStrings);
    }

    [Test]
    public void ParseDIncorrectLargeSpan()
    {
        TryParseIncorrectSpan(UuidTestData.LargeDStrings);
    }

    [Test]
    public void ParseDIncorrectSmallString()
    {
        TryParseIncorrectString(UuidTestData.SmallDStrings);
    }

    [Test]
    public void ParseDIncorrectSmallSpan()
    {
        TryParseIncorrectSpan(UuidTestData.SmallDStrings);
    }

    [Test]
    public void ParseIncorrectDString()
    {
        TryParseIncorrectString(UuidTestData.BrokenDStrings);
    }

    [Test]
    public void ParseIncorrectDSpan()
    {
        TryParseIncorrectSpan(UuidTestData.BrokenDStrings);
    }

    #endregion

    #region TryParseB

    [Test]
    public void TryParseCorrectBString()
    {
        TryParseCorrectString(UuidTestData.CorrectBStrings);
    }

    [Test]
    public void TryParseCorrectBSpan()
    {
        TryParseCorrectSpan(UuidTestData.CorrectBStrings);
    }

    [Test]
    public void TryParseBIncorrectLargeString()
    {
        TryParseIncorrectString(UuidTestData.LargeBStrings);
    }

    [Test]
    public void ParseBIncorrectLargeSpan()
    {
        TryParseIncorrectSpan(UuidTestData.LargeBStrings);
    }

    [Test]
    public void ParseBIncorrectSmallString()
    {
        TryParseIncorrectString(UuidTestData.SmallBStrings);
    }

    [Test]
    public void ParseBIncorrectSmallSpan()
    {
        TryParseIncorrectSpan(UuidTestData.SmallBStrings);
    }

    [Test]
    public void ParseIncorrectBString()
    {
        TryParseIncorrectString(UuidTestData.BrokenBStrings);
    }

    [Test]
    public void ParseIncorrectBSpan()
    {
        TryParseIncorrectSpan(UuidTestData.BrokenBStrings);
    }

    #endregion

    #region TryParseP

    [Test]
    public void TryParseCorrectPString()
    {
        TryParseCorrectString(UuidTestData.CorrectPStrings);
    }

    [Test]
    public void TryParseCorrectPSpan()
    {
        TryParseCorrectSpan(UuidTestData.CorrectPStrings);
    }

    [Test]
    public void TryParsePIncorrectLargeString()
    {
        TryParseIncorrectString(UuidTestData.LargePStrings);
    }

    [Test]
    public void ParsePIncorrectLargeSpan()
    {
        TryParseIncorrectSpan(UuidTestData.LargePStrings);
    }

    [Test]
    public void ParsePIncorrectSmallString()
    {
        TryParseIncorrectString(UuidTestData.SmallPStrings);
    }

    [Test]
    public void ParsePIncorrectSmallSpan()
    {
        TryParseIncorrectSpan(UuidTestData.SmallPStrings);
    }

    [Test]
    public void ParseIncorrectPString()
    {
        TryParseIncorrectString(UuidTestData.BrokenPStrings);
    }

    [Test]
    public void ParseIncorrectPSpan()
    {
        TryParseIncorrectSpan(UuidTestData.BrokenPStrings);
    }

    #endregion

    #region TryParseX

    [Test]
    public void TryParseCorrectXString()
    {
        TryParseCorrectString(UuidTestData.CorrectXStrings);
    }

    [Test]
    public void TryParseCorrectXSpan()
    {
        TryParseCorrectSpan(UuidTestData.CorrectXStrings);
    }

    [Test]
    public void TryParseXIncorrectLargeString()
    {
        TryParseIncorrectString(UuidTestData.LargeXStrings);
    }

    [Test]
    public void ParseXIncorrectLargeSpan()
    {
        TryParseIncorrectSpan(UuidTestData.LargeXStrings);
    }

    [Test]
    public void ParseXIncorrectSmallString()
    {
        TryParseIncorrectString(UuidTestData.SmallXStrings);
    }

    [Test]
    public void ParseXIncorrectSmallSpan()
    {
        TryParseIncorrectSpan(UuidTestData.SmallXStrings);
    }

    [Test]
    public void ParseIncorrectXString()
    {
        TryParseIncorrectString(UuidTestData.BrokenXStrings);
    }

    [Test]
    public void ParseIncorrectXSpan()
    {
        TryParseIncorrectSpan(UuidTestData.BrokenXStrings);
    }

    #endregion

    #region Helpers

    private unsafe void TryParseCorrectString(UuidStringWithBytes[] correctStrings)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctString in correctStrings)
            {
                string stringToParse = correctString.String;
                byte[] expectedBytes = correctString.Bytes;

                bool parsed = Uuid.TryParse(stringToParse, out Uuid uuid);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = uuid;
                }

                Assert.That(parsed);
                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    private unsafe void TryParseCorrectSpan(UuidStringWithBytes[] correctStrings)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctString in correctStrings)
            {
                var spanToParse = new ReadOnlySpan<char>(correctString.String.ToCharArray());
                byte[] expectedBytes = correctString.Bytes;

                bool parsed = Uuid.TryParse(spanToParse, out Uuid uuid);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = uuid;
                }

                Assert.That(parsed);
                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    private void TryParseIncorrectString(string[] incorrectLargeStrings)
    {
        Assert.Multiple(() =>
        {
            foreach (string largeString in incorrectLargeStrings)
            {
                Assert.That(Uuid.TryParse(largeString, out _), Is.False);
            }
        });
    }

    private void TryParseIncorrectSpan(string[] incorrectLargeStrings)
    {
        Assert.Multiple(() =>
        {
            foreach (string largeString in incorrectLargeStrings)
            {
                var largeSpan = new ReadOnlySpan<char>(largeString.ToCharArray());
                Assert.That(Uuid.TryParse(largeSpan, out _), Is.False);
            }
        });
    }

    #endregion
}
