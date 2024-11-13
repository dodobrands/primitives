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
        Assert.That(Primitives.Hex.IsHexString(validHexString));
    }

    [Test]
    public void IsHexStringReturnFalseWhenNull()
    {
        Assert.That(Primitives.Hex.IsHexString(null), Is.False);
    }

    [Test]
    public void IsHexStringReturnFalseWhenOddStringLength()
    {
        Assert.That(Primitives.Hex.IsHexString("F"), Is.False);
    }

    [TestCaseSource(typeof(HexTestsData), nameof(HexTestsData.BrokenHexStrings))]
    public void IsHexStringReturnFalseWithBrokenStrings(string brokenHexString)
    {
        Assert.That(Primitives.Hex.IsHexString(brokenHexString), Is.False);
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
            Assert.That(expected, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(expected!.Length == actual!.Length, Is.True);
            Assert.That(expected.SequenceEqual(actual), Is.True);
        });
    }

    [Test]
    public void CanGetBytesReturnNullWhenNull()
    {
        Assert.That(Primitives.Hex.GetBytes(null!), Is.Null);
    }

    [Test]
    public void CanGetBytesReturnNullWhenOddStringLength()
    {
        Assert.That(Primitives.Hex.GetBytes("fff"), Is.Null);
    }

    [TestCaseSource(typeof(HexTestsData), nameof(HexTestsData.BrokenHexStrings))]
    public void CanGetBytesReturnNullWithBrokenStrings(string brokenHexString)
    {
        Assert.That(Primitives.Hex.GetBytes(brokenHexString), Is.Null);
    }

    #endregion

    #region GetString

    [TestCaseSource(typeof(HexTestsData), nameof(HexTestsData.ValidByteArrays))]
    public void CanGetStringCorrect(byte[] bytesToHex)
    {
        string expected = ByteArrayToHexStringNaive(bytesToHex);
        string? actual = Primitives.Hex.GetString(bytesToHex);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CanGetStringReturnNullWhenArrayIsNull()
    {
        Assert.That(Primitives.Hex.GetString(null!), Is.Null);
    }

    [Test]
    public void CanGetStringReturnEmptyStringWhenArrayIsEmpty()
    {
        Assert.That(Primitives.Hex.GetString(new byte[] { }), Is.EqualTo(string.Empty));
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
