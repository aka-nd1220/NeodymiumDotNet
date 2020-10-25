using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NeodymiumDotNet.Test
{
    public class TransposeTest
    {

        private static IEnumerable<object[]> TestCaseFor2Dim()
        {
            object[] core(NdArray<int> from, NdArray<int> to)
                => new[] { from, to };

            yield return core(
                NdArray.Create(new[,] { { 0 } }),
                NdArray.Create(new[,] { { 0 } }));
            yield return core(
                NdArray.Create(new[,] {
                    { 0, 1 },
                    { 2, 3 },
                }),
                NdArray.Create(new[,] {
                    { 0, 2 },
                    { 1, 3 },
                }));
            yield return core(
                NdArray.Create(new[,] {
                    { 0, 1, 2 },
                    { 3, 4, 5 },
                }),
                NdArray.Create(new[,] {
                    { 0, 3 },
                    { 1, 4 },
                    { 2, 5 },
                }));
            yield return core(
                NdArray.Create(new[,] {
                    { 0, 1, 2 },
                    { 3, 4, 5 },
                    { 6, 7, 8 },
                }),
                NdArray.Create(new[,] {
                    { 0, 3, 6 },
                    { 1, 4, 7 },
                    { 2, 5, 8 },
                }));
            yield break;
        }


        private static IEnumerable<object[]> TestCaseForXDim()
        {
            object[] core(int[] axesMap, NdArray<int> from, NdArray<int> to)
                => new object[] { axesMap, from, to };

            yield return core(
                new[] {0},
                NdArray.Create(new[] { 0 }),
                NdArray.Create(new[] { 0 }));
            yield return core(
                new[] {2, 1, 0},
                NdArray.Create(new[, ,] {
                    {
                        { 000, 001, 002, 003 },
                        { 010, 011, 012, 013 },
                        { 020, 021, 022, 023 },
                    },
                    {
                        { 100, 101, 102, 103 },
                        { 110, 111, 112, 113 },
                        { 120, 121, 122, 123 },
                    },
                }),
                NdArray.Create(new[, ,] {
                    {
                        { 000, 100 },
                        { 010, 110 },
                        { 020, 120 },
                    },
                    {
                        { 001, 101 },
                        { 011, 111 },
                        { 021, 121 },
                    },
                    {
                        { 002, 102 },
                        { 012, 112 },
                        { 022, 122 },
                    },
                    {
                        { 003, 103 },
                        { 013, 113 },
                        { 023, 123 },
                    },
                }));
        }


        [Theory]
        [MemberData(nameof(TestCaseFor2Dim))]
        public void TransposeFor2Dim(NdArray<int> from, NdArray<int> to)
        {
            Assert.Equal(to, from.Transpose(), NdArrayComparer<int>.Default);
            Assert.Equal(to.ToMutable(), from.ToMutable().Transpose(), NdArrayComparer<int>.Default);
        }


        [Theory]
        [MemberData(nameof(TestCaseForXDim))]
        public void TransposeForXDim(int[] axesMap, NdArray<int> from, NdArray<int> to)
        {
            Assert.Equal(to, from.Transpose(axesMap), NdArrayComparer<int>.Default);
            Assert.Equal(to.ToMutable(), from.ToMutable().Transpose(axesMap), NdArrayComparer<int>.Default);
        }

    }
}
