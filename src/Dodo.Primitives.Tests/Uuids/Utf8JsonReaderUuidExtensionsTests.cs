using System;
using System.Buffers;
using System.Text;
using System.Text.Json;
using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids
{
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
                var data = Encoding.UTF8.GetBytes("{ \"value\": \"hsadgfhygsdaf\" }");
                var reader = new Utf8JsonReader(data);
                reader.Read(); // StartObject
                reader.Read(); // "value"
                reader.Read(); // "hsadgfhygsdaf"
                reader.GetUuid();
            });
            Assert.AreEqual("System.Text.Json.Rethrowable", ex.Source);
        }

        [Test]
        public void TryGetUuidTokenTypeStartObjectThrowsInvalidOperationException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                var data = Encoding.UTF8.GetBytes("{ \"value\": 42 }");
                var reader = new Utf8JsonReader(data);
                reader.Read();
                reader.TryGetUuid(out _);
            });
            Assert.AreEqual("System.Text.Json.Rethrowable", ex.Source);
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
            var json = "{\"value\":\"" + longUnescapedJsonString + "\"}";
            var utf8JsonBytes = Encoding.UTF8.GetBytes(json);
            var (head, tail) = SplitByteArrayIntoSegments(utf8JsonBytes, 13);
            var sequence = new ReadOnlySequence<byte>(head, 0, tail, tail.Memory.Length);
            var reader = new Utf8JsonReader(sequence);
            reader.Read(); // StartObject
            reader.Read(); // "value"
            reader.Read(); // UuidTooLongEscapedValue

            Assert.False(reader.TryGetUuid(out Uuid actualUuid));
            Assert.AreEqual(Uuid.Empty, actualUuid);
        }

        [Test]
        public void TryGetUuidCorrectUnescapedStringNoValueSequence()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctUtf8String in Utf8JsonTestData.CorrectUtf8UnescapedStrings)
                {
                    var expectedUuid = new Uuid(correctUtf8String.UuidBytes);
                    var json = "{\"value\":\"" + correctUtf8String.Utf8String + "\"}";
                    var utf8JsonBytes = Encoding.UTF8.GetBytes(json);
                    var reader = new Utf8JsonReader(utf8JsonBytes);
                    reader.Read(); // StartObject
                    reader.Read(); // "value"
                    reader.Read(); // UuidEscapedValue

                    Assert.True(reader.TryGetUuid(out Uuid actualUuid));
                    Assert.AreEqual(expectedUuid, actualUuid);
                }
            });
        }

        [Test]
        public void TryGetUuidCorrectEscapedStringNoValueSequence()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctUtf8EscapedString in Utf8JsonTestData.CorrectUtf8EscapedStrings)
                {
                    var expectedUuid = new Uuid(correctUtf8EscapedString.UuidBytes);
                    var json = "{\"value\":\"" + correctUtf8EscapedString.Utf8String + "\"}";
                    var utf8JsonBytes = Encoding.UTF8.GetBytes(json);
                    var reader = new Utf8JsonReader(utf8JsonBytes);
                    reader.Read(); // StartObject
                    reader.Read(); // "value"
                    reader.Read(); // UuidEscapedValue

                    Assert.True(reader.TryGetUuid(out Uuid actualUuid));
                    Assert.AreEqual(expectedUuid, actualUuid);
                }
            });
        }

        [Test]
        public void TryGetUuidCorrectUnescapedStringWithValueSequence()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctUtf8String in Utf8JsonTestData.CorrectUtf8UnescapedStrings)
                {
                    var expectedUuid = new Uuid(correctUtf8String.UuidBytes);
                    var json = "{\"value\":\"" + correctUtf8String.Utf8String + "\"}";
                    var utf8JsonBytes = Encoding.UTF8.GetBytes(json);
                    var (head, tail) = SplitByteArrayIntoSegments(utf8JsonBytes, 13);
                    var sequence = new ReadOnlySequence<byte>(head, 0, tail, tail.Memory.Length);
                    var reader = new Utf8JsonReader(sequence);
                    reader.Read(); // StartObject
                    reader.Read(); // "value"
                    reader.Read(); // UuidEscapedValue

                    Assert.True(reader.TryGetUuid(out Uuid actualUuid));
                    Assert.AreEqual(expectedUuid, actualUuid);
                }
            });
        }

        [Test]
        public void TryGetUuidCorrectEscapedStringWithValueSequence()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctUtf8EscapedString in Utf8JsonTestData.CorrectUtf8EscapedStrings)
                {
                    var expectedUuid = new Uuid(correctUtf8EscapedString.UuidBytes);
                    var json = "{\"value\":\"" + correctUtf8EscapedString.Utf8String + "\"}";
                    var utf8JsonBytes = Encoding.UTF8.GetBytes(json);
                    var (head, tail) = SplitByteArrayIntoSegments(utf8JsonBytes, 13);
                    var sequence = new ReadOnlySequence<byte>(head, 0, tail, tail.Memory.Length);
                    var reader = new Utf8JsonReader(sequence);
                    reader.Read(); // StartObject
                    reader.Read(); // "value"
                    reader.Read(); // UuidEscapedValue

                    Assert.True(reader.TryGetUuid(out Uuid actualUuid));
                    Assert.AreEqual(expectedUuid, actualUuid);
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
            var tail = head;
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
            var escapedString = Utf8JsonTestData.ToUtf8EscapedString(new Uuid("f91c971cf7ab404e9a24546b133533dd"), "N");
            int charsToAppend = MaximumEscapedUuidLength - escapedString.Length;
            var longUnescapedJsonStringBuilder = new StringBuilder(escapedString, MaximumEscapedUuidLength);
            for (var i = 0; i < charsToAppend; i++)
            {
                longUnescapedJsonStringBuilder.Append('0');
            }

            var longUnescapedJsonString = longUnescapedJsonStringBuilder.ToString();
            var json = "{\"value\":\"" + longUnescapedJsonString + "\"}";
            var utf8JsonBytes = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(utf8JsonBytes);
            reader.Read(); // StartObject
            reader.Read(); // "value"
            reader.Read(); // UuidTooLongEscapedValue

            Assert.False(reader.TryGetUuid(out Uuid actualUuid));
            Assert.AreEqual(Uuid.Empty, actualUuid);
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
            var json = "{\"value\":\"" + longUnescapedJsonString + "\"}";
            var utf8JsonBytes = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(utf8JsonBytes);
            reader.Read(); // StartObject
            reader.Read(); // "value"
            reader.Read(); // UuidTooLongEscapedValue

            Assert.False(reader.TryGetUuid(out Uuid actualUuid));
            Assert.AreEqual(Uuid.Empty, actualUuid);
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
}
