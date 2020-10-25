using System;
using System.Collections.Generic;

namespace NeodymiumDotNet
{
    internal abstract class MutableNdArrayImpl<T> : NdArrayImpl<T>
    {

        /// <summary>
        ///     The element indexer which is called with the index of NdArray flatten sequence.
        ///     NOTE: Do not capture with potential of lifetime extension.
        /// </summary>
        /// <param name="flattenIndex"></param>
        internal new ref T this[int flattenIndex] => ref GetItemRef(flattenIndex);


        /// <summary>
        ///     The element indexer which is called with flatten index.
        ///     NOTE: Do not capture with potential of lifetime extension.
        /// </summary>
        /// <remarks>
        ///     This indexer may be destructive for <paramref name="shapedIndices"/>.
        /// </remarks>
        /// <param name="shapedIndices"></param>
        internal new ref T this[ReadOnlySpan<int> shapedIndices] => ref GetItemRef(shapedIndices);


        /// <inheritdoc />
        /// <summary>
        ///     Create new MutableNdArrayImpl{T} object with assigned shape.
        /// </summary>
        /// <param name="shape"></param>
        protected MutableNdArrayImpl(IndexArray shape)
            : base(shape)
        {
        }


        /// <summary>
        ///     Gets reference to element which is called with the index of NdArray flatten sequence.
        /// </summary>
        /// <param name="flattenIndex"></param>
        /// <returns></returns>
        protected abstract ref T GetItemRef(int flattenIndex);


        /// <summary>
        ///     Gets reference to element which is called with flatten index.
        /// </summary>
        /// <param name="shapedIndices"></param>
        protected abstract ref T GetItemRef(ReadOnlySpan<int> shapedIndices);


        protected override sealed T GetItem(int flattenIndex) => GetItemRef(flattenIndex);


        protected override sealed T GetItem(ReadOnlySpan<int> shapedIndices) => GetItemRef(shapedIndices);
    }
}
