using System;
using System.Collections.Generic;
using System.Linq;

namespace NeodymiumDotNet
{
    internal static class InternalUtils
    {
        /// <summary>
        ///     Ceilings the specified value to the nearest power of 2.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static int CeilingPow2(int value)
        {
            var x = (uint)value - 1;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return (int)(x + 1);
        }


        /// <summary>
        ///     Exchanges two values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal static void Exchange<T>(ref T x, ref T y)
        {
            var tmp = x;
            x = y;
            y = tmp;
        }


        /// <summary>
        ///     Fills whole range to shapes.
        /// </summary>
        /// <param name="indices"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        internal static IndexOrRange[] FillImplicitSlices(IndexOrRange[] indices, int rank)
        {
            if(indices.Length == rank)
                return indices;
            var remain = rank - indices.Length;
            indices = indices
                     .Concat(Enumerable.Repeat((IndexOrRange)Range.Whole, remain))
                     .ToArray();
            return indices;
        }


        /// <summary>
        ///     For axes-formed linq operator, calculates new shape.
        /// </summary>
        /// <param name="sourceShape"></param>
        /// <param name="axes"></param>
        /// <returns></returns>
        internal static IndexArray CalculateAxesFormedShape(
            IndexArray sourceShape, ReadOnlySpan<int> axes)
        {
            var newShape = new int[sourceShape.Length - axes.Length];
            for(int i = 0, j = 0 ; i < sourceShape.Length ; ++i)
            {
                if(axes.Contains(i))
                    continue;
                newShape[j] = sourceShape[i];
                ++j;
            }

            return new IndexArray(newShape);
        }


        /// <summary>
        ///     For axes-formed linq, operator, calculates the partial array shape.
        /// </summary>
        /// <param name="sourceShape"></param>
        /// <param name="axes"></param>
        /// <param name="flattenIndex"></param>
        /// <returns></returns>
        internal static IndexOrRange[] CalculatePartialShape(
            IndexArray sourceShape,
            ReadOnlySpan<int> axes,
            int flattenIndex)
        {
            var len = sourceShape.Length;
            var newShape = CalculateAxesFormedShape(sourceShape, axes);
            var indicesOnThis = NdArrayImpl.ToShapedIndices(newShape, flattenIndex);
            var indexOrRangesOnSource = new IndexOrRange[len];
            for(int i = 0, j = 0 ; i < len ; ++i)
            {
                if(!axes.Contains(i))
                {
                    indexOrRangesOnSource[i] = new Index(indicesOnThis[j], false);
                    ++j;
                }
                else
                {
                    indexOrRangesOnSource[i] = Range.Whole;
                }
            }

            return indexOrRangesOnSource;
        }


        /// <summary>
        ///     Determines whether an element is in the <see cref="Span{T}"/>.
        /// </summary>
        /// <param name="span"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool Contains(this Span<int> span, int value)
            => ((ReadOnlySpan<int>)span).Contains(value);


        /// <summary>
        ///     Determines whether an element is in the <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <param name="span"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool Contains(this ReadOnlySpan<int> span, int value)
        {
            foreach(var x in span)
                if(x == value)
                    return true;
            return false;
        }


        /// <summary>
        ///     Gets an equivalent memory buffer of the data of the specified NdArray.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        internal static IBufferNdArrayImpl<T> GetOrCopyBuffer<T>(this NdArray<T> ndArray)
            => (ndArray.Entity as IBufferNdArrayImpl<T>)
               ?? (ndArray.ToMutable().Entity as RawNdArrayImpl<T>)!;


        /// <summary>
        ///     Gets an equivalent memory buffer of the data of the specified NdArray.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        internal static IBufferNdArrayImpl<T> GetOrCopyBuffer<T>(this MutableNdArray<T> ndArray)
            => (ndArray.Entity as IBufferNdArrayImpl<T>)
               ?? (ndArray.ToMutable().Entity as RawNdArrayImpl<T>)!;
    }
}
