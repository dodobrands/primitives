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
#if NETCOREAPP3_1
            // ReSharper disable once SuggestVarOrType_Elsewhere
            Span<char> outputBuffer = stackalloc char[32];
            // Always will be well-formatted, cuz we allocate exact buffer for output format
            value.TryFormat(outputBuffer, out _, "N");
            writer.WriteStringValue(outputBuffer);
#endif
#if NETSTANDARD2_0
            // ReSharper disable once SuggestVarOrType_Elsewhere
            Span<char> outputBuffer = stackalloc char[32]; // Always will be well-formatted, cuz we allocate exact buffer for output format
            Span<char> format = stackalloc char[1];
            format[0] = 'N';
            value.TryFormat(outputBuffer, out _, format);
            writer.WriteStringValue(outputBuffer);
#endif
        }
    }
}
