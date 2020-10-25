using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using NeodymiumDotNet.LinearAlgebra;
using NeodymiumDotNet.Linq;

namespace NeodymiumDotNet.Benchmark
{
    public class DotBenchmark
    {
        public const int Size = 256;
        public static NdArray<double > A { get; } = Random.RandomNdArray.Rand64(new[] { Size, Size });
        public static NdArray<double > B { get; } = Random.RandomNdArray.Rand64(new[] { Size, Size });
        public static NdArray<decimal> C { get; } = Random.RandomNdArray.Rand64(new[] { Size, Size }).Select(x => (decimal)x).ToMutable().MoveToImmutable();
        public static NdArray<decimal> D { get; } = Random.RandomNdArray.Rand64(new[] { Size, Size }).Select(x => (decimal)x).ToMutable().MoveToImmutable();


        [Benchmark]
        public NdArray<double> DotPrimitive()
        {
            return A.Dot(B);
        }


        [Benchmark]
        public NdArray<double> DotPrimitiveLegacy()
        {
            return A.DotLegacy(B);
        }


        [Benchmark]
        public NdArray<decimal> DotStandard()
        {
            return C.Dot(D);
        }


        [Benchmark]
        public NdArray<decimal> DotStandardLegacy()
        {
            return C.DotLegacy(D);
        }
    }
}
