using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Validators;

namespace Dodo.Primitives.Benchmarks.Benchmarks;

public class CompareToBenchmarks
{
    private const int Limit = 1_000_000;

    [Params(0.0, 0.001, 0.002, 0.005, 0.01, 0.02, 0.05, 0.1, 0.2, 0.3, 0.5, 0.8, 0.9, 0.99, 1.0)]
    public double matchingProbability;

    private (Uuid, Uuid)[] _instances = null!;

    [GlobalSetup]
    public void SetUp()
    {
        _instances = GenerateInstances(Limit, matchingProbability);
    }

    private static (Uuid, Uuid)[] GenerateInstances(int limit, double matchingProbability)
    {
        var random = new Random();
        var instances = new (Uuid, Uuid)[limit];

        for (var i = 0; i < limit; i++) {
            Uuid a = Uuid.NewTimeBased();
            Uuid b = random.NextDouble() > matchingProbability ? Uuid.NewTimeBased() : a;
            instances[i] = (a, b);
        }

        return instances;
    }

    [Benchmark(OperationsPerInvoke = Limit)]
    public int CompareToBenchmark()
    {
        int observer = 0;

        foreach ((Uuid left, Uuid right) in _instances)
        {
            observer += left.CompareTo(right);
        }

        return observer;
    }
}
