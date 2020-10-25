using System;
using System.Collections.Generic;
using Xunit;
using NeodymiumDotNet.Optimizations.Linq;
using NdArrayI = NeodymiumDotNet.NdArray<int>;
using System.Linq.Expressions;
using NeodymiumDotNet.Linq;
using NeodymiumDotNet.Random;

namespace NeodymiumDotNet.Optimizations.Test.Linq
{
    public class SelectTest
    {
        public static IEnumerable<object[]> TestSelectArgs()
        {
            object[] core(NdArrayI source, Expression<Func<int, int>> selector)
                => new object[] { source, selector };

            yield return core(
                NdArray.Create(new[, ,]
                {
                    {{  0,  1,  2,  3 }, {  4,  5,  6,  7 }, {  8,  9, 10, 11 }},
                    {{ 12, 13, 14, 15 }, { 16, 17, 18, 19 }, { 20, 21, 22, 23 }},
                }), x=> x);
            yield return core(
                NdArray.Create(new[, ,]
                {
                    {{  1,  2,  3,  4 }, {  5,  6,  7,  8 }, {  9, 10, 11, 12 }},
                    {{ 13, 14, 15, 16 }, { 17, 18, 19, 20 }, { 21, 22, 23, 24 }},
                }), x => x + 1);
            yield return core(RandomNdArray.RandInt32(new[] { 2, 3, 4, 5, 6, 7, 8, 9 }), x => x);
            yield return core(RandomNdArray.RandInt32(new[] { 2, 3, 4, 5, 6, 7, 8, 9 }), x => x + 1);
            yield return core(RandomNdArray.RandInt32(new[] { 2, 3, 4, 5, 6, 7, 8, 9 }), x => 2 * x);
            yield return core(RandomNdArray.RandInt32(new[] { 2, 3, 4, 5, 6, 7, 8, 9 }), x => x / 2);
        }


        [Theory]
        [MemberData(nameof(TestSelectArgs))]
        public void Select(
            NdArrayI source, Expression<Func<int, int>> selector)
        {
            var expected = NdLinq.Select(source, selector.Compile());
            Assert.Equal(expected, source.Select(selector));
        }
    }
}
