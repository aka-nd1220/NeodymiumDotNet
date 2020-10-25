using System;
using System.Collections.Generic;
using Sprache;
using Xunit;
using NeodymiumDotNet.Io.Numpy.PythonSyntax;

namespace NeodymiumDotNet.Io.Numpy.Test.PythonSyntax
{
    public class PyIntegerTest
    {

        [Theory]
        [InlineData("0",            true,    0)]
        [InlineData("1",            true,    1)]
        [InlineData("0000",         true,    0)]
        [InlineData("0_0_0_0",      true,    0)]
        [InlineData("11_11",        true, 1111)]
        [InlineData("0b11111111",   true,  255)]
        [InlineData("0o377",        true,  255)]
        [InlineData("0xff",         true,  255)]
        [InlineData("0B11111111",   true,  255)]
        [InlineData("0O377",        true,  255)]
        [InlineData("0XFF",         true,  255)]
        [InlineData("0b_1111_1111", true,  255)]
        [InlineData("0o_3_7_7",     true,  255)]
        [InlineData("0x_f_f",       true,  255)]
        [InlineData("0b11111111_",  false, 255)]
        [InlineData("0o777_",       false, 255)]
        [InlineData("0xff_",        false, 255)]
        public void TryParse(string expression, bool succeeded, int result)
        {
            var actual = PyInteger.IntegerLiteral.MatchPerfectly().TryParse(expression);
            Assert.Equal(succeeded, actual.WasSuccessful);
            if(!succeeded)
            {
                return;
            }

            Assert.Equal(result, actual.Value.Value);
        }

    }
}
