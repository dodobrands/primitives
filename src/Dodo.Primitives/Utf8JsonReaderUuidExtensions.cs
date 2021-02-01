using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Dodo.Primitives
{
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    public static class Utf8JsonReaderUuidExtensions
    {
        // https://github.com/dotnet/runtime/blob/v5.0.0-preview.3.20214.6/src/libraries/System.Text.Json/src/System/Text/Json/ThrowHelper.cs#L14
        private const string ExceptionSourceValueToRethrowAsJsonException = "System.Text.Json.Rethrowable";

        public static Uuid GetUuid(this ref Utf8JsonReader reader)
        {
            if (!reader.TryGetUuid(out Uuid value))
            {
                throw new FormatException("The JSON value is not in a supported Uuid format.")
                {
                    Source = ExceptionSourceValueToRethrowAsJsonException
                };
            }

            return value;
        }


        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static bool TryGetUuid(this ref Utf8JsonReader reader, out Uuid value)
        {
            string? possibleUuidString = reader.GetString();
            if (Uuid.TryParse(possibleUuidString, out value))
            {
                return true;
            }

            value = default;
            return false;
        }
    }
}
