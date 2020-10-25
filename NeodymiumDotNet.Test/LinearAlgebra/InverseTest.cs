using System;
using System.Collections.Generic;
using NeodymiumDotNet.LinearAlgebra;
using NeodymiumDotNet.Random;
using Xunit;

namespace NeodymiumDotNet.Test.LinearAlgebra
{
    public class InverseTest
    {
        public static IEqualityComparer<NdArray<double>> Comparer { get; }
            = new NdArrayComparer<double>((x, y) => Math.Abs(x - y) < 1e-6);


        public static IEnumerable<object[]> TestData()
        {
            object[] core(NdArray<double> a, NdArray<double> inv)
                => new object[] { a, inv };

            yield return core(
                NdArray.Create(new[,] { {  1.0,  1.0,  1.0 },
                                        {  2.0,  1.0,  1.0 },
                                        {  2.0,  2.0,  1.0 }, }),
                NdArray.Create(new[,] { { -1.0,  1.0,  0.0 },
                                        {  0.0, -1.0,  1.0 },
                                        {  2.0,  0.0, -1.0 }, })
                );
            yield return core(
                NdArray.Create(new[,] { {  1.0,  2.0,  3.0,  4.0 },
                                        {  2.0,  2.0,  3.0,  4.0 },
                                        {  3.0,  3.0,  3.0,  4.0 },
                                        {  4.0,  4.0,  4.0,  4.0 } }),
                NdArray.Create(new[,] { { -1.0,  1.0,  0.0,  0.0 },
                                        {  1.0, -2.0,  1.0,  0.0 },
                                        {  0.0,  1.0, -2.0,  1.0 },
                                        {  0.0,  0.0,  1.0, -0.75} })
                );
            yield return core(
                NdArray.Create(new[,] { {  1.0,  2.0,  3.0,  4.0, 5.0 },
                                        {  2.0,  2.0,  3.0,  4.0, 5.0 },
                                        {  3.0,  3.0,  3.0,  4.0, 5.0 },
                                        {  4.0,  4.0,  4.0,  4.0, 5.0 },
                                        {  5.0,  5.0,  5.0,  5.0, 5.0 } }),
                NdArray.Create(new[,] { {-1.0,  1.0,  0.0,  0.0,  0.0 },
                                        { 1.0, -2.0,  1.0,  0.0,  0.0 },
                                        { 0.0,  1.0, -2.0,  1.0,  0.0 },
                                        { 0.0,  0.0,  1.0, -2.0,  1.0 },
                                        { 0.0,  0.0,  0.0,  1.0, -0.8 } })
                );
            yield return core(
                NdArray.Create(new[,] { { 5.0,  5.0,  5.0,  5.0,  5.0 },
                                        { 5.0,  4.0,  4.0,  4.0,  4.0 },
                                        { 5.0,  4.0,  3.0,  3.0,  3.0 },
                                        { 5.0,  4.0,  3.0,  2.0,  2.0 },
                                        { 5.0,  4.0,  3.0,  2.0,  1.0 } }),
                NdArray.Create(new[,] { {-0.8,  1.0,  0.0,  0.0,  0.0 },
                                        { 1.0, -2.0,  1.0,  0.0,  0.0 },
                                        { 0.0,  1.0, -2.0,  1.0,  0.0 },
                                        { 0.0,  0.0,  1.0, -2.0,  1.0 },
                                        { 0.0,  0.0,  0.0,  1.0, -1.0 } })
                );
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(NdArray<double> a, NdArray<double> inv)
        {
            Assert.Equal(inv, a.Inverse(), Comparer);
        }


        public static IEnumerable<object[]> RandomTestData()
        {
            for(var dim = 1 ; dim <= 20; ++dim)
            for(var i = 0 ; i < 1 ; ++i)
            {
                yield return new object[] { RandomNdArray.RandN64(new []{dim, dim}) };
            }
        }


        [Theory]
        [MemberData(nameof(RandomTestData))]
        public void RandomTest(NdArray<double> a)
        {
            Assert.Equal(NdLinAlg.Identity<double>(a.Shape[0]), a.DotLegacy(a.Inverse()), Comparer);
        }
    }
}
