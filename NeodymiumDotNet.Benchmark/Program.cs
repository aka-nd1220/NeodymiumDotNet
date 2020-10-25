using System;
using BenchmarkDotNet.Running;

namespace NeodymiumDotNet.Benchmark
{
    public class Program
    {

        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<LUPBenchmark>();
            Console.ReadKey();
        }
    }
}
