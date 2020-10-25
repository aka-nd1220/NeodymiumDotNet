using System;
using System.Collections.Generic;
using Xunit;

namespace NeodymiumDotNet.Test
{
    public class RangeTest
    {

        [Theory]
        [InlineData(4, 2, 2)]
        [InlineData(5, 2, 3)]
        [InlineData(5, 1, 5)]
        public static void Ceiling(int x, int y, int ans)
        {
            Assert.Equal(ans, Range.Ceiling(x, y));
        }

    }
}
