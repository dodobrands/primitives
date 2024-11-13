using System;
using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidToStringTests
{
    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void ToString(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringN(correctBytes);

        var actualString = uuid.ToString();

        Assert.That(actualString, Is.EqualTo(expectedString));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void ToStringNullFormat(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringN(correctBytes);

        var actualString = uuid.ToString(null);

        Assert.That(actualString, Is.EqualTo(expectedString));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void ToStringEmptyFormat(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringN(correctBytes);

        var actualString = uuid.ToString(string.Empty);

        Assert.That(actualString, Is.EqualTo(expectedString));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void ToStringIncorrectFormat(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);

        Assert.Throws<FormatException>(() =>
        {
            var _ = uuid.ToString("Ъ");
        });
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void ToStringTooLongFormat(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);

        Assert.Throws<FormatException>(() =>
        {
            var _ = uuid.ToString("NN");
        });
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void ToStringN(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringN(correctBytes);

        var actualString = uuid.ToString("N");

        Assert.That(actualString, Is.EqualTo(expectedString));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void ToStringD(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringD(correctBytes);

        var actualString = uuid.ToString("D");

        Assert.That(actualString, Is.EqualTo(expectedString));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void ToStringB(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringB(correctBytes);

        var actualString = uuid.ToString("B");

        Assert.That(actualString, Is.EqualTo(expectedString));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void ToStringP(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringP(correctBytes);

        var actualString = uuid.ToString("P");

        Assert.That(actualString, Is.EqualTo(expectedString));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void ToStringX(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        string expectedString = UuidTestsUtils.GetStringX(correctBytes);

        var actualString = uuid.ToString("X");

        Assert.That(actualString, Is.EqualTo(expectedString));
    }
}
