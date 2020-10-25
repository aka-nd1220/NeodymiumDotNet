using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Sprache;
using NeodymiumDotNet.Io.Numpy.PythonSyntax;

namespace NeodymiumDotNet.Io.Numpy.Test.PythonSyntax
{
    public class PyStringTest
    {

        [Theory]
        [InlineData("'"      + @"abc"      + "'"     , true, "abc")]
        [InlineData("\""     + @"abc"      + "\""    , true, "abc")]
        [InlineData("'''"    + @"abc"      + "'''"   , true, "abc")]
        [InlineData("\"\"\"" + @"abc"      + "\"\"\"", true, "abc")]
        [InlineData("'"      + @"abc\ndef" + "'"     , true, "abc\ndef")]
        [InlineData("'''"    +  "A quick brown fox \\\njumps over the lazy dog." + "'''", true,
            "A quick brown fox jumps over the lazy dog.")]
        [InlineData("'''"    + @"A quick brown fox \njumps over the lazy dog."   + "'''", true,
            "A quick brown fox \njumps over the lazy dog.")]
        [InlineData("'''abc''", false, default)]
        public void TryParse(string expression, bool succeeded, string resultText)
        {
            var actual = PyString.StringLiteral.MatchPerfectly().TryParse(expression);
            Assert.Equal(succeeded, actual.WasSuccessful);
            if(!succeeded)
            {
                return;
            }

            Assert.Equal(resultText, actual.Value.Value);
        }

    }
}
