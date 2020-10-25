using System;
using System.Collections.Generic;
using Xunit;
using NeodymiumDotNet.LinearAlgebra;

namespace NeodymiumDotNet.Test.LinearAlgebra
{
    public class CrossTest
    {

        public static IEnumerable<object[]> SuccessTestData
            => new[]
            {
                new object[]
                {
                    NdArray.Create(new double[] { 1, 2, 3 }),
                    NdArray.Create(new double[] { 2, 3, 4 }),
                    NdArray.Create(new double[] { -1, 2, -1 })
                },
            };


        public static IEnumerable<object[]> ErrorTestData
            => new[]
            {
                new object[]
                {
                    NdArray.Create(new double[] { 1, 2, 3, }),
                    NdArray.Create(new double[] { 2, 3 })
                },
            };


        [Theory]
        [MemberData(nameof(SuccessTestData))]
        public void LinqSuccessful(NdArray<double> x, NdArray<double> y,
                                       NdArray<double> answer)
        {
            Assert.Equal(answer, NdLinAlg.Cross(x, y));
        }


        [Theory]
        [MemberData(nameof(ErrorTestData))]
        public void LinqError(NdArray<double> x, NdArray<double> y)
        {
            Assert.Throws<ShapeMismatchException>(() => NdLinAlg.Cross(x, y));
        }

    }
}
