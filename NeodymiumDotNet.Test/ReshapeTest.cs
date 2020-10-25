using System;
using System.Collections.Generic;
using Xunit;

namespace NeodymiumDotNet.Test
{
    public class ReshapeTest
    {

        private static NdArray<int> CreateTestSample()
            => NdArray.Create(new[,,]
            {
                { {  0,  1, }, {  2,  3, }, {  4,  5, }, },
                { {  6,  7, }, {  8,  9, }, { 10, 11, }, },
                { { 12, 13, }, { 14, 15, }, { 16, 17, }, },
                { { 18, 19, }, { 20, 21, }, { 22, 23, }, },
            });


        [Fact]
        public void Immutable()
        {
            var ndarray = CreateTestSample();
            var reshaped = ndarray.Reshape(6, 4);
            Assert.Equal(new IndexArray(6, 4), reshaped.Shape);
        }

    }
}
