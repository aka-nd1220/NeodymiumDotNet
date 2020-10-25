using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Mutable multi-dim NdArray.
    ///     This type is not compatible with <see cref="NdArray{T}"/>.
    ///     Use <see cref="MutableNdArray{T}.ToImmutable"/> or <see cref="MutableNdArray{T}.MoveToImmutable"/> to get immutable NdArray from this.
    /// </summary>
    /// <typeparam name="T"> The data type. </typeparam>
    public class RawNdArray<T> : MutableNdArray<T>
    {

        /// <summary>
        ///     The NdArray elements entity.
        /// </summary>
        internal new RawNdArrayImpl<T> Entity => (RawNdArrayImpl<T>)base.Entity;

        #region constructors

        /// <summary>
        ///     Creates new NdArray object with entity object.
        /// </summary>
        /// <param name="entity"></param>
        internal RawNdArray(RawNdArrayImpl<T> entity)
            : base(entity)
        {
        }


        /// <summary>
        ///     Creates new NdArray object with values and shapes.
        /// </summary>
        /// <param name="shape"> [<c>shape.Product() == array.Length</c>] </param>
        internal RawNdArray(IndexArray shape)
            : base(new RawNdArrayImpl<T>(shape))
        {
        }


        /// <summary>
        ///     Creates new NdArray object.
        /// </summary>
        /// <param name="array"></param>
        internal RawNdArray(Array array)
            : base(GetInitializedEntity(array))
        {
        }
        private static RawNdArrayImpl<T> GetInitializedEntity(Array array)
        {
            var shape = Enumerable
                       .Range(0, array.Rank)
                       .Select(array.GetLength)
                       .ToIndexArray();
            var entity = new RawNdArrayImpl<T>(shape);
            foreach(var (value, i) in array.Cast<T>().Select((x, i) => (x, i)))
                entity.Buffer.Span[i] = value;
            return entity;
        }

        #endregion

    }
}
