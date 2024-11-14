using System;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Dodo.Primitives.Benchmarks.Benchmarks;

[GcServer(true)]
[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GeneratorBenchmarks
{
    [Benchmark]
    public Guid guid_CreateVersion7()
    {
        return Guid.CreateVersion7();
    }

    [Benchmark]
    public Uuid uuid_CreateVersion7()
    {
        return Uuid.CreateVersion7();
    }
}
