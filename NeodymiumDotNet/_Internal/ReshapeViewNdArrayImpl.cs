using System;
using System.Collections.Generic;

namespace NeodymiumDotNet
{
    internal sealed class ReshapeViewNdArrayImpl<T> : NdArrayImpl<T>
    {

        private readonly NdArrayImpl<T> _source;


        protected override T GetItem(int flattenIndex)
            => _source[flattenIndex];


        protected override T GetItem(ReadOnlySpan<int> shapedIndices)
            => _source[ToFlattenIndex(Shape, shapedIndices)];


        internal ReshapeViewNdArrayImpl(NdArrayImpl<T> source, IndexArray shape)
            : base(shape)
        {
            _source = source;
        }

    }



    internal sealed class MutableReshapeViewNdArrayImpl<T> : MutableNdArrayImpl<T>
    {

        private readonly MutableNdArrayImpl<T> _source;


        protected override ref T GetItemRef(int flattenIndex)
            => ref _source[flattenIndex];


        protected override ref T GetItemRef(ReadOnlySpan<int> shapedIndices)
            => ref _source[ToFlattenIndex(Shape, shapedIndices)];


        internal MutableReshapeViewNdArrayImpl(MutableNdArrayImpl<T> source, IndexArray shape)
            : base(shape)
        {
            _source = source;
        }

    }
}
