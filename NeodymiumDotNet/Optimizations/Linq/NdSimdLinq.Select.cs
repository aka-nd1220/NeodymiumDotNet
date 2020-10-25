using System;
using System.Linq.Expressions;

namespace NeodymiumDotNet.Optimizations.Linq
{
    partial class NdSimdLinq
    {
        /// <summary>
        ///     [Pure] Projects each element of a NdArray into a new form.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndarray"> A NdArray for values to invoke a transform function on. </param>
        /// <param name="selector"> A transform function to apply to each element. </param>
        /// <returns> [<c>$ReturnValue.Shape == NdArray.Shape</c>] </returns>
        public static NdArray<T> Select<T>(
            this NdArray<T> ndarray,
            Expression<Func<T, T>> selector)
            where T : unmanaged
        {
            Guard.AssertArgumentNotNull(ndarray, nameof(ndarray));
            Guard.AssertArgumentNotNull(selector, nameof(selector));

            ReadOnlyMemory<T> source = ndarray.Entity switch
            {
                IBufferNdArrayImpl<T> romProvider => romProvider.Buffer,
                _ => default(ReadOnlyMemory<T>?),
            } ?? throw new NotSupportedException();

            var simdSelector = VectorOperation.Simdize(selector);
            var resultEntity = new RawNdArrayImpl<T>(ndarray.Shape);
            simdSelector(source, resultEntity.Buffer.Slice(0, resultEntity.Length));
            return new NdArray<T>(resultEntity);
        }
    }
}
