# Dodo.Primitives

[![Latest release](https://img.shields.io/badge/nuget-1.4.0-blue?&kill_cache=1)](https://www.nuget.org/packages/Dodo.Primitives/1.4.0)
[![Coverage Status](https://coveralls.io/repos/github/dodopizza/primitives/badge.svg?branch=refs/tags/1.4.0&kill_cache=1)](https://coveralls.io/github/dodopizza/primitives?branch=refs/tags/1.4.0)

Library provides .NET primitive types:

- [Uuid](./src/Dodo.Primitives/Uuid.cs)

and utils to work with types:

- [Hex](./src/Dodo.Primitives/Hex.cs)

## Project goal

The main goal is Uuid implementation according to the [RFC4122](https://tools.ietf.org/html/rfc4122).

.NET provides [System.Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid?view=netcore-3.1) struct which is special case of the RFC4122 implementation. System.Guid has [little-endian layout](https://github.com/dotnet/runtime/blob/v6.0.0-preview.1.21102.12/src/libraries/System.Private.CoreLib/src/System/Guid.cs#L25-L27) for the first 8 bytes (int32, int16, int16).

Our goal is to provide Uuid fully compliant with RFC4122 (big-endian layout) and preserve System.Guid-like behaviour. Also project contains generators to create different Uuid variants. Currently supported variants:

- Time-based (like [Uuid v1](https://tools.ietf.org/html/rfc4122#section-4.1.3)).

  ```csharp
    var uuid = Uuid.NewTimeBased();
  ```

- Time-based, optimized for MySQL.

  ```csharp
    var uuid = Uuid.NewMySqlOptimized();
  ```
  Equals `UUID_TO_BIN(UUID(), 1)` from [MySQL 8.0](https://dev.mysql.com/doc/refman/8.0/en/miscellaneous-functions.html#function_uuid-to-bin)

## Project documentation

- [Prerequisites, build and development](https://github.com/dodopizza/primitives/wiki/Prerequisites,-build-and-development)
- [Benchmarks](https://github.com/dodopizza/primitives/wiki/Benchmarks)