using System;
using System.Collections.Generic;
using Dodo.Primitives.Tests.Uuids.Data;
using Dodo.Primitives.Tests.Uuids.Data.Models;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidParseExactTests
{
    private const string? NullString = null;

    [Test]
    public void ParseExactNullStringCorrectFormatShouldThrows()
    {
        Assert.Multiple(() =>
        {
            foreach (string format in UuidTestData.Formats.All)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
#pragma warning disable 8625
                    Uuid _ = Uuid.ParseExact(NullString, format);
#pragma warning restore 8625
                });
            }
        });
    }

    [Test]
    public void ParseExactCorrectStringNullFormatShouldThrows()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
#pragma warning disable 8625
                    Uuid _ = Uuid.ParseExact(correctNString.String, NullString);
#pragma warning restore 8625
                });
            }
        });
    }

    [Test]
    public void ParseExactCorrectStringIncorrectFormatShouldThrows()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    // ReSharper disable once RedundantCast
                    Uuid _ = Uuid.ParseExact(correctNString.String, "Ъ");
                });
            }
        });
    }

    [Test]
    public void ParseExactEmptyStringCorrectFormatShouldThrows()
    {
        Assert.Multiple(() =>
        {
            foreach (string format in UuidTestData.Formats.All)
            {
                Assert.Throws<FormatException>(() =>
                {
                    Uuid _ = Uuid.ParseExact(string.Empty, format);
                });
            }
        });
    }

    [Test]
    public void ParseExactEmptySpanCorrectFormatShouldThrows()
    {
        Assert.Multiple(() =>
        {
            foreach (string format in UuidTestData.Formats.All)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var formatSpan = new ReadOnlySpan<char>(format.ToCharArray());
                    Uuid _ = Uuid.ParseExact(new ReadOnlySpan<char>(new char[] { }), formatSpan);
                });
            }
        });
    }

    [Test]
    public void ParseExactCorrectSpanEmptyFormatShouldThrows()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var nStringSpan = new ReadOnlySpan<char>(correctNString.String.ToCharArray());
                    var formatSpan = new ReadOnlySpan<char>(new char[] { });
                    // ReSharper disable once RedundantCast
                    Uuid _ = Uuid.ParseExact(nStringSpan, formatSpan);
                });
            }
        });
    }

    [Test]
    public void ParseExactCorrectSpanIncorrectFormatShouldThrows()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var nStringSpan = new ReadOnlySpan<char>(correctNString.String.ToCharArray());
                    var formatSpan = new ReadOnlySpan<char>(new[] { 'Ъ' });
                    // ReSharper disable once RedundantCast
                    Uuid _ = Uuid.ParseExact(nStringSpan, formatSpan);
                });
            }
        });
    }

    #region ParseExactN

    [Test]
    public unsafe void ParseExactCorrectNCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                var results = new List<byte[]>();
                foreach (string format in UuidTestData.Formats.N)
                {
                    Uuid parsedUuidString = Uuid.ParseExact(correctNString.String, format);
                    Uuid parsedUuidSpan = Uuid.ParseExact(
                        new ReadOnlySpan<char>(correctNString.String.ToCharArray()),
                        new ReadOnlySpan<char>(format.ToCharArray()));

                    var actualBytesString = new byte[16];
                    var actualBytesSpan = new byte[16];
                    fixed (byte* pinnedString = actualBytesString, pinnedSpan = actualBytesSpan)
                    {
                        *(Uuid*) pinnedString = parsedUuidString;
                        *(Uuid*) pinnedSpan = parsedUuidSpan;
                    }

                    results.Add(actualBytesString);
                    results.Add(actualBytesSpan);
                }

                foreach (byte[] result in results)
                {
                    Assert.That(result, Is.EqualTo(correctNString.Bytes));
                }
            }
        });
    }

    [Test]
    public void ParseExactCorrectNIncorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                foreach (string format in UuidTestData.Formats.AllExceptN)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(correctNString.String, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(correctNString.String.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    [Test]
    public void ParseExactIncorrectNCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenNString in UuidTestData.BrokenNStrings)
            {
                foreach (string format in UuidTestData.Formats.N)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(brokenNString, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(brokenNString.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    [Test]
    public void ParseExactNotNStringCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctDString in UuidTestData.CorrectDStrings)
            {
                foreach (string format in UuidTestData.Formats.N)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(correctDString.String, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(correctDString.String.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    #endregion

    #region ParseExactD

    [Test]
    public unsafe void ParseExactCorrectDCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctDString in UuidTestData.CorrectDStrings)
            {
                var results = new List<byte[]>();
                foreach (string format in UuidTestData.Formats.D)
                {
                    Uuid parsedUuidString = Uuid.ParseExact(correctDString.String, format);
                    Uuid parsedUuidSpan = Uuid.ParseExact(
                        new ReadOnlySpan<char>(correctDString.String.ToCharArray()),
                        new ReadOnlySpan<char>(format.ToCharArray()));

                    var actualBytesString = new byte[16];
                    var actualBytesSpan = new byte[16];
                    fixed (byte* pinnedString = actualBytesString, pinnedSpan = actualBytesSpan)
                    {
                        *(Uuid*) pinnedString = parsedUuidString;
                        *(Uuid*) pinnedSpan = parsedUuidSpan;
                    }

                    results.Add(actualBytesString);
                    results.Add(actualBytesSpan);
                }

                foreach (byte[] result in results)
                {
                    Assert.That(result, Is.EqualTo(correctDString.Bytes));
                }
            }
        });
    }

    [Test]
    public void ParseExactCorrectDIncorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctDString in UuidTestData.CorrectDStrings)
            {
                foreach (string format in UuidTestData.Formats.AllExceptD)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(correctDString.String, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(correctDString.String.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    [Test]
    public void ParseExactIncorrectDCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenDString in UuidTestData.BrokenDStrings)
            {
                foreach (string format in UuidTestData.Formats.D)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(brokenDString, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(brokenDString.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    [Test]
    public void ParseExactNotDStringCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                foreach (string format in UuidTestData.Formats.D)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(correctNString.String, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(correctNString.String.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    #endregion

    #region ParseExactB

    [Test]
    public unsafe void ParseExactCorrectBCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctBString in UuidTestData.CorrectBStrings)
            {
                var results = new List<byte[]>();
                foreach (string format in UuidTestData.Formats.B)
                {
                    Uuid parsedUuidString = Uuid.ParseExact(correctBString.String, format);
                    Uuid parsedUuidSpan = Uuid.ParseExact(
                        new ReadOnlySpan<char>(correctBString.String.ToCharArray()),
                        new ReadOnlySpan<char>(format.ToCharArray()));

                    var actualBytesString = new byte[16];
                    var actualBytesSpan = new byte[16];
                    fixed (byte* pinnedString = actualBytesString, pinnedSpan = actualBytesSpan)
                    {
                        *(Uuid*) pinnedString = parsedUuidString;
                        *(Uuid*) pinnedSpan = parsedUuidSpan;
                    }

                    results.Add(actualBytesString);
                    results.Add(actualBytesSpan);
                }

                foreach (byte[] result in results)
                {
                    Assert.That(result, Is.EqualTo(correctBString.Bytes));
                }
            }
        });
    }

    [Test]
    public void ParseExactCorrectBIncorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctBString in UuidTestData.CorrectBStrings)
            {
                foreach (string format in UuidTestData.Formats.AllExceptB)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(correctBString.String, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(correctBString.String.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    [Test]
    public void ParseExactIncorrectBCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenBString in UuidTestData.BrokenBStrings)
            {
                foreach (string format in UuidTestData.Formats.B)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(brokenBString, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(brokenBString.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    [Test]
    public void ParseExactNotBStringCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                foreach (string format in UuidTestData.Formats.B)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(correctNString.String, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(correctNString.String.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    #endregion

    #region ParseExactP

    [Test]
    public unsafe void ParseExactCorrectPCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctPString in UuidTestData.CorrectPStrings)
            {
                var results = new List<byte[]>();
                foreach (string format in UuidTestData.Formats.P)
                {
                    Uuid parsedUuidString = Uuid.ParseExact(correctPString.String, format);
                    Uuid parsedUuidSpan = Uuid.ParseExact(
                        new ReadOnlySpan<char>(correctPString.String.ToCharArray()),
                        new ReadOnlySpan<char>(format.ToCharArray()));

                    var actualBytesString = new byte[16];
                    var actualBytesSpan = new byte[16];
                    fixed (byte* pinnedString = actualBytesString, pinnedSpan = actualBytesSpan)
                    {
                        *(Uuid*) pinnedString = parsedUuidString;
                        *(Uuid*) pinnedSpan = parsedUuidSpan;
                    }

                    results.Add(actualBytesString);
                    results.Add(actualBytesSpan);
                }

                foreach (byte[] result in results)
                {
                    Assert.That(result, Is.EqualTo(correctPString.Bytes));
                }
            }
        });
    }

    [Test]
    public void ParseExactCorrectPIncorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctPString in UuidTestData.CorrectPStrings)
            {
                foreach (string format in UuidTestData.Formats.AllExceptP)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(correctPString.String, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(correctPString.String.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    [Test]
    public void ParseExactIncorrectPCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenPString in UuidTestData.BrokenPStrings)
            {
                foreach (string format in UuidTestData.Formats.P)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(brokenPString, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(brokenPString.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    [Test]
    public void ParseExactNotPStringCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                foreach (string format in UuidTestData.Formats.P)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(correctNString.String, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(correctNString.String.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    #endregion

    #region ParseExactX

    [Test]
    public unsafe void ParseExactCorrectXCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctXString in UuidTestData.CorrectXStrings)
            {
                var results = new List<byte[]>();
                foreach (string format in UuidTestData.Formats.X)
                {
                    Uuid parsedUuidString = Uuid.ParseExact(correctXString.String, format);
                    Uuid parsedUuidSpan = Uuid.ParseExact(
                        new ReadOnlySpan<char>(correctXString.String.ToCharArray()),
                        new ReadOnlySpan<char>(format.ToCharArray()));

                    var actualBytesString = new byte[16];
                    var actualBytesSpan = new byte[16];
                    fixed (byte* pinnedString = actualBytesString, pinnedSpan = actualBytesSpan)
                    {
                        *(Uuid*) pinnedString = parsedUuidString;
                        *(Uuid*) pinnedSpan = parsedUuidSpan;
                    }

                    results.Add(actualBytesString);
                    results.Add(actualBytesSpan);
                }

                foreach (byte[] result in results)
                {
                    Assert.That(result, Is.EqualTo(correctXString.Bytes));
                }
            }
        });
    }

    [Test]
    public void ParseExactCorrectXIncorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctXString in UuidTestData.CorrectXStrings)
            {
                foreach (string format in UuidTestData.Formats.AllExceptX)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(correctXString.String, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(correctXString.String.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    [Test]
    public void ParseExactIncorrectXCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (string brokenXString in UuidTestData.BrokenXStrings)
            {
                foreach (string format in UuidTestData.Formats.X)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(brokenXString, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(brokenXString.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    [Test]
    public void ParseExactNotXStringCorrectFormat()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes correctNString in UuidTestData.CorrectNStrings)
            {
                foreach (string format in UuidTestData.Formats.X)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(correctNString.String, format);
                    });

                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.ParseExact(
                            new ReadOnlySpan<char>(correctNString.String.ToCharArray()),
                            new ReadOnlySpan<char>(format.ToCharArray()));
                    });
                }
            }
        });
    }

    #endregion
}
