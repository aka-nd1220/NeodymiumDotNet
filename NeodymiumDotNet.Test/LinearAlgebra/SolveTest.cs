using System;
using System.Collections.Generic;
using System.Text;
using NeodymiumDotNet.LinearAlgebra;
using Xunit;

namespace NeodymiumDotNet.Test.LinearAlgebra
{
    public class SolveTest
    {
        public static IEqualityComparer<NdArray<double>> Comparer { get; }
            = new NdArrayComparer<double>((x, y) => Math.Abs(x - y) < 1e-6);


        public static IEnumerable<object[]> TestData()
        {
            object[] core(NdArray<double> a, NdArray<double> b, NdArray<double> x)
                => new object[] { a, b, x };

            yield return core(
                NdArray.Create(new double[,] { {  1,  1,  1,  1 },
                                               {  1,  1,  1, -1 },
                                               {  1,  1, -1,  1 },
                                               {  1, -1,  1,  1 }, }),
                NdArray.Create(new double[] { 0, 4, -4, 2 }),
                NdArray.Create(new double[] { 1, -1, 2, -2 })
                );
            yield return core(
                NdArray.Create(new double[,] { {  1.0,  1.0,  1.0,  1.0 },
                                               {  1.0,  1.0,  1.0, -1.0 },
                                               {  1.0,  1.0, -1.0,  1.0 },
                                               {  1.0, -1.0,  1.0,  1.0 }, }),
                NdArray.Create(new double[,] { {  0.0,  1.0 },
                                               {  4.0,  2.0 },
                                               { -4.0,  3.0 },
                                               {  2.0,  4.0 }, }),
                // NdArray.Create(new double[,] { {  0.0,  4.0, -4.0,  2.0 },
                //                                {  1.0,  2.0,  3.0,  4.0 } }),
                NdArray.Create(new double[,] { {  1.0,  4.0 },
                                               { -1.0, -1.5 },
                                               {  2.0, -1.0 },
                                               { -2.0, -0.5 }, })
                // NdArray.Create(new double[,] { {  1.0, -1.0,  2.0, -2.0 },
                //                                {  4.0, -1.5, -1.0, -0.5 }, })
                );
            yield break;
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void Solve(NdArray<double> a, NdArray<double> b, NdArray<double> x)
        {
            Assert.Equal(x, a.Solve(b), Comparer);
        }
    }
}
