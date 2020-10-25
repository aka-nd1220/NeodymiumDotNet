using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace NeodymiumDotNet.Io.Numpy.Test
{
    public class NpyHeaderTest
    {

        public static Stream LoadTestResource(string name)
            => Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("NeodymiumDotNet.Io.Numpy.Test.TestFiles." +
                                                  name);


        public static IEnumerable<object[]> TestData { get; } = new object[][]
        {
            new object[]
                { "test01.npy", (byte)1, (byte)0, "<f8", false, new IndexArray(2, 10) },
            new object[] { "test02.npy", (byte)1, (byte)0, "<i4", false, new IndexArray(16) },
        };


        [Theory]
        [MemberData(nameof(TestData))]
        public void Read(string filename, byte major, byte minor, DType dtype,
                             bool fortranOrder, IndexArray shape)
        {
            var stream = LoadTestResource(filename);
            var header = NpyHeader.LoadHeader(stream);
            Assert.Equal(major, header.MajorVersion);
            Assert.Equal(minor, header.MinorVersion);
            Assert.Equal(dtype, header.NumpyType);
            Assert.Equal(fortranOrder, header.FortranOrder);
            Assert.Equal(shape, header.Shape);
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void Write(string filename, byte major, byte minor, DType dtype,
                              bool fortranOrder, IndexArray shape)
        {
            var stream = LoadTestResource(filename);
            var buffer = new byte[stream.Length];
            stream.Read(buffer);

            var header = new NpyHeader(major, minor, dtype, fortranOrder, shape);
            var headerBuffer = header.GenerateHeader();
            Assert.Equal(0, headerBuffer.Length % 16);
            Assert.True(buffer.Take(headerBuffer.Length).SequenceEqual(headerBuffer));
        }

    }
}
