using System;
using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidCompareToTests
{
    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectCompareToArraysAndResult))]
    public void CompareToObjectCorrect(
        byte[] correctBytes,
        byte[] correctCompareToBytes,
        int expectedResult)
    {
        var uuid = new Uuid(correctBytes);
        var uuidToCompareAsObject = (object) new Uuid(correctCompareToBytes);

        int compareResult = uuid.CompareTo(uuidToCompareAsObject);

        Assert.AreEqual(expectedResult, compareResult);
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void CompareToObjectNullShouldReturn1(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);

        int compareResult = uuid.CompareTo(null);

        Assert.AreEqual(1, compareResult);
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void CompareToObjectOtherTypeShouldThrows(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);

        Assert.Throws<ArgumentException>(() =>
        {
            int _ = uuid.CompareTo(1337);
        });
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectCompareToArraysAndResult))]
    public void CompareToUuidCorrect(
        byte[] correctBytes,
        byte[] correctCompareToBytes,
        int expectedResult)
    {
        var uuid = new Uuid(correctBytes);
        var uuidToCompareAsObject = new Uuid(correctCompareToBytes);

        int compareResult = uuid.CompareTo(uuidToCompareAsObject);

        Assert.AreEqual(expectedResult, compareResult);
    }
}
