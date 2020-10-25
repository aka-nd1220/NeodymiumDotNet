using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using NeodymiumDotNet.Io.Numpy.PythonSyntax;
using Sprache;
using Xunit;

namespace NeodymiumDotNet.Io.Numpy.Test.PythonSyntax
{
    public class PyTupleTest
    {

        public static IEnumerable<object[]> CreateTestData()
        {
            yield return new object[] { "(0,)", new object[] { 0L, } };
            yield return new object[] { "(0, )", new object[] { 0L, } };
            yield return new object[] { "( 0,)", new object[] { 0L, } };
            yield return new object[] { "( 0 , )", new object[] { 0L, } };
            yield return new object[] { "(0,'1')", new object[] { 0L, "1" } };
            yield return new object[] { "(0, '1')", new object[] { 0L, "1" } };
            yield return new object[] { "(0,\t'1')", new object[] { 0L, "1" } };
            yield return new object[] { "(0,\r\n'1')", new object[] { 0L, "1" } };
            yield return new object[] { "(0,'1',)", new object[] { 0L, "1" } };
            yield return new object[] { "(0, '1', )", new object[] { 0L, "1" } };
            yield return new object[]
            {
                "(0, 1, 2, 3., 4, 5j )", new object[] { 0L, 1L, 2L, 3.0, 4L, new Complex(0, 5) }
            };
        }


        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void Test(string expression, IEnumerable<object> expected)
        {
            var result = PyTuple.Tuple.TryParse(expression);
            var actual = result.Value.Value.Select(x => x.Value);
            Assert.True(expected.SequenceEqual(actual, PyObjectComparer.Instance));
        }

    }
}
