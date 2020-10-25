using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeodymiumDotNet.Random;
using Xunit;

namespace NeodymiumDotNet.Test.Random
{
    public class RandomGeneratorValidation
    {
        public static IEnumerable<object[]> TestArgs()
        {
            object[] core(RandomGenerator gen)
                => new object[] { gen };

            yield return core(new LinearCongruentialGenerator());
            yield return core(new MersenneTwisterGenerator(123456789));
            yield return core(new XorShift32Generator(123456789));
            yield return core(new XorShift64Generator(123456789));
            yield return core(new XorShift96Generator(123456789, 456789123, 789123456));
            yield return core(new XorShift128Generator(123456789, 1122334455, 111222333, 111111111));
        }


        public static double Erf(double x)
        {
            const double a = - 8 * (Math.PI - 3) / (3 * Math.PI * (Math.PI - 4));
            var x2 = x * x;
            return 1 - Math.Exp(-x2 * (4 / Math.PI + a * x2) / (1 + a * x2));
        }


        public static double Erfc(double x)
            => 1 - Erf(x);


        public static IEnumerable<int> GetBitFields(RandomGenerator gen, long length)
        {
            var j = 0uL;
            var x = 0uL;
            for(var i = 0L; i < length; ++i, j <<= 1)
            {
                if(j == 0)
                {
                    j = 1uL;
                    x = (ulong)gen.NextInt64();
                }
                yield return (x & j) != 0 ? 1 : 0;
            }
        }


        [SkippableTheory]
        [MemberData(nameof(TestArgs))]
        public void NistSp800_22_FrequencyTest(RandomGenerator gen)
        {
            const long n = 1 << 24;
            var xi = GetBitFields(gen, n).Select(x => 2 * x - 1);
            var sn = xi.Sum();
            var sobs = Math.Abs(sn) / Math.Sqrt(n);
            var pvalue = Erfc(sobs / Math.Sqrt(2));
            Skip.IfNot(pvalue >= 0.01, $"{gen.GetType().Name} has generated doubtful random value sequence. (p-value={pvalue})");
        }


        [SkippableTheory]
        [MemberData(nameof(TestArgs))]
        public void NistSp800_22_RunsTest(RandomGenerator gen)
        {
            const long n = 1 << 24;
            var x = GetBitFields(gen, n).Select(xi => (byte)xi).ToArray();
            var freq = x.Select(xi => (long)xi).Sum() / (double)n;
            var bias = freq * (1 - freq);
            var vn = x.Skip(1).Zip(x, (a, b) => a == b ? 0 : 1).Sum() + 1;
            var pvalue = Erfc(Math.Abs(vn - 2.0 * n * bias) / (2 * Math.Sqrt(2.0 * n) * bias));
            Skip.IfNot(pvalue >= 0.01, $"{gen.GetType().Name} has generated doubtful random value sequence. (p-value={pvalue})");
        }

    }
}
