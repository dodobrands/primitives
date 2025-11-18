# Copilot Onboarding Instructions for Dodo.Primitives Repository

## Repository Overview

The `Dodo.Primitives` repository provides .NET primitive types, with a primary focus on implementing `Uuid` (Universally Unique Identifier) according to [RFC9562](https://www.rfc-editor.org/rfc/rfc9562.html). This implementation ensures compliance with the big-endian layout specified in the RFC while preserving `System.Guid`-like behavior. The repository also includes generators for creating `Uuid` version 7.

### Key Features:
- Fully compliant `Uuid` implementation.
- Benchmarks for performance evaluation.
- Tests for validation.

### Technologies Used:
- **Languages**: C#
- **Frameworks**: .NET 8, .NET 9, .NET 10
- **Build System**: MSBuild
- **Testing Framework**: xUnit
- **Benchmarking**: BenchmarkDotNet

## Build and Validation Instructions

### Prerequisites
1. Install the .NET SDK (version 8.0 or higher).
2. Ensure `dotnet` CLI is available in your PATH.
3. Clone the repository and navigate to the root directory.

### Build
To build the solution:
```bash
cd src
# Restore dependencies
 dotnet restore
# Build the solution
 dotnet build
```

### Test
To run the tests:
```bash
cd src/Dodo.Primitives.Tests
# Run all tests
 dotnet test
```

### Benchmarks
To run benchmarks:
```bash
cd src/Dodo.Primitives.Benchmarks
# Run benchmarks
 dotnet run -c Release
```

### Linting
Currently, no explicit linting configuration is provided. Ensure code adheres to standard C# conventions.

## Project Layout

### Directory Structure
- `src/Dodo.Primitives`: Core library implementation.
- `src/Dodo.Primitives.Tests`: Unit tests for the library.
- `src/Dodo.Primitives.Benchmarks`: Benchmarking projects.
- `src/Dodo.Primitives.Generation`: Code generation utilities.

### Key Files
- `Dodo.Primitives/Dodo.Primitives.csproj`: Project file for the core library.
- `Dodo.Primitives.Tests/Dodo.Primitives.Tests.csproj`: Project file for tests.
- `Dodo.Primitives.Benchmarks/Dodo.Primitives.Benchmarks.csproj`: Project file for benchmarks.

### Continuous Integration
- Ensure all tests pass before committing changes.
- Validate benchmarks for performance regressions.

## Additional Notes
- Refer to the [README](../README.md) for more details.
- Always run `dotnet restore` before building or testing to ensure dependencies are up-to-date.

## Agent Guidance
- Trust these instructions for build, test, and validation steps.
- Only perform additional searches if these instructions are incomplete or produce errors.
