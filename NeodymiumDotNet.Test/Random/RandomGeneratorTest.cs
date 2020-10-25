using System;
using System.Collections.Generic;
using System.Linq;
using NeodymiumDotNet.Random;
using Xunit;
using Xunit.Sdk;

namespace NeodymiumDotNet.Test.Random
{
    public class RandomGeneratorTest
    {

        public static IEnumerable<object[]> TestArgs()
        {
            object[] core(RandomGenerator gen)
                => new object[]{ gen };

            yield return core(new LinearCongruentialGenerator());
            yield return core(new MersenneTwisterGenerator(0));
            yield return core(new XorShift32Generator(0));
            yield return core(new XorShift64Generator(0));
            yield return core(new XorShift96Generator(0, 0, 0));
            yield return core(new XorShift128Generator(0, 0, 0, 0));
        }


        [Theory]
        [MemberData(nameof(TestArgs))]
        public void NextDouble(RandomGenerator gen)
        {
            foreach(var x in gen.NextFloat64(1 << 20))
                Assert.True(0 <= x && x < 1);
        }
    }
}
