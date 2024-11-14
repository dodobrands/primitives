using System;
using Dodo.Primitives.Tests.Uuids.Data;
using Dodo.Primitives.Tests.Uuids.Data.Models;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidToGuidTests
{
    [Test]
    public void ToGuidByteLayout()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes nString in UuidTestData.CorrectNStrings)
            {
                var uuid = new Uuid(nString.Bytes);
#pragma warning disable CS0618 // Type or member is obsolete
                Guid guid = uuid.ToGuidByteLayout();
#pragma warning restore CS0618 // Type or member is obsolete

                byte[] expectedBytes = uuid.ToByteArray();
                byte[] actualBytes = guid.ToByteArray();
                string? expectedGuidString = Convert.ToHexString(new[]
                {
                    nString.Bytes[3],
                    nString.Bytes[2],
                    nString.Bytes[1],
                    nString.Bytes[0],
                    nString.Bytes[5],
                    nString.Bytes[4],
                    nString.Bytes[7],
                    nString.Bytes[6],
                    nString.Bytes[8],
                    nString.Bytes[9],
                    nString.Bytes[10],
                    nString.Bytes[11],
                    nString.Bytes[12],
                    nString.Bytes[13],
                    nString.Bytes[14],
                    nString.Bytes[15]
                });
                var actualGuidString = guid.ToString("N");

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
                Assert.That(string.Equals(expectedGuidString, actualGuidString, StringComparison.Ordinal));
            }
        });
    }

    [Test]
    public void ToGuidStringLayout()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidStringWithBytes nString in UuidTestData.CorrectNStrings)
            {
                var uuid = new Uuid(nString.Bytes);
#pragma warning disable CS0618 // Type or member is obsolete
                Guid guid = uuid.ToGuidStringLayout();
#pragma warning restore CS0618 // Type or member is obsolete

                byte[] expectedBytes =
                {
                    nString.Bytes[3],
                    nString.Bytes[2],
                    nString.Bytes[1],
                    nString.Bytes[0],
                    nString.Bytes[5],
                    nString.Bytes[4],
                    nString.Bytes[7],
                    nString.Bytes[6],
                    nString.Bytes[8],
                    nString.Bytes[9],
                    nString.Bytes[10],
                    nString.Bytes[11],
                    nString.Bytes[12],
                    nString.Bytes[13],
                    nString.Bytes[14],
                    nString.Bytes[15]
                };
                byte[] actualBytes = guid.ToByteArray();
                var expectedGuidString = uuid.ToString("N");
                var actualGuidString = guid.ToString("N");

                Assert.That(actualBytes, Is.EqualTo(expectedBytes));
                Assert.That(string.Equals(expectedGuidString, actualGuidString, StringComparison.Ordinal));
            }
        });
    }
}
