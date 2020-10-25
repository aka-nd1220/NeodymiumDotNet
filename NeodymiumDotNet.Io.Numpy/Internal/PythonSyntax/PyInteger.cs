using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy.PythonSyntax
{
    /// <summary>
    ///     Python integer literal parser.
    /// </summary>
    internal static class PyInteger
    {

        private static Parser<char> GetSeparatorNext(Parser<char> parser)
            => from character in Parse.Char('_')
               from sequel in parser
               select sequel;


        private static readonly Parser<char> _BinDigit = Parse.Chars('0', '1');


        private static readonly Parser<long> _BinInteger
            = from prefix1 in Parse.Char('0')
              from prefix2 in Parse.Chars('b', 'B')
              from integer in _BinDigit.Or(GetSeparatorNext(_BinDigit)).AtLeastOnce().Text()
              select Convert.ToInt64(integer.Replace("_", ""), 2);


        private static readonly Parser<char> _OctDigit
            = Parse.Chars("01234567".ToArray());


        private static readonly Parser<long> _OctInteger
            = from prefix1 in Parse.Char('0')
              from prefix2 in Parse.Chars('o', 'O')
              from integer in _OctDigit.Or(GetSeparatorNext(_OctDigit)).AtLeastOnce().Text()
              select Convert.ToInt64(integer.Replace("_", ""), 8);


        private static readonly Parser<char> _HexDigit
            = Parse.Chars("0123456789abcdefABCDEF".ToArray());


        private static readonly Parser<long> _HexInteger
            = from prefix1 in Parse.Char('0')
              from prefix2 in Parse.Chars('x', 'X')
              from integer in _HexDigit.Or(GetSeparatorNext(_HexDigit)).AtLeastOnce().Text()
              select Convert.ToInt64(integer.Replace("_", ""), 16);


        private static readonly Parser<char> _NonZeroDigit
            = Parse.Chars("123456789".ToArray());


        private static readonly Parser<long> _DecInteger
            = (from first in _NonZeroDigit
               from sequel in Parse.Digit.Or(GetSeparatorNext(Parse.Digit)).Many().Text()
               select Convert.ToInt64((first + sequel).Replace("_", ""), 10))
           .Or
                (from first in Parse.Char('0')
                 from sequel in Parse.Char('0').Or(GetSeparatorNext(Parse.Char('0'))).Many()
                 select 0L);


        public static readonly Parser<PyObject<long>> IntegerLiteral
            = from value in ParserUtils.Or(_BinInteger, _OctInteger, _HexInteger, _DecInteger)
                                       .MakePositioned()
              select new PyObject<long>(value.Value, value.StartInInput, value.LengthInInput);

    }
}
