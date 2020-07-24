#if NETCOREAPP3_1
using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Dodo.Primitives.IL;

#endif
#if NETSTANDARD2_0
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

#endif


namespace Dodo.Primitives
{
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    public static class Utf8JsonReaderUuidExtensions
    {
        // https://github.com/dotnet/runtime/blob/v5.0.0-preview.3.20214.6/src/libraries/System.Text.Json/src/System/Text/Json/ThrowHelper.cs#L14
        private const string ExceptionSourceValueToRethrowAsJsonException = "System.Text.Json.Rethrowable";
#if NETCOREAPP3_1
        // https://github.com/dotnet/runtime/blob/v5.0.0-preview.3.20214.6/src/libraries/System.Text.Json/src/System/Text/Json/JsonConstants.cs#L20
        private const byte BackSlash = 0x5C;

        // https://github.com/dotnet/runtime/blob/v5.0.0-preview.3.20214.6/src/libraries/System.Text.Json/src/System/Text/Json/JsonConstants.cs#L57
        private const int MaxExpansionFactorWhileEscaping = 6;

        // https://github.com/dotnet/runtime/blob/v5.0.0-preview.3.20214.6/src/libraries/System.Text.Json/src/System/Text/Json/JsonConstants.cs#L74
        private const int MaximumFormatUuidLength = 68;

        // https://github.com/dotnet/runtime/blob/v5.0.0-preview.3.20214.6/src/libraries/System.Text.Json/src/System/Text/Json/JsonConstants.cs#L75
        private const int MaximumEscapedUuidLength = MaxExpansionFactorWhileEscaping * MaximumFormatUuidLength;

        public static unsafe bool TryGetUuid(this ref Utf8JsonReader reader, out Uuid value)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new InvalidOperationException($"Cannot get the value of a token type '{reader.TokenType}' as a string.")
                {
                    Source = ExceptionSourceValueToRethrowAsJsonException
                };
            }

            if (reader.HasValueSequence)
            {
                long sequenceLength = reader.ValueSequence.Length;
                if (sequenceLength > MaximumEscapedUuidLength)
                {
                    value = default;
                    return false;
                }

                byte* stackBuffer = stackalloc byte[(int) sequenceLength];
                var stackSpan = new Span<byte>(stackBuffer, (int) sequenceLength);
                reader.ValueSequence.CopyTo(stackSpan);
                return reader.IsStringHasEscaping()
                    ? TryGetEscapedUuid(stackSpan, out value)
                    : TryParseUuid(stackSpan, out value);
            }

            if (reader.ValueSpan.Length > MaximumEscapedUuidLength)
            {
                value = default;
                return false;
            }

            return reader.IsStringHasEscaping()
                ? TryGetEscapedUuid(reader.ValueSpan, out value)
                : TryParseUuid(reader.ValueSpan, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static bool TryGetEscapedUuid(ReadOnlySpan<byte> source, out Uuid value)
        {
            int idx = source.IndexOf(BackSlash);
            Span<byte> utf8Unescaped = stackalloc byte[source.Length];
            SystemTextJson.Unescape(source, utf8Unescaped, idx, out int written);
            utf8Unescaped = utf8Unescaped.Slice(0, written);
            if (utf8Unescaped.Length <= MaximumFormatUuidLength && TryParseUuid(utf8Unescaped, out value))
            {
                return true;
            }

            value = default;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static bool TryParseUuid(ReadOnlySpan<byte> utf8Unescaped, out Uuid value)
        {
            if (Uuid.TryParse(utf8Unescaped, out value))
            {
                return true;
            }

            value = default;
            return false;
        }

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
#endif
#if NETSTANDARD2_0
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

        public static bool TryGetUuid(this ref Utf8JsonReader reader, out Uuid value)
        {
            var possibleUuidString = reader.GetString();
            if (Uuid.TryParse(possibleUuidString, out value))
            {
                return true;
            }

            value = default;
            return false;
        }
#endif
    }
}
