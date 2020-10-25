using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace NeodymiumDotNet.Linq
{
    public static partial class NdLinq
    {

        /// <summary>
        ///     [Pure] Filter  partial NdArray.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndarray"> [NonNull] </param>
        /// <param name="filterAxis"></param>
        /// <param name="predicate"> [NonNull] </param>
        /// <returns> [NonNull] </returns>
        public static NdArray<T> Where<T>(
            this NdArray<T> ndarray,
            int filterAxis,
            Func<NdArray<T>, bool> predicate)
        {
            Guard.AssertArgumentNotNull(ndarray, nameof(ndarray));
            Guard.AssertArgumentNotNull(predicate, nameof(predicate));

            return new NdArray<T>(new WhereNdArrayImpl<T>(ndarray, filterAxis, predicate));
        }


        /// <summary>
        ///     Linq NdArray implementation with <c>Where</c> operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal class WhereNdArrayImpl<T> : NdArrayImpl<T>
        {

            private readonly NdArray<T> _Source;

            private readonly int _FilterAxis;

            private readonly IReadOnlyDictionary<int, int> _AxisIndexMap;


            public WhereNdArrayImpl(
                NdArray<T> source,
                int filterAxis,
                Func<NdArray<T>, bool> predicate)
                : this(source, filterAxis, predicate, null)
            {
            }


            private WhereNdArrayImpl(
                NdArray<T> source,
                int filterAxis,
                Func<NdArray<T>, bool> predicate,
                IReadOnlyDictionary<int, int>? axisIndexMap)
                : base(Calculate(source, filterAxis, predicate, out axisIndexMap))
            {
                _Source = source;
                _FilterAxis = filterAxis;
                _AxisIndexMap = axisIndexMap;
            }


            private static IndexArray Calculate(
                NdArray<T> source,
                int filterAxis,
                Func<NdArray<T>, bool> predicate,
                out IReadOnlyDictionary<int, int> axisIndexMap)
            {
                var tmpAxesMap = new Dictionary<int, int>();
                var from = 0;
                var to = 0;
                foreach(var part in source.AsEnumerable(filterAxis))
                {
                    if(predicate(part))
                    {
                        tmpAxesMap.Add(from, to);
                        ++from;
                    }

                    ++to;
                }

                axisIndexMap = tmpAxesMap;
                var newShape = source.Shape.ToArray();
                newShape[filterAxis] = from;
                return newShape;
            }


            protected override T GetItem(ReadOnlySpan<int> shapedIndices)
            {
                Span<Index> indices = stackalloc Index[shapedIndices.Length];
                MemoryMarshal.Cast<int, Index>(shapedIndices).CopyTo(indices);
                indices[_FilterAxis] = _AxisIndexMap[indices[_FilterAxis].Value];
                return _Source[indices];
            }


            protected override T GetItem(int flattenIndex)
                => GetItem(ToShapedIndices(flattenIndex));
        }

    }
}
