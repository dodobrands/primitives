using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidGetHashCodeTests
{
    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public unsafe void GetHashCode(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);
        Uuid* uuidPtr = stackalloc Uuid[1];
        uuidPtr[0] = uuid;
        var intPtr = (int*)uuidPtr;
        int int0 = intPtr[0];
        int int1 = intPtr[1];
        int int2 = intPtr[2];
        int int3 = intPtr[3];
        int expectedHashCode = int0 ^ int1 ^ int2 ^ int3;

        int actualHashCode = uuid.GetHashCode();

        Assert.AreEqual(expectedHashCode, actualHashCode);
    }
}
