using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy.PythonSyntax
{
    /// <summary>
    ///     Python float literal parser.
    /// </summary>
    internal static class PyFloat
    {

        private static Parser<char> GetSeparatorNext(Parser<char> parser)
            => from character in Parse.Char('_')
               from sequel in parser
               select sequel;


        public static readonly Parser<string> DigitPart
            = from first in Parse.Digit
              from sequel in Parse.Digit
                                  .Or(GetSeparatorNext(Parse.Digit))
                                  .Many().Text()
              select first + sequel;


        private static readonly Parser<string> _Fraction
            = from point in Parse.Char('.')
              from sequel in DigitPart
              select point + sequel;


        private static readonly Parser<string> _PointFloat
            = (from intPart in DigitPart.Optional().Select(x => x.GetOrElse(""))
               from fraction in _Fraction
               select intPart + fraction)
           .Or
                (from intPart in DigitPart
                 from point in Parse.Char('.')
                 select intPart + point);


        private static readonly Parser<string> _Exponent
            = from e in Parse.Chars('e', 'E')
              from sig in Parse.Chars('+', '-').SingleOrNone()
              from exp in DigitPart
              select e + sig + exp;


        private static readonly Parser<string> _ExponentFloat
            = from real in _PointFloat.Or(DigitPart)
              from exp in _Exponent
              select real + exp;


        public static readonly Parser<PyObject<double>> FloatLiteral
            = _ExponentFloat.Or(_PointFloat)
                            .MakePositioned()
                            .Select(ToPyObject);


        private static PyObject<double> ToPyObject(SourcePositionedObject<string> expr)
            => new PyObject<double>(double.Parse(expr.Value.Replace("_", "")),
                                    expr.StartInInput,
                                    expr.LengthInInput);

    }
}
