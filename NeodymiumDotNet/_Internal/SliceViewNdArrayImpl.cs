using System;
using System.Linq;

namespace NeodymiumDotNet
{
    /// <inheritdoc />
    /// <summary>
    ///     NdArray sliced view.
    /// </summary>
    internal sealed class SliceViewNdArrayImpl<T> : NdArrayImpl<T>
    {

        private readonly NdArrayImpl<T> _source;

        private readonly IndexOrRange[] _slices;


        protected override T GetItem(int flattenIndex)
            => this[ToShapedIndices(Shape, flattenIndex)];


        protected override T GetItem(ReadOnlySpan<int> shapedIndices)
        {
            Span<int> indices = stackalloc int[_source.Rank];
            ToSlicedShapedIndices(_source.Shape, shapedIndices, _slices, indices);
            return _source[indices];
        }


        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="slices"> [source.Rank != slices.Length] </param>
        public SliceViewNdArrayImpl(NdArrayImpl<T> source, IndexOrRange[] slices)
            : base(CalculateShape(source.Shape, slices))
        {
            Guard.AssertIndices(source.Shape, slices);
            _source = source;
            _slices = new IndexOrRange[slices.Length];
            Array.Copy(slices, _slices, slices.Length);
        }


        internal static IndexArray CalculateShape(
            IndexArray sourceShape,
            IndexOrRange[] slices)
            => slices
              .Zip(sourceShape, (slice, shape) => (slice, shape))
              .Where(x => x.slice.IsRange)
              .Select(x => x.slice.Range.MapLength(x.shape))
              .ToIndexArray();


        internal static void ToSlicedShapedIndices(
            IndexArray sourceShape,
            ReadOnlySpan<int> shapedIndices,
            IndexOrRange[] slices,
            Span<int> resultIndices)
        {
            var rank = sourceShape.Length;
            for(int i = 0, j = 0 ; i < rank ; ++i)
            {
                if(slices[i].IsRange)
                {
                    resultIndices[i] = slices[i]
                                .Range
                                .Map(shapedIndices[j], sourceShape[i]);
                    ++j;
                }
                else
                {
                    resultIndices[i] = slices[i]
                                .Index.Map(sourceShape[i]);
                }
            }
        }

    }



    /// <inheritdoc />
    /// <summary>
    ///     NdArray sliced view.
    /// </summary>
    internal sealed class MutableSliceViewNdArrayImpl<T> : MutableNdArrayImpl<T>
    {

        private readonly MutableNdArrayImpl<T> _source;

        private readonly IndexOrRange[] _slices;


        protected override ref T GetItemRef(int flattenIndex)
            => ref this[ToShapedIndices(Shape, flattenIndex)];


        protected override ref T GetItemRef(ReadOnlySpan<int> shapedIndices)
        {
            var indices = new int[_source.Rank];
            SliceViewNdArrayImpl<T>.ToSlicedShapedIndices(_source.Shape, shapedIndices, _slices, indices);
            return ref _source[indices];
        }


        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="slices"> [source.Rank != slices.Length] </param>
        public MutableSliceViewNdArrayImpl(MutableNdArrayImpl<T> source, IndexOrRange[] slices)
            : base(CalculateShape(source.Shape, slices))
        {
            Guard.AssertIndices(source.Shape, slices);
            _source = source;
            _slices = new IndexOrRange[slices.Length];
            Array.Copy(slices, _slices, slices.Length);
        }


        /// <summary>
        ///     [Pure]
        /// </summary>
        /// <param name="sourceShape"></param>
        /// <param name="slices"></param>
        /// <returns></returns>
        internal static IndexArray CalculateShape(
            IndexArray sourceShape,
            IndexOrRange[] slices)
            => slices
              .Zip(sourceShape, (slice, shape) => (slice, shape))
              .Where(x => x.Item1.IsRange)
              .Select(x => x.Item1.Range.MapLength(x.Item2))
              .ToIndexArray();

    }
}
