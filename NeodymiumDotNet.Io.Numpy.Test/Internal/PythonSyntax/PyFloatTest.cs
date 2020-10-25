using System;
using System.Collections.Generic;
using Xunit;
using Sprache;
using NeodymiumDotNet.Io.Numpy.PythonSyntax;

namespace NeodymiumDotNet.Io.Numpy.Test.PythonSyntax
{
    public class PyFloatTest
    {

        [Theory]
        [InlineData("3.14",       true, 3.14    )]
        [InlineData("10.",        true, 10.0    )]
        [InlineData(".001",       true, 0.001   )]
        [InlineData("1e100",      true, 1e+100  )]
        [InlineData("3.14e-10",   true, 3.14e-10)]
        [InlineData("0e0",        true, 0e0     )]
        [InlineData("3.14_15_93", true, 3.141593)]
        public void TryParse(string expression, bool succeeded, double result)
        {
            var actual = PyFloat.FloatLiteral.MatchPerfectly().TryParse(expression);
            Assert.Equal(succeeded, actual.WasSuccessful);
            if(!succeeded)
            {
                return;
            }

            Assert.Equal(result, actual.Value.Value);
        }

    }
}
