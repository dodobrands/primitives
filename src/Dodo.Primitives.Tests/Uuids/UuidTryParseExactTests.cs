using System;
using Dodo.Primitives.Tests.Uuids.Data;
using Dodo.Primitives.Tests.Uuids.Data.Models;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidTryParseExactTests
{
    private const string? NullString = null;

    [Test]
    public void TryParseExactNullStringCorrectFormatShouldFalse()
    {
        foreach (var format in UuidTestData.Formats.All)
        {
            bool parsed = Uuid.TryParseExact(NullString, format, out Uuid uuid);
            Assert.Multiple(() =>
            {
                Assert.False(parsed);
                Assert.AreEqual(Uuid.Empty, uuid);
            });
        }
    }

    [Test]
    public void TryParseExactCorrectStringNullFormatShouldFalse()
    {
        Assert.Multiple(() =>
        {
            foreach (var correctNString in UuidTestData.CorrectNStrings)
            {
#pragma warning disable 8625
                bool parsed = Uuid.TryParseExact(correctNString.String, NullString, out Uuid uuid);
#pragma warning restore 8625
                Assert.False(parsed);
                Assert.AreEqual(Uuid.Empty, uuid);
            }
        });
    }

    [Test]
    public void TryParseExactCorrectStringIncorrectFormatShouldFalse()
    {
        Assert.Multiple(() =>
        {
            foreach (var correctNString in UuidTestData.CorrectNStrings)
            {
                bool parsed = Uuid.TryParseExact(correctNString.String, "Ъ", out Uuid uuid);
                Assert.False(parsed);
                Assert.AreEqual(Uuid.Empty, uuid);
            }
        });
    }

    [Test]
    public void TryParseExactEmptyStringCorrectFormatShouldFalse()
    {
        Assert.Multiple(() =>
        {
            foreach (var format in UuidTestData.Formats.All)
            {
                bool parsed = Uuid.TryParseExact(string.Empty, format, out Uuid uuid);
                Assert.False(parsed);
                Assert.AreEqual(Uuid.Empty, uuid);
            }
        });
    }

    [Test]
    public void TryParseExactEmptySpanCorrectFormatShouldFalse()
    {
        Assert.Multiple(() =>
        {
            foreach (var format in UuidTestData.Formats.All)
            {
                var stringSpan = new ReadOnlySpan<char>(new char[] { });
                var formatSpan = new ReadOnlySpan<char>(format.ToCharArray());
                bool parsed = Uuid.TryParseExact(stringSpan, formatSpan, out Uuid uuid);
                Assert.False(parsed);
                Assert.AreEqual(Uuid.Empty, uuid);
            }
        });
    }

    [Test]
    public void TryParseExactCorrectSpanEmptyFormatShouldFalse()
    {
        Assert.Multiple(() =>
        {
            foreach (var correctNString in UuidTestData.CorrectNStrings)
            {
                var stringSpan = new ReadOnlySpan<char>(correctNString.String.ToCharArray());
                var formatSpan = new ReadOnlySpan<char>(new char[] { });
                bool parsed = Uuid.TryParseExact(stringSpan, formatSpan, out Uuid uuid);
                Assert.False(parsed);
                Assert.AreEqual(Uuid.Empty, uuid);
            }
        });
    }

    [Test]
    public void TryParseExactCorrectSpanIncorrectFormatShouldFalse()
    {
        Assert.Multiple(() =>
        {
            foreach (var correctNString in UuidTestData.CorrectNStrings)
            {
                var stringSpan = new ReadOnlySpan<char>(correctNString.String.ToCharArray());
                var formatSpan = new ReadOnlySpan<char>(new[] {'Ъ'});
                bool parsed = Uuid.TryParseExact(stringSpan, formatSpan, out Uuid uuid);
                Assert.False(parsed);
                Assert.AreEqual(Uuid.Empty, uuid);
            }
        });
    }

    #region TryParseExactN

    [Test]
    public void TryParseExactCorrectNCorrectFormat()
    {
        TryParseExactCorrectStringCorrectFormat(
            UuidTestData.CorrectNStrings,
            UuidTestData.Formats.N);
    }

    [Test]
    public void ParseExactCorrectNIncorrectFormat()
    {
        TryParseExactCorrectStringIncorrectFormat(
            UuidTestData.CorrectNStrings,
            UuidTestData.Formats.AllExceptN);
    }

    [Test]
    public void ParseExactIncorrectNCorrectFormat()
    {
        TryParseExactIncorrectStringCorrectFormat(
            UuidTestData.BrokenNStrings,
            UuidTestData.Formats.N);
    }

    [Test]
    public void ParseExactNotNStringCorrectFormat()
    {
        TryParseExactOtherFormatStringCorrectFormat(
            UuidTestData.CorrectDStrings,
            UuidTestData.Formats.N);
    }

    #endregion

    #region TryParseExactD

    [Test]
    public void TryParseExactCorrectDCorrectFormat()
    {
        TryParseExactCorrectStringCorrectFormat(
            UuidTestData.CorrectDStrings,
            UuidTestData.Formats.D);
    }

    [Test]
    public void ParseExactCorrectDIncorrectFormat()
    {
        TryParseExactCorrectStringIncorrectFormat(
            UuidTestData.CorrectDStrings,
            UuidTestData.Formats.AllExceptD);
    }

    [Test]
    public void ParseExactIncorrectDCorrectFormat()
    {
        TryParseExactIncorrectStringCorrectFormat(
            UuidTestData.BrokenDStrings,
            UuidTestData.Formats.D);
    }

    [Test]
    public void ParseExactNotDStringCorrectFormat()
    {
        TryParseExactOtherFormatStringCorrectFormat(
            UuidTestData.CorrectNStrings,
            UuidTestData.Formats.D);
    }

    #endregion

    #region TryParseExactB

    [Test]
    public void TryParseExactCorrectBCorrectFormat()
    {
        TryParseExactCorrectStringCorrectFormat(
            UuidTestData.CorrectBStrings,
            UuidTestData.Formats.B);
    }

    [Test]
    public void ParseExactCorrectBIncorrectFormat()
    {
        TryParseExactCorrectStringIncorrectFormat(
            UuidTestData.CorrectBStrings,
            UuidTestData.Formats.AllExceptB);
    }

    [Test]
    public void ParseExactIncorrectBCorrectFormat()
    {
        TryParseExactIncorrectStringCorrectFormat(
            UuidTestData.BrokenBStrings,
            UuidTestData.Formats.B);
    }

    [Test]
    public void ParseExactNotBStringCorrectFormat()
    {
        TryParseExactOtherFormatStringCorrectFormat(
            UuidTestData.CorrectNStrings,
            UuidTestData.Formats.B);
    }

    #endregion

    #region TryParseExactP

    [Test]
    public void TryParseExactCorrectPCorrectFormat()
    {
        TryParseExactCorrectStringCorrectFormat(
            UuidTestData.CorrectPStrings,
            UuidTestData.Formats.P);
    }

    [Test]
    public void ParseExactCorrectPIncorrectFormat()
    {
        TryParseExactCorrectStringIncorrectFormat(
            UuidTestData.CorrectPStrings,
            UuidTestData.Formats.AllExceptP);
    }

    [Test]
    public void ParseExactIncorrectPCorrectFormat()
    {
        TryParseExactIncorrectStringCorrectFormat(
            UuidTestData.BrokenPStrings,
            UuidTestData.Formats.P);
    }

    [Test]
    public void ParseExactNotPStringCorrectFormat()
    {
        TryParseExactOtherFormatStringCorrectFormat(
            UuidTestData.CorrectNStrings,
            UuidTestData.Formats.P);
    }

    #endregion

    #region TryParseExactX

    [Test]
    public void TryParseExactCorrectXCorrectFormat()
    {
        TryParseExactCorrectStringCorrectFormat(
            UuidTestData.CorrectXStrings,
            UuidTestData.Formats.X);
    }

    [Test]
    public void ParseExactCorrectXIncorrectFormat()
    {
        TryParseExactCorrectStringIncorrectFormat(
            UuidTestData.CorrectXStrings,
            UuidTestData.Formats.AllExceptX);
    }

    [Test]
    public void ParseExactIncorrectXCorrectFormat()
    {
        TryParseExactIncorrectStringCorrectFormat(
            UuidTestData.BrokenXStrings,
            UuidTestData.Formats.X);
    }

    [Test]
    public void ParseExactNotXStringCorrectFormat()
    {
        TryParseExactOtherFormatStringCorrectFormat(
            UuidTestData.CorrectNStrings,
            UuidTestData.Formats.X);
    }

    #endregion


    #region Helpers

    private unsafe void TryParseExactCorrectStringCorrectFormat(
        UuidStringWithBytes[] correctStrings,
        string[] correctFormats)
    {
        Assert.Multiple(() =>
        {
            foreach (var correctString in correctStrings)
            foreach (var format in correctFormats)
            {
                bool isParsedFromString = Uuid.TryParseExact(
                    correctString.String,
                    format,
                    out Uuid parsedUuidFromString);
                bool isParsedBoolFromSpan = Uuid.TryParseExact(
                    new ReadOnlySpan<char>(correctString.String.ToCharArray()),
                    new ReadOnlySpan<char>(format.ToCharArray()),
                    out Uuid parsedUuidFromSpan);

                var actualBytesString = new byte[16];
                var actualBytesSpan = new byte[16];
                fixed (byte* pinnedString = actualBytesString, pinnedSpan = actualBytesSpan)
                {
                    *(Uuid*) pinnedString = parsedUuidFromString;
                    *(Uuid*) pinnedSpan = parsedUuidFromSpan;
                }

                Assert.True(isParsedFromString);
                Assert.True(isParsedBoolFromSpan);
                Assert.AreEqual(correctString.Bytes, actualBytesString);
                Assert.AreEqual(correctString.Bytes, actualBytesSpan);
            }
        });
    }

    private static readonly byte[] ExpectedEmptyUuidBytes = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

    private unsafe void TryParseExactCorrectStringIncorrectFormat(
        UuidStringWithBytes[] correctStrings,
        string[] incorrectFormats)
    {
        Assert.Multiple(() =>
        {
            foreach (var correctString in correctStrings)
            foreach (var incorrectFormat in incorrectFormats)
            {
                bool isParsedFromString = Uuid.TryParseExact(
                    correctString.String,
                    incorrectFormat,
                    out Uuid parsedUuidFromString);
                bool isParsedBoolFromSpan = Uuid.TryParseExact(
                    new ReadOnlySpan<char>(correctString.String.ToCharArray()),
                    new ReadOnlySpan<char>(incorrectFormat.ToCharArray()),
                    out Uuid parsedUuidFromSpan);

                var actualBytesString = new byte[16];
                var actualBytesSpan = new byte[16];
                fixed (byte* pinnedString = actualBytesString, pinnedSpan = actualBytesSpan)
                {
                    *(Uuid*) pinnedString = parsedUuidFromString;
                    *(Uuid*) pinnedSpan = parsedUuidFromSpan;
                }

                Assert.False(isParsedFromString);
                Assert.False(isParsedBoolFromSpan);
                Assert.AreEqual(ExpectedEmptyUuidBytes, actualBytesString);
                Assert.AreEqual(ExpectedEmptyUuidBytes, actualBytesSpan);
            }
        });
    }

    private unsafe void TryParseExactIncorrectStringCorrectFormat(
        string[] brokenStrings,
        string[] correctFormats)
    {
        Assert.Multiple(() =>
        {
            foreach (var brokenString in brokenStrings)
            foreach (var correctFormat in correctFormats)
            {
                bool isParsedFromString = Uuid.TryParseExact(
                    brokenString,
                    correctFormat,
                    out Uuid parsedUuidFromString);
                bool isParsedBoolFromSpan = Uuid.TryParseExact(
                    new ReadOnlySpan<char>(brokenString.ToCharArray()),
                    new ReadOnlySpan<char>(correctFormat.ToCharArray()),
                    out Uuid parsedUuidFromSpan);

                var actualBytesString = new byte[16];
                var actualBytesSpan = new byte[16];
                fixed (byte* pinnedString = actualBytesString, pinnedSpan = actualBytesSpan)
                {
                    *(Uuid*) pinnedString = parsedUuidFromString;
                    *(Uuid*) pinnedSpan = parsedUuidFromSpan;
                }

                Assert.False(isParsedFromString);
                Assert.False(isParsedBoolFromSpan);
                Assert.AreEqual(ExpectedEmptyUuidBytes, actualBytesString);
                Assert.AreEqual(ExpectedEmptyUuidBytes, actualBytesSpan);
            }
        });
    }

    private unsafe void TryParseExactOtherFormatStringCorrectFormat(
        UuidStringWithBytes[] otherFormatStrings,
        string[] correctFormats)
    {
        Assert.Multiple(() =>
        {
            foreach (var otherFormatString in otherFormatStrings)
            foreach (var correctFormat in correctFormats)
            {
                bool isParsedFromString = Uuid.TryParseExact(
                    otherFormatString.String,
                    correctFormat,
                    out Uuid parsedUuidFromString);
                bool isParsedBoolFromSpan = Uuid.TryParseExact(
                    new ReadOnlySpan<char>(otherFormatString.String.ToCharArray()),
                    new ReadOnlySpan<char>(correctFormat.ToCharArray()),
                    out Uuid parsedUuidFromSpan);

                var actualBytesString = new byte[16];
                var actualBytesSpan = new byte[16];
                fixed (byte* pinnedString = actualBytesString, pinnedSpan = actualBytesSpan)
                {
                    *(Uuid*) pinnedString = parsedUuidFromString;
                    *(Uuid*) pinnedSpan = parsedUuidFromSpan;
                }

                Assert.False(isParsedFromString);
                Assert.False(isParsedBoolFromSpan);
                Assert.AreEqual(ExpectedEmptyUuidBytes, actualBytesString);
                Assert.AreEqual(ExpectedEmptyUuidBytes, actualBytesSpan);
            }
        });
    }

    #endregion
}