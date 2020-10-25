using System;
using System.Collections.Generic;
using System.Text;

namespace NeodymiumDotNet._Internal
{
    internal class TransposeNdArrayImpl<T> : NdArrayImpl<T>
    {
        private readonly NdArrayImpl<T> _source;

        private readonly int[] _axisMap;


        public TransposeNdArrayImpl(NdArrayImpl<T> source, ReadOnlySpan<int> axisMap)
            : base(TransposeShape(source.Shape, axisMap))
        {
            _source = source;
            _axisMap = axisMap.ToArray();
        }


        protected override T GetItem(int flattenIndex)
            => this[ToShapedIndices(Shape, flattenIndex)];


        protected override T GetItem(ReadOnlySpan<int> shapedIndices)
        {
            Span<int> indices = stackalloc int[_source.Rank];
            ToTransposedShapedIndices(Shape, shapedIndices, _axisMap, indices);
            return _source[indices];
        }


        private static IndexArray TransposeShape(IndexArray baseShape, ReadOnlySpan<int> axisMap)
        {
            Span<int> newShape = stackalloc int[baseShape.Length];
            for(var i = 0; i < baseShape.Length; ++i)
                newShape[i] = baseShape[axisMap[i]];
            return new IndexArray(newShape);
        }


        internal static void ToTransposedShapedIndices(
            IndexArray shape,
            ReadOnlySpan<int> shapedIndices,
            ReadOnlySpan<int> axisMap,
            Span<int> resultIndices)
        {
            for(var i = 0; i < shape.Length; ++i)
                resultIndices[i] = shapedIndices[axisMap[i]];
        }
    }



    internal class MutableTransposeNdArrayImpl<T> : MutableNdArrayImpl<T>
    {
        private readonly MutableNdArrayImpl<T> _source;

        private readonly int[] _axisMap;


        public MutableTransposeNdArrayImpl(MutableNdArrayImpl<T> source, ReadOnlySpan<int> axisMap)
            : base(TransposeShape(source.Shape, axisMap))
        {
            _source = source;
            _axisMap = axisMap.ToArray();
        }


        protected override ref T GetItemRef(int flattenIndex)
            => ref this[ToShapedIndices(Shape, flattenIndex)];


        protected override ref T GetItemRef(ReadOnlySpan<int> shapedIndices)
        {
            var indices = new int[_source.Rank];
            TransposeNdArrayImpl<T>.ToTransposedShapedIndices(Shape, shapedIndices, _axisMap, indices);
            return ref _source[indices];
        }


        private static IndexArray TransposeShape(IndexArray baseShape, ReadOnlySpan<int> axisMap)
        {
            Span<int> newShape = stackalloc int[baseShape.Length];
            for(var i = 0; i < baseShape.Length; ++i)
                newShape[i] = baseShape[axisMap[i]];
            return new IndexArray(newShape);
        }
    }

}
