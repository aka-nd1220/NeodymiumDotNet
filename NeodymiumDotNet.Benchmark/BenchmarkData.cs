using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeodymiumDotNet.Benchmark
{
    public static class BenchmarkData
    {
        public static NdArray<double> A { get; } = Random.RandomNdArray.Rand64(new[] { 100, 100 });
        public static NdArray<double> B { get; } = Random.RandomNdArray.Rand64(new[] { 100, 100 });
        public static NdArray<double> C { get; } = Random.RandomNdArray.Rand64(new[] { 100, 100 });
        public static NdArray<double> D { get; } = Random.RandomNdArray.Rand64(new[] { 100, 100 });
        public static NdArray<double> E { get; } = Random.RandomNdArray.Rand64(new[] { 100, 100 });

        public static double[] NC_A { get; } = A.AsEnumerable().ToArray();
        public static double[] NC_B { get; } = B.AsEnumerable().ToArray();
        public static double[] NC_C { get; } = C.AsEnumerable().ToArray();
        public static double[] NC_D { get; } = D.AsEnumerable().ToArray();
        public static double[] NC_E { get; } = E.AsEnumerable().ToArray();
    }
}
