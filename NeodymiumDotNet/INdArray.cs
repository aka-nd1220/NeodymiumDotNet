using System;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Generic type independent interface of <see cref="NdArray{T}"/> and <see cref="MutableNdArray{T}"/>.
    /// </summary>
    /// <remarks>
    ///     Do not implement this interface except <see cref="NdArray{T}"/> or <see cref="MutableNdArray{T}"/>.
    /// </remarks>
    public interface INdArray : IFormattable
    {

        /// <summary>
        ///     Gets the rank of this NdArray.
        /// </summary>
        int Rank { get; }


        /// <summary>
        ///     Gets the shape of this NdArray.
        /// </summary>
        IndexArray Shape { get; }


        /// <summary>
        ///     Gets the number of all elements.
        /// </summary>
        int Length { get; }


        /// <summary>
        ///     [Pure] Returns a string that represents the current object.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        string ToString(string format);


        /// <summary>
        ///     [Pure] Calculates the index of NdArray flatten sequence from the shaped indices.
        /// </summary>
        /// <param name="shapedIndices"> [<c>shapedIndices.Length == shape.Length</c>] </param>
        /// <returns></returns>
        int ToFlattenIndex(ReadOnlySpan<int> shapedIndices);


        /// <summary>
        ///     [Pure] Calculate the shaped indices from the index of NdArray flatten sequence.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IndexArray ToShapedIndices(int value);
    }


    /// <summary>
    ///     Presents readable n-dimension array.
    ///     This type does not mention mutability / immutability.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    ///     Do not implement this interface except <see cref="NdArray{T}"/> or <see cref="MutableNdArray{T}"/>.
    /// </remarks>
    public interface INdArray<T> : INdArray
    {
        /// <summary>
        ///     Gets the element of this NdArray.
        /// </summary>
        /// <param name="indices"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        T this[params Index[] indices] { get; }


        /// <summary>
        ///     Gets the element of this NdArray.
        /// </summary>
        /// <param name="indices"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        T this[ReadOnlySpan<Index> indices] { get; }

        /// <summary>
        ///     Gets the partial array.
        /// </summary>
        /// <param name="indices"></param>
        /// <returns></returns>
        INdArray<T> this[params IndexOrRange[] indices] { get; }

        /// <summary>
        ///     [Pure] Gets value by flatten index.
        /// </summary>
        /// <param name="flattenIndex"></param>
        /// <returns></returns>
        T GetItem(int flattenIndex);


        /// <summary>
        ///     [Pure] Gets immutable copy of this NdArray.
        /// </summary>
        /// <returns></returns>
        NdArray<T> ToImmutable();


        /// <summary>
        ///     [Pure] Gets mutable copy of this NdArray;
        ///     Its buffer is sequential in memory.
        /// </summary>
        /// <returns></returns>
        MutableNdArray<T> ToMutable();
    }
}
