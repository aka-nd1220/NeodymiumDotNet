using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using NeodymiumDotNet.LinearAlgebra;
using NeodymiumDotNet.Linq;

namespace NeodymiumDotNet.Benchmark
{
    public class LUPBenchmark
    {
        public const int Size = 256;
        public static NdArray<double > A { get; } = Random.RandomNdArray.Rand64(new[] { Size, Size });
        public static NdArray<decimal> B { get; } = A.Select(x => (decimal)x).ToMutable().MoveToImmutable();


        public LUPBenchmark()
        {
        }


        [Benchmark]
        public (NdArray<double>, NdArray<double>, NdArray<double>) LUPPrimitive()
        {
            return A.LUP();
        }


        [Benchmark]
        public (NdArray<double>, NdArray<double>, NdArray<double>) LUPPrimitiveLegacy()
        {
            return A.LUPLegacy();
        }


        [Benchmark]
        public (NdArray<decimal>, NdArray<decimal>, NdArray<decimal>) LUPStandard()
        {
            return B.LUP();
        }


        [Benchmark]
        public (NdArray<decimal>, NdArray<decimal>, NdArray<decimal>) LUPStandardLegacy()
        {
            return B.LUPLegacy();
        }
    }
}
