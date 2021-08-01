using BenchmarkDotNet.Running;
using System;

namespace StringInterpolationBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<StringCreator>();
        }
    }
}
