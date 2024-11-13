using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidToByteArrayTests
{
    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
    public void ToByteArray(byte[] correctBytes)
    {
        var uuid = new Uuid(correctBytes);

        byte[] uuidBytes = uuid.ToByteArray();

        Assert.That(uuidBytes, Is.EqualTo(correctBytes));
    }
}
