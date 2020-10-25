using System;
using System.Collections.Generic;
using System.Text;
using NeodymiumDotNet.LinearAlgebra;
using Xunit;

namespace NeodymiumDotNet.Test.LinearAlgebra
{
    public class DeterminantTest
    {
        public static IEnumerable<object[]> TestData()
        {
            object[] core(NdArray<double> a, double det)
                => new object[] { a, det };

            yield return core(NdArray.Create(new double[,] { { 1, 2, 3 },
                                                             { 4, 5, 6 },
                                                             { 7, 8, 9 } }), 0);

            yield return core(NdArray.Create(new double[,] { {  1,  2,  3,  4 },
                                                             {  5,  6,  7,  8 },
                                                             {  9, 10, 11, 12 },
                                                             { 13, 14, 15, 16 } }), 0);

            yield return core(NdArray.Create(new double[,] { { 10,  4,  2, 10,  5,  4,  4,  1,  1,  2 },
                                                             {  5,  1,  1,  7,  7,  2,  5,  5,  6,  4 },
                                                             {  2,  3,  6,  5,  4,  1,  9,  3,  6,  1 },
                                                             {  9,  3,  3,  8,  3,  5,  6,  6,  6,  5 },
                                                             {  0,  1,  6,  3,  0,  5,  8,  2,  8,  8 },
                                                             {  5,  4,  4,  7,  2,  9,  6,  1,  3,  5 },
                                                             {  8,  2,  6,  4,  1,  5,  8,  3,  3,  3 },
                                                             {  1,  3,  6,  7,  4, 10,  3,  1,  0,  4 },
                                                             { 10,  1,  2,  2,  3,  4,  6, 10,  8,  6 },
                                                             {  3,  8,  9,  4,  1,  3,  6, 10,  4,  1 } }), 18492754);

            yield return core(NdArray.Create(new double[,] { {  6,  4,  5,  3, 10,  8,  2,  2,  7,  9},
                                                             {  9,  0,  2,  5,  1, 10,  4,  4,  2,  4},
                                                             {  0,  1,  3,  6,  1,  2,  8,  4,  9,  1},
                                                             {  5,  1,  1,  7,  1,  4, 10,  4,  7,  6},
                                                             {  8,  5,  5,  4,  1,  1,  7, 10,  1,  7},
                                                             {  3,  3,  6,  2,  3,  9,  6,  6,  8,  7},
                                                             {  6,  1,  2,  8,  7,  5,  8,  7,  4,  5},
                                                             {  1,  2,  2,  5,  0,  6,  2,  6,  9,  7},
                                                             {  3,  7,  1,  8,  9,  5,  4,  1,  8,  1},
                                                             { 10,  4,  8,  2,  3,  1,  1,  3,  1,  7} }), -167329180);

        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void Determinant(NdArray<double> a, double det)
        {
            Assert.Equal(det, a.Determinant(), 6);
        }
    }
}
