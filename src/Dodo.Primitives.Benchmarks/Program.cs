using BenchmarkDotNet.Running;

namespace Dodo.Primitives.Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            args = ["--filter", "*"];
        }

        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
