using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using NeodymiumDotNet.Optimizations;

namespace NeodymiumDotNet.Benchmark
{
    public class MemoryOperationSimdizerBenchmark
    {
        private const int _times = 1000;
        private const int _length = 65536;

        public static readonly double[] A = Random.RandomNdArray.Rand64(new[] { _length }).AsEnumerable().ToArray();
        public static readonly double[] B = Random.RandomNdArray.Rand64(new[] { _length }).AsEnumerable().ToArray();
        public static readonly double[] C = Random.RandomNdArray.Rand64(new[] { _length }).AsEnumerable().ToArray();
        public static readonly double[] D = Random.RandomNdArray.Rand64(new[] { _length }).AsEnumerable().ToArray();
        public static readonly double[] E = Random.RandomNdArray.Rand64(new[] { _length }).AsEnumerable().ToArray();


        [Benchmark]
        public double[] AddSimple()
        {
            var array = new double[_length];
            for(var j = 0; j < _times; ++j)
            {
                for(var i = 0; i < _length; ++i)
                    array[i] = A[i] + B[i] + C[i] + D[i] + E[i];
            }
            return array;
        }


        [Benchmark]
        public double[] AddSimd()
        {
            var array = new double[_length];
            var simdOp = VectorOperation.Simdize<double>((a, b, c, d, e) => a + b + c + d + e);
            for(var j = 0; j < _times; ++j)
            {
                simdOp(A, B, C, D, E, array);
            }
            return array;
        }
    }
}
