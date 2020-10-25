using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;
using NeodymiumDotNet.Linq;

namespace NeodymiumDotNet.Test.Linq
{
    public class ZipTest
    {

        [Fact]
        public void Zip()
        {
            var A = NdArray.Create(new double[,,]
            {
                { { 0,  1,  2,  3 }, { 4,  5,  6,  7 } },
                { { 8,  9, 10, 11 }, { 12, 13, 14, 15 } }
            });
            var B = NdArray.Create(new double[,,]
            {
                { { 16, 17, 18, 19 }, { 20, 21, 22, 23 } },
                { { 24, 25, 26, 27 }, { 28, 29, 30, 31 } }
            });
            var addAns = NdArray.Create(new double[,,]
            {
                {
                    { 0 + 16,  1 + 17,  2 + 18,  3 + 19 }, { 4 + 20,  5 + 21,  6 + 22,  7 + 23 }
                },
                {
                    { 8 + 24,  9 + 25, 10 + 26, 11 + 27 },
                    { 12 + 28, 13 + 29, 14 + 30, 15 + 31 }
                }
            });
            var subAns = NdArray.Create(new double[,,]
            {
                {
                    { 0 - 16,  1 - 17,  2 - 18,  3 - 19 }, { 4 - 20,  5 - 21,  6 - 22,  7 - 23 }
                },
                {
                    { 8 - 24,  9 - 25, 10 - 26, 11 - 27 },
                    { 12 - 28, 13 - 29, 14 - 30, 15 - 31 }
                }
            });
            var mulAns = NdArray.Create(new double[,,]
            {
                {
                    { 0 * 16,  1 * 17,  2 * 18,  3 * 19 }, { 4 * 20,  5 * 21,  6 * 22,  7 * 23 }
                },
                {
                    { 8 * 24,  9 * 25, 10 * 26, 11 * 27 },
                    { 12 * 28, 13 * 29, 14 * 30, 15 * 31 }
                }
            });
            var divAns = NdArray.Create(new double[,,]
            {
                {
                    { 0.0 / 16,  1.0 / 17,  2.0 / 18,  3.0 / 19 },
                    { 4.0 / 20,  5.0 / 21,  6.0 / 22,  7.0 / 23 }
                },
                {
                    { 8.0 / 24,  9.0 / 25, 10.0 / 26, 11.0 / 27 },
                    { 12.0 / 28, 13.0 / 29, 14.0 / 30, 15.0 / 31 }
                }
            });
            Assert.Equal(addAns, A.Zip(B, (a, b) => a + b));
            Assert.Equal(subAns, A.Zip(B, (a, b) => a - b));
            Assert.Equal(mulAns, A.Zip(B, (a, b) => a * b));
            Assert.Equal(divAns, A.Zip(B, (a, b) => a / b));
            Assert.Equal(addAns, (A, B).Zip((a, b) => a + b));
            Assert.Equal(subAns, (A, B).Zip((a, b) => a - b));
            Assert.Equal(mulAns, (A, B).Zip((a, b) => a * b));
            Assert.Equal(divAns, (A, B).Zip((a, b) => a / b));
        }

    }
}
