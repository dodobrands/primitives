# Dodo.Primitives

[![Latest release](https://img.shields.io/badge/nuget-4.0.0-blue?&kill_cache=1)](https://www.nuget.org/packages/Dodo.Primitives/4.0.0)
[![codecov](https://codecov.io/gh/dodobrands/primitives/graph/badge.svg?token=7ILQPREIVA)](https://codecov.io/gh/dodobrands/primitives)

Library provides .NET primitive types:

- [Uuid](./src/Dodo.Primitives/Uuid.cs)

## Project goal

The main goal is Uuid implementation according to the [RFC9562](https://www.rfc-editor.org/rfc/rfc9562.html).

.NET provides [System.Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid) struct which is special case of the RFC9562 implementation. System.Guid has [little-endian layout](https://github.com/dotnet/runtime/blob/v9.0.0/src/libraries/System.Private.CoreLib/src/System/Guid.cs#L44-L46) for the first 8 bytes (int32, int16, int16).

Our goal is to provide Uuid fully compliant with RFC9562 (big-endian layout) and preserve System.Guid-like behaviour. 
Also project contains generators to create Uuid version 7.

  ```csharp
    var uuid = Uuid.CreateVersion7();
  ```

## Project documentation

- [Prerequisites, build and development](https://github.com/dodobrands/primitives/wiki/Prerequisites,-build-and-development)
- [Benchmarks](https://github.com/dodobrands/primitives/wiki/Benchmarks)
