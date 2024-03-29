﻿using System;
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
                Guid guid = uuid.ToGuidByteLayout();

                byte[] expectedBytes = uuid.ToByteArray();
                byte[] actualBytes = guid.ToByteArray();
                string? expectedGuidString = Primitives.Hex.GetString(new[]
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

                Assert.AreEqual(expectedBytes, actualBytes);
                Assert.True(string.Equals(expectedGuidString, actualGuidString, StringComparison.Ordinal));
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
                Guid guid = uuid.ToGuidStringLayout();

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

                Assert.AreEqual(expectedBytes, actualBytes);
                Assert.True(string.Equals(expectedGuidString, actualGuidString, StringComparison.Ordinal));
            }
        });
    }
}
