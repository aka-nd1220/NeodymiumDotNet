using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy.PythonSyntax
{
    /// <summary>
    ///     Python boolean literal parser.
    /// </summary>
    internal static class PyBoolean
    {

        private static readonly Parser<bool> _True
            = Parse.String("True").Select(_ => true);


        private static readonly Parser<bool> _False
            = Parse.String("False").Select(_ => false);


        public static Parser<PyObject<bool>> Boolean
            => _True.Or(_False)
                    .MakePositioned()
                    .Select(x => new PyObject<bool>(x.Value, x.StartInInput, x.LengthInInput));

    }
}
