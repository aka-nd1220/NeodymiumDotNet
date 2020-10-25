using System;
using System.Collections.Generic;
using System.Numerics;
using Xunit;

namespace NeodymiumDotNet.Test
{
    public class ExpTest
    {
        public static IEnumerable<object[]> FloatTestArgs()
        {
            object[] core(float x, float ans)
                => new object[] { x, ans };

            yield return core(0, 1);
            yield return core(1, NdMath.E<float>());
            yield return core(0.5f, 1.6487212707f);
            yield return core(1.5f, 4.48168907034f);
            yield return core(-1.5f, 0.22313016014f);
        }


        [Theory]
        [MemberData(nameof(FloatTestArgs))]
        public void FloatTest(float x, float ans)
            => Assert.Equal(ans, NdMath.Exp(x), 6);


        [Fact]
        public void DecimalTest()
        {
            Assert.Equal(1m                               , NdMath.Exp(0m)   , 27);
            Assert.Equal(NdMath.E<decimal>()              , NdMath.Exp(1m)   , 27);
            Assert.Equal(1.64872127070012814684865078781m , NdMath.Exp(0.5m ), 27);
            Assert.Equal(4.48168907033806482260205546012m , NdMath.Exp(1.5m ), 27);
            Assert.Equal(0.223130160148429828933280470764m, NdMath.Exp(-1.5m), 27);
        }
    }
}
