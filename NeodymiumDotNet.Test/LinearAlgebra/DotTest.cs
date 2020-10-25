using System;
using System.Collections.Generic;
using System.Numerics;
using Xunit;
using NeodymiumDotNet.LinearAlgebra;
using NeodymiumDotNet.Linq;

namespace NeodymiumDotNet.Test.LinearAlgebra
{
    public class DotTest
    {

        public static IEnumerable<object[]> SuccessTestData
            => new[]
            {
                new object[]
                {
                    NdArray.Create(new int[] { 0, 1, 2 })                ,
                    NdArray.Create(new int[] { 0, 1, 2 })                  ,
                    NdArray.Create(new int[] { 5 })
                },
                new object[]
                {
                    NdArray.Create(new int[,] { { 0, 1, 2 }, { 0, 2, 4 } }),
                    NdArray.Create(new int[] { 0, 1, 2 })                  ,
                    NdArray.Create(new int[] { 5, 10 })
                },
                new object[]
                {
                    NdArray.Create(new int[] { 0, 1, 2 })                ,
                    NdArray.Create(new int[,] { { 0, 0 }, { 1, 2 }, { 2, 4 } }),
                    NdArray.Create(new int[] { 5, 10 })
                },
                new object[]
                {
                    NdArray.Create(new int[,] { { 0, 1, 2 }, { 0, 2, 4 } }),
                    NdArray.Create(new int[,] { { 0, 0 }, { 1, 2 }, { 2, 4 } }),
                    NdArray.Create(new int[,] { { 5, 10 }, { 10, 20 } })
                },
            };


        public static IEnumerable<object[]> ErrorTestData
            => new[]
            {
                new object[] { NdArray.Create(new int[3]), NdArray.Create(new int[2]) },
                new object[]
                    { NdArray.Create(new int[2, 2]), NdArray.Create(new int[3]) },
                new object[]
                {
                    NdArray.Create(new int[2, 2, 2]), NdArray.Create(new int[2, 2, 2])
                },
            };


        [Theory]
        [MemberData(nameof(SuccessTestData))]
        public void Successful(NdArray<int> x, NdArray<int> y,
                                       NdArray<int> answer)
        {
            void core<T>(NdArray<T> x, NdArray<T> y, NdArray<T> answer)
            {
                Assert.Equal(answer, NdLinAlg.Dot(x, y));
            }

            core(x.Select(t => (byte   )t), y.Select(t => (byte   )t), answer.Select(t => (byte   )t));
            core(x.Select(t => (ushort )t), y.Select(t => (ushort )t), answer.Select(t => (ushort )t));
            core(x.Select(t => (uint   )t), y.Select(t => (uint   )t), answer.Select(t => (uint   )t));
            core(x.Select(t => (ulong  )t), y.Select(t => (ulong  )t), answer.Select(t => (ulong  )t));
            core(x.Select(t => (sbyte  )t), y.Select(t => (sbyte  )t), answer.Select(t => (sbyte  )t));
            core(x.Select(t => (short  )t), y.Select(t => (short  )t), answer.Select(t => (short  )t));
            core(x.Select(t => (int    )t), y.Select(t => (int    )t), answer.Select(t => (int    )t));
            core(x.Select(t => (long   )t), y.Select(t => (long   )t), answer.Select(t => (long   )t));
            core(x.Select(t => (float  )t), y.Select(t => (float  )t), answer.Select(t => (float  )t));
            core(x.Select(t => (double )t), y.Select(t => (double )t), answer.Select(t => (double )t));
            core(x.Select(t => (decimal)t), y.Select(t => (decimal)t), answer.Select(t => (decimal)t));
            core(x.Select(t => (Complex)t), y.Select(t => (Complex)t), answer.Select(t => (Complex)t));
        }


        [Theory]
        [MemberData(nameof(ErrorTestData))]
        public void Error(NdArray<int> x, NdArray<int> y)
        {
            Assert.Throws<ShapeMismatchException>(() => NdLinAlg.Dot(x, y));
        }


        [Theory]
        [MemberData(nameof(SuccessTestData))]
        public void SuccessfulLegacy(NdArray<int> x, NdArray<int> y,
                                       NdArray<int> answer)
        {
            void core<T>(NdArray<T> x, NdArray<T> y, NdArray<T> answer)
            {
                Assert.Equal(answer, NdLinAlg.DotLegacy(x, y));
            }

            core(x.Select(t => (byte   )t), y.Select(t => (byte   )t), answer.Select(t => (byte   )t));
            core(x.Select(t => (ushort )t), y.Select(t => (ushort )t), answer.Select(t => (ushort )t));
            core(x.Select(t => (uint   )t), y.Select(t => (uint   )t), answer.Select(t => (uint   )t));
            core(x.Select(t => (ulong  )t), y.Select(t => (ulong  )t), answer.Select(t => (ulong  )t));
            core(x.Select(t => (sbyte  )t), y.Select(t => (sbyte  )t), answer.Select(t => (sbyte  )t));
            core(x.Select(t => (short  )t), y.Select(t => (short  )t), answer.Select(t => (short  )t));
            core(x.Select(t => (int    )t), y.Select(t => (int    )t), answer.Select(t => (int    )t));
            core(x.Select(t => (long   )t), y.Select(t => (long   )t), answer.Select(t => (long   )t));
            core(x.Select(t => (float  )t), y.Select(t => (float  )t), answer.Select(t => (float  )t));
            core(x.Select(t => (double )t), y.Select(t => (double )t), answer.Select(t => (double )t));
            core(x.Select(t => (decimal)t), y.Select(t => (decimal)t), answer.Select(t => (decimal)t));
            core(x.Select(t => (Complex)t), y.Select(t => (Complex)t), answer.Select(t => (Complex)t));
        }


        [Theory]
        [MemberData(nameof(ErrorTestData))]
        public void ErrorLegacy(NdArray<int> x, NdArray<int> y)
        {
            Assert.Throws<ShapeMismatchException>(() => NdLinAlg.DotLegacy(x, y));
        }


    }
}
