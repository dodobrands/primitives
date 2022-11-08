using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Hex;

public class HexTests
{
    #region IsHexString

    [TestCaseSource(typeof(HexTestsData), nameof(HexTestsData.ValidHexStrings))]
    public void IsHexStringCorrect(string validHexString)
    {
        Assert.True(Primitives.Hex.IsHexString(validHexString));
    }

    [Test]
    public void IsHexStringReturnFalseWhenNull()
    {
        Assert.False(Primitives.Hex.IsHexString(null));
    }

    [Test]
    public void IsHexStringReturnFalseWhenOddStringLength()
    {
        Assert.False(Primitives.Hex.IsHexString("F"));
    }

    [TestCaseSource(typeof(HexTestsData), nameof(HexTestsData.BrokenHexStrings))]
    public void IsHexStringReturnFalseWithBrokenStrings(string brokenHexString)
    {
        Assert.False(Primitives.Hex.IsHexString(brokenHexString));
    }

    #endregion

    #region GetBytes

    [TestCaseSource(typeof(HexTestsData), nameof(HexTestsData.ValidHexStrings))]
    public void CanGetBytesCorrect(string validHexString)
    {
        byte[] expected = HexStringToByteArrayNaive(validHexString);
        byte[]? actual = Primitives.Hex.GetBytes(validHexString);
        Assert.Multiple(() =>
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);
            Assert.True(expected!.Length == actual!.Length);
            Assert.True(expected.SequenceEqual(actual));
        });
    }

    [Test]
    public void CanGetBytesReturnNullWhenNull()
    {
        Assert.Null(Primitives.Hex.GetBytes(null!));
    }

    [Test]
    public void CanGetBytesReturnNullWhenOddStringLength()
    {
        Assert.Null(Primitives.Hex.GetBytes("fff"));
    }

    [TestCaseSource(typeof(HexTestsData), nameof(HexTestsData.BrokenHexStrings))]
    public void CanGetBytesReturnNullWithBrokenStrings(string brokenHexString)
    {
        Assert.Null(Primitives.Hex.GetBytes(brokenHexString));
    }

    #endregion

    #region GetString

    [TestCaseSource(typeof(HexTestsData), nameof(HexTestsData.ValidByteArrays))]
    public void CanGetStringCorrect(byte[] bytesToHex)
    {
        string expected = ByteArrayToHexStringNaive(bytesToHex);
        string? actual = Primitives.Hex.GetString(bytesToHex);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CanGetStringReturnNullWhenArrayIsNull()
    {
        Assert.Null(Primitives.Hex.GetString(null!));
    }

    [Test]
    public void CanGetStringReturnEmptyStringWhenArrayIsEmpty()
    {
        Assert.AreEqual(string.Empty, Primitives.Hex.GetString(new byte[] { }));
    }

    #endregion

    #region Utils

    private static byte[] HexStringToByteArrayNaive(string hex)
    {
        return Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
    }

    private static string ByteArrayToHexStringNaive(byte[] bytes)
    {
        var builder = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
        {
            builder.AppendFormat("{0:x2}", b);
        }

        return builder.ToString();
    }

    #endregion
}