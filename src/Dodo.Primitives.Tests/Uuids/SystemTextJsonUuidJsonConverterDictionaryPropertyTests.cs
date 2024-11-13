using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class SystemTextJsonUuidJsonConverterDictionaryPropertyTests
{
    [Test]
    public async Task ReadCorrectDictionaryPropertyUuid()
    {
        var expectedUuid = new Uuid("d0bec403-3323-44df-9dd4-4456121ab00b");

        var options = new JsonSerializerOptions();
        options.Converters.Add(new SystemTextJsonUuidJsonConverter());

        await using Stream stream = GenerateStreamFromString("""{"d0bec403332344df9dd44456121ab00b": 1}""");

        var result = await JsonSerializer.DeserializeAsync<Dictionary<Uuid, int>>(
            stream,
            options
        );
        Assert.That(result, Is.Not.Null);
        if (result != null)
        {
            Assert.Multiple(() =>
            {
                Assert.That(result.ContainsKey(expectedUuid));
                Assert.That(result[expectedUuid], Is.EqualTo(1));
            });
        }
    }

    [Test]
    public void WriteCorrectDictionaryPropertyUuid()
    {
        var expectedValue = """{"d0bec403332344df9dd44456121ab00b":1}""";

        var target = new Dictionary<Uuid, int>
        {
            { new Uuid("d0bec403332344df9dd44456121ab00b"), 1 }
        };

        var options = new JsonSerializerOptions();
        options.Converters.Add(new SystemTextJsonUuidJsonConverter());

        string actualValue = JsonSerializer.Serialize(target, options);

        Assert.That(actualValue, Is.EqualTo(expectedValue));
    }

    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}
