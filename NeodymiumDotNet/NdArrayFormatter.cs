using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Defines a method to format a <see cref="NdArray{T}"/> instance to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NdArrayFormatter<T> : ICustomFormatter
    {
        /// <summary>
        ///     Gets a default instance of <see cref="NdArrayFormatter{T}"/>.
        /// </summary>
        public static NdArrayFormatter<T> Default = new NdArrayFormatter<T>();


        /// <inheritdoc />
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if(arg is NdArray<T> array)
            {
                return Format(format, array, formatProvider);
            }

            if(arg is MutableNdArray<T> mutableArray)
            {
                return Format(format, mutableArray, formatProvider);
            }

            Guard.ThrowArgumentError("arg is not NdArray instance.");
            throw new NotSupportedException();
        }


        /// <inheritdoc />
        public string Format(string format, NdArray<T> array, IFormatProvider formatProvider)
            => FormatCore(format, array, formatProvider, "NdArray");


        /// <inheritdoc />
        public string Format(string format, MutableNdArray<T> array,
                             IFormatProvider formatProvider)
            => FormatCore(format, array.ToImmutable(), formatProvider, "MutableNdArray");


        private string FormatCore(string format, NdArray<T> array,
                                  IFormatProvider formatProvider, string typeName)
        {
            var cfg = string.IsNullOrEmpty(format)
                          ? NdArrayFormatConfig.GetDefault(array.Rank)
                          : new NdArrayFormatConfig(format);
            NdArray<string> texts;
            {
                string elementToString(T x)
                    => x is IFormattable y
                           ? y.ToString(cfg.ElementFormat, formatProvider)
                           : x?.ToString() ?? "";

                var tmp = array.AsEnumerable().Select(elementToString).ToArray();
                var maxlen = tmp.Select(x => x.Length).Max();
                for(var i = 0 ; i < tmp.Length ; ++i)
                    tmp[i] = tmp[i].PadLeft(maxlen, ' ');
                texts = NdArray.Create(tmp, array.Shape);
            }

            var baseIndent = new string(' ', typeName.Length + 1);
            var result = new StringBuilder();
            switch(cfg.Options)
            {
            case FormatOption.OutputsElementType:
                result.AppendLine($"{typeName}(Type={typeof(T).Name},");
                result.Append(baseIndent);
                break;
            case FormatOption.OutputsShapeSummary:
                result.AppendLine($"{typeName}(Shape={array.Shape},");
                result.Append(baseIndent);
                break;
            case FormatOption.OutputsElementType | FormatOption.OutputsShapeSummary:
                result.AppendLine($"{typeName}(Type={typeof(T).Name}, Shape={array.Shape},");
                result.Append(baseIndent);
                break;
            default:
                result.Append($"{typeName}(");
                break;
            }

            var contentLines = ToStringRect(texts, cfg.GetFilledAxesLimits(array.Rank),
                                            cfg.LineWidth - typeName.Length - 1);
            var content = string.Join("\r\n" + baseIndent, contentLines);
            result.Append(content);
            result.Append(')');
            return result.ToString();
        }


        private static IEnumerable<string> ToStringRect(NdArray<string> array,
                                                        ReadOnlySpan<int> shapeLim, int width)
        {
            if(array.Rank == 1)
                return ToStringRect1(array, shapeLim, width);
            return ToStringRectN(array, shapeLim, width);
        }


        private static IEnumerable<string> ToStringRectN(NdArray<string> array,
                                                         ReadOnlySpan<int> shapeLim, int width)
        {
            var currentShapeLim = shapeLim[0];
            var formerSideShapeLim = (currentShapeLim + 1) / 2;
            var laterSideShapeLim = currentShapeLim / 2;
            var nextShapeLim = shapeLim.Slice(1).ToArray();

            IEnumerable<string> core()
            {
                for(var i = 0 ; i < array.Shape[0] ; ++i)
                {
                    var cache = "";
                    if(i < formerSideShapeLim || array.Shape[0] - laterSideShapeLim <= i)
                    {
                        // sub-array
                        var j = 0;
                        foreach(var line in ToStringRect(array[new IndexOrRange(i)],
                                                         nextShapeLim, width - 1))
                        {
                            if(j > 0)
                                yield return cache;
                            cache = line;
                            ++j;
                        }
                    }
                    else
                    {
                        // abbreviation
                        cache = "...";
                        i = array.Shape[0] - currentShapeLim / 2 - 1;
                    }

                    if(i < array.Shape[0] - 1)
                        cache += ",";
                    yield return cache;
                }
            }

            return PutInBracket(core());
        }


        private static IEnumerable<string> ToStringRect1(NdArray<string> array,
                                                         ReadOnlySpan<int> shapeLim, int width)
        {
            var currentShapeLim = shapeLim[0];
            var leftSideShapeLim = (currentShapeLim + 1) / 2;
            var rightSideShapeLim = currentShapeLim / 2;
            var elLen = array[0].Length + 2;

            IEnumerable<string> core()
            {
                var sb = new StringBuilder();
                for(var i = 0 ; ; ++i)
                {
                    if(i < leftSideShapeLim || array.Shape[0] - rightSideShapeLim <= i)
                    {
                        // element
                        sb.Append(array.GetItem(i) + ", ");
                    }
                    else
                    {
                        // abbreviation
                        sb.Append("..., ");
                        i = array.Shape[0] - currentShapeLim / 2 - 1;
                    }

                    if(i >= array.Shape[0] - 1)
                        break;
                    if(sb.Length + elLen > width)
                    {
                        // line feeding
                        sb.Length -= 1;
                        yield return sb.ToString();
                        sb = new StringBuilder();
                    }
                }

                sb.Length -= NdMath.Min(2, sb.Length);
                yield return sb.ToString();
            }

            return PutInBracket(core());
        }


        private static IEnumerable<string> PutInBracket(IEnumerable<string> lines)
            => lines.SelectTop(x => "{" + x, x => " " + x)
                    .SelectTail(x => x, x => x + "}")
                    .DefaultIfEmpty("{}");

    }
}
