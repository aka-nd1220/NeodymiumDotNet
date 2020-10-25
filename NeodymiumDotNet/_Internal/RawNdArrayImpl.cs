using System;
using System.Collections.Generic;
using System.Linq;
using NeodymiumDotNet.Optimizations;

namespace NeodymiumDotNet
{
    internal class RawNdArrayImpl<T> : MutableNdArrayImpl<T>, IBufferNdArrayImpl<T>
    {
        private readonly Allocation<T> _bufferCollector;

        /// <summary>
        ///     NOTE: This array may be managed by ArrayPool.
        ///     Do not capture with potential of lifetime extension.
        /// </summary>
        public Memory<T> Buffer => _bufferCollector.Memory;
        ReadOnlyMemory<T> IBufferNdArrayImpl<T>.Buffer => Buffer;


        /// <inheritdoc />
        /// <summary>
        ///     The element count when this NdArray was flatten.
        /// </summary>
        public override int Length { get; }


        /// <inheritdoc />
        /// <summary>
        ///     Create new RawNdArrayImpl{T} object.
        /// </summary>
        /// <param name="shape"></param>
        public RawNdArrayImpl(IndexArray shape)
            : base(shape)
        {
            Length = Shape.TotalLength;
            _bufferCollector = new Allocation<T>(Length);
        }


        protected override ref T GetItemRef(int flattenIndex)
            => ref Buffer.Span[flattenIndex];


        protected override ref T GetItemRef(ReadOnlySpan<int> shapedIndices)
            => ref Buffer.Span[ToFlattenIndex(Shape, shapedIndices)];


        protected override void CopyToCore(Span<T> dest)
        {
            Buffer.Span.CopyTo(dest);
        }


        public override T[] ToArray()
            => Buffer.ToArray();
    }
}
