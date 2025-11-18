using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidPropertiesTests
{
    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.GetAllUuidVersions))]
    public void VersionHasCorrectValue(string uuidString, int expectedVersion)
    {
        var uuid = new Uuid(uuidString);
        Assert.That(uuid.Version, Is.EqualTo(expectedVersion));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.GetAllUuidVariants))]
    public void VariantHasCorrectValue(string uuidString, int expectedVariant)
    {
        var uuid = new Uuid(uuidString);
        Assert.That(uuid.Variant, Is.EqualTo(expectedVariant));
    }

#if NET9_0_OR_GREATER
    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.GetAllUuidVersions))]
    public void VersionHasCorrectValueThatMathGuidOnNet9(string uuidString, int expectedVersion)
    {
        var uuid = new Uuid(uuidString);
        var guid = new System.Guid(uuidString);
        Assert.That(uuid.Version, Is.EqualTo(expectedVersion));
        Assert.That(uuid.Version, Is.EqualTo(guid.Version));
    }

    [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.GetAllUuidVariants))]
    public void VariantHasCorrectValueThatMathGuidOnNet9(string uuidString, int expectedVariant)
    {
        var uuid = new Uuid(uuidString);
        var guid = new System.Guid(uuidString);
        Assert.That(uuid.Variant, Is.EqualTo(expectedVariant));
        Assert.That(uuid.Variant, Is.EqualTo(guid.Variant));
    }
#endif
}
