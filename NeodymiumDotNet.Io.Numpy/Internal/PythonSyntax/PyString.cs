using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Sprache;
using static NeodymiumDotNet.Io.Numpy.UnicodeUtils;

namespace NeodymiumDotNet.Io.Numpy.PythonSyntax
{
    /// <summary>
    ///     Python string literal parser.
    /// </summary>
    internal static class PyString
    {

        private class EscapeSeqInfo
        {

            public string RegexPattern { get; }

            public Regex Regex { get; }

            public Func<Match, string> Evaluator { get; }


            public EscapeSeqInfo(string pattern,
                                 Func<Match, string> evaluator)
            {
                RegexPattern = pattern;
                Regex = new Regex(pattern);
                Evaluator = evaluator;
            }

        }


        private static string GetHex(int count)
            => string.Concat(Enumerable.Repeat("[0-9a-fA-F]", count));


        /// <summary>
        ///     Escape sequence regex pattern and replacing func pairs.
        /// </summary>
        private static readonly List<EscapeSeqInfo>
            _EscapeSeqReplacers = new List<EscapeSeqInfo>
            {
                new EscapeSeqInfo(  @"\\\n", x => ""),
                new EscapeSeqInfo(  @"\\\\", x => @"\"),
                new EscapeSeqInfo(  @"\\'",  x => "'"),
                new EscapeSeqInfo(  @"\\""", x => "\""),
                new EscapeSeqInfo(  @"\\a",  x => "\a"),
                new EscapeSeqInfo(  @"\\b",  x => "\b"),
                new EscapeSeqInfo(  @"\\f",  x => "\f"),
                new EscapeSeqInfo(  @"\\n",  x => "\n"),
                new EscapeSeqInfo(  @"\\r",  x => "\r"),
                new EscapeSeqInfo(  @"\\t",  x => "\t"),
                new EscapeSeqInfo(  @"\\v",  x => "\v"),
                new EscapeSeqInfo(  @"\\([0-7][0-7][0-7])", x => FromOct(x.Groups[1].Value)),
                new EscapeSeqInfo($@"\\x({GetHex(2)})",    x => FromHex(x.Groups[1].Value)),
                new EscapeSeqInfo(  @"\\N{([\w\- ]+)}",     x => FromAlias(x.Groups[1].Value)),
                new EscapeSeqInfo($@"\\u({GetHex(4)})",    x => FromHex(x.Groups[1].Value)),
                new EscapeSeqInfo($@"\\U({GetHex(8)})",    x => FromHex(x.Groups[1].Value)),
                new EscapeSeqInfo(  @"\\(.)",               x => x.Groups[1].Value),
            };


        private static readonly Regex _EscapeSeqReg
            = new Regex(string.Join("|",
                                    _EscapeSeqReplacers.Select(x => $"(?:{x.RegexPattern})")));


        private static readonly Parser<string> _StringEscapeSeq
            = from predecessor in Parse.Char('\\')
              from character in Parse.AnyChar
              select "" + predecessor + character;


        private static readonly Parser<char> _LongStringSingleQuote
            = from quote in Parse.Char('\'')
              from sequel in Parse.AnyChar.Many().Text()
                                  .Where(x => x.Length < 2 || x.Substring(0, 2) != "''")
              select quote;


        private static readonly Parser<string> _SingleQuotedLongStringChar
            = Parse.AnyChar
                   .Except(Parse.Chars('\\', '\''))
                   .Or(_LongStringSingleQuote).Once().Text();


        private static readonly Parser<string> _SingleQuotedLongStringItem
            = _StringEscapeSeq.Or(_SingleQuotedLongStringChar);


        private static readonly Parser<string> _SingleQuotedLongString
            = from begin in Parse.String("'''")
              from content in _SingleQuotedLongStringItem.Many()
              from end in Parse.String("'''")
              select string.Concat(content);


        private static readonly Parser<char> _LongStringDoubleQuote
            = from quote in Parse.Char('"')
              from sequel in Parse.AnyChar.Many().Text()
                                  .Where(x => x.Length < 2 || x.Substring(0, 2) != "\"\"")
              select quote;


        private static readonly Parser<string> _DoubleQuotedLongStringChar
            = Parse.AnyChar
                   .Except(Parse.Chars('\\', '"'))
                   .Or(_LongStringDoubleQuote).Once().Text();


        private static readonly Parser<string> _DoubleQuotedLongStringItem
            = _StringEscapeSeq.Or(_DoubleQuotedLongStringChar);


        private static readonly Parser<string> _DoubleQuotedLongString
            = from begin in Parse.String("\"\"\"")
              from content in _DoubleQuotedLongStringItem.Many()
              from end in Parse.String("\"\"\"")
              select string.Concat(content);


        private static readonly Parser<string> _SingleQuotedShortStringChar
            = Parse.AnyChar.Except(Parse.Chars('\\', '\'')).Once().Text();


        private static readonly Parser<string> _SingleQuotedShortStringItem
            = _StringEscapeSeq.Or(_SingleQuotedShortStringChar);


        private static readonly Parser<string> _SingleQuotedShortString
            = from begin in Parse.String("'")
              from content in _SingleQuotedShortStringItem.Many()
              from end in Parse.String("'")
              select string.Concat(content);


        private static readonly Parser<string> _DoubleQuotedShortStringChar
            = Parse.AnyChar.Except(Parse.Chars('\\', '"')).Once().Text();


        private static readonly Parser<string> _DoubleQuotedShortStringItem
            = _StringEscapeSeq.Or(_DoubleQuotedShortStringChar);


        private static readonly Parser<string> _DoubleQuotedShortString
            = from begin in Parse.String("\"")
              from content in _DoubleQuotedShortStringItem.Many()
              from end in Parse.String("\"")
              select string.Concat(content);


        private static readonly Parser<string> _StringPrefix
            = Parse.String("u")
                   .Or(Parse.String("U"))
                   .Or(Parse.String("r"))
                   .Or(Parse.String("R")).Text();


        public static readonly Parser<PyObject<string>> StringLiteral
            = from prefix in _StringPrefix.Optional()
                                          .GetOrElse("")
                                          .MakePositioned()
              from content in _SingleQuotedLongString.Or(_DoubleQuotedLongString)
                                                     .Or(_SingleQuotedShortString)
                                                     .Or(_DoubleQuotedShortString)
                                                     .MakePositioned()
              select GetPythonString(prefix, content);


        private static PyObject<string> GetPythonString(
            SourcePositionedObject<string> prefix,
            SourcePositionedObject<string> content
        )
        {
            var str = GetPythonString(prefix.Value, content.Value);
            return new PyObject<string>(str,
                                        prefix.StartInInput,
                                        content.EndInInput - prefix.StartInInput);
        }


        private static string GetPythonString(string prefix, string content)
        {
            prefix = string.IsNullOrEmpty(prefix) ? "U" : prefix.ToUpper();
            if(prefix == "R")
                return content;

            string evaluator(Match match) => _EscapeSeqReplacers
                                            .FirstOrDefault(x => x.Regex.IsMatch(match.Value))
                                           ?.Evaluator(match);

            return _EscapeSeqReg.Replace(content, evaluator);
        }

    }
}
