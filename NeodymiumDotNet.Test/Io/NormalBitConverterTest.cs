using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Io;
using Xunit;

namespace NeodymiumDotNet.Test.Io
{
    public class NormalBitConverterTest
    {
        public static IEnumerable<object?[]> GetReadPrimitiveArgs()
        {
            object?[] core(byte[] input, bool succeeded, uint value)
                => new object?[]{input, succeeded, value};

            yield return core(new byte[0], false, default);
            yield return core(new byte[4], true, 0u);
            yield return core(new byte[] { 1, 0, 0, 0 }, true, 1);
            yield return core(new byte[] { 0x01, 0x23, 0x45, 0x67 }, true, 0x67452301u);
            yield return core(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89 }, true, 0x67452301u);
            yield return core(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef }, true, 0x67452301u);
            yield return core(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01 }, true, 0x67452301u);
        }


        [Theory]
        [MemberData(nameof(GetReadPrimitiveArgs))]
        public void ReadPrimitive(byte[] input, bool expectedSucceeded, uint expectedValue)
        {
            var actualSucceeded = NormalBitConverter.Instance.TryReadPrimitive<uint>(input, out var actualValue);
            Assert.Equal(expectedSucceeded, actualSucceeded);
            if(actualSucceeded)
                Assert.Equal(expectedValue, actualValue);
        }


        public static IEnumerable<object?[]> GetReadPrimitivesArgs()
        {
            object?[] core(byte[] input, int dstSize, bool succeeded, uint[] values)
                => new object[] { input, dstSize, succeeded, values };
            
            yield return core(new byte[0], 1, false, Array.Empty<uint>());
            yield return core(new byte[4], 1, true, new[] { 0u });
            yield return core(new byte[4], 2, false, Array.Empty<uint>());
            yield return core(new byte[] { 0x01, 0x23, 0x45, 0x67 }, 1, true, new []{ 0x67452301u });
            yield return core(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89 }, 1, true, new []{ 0x67452301u });
            yield return core(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef }, 2, true, new[] { 0x67452301u, 0xefcdab89u });
            yield return core(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01 }, 2, true, new[] { 0x67452301u, 0xefcdab89u });
            yield return core(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01 }, 3, false, Array.Empty<uint>());
        }


        [Theory]
        [MemberData(nameof(GetReadPrimitivesArgs))]
        public void ReadPrimitives(byte[] input, int dstSize, bool expectedSucceeded, uint[] expectedValues)
        {
            var actualValues = new uint[dstSize];
            var actualSucceeded = NormalBitConverter.Instance.TryReadPrimitives<uint>(input, actualValues);
            Assert.Equal(expectedSucceeded, actualSucceeded);
            if(actualSucceeded)
                Assert.Equal(expectedValues, actualValues);
        }


        public static IEnumerable<object[]> GetWritePrimitiveArgs()
        {
            object[] core(uint input, int dstSize, bool expectSucceeded, byte[] expectedValues)
                => new object[]{input, dstSize, expectSucceeded, expectedValues};

            yield return core(0u, 1, false, Array.Empty<byte>());
            yield return core(0u, 4, true, new byte[] { 0, 0, 0, 0 });
            yield return core(0x01234567u, 4, true, new byte[] { 0x67, 0x45, 0x23, 0x01 });
            yield return core(0x01234567u, 5, true, new byte[] { 0x67, 0x45, 0x23, 0x01, 0x00 });
        }


        [Theory]
        [MemberData(nameof(GetWritePrimitiveArgs))]
        public void WritePrimitive(uint input, int dstSize, bool expectedSucceeded, byte[] expectedValues)
        {
            var actualValues = new byte[dstSize];
            var actualSucceeded = NormalBitConverter.Instance.TryWritePrimitive(actualValues, input);
            Assert.Equal(expectedSucceeded, actualSucceeded);
            if(actualSucceeded)
                Assert.Equal(expectedValues, actualValues);
        }


        public static IEnumerable<object[]> GetWritePrimitivesArgs()
        {
            object[] core(uint[] input, int dstSize, bool expectedSucceeded, byte[] expectedValues)
                => new object[]{input, dstSize, expectedSucceeded, expectedValues};

            yield return core(new[] { 0u }, 1, false, Array.Empty<byte>());
            yield return core(new[] { 0u }, 4, true, new byte[] { 0, 0, 0, 0 });
            yield return core(new[] { 0x01234567u }, 4, true, new byte[] { 0x67, 0x45, 0x23, 0x01 });
            yield return core(new[] { 0x01234567u }, 5, true, new byte[] { 0x67, 0x45, 0x23, 0x01, 0x00 });
            yield return core(new[] { 0x01234567u, 0x89abcdefu }, 7, false, Array.Empty<byte>());
            yield return core(new[] { 0x01234567u, 0x89abcdefu }, 8, true, new byte[] { 0x67, 0x45, 0x23, 0x01, 0xef, 0xcd, 0xab, 0x89 });
            yield return core(new[] { 0x01234567u, 0x89abcdefu }, 9, true, new byte[] { 0x67, 0x45, 0x23, 0x01, 0xef, 0xcd, 0xab, 0x89, 0x00 });
        }


        [Theory]
        [MemberData(nameof(GetWritePrimitivesArgs))]
        public void WritePrimitives(uint[] input, int dstSize, bool expectedSucceeded, byte[] expectedValues)
        {
            var actualValues = new byte[dstSize];
            var actualSucceeded = NormalBitConverter.Instance.TryWritePrimitives<uint>(actualValues, input);
            Assert.Equal(expectedSucceeded, actualSucceeded);
            if(actualSucceeded)
                Assert.Equal(expectedValues, actualValues);
        }
    }
}
