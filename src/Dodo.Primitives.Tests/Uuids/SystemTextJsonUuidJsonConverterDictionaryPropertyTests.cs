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

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.Converters.Add(new SystemTextJsonUuidJsonConverter());

        await using var stream = GenerateStreamFromString("""{"d0bec403332344df9dd44456121ab00b": 1}""");

        var result = await JsonSerializer.DeserializeAsync<Dictionary<Uuid, int>>(
            stream,
            options
        );
        Assert.NotNull(result);
        if (result != null)
        {
            Assert.IsTrue(result.ContainsKey(expectedUuid));
            Assert.AreEqual(1, result[expectedUuid]);
        }
    }

    [Test]
    public void WriteCorrectDictionaryPropertyUuid()
    {
        var expectedValue = """{"d0bec403332344df9dd44456121ab00b":1}""";

        var target = new Dictionary<Uuid, int>()
        {
            {new Uuid("d0bec403332344df9dd44456121ab00b"), 1}
        };

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.Converters.Add(new SystemTextJsonUuidJsonConverter());

        var actualValue = JsonSerializer.Serialize<Dictionary<Uuid, int>>(target, options);

        Assert.AreEqual(expectedValue, actualValue);
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
