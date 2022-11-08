using System;
using Newtonsoft.Json;

namespace Dodo.Primitives.Tests.Uuids.Data.Models;

public class UuidStringWithBytes
{
    public UuidStringWithBytes(string uuidString, byte[] bytes)
    {
        String = uuidString ?? throw new ArgumentNullException(nameof(uuidString));
        Bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));
    }

    public string String { get; }

    public byte[] Bytes { get; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}