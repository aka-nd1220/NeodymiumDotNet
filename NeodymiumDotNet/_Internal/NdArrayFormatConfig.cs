using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NeodymiumDotNet
{
    internal class NdArrayFormatConfig
    {

        /*
         *  # Format style
         *
         *  Format strings for NdArray should be constructed with following options.
         *  These options must be joined with empty-or-whitespace separator.
         *
         *  - T
         *    : Show element type
         *
         *  - S
         *    : Shows shape summary
         *    
         *  - W(*width of the line*)
         *    : Sets the width of the line.
         *
         *  - L(*integer array*)
         *    : Sets row count limit for each axes.
         *      `*` means infinity.
         *
         *  - E(*format for element*)
         *    : Sets format string for element formatting.
         *      `\(` and `\)` are escape sequence which are mapped to `(` and `)`.
         */


        private static readonly Regex _Format =
            new Regex(@"\s*(\w)(?:\(((?:\\\(|\\\)|[^()])*)\))?\s*");


        public FormatOption Options { get; }

        public int LineWidth { get; } = 80;

        public           ReadOnlySpan<int> AxesLimits => _AxesLimits;
        private readonly int[]             _AxesLimits = Array.Empty<int>();

        public string ElementFormat { get; } = "";


        private NdArrayFormatConfig(int rank)
        {
            _AxesLimits = Enumerable.Range(0, rank).Select(_ => 20).ToArray();
        }


        public NdArrayFormatConfig(string expression)
        {
            var index = 0;
            foreach(var match in _Format.Matches(expression).Cast<Match>())
            {
                Guard.AssertFormat(match.Index == index);
                try
                {
                    var opt = match.Groups[1].Value;
                    var arg = match.Groups[2].Value;
                    switch(opt)
                    {
                    case "T":
                        Options |= FormatOption.OutputsElementType;
                        break;
                    case "S":
                        Options |= FormatOption.OutputsShapeSummary;
                        break;
                    case "W":
                        LineWidth = int.Parse(arg);
                        break;
                    case "L":
                        ReadOnlySpan<string> indices = arg.Split(',');
                        if(string.IsNullOrWhiteSpace(indices[indices.Length - 1]))
                            indices = indices.Slice(0, indices.Length - 1);
                        _AxesLimits = new int[indices.Length];
                        for(var i = 0 ; i < indices.Length ; ++i)
                            _AxesLimits[i] = ParseLimitArrayElement(indices[i]);
                        break;
                    case "E":
                        ElementFormat = arg;
                        break;
                    default:
                        Guard.ThrowFormatError();
                        return;
                    }
                }
                catch(FormatException)
                {
                    throw;
                }
                catch(Exception e)
                {
                    throw new FormatException(null, e);
                }

                _AxesLimits = _AxesLimits ?? Array.Empty<int>();
                index = match.Index + match.Length;
            }
        }


        public int[] GetFilledAxesLimits(int rank)
        {
            if(rank <= _AxesLimits.Length)
                return _AxesLimits.AsSpan(0, rank).ToArray();

            var retval = new int[rank];
            retval.AsSpan().Fill(20);
            AxesLimits.CopyTo(retval);
            return retval;
        }


        public static NdArrayFormatConfig GetDefault(int rank)
            => new NdArrayFormatConfig(rank);


        private static int ParseLimitArrayElement(string text)
        {
            text = text.Trim();
            if(text == "*")
                return int.MaxValue;
            if(int.TryParse(text, out var value))
                return value;
            Guard.ThrowFormatError();
            return default;
        }

    }

    [Flags]
    internal enum FormatOption
    {

        OutputsElementType  = 1,
        OutputsShapeSummary = 2,

    }
}
