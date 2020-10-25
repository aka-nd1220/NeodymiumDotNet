using System;
using System.Collections.Generic;
using Xunit;
using NeodymiumDotNet.Linq;
using NdArrayI = NeodymiumDotNet.NdArray<int>;

namespace NeodymiumDotNet.Test.Linq
{
    public class WhereTest
    {

        public static IEnumerable<object[]> TestWhereArgs()
        {
            var source = NdArray.Create(new[, ,]
            {
                {{  0,  1,  2,  3 }, {  4,  5,  6,  7 }, {  8,  9, 10, 11 }},
                {{ 12, 13, 14, 15 }, { 16, 17, 18, 19 }, { 20, 21, 22, 23 }},
            });
            bool predicate(NdArrayI x) => x[0, 0] == 0;

            object[] core(
                int axis,
                NdArrayI expected)
                => new object[] { source, axis, (Func<NdArrayI, bool>)predicate, expected };

            yield return core(0,
                NdArray.Create(new [,,]
                {
                    {{  0,  1,  2,  3 }, {  4,  5,  6,  7 }, {  8,  9, 10, 11 }},
                }));
            yield return core(1,
                NdArray.Create(new[, ,]
                {
                    {{  0,  1,  2,  3 }},
                    {{ 12, 13, 14, 15 }},
                }));
            yield return core(2,
                NdArray.Create(new[, ,]
                {
                    {{ 0}, { 4}, { 8}},
                    {{12}, {16}, {20}},
                }));
        }

        [Theory]
        [MemberData(nameof(TestWhereArgs))]
        public void Where(
            NdArrayI source, int axis, Func<NdArrayI, bool> predicate,
            NdArrayI expected)
        {
            Assert.Equal(expected, source.Where(axis, predicate));
        }
    }
}
