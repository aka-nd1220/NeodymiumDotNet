using System;
using System.Buffers;
using System.Linq;

namespace NeodymiumDotNet.Linq
{
    public static partial class NdLinq
    {

        /// <summary>
        ///     [Pure] Projects each element of a NdArray into a new form.
        /// </summary>
        /// <typeparam name="TSource"> The type of the elements of <paramref name="ndarray"/>. </typeparam>
        /// <typeparam name="TResult"> The type of the value returned by <paramref name="selector"/>. </typeparam>
        /// <param name="ndarray"> A NdArray for values to invoke a transform function on. </param>
        /// <param name="selector"> A transform function to apply to each element. </param>
        /// <returns> [<c>$ReturnValue.Shape == NdArray.Shape</c>] </returns>
        public static NdArray<TResult> Select<TSource, TResult>(
            this NdArray<TSource> ndarray,
            Func<TSource, TResult> selector)
            => ndarray.Select(selector, default);


        /// <summary>
        ///     [Pure] Projects each element of a NdArray into a new form.
        /// </summary>
        /// <typeparam name="TSource"> The type of the elements of <paramref name="ndarray"/>. </typeparam>
        /// <typeparam name="TResult"> The type of the value returned by <paramref name="selector"/>. </typeparam>
        /// <param name="ndarray"> A NdArray for values to invoke a transform function on. </param>
        /// <param name="selector"> A transform function to apply to each element. </param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns> [<c>$ReturnValue.Shape == NdArray.Shape</c>] </returns>
        public static NdArray<TResult> Select<TSource, TResult>(
            this NdArray<TSource> ndarray,
            Func<TSource, TResult> selector,
            IIterationStrategy? strategy = default)
        {
            Guard.AssertArgumentNotNull(ndarray, nameof(ndarray));
            Guard.AssertArgumentNotNull(selector, nameof(selector));

            var len = ndarray.Length;
            var entity = new RawNdArrayImpl<TResult>(ndarray.Shape);
            var array = entity.Buffer;
            if(strategy is null || strategy is IterationStrategy)
            {
                if(ndarray.Entity is RawNdArrayImpl<TSource> raw)
                {
                    for(var i = 0; i < len; ++i)
                        array.Span[i] = selector(raw.Buffer.Span[i]);
                }
                else
                {
                    for(var i = 0; i < len; ++i)
                        array.Span[i] = selector(ndarray.GetItem(i));
                }
            }
            else
            {
                if(ndarray.Entity is RawNdArrayImpl<TSource> raw)
                {
                    strategy.For(0, len, i=>
                        array.Span[i] = selector(raw.Buffer.Span[i]));
                }
                else
                {
                    strategy.For(0, len, i =>
                        array.Span[i] = selector(ndarray.GetItem(i)));
                }
            }
            return new NdArray<TResult>(entity);
        }


        /// <summary>
        ///     [Pure] Projects each partial NdArray of the specified NdArray into a new form.
        /// </summary>
        /// <typeparam name="TSource"> The type of the elements of <paramref name="ndarray"/>. </typeparam>
        /// <typeparam name="TResult"> The type of the value returned by <paramref name="selector"/>. </typeparam>
        /// <param name="ndarray"> A NdArray for values to invoke a transform function on. </param>
        /// <param name="projectionAxes"> The axes to enumerate partial NdArray. </param>
        /// <param name="selector"> A transform function to apply to each partial NdArray. </param>
        /// <returns>  </returns>
        public static NdArray<TResult> Select<TSource, TResult>(
            this NdArray<TSource> ndarray,
            ReadOnlySpan<int> projectionAxes,
            Func<NdArray<TSource>, TResult> selector)
            => ndarray.Select(projectionAxes, selector, default);


        /// <summary>
        ///     [Pure] Projects each partial NdArray of the specified NdArray into a new form.
        /// </summary>
        /// <typeparam name="TSource"> The type of the elements of <paramref name="ndarray"/>. </typeparam>
        /// <typeparam name="TResult"> The type of the value returned by <paramref name="selector"/>. </typeparam>
        /// <param name="ndarray"> A NdArray for values to invoke a transform function on. </param>
        /// <param name="projectionAxes"> The axes to enumerate partial NdArray. </param>
        /// <param name="selector"> A transform function to apply to each partial NdArray. </param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns>  </returns>
        public static NdArray<TResult> Select<TSource, TResult>(
            this NdArray<TSource> ndarray,
            ReadOnlySpan<int> projectionAxes,
            Func<NdArray<TSource>, TResult> selector,
            IIterationStrategy? strategy = default)
        {
            Guard.AssertArgumentNotNull(ndarray, nameof(ndarray));
            Guard.AssertArgumentNotNull(selector, nameof(selector));

            var shape = InternalUtils.CalculateAxesFormedShape(ndarray.Shape, projectionAxes);
            var len = shape.TotalLength;
            var entity = new RawNdArrayImpl<TResult>(shape);
            var array = entity.Buffer;
            if(strategy is null || strategy is IterationStrategy)
            {
                for(var i = 0; i < len; ++i)
                {
                    var indexOrRanges = InternalUtils.CalculatePartialShape(ndarray.Shape, projectionAxes, i);
                    array.Span[i] = selector(ndarray[indexOrRanges]);
                }
            }
            else
            {
                var projAxesCopy = projectionAxes.ToArray();
                strategy.For(0, len, i =>
                {
                    var indexOrRanges = InternalUtils.CalculatePartialShape(ndarray.Shape, projAxesCopy, i);
                    array.Span[i] = selector(ndarray[indexOrRanges]);
                });
            }
            return new NdArray<TResult>(entity);
        }
    }
}
