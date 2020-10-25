//
// T4 auto generated code
//
#nullable enable
using System;

namespace NeodymiumDotNet.Linq
{
    public static partial class NdLinq
    {
        #region 2 arguments input

        /// <summary>
        ///     [Pure] Zip elements of several NdArrays to tuple.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <returns></returns>
        public static NdArray<(T1, T2)> Zip<T1, T2>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));

            return new NdArray<(T1, T2)>(new ZipInTupleArrayImpl<T1, T2>(ndarray1.Entity, ndarray2.Entity));
        }


        internal sealed class ZipInTupleArrayImpl<T1, T2>: NdArrayImpl<(T1, T2)>
        {
            private readonly NdArrayImpl<T1> _Src1;
            private readonly NdArrayImpl<T2> _Src2;

            protected override (T1, T2) GetItem(int flattenIndex)
                => (_Src1[flattenIndex], _Src2[flattenIndex]);

            protected override (T1, T2) GetItem(ReadOnlySpan<int> shapedIndices)
                => (_Src1[shapedIndices], _Src2[shapedIndices]);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ndarray1"></param>
            /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
            internal ZipInTupleArrayImpl(NdArrayImpl<T1> ndarray1, NdArrayImpl<T2> ndarray2)
                : base(ndarray1.Shape)
            {
                _Src1 = ndarray1;
                _Src2 = ndarray2;
            }
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                Func<T1, T2, TResult> selector
            )
            => Zip(ndarray1, ndarray2, selector, default);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                Func<T1, T2, TResult> selector,
                IIterationStrategy? strategy = default
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));
            Guard.AssertArgumentNotNull(selector, nameof(selector));

            var len = ndarray1.Length;
            var resultEntity = new RawNdArrayImpl<TResult>(ndarray1.Shape);
            var array = resultEntity.Buffer;
            var entity1 = ndarray1.Entity is RawNdArrayImpl<T1> raw1 ? raw1.Buffer : ndarray1.Entity.ToArray();
            var entity2 = ndarray2.Entity is RawNdArrayImpl<T2> raw2 ? raw2.Buffer : ndarray2.Entity.ToArray();
            if(strategy is null || strategy is IterationStrategy)
            {
                for(var i = 0; i < len; ++i)
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i]);
            }
            else
            {
                strategy.For(0, len, i =>
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i]));
            }
            return new NdArray<TResult>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, TResult>(this
                (NdArray<T1>, NdArray<T2>) argsTuple,
                Func<T1, T2, TResult> selector
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, selector);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, TResult>(this
                (NdArray<T1>, NdArray<T2>) argsTuple,
                Func<T1, T2, TResult> selector,
                IIterationStrategy? strategy = default
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, selector, strategy);

        #endregion

        #region 3 arguments input

        /// <summary>
        ///     [Pure] Zip elements of several NdArrays to tuple.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <returns></returns>
        public static NdArray<(T1, T2, T3)> Zip<T1, T2, T3>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T3>.Shape));

            return new NdArray<(T1, T2, T3)>(new ZipInTupleArrayImpl<T1, T2, T3>(ndarray1.Entity, ndarray2.Entity, ndarray3.Entity));
        }


        internal sealed class ZipInTupleArrayImpl<T1, T2, T3>: NdArrayImpl<(T1, T2, T3)>
        {
            private readonly NdArrayImpl<T1> _Src1;
            private readonly NdArrayImpl<T2> _Src2;
            private readonly NdArrayImpl<T3> _Src3;

            protected override (T1, T2, T3) GetItem(int flattenIndex)
                => (_Src1[flattenIndex], _Src2[flattenIndex], _Src3[flattenIndex]);

            protected override (T1, T2, T3) GetItem(ReadOnlySpan<int> shapedIndices)
                => (_Src1[shapedIndices], _Src2[shapedIndices], _Src3[shapedIndices]);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ndarray1"></param>
            /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
            /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
            internal ZipInTupleArrayImpl(NdArrayImpl<T1> ndarray1, NdArrayImpl<T2> ndarray2, NdArrayImpl<T3> ndarray3)
                : base(ndarray1.Shape)
            {
                _Src1 = ndarray1;
                _Src2 = ndarray2;
                _Src3 = ndarray3;
            }
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                Func<T1, T2, T3, TResult> selector
            )
            => Zip(ndarray1, ndarray2, ndarray3, selector, default);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                Func<T1, T2, T3, TResult> selector,
                IIterationStrategy? strategy = default
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T3>.Shape));
            Guard.AssertArgumentNotNull(selector, nameof(selector));

            var len = ndarray1.Length;
            var resultEntity = new RawNdArrayImpl<TResult>(ndarray1.Shape);
            var array = resultEntity.Buffer;
            var entity1 = ndarray1.Entity is RawNdArrayImpl<T1> raw1 ? raw1.Buffer : ndarray1.Entity.ToArray();
            var entity2 = ndarray2.Entity is RawNdArrayImpl<T2> raw2 ? raw2.Buffer : ndarray2.Entity.ToArray();
            var entity3 = ndarray3.Entity is RawNdArrayImpl<T3> raw3 ? raw3.Buffer : ndarray3.Entity.ToArray();
            if(strategy is null || strategy is IterationStrategy)
            {
                for(var i = 0; i < len; ++i)
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i], entity3.Span[i]);
            }
            else
            {
                strategy.For(0, len, i =>
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i], entity3.Span[i]));
            }
            return new NdArray<TResult>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, TResult>(this
                (NdArray<T1>, NdArray<T2>, NdArray<T3>) argsTuple,
                Func<T1, T2, T3, TResult> selector
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, selector);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, TResult>(this
                (NdArray<T1>, NdArray<T2>, NdArray<T3>) argsTuple,
                Func<T1, T2, T3, TResult> selector,
                IIterationStrategy? strategy = default
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, selector, strategy);

        #endregion

        #region 4 arguments input

        /// <summary>
        ///     [Pure] Zip elements of several NdArrays to tuple.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <returns></returns>
        public static NdArray<(T1, T2, T3, T4)> Zip<T1, T2, T3, T4>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T3>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T4>.Shape));

            return new NdArray<(T1, T2, T3, T4)>(new ZipInTupleArrayImpl<T1, T2, T3, T4>(ndarray1.Entity, ndarray2.Entity, ndarray3.Entity, ndarray4.Entity));
        }


        internal sealed class ZipInTupleArrayImpl<T1, T2, T3, T4>: NdArrayImpl<(T1, T2, T3, T4)>
        {
            private readonly NdArrayImpl<T1> _Src1;
            private readonly NdArrayImpl<T2> _Src2;
            private readonly NdArrayImpl<T3> _Src3;
            private readonly NdArrayImpl<T4> _Src4;

            protected override (T1, T2, T3, T4) GetItem(int flattenIndex)
                => (_Src1[flattenIndex], _Src2[flattenIndex], _Src3[flattenIndex], _Src4[flattenIndex]);

            protected override (T1, T2, T3, T4) GetItem(ReadOnlySpan<int> shapedIndices)
                => (_Src1[shapedIndices], _Src2[shapedIndices], _Src3[shapedIndices], _Src4[shapedIndices]);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ndarray1"></param>
            /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
            /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
            /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
            internal ZipInTupleArrayImpl(NdArrayImpl<T1> ndarray1, NdArrayImpl<T2> ndarray2, NdArrayImpl<T3> ndarray3, NdArrayImpl<T4> ndarray4)
                : base(ndarray1.Shape)
            {
                _Src1 = ndarray1;
                _Src2 = ndarray2;
                _Src3 = ndarray3;
                _Src4 = ndarray4;
            }
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4,
                Func<T1, T2, T3, T4, TResult> selector
            )
            => Zip(ndarray1, ndarray2, ndarray3, ndarray4, selector, default);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4,
                Func<T1, T2, T3, T4, TResult> selector,
                IIterationStrategy? strategy = default
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T3>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T4>.Shape));
            Guard.AssertArgumentNotNull(selector, nameof(selector));

            var len = ndarray1.Length;
            var resultEntity = new RawNdArrayImpl<TResult>(ndarray1.Shape);
            var array = resultEntity.Buffer;
            var entity1 = ndarray1.Entity is RawNdArrayImpl<T1> raw1 ? raw1.Buffer : ndarray1.Entity.ToArray();
            var entity2 = ndarray2.Entity is RawNdArrayImpl<T2> raw2 ? raw2.Buffer : ndarray2.Entity.ToArray();
            var entity3 = ndarray3.Entity is RawNdArrayImpl<T3> raw3 ? raw3.Buffer : ndarray3.Entity.ToArray();
            var entity4 = ndarray4.Entity is RawNdArrayImpl<T4> raw4 ? raw4.Buffer : ndarray4.Entity.ToArray();
            if(strategy is null || strategy is IterationStrategy)
            {
                for(var i = 0; i < len; ++i)
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i], entity3.Span[i], entity4.Span[i]);
            }
            else
            {
                strategy.For(0, len, i =>
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i], entity3.Span[i], entity4.Span[i]));
            }
            return new NdArray<TResult>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item4.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, TResult>(this
                (NdArray<T1>, NdArray<T2>, NdArray<T3>, NdArray<T4>) argsTuple,
                Func<T1, T2, T3, T4, TResult> selector
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, selector);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item4.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, TResult>(this
                (NdArray<T1>, NdArray<T2>, NdArray<T3>, NdArray<T4>) argsTuple,
                Func<T1, T2, T3, T4, TResult> selector,
                IIterationStrategy? strategy = default
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, selector, strategy);

        #endregion

        #region 5 arguments input

        /// <summary>
        ///     [Pure] Zip elements of several NdArrays to tuple.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <returns></returns>
        public static NdArray<(T1, T2, T3, T4, T5)> Zip<T1, T2, T3, T4, T5>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4,
                NdArray<T5> ndarray5
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T3>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T4>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray5.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray5) + "." + nameof(INdArray<T5>.Shape));

            return new NdArray<(T1, T2, T3, T4, T5)>(new ZipInTupleArrayImpl<T1, T2, T3, T4, T5>(ndarray1.Entity, ndarray2.Entity, ndarray3.Entity, ndarray4.Entity, ndarray5.Entity));
        }


        internal sealed class ZipInTupleArrayImpl<T1, T2, T3, T4, T5>: NdArrayImpl<(T1, T2, T3, T4, T5)>
        {
            private readonly NdArrayImpl<T1> _Src1;
            private readonly NdArrayImpl<T2> _Src2;
            private readonly NdArrayImpl<T3> _Src3;
            private readonly NdArrayImpl<T4> _Src4;
            private readonly NdArrayImpl<T5> _Src5;

            protected override (T1, T2, T3, T4, T5) GetItem(int flattenIndex)
                => (_Src1[flattenIndex], _Src2[flattenIndex], _Src3[flattenIndex], _Src4[flattenIndex], _Src5[flattenIndex]);

            protected override (T1, T2, T3, T4, T5) GetItem(ReadOnlySpan<int> shapedIndices)
                => (_Src1[shapedIndices], _Src2[shapedIndices], _Src3[shapedIndices], _Src4[shapedIndices], _Src5[shapedIndices]);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ndarray1"></param>
            /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
            /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
            /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
            /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
            internal ZipInTupleArrayImpl(NdArrayImpl<T1> ndarray1, NdArrayImpl<T2> ndarray2, NdArrayImpl<T3> ndarray3, NdArrayImpl<T4> ndarray4, NdArrayImpl<T5> ndarray5)
                : base(ndarray1.Shape)
            {
                _Src1 = ndarray1;
                _Src2 = ndarray2;
                _Src3 = ndarray3;
                _Src4 = ndarray4;
                _Src5 = ndarray5;
            }
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4,
                NdArray<T5> ndarray5,
                Func<T1, T2, T3, T4, T5, TResult> selector
            )
            => Zip(ndarray1, ndarray2, ndarray3, ndarray4, ndarray5, selector, default);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4,
                NdArray<T5> ndarray5,
                Func<T1, T2, T3, T4, T5, TResult> selector,
                IIterationStrategy? strategy = default
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T3>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T4>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray5.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray5) + "." + nameof(INdArray<T5>.Shape));
            Guard.AssertArgumentNotNull(selector, nameof(selector));

            var len = ndarray1.Length;
            var resultEntity = new RawNdArrayImpl<TResult>(ndarray1.Shape);
            var array = resultEntity.Buffer;
            var entity1 = ndarray1.Entity is RawNdArrayImpl<T1> raw1 ? raw1.Buffer : ndarray1.Entity.ToArray();
            var entity2 = ndarray2.Entity is RawNdArrayImpl<T2> raw2 ? raw2.Buffer : ndarray2.Entity.ToArray();
            var entity3 = ndarray3.Entity is RawNdArrayImpl<T3> raw3 ? raw3.Buffer : ndarray3.Entity.ToArray();
            var entity4 = ndarray4.Entity is RawNdArrayImpl<T4> raw4 ? raw4.Buffer : ndarray4.Entity.ToArray();
            var entity5 = ndarray5.Entity is RawNdArrayImpl<T5> raw5 ? raw5.Buffer : ndarray5.Entity.ToArray();
            if(strategy is null || strategy is IterationStrategy)
            {
                for(var i = 0; i < len; ++i)
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i], entity3.Span[i], entity4.Span[i], entity5.Span[i]);
            }
            else
            {
                strategy.For(0, len, i =>
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i], entity3.Span[i], entity4.Span[i], entity5.Span[i]));
            }
            return new NdArray<TResult>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item4.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item5.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, TResult>(this
                (NdArray<T1>, NdArray<T2>, NdArray<T3>, NdArray<T4>, NdArray<T5>) argsTuple,
                Func<T1, T2, T3, T4, T5, TResult> selector
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, argsTuple.Item5, selector);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item4.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item5.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, TResult>(this
                (NdArray<T1>, NdArray<T2>, NdArray<T3>, NdArray<T4>, NdArray<T5>) argsTuple,
                Func<T1, T2, T3, T4, T5, TResult> selector,
                IIterationStrategy? strategy = default
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, argsTuple.Item5, selector, strategy);

        #endregion

        #region 6 arguments input

        /// <summary>
        ///     [Pure] Zip elements of several NdArrays to tuple.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <param name="ndarray6"> [<c>ndarray1.Shape == ndarray6.Shape</c>] </param>
        /// <returns></returns>
        public static NdArray<(T1, T2, T3, T4, T5, T6)> Zip<T1, T2, T3, T4, T5, T6>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4,
                NdArray<T5> ndarray5,
                NdArray<T6> ndarray6
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T3>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T4>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray5.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray5) + "." + nameof(INdArray<T5>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray6.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray6) + "." + nameof(INdArray<T6>.Shape));

            return new NdArray<(T1, T2, T3, T4, T5, T6)>(new ZipInTupleArrayImpl<T1, T2, T3, T4, T5, T6>(ndarray1.Entity, ndarray2.Entity, ndarray3.Entity, ndarray4.Entity, ndarray5.Entity, ndarray6.Entity));
        }


        internal sealed class ZipInTupleArrayImpl<T1, T2, T3, T4, T5, T6>: NdArrayImpl<(T1, T2, T3, T4, T5, T6)>
        {
            private readonly NdArrayImpl<T1> _Src1;
            private readonly NdArrayImpl<T2> _Src2;
            private readonly NdArrayImpl<T3> _Src3;
            private readonly NdArrayImpl<T4> _Src4;
            private readonly NdArrayImpl<T5> _Src5;
            private readonly NdArrayImpl<T6> _Src6;

            protected override (T1, T2, T3, T4, T5, T6) GetItem(int flattenIndex)
                => (_Src1[flattenIndex], _Src2[flattenIndex], _Src3[flattenIndex], _Src4[flattenIndex], _Src5[flattenIndex], _Src6[flattenIndex]);

            protected override (T1, T2, T3, T4, T5, T6) GetItem(ReadOnlySpan<int> shapedIndices)
                => (_Src1[shapedIndices], _Src2[shapedIndices], _Src3[shapedIndices], _Src4[shapedIndices], _Src5[shapedIndices], _Src6[shapedIndices]);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ndarray1"></param>
            /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
            /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
            /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
            /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
            /// <param name="ndarray6"> [<c>ndarray1.Shape == ndarray6.Shape</c>] </param>
            internal ZipInTupleArrayImpl(NdArrayImpl<T1> ndarray1, NdArrayImpl<T2> ndarray2, NdArrayImpl<T3> ndarray3, NdArrayImpl<T4> ndarray4, NdArrayImpl<T5> ndarray5, NdArrayImpl<T6> ndarray6)
                : base(ndarray1.Shape)
            {
                _Src1 = ndarray1;
                _Src2 = ndarray2;
                _Src3 = ndarray3;
                _Src4 = ndarray4;
                _Src5 = ndarray5;
                _Src6 = ndarray6;
            }
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <param name="ndarray6"> [<c>ndarray1.Shape == ndarray6.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, T6, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4,
                NdArray<T5> ndarray5,
                NdArray<T6> ndarray6,
                Func<T1, T2, T3, T4, T5, T6, TResult> selector
            )
            => Zip(ndarray1, ndarray2, ndarray3, ndarray4, ndarray5, ndarray6, selector, default);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <param name="ndarray6"> [<c>ndarray1.Shape == ndarray6.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, T6, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4,
                NdArray<T5> ndarray5,
                NdArray<T6> ndarray6,
                Func<T1, T2, T3, T4, T5, T6, TResult> selector,
                IIterationStrategy? strategy = default
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T3>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T4>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray5.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray5) + "." + nameof(INdArray<T5>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray6.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray6) + "." + nameof(INdArray<T6>.Shape));
            Guard.AssertArgumentNotNull(selector, nameof(selector));

            var len = ndarray1.Length;
            var resultEntity = new RawNdArrayImpl<TResult>(ndarray1.Shape);
            var array = resultEntity.Buffer;
            var entity1 = ndarray1.Entity is RawNdArrayImpl<T1> raw1 ? raw1.Buffer : ndarray1.Entity.ToArray();
            var entity2 = ndarray2.Entity is RawNdArrayImpl<T2> raw2 ? raw2.Buffer : ndarray2.Entity.ToArray();
            var entity3 = ndarray3.Entity is RawNdArrayImpl<T3> raw3 ? raw3.Buffer : ndarray3.Entity.ToArray();
            var entity4 = ndarray4.Entity is RawNdArrayImpl<T4> raw4 ? raw4.Buffer : ndarray4.Entity.ToArray();
            var entity5 = ndarray5.Entity is RawNdArrayImpl<T5> raw5 ? raw5.Buffer : ndarray5.Entity.ToArray();
            var entity6 = ndarray6.Entity is RawNdArrayImpl<T6> raw6 ? raw6.Buffer : ndarray6.Entity.ToArray();
            if(strategy is null || strategy is IterationStrategy)
            {
                for(var i = 0; i < len; ++i)
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i], entity3.Span[i], entity4.Span[i], entity5.Span[i], entity6.Span[i]);
            }
            else
            {
                strategy.For(0, len, i =>
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i], entity3.Span[i], entity4.Span[i], entity5.Span[i], entity6.Span[i]));
            }
            return new NdArray<TResult>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="TResult"></typeparam>
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
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, T6, TResult>(this
                (NdArray<T1>, NdArray<T2>, NdArray<T3>, NdArray<T4>, NdArray<T5>, NdArray<T6>) argsTuple,
                Func<T1, T2, T3, T4, T5, T6, TResult> selector
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, argsTuple.Item5, argsTuple.Item6, selector);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="argsTuple">
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item2.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item3.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item4.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item5.Shape</c>]</para>
        ///     <para>[<c>argsTuple.Item1.Shape == argsTuple.Item6.Shape</c>]</para>
        ///     <para>The tuple of <see cref="NdArray{T}"/> for selector. </para>
        /// </param>
        /// <param name="selector"></param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, T6, TResult>(this
                (NdArray<T1>, NdArray<T2>, NdArray<T3>, NdArray<T4>, NdArray<T5>, NdArray<T6>) argsTuple,
                Func<T1, T2, T3, T4, T5, T6, TResult> selector,
                IIterationStrategy? strategy = default
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, argsTuple.Item5, argsTuple.Item6, selector, strategy);

        #endregion

        #region 7 arguments input

        /// <summary>
        ///     [Pure] Zip elements of several NdArrays to tuple.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <param name="ndarray6"> [<c>ndarray1.Shape == ndarray6.Shape</c>] </param>
        /// <param name="ndarray7"> [<c>ndarray1.Shape == ndarray7.Shape</c>] </param>
        /// <returns></returns>
        public static NdArray<(T1, T2, T3, T4, T5, T6, T7)> Zip<T1, T2, T3, T4, T5, T6, T7>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4,
                NdArray<T5> ndarray5,
                NdArray<T6> ndarray6,
                NdArray<T7> ndarray7
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T3>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T4>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray5.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray5) + "." + nameof(INdArray<T5>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray6.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray6) + "." + nameof(INdArray<T6>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray7.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray7) + "." + nameof(INdArray<T7>.Shape));

            return new NdArray<(T1, T2, T3, T4, T5, T6, T7)>(new ZipInTupleArrayImpl<T1, T2, T3, T4, T5, T6, T7>(ndarray1.Entity, ndarray2.Entity, ndarray3.Entity, ndarray4.Entity, ndarray5.Entity, ndarray6.Entity, ndarray7.Entity));
        }


        internal sealed class ZipInTupleArrayImpl<T1, T2, T3, T4, T5, T6, T7>: NdArrayImpl<(T1, T2, T3, T4, T5, T6, T7)>
        {
            private readonly NdArrayImpl<T1> _Src1;
            private readonly NdArrayImpl<T2> _Src2;
            private readonly NdArrayImpl<T3> _Src3;
            private readonly NdArrayImpl<T4> _Src4;
            private readonly NdArrayImpl<T5> _Src5;
            private readonly NdArrayImpl<T6> _Src6;
            private readonly NdArrayImpl<T7> _Src7;

            protected override (T1, T2, T3, T4, T5, T6, T7) GetItem(int flattenIndex)
                => (_Src1[flattenIndex], _Src2[flattenIndex], _Src3[flattenIndex], _Src4[flattenIndex], _Src5[flattenIndex], _Src6[flattenIndex], _Src7[flattenIndex]);

            protected override (T1, T2, T3, T4, T5, T6, T7) GetItem(ReadOnlySpan<int> shapedIndices)
                => (_Src1[shapedIndices], _Src2[shapedIndices], _Src3[shapedIndices], _Src4[shapedIndices], _Src5[shapedIndices], _Src6[shapedIndices], _Src7[shapedIndices]);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ndarray1"></param>
            /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
            /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
            /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
            /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
            /// <param name="ndarray6"> [<c>ndarray1.Shape == ndarray6.Shape</c>] </param>
            /// <param name="ndarray7"> [<c>ndarray1.Shape == ndarray7.Shape</c>] </param>
            internal ZipInTupleArrayImpl(NdArrayImpl<T1> ndarray1, NdArrayImpl<T2> ndarray2, NdArrayImpl<T3> ndarray3, NdArrayImpl<T4> ndarray4, NdArrayImpl<T5> ndarray5, NdArrayImpl<T6> ndarray6, NdArrayImpl<T7> ndarray7)
                : base(ndarray1.Shape)
            {
                _Src1 = ndarray1;
                _Src2 = ndarray2;
                _Src3 = ndarray3;
                _Src4 = ndarray4;
                _Src5 = ndarray5;
                _Src6 = ndarray6;
                _Src7 = ndarray7;
            }
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <param name="ndarray6"> [<c>ndarray1.Shape == ndarray6.Shape</c>] </param>
        /// <param name="ndarray7"> [<c>ndarray1.Shape == ndarray7.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4,
                NdArray<T5> ndarray5,
                NdArray<T6> ndarray6,
                NdArray<T7> ndarray7,
                Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector
            )
            => Zip(ndarray1, ndarray2, ndarray3, ndarray4, ndarray5, ndarray6, ndarray7, selector, default);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ndarray1"></param>
        /// <param name="ndarray2"> [<c>ndarray1.Shape == ndarray2.Shape</c>] </param>
        /// <param name="ndarray3"> [<c>ndarray1.Shape == ndarray3.Shape</c>] </param>
        /// <param name="ndarray4"> [<c>ndarray1.Shape == ndarray4.Shape</c>] </param>
        /// <param name="ndarray5"> [<c>ndarray1.Shape == ndarray5.Shape</c>] </param>
        /// <param name="ndarray6"> [<c>ndarray1.Shape == ndarray6.Shape</c>] </param>
        /// <param name="ndarray7"> [<c>ndarray1.Shape == ndarray7.Shape</c>] </param>
        /// <param name="selector"></param>
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, TResult>(this
                NdArray<T1> ndarray1,
                NdArray<T2> ndarray2,
                NdArray<T3> ndarray3,
                NdArray<T4> ndarray4,
                NdArray<T5> ndarray5,
                NdArray<T6> ndarray6,
                NdArray<T7> ndarray7,
                Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector,
                IIterationStrategy? strategy = default
            )
        {
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray2.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray2) + "." + nameof(INdArray<T2>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray3.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray3) + "." + nameof(INdArray<T3>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray4.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray4) + "." + nameof(INdArray<T4>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray5.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray5) + "." + nameof(INdArray<T5>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray6.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray6) + "." + nameof(INdArray<T6>.Shape));
            Guard.AssertShapeMatch(ndarray1.Shape, ndarray7.Shape, nameof(ndarray1) + "." + nameof(INdArray<T1>.Shape), nameof(ndarray7) + "." + nameof(INdArray<T7>.Shape));
            Guard.AssertArgumentNotNull(selector, nameof(selector));

            var len = ndarray1.Length;
            var resultEntity = new RawNdArrayImpl<TResult>(ndarray1.Shape);
            var array = resultEntity.Buffer;
            var entity1 = ndarray1.Entity is RawNdArrayImpl<T1> raw1 ? raw1.Buffer : ndarray1.Entity.ToArray();
            var entity2 = ndarray2.Entity is RawNdArrayImpl<T2> raw2 ? raw2.Buffer : ndarray2.Entity.ToArray();
            var entity3 = ndarray3.Entity is RawNdArrayImpl<T3> raw3 ? raw3.Buffer : ndarray3.Entity.ToArray();
            var entity4 = ndarray4.Entity is RawNdArrayImpl<T4> raw4 ? raw4.Buffer : ndarray4.Entity.ToArray();
            var entity5 = ndarray5.Entity is RawNdArrayImpl<T5> raw5 ? raw5.Buffer : ndarray5.Entity.ToArray();
            var entity6 = ndarray6.Entity is RawNdArrayImpl<T6> raw6 ? raw6.Buffer : ndarray6.Entity.ToArray();
            var entity7 = ndarray7.Entity is RawNdArrayImpl<T7> raw7 ? raw7.Buffer : ndarray7.Entity.ToArray();
            if(strategy is null || strategy is IterationStrategy)
            {
                for(var i = 0; i < len; ++i)
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i], entity3.Span[i], entity4.Span[i], entity5.Span[i], entity6.Span[i], entity7.Span[i]);
            }
            else
            {
                strategy.For(0, len, i =>
                    array.Span[i] = selector(entity1.Span[i], entity2.Span[i], entity3.Span[i], entity4.Span[i], entity5.Span[i], entity6.Span[i], entity7.Span[i]));
            }
            return new NdArray<TResult>(resultEntity);
        }


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="TResult"></typeparam>
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
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, TResult>(this
                (NdArray<T1>, NdArray<T2>, NdArray<T3>, NdArray<T4>, NdArray<T5>, NdArray<T6>, NdArray<T7>) argsTuple,
                Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, argsTuple.Item5, argsTuple.Item6, argsTuple.Item7, selector);


        /// <summary>
        ///     [Pure] Applies selector against each corresponding element set of input NdArrays.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="TResult"></typeparam>
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
        /// <param name="strategy">
        ///     [nullable] A strategy object to iterate calculation for each element.
        ///     <c>null</c> means to use <see cref="IterationStrategy.Default"/>.
        /// </param>
        /// <returns></returns>
        public static NdArray<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, TResult>(this
                (NdArray<T1>, NdArray<T2>, NdArray<T3>, NdArray<T4>, NdArray<T5>, NdArray<T6>, NdArray<T7>) argsTuple,
                Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector,
                IIterationStrategy? strategy = default
            )
            => Zip(argsTuple.Item1, argsTuple.Item2, argsTuple.Item3, argsTuple.Item4, argsTuple.Item5, argsTuple.Item6, argsTuple.Item7, selector, strategy);

        #endregion

    }
}
