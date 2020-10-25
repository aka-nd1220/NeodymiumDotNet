using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NeodymiumDotNet.Linq;
using NeodymiumDotNet.Optimizations.Linq;
using NeodymiumDotNet.Random;
using Xunit;

namespace NeodymiumDotNet.Optimizations.Test.Linq
{
    public class ZipTest
    {
        /// <summary>
        ///     Stringized NdArray may be too long for unit test tools, so wrapper is inserted.
        /// </summary>
        public struct ArgContainer : IEquatable<ArgContainer>
        {
            public INdArray Value { get; }
            public ArgContainer(INdArray value) => Value = value;

            public bool Equals([AllowNull] ArgContainer other)
                => Equals(Value, other.Value);
        }

        public static int Foo(int x, int y, int z)
            => x + y + z;

        public static IEnumerable<object[]> TestArgs()
        {
            object[] core<T>(NdArray<T> x, NdArray<T> y, NdArray<T> z, Expression<Func<T, T, T, T>> expr)
                where T : unmanaged
            {
                var expected = (x, y, z).Zip(expr.Compile());
                var actual = (x, y, z).Zip(expr);
                return new object[] {
                    new ArgContainer(expected),
                    new ArgContainer(actual),
                };
            }

            yield return core(
                NdArray.Zeros<int>(new[] { 2, 3, 4 }),
                NdArray.Zeros<int>(new[] { 2, 3, 4 }),
                NdArray.Zeros<int>(new[] { 2, 3, 4 }),
                (x, y, z) => x + y + z
            );
            yield return core(
                NdArray.Ones<int>(new[] { 2, 3, 4 }),
                NdArray.Ones<int>(new[] { 2, 3, 4 }),
                NdArray.Ones<int>(new[] { 2, 3, 4 }),
                (x, y, z) => x + y + z
            );
            yield return core(
                NdArray.Ones<int>(new[] { 2, 3, 4 }),
                NdArray.Ones<int>(new[] { 2, 3, 4 }),
                NdArray.Ones<int>(new[] { 2, 3, 4 }),
                (x, y, z) => x + y + z + 1
            );
            yield return core(
                NdArray.Ones<int>(new[] { 2, 3, 4 }),
                NdArray.Ones<int>(new[] { 2, 3, 4 }),
                NdArray.Ones<int>(new[] { 2, 3, 4 }),
                (x, y, z) => Math.Max(x, y) + Math.Min(0, z + 1)
            );
            yield return core(
                RandomNdArray.RandInt32(new[] { 10, 20, 30, 40, 50 }),
                RandomNdArray.RandInt32(new[] { 10, 20, 30, 40, 50 }),
                RandomNdArray.RandInt32(new[] { 10, 20, 30, 40, 50 }),
                (x, y, z) => (Math.Max(x, 0) + Math.Min(y, 0) + x * y * z) / Math.Min(Foo(x, y, z), -1)
            );
            yield return core(
                RandomNdArray.RandN64(new[] { 10, 20, 30, 40, 50 }),
                RandomNdArray.RandN64(new[] { 10, 20, 30, 40, 50 }),
                RandomNdArray.RandN64(new[] { 10, 20, 30, 40, 50 }),
                (x, y, z) => Math.Sqrt(Math.Abs(x)) + Math.Sqrt(Math.Min(Math.Pow(y + z, 2), Math.Abs(y + z)))
            );
        }


        [Theory]
        [MemberData(nameof(TestArgs))]
        public void Zip(ArgContainer expected, ArgContainer actual)
        {
            Assert.Equal(expected, actual);
        }
    }
}
