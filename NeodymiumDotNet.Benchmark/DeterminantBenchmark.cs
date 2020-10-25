using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using NeodymiumDotNet.LinearAlgebra;
using static NeodymiumDotNet.Benchmark.BenchmarkData;

namespace NeodymiumDotNet.Benchmark
{
    public class DeterminantBenchmark
    {
        [Benchmark]
        public double Determinant()
            => A.Determinant();

        [Benchmark]
        public double DeterminantParallel()
            => A.Determinant(ParallelIterationStrategy.Instance);
    }
}
