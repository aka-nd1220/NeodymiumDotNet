using System;
using System.Collections.Generic;
using System.Text;
using NeodymiumDotNet.LinearAlgebra;
using Xunit;

namespace NeodymiumDotNet.Test.LinearAlgebra
{
    public class LUTest
    {
        public static bool Similar(double x, double y)
            => Math.Abs(x - y) < 1e-10;


        public static IEqualityComparer<INdArray<double>> Comparer { get; }
            = new NdArrayComparer<double>(Similar);


        public static IEnumerable<object?[]> TestData()
        {
            var strategies = new IIterationStrategy?[]
            {
                null,
                IterationStrategy.Default,
                ParallelIterationStrategy.Instance,
            };
            var arrays = new NdArray<double>[]
            {
                NdArray.Create(new double[,] { { 1, 2, 3 },
                                               { 4, 5, 6 },
                                               { 7, 8, 9 } }),
                NdArray.Create(new double[,] { {  1,  2,  3,  4 },
                                               {  5,  6,  7,  8 },
                                               {  9, 10, 11, 12 },
                                               { 13, 14, 15, 16 } }),
                NdArray.Create(new double[,] { { 10,  4,  2, 10,  5,  4,  4,  1,  1,  2 },
                                               {  5,  1,  1,  7,  7,  2,  5,  5,  6,  4 },
                                               {  2,  3,  6,  5,  4,  1,  9,  3,  6,  1 },
                                               {  9,  3,  3,  8,  3,  5,  6,  6,  6,  5 },
                                               {  0,  1,  6,  3,  0,  5,  8,  2,  8,  8 },
                                               {  5,  4,  4,  7,  2,  9,  6,  1,  3,  5 },
                                               {  8,  2,  6,  4,  1,  5,  8,  3,  3,  3 },
                                               {  1,  3,  6,  7,  4, 10,  3,  1,  0,  4 },
                                               { 10,  1,  2,  2,  3,  4,  6, 10,  8,  6 },
                                               {  3,  8,  9,  4,  1,  3,  6, 10,  4,  1 } }),
                NdArray.Create(new double[,] { {  6,  4,  5,  3, 10,  8,  2,  2,  7,  9},
                                               {  9,  0,  2,  5,  1, 10,  4,  4,  2,  4},
                                               {  0,  1,  3,  6,  1,  2,  8,  4,  9,  1},
                                               {  5,  1,  1,  7,  1,  4, 10,  4,  7,  6},
                                               {  8,  5,  5,  4,  1,  1,  7, 10,  1,  7},
                                               {  3,  3,  6,  2,  3,  9,  6,  6,  8,  7},
                                               {  6,  1,  2,  8,  7,  5,  8,  7,  4,  5},
                                               {  1,  2,  2,  5,  0,  6,  2,  6,  9,  7},
                                               {  3,  7,  1,  8,  9,  5,  4,  1,  8,  1},
                                               { 10,  4,  8,  2,  3,  1,  1,  3,  1,  7} }),
            };
            foreach(var strategy in strategies)
                foreach(var array in arrays)
                    yield return new object?[] { strategy, array };
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void LUP(IIterationStrategy? strategy, NdArray<double> a)
        {
            var (p, l, u) = a.LUP(strategy);
            Assert.True(IsL(l));
            Assert.True(IsU(u));
            Assert.Equal(a, p.Dot(l).Dot(u), Comparer);
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void LUPLegacy(IIterationStrategy? strategy, NdArray<double> a)
        {
            var (p, l, u) = a.LUPLegacy(strategy);
            Assert.True(IsL(l));
            Assert.True(IsU(u));
            Assert.Equal(a, p.Dot(l).Dot(u), Comparer);
        }


        private static bool IsL(NdArray<double> array)
        {
            var n = array.Shape[0];
            for(var i = 0; i < n; ++i)
                for(var j = i + 1; j < n; ++j)
                    if(!Similar(array[i, j], 0))
                        return false;

            for(var k = 0; k < n; ++k)
                if(!Similar(array[k, k], 1))
                    return false;

            return true;
        }


        private static bool IsU(NdArray<double> array)
        {
            var n = array.Shape[0];
            for(var j = 0; j < n; ++j)
                for(var i = j + 1; i < n; ++i)
                    if(!Similar(array[i, j], 0))
                        return false;

            return true;
        }
    }
}
