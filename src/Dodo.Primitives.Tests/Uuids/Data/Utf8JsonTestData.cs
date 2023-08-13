using System.Collections.Generic;
using System.Linq;
using Dodo.Primitives.Tests.Uuids.Data.Models;

namespace Dodo.Primitives.Tests.Uuids.Data;

public static class Utf8JsonTestData
{
    public static UuidBytesWithUtf8Bytes[] CorrectUtf8EscapedStrings { get; } = GetCorrectUtf8EscapedStrings();

    public static UuidBytesWithUtf8Bytes[] CorrectUtf8UnescapedStrings { get; } = GetCorrectUtf8UnescapedStrings();

    private static UuidBytesWithUtf8Bytes[] GetCorrectUtf8EscapedStrings()
    {
        var result = new List<UuidBytesWithUtf8Bytes>();
        result.AddRange(GetUtf8EscapedStrings(UuidTestData.CorrectNStrings, "N"));
        result.AddRange(GetUtf8EscapedStrings(UuidTestData.CorrectDStrings, "D"));
        result.AddRange(GetUtf8EscapedStrings(UuidTestData.CorrectBStrings, "B"));
        result.AddRange(GetUtf8EscapedStrings(UuidTestData.CorrectPStrings, "P"));
        result.AddRange(GetUtf8EscapedStrings(UuidTestData.CorrectXStrings, "X"));
        return result.ToArray();
    }

    private static UuidBytesWithUtf8Bytes[] GetCorrectUtf8UnescapedStrings()
    {
        var result = new List<UuidBytesWithUtf8Bytes>();
        result.AddRange(GetUtf8UnescapedStrings(UuidTestData.CorrectNStrings, "N"));
        result.AddRange(GetUtf8UnescapedStrings(UuidTestData.CorrectDStrings, "D"));
        result.AddRange(GetUtf8UnescapedStrings(UuidTestData.CorrectBStrings, "B"));
        result.AddRange(GetUtf8UnescapedStrings(UuidTestData.CorrectPStrings, "P"));
        result.AddRange(GetUtf8UnescapedStrings(UuidTestData.CorrectXStrings, "X"));
        return result.ToArray();
    }

    private static UuidBytesWithUtf8Bytes[] GetUtf8UnescapedStrings(UuidStringWithBytes[] src, string format)
    {
        var result = new UuidBytesWithUtf8Bytes[src.Length];
        for (var i = 0; i < src.Length; i++)
        {
            result[i] = new UuidBytesWithUtf8Bytes(src[i].Bytes, new Uuid(src[i].Bytes).ToString(format));
        }

        return result;
    }

    private static UuidBytesWithUtf8Bytes[] GetUtf8EscapedStrings(UuidStringWithBytes[] src, string format)
    {
        var result = new UuidBytesWithUtf8Bytes[src.Length];
        for (var i = 0; i < src.Length; i++)
        {
            byte[] bytes = src[i].Bytes;
            var uuid = new Uuid(bytes);
            string escapedString = ToUtf8EscapedString(uuid, format);
            result[i] = new UuidBytesWithUtf8Bytes(bytes, escapedString);
        }

        return result;
    }

    public static string ToUtf8EscapedString(Uuid uuid, string format)
    {
        var uuidString = uuid.ToString(format);
        var escapedCharacters = new List<string>();
        foreach (char ch in uuidString)
        {
            var intChar = (int) ch;
            escapedCharacters.Add("\\u" + intChar.ToString("x4"));
        }

        char[] flatChars = escapedCharacters
            .Select(x => x.Select(ch => ch))
            .SelectMany(x => x)
            .ToArray();

        var utf8EscapedFlatString = new string(flatChars);
        return utf8EscapedFlatString;
    }
}
