using System;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidTypeConverterTests
{
    [TestCase(typeof(string))]
    [TestCase(typeof(InstanceDescriptor))]
    public void CanConvertToCorrect(Type type)
    {
        var converter = new UuidTypeConverter();
        Assert.That(converter.CanConvertTo(type));
    }

    [Test]
    public void CanConvertFromCorrect()
    {
        var converter = new UuidTypeConverter();
        Assert.That(converter.CanConvertFrom(typeof(string)));
    }

    [Test]
    public void CanConvertFromInt32()
    {
        var converter = new UuidTypeConverter();
        Assert.That(converter.CanConvertFrom(typeof(int)), Is.False);
    }

    [Test]
    public void ConvertNotUuidToStringWillCallOverrideToString()
    {
        var expectedValue = "133742";
        var notUuid = new NotUuid(133742);
        var converter = new UuidTypeConverter();

        object? actualValue = converter.ConvertTo(notUuid, typeof(string));

        Assert.That(actualValue, Is.Not.Null);
        Assert.That(actualValue, Is.InstanceOf<string>());
        Assert.That((string?) actualValue, Is.EqualTo(expectedValue));
        Assert.That(notUuid.ToStringCalls, Is.EqualTo(1));
    }

    [Test]
    public void ConvertToString()
    {
        var expectedValue = "28d2b480b9e743f48ee32ecf03247ad1";
        var uuid = new Uuid("28d2b480-b9e7-43f4-8ee3-2ecf03247ad1");
        var converter = new UuidTypeConverter();

        object? actualValue = converter.ConvertTo(uuid, typeof(string));

        Assert.That(actualValue, Is.Not.Null);
        Assert.That(actualValue, Is.InstanceOf<string>());
        Assert.That((string?) actualValue, Is.EqualTo(expectedValue));
    }

    [Test]
    public void ConvertToInstanceDescriptor()
    {
        ConstructorInfo? uuidCtor = typeof(Uuid)!.GetConstructor(new[] { typeof(string) });
        var expectedValue = new InstanceDescriptor(uuidCtor, new object[] { "ee753afdd98a45678de9740de0441987" });
        var uuid = new Uuid("ee753afd-d98a-4567-8de9-740de0441987");
        var converter = new UuidTypeConverter();

        object? actualValue = converter.ConvertTo(uuid, typeof(InstanceDescriptor));

        Assert.That(actualValue, Is.Not.Null);
        Assert.That(actualValue, Is.InstanceOf<InstanceDescriptor>());
        var actualDescriptor = (InstanceDescriptor?) actualValue;
        Assert.That(actualDescriptor?.MemberInfo, Is.EqualTo(expectedValue.MemberInfo));
        Assert.That(actualDescriptor?.IsComplete, Is.EqualTo(expectedValue.IsComplete));
        Assert.That(actualDescriptor?.Arguments, Is.EqualTo(expectedValue.Arguments));
    }

    [Test]
    public void ConvertToInt32()
    {
        var uuid = new Uuid("28d2b480-b9e7-43f4-8ee3-2ecf03247ad1");
        var converter = new UuidTypeConverter();

        Assert.Throws<NotSupportedException>(() =>
        {
            object? _ = converter.ConvertTo(uuid, typeof(int));
        });
    }

    [Test]
    public void ConvertFromString()
    {
        var expectedValue = new Uuid("28d2b480-b9e7-43f4-8ee3-2ecf03247ad1");
        var converter = new UuidTypeConverter();

        object? actualValue = converter.ConvertFrom("28d2b480b9e743f48ee32ecf03247ad1");

        Assert.That(actualValue, Is.Not.Null);
        Assert.That(actualValue, Is.InstanceOf<Uuid>());
        Assert.That((Uuid) actualValue!, Is.EqualTo(expectedValue));
    }

    [Test]
    public void ConvertFromValidInstanceDescriptor()
    {
        var expectedValue = new Uuid("b28d9df8-fd78-429f-89c7-c669e82eb604");
        var converter = new UuidTypeConverter();
        ConstructorInfo? uuidCtor = typeof(Uuid)!.GetConstructor(new[] { typeof(string) });
        var descriptor = new InstanceDescriptor(uuidCtor, new object[] { "b28d9df8fd78429f89c7c669e82eb604" });

        object? actualValue = converter.ConvertFrom(descriptor);

        Assert.That(actualValue, Is.Not.Null);
        Assert.That(actualValue, Is.InstanceOf<Uuid>());
        Assert.That((Uuid) actualValue!, Is.EqualTo(expectedValue));
    }

    [Test]
    public void ConvertFromInvalidInstanceDescriptor()
    {
        var converter = new UuidTypeConverter();
        ConstructorInfo? guidCtor = typeof(Guid)!.GetConstructor(new[] { typeof(string) });
        var descriptor = new InstanceDescriptor(guidCtor, new object[] { "b28d9df8fd78429f89c7c669e82eb604" });

        Assert.Throws<NotSupportedException>(() =>
        {
            object? _ = converter.ConvertFrom(descriptor);
        });
    }

    [Test]
    public void ConvertFromInt32()
    {
        var converter = new UuidTypeConverter();

        Assert.Throws<NotSupportedException>(() =>
        {
            object? _ = converter.ConvertFrom(42);
        });
    }

    private class NotUuid
    {
        public NotUuid(int id)
        {
            Id = id;
        }

        private int Id { get; }

        public int ToStringCalls { get; private set; }

        [DebuggerStepThrough]
        public override string ToString()
        {
            ToStringCalls++;
            return Id.ToString("D");
        }
    }
}
