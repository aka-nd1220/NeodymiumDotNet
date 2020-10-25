using System;
using System.Collections.Generic;
using Xunit;
using NeodymiumDotNet.Linq;
using NdArrayI = NeodymiumDotNet.NdArray<int>;

namespace NeodymiumDotNet.Test.Linq
{
    public class SelectTest
    {

        public static IEnumerable<object[]> TestSelectArgs()
        {
            var source = NdArray.Create(new[, ,]
            {
                {{  0,  1,  2,  3 }, {  4,  5,  6,  7 }, {  8,  9, 10, 11 }},
                {{ 12, 13, 14, 15 }, { 16, 17, 18, 19 }, { 20, 21, 22, 23 }},
            });

            object[] core(Func<int, int> selector, NdArrayI expected)
                => new object[]{source, selector, expected};

            yield return core(x => x,
                NdArray.Create(new [,,]
                {
                    {{  0,  1,  2,  3 }, {  4,  5,  6,  7 }, {  8,  9, 10, 11 }},
                    {{ 12, 13, 14, 15 }, { 16, 17, 18, 19 }, { 20, 21, 22, 23 }},
                }));
            yield return core(x => x + 1,
                NdArray.Create(new[,,]
                {
                    {{  1,  2,  3,  4 }, {  5,  6,  7,  8 }, {  9, 10, 11, 12 }},
                    {{ 13, 14, 15, 16 }, { 17, 18, 19, 20 }, { 21, 22, 23, 24 }},
                }));
        }


        [Theory]
        [MemberData(nameof(TestSelectArgs))]
        public void Select(
            NdArrayI source, Func<int, int> selector,
            NdArrayI expected)
        {
            Assert.Equal(expected, source.Select(selector));
        }


        public static IEnumerable<object[]> TestSelectByAxesArgs()
        {
            var source = NdArray.Create(new[, ,]
            {
                {{  0,  1,  2,  3 }, {  4,  5,  6,  7 }, {  8,  9, 10, 11 }},
                {{ 12, 13, 14, 15 }, { 16, 17, 18, 19 }, { 20, 21, 22, 23 }},
            });

            object[] core(int[] projectionAxes, Func<NdArrayI, int> selector, NdArrayI expected)
                => new object[] { source, projectionAxes, selector, expected };

            yield return core(new[] { 0 }, x => x[0],
                NdArray.Create(new[,]
                {
                    {  0,  1,  2,  3 }, {  4,  5,  6,  7 }, {  8,  9, 10, 11 },
                }));
            yield return core(new[] { 1 }, x => x[0],
                NdArray.Create(new[,]
                {
                    {  0,  1,  2,  3 },
                    { 12, 13, 14, 15 },
                }));
            yield return core(new[] { 2 }, x => x[0],
                NdArray.Create(new[,]
                {
                    {  0,  4,  8 },
                    { 12, 16, 20 },
                }));
            yield return core(new[] { 0, 1 }, x => x[0, 0],
                NdArray.Create(new[]
                {
                    0,  1,  2,  3
                }));
            yield return core(new[] { 0, 1 }, x => x[0, 0],
                NdArray.Create(new[]
                {
                    0,  1,  2,  3
                }));
            yield return core(new[] { 1, 2 }, x => x[0, 0],
                NdArray.Create(new[]
                {
                    0, 12
                }));
            yield return core(new[] { 2, 1 }, x => x[0, 0],
                NdArray.Create(new[]
                {
                    0, 12
                }));
            yield return core(new[] { 0, 2 }, x => x[0, 0],
                NdArray.Create(new[]
                {
                    0, 4, 8
                }));
            yield return core(new[] { 2, 0 }, x => x[0, 0],
                NdArray.Create(new[]
                {
                    0, 4, 8
                }));
        }


        [Theory]
        [MemberData(nameof(TestSelectByAxesArgs))]
        public void SelectByAxes(
            NdArrayI source, int[] projectionAxes, Func<NdArrayI, int> selector,
            NdArrayI expected)
        {
            Assert.Equal(expected, source.Select(projectionAxes, selector));
        }
    }
}
