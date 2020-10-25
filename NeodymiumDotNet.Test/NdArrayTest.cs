using System;
using System.Collections.Generic;
using Xunit;

namespace NeodymiumDotNet.Test
{
    public class NdArrayTest
    {

        [Theory]
        [InlineData(new[] { 1 },                new[] { 0 },             0)]
        [InlineData(new[] { 3 },              new[] { 2 },             2)]
        [InlineData(new[] { 2, 3 },           new[] { 1, 1 },          4)]
        [InlineData(new[] { 2, 3, 5, 7, 11 }, new[] { 1, 2, 3, 4, 5 }, 2205)]
        public void ToFlattenIndex(int[] shape, int[] shapedIndices, int ans)
        {
            var imShape = new IndexArray(shape);
            Assert.Equal(ans, NdArrayImpl<object>.ToFlattenIndex(imShape, shapedIndices));
        }


        [Theory]
        [InlineData(new[] { 1 },              0,    new[] { 0 }              )]
        [InlineData(new[] { 3 },              2,    new[] { 2 }            )]
        [InlineData(new[] { 2, 3 },           4,    new[] { 1, 1 }         )]
        [InlineData(new[] { 2, 3, 5, 7, 11 }, 2205, new[] { 1, 2, 3, 4, 5 })]
        public void ToShapedIndices(int[] shape, int flattenIndex, int[] ans)
        {
            var imShape = new IndexArray(shape);
            Assert.Equal(ans, NdArrayImpl.ToShapedIndices(imShape, flattenIndex));
        }


        [Fact]
        public void Create()
        {
            void doNothing<T>(T value)
            {
            }

            Assert.Equal(new IndexArray(0               ),
                         NdArray.Create(new int[0               ]).Shape);
            Assert.Equal(new IndexArray(0, 0            ),
                         NdArray.Create(new int[0, 0            ]).Shape);
            Assert.Equal(new IndexArray(0, 0, 0         ),
                         NdArray.Create(new int[0, 0, 0         ]).Shape);
            Assert.Equal(new IndexArray(0, 0, 0, 0      ),
                         NdArray.Create(new int[0, 0, 0, 0      ]).Shape);
            Assert.Equal(new IndexArray(0, 0, 0, 0, 0   ),
                         NdArray.Create(new int[0, 0, 0, 0, 0   ]).Shape);
            Assert.Equal(new IndexArray(0, 0, 0, 0, 0, 0),
                         NdArray.Create(new int[0, 0, 0, 0, 0, 0]).Shape);

            {
                var ndarray = NdArray.Create(new int[] { 0, 1, 2, 3 });
                Assert.Equal(new IndexArray(4), ndarray.Shape);
                Assert.Equal(0, ndarray[0]);
                Assert.Equal(1, ndarray[1]);
                Assert.Equal(2, ndarray[2]);
                Assert.Equal(3, ndarray[3]);
                Assert.Equal(0, ndarray[-4]);
                Assert.Equal(1, ndarray[-3]);
                Assert.Equal(2, ndarray[-2]);
                Assert.Equal(3, ndarray[-1]);
                Assert.Throws<ArgumentOutOfRangeException>(() => doNothing(ndarray[4]));
                Assert.Throws<ArgumentOutOfRangeException>(() => doNothing(ndarray[0, 0]));
            }
            {
                var ndarray = NdArray.Create(new int[,] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 } });
                Assert.Equal(new IndexArray(2, 4), ndarray.Shape);
                Assert.Equal(0, ndarray[0,  0]);
                Assert.Equal(1, ndarray[0,  1]);
                Assert.Equal(2, ndarray[0,  2]);
                Assert.Equal(3, ndarray[0,  3]);
                Assert.Equal(4, ndarray[1,  0]);
                Assert.Equal(5, ndarray[1,  1]);
                Assert.Equal(6, ndarray[1,  2]);
                Assert.Equal(7, ndarray[1,  3]);
                Assert.Equal(0, ndarray[0, -4]);
                Assert.Equal(1, ndarray[0, -3]);
                Assert.Equal(2, ndarray[0, -2]);
                Assert.Equal(3, ndarray[0, -1]);
                Assert.Equal(4, ndarray[1, -4]);
                Assert.Equal(5, ndarray[1, -3]);
                Assert.Equal(6, ndarray[1, -2]);
                Assert.Equal(7, ndarray[1, -1]);
                Assert.Equal(0, ndarray[-2,  0]);
                Assert.Equal(1, ndarray[-2,  1]);
                Assert.Equal(2, ndarray[-2,  2]);
                Assert.Equal(3, ndarray[-2,  3]);
                Assert.Equal(4, ndarray[-1,  0]);
                Assert.Equal(5, ndarray[-1,  1]);
                Assert.Equal(6, ndarray[-1,  2]);
                Assert.Equal(7, ndarray[-1,  3]);
                Assert.Equal(0, ndarray[-2, -4]);
                Assert.Equal(1, ndarray[-2, -3]);
                Assert.Equal(2, ndarray[-2, -2]);
                Assert.Equal(3, ndarray[-2, -1]);
                Assert.Equal(4, ndarray[-1, -4]);
                Assert.Equal(5, ndarray[-1, -3]);
                Assert.Equal(6, ndarray[-1, -2]);
                Assert.Equal(7, ndarray[-1, -1]);
                Assert.Throws<ArgumentOutOfRangeException>(() => doNothing(ndarray[0]));
                Assert.Throws<ArgumentOutOfRangeException>(() => doNothing(ndarray[2,  0]));
                Assert.Throws<ArgumentOutOfRangeException>(() => doNothing(ndarray[0,  4]));
                Assert.Throws<ArgumentOutOfRangeException>(() => doNothing(ndarray[0, 0, 0]));
            }
            {
                var ndarray = NdArray.Create(new int[,,]
                {
                    { { 0,  1,  2,  3 }, { 4,  5,  6,  7 }, { 8,  9, 10, 11 } },
                    { { 12, 13, 14, 15 }, { 16, 17, 18, 19 }, { 20, 21, 22, 23 } }
                });
                Assert.Equal(new IndexArray(2, 3, 4), ndarray.Shape);
                Assert.Equal(0, ndarray[0,  0,  0]);
                Assert.Equal(1, ndarray[0,  0,  1]);
                Assert.Equal(2, ndarray[0,  0,  2]);
                Assert.Equal(3, ndarray[0,  0,  3]);
                Assert.Equal(4, ndarray[0,  1,  0]);
                Assert.Equal(5, ndarray[0,  1,  1]);
                Assert.Equal(6, ndarray[0,  1,  2]);
                Assert.Equal(7, ndarray[0,  1,  3]);
                Assert.Equal(8, ndarray[0,  2,  0]);
                Assert.Equal(9, ndarray[0,  2,  1]);
                Assert.Equal(10, ndarray[0,  2,  2]);
                Assert.Equal(11, ndarray[0,  2,  3]);
                Assert.Equal(12, ndarray[1,  0,  0]);
                Assert.Equal(13, ndarray[1,  0,  1]);
                Assert.Equal(14, ndarray[1,  0,  2]);
                Assert.Equal(15, ndarray[1,  0,  3]);
                Assert.Equal(16, ndarray[1,  1,  0]);
                Assert.Equal(17, ndarray[1,  1,  1]);
                Assert.Equal(18, ndarray[1,  1,  2]);
                Assert.Equal(19, ndarray[1,  1,  3]);
                Assert.Equal(20, ndarray[1,  2,  0]);
                Assert.Equal(21, ndarray[1,  2,  1]);
                Assert.Equal(22, ndarray[1,  2,  2]);
                Assert.Equal(23, ndarray[1,  2,  3]);
                Assert.Equal(0, ndarray[-2, -3, -4]);
                Assert.Equal(1, ndarray[-2, -3, -3]);
                Assert.Equal(2, ndarray[-2, -3, -2]);
                Assert.Equal(3, ndarray[-2, -3, -1]);
                Assert.Equal(4, ndarray[-2, -2, -4]);
                Assert.Equal(5, ndarray[-2, -2, -3]);
                Assert.Equal(6, ndarray[-2, -2, -2]);
                Assert.Equal(7, ndarray[-2, -2, -1]);
                Assert.Equal(8, ndarray[-2, -1, -4]);
                Assert.Equal(9, ndarray[-2, -1, -3]);
                Assert.Equal(10, ndarray[-2, -1, -2]);
                Assert.Equal(11, ndarray[-2, -1, -1]);
                Assert.Equal(12, ndarray[-1, -3, -4]);
                Assert.Equal(13, ndarray[-1, -3, -3]);
                Assert.Equal(14, ndarray[-1, -3, -2]);
                Assert.Equal(15, ndarray[-1, -3, -1]);
                Assert.Equal(16, ndarray[-1, -2, -4]);
                Assert.Equal(17, ndarray[-1, -2, -3]);
                Assert.Equal(18, ndarray[-1, -2, -2]);
                Assert.Equal(19, ndarray[-1, -2, -1]);
                Assert.Equal(20, ndarray[-1, -1, -4]);
                Assert.Equal(21, ndarray[-1, -1, -3]);
                Assert.Equal(22, ndarray[-1, -1, -2]);
                Assert.Equal(23, ndarray[-1, -1, -1]);
                Assert.Throws<ArgumentOutOfRangeException>(() => doNothing(ndarray[0]));
                Assert.Throws<ArgumentOutOfRangeException>(() => doNothing(ndarray[0, 0]));
                Assert.Throws<ArgumentOutOfRangeException>(() => doNothing(ndarray[2,  0,  0]));
                Assert.Throws<ArgumentOutOfRangeException>(() => doNothing(ndarray[0,  3,  0]));
                Assert.Throws<ArgumentOutOfRangeException>(() => doNothing(ndarray[0,  0,  4]));
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                                                               doNothing(ndarray[0, 0, 0, 0]));
            }
        }


        [Fact]
        public void Slice()
        {
            void core(NdArray<int> input, IndexOrRange[] indices, NdArray<int> ans)
                => Assert.Equal(ans, input[indices]);

            core(NdArray.Create(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }),
                 new IndexOrRange[] { Range.Create(0, 8) },
                 NdArray.Create(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 })
                );
            core(NdArray.Create(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }),
                 new IndexOrRange[] { Range.Create(0, 4) },
                 NdArray.Create(new int[] { 0, 1, 2, 3 })
                );
            core(NdArray.Create(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }),
                 new IndexOrRange[] { Range.Create(0, -4) },
                 NdArray.Create(new int[] { 0, 1, 2, 3 })
                );
            core(NdArray.Create(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }),
                 new IndexOrRange[] { Range.Create(0, 8, 2) },
                 NdArray.Create(new int[] { 0, 2, 4, 6 })
                );

            core(NdArray.Create(new int[,] { { 0, 1, 2, 3, 4 }, { 5, 6, 7, 8, 9 }, { 10, 11, 12, 13, 14 }, { 15, 16, 17, 18, 19 } }),
                 new IndexOrRange[] { Range.Create(0, 4), Range.Create(0, 5) },
                 NdArray.Create(new int[,]
                 {
                     { 0, 1, 2, 3, 4 }, { 5, 6, 7, 8, 9 }, { 10, 11, 12, 13, 14 },
                     { 15, 16, 17, 18, 19 }
                 })
                );
            core(NdArray.Create(new int[,] { { 0, 1, 2, 3, 4 }, { 5, 6, 7, 8, 9 }, { 10, 11, 12, 13, 14 }, { 15, 16, 17, 18, 19 } }),
                 new IndexOrRange[] { Range.Create(1, 3), Range.Create(1, 4) },
                 NdArray.Create(new int[,] { { 6, 7, 8 }, { 11, 12, 13 } })
                );
            core(NdArray.Create(new int[,] { { 0, 1, 2, 3, 4 }, { 5, 6, 7, 8, 9 }, { 10, 11, 12, 13, 14 }, { 15, 16, 17, 18, 19 } }),
                 new IndexOrRange[] { Range.Create(1, -1), Range.Create(1, -1) },
                 NdArray.Create(new int[,] { { 6, 7, 8 }, { 11, 12, 13 } })
                );
            core(NdArray.Create(new int[,] { { 0, 1, 2, 3, 4 }, { 5, 6, 7, 8, 9 }, { 10, 11, 12, 13, 14 }, { 15, 16, 17, 18, 19 } }),
                 new IndexOrRange[] { Range.Create(1, 4, 2), Range.Create(1, 4, 2) },
                 NdArray.Create(new int[,] { { 6, 8 }, { 16, 18 } })
                );

            core(NdArray.Create(new int[,,]
                 {
                     {
                         {  0,   1,   2,   3,   4,   5 }, { 10,  11,  12,  13,  14,  15 },
                         { 20,  21,  22,  23,  24,  25 }, { 30,  31,  32,  33,  34,  35 },
                         { 40,  41,  42,  43,  44,  45 }
                     },
                     {
                         { 100, 101, 102, 103, 104, 105 }, { 110, 111, 112, 113, 114, 115 },
                         { 120, 121, 122, 123, 124, 125 }, { 130, 131, 132, 133, 134, 135 },
                         { 140, 141, 142, 143, 144, 145 }
                     },
                     {
                         { 200, 201, 202, 203, 204, 205 }, { 210, 211, 212, 213, 214, 215 },
                         { 220, 221, 222, 223, 224, 225 }, { 230, 231, 232, 233, 234, 235 },
                         { 240, 241, 242, 243, 244, 245 }
                     },
                     {
                         { 300, 301, 302, 303, 304, 305 }, { 310, 311, 312, 313, 314, 315 },
                         { 320, 321, 322, 323, 324, 325 }, { 330, 331, 332, 333, 334, 335 },
                         { 340, 341, 342, 343, 344, 345 }
                     },
                 }),
                 new IndexOrRange[]
                     { Range.Create(0, 4), Range.Create(0, 5), Range.Create(0, 6) },
                 NdArray.Create(new int[,,]
                 {
                     {
                         {  0,   1,   2,   3,   4,   5 }, { 10,  11,  12,  13,  14,  15 },
                         { 20,  21,  22,  23,  24,  25 }, { 30,  31,  32,  33,  34,  35 },
                         { 40,  41,  42,  43,  44,  45 }
                     },
                     {
                         { 100, 101, 102, 103, 104, 105 }, { 110, 111, 112, 113, 114, 115 },
                         { 120, 121, 122, 123, 124, 125 }, { 130, 131, 132, 133, 134, 135 },
                         { 140, 141, 142, 143, 144, 145 }
                     },
                     {
                         { 200, 201, 202, 203, 204, 205 }, { 210, 211, 212, 213, 214, 215 },
                         { 220, 221, 222, 223, 224, 225 }, { 230, 231, 232, 233, 234, 235 },
                         { 240, 241, 242, 243, 244, 245 }
                     },
                     {
                         { 300, 301, 302, 303, 304, 305 }, { 310, 311, 312, 313, 314, 315 },
                         { 320, 321, 322, 323, 324, 325 }, { 330, 331, 332, 333, 334, 335 },
                         { 340, 341, 342, 343, 344, 345 }
                     },
                 })
                );
            core(NdArray.Create(new int[,,]
                 {
                     {
                         {  0,   1,   2,   3,   4,   5 }, { 10,  11,  12,  13,  14,  15 },
                         { 20,  21,  22,  23,  24,  25 }, { 30,  31,  32,  33,  34,  35 },
                         { 40,  41,  42,  43,  44,  45 }
                     },
                     {
                         { 100, 101, 102, 103, 104, 105 }, { 110, 111, 112, 113, 114, 115 },
                         { 120, 121, 122, 123, 124, 125 }, { 130, 131, 132, 133, 134, 135 },
                         { 140, 141, 142, 143, 144, 145 }
                     },
                     {
                         { 200, 201, 202, 203, 204, 205 }, { 210, 211, 212, 213, 214, 215 },
                         { 220, 221, 222, 223, 224, 225 }, { 230, 231, 232, 233, 234, 235 },
                         { 240, 241, 242, 243, 244, 245 }
                     },
                     {
                         { 300, 301, 302, 303, 304, 305 }, { 310, 311, 312, 313, 314, 315 },
                         { 320, 321, 322, 323, 324, 325 }, { 330, 331, 332, 333, 334, 335 },
                         { 340, 341, 342, 343, 344, 345 }
                     },
                 }),
                 new IndexOrRange[]
                     { Range.Create(1, 3), Range.Create(1, 4), Range.Create(1, 5) },
                 NdArray.Create(new int[,,]
                 {
                     { { 111, 112, 113, 114 }, { 121, 122, 123, 124 }, { 131, 132, 133, 134 } },
                     { { 211, 212, 213, 214 }, { 221, 222, 223, 224 }, { 231, 232, 233, 234 } },
                 })
                );
            core(NdArray.Create(new int[,,]
                 {
                     {
                         {  0,   1,   2,   3,   4,   5 }, { 10,  11,  12,  13,  14,  15 },
                         { 20,  21,  22,  23,  24,  25 }, { 30,  31,  32,  33,  34,  35 },
                         { 40,  41,  42,  43,  44,  45 }
                     },
                     {
                         { 100, 101, 102, 103, 104, 105 }, { 110, 111, 112, 113, 114, 115 },
                         { 120, 121, 122, 123, 124, 125 }, { 130, 131, 132, 133, 134, 135 },
                         { 140, 141, 142, 143, 144, 145 }
                     },
                     {
                         { 200, 201, 202, 203, 204, 205 }, { 210, 211, 212, 213, 214, 215 },
                         { 220, 221, 222, 223, 224, 225 }, { 230, 231, 232, 233, 234, 235 },
                         { 240, 241, 242, 243, 244, 245 }
                     },
                     {
                         { 300, 301, 302, 303, 304, 305 }, { 310, 311, 312, 313, 314, 315 },
                         { 320, 321, 322, 323, 324, 325 }, { 330, 331, 332, 333, 334, 335 },
                         { 340, 341, 342, 343, 344, 345 }
                     },
                 }),
                 new IndexOrRange[]
                     { Range.Create(1, -1), Range.Create(1, -1), Range.Create(1, -1) },
                 NdArray.Create(new int[,,]
                 {
                     { { 111, 112, 113, 114 }, { 121, 122, 123, 124 }, { 131, 132, 133, 134 } },
                     { { 211, 212, 213, 214 }, { 221, 222, 223, 224 }, { 231, 232, 233, 234 } },
                 })
                );
            core(NdArray.Create(new int[,,]
                 {
                     {
                         {  0,   1,   2,   3,   4,   5 }, { 10,  11,  12,  13,  14,  15 },
                         { 20,  21,  22,  23,  24,  25 }, { 30,  31,  32,  33,  34,  35 },
                         { 40,  41,  42,  43,  44,  45 }
                     },
                     {
                         { 100, 101, 102, 103, 104, 105 }, { 110, 111, 112, 113, 114, 115 },
                         { 120, 121, 122, 123, 124, 125 }, { 130, 131, 132, 133, 134, 135 },
                         { 140, 141, 142, 143, 144, 145 }
                     },
                     {
                         { 200, 201, 202, 203, 204, 205 }, { 210, 211, 212, 213, 214, 215 },
                         { 220, 221, 222, 223, 224, 225 }, { 230, 231, 232, 233, 234, 235 },
                         { 240, 241, 242, 243, 244, 245 }
                     },
                     {
                         { 300, 301, 302, 303, 304, 305 }, { 310, 311, 312, 313, 314, 315 },
                         { 320, 321, 322, 323, 324, 325 }, { 330, 331, 332, 333, 334, 335 },
                         { 340, 341, 342, 343, 344, 345 }
                     },
                 }),
                 new IndexOrRange[]
                     { Range.Create(0, 4, 3), Range.Create(0, 5, 3), Range.Create(0, 6, 3) },
                 NdArray.Create(new int[,,]
                 {
                     { {  0,   3, }, { 30,  33 } },
                     { { 300, 303, }, { 330, 333 } },
                 })
                );
        }

    }
}
