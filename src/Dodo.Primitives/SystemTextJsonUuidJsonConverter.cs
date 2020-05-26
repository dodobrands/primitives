using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dodo.Primitives
{
    public class SystemTextJsonUuidJsonConverter : JsonConverter<Uuid>
    {
        public override Uuid Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return reader.GetUuid();
        }

        public override void Write(
            Utf8JsonWriter writer,
            Uuid value,
            JsonSerializerOptions options)
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            Span<char> outputBuffer = stackalloc char[32];
            // Always will be well-formatted, cuz we allocate exact buffer for output format
            value.TryFormat(outputBuffer, out _, "N");
            writer.WriteStringValue(outputBuffer);
        }
    }
}
