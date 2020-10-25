using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy
{
    internal static class ParserUtils
    {

        public static Parser<T> Or<T>(params Parser<T>[] parsers)
            => parsers.Aggregate((x, y) => x.Or(y));


        public static Parser<T> GetOrDefault<T>(this Parser<IOption<T>> parser)
            => parser.Select(x => x.GetOrDefault());


        public static Parser<T> GetOrElse<T>(this Parser<IOption<T>> parser, T defaultValue)
            => parser.Select(x => x.GetOrElse(defaultValue));


        public static Parser<string> Single(this Parser<char> parser)
            => parser.Once().Text();


        public static Parser<string> SingleOrNone(this Parser<char> parser)
            => parser.Single().Optional().GetOrElse("");

    }
}
