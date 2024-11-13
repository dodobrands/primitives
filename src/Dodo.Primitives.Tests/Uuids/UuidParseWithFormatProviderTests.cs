using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Dodo.Primitives.Tests.Uuids.Data;
using Dodo.Primitives.Tests.Uuids.Data.Models;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidParseWithFormatProviderTests
{
    private const string? NullString = null;

    [SuppressMessage("ReSharper", "RedundantCast")]
    public static IEnumerable GetFormatProviders()
    {
        yield return (IFormatProvider?) CultureInfo.InvariantCulture;
        yield return (IFormatProvider?) new CultureInfo("en-US");
        yield return (IFormatProvider?) null!;
    }

    [Test]
    public void ParseNullStringShouldThrows([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
#nullable disable
            Uuid _ = Uuid.Parse(NullString!, formatProvider);
#nullable restore
        });
    }

    [Test]
    public void ParseEmptyStringShouldThrows([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Throws<FormatException>(() =>
        {
            Uuid _ = Uuid.Parse(string.Empty, formatProvider);
        });
    }

    [Test]
    public void ParseEmptySpanShouldThrows([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Throws<FormatException>(() =>
        {
            Uuid _ = Uuid.Parse(new ReadOnlySpan<char>(new char[] { }), formatProvider);
        });
    }

    #region ParseN

    [Test]
    public unsafe void ParseCorrectNString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                string nString = correctNString.String;
                byte[] expectedBytes = correctNString.Bytes;

                Uuid parsedUuid = Uuid.Parse(nString, formatProvider);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = parsedUuid;
                }

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    [Test]
    public unsafe void ParseCorrectNSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                var nSpan = new ReadOnlySpan<char>(correctNString.String.ToCharArray());
                byte[] expectedBytes = correctNString.Bytes;

                Uuid parsedUuid = Uuid.Parse(nSpan, formatProvider);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = parsedUuid;
                }

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    [Test]
    public void ParseNIncorrectLargeString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string largeNString in UuidTestData.LargeNStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(largeNString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseNIncorrectLargeSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string largeNString in UuidTestData.LargeNStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var largeNSpan = new ReadOnlySpan<char>(largeNString.ToCharArray());
                    Uuid _ = Uuid.Parse(largeNSpan, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseNIncorrectSmallString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string smallNString in UuidTestData.SmallNStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(smallNString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseNIncorrectSmallSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string smallNString in UuidTestData.SmallNStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var smallNSpan = new ReadOnlySpan<char>(smallNString.ToCharArray());
                    Uuid _ = Uuid.Parse(smallNSpan, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseIncorrectNString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenNString in UuidTestData.BrokenNStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(brokenNString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseIncorrectNSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenNString in UuidTestData.BrokenNStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var brokenNSpan = new ReadOnlySpan<char>(brokenNString.ToCharArray());
                    Uuid _ = Uuid.Parse(brokenNSpan, formatProvider);
                });
            }
        });
    }

    #endregion

    #region ParseD

    [Test]
    public unsafe void ParseCorrectDString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctDString in UuidTestData.CorrectDStrings)
            {
                string dString = correctDString.String;
                byte[] expectedBytes = correctDString.Bytes;

                Uuid parsedUuid = Uuid.Parse(dString, formatProvider);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = parsedUuid;
                }

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    [Test]
    public unsafe void ParseCorrectDSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctDString in UuidTestData.CorrectDStrings)
            {
                var dSpan = new ReadOnlySpan<char>(correctDString.String.ToCharArray());
                byte[] expectedBytes = correctDString.Bytes;

                Uuid parsedUuid = Uuid.Parse(dSpan, formatProvider);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = parsedUuid;
                }

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    [Test]
    public void ParseDIncorrectLargeString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string largeDString in UuidTestData.LargeDStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(largeDString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseDIncorrectLargeSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string largeDString in UuidTestData.LargeDStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var largeDSpan = new ReadOnlySpan<char>(largeDString.ToCharArray());
                    Uuid _ = Uuid.Parse(largeDSpan, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseDIncorrectSmallString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string smallDString in UuidTestData.SmallDStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(smallDString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseDIncorrectSmallSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string smallDString in UuidTestData.SmallDStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var smallDSpan = new ReadOnlySpan<char>(smallDString.ToCharArray());
                    Uuid _ = Uuid.Parse(smallDSpan, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseIncorrectDString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenDString in UuidTestData.BrokenDStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(brokenDString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseIncorrectDSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenDString in UuidTestData.BrokenDStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var brokenDSpan = new ReadOnlySpan<char>(brokenDString.ToCharArray());
                    Uuid _ = Uuid.Parse(brokenDSpan, formatProvider);
                });
            }
        });
    }

    #endregion

    #region ParseB

    [Test]
    public unsafe void ParseCorrectBString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctBString in UuidTestData.CorrectBStrings)
            {
                string bString = correctBString.String;
                byte[] expectedBytes = correctBString.Bytes;

                Uuid parsedUuid = Uuid.Parse(bString, formatProvider);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = parsedUuid;
                }

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    [Test]
    public unsafe void ParseCorrectBSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctBString in UuidTestData.CorrectBStrings)
            {
                var bSpan = new ReadOnlySpan<char>(correctBString.String.ToCharArray());
                byte[] expectedBytes = correctBString.Bytes;

                Uuid parsedUuid = Uuid.Parse(bSpan, formatProvider);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = parsedUuid;
                }

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    [Test]
    public void ParseBIncorrectLargeString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string largeBString in UuidTestData.LargeBStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(largeBString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseBIncorrectLargeSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string largeBString in UuidTestData.LargeBStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var largeBSpan = new ReadOnlySpan<char>(largeBString.ToCharArray());
                    Uuid _ = Uuid.Parse(largeBSpan, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseBIncorrectSmallString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string smallBString in UuidTestData.SmallBStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(smallBString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseBIncorrectSmallSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string smallBString in UuidTestData.SmallBStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var smallBSpan = new ReadOnlySpan<char>(smallBString.ToCharArray());
                    Uuid _ = Uuid.Parse(smallBSpan, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseIncorrectBString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenBString in UuidTestData.BrokenBStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(brokenBString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseIncorrectBSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenBString in UuidTestData.BrokenBStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var brokenBSpan = new ReadOnlySpan<char>(brokenBString.ToCharArray());
                    Uuid _ = Uuid.Parse(brokenBSpan, formatProvider);
                });
            }
        });
    }

    #endregion

    #region ParseP

    [Test]
    public unsafe void ParseCorrectPString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctPString in UuidTestData.CorrectPStrings)
            {
                string pString = correctPString.String;
                byte[] expectedBytes = correctPString.Bytes;

                Uuid parsedUuid = Uuid.Parse(pString, formatProvider);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = parsedUuid;
                }

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    [Test]
    public unsafe void ParseCorrectPSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctPString in UuidTestData.CorrectPStrings)
            {
                var pSpan = new ReadOnlySpan<char>(correctPString.String.ToCharArray());
                byte[] expectedBytes = correctPString.Bytes;

                Uuid parsedUuid = Uuid.Parse(pSpan, formatProvider);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = parsedUuid;
                }

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    [Test]
    public void ParsePIncorrectLargeString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string largePString in UuidTestData.LargePStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(largePString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParsePIncorrectLargeSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string largePString in UuidTestData.LargePStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var largePSpan = new ReadOnlySpan<char>(largePString.ToCharArray());
                    Uuid _ = Uuid.Parse(largePSpan, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParsePIncorrectSmallString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string smallPString in UuidTestData.SmallPStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(smallPString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParsePIncorrectSmallSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string smallPString in UuidTestData.SmallPStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var smallPSpan = new ReadOnlySpan<char>(smallPString.ToCharArray());
                    Uuid _ = Uuid.Parse(smallPSpan, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseIncorrectPString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenPString in UuidTestData.BrokenPStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(brokenPString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseIncorrectPSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenPString in UuidTestData.BrokenPStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var brokenPSpan = new ReadOnlySpan<char>(brokenPString.ToCharArray());
                    Uuid _ = Uuid.Parse(brokenPSpan, formatProvider);
                });
            }
        });
    }

    #endregion

    #region ParseX

    [Test]
    public unsafe void ParseCorrectXString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctXString in UuidTestData.CorrectXStrings)
            {
                string xString = correctXString.String;
                byte[] expectedBytes = correctXString.Bytes;

                Uuid parsedUuid = Uuid.Parse(xString, formatProvider);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = parsedUuid;
                }

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    [Test]
    public unsafe void ParseCorrectXSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctXString in UuidTestData.CorrectXStrings)
            {
                var xSpan = new ReadOnlySpan<char>(correctXString.String.ToCharArray());
                byte[] expectedBytes = correctXString.Bytes;

                Uuid parsedUuid = Uuid.Parse(xSpan, formatProvider);

                var actualBytes = new byte[16];
                fixed (byte* pinnedActualBytes = actualBytes)
                {
                    *(Uuid*) pinnedActualBytes = parsedUuid;
                }

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
            }
        });
    }

    [Test]
    public void ParseXIncorrectLargeString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string largeXString in UuidTestData.LargeXStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(largeXString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseXIncorrectLargeSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string largeXString in UuidTestData.LargeXStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var largeXSpan = new ReadOnlySpan<char>(largeXString.ToCharArray());
                    Uuid _ = Uuid.Parse(largeXSpan, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseXIncorrectSmallString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string smallXString in UuidTestData.SmallXStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(smallXString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseXIncorrectSmallSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string smallXString in UuidTestData.SmallXStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var smallXSpan = new ReadOnlySpan<char>(smallXString.ToCharArray());
                    Uuid _ = Uuid.Parse(smallXSpan, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseIncorrectXString([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenXString in UuidTestData.BrokenXStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.Parse(brokenXString, formatProvider);
                });
            }
        });
    }

    [Test]
    public void ParseIncorrectXSpan([ValueSource(nameof(GetFormatProviders))] IFormatProvider formatProvider)
    {
        foreach (string brokenXString in UuidTestData.BrokenXStrings)
        {
            Assert.Throws<FormatException>(() =>
            {
                var brokenXSpan = new ReadOnlySpan<char>(brokenXString.ToCharArray());
                Uuid _ = Uuid.Parse(brokenXSpan, formatProvider);
            });
        }
    }

    #endregion
}
