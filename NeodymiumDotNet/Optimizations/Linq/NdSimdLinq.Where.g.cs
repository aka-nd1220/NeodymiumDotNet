using System;
using System.Linq.Expressions;

namespace NeodymiumDotNet.Optimizations.Linq
{
    partial class NdSimdLinq
    {
        #region 2 arguments input

        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                NdArray<T> ndarray1,
                NdArray<T> ndarray2,
                Expression<Func<T, T, T>> selector
            )
            where T : unmanaged
        {
            ReadOnlyMemory<T> getSource(NdArray<T> ndarray)
                => ndarray.Entity switch
                {
                    IBufferNdArrayImpl<T> romProvider => romProvider.Buffer,
                    _ => default(ReadOnlyMemory<T>?),
                } ?? throw new NotSupportedException();

            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T>.Shape));

            var source1 = getSource(ndarray1);
            var source2 = getSource(ndarray2);
            var simdSelector = VectorOperation.Simdize(selector);
            var resultEntity = new RawNdArrayImpl<T>(ndarray1.Shape);
            simdSelector(source1, source2, resultEntity.Buffer.Slice(0, resultEntity.Length));
            return new NdArray<T>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                (NdArray<T>, NdArray<T>) argsTuple,
                Expression<Func<T, T, T>> selector
            )
            where T : unmanaged
            => Zip(argsTuple.Item1, argsTuple.Item2, selector);

        #endregion


        #region 3 arguments input

        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                NdArray<T> ndarray1,
                NdArray<T> ndarray2,
                NdArray<T> ndarray3,
                Expression<Func<T, T, T, T>> selector
            )
            where T : unmanaged
        {
            ReadOnlyMemory<T> getSource(NdArray<T> ndarray)
                => ndarray.Entity switch
                {
                    IBufferNdArrayImpl<T> romProvider => romProvider.Buffer,
                    _ => default(ReadOnlyMemory<T>?),
                } ?? throw new NotSupportedException();

            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T>.Shape));

            var source1 = getSource(ndarray1);
            var source2 = getSource(ndarray2);
            var source3 = getSource(ndarray3);
            var simdSelector = VectorOperation.Simdize(selector);
            var resultEntity = new RawNdArrayImpl<T>(ndarray1.Shape);
            simdSelector(source1, source2, source3, resultEntity.Buffer.Slice(0, resultEntity.Length));
            return new NdArray<T>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                (NdArray<T>, NdArray<T>, NdArray<T>) argsTuple,
                Expression<Func<T, T, T, T>> selector
            )
            where T : unmanaged
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, selector);

        #endregion


        #region 4 arguments input

        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                NdArray<T> ndarray1,
                NdArray<T> ndarray2,
                NdArray<T> ndarray3,
                NdArray<T> ndarray4,
                Expression<Func<T, T, T, T, T>> selector
            )
            where T : unmanaged
        {
            ReadOnlyMemory<T> getSource(NdArray<T> ndarray)
                => ndarray.Entity switch
                {
                    IBufferNdArrayImpl<T> romProvider => romProvider.Buffer,
                    _ => default(ReadOnlyMemory<T>?),
                } ?? throw new NotSupportedException();

            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T>.Shape));

            var source1 = getSource(ndarray1);
            var source2 = getSource(ndarray2);
            var source3 = getSource(ndarray3);
            var source4 = getSource(ndarray4);
            var simdSelector = VectorOperation.Simdize(selector);
            var resultEntity = new RawNdArrayImpl<T>(ndarray1.Shape);
            simdSelector(source1, source2, source3, source4, resultEntity.Buffer.Slice(0, resultEntity.Length));
            return new NdArray<T>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item4.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                (NdArray<T>, NdArray<T>, NdArray<T>, NdArray<T>) argsTuple,
                Expression<Func<T, T, T, T, T>> selector
            )
            where T : unmanaged
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, selector);

        #endregion


        #region 5 arguments input

        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                NdArray<T> ndarray1,
                NdArray<T> ndarray2,
                NdArray<T> ndarray3,
                NdArray<T> ndarray4,
                NdArray<T> ndarray5,
                Expression<Func<T, T, T, T, T, T>> selector
            )
            where T : unmanaged
        {
            ReadOnlyMemory<T> getSource(NdArray<T> ndarray)
                => ndarray.Entity switch
                {
                    IBufferNdArrayImpl<T> romProvider => romProvider.Buffer,
                    _ => default(ReadOnlyMemory<T>?),
                } ?? throw new NotSupportedException();

            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray5.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray5) + "." + nameof(INdArray<T>.Shape));

            var source1 = getSource(ndarray1);
            var source2 = getSource(ndarray2);
            var source3 = getSource(ndarray3);
            var source4 = getSource(ndarray4);
            var source5 = getSource(ndarray5);
            var simdSelector = VectorOperation.Simdize(selector);
            var resultEntity = new RawNdArrayImpl<T>(ndarray1.Shape);
            simdSelector(source1, source2, source3, source4, source5, resultEntity.Buffer.Slice(0, resultEntity.Length));
            return new NdArray<T>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item4.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item5.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                (NdArray<T>, NdArray<T>, NdArray<T>, NdArray<T>, NdArray<T>) argsTuple,
                Expression<Func<T, T, T, T, T, T>> selector
            )
            where T : unmanaged
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, argsTuple.Item5, selector);

        #endregion


        #region 6 arguments input

        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <param name="ndarray6"> [<c>ndarray1.Shape == ndarray6.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                NdArray<T> ndarray1,
                NdArray<T> ndarray2,
                NdArray<T> ndarray3,
                NdArray<T> ndarray4,
                NdArray<T> ndarray5,
                NdArray<T> ndarray6,
                Expression<Func<T, T, T, T, T, T, T>> selector
            )
            where T : unmanaged
        {
            ReadOnlyMemory<T> getSource(NdArray<T> ndarray)
                => ndarray.Entity switch
                {
                    IBufferNdArrayImpl<T> romProvider => romProvider.Buffer,
                    _ => default(ReadOnlyMemory<T>?),
                } ?? throw new NotSupportedException();

            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray5.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray5) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray6.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray6) + "." + nameof(INdArray<T>.Shape));

            var source1 = getSource(ndarray1);
            var source2 = getSource(ndarray2);
            var source3 = getSource(ndarray3);
            var source4 = getSource(ndarray4);
            var source5 = getSource(ndarray5);
            var source6 = getSource(ndarray6);
            var simdSelector = VectorOperation.Simdize(selector);
            var resultEntity = new RawNdArrayImpl<T>(ndarray1.Shape);
            simdSelector(source1, source2, source3, source4, source5, source6, resultEntity.Buffer.Slice(0, resultEntity.Length));
            return new NdArray<T>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item4.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item5.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item6.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                (NdArray<T>, NdArray<T>, NdArray<T>, NdArray<T>, NdArray<T>, NdArray<T>) argsTuple,
                Expression<Func<T, T, T, T, T, T, T>> selector
            )
            where T : unmanaged
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, argsTuple.Item5, argsTuple.Item6, selector);

        #endregion


        #region 7 arguments input

        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <param name="ndarray6"> [<c>ndarray1.Shape == ndarray6.Shape</c>] </param>
        /// <param name="ndarray7"> [<c>ndarray1.Shape == ndarray7.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                NdArray<T> ndarray1,
                NdArray<T> ndarray2,
                NdArray<T> ndarray3,
                NdArray<T> ndarray4,
                NdArray<T> ndarray5,
                NdArray<T> ndarray6,
                NdArray<T> ndarray7,
                Expression<Func<T, T, T, T, T, T, T, T>> selector
            )
            where T : unmanaged
        {
            ReadOnlyMemory<T> getSource(NdArray<T> ndarray)
                => ndarray.Entity switch
                {
                    IBufferNdArrayImpl<T> romProvider => romProvider.Buffer,
                    _ => default(ReadOnlyMemory<T>?),
                } ?? throw new NotSupportedException();

            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray5.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray5) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray6.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray6) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray7.Shape, nameof(ndarray1) + "." + nameof(INdArray<T>.Shape), nameof(ndarray7) + "." + nameof(INdArray<T>.Shape));

            var source1 = getSource(ndarray1);
            var source2 = getSource(ndarray2);
            var source3 = getSource(ndarray3);
            var source4 = getSource(ndarray4);
            var source5 = getSource(ndarray5);
            var source6 = getSource(ndarray6);
            var source7 = getSource(ndarray7);
            var simdSelector = VectorOperation.Simdize(selector);
            var resultEntity = new RawNdArrayImpl<T>(ndarray1.Shape);
            simdSelector(source1, source2, source3, source4, source5, source6, source7, resultEntity.Buffer.Slice(0, resultEntity.Length));
            return new NdArray<T>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item4.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item5.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item6.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item7.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<T> Zip<T>(this
                (NdArray<T>, NdArray<T>, NdArray<T>, NdArray<T>, NdArray<T>, NdArray<T>, NdArray<T>) argsTuple,
                Expression<Func<T, T, T, T, T, T, T, T>> selector
            )
            where T : unmanaged
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, argsTuple.Item5, argsTuple.Item6, argsTuple.Item7, selector);

        #endregion


    }
}
