using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace NeodymiumDotNet.Io.Numpy.Test
{
    public class NpyFileTest
    {

        public static Stream LoadTestResource(string name)
            => Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("NeodymiumDotNet.Io.Numpy.Test.TestFiles." +
                                                  name);


        public static IEnumerable<object[]> TestData => new object[][]
        {
            new object[]
            {
                typeof(double), "test01.npy", NdArray.Create(new double[,]
                {
                    { 0.0, 0.1, 0.4, 0.9, 1.6, 2.5, 3.6, 4.9, 6.4, 8.1, },
                    { 0  , 1  , 2  , 3  , 4  , 5  , 6  , 7  , 8  , 9  , },
                })
            },
            new object[]
            {
                typeof(int)   , "test02.npy",
                NdArray.Create(new int[]
                {
                    0,  4369,  8738, 13107, 17476, 21845, 26214, 30583, 34952, 39321, 43690,
                    48059, 52428, 56797, 61166, 65535
                })
            },
        };


        [Theory]
        [MemberData(nameof(TestData))]
        public void Load(Type type, string resourceName, object expected)
        {
            var minfo = GetType().GetMethod(nameof(TestLoadCore),
                                            BindingFlags.NonPublic | BindingFlags.Static);
            minfo.MakeGenericMethod(type).Invoke(null, new object[] { resourceName, expected });
        }


        private static void TestLoadCore<T>(string resourceName, NdArray<T> expected)
        {
            var actual = NpyFile.Load<T>(LoadTestResource(resourceName));
            Assert.Equal(expected, actual);
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void Save(Type type, string expectedResourceName, object targetNdArray)
        {
            var minfo = GetType().GetMethod(nameof(TestLoadCore),
                                            BindingFlags.NonPublic | BindingFlags.Static);
            minfo.MakeGenericMethod(type)
                 .Invoke(null, new object[] { expectedResourceName, targetNdArray });
        }


        private static void TestSaveCore<T>(string expectedResourceName,
                                            NdArray<T> targetNdArray)
        {
            byte[] expected, actual;

            {
                var stream = LoadTestResource(expectedResourceName);
                expected = new byte[stream.Length];
                stream.Read(expected, 0, expected.Length);
            }

            using(var stream = new MemoryStream())
            {
                NpyFile.Save(stream, (dynamic)targetNdArray);

                stream.Seek(0, SeekOrigin.Begin);
                actual = new byte[stream.Length];
                stream.Read(actual, 0, expected.Length);
            }

            Assert.Equal(expected, actual);
        }

    }
}
