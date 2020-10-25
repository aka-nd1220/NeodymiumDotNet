using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using NeodymiumDotNet.Statistics;

namespace NeodymiumDotNet.Benchmark
{
    public class SumBenchmark
    {
        private readonly SimpleCalculationBenchmark _calc = new SimpleCalculationBenchmark();

        [Benchmark]
        public double AddSum()
            => _calc.AddSimple1000().Sum();
    }
}
