using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using NeodymiumDotNet.Linq;
using NeodymiumDotNet.Optimizations.Linq;

namespace NeodymiumDotNet.Benchmark
{
    public class SimpleCalculationBenchmark
    {
        public static NdArray<double> A1    { get; } = Random.RandomNdArray.Rand64(new[] {    1,    1 });
        public static NdArray<double> B1    { get; } = Random.RandomNdArray.Rand64(new[] {    1,    1 });
        public static NdArray<double> C1    { get; } = Random.RandomNdArray.Rand64(new[] {    1,    1 });
        public static NdArray<double> A3    { get; } = Random.RandomNdArray.Rand64(new[] {    3,    3 });
        public static NdArray<double> B3    { get; } = Random.RandomNdArray.Rand64(new[] {    3,    3 });
        public static NdArray<double> C3    { get; } = Random.RandomNdArray.Rand64(new[] {    3,    3 });
        public static NdArray<double> A10   { get; } = Random.RandomNdArray.Rand64(new[] {   10,   10 });
        public static NdArray<double> B10   { get; } = Random.RandomNdArray.Rand64(new[] {   10,   10 });
        public static NdArray<double> C10   { get; } = Random.RandomNdArray.Rand64(new[] {   10,   10 });
        public static NdArray<double> A30   { get; } = Random.RandomNdArray.Rand64(new[] {   30,   30 });
        public static NdArray<double> B30   { get; } = Random.RandomNdArray.Rand64(new[] {   30,   30 });
        public static NdArray<double> C30   { get; } = Random.RandomNdArray.Rand64(new[] {   30,   30 });
        public static NdArray<double> A100  { get; } = Random.RandomNdArray.Rand64(new[] {  100,  100 });
        public static NdArray<double> B100  { get; } = Random.RandomNdArray.Rand64(new[] {  100,  100 });
        public static NdArray<double> C100  { get; } = Random.RandomNdArray.Rand64(new[] {  100,  100 });
        public static NdArray<double> A300  { get; } = Random.RandomNdArray.Rand64(new[] {  300,  300 });
        public static NdArray<double> B300  { get; } = Random.RandomNdArray.Rand64(new[] {  300,  300 });
        public static NdArray<double> C300  { get; } = Random.RandomNdArray.Rand64(new[] {  300,  300 });
        public static NdArray<double> A1000 { get; } = Random.RandomNdArray.Rand64(new[] { 1000, 1000 });
        public static NdArray<double> B1000 { get; } = Random.RandomNdArray.Rand64(new[] { 1000, 1000 });
        public static NdArray<double> C1000 { get; } = Random.RandomNdArray.Rand64(new[] { 1000, 1000 });
        public static NdArray<double> A3000 { get; } = Random.RandomNdArray.Rand64(new[] { 3000, 3000 });
        public static NdArray<double> B3000 { get; } = Random.RandomNdArray.Rand64(new[] { 3000, 3000 });
        public static NdArray<double> C3000 { get; } = Random.RandomNdArray.Rand64(new[] { 3000, 3000 });

        public static double[] NC_A1    { get; } = A1   .AsEnumerable().ToArray();
        public static double[] NC_B1    { get; } = B1   .AsEnumerable().ToArray();
        public static double[] NC_C1    { get; } = C1   .AsEnumerable().ToArray();
        public static double[] NC_A3    { get; } = A3   .AsEnumerable().ToArray();
        public static double[] NC_B3    { get; } = B3   .AsEnumerable().ToArray();
        public static double[] NC_C3    { get; } = C3   .AsEnumerable().ToArray();
        public static double[] NC_A10   { get; } = A10  .AsEnumerable().ToArray();
        public static double[] NC_B10   { get; } = B10  .AsEnumerable().ToArray();
        public static double[] NC_C10   { get; } = C10  .AsEnumerable().ToArray();
        public static double[] NC_A30   { get; } = A30  .AsEnumerable().ToArray();
        public static double[] NC_B30   { get; } = B30  .AsEnumerable().ToArray();
        public static double[] NC_C30   { get; } = C30  .AsEnumerable().ToArray();
        public static double[] NC_A100  { get; } = A100 .AsEnumerable().ToArray();
        public static double[] NC_B100  { get; } = B100 .AsEnumerable().ToArray();
        public static double[] NC_C100  { get; } = C100 .AsEnumerable().ToArray();
        public static double[] NC_A300  { get; } = A300 .AsEnumerable().ToArray();
        public static double[] NC_B300  { get; } = B300 .AsEnumerable().ToArray();
        public static double[] NC_C300  { get; } = C300 .AsEnumerable().ToArray();
        public static double[] NC_A1000 { get; } = A1000.AsEnumerable().ToArray();
        public static double[] NC_B1000 { get; } = B1000.AsEnumerable().ToArray();
        public static double[] NC_C1000 { get; } = C1000.AsEnumerable().ToArray();
        public static double[] NC_A3000 { get; } = A3000.AsEnumerable().ToArray();
        public static double[] NC_B3000 { get; } = B3000.AsEnumerable().ToArray();
        public static double[] NC_C3000 { get; } = C3000.AsEnumerable().ToArray();


        public SimpleCalculationBenchmark()
        {
            var answers = new[]
            {
                new[]{AddSimple1   ().AsEnumerable().ToArray(), AddParallel1   ().AsEnumerable().ToArray(), AddSimple1   ().AsEnumerable().ToArray(), AddSimpleNegativeControl1   (), AddSimdNegativeControl1   () },
                new[]{AddSimple3   ().AsEnumerable().ToArray(), AddParallel3   ().AsEnumerable().ToArray(), AddSimple3   ().AsEnumerable().ToArray(), AddSimpleNegativeControl3   (), AddSimdNegativeControl3   () },
                new[]{AddSimple10  ().AsEnumerable().ToArray(), AddParallel10  ().AsEnumerable().ToArray(), AddSimple10  ().AsEnumerable().ToArray(), AddSimpleNegativeControl10  (), AddSimdNegativeControl10  () },
                new[]{AddSimple30  ().AsEnumerable().ToArray(), AddParallel30  ().AsEnumerable().ToArray(), AddSimple30  ().AsEnumerable().ToArray(), AddSimpleNegativeControl30  (), AddSimdNegativeControl30  () },
                new[]{AddSimple100 ().AsEnumerable().ToArray(), AddParallel100 ().AsEnumerable().ToArray(), AddSimple100 ().AsEnumerable().ToArray(), AddSimpleNegativeControl100 (), AddSimdNegativeControl100 () },
                new[]{AddSimple300 ().AsEnumerable().ToArray(), AddParallel300 ().AsEnumerable().ToArray(), AddSimple300 ().AsEnumerable().ToArray(), AddSimpleNegativeControl300 (), AddSimdNegativeControl300 () },
                new[]{AddSimple1000().AsEnumerable().ToArray(), AddParallel1000().AsEnumerable().ToArray(), AddSimple1000().AsEnumerable().ToArray(), AddSimpleNegativeControl1000(), AddSimdNegativeControl1000() },
                new[]{AddSimple3000().AsEnumerable().ToArray(), AddParallel3000().AsEnumerable().ToArray(), AddSimple3000().AsEnumerable().ToArray(), AddSimpleNegativeControl3000(), AddSimdNegativeControl3000() },
            };

            foreach(var answer in answers)
            {
                for(var i = 1; i < answer.Length; ++i)
                {
                    if(!Enumerable.SequenceEqual(answer[0], answer[i]))
                        throw new InvalidOperationException();
                }
            }
        }


        [Benchmark] public NdArray<double> AddSimple1   () => AddSimpleCore(A1   , B1   , C1   );
        [Benchmark] public NdArray<double> AddSimple3   () => AddSimpleCore(A3   , B3   , C3   );
        [Benchmark] public NdArray<double> AddSimple10  () => AddSimpleCore(A10  , B10  , C10  );
        [Benchmark] public NdArray<double> AddSimple30  () => AddSimpleCore(A30  , B30  , C30  );
        [Benchmark] public NdArray<double> AddSimple100 () => AddSimpleCore(A100 , B100 , C100 );
        [Benchmark] public NdArray<double> AddSimple300 () => AddSimpleCore(A300 , B300 , C300 );
        [Benchmark] public NdArray<double> AddSimple1000() => AddSimpleCore(A1000, B1000, C1000);
        [Benchmark] public NdArray<double> AddSimple3000() => AddSimpleCore(A3000, B3000, C3000);
        [Benchmark] public NdArray<double> AddParallel1   () => AddParallelCore(A1   , B1   , C1   );
        [Benchmark] public NdArray<double> AddParallel3   () => AddParallelCore(A3   , B3   , C3   );
        [Benchmark] public NdArray<double> AddParallel10  () => AddParallelCore(A10  , B10  , C10  );
        [Benchmark] public NdArray<double> AddParallel30  () => AddParallelCore(A30  , B30  , C30  );
        [Benchmark] public NdArray<double> AddParallel100 () => AddParallelCore(A100 , B100 , C100 );
        [Benchmark] public NdArray<double> AddParallel300 () => AddParallelCore(A300 , B300 , C300 );
        [Benchmark] public NdArray<double> AddParallel1000() => AddParallelCore(A1000, B1000, C1000);
        [Benchmark] public NdArray<double> AddParallel3000() => AddParallelCore(A3000, B3000, C3000);
        [Benchmark] public NdArray<double> AddSimd1   () => AddSimdCore(A1   , B1   , C1   );
        [Benchmark] public NdArray<double> AddSimd3   () => AddSimdCore(A3   , B3   , C3   );
        [Benchmark] public NdArray<double> AddSimd10  () => AddSimdCore(A10  , B10  , C10  );
        [Benchmark] public NdArray<double> AddSimd30  () => AddSimdCore(A30  , B30  , C30  );
        [Benchmark] public NdArray<double> AddSimd100 () => AddSimdCore(A100 , B100 , C100 );
        [Benchmark] public NdArray<double> AddSimd300 () => AddSimdCore(A300 , B300 , C300 );
        [Benchmark] public NdArray<double> AddSimd1000() => AddSimdCore(A1000, B1000, C1000);
        [Benchmark] public NdArray<double> AddSimd3000() => AddSimdCore(A3000, B3000, C3000);
        [Benchmark] public double[] AddSimpleNegativeControl1   () => AddSimpleNegativeControlCore(NC_A1   , NC_B1   , NC_C1   );
        [Benchmark] public double[] AddSimpleNegativeControl3   () => AddSimpleNegativeControlCore(NC_A3   , NC_B3   , NC_C3   );
        [Benchmark] public double[] AddSimpleNegativeControl10  () => AddSimpleNegativeControlCore(NC_A10  , NC_B10  , NC_C10  );
        [Benchmark] public double[] AddSimpleNegativeControl30  () => AddSimpleNegativeControlCore(NC_A30  , NC_B30  , NC_C30  );
        [Benchmark] public double[] AddSimpleNegativeControl100 () => AddSimpleNegativeControlCore(NC_A100 , NC_B100 , NC_C100 );
        [Benchmark] public double[] AddSimpleNegativeControl300 () => AddSimpleNegativeControlCore(NC_A300 , NC_B300 , NC_C300 );
        [Benchmark] public double[] AddSimpleNegativeControl1000() => AddSimpleNegativeControlCore(NC_A1000, NC_B1000, NC_C1000);
        [Benchmark] public double[] AddSimpleNegativeControl3000() => AddSimpleNegativeControlCore(NC_A3000, NC_B3000, NC_C3000);
        [Benchmark] public double[] AddSimdNegativeControl1   () => AddSimdNegativeControlCore(NC_A1   , NC_B1   , NC_C1   );
        [Benchmark] public double[] AddSimdNegativeControl3   () => AddSimdNegativeControlCore(NC_A3   , NC_B3   , NC_C3   );
        [Benchmark] public double[] AddSimdNegativeControl10  () => AddSimdNegativeControlCore(NC_A10  , NC_B10  , NC_C10  );
        [Benchmark] public double[] AddSimdNegativeControl30  () => AddSimdNegativeControlCore(NC_A30  , NC_B30  , NC_C30  );
        [Benchmark] public double[] AddSimdNegativeControl100 () => AddSimdNegativeControlCore(NC_A100 , NC_B100 , NC_C100 );
        [Benchmark] public double[] AddSimdNegativeControl300 () => AddSimdNegativeControlCore(NC_A300 , NC_B300 , NC_C300 );
        [Benchmark] public double[] AddSimdNegativeControl1000() => AddSimdNegativeControlCore(NC_A1000, NC_B1000, NC_C1000);
        [Benchmark] public double[] AddSimdNegativeControl3000() => AddSimdNegativeControlCore(NC_A3000, NC_B3000, NC_C3000);


        private static NdArray<double> AddSimpleCore(NdArray<double> A, NdArray<double> B, NdArray<double> C)
            => (A, B, C).Zip((a, b, c) => a + b + c, IterationStrategy.Default);


        private static NdArray<double> AddParallelCore(NdArray<double> A, NdArray<double> B, NdArray<double> C)
            => (A, B, C).Zip((a, b, c) => a + b + c, ParallelIterationStrategy.Instance);


        private static NdArray<double> AddSimdCore(NdArray<double> A, NdArray<double> B, NdArray<double> C)
            => (A, B, C).Zip<double>((a, b, c) => a + b + c);


        private double[] AddSimpleNegativeControlCore(double[] A, double[] B, double[] C)
        {
            var res = new double[A.Length];
            for(var i = 0; i < res.Length; ++i)
                res[i] = A[i] + B[i] + C[i];
            return res;
        }


        private double[] AddSimdNegativeControlCore(double[] A, double[] B, double[] C)
        {
            var res = new double[A.Length];
            var res_vec = MemoryMarshal.Cast<double, Vector<double>>(res.AsSpan());
            var a_vec = MemoryMarshal.Cast<double, Vector<double>>(A.AsSpan());
            var b_vec = MemoryMarshal.Cast<double, Vector<double>>(B.AsSpan());
            var c_vec = MemoryMarshal.Cast<double, Vector<double>>(C.AsSpan());
            for(var i = 0; i < res_vec.Length; ++i)
                res_vec[i] = a_vec[i] + b_vec[i] + c_vec[i];
            for(var i = Vector<double>.Count * res_vec.Length; i < res.Length; ++i)
                res[i] = A[i] + B[i] + C[i];
            return res;
        }
    }
}
