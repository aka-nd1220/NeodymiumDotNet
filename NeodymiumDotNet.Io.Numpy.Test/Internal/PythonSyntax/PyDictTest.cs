using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using NeodymiumDotNet.Io.Numpy.PythonSyntax;
using Sprache;
using Xunit;

namespace NeodymiumDotNet.Io.Numpy.Test.PythonSyntax
{
    using PyObj = PyObject<object>;

    public class PyDictTest
    {

        public static IEnumerable<object[]> CreateTestData()
        {
            yield return new object[] { "{}", new Dictionary<PyObj, PyObj> { } };
            yield return new object[]
                { "{0: '0'}", new Dictionary<PyObj, PyObj> { { 0, "0" } } };
            yield return new object[]
                { "{0: '0',}", new Dictionary<PyObj, PyObj>  { { 0, "0" } } };
            yield return new object[]
            {
                "{0: '0', 1: '1', 2: '2',}",
                new Dictionary<PyObj, PyObj>  { { 0, "0" }, { 1, "1" }, { 2, "2" } }
            };
        }


        [Theory]
        [MemberData(nameof(CreateTestData))]
        internal void TestTryParse(string expression, IDictionary<PyObj, PyObj> expected)
        {
            var result = PyDict.Dict.TryParse(expression);
            var actual = result.Value.Value;
            foreach(var actualKey in actual.Keys)
            {
                var expectedKey =
                    expected.Keys.Single(key => PyObjectComparer
                                               .Instance.Equals(key, actualKey));
                var actualValue = actual[actualKey];
                var expectedValue = expected[expectedKey];
                Assert.Equal(expectedValue, actualValue, PyObjectComparer.Instance);
            }
        }

    }
}
