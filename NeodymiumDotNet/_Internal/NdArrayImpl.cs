using System;
using System.Collections;
using System.Collections.Generic;

namespace NeodymiumDotNet
{
    internal interface INdArrayImpl
    {
        int Length { get; }
        int Rank { get; }
        IndexArray Shape { get; }
        int ToFlattenIndex(int i1);
        int ToFlattenIndex(int i1, int i2);
        int ToFlattenIndex(int i1, int i2, int i3);
        int ToFlattenIndex(ReadOnlySpan<int> shapedIndices);
        IndexArray ToShapedIndices(int flattenIndex);
    }


    internal interface INdArrayImpl<T> : INdArrayImpl
    {
        T this[int flattenIndex] { get; }
        T this[ReadOnlySpan<int> shapedIndices] { get; }
    }


    internal abstract class NdArrayImpl : INdArrayImpl
    {
        /// <summary>
        ///     The element count when this NdArray was flatten.
        /// </summary>
        public virtual int Length => Shape.TotalLength;


        /// <summary>
        ///     The rank of NdArray.
        /// </summary>
        public int Rank => Shape.Length;


        /// <summary>
        ///     The shape of NdArray.
        /// </summary>
        public IndexArray Shape { get; }


        protected NdArrayImpl(IndexArray shape)
        {
            Shape = shape;
        }


        /// <summary>
        ///     [Pure] Calculate the index of NdArray flatten sequence from the shaped indices.
        /// </summary>
        /// <param name="i1"></param>
        /// <returns></returns>
        public int ToFlattenIndex(int i1) => ToFlattenIndex(stackalloc int[] { i1 });


        /// <summary>
        ///     [Pure] Calculate the index of NdArray flatten sequence from the shaped indices.
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <returns></returns>
        public int ToFlattenIndex(int i1, int i2) => ToFlattenIndex(stackalloc int[] { i1, i2 });


        /// <summary>
        ///     [Pure] Calculate the index of NdArray flatten sequence from the shaped indices.
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <param name="i3"></param>
        /// <returns></returns>
        public int ToFlattenIndex(int i1, int i2, int i3) => ToFlattenIndex(stackalloc int[] { i1, i2, i3 });


        /// <summary>
        ///     [Pure] Calculate the index of NdArray flatten sequence from the shaped indices.
        /// </summary>
        /// <param name="shapedIndices"> [<c>shapedIndices.Length == shape.Length</c>] </param>
        /// <returns></returns>
        public int ToFlattenIndex(ReadOnlySpan<int> shapedIndices)
            => ToFlattenIndex(Shape, shapedIndices);


        /// <summary>
        ///     [Pure] Calculate the index of NdArray flatten sequence from the shaped indices.
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="shapedIndices"> [<c>shapedIndices.Length == shape.Length</c>] </param>
        /// <returns></returns>
        public static int ToFlattenIndex(IndexArray shape, ReadOnlySpan<int> shapedIndices)
        {
            Guard.AssertIndices(shape, shapedIndices);

            var rank = shape.Length;
            var i = 0;
            var retval = 0;
            for(; i < rank - 1; ++i)
            {
                retval = shape[i + 1] * (retval + shapedIndices[i]);
            }

            return retval + shapedIndices[i];
        }


        /// <summary>
        ///     [Pure] Calculate the shaped indices from the index of NdArray flatten sequence.
        /// </summary>
        /// <param name="flattenIndex"></param>
        /// <returns></returns>
        public IndexArray ToShapedIndices(int flattenIndex)
            => ToShapedIndices(Shape, flattenIndex);


        /// <summary>
        ///     [Pure] Calculate the shaped indices from the index of NdArray flatten sequence.
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="flattenIndex"></param>
        /// <returns></returns>
        public static IndexArray ToShapedIndices(IndexArray shape, int flattenIndex)
        {
            var rank = shape.Length;
            var retval = new int[rank];
            for(var i = rank - 1; i >= 0; --i)
            {
                retval[i] = flattenIndex % shape[i];
                flattenIndex /= shape[i];
            }

            return retval;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Immutable NdArray core system implement base class.
    /// </summary>
    internal abstract class NdArrayImpl<T> : NdArrayImpl
    {

        /// <summary>
        ///     The element indexer which is called with the index of NdArray flatten sequence.
        /// </summary>
        /// <param name="flattenIndex"></param>
        public T this[int flattenIndex] => GetItem(flattenIndex);


        /// <summary>
        ///     The element indexer which is called with flatten index.
        /// </summary>
        /// <remarks>
        ///     This indexer may be destructive for <paramref name="shapedIndices"/>.
        /// </remarks>
        /// <param name="shapedIndices"></param>
        public T this[ReadOnlySpan<int> shapedIndices] => GetItem(shapedIndices);


        /// <summary>
        ///     Create new NdArrayImpl{T} object with assigned shape.
        /// </summary>
        /// <param name="shape"></param>
        private protected NdArrayImpl(IndexArray shape)
            : base(shape)
        {
        }


        /// <summary>
        ///     Get element which is called with the index of NdArray flatten sequence.
        /// </summary>
        /// <param name="flattenIndex"></param>
        /// <returns></returns>
        protected abstract T GetItem(int flattenIndex);


        /// <summary>
        ///     Get element which is called with flatten index.
        /// </summary>
        /// <param name="shapedIndices"></param>
        protected abstract T GetItem(ReadOnlySpan<int> shapedIndices);


        /// <summary>
        ///     Copies the elements to destination.
        /// </summary>
        /// <param name="dest"></param>
        protected virtual void CopyToCore(Span<T> dest)
        {
            for(var i = 0; i < Length; ++i)
                dest[i] = GetItem(i);
        }


        /// <summary>
        ///     Copies the elements to destination.
        /// </summary>
        /// <param name="dest"></param>
        public void CopyTo(Span<T> dest)
        {
            Guard.AssertArgument(dest.Length >= Length, "Too short destination.");
            CopyToCore(dest);
        }


        /// <summary>
        ///     Create copy array.
        /// </summary>
        /// <returns></returns>
        public virtual T[] ToArray()
        {
            var len    = Length;
            var retval = new T[len];
            for(var i = 0 ; i < len ; ++i)
            {
                retval[i] = this[i];
            }

            return retval;
        }


        /// <summary>
        ///     [Pure]
        /// </summary>
        /// <returns></returns>
        public sealed override int GetHashCode()
        {
            var basis = Shape.GetHashCode();
            switch(Length)
            {
            case 0:
                return basis;
            case 1:
                return basis ^ this[0]!.GetHashCode();
            case 2:
                return basis ^ this[0]!.GetHashCode() ^ this[Length - 1]!.GetHashCode();
            default:
                return basis ^ this[0]!.GetHashCode() ^ this[1]!.GetHashCode()
                       ^ this[Length - 1]!.GetHashCode();
            }
        }

    }
}
