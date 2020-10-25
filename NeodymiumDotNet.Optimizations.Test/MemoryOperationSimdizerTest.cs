using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace NeodymiumDotNet.Optimizations.Test
{
    public class MemoryOperationSimdizerTest
    {
        public MemoryOperationSimdizerTest(ITestOutputHelper output)
        {
            Console.SetOut(new TestHelperWriter(output));
        }


        private static readonly int[] _testLength= new[]
                {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                    100,
                    101,
                    1024,
                    1025,
                    65536,
                };

        #region int

        public static IEnumerable<object[]> TestArgsInt2()
        {
            object[] core(int length, Expression<Func<int, int, int>> expr)
                => new object[] { length, expr};

            foreach(var len in _testLength)
                foreach(var expr in new Expression<Func<int, int, int>>[]
                {
                //    (x, y) => 0,
                    (x, y) => x + y,
                    (x, y) => x + y + 1,
                    (x, y) => x * y,
                    (x, y) => x | y,
                })
                {
                    yield return core(len, expr);
                }
        }

        [Theory]
        [MemberData(nameof(TestArgsInt2))]
        public void SimdizeInt2(int length, Expression<Func<int, int, int>> expr)
        {
            var random = new System.Random(1234);
            var x = Enumerable.Range(0, length).Select(_ => random.Next()).ToArray();
            var y = Enumerable.Range(0, length).Select(_ => random.Next()).ToArray();

            var func = expr.Compile();
            var expected = x.Zip(y, func).ToArray();

            // 1st time: compile expression
            // 2nd or later: load from cache
            for(var i = 0; i < 8; ++i)
            {
                var simdFunc = VectorOperation.Simdize(expr);
                var actual = new int[x.Length];
                simdFunc(x, y, actual);

                Assert.Equal(expected.Length, actual.Length);
                for(var j = 0; j < expected.Length; ++j)
                {
                    Assert.Equal(expected[j], actual[j]);
                }
            }
        }

        #endregion

        #region double

        static double GenerateDouble(System.Random random)
        {
            double real()
            {
                var sign = (ulong)random.Next(0, 2) << 63;
                var exponent = (ulong)random.Next(0, 0x7ff) << 52;
                var fraction = ((((ulong)random.Next()) << 32) | (uint)random.Next()) & 0xF_FFFF_FFFF_FFFFuL;
                var value = sign | exponent | fraction;
                var retval = Unsafe.As<ulong, double>(ref value);
                return retval;
            }

            return random.Next(0, 256) switch
            {
                0 => 0,
                1 => 1,
                2 => -1,
                3 => double.PositiveInfinity,
                4 => double.NegativeInfinity,
                5 => double.NaN,
                _ => real(),
            };
        }

        public static IEnumerable<object[]> TestArgsDouble2()
        {
            object[] core(int length, Expression<Func<double, double, double>> expr, bool strict_nan)
                => new object[] { length, expr, strict_nan };

            foreach(var len in _testLength)
                foreach(var (expr, strict_nan) in new (Expression<Func<double, double, double>>, bool)[]
                    {
                        ((x, y) => 0, true),
                        ((x, y) => x + y, true),
                        ((x, y) => x + y + 1, true),
                        ((x, y) => Math.Sqrt(x + y) + Math.Sqrt(y + y), true),
                        ((x, y) => Math.Max(x, y) + Math.Min(y, y), false), // max(x, y) and min(x, y) may behaviour strange because of QNaN/SNaN.
                    })
                {
                    yield return core(len, expr, strict_nan);
                }
        }

        /// <summary></summary>
        /// <param name="length"></param>
        /// <param name="expr"></param>
        /// <param name="strict_nan">
        ///     Skips value equality validation for the case that at least one input is NaN if <c>false</c>.
        ///     Some primitive operations for floating value are different behaviour against NaN between SISD and SIMD.
        /// </param>
        [Theory]
        [MemberData(nameof(TestArgsDouble2))]
        public void SimdizeDouble2(int length, Expression<Func<double, double, double>> expr, bool strict_nan)
        {
            var random = new System.Random(1234);
            var x = Enumerable.Range(0, length).Select(_ => GenerateDouble(random)).ToArray();
            var y = Enumerable.Range(0, length).Select(_ => GenerateDouble(random)).ToArray();

            var func = expr.Compile();
            var expected = x.Zip(y, func).ToArray();

            // 1st time: compile expression
            // 2nd or later: load from cache
            for(var i = 0; i < 8; ++i)
            {
                var simdFunc = VectorOperation.Simdize(expr);
                var actual = new double[length];
                simdFunc(x, y, actual);

                Assert.Equal(expected.Length, actual.Length);
                for(var j = 0; j < expected.Length; ++j)
                {
                    if(!strict_nan && (double.IsNaN(x[j]) || double.IsNaN(y[j])))
                    {
                        continue;
                    }
                    Assert.Equal(expected[j], actual[j]);
                }
            }
        }

        #endregion
    }
}
