using System;
using System.Collections.Generic;
using System.Numerics;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy.PythonSyntax
{
    internal static class PyImaginary
    {

        public static readonly Parser<PyObject<Complex>> ImaginaryLiteral
            = from number in PyFloat.FloatLiteral.Select(x => x.Value)
                                    .Or(PyFloat.DigitPart.Select(double.Parse))
                                    .MakePositioned()
              from j in Parse.Chars('j', 'J')
                             .MakePositioned()
              select new PyObject<Complex>(new Complex(0, number.Value),
                                           number.StartInInput,
                                           j.EndInInput - number.StartInInput);

    }
}
