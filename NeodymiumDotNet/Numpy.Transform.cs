using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeodymiumDotNet._Internal;

namespace NeodymiumDotNet
{
    partial class NdArray
    {
        #region Transpose

        /// <summary>
        ///     Transposes the row and the column of the specified NdArray.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        /// <exception cref="ShapeMismatchException"></exception>
        public static NdArray<T> Transpose<T>(this NdArray<T> ndArray)
        {
            Guard.AssertShapeMatch(
                ndArray.Rank == 2,
                "To transpose NdArrays except its rank is 2, the axes replacement set must be specified.");
            return ndArray.Transpose(new IndexArray(stackalloc int[] { 1, 0 }));
        }

        /// <summary>
        ///     Transposes the axes of the specified NdArray.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <param name="axesMap"></param>
        /// <returns></returns>
        /// <exception cref="ShapeMismatchException"></exception>
        public static NdArray<T> Transpose<T>(this NdArray<T> ndArray, ReadOnlySpan<int> axesMap)
        {
            Guard.AssertShapeMatch(
                ndArray.Rank == axesMap.Length,
                "replacedAxes array must be a same length with the rank of the specified NdArray.");

            return new NdArray<T>(new TransposeNdArrayImpl<T>(ndArray.Entity, axesMap));
        }

        /// <summary>
        ///     Transposes the row and the column of the specified NdArray.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        /// <exception cref="ShapeMismatchException"></exception>
        public static MutableNdArray<T> Transpose<T>(this MutableNdArray<T> ndArray)
        {
            Guard.AssertShapeMatch(
                ndArray.Rank == 2,
                "To transpose NdArrays except its rank is 2, the axes replacement set must be specified.");
            return ndArray.Transpose(new IndexArray(stackalloc int[] { 1, 0 }));
        }

        /// <summary>
        ///     Transposes the axes of the specified NdArray.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <param name="axesMap"></param>
        /// <returns></returns>
        /// <exception cref="ShapeMismatchException"></exception>
        public static MutableNdArray<T> Transpose<T>(this MutableNdArray<T> ndArray, ReadOnlySpan<int> axesMap)
        {
            Guard.AssertShapeMatch(
                ndArray.Rank == axesMap.Length,
                "replacedAxes array must be a same length with the rank of the specified NdArray.");

            return new MutableNdArray<T>(new MutableTransposeNdArrayImpl<T>(ndArray.Entity, axesMap));
        }

        #endregion


        #region Reshape

        /// <summary>
        ///     [Pure] Reshapes this NdArray.
        /// </summary>
        /// <param name="ndArray"></param>
        /// <param name="newShape">
        ///     [<c>
        ///         Length == newLen ||
        ///         (newShape.Count(i =&gt; i == -1) == 1 &amp;&amp; newShape.Count(i =&gt; i &lt; -1) == 0 &amp;&amp; Length % newLen == 0)
        ///     </c>]
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static NdArray<T> Reshape<T>(this NdArray<T> ndArray, params int[] newShape)
        {
            var (newLen, flexRank, flexDefiner) = GetReshapeFlexibleAxis(ndArray, newShape);
            switch(flexDefiner)
            {
            case 0:
                return new NdArray<T>(new ReshapeViewNdArrayImpl<T>(ndArray.Entity, newShape));
            case -1 when ndArray.Length % newLen == 0:
                newShape = newShape.ToArray();
                newShape[flexRank] = ndArray.Length / newLen;
                return ndArray.Reshape(newShape);
            default:
                Guard.ThrowArgumentError("In newShape, the number of flexible index specifier must be 0 or 1.");
                throw new NotSupportedException();
            }
        }


        /// <summary>
        ///     [Pure] Reshapes this NdArray.
        /// </summary>
        /// <param name="ndArray"></param>
        /// <param name="newShape">
        ///     [<c>
        ///         Length == newLen ||
        ///       (newShape.Count(i =&gt; i == -1) == 1 &amp;&amp; newShape.Count(i =&gt; i &lt; -1) == 0 &amp;&amp; Length % newLen == 0)
        ///     </c>]
        /// </param>
        /// <returns></returns>
        public static MutableNdArray<T> Reshape<T>(this MutableNdArray<T> ndArray, params int[] newShape)
        {
            var (newLen, flexRank, flexDefiner) = GetReshapeFlexibleAxis(ndArray, newShape);
            switch(flexDefiner)
            {
            case 0:
                return new MutableNdArray<T>(new MutableReshapeViewNdArrayImpl<T>(ndArray.Entity, newShape));
            case -1 when ndArray.Length % newLen == 0:
                newShape = newShape.ToArray();
                newShape[flexRank] = ndArray.Length / newLen;
                return ndArray.Reshape(newShape);
            default:
                Guard.ThrowArgumentError("In newShape, the number of flexible index specifier must be 0 or 1.");
                throw new NotSupportedException();
            }
        }


        private static (int newLen, int flexRank, int flexDefiner) GetReshapeFlexibleAxis<T>(INdArray<T> ndArray, int[] newShape)
        {
            var newLen = newShape.Aggregate((x, y) => x * y);
            var (flexRank, flexDefiner) = newShape
                                         .Select((index, i) => (index, i))
                                         .SingleOrDefault(x => x.i < 0);
            return (newLen, flexRank, flexDefiner);
        }

        #endregion
    }
}
