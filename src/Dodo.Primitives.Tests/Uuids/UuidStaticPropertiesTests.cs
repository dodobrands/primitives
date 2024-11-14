using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidStaticPropertiesTests
{
    [Test]
    public void AllBitsSetReturnsUuidWithByteMaxValues()
    {
        var expected = new byte[16];
        for (var i = 0; i < expected.Length; i++)
        {
            expected[i] = 0xFF;
        }

        byte[] actual = Uuid.AllBitsSet.ToByteArray();
        Assert.That(actual, Is.EquivalentTo(expected));
    }
}
