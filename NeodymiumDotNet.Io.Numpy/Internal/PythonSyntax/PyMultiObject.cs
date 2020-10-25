using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy.PythonSyntax
{
    internal static class PyMultiObject
    {

        public static Parser<IEnumerable<T>> GetCommaChainedItems<T>(
            Parser<T> parser, bool needsCommaForSingle, bool allowsEmpty)
        {
            var retval = GetMultiple(parser).Or(GetSingle(parser, needsCommaForSingle));
            return allowsEmpty ? retval.Or(GetEmpty<T>()) : retval;
        }


        private static Parser<IEnumerable<T>> GetEmpty<T>()
            => Parse.WhiteSpace.Many().Select(x => Enumerable.Empty<T>());


        private static Parser<IEnumerable<T>> GetSingle<T>(
            Parser<T> parser, bool needsToCommaForSingle)
        {
            if(needsToCommaForSingle)
            {
                return from space1 in Parse.WhiteSpace.Many()
                       from content in parser
                       from space2 in Parse.WhiteSpace.Many()
                       from comma in Parse.Char(',')
                       from space3 in Parse.WhiteSpace.Many()
                       select Enumerable.Repeat(content, 1);
            }

            return from space1 in Parse.WhiteSpace.Many()
                   from content in parser
                   from space2 in Parse.WhiteSpace.Many()
                   from comma in Parse.Char(',').Optional()
                   from space3 in Parse.WhiteSpace.Many().Optional()
                   select Enumerable.Repeat(content, 1);
        }


        private static Parser<IEnumerable<T>> GetMultiple<T>(
            Parser<T> parser)
        {
            Parser<T> sequelsParser
                = from space1 in Parse.WhiteSpace.Many()
                  from comma in Parse.Char(',')
                  from space2 in Parse.WhiteSpace.Many()
                  from obj in parser
                  select obj;
            return from space1 in Parse.WhiteSpace.Many()
                   from first in parser
                   from sequels in sequelsParser.AtLeastOnce()
                   from comma in Parse.Char(',').Optional()
                   from space2 in Parse.WhiteSpace.Many()
                   select Enumerable.Repeat(first, 1).Concat(sequels);
        }

    }
}
