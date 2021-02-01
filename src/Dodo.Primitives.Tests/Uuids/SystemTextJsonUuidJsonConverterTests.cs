using System.IO;
using System.Text;
using System.Text.Json;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids
{
    public class SystemTextJsonUuidJsonConverterTests
    {
        [Test]
        public void ReadCorrect()
        {
            var expectedUuid = new Uuid("d0bec403-3323-44df-9dd4-4456121ab00b");
            byte[] data = Encoding.UTF8.GetBytes($"\"{expectedUuid.ToString("N")}\"");
            var reader = new Utf8JsonReader(data);
            reader.Read();
            var converter = new SystemTextJsonUuidJsonConverter();

#pragma warning disable 8625
            Uuid actualUuid = converter.Read(ref reader, typeof(Uuid), null);
#pragma warning restore 8625

            Assert.AreEqual(expectedUuid, actualUuid);
        }

        [Test]
        public void WriteCorrect()
        {
            var expectedValue = "\"edbe2e116ead4ee7848eaef7bc2ae2d6\"";
            var uuid = new Uuid(expectedValue.Trim('"'));
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            var converter = new SystemTextJsonUuidJsonConverter();

#pragma warning disable 8625
            converter.Write(writer, uuid, null);
#pragma warning restore 8625

            writer.Flush();
            string actualValue = Encoding.UTF8.GetString(stream.ToArray());
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
