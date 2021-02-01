using System;
using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids
{
    public class UuidParseTests
    {
        private const string? NullString = null;

        [Test]
        public void ParseNullStringShouldThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
#nullable disable
                Uuid _ = Uuid.Parse(NullString!);
#nullable restore
            });
        }

        [Test]
        public void ParseEmptyStringShouldThrows()
        {
            Assert.Throws<FormatException>(() =>
            {
                Uuid _ = Uuid.Parse(string.Empty);
            });
        }

        [Test]
        public void ParseEmptySpanShouldThrows()
        {
            Assert.Throws<FormatException>(() =>
            {
                Uuid _ = Uuid.Parse(new ReadOnlySpan<char>(new char[] { }));
            });
        }

        #region ParseN

        [Test]
        public unsafe void ParseCorrectNString()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctNString in UuidTestData.CorrectNStrings)
                {
                    string nString = correctNString.String;
                    byte[] expectedBytes = correctNString.Bytes;

                    Uuid parsedUuid = Uuid.Parse(nString);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public unsafe void ParseCorrectNSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctNString in UuidTestData.CorrectNStrings)
                {
                    var nSpan = new ReadOnlySpan<char>(correctNString.String.ToCharArray());
                    byte[] expectedBytes = correctNString.Bytes;

                    Uuid parsedUuid = Uuid.Parse(nSpan);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public void ParseNIncorrectLargeString()
        {
            Assert.Multiple(() =>
            {
                foreach (var largeNString in UuidTestData.LargeNStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(largeNString);
                    });
                }
            });
        }

        [Test]
        public void ParseNIncorrectLargeSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var largeNString in UuidTestData.LargeNStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var largeNSpan = new ReadOnlySpan<char>(largeNString.ToCharArray());
                        Uuid _ = Uuid.Parse(largeNSpan);
                    });
                }
            });
        }

        [Test]
        public void ParseNIncorrectSmallString()
        {
            Assert.Multiple(() =>
            {
                foreach (var smallNString in UuidTestData.SmallNStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(smallNString);
                    });
                }
            });
        }

        [Test]
        public void ParseNIncorrectSmallSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var smallNString in UuidTestData.SmallNStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var smallNSpan = new ReadOnlySpan<char>(smallNString.ToCharArray());
                        Uuid _ = Uuid.Parse(smallNSpan);
                    });
                }
            });
        }

        [Test]
        public void ParseIncorrectNString()
        {
            Assert.Multiple(() =>
            {
                foreach (var brokenNString in UuidTestData.BrokenNStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(brokenNString);
                    });
                }
            });
        }

        [Test]
        public void ParseIncorrectNSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var brokenNString in UuidTestData.BrokenNStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var brokenNSpan = new ReadOnlySpan<char>(brokenNString.ToCharArray());
                        Uuid _ = Uuid.Parse(brokenNSpan);
                    });
                }
            });
        }

        #endregion

        #region ParseD

        [Test]
        public unsafe void ParseCorrectDString()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctDString in UuidTestData.CorrectDStrings)
                {
                    string dString = correctDString.String;
                    byte[] expectedBytes = correctDString.Bytes;

                    Uuid parsedUuid = Uuid.Parse(dString);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public unsafe void ParseCorrectDSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctDString in UuidTestData.CorrectDStrings)
                {
                    var dSpan = new ReadOnlySpan<char>(correctDString.String.ToCharArray());
                    byte[] expectedBytes = correctDString.Bytes;

                    Uuid parsedUuid = Uuid.Parse(dSpan);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public void ParseDIncorrectLargeString()
        {
            Assert.Multiple(() =>
            {
                foreach (var largeDString in UuidTestData.LargeDStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(largeDString);
                    });
                }
            });
        }

        [Test]
        public void ParseDIncorrectLargeSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var largeDString in UuidTestData.LargeDStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var largeDSpan = new ReadOnlySpan<char>(largeDString.ToCharArray());
                        Uuid _ = Uuid.Parse(largeDSpan);
                    });
                }
            });
        }

        [Test]
        public void ParseDIncorrectSmallString()
        {
            Assert.Multiple(() =>
            {
                foreach (var smallDString in UuidTestData.SmallDStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(smallDString);
                    });
                }
            });
        }

        [Test]
        public void ParseDIncorrectSmallSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var smallDString in UuidTestData.SmallDStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var smallDSpan = new ReadOnlySpan<char>(smallDString.ToCharArray());
                        Uuid _ = Uuid.Parse(smallDSpan);
                    });
                }
            });
        }

        [Test]
        public void ParseIncorrectDString()
        {
            Assert.Multiple(() =>
            {
                foreach (var brokenDString in UuidTestData.BrokenDStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(brokenDString);
                    });
                }
            });
        }

        [Test]
        public void ParseIncorrectDSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var brokenDString in UuidTestData.BrokenDStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var brokenDSpan = new ReadOnlySpan<char>(brokenDString.ToCharArray());
                        Uuid _ = Uuid.Parse(brokenDSpan);
                    });
                }
            });
        }

        #endregion

        #region ParseB

        [Test]
        public unsafe void ParseCorrectBString()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctBString in UuidTestData.CorrectBStrings)
                {
                    string bString = correctBString.String;
                    byte[] expectedBytes = correctBString.Bytes;

                    Uuid parsedUuid = Uuid.Parse(bString);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public unsafe void ParseCorrectBSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctBString in UuidTestData.CorrectBStrings)
                {
                    var bSpan = new ReadOnlySpan<char>(correctBString.String.ToCharArray());
                    byte[] expectedBytes = correctBString.Bytes;

                    Uuid parsedUuid = Uuid.Parse(bSpan);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public void ParseBIncorrectLargeString()
        {
            Assert.Multiple(() =>
            {
                foreach (var largeBString in UuidTestData.LargeBStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(largeBString);
                    });
                }
            });
        }

        [Test]
        public void ParseBIncorrectLargeSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var largeBString in UuidTestData.LargeBStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var largeBSpan = new ReadOnlySpan<char>(largeBString.ToCharArray());
                        Uuid _ = Uuid.Parse(largeBSpan);
                    });
                }
            });
        }

        [Test]
        public void ParseBIncorrectSmallString()
        {
            Assert.Multiple(() =>
            {
                foreach (var smallBString in UuidTestData.SmallBStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(smallBString);
                    });
                }
            });
        }

        [Test]
        public void ParseBIncorrectSmallSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var smallBString in UuidTestData.SmallBStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var smallBSpan = new ReadOnlySpan<char>(smallBString.ToCharArray());
                        Uuid _ = Uuid.Parse(smallBSpan);
                    });
                }
            });
        }

        [Test]
        public void ParseIncorrectBString()
        {
            Assert.Multiple(() =>
            {
                foreach (var brokenBString in UuidTestData.BrokenBStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(brokenBString);
                    });
                }
            });
        }

        [Test]
        public void ParseIncorrectBSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var brokenBString in UuidTestData.BrokenBStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var brokenBSpan = new ReadOnlySpan<char>(brokenBString.ToCharArray());
                        Uuid _ = Uuid.Parse(brokenBSpan);
                    });
                }
            });
        }

        #endregion

        #region ParseP

        [Test]
        public unsafe void ParseCorrectPString()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctPString in UuidTestData.CorrectPStrings)
                {
                    string pString = correctPString.String;
                    byte[] expectedBytes = correctPString.Bytes;

                    Uuid parsedUuid = Uuid.Parse(pString);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public unsafe void ParseCorrectPSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctPString in UuidTestData.CorrectPStrings)
                {
                    var pSpan = new ReadOnlySpan<char>(correctPString.String.ToCharArray());
                    byte[] expectedBytes = correctPString.Bytes;

                    Uuid parsedUuid = Uuid.Parse(pSpan);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public void ParsePIncorrectLargeString()
        {
            Assert.Multiple(() =>
            {
                foreach (var largePString in UuidTestData.LargePStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(largePString);
                    });
                }
            });
        }

        [Test]
        public void ParsePIncorrectLargeSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var largePString in UuidTestData.LargePStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var largePSpan = new ReadOnlySpan<char>(largePString.ToCharArray());
                        Uuid _ = Uuid.Parse(largePSpan);
                    });
                }
            });
        }

        [Test]
        public void ParsePIncorrectSmallString()
        {
            Assert.Multiple(() =>
            {
                foreach (var smallPString in UuidTestData.SmallPStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(smallPString);
                    });
                }
            });
        }

        [Test]
        public void ParsePIncorrectSmallSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var smallPString in UuidTestData.SmallPStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var smallPSpan = new ReadOnlySpan<char>(smallPString.ToCharArray());
                        Uuid _ = Uuid.Parse(smallPSpan);
                    });
                }
            });
        }

        [Test]
        public void ParseIncorrectPString()
        {
            Assert.Multiple(() =>
            {
                foreach (var brokenPString in UuidTestData.BrokenPStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(brokenPString);
                    });
                }
            });
        }

        [Test]
        public void ParseIncorrectPSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var brokenPString in UuidTestData.BrokenPStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var brokenPSpan = new ReadOnlySpan<char>(brokenPString.ToCharArray());
                        Uuid _ = Uuid.Parse(brokenPSpan);
                    });
                }
            });
        }

        #endregion

        #region ParseX

        [Test]
        public unsafe void ParseCorrectXString()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctXString in UuidTestData.CorrectXStrings)
                {
                    string xString = correctXString.String;
                    byte[] expectedBytes = correctXString.Bytes;

                    Uuid parsedUuid = Uuid.Parse(xString);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public unsafe void ParseCorrectXSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctXString in UuidTestData.CorrectXStrings)
                {
                    var xSpan = new ReadOnlySpan<char>(correctXString.String.ToCharArray());
                    byte[] expectedBytes = correctXString.Bytes;

                    Uuid parsedUuid = Uuid.Parse(xSpan);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public void ParseXIncorrectLargeString()
        {
            Assert.Multiple(() =>
            {
                foreach (var largeXString in UuidTestData.LargeXStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(largeXString);
                    });
                }
            });
        }

        [Test]
        public void ParseXIncorrectLargeSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var largeXString in UuidTestData.LargeXStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var largeXSpan = new ReadOnlySpan<char>(largeXString.ToCharArray());
                        Uuid _ = Uuid.Parse(largeXSpan);
                    });
                }
            });
        }

        [Test]
        public void ParseXIncorrectSmallString()
        {
            Assert.Multiple(() =>
            {
                foreach (var smallXString in UuidTestData.SmallXStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(smallXString);
                    });
                }
            });
        }

        [Test]
        public void ParseXIncorrectSmallSpan()
        {
            Assert.Multiple(() =>
            {
                foreach (var smallXString in UuidTestData.SmallXStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        var smallXSpan = new ReadOnlySpan<char>(smallXString.ToCharArray());
                        Uuid _ = Uuid.Parse(smallXSpan);
                    });
                }
            });
        }

        [Test]
        public void ParseIncorrectXString()
        {
            Assert.Multiple(() =>
            {
                foreach (var brokenXString in UuidTestData.BrokenXStrings)
                {
                    Assert.Throws<FormatException>(() =>
                    {
                        Uuid _ = Uuid.Parse(brokenXString);
                    });
                }
            });
        }

        [Test]
        public void ParseIncorrectXSpan()
        {
            foreach (var brokenXString in UuidTestData.BrokenXStrings)
            {
                Assert.Throws<FormatException>(() =>
                {
                    var brokenXSpan = new ReadOnlySpan<char>(brokenXString.ToCharArray());
                    Uuid _ = Uuid.Parse(brokenXSpan);
                });
            }
        }

        #endregion
    }
}
