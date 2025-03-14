﻿using System;
using System.Buffers;
using System.Text;
using System.Text.Json;
using Dodo.Primitives.Tests.Uuids.Data;
using Dodo.Primitives.Tests.Uuids.Data.Models;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class Utf8JsonReaderUuidExtensionsTests
{
    private const int MaxExpansionFactorWhileEscaping = 6;
    private const int MaximumFormatUuidLength = 68;
    private const int MaximumEscapedUuidLength = MaxExpansionFactorWhileEscaping * MaximumFormatUuidLength;

    [Test]
    public void GetUuidTokenTypeStringButNotUuidThrowsFormatException()
    {
        var ex = Assert.Throws<FormatException>(() =>
        {
            byte[] data = Encoding.UTF8.GetBytes("{ \"value\": \"hsadgfhygsdaf\" }");
            var reader = new Utf8JsonReader(data);
            reader.Read(); // StartObject
            reader.Read(); // "value"
            reader.Read(); // "hsadgfhygsdaf"
            reader.GetUuid();
        });
        Assert.That(ex?.Source, Is.EqualTo("System.Text.Json.Rethrowable"));
    }

    [Test]
    public void TryGetUuidTokenTypeStartObjectThrowsInvalidOperationException()
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            byte[] data = Encoding.UTF8.GetBytes("{ \"value\": 42 }");
            var reader = new Utf8JsonReader(data);
            reader.Read();
            reader.TryGetUuid(out _);
        });
        Assert.That(ex?.Source, Is.EqualTo("System.Text.Json.Rethrowable"));
    }

    [Test]
    public void TryGetUuidTooLongInputStringWithValueSequenceNotParsed()
    {
        var longUnescapedJsonStringBuilder = new StringBuilder(MaximumEscapedUuidLength + 1);
        for (var i = 0; i < MaximumEscapedUuidLength + 1; i++)
        {
            longUnescapedJsonStringBuilder.Append('0');
        }

        var longUnescapedJsonString = longUnescapedJsonStringBuilder.ToString();
        string json = "{\"value\":\"" + longUnescapedJsonString + "\"}";
        byte[] utf8JsonBytes = Encoding.UTF8.GetBytes(json);
        (MemorySegment head, MemorySegment tail) = SplitByteArrayIntoSegments(utf8JsonBytes, 13);
        var sequence = new ReadOnlySequence<byte>(head, 0, tail, tail.Memory.Length);
        var reader = new Utf8JsonReader(sequence);
        reader.Read(); // StartObject
        reader.Read(); // "value"
        reader.Read(); // UuidTooLongEscapedValue

        Assert.That(reader.TryGetUuid(out Uuid actualUuid), Is.False);
        Assert.That(actualUuid, Is.EqualTo(Uuid.Empty));
    }

    [Test]
    public void TryGetUuidCorrectUnescapedStringNoValueSequence()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidBytesWithUtf8Bytes correctUtf8String in Utf8JsonTestData.CorrectUtf8UnescapedStrings)
            {
                var expectedUuid = new Uuid(correctUtf8String.UuidBytes);
                string json = "{\"value\":\"" + correctUtf8String.Utf8String + "\"}";
                byte[] utf8JsonBytes = Encoding.UTF8.GetBytes(json);
                var reader = new Utf8JsonReader(utf8JsonBytes);
                reader.Read(); // StartObject
                reader.Read(); // "value"
                reader.Read(); // UuidEscapedValue

                Assert.That(reader.TryGetUuid(out Uuid actualUuid), Is.True);
                Assert.That(actualUuid, Is.EqualTo(expectedUuid));
            }
        });
    }

    [Test]
    public void TryGetUuidCorrectEscapedStringNoValueSequence()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidBytesWithUtf8Bytes correctUtf8EscapedString in Utf8JsonTestData.CorrectUtf8EscapedStrings)
            {
                var expectedUuid = new Uuid(correctUtf8EscapedString.UuidBytes);
                string json = "{\"value\":\"" + correctUtf8EscapedString.Utf8String + "\"}";
                byte[] utf8JsonBytes = Encoding.UTF8.GetBytes(json);
                var reader = new Utf8JsonReader(utf8JsonBytes);
                reader.Read(); // StartObject
                reader.Read(); // "value"
                reader.Read(); // UuidEscapedValue

                Assert.That(reader.TryGetUuid(out Uuid actualUuid), Is.True);
                Assert.That(actualUuid, Is.EqualTo(expectedUuid));
            }
        });
    }

    [Test]
    public void TryGetUuidCorrectUnescapedStringWithValueSequence()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidBytesWithUtf8Bytes correctUtf8String in Utf8JsonTestData.CorrectUtf8UnescapedStrings)
            {
                var expectedUuid = new Uuid(correctUtf8String.UuidBytes);
                string json = "{\"value\":\"" + correctUtf8String.Utf8String + "\"}";
                byte[] utf8JsonBytes = Encoding.UTF8.GetBytes(json);
                (MemorySegment head, MemorySegment tail) = SplitByteArrayIntoSegments(utf8JsonBytes, 13);
                var sequence = new ReadOnlySequence<byte>(head, 0, tail, tail.Memory.Length);
                var reader = new Utf8JsonReader(sequence);
                reader.Read(); // StartObject
                reader.Read(); // "value"
                reader.Read(); // UuidEscapedValue

                Assert.That(reader.TryGetUuid(out Uuid actualUuid), Is.True);
                Assert.That(actualUuid, Is.EqualTo(expectedUuid));
            }
        });
    }

    [Test]
    public void TryGetUuidCorrectEscapedStringWithValueSequence()
    {
        Assert.Multiple(() =>
        {
            foreach (UuidBytesWithUtf8Bytes correctUtf8EscapedString in Utf8JsonTestData.CorrectUtf8EscapedStrings)
            {
                var expectedUuid = new Uuid(correctUtf8EscapedString.UuidBytes);
                string json = "{\"value\":\"" + correctUtf8EscapedString.Utf8String + "\"}";
                byte[] utf8JsonBytes = Encoding.UTF8.GetBytes(json);
                (MemorySegment head, MemorySegment tail) = SplitByteArrayIntoSegments(utf8JsonBytes, 13);
                var sequence = new ReadOnlySequence<byte>(head, 0, tail, tail.Memory.Length);
                var reader = new Utf8JsonReader(sequence);
                reader.Read(); // StartObject
                reader.Read(); // "value"
                reader.Read(); // UuidEscapedValue

                Assert.That(reader.TryGetUuid(out Uuid actualUuid));
                Assert.That(actualUuid, Is.EqualTo(expectedUuid));
            }
        });
    }

    private static (MemorySegment head, MemorySegment tail) SplitByteArrayIntoSegments(byte[] bytes, int splitBy)
    {
        int parts = bytes.Length / splitBy;
        if (parts * splitBy < bytes.Length)
        {
            parts += 1;
        }

        var head = new MemorySegment(new ReadOnlyMemory<byte>());
        MemorySegment tail = head;
        for (var i = 0; i < parts; i++)
        {
            int offset = i * splitBy;
            int memoryLength = offset + splitBy > bytes.Length
                ? splitBy - (offset + splitBy - bytes.Length)
                : splitBy;

            var memory = new ReadOnlyMemory<byte>(bytes, offset, memoryLength);
            tail = tail.Append(memory);
        }

        return (head, tail);
    }

    [Test]
    public void TryGetUuidTooLongEscapedStringAfterUnescapeNotParsed()
    {
        string escapedString = Utf8JsonTestData.ToUtf8EscapedString(new Uuid("f91c971cf7ab404e9a24546b133533dd"), "N");
        int charsToAppend = MaximumEscapedUuidLength - escapedString.Length;
        var longUnescapedJsonStringBuilder = new StringBuilder(escapedString, MaximumEscapedUuidLength);
        for (var i = 0; i < charsToAppend; i++)
        {
            longUnescapedJsonStringBuilder.Append('0');
        }

        var longUnescapedJsonString = longUnescapedJsonStringBuilder.ToString();
        string json = "{\"value\":\"" + longUnescapedJsonString + "\"}";
        byte[] utf8JsonBytes = Encoding.UTF8.GetBytes(json);
        var reader = new Utf8JsonReader(utf8JsonBytes);
        reader.Read(); // StartObject
        reader.Read(); // "value"
        reader.Read(); // UuidTooLongEscapedValue

        Assert.That(reader.TryGetUuid(out Uuid actualUuid), Is.False);
        Assert.That(actualUuid, Is.EqualTo(Uuid.Empty));
    }

    [Test]
    public void TryGetUuidTooLongInputStringNotParsed()
    {
        var longUnescapedJsonStringBuilder = new StringBuilder(MaximumEscapedUuidLength + 1);
        for (var i = 0; i < MaximumEscapedUuidLength + 1; i++)
        {
            longUnescapedJsonStringBuilder.Append('0');
        }

        var longUnescapedJsonString = longUnescapedJsonStringBuilder.ToString();
        string json = "{\"value\":\"" + longUnescapedJsonString + "\"}";
        byte[] utf8JsonBytes = Encoding.UTF8.GetBytes(json);
        var reader = new Utf8JsonReader(utf8JsonBytes);
        reader.Read(); // StartObject
        reader.Read(); // "value"
        reader.Read(); // UuidTooLongEscapedValue

        Assert.That(reader.TryGetUuid(out Uuid actualUuid), Is.False);
        Assert.That(actualUuid, Is.EqualTo(Uuid.Empty));
    }

    internal class MemorySegment : ReadOnlySequenceSegment<byte>
    {
        public MemorySegment(ReadOnlyMemory<byte> memory)
        {
            Memory = memory;
        }

        public MemorySegment Append(ReadOnlyMemory<byte> memory)
        {
            var segment = new MemorySegment(memory)
            {
                RunningIndex = RunningIndex + Memory.Length
            };
            Next = segment;
            return segment;
        }
    }
}
