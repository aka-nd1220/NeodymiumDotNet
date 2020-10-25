using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Mutable multi-dim NdArray.
    ///     This type is not compatible with <see cref="NdArray{T}"/>.
    ///     Use <see cref="MutableNdArray{T}.ToImmutable"/> or <see cref="MutableNdArray{T}.MoveToImmutable"/> to get immutable NdArray from this.
    /// </summary>
    /// <typeparam name="T"> The data type. </typeparam>
    public class MutableNdArray<T> : INdArrayInternal<T>
    {

        /// <summary>
        ///     The NdArray elements entity.
        /// </summary>
        internal MutableNdArrayImpl<T> Entity { get; private set; }
        NdArrayImpl<T> INdArrayInternal<T>.Entity => Entity;


        /// <summary>
        ///     <c>true</c> if this instance is in available status; otherwise false.
        /// </summary>
        public bool IsAlive => Entity is null;


        #region NdArray basic properties

        /// <summary>
        ///     The rank of this NdArray.
        /// </summary>
        public int Rank => Entity.Rank;


        /// <summary>
        ///     The shape of this NdArray.
        /// </summary>
        public IndexArray Shape => Entity.Shape;


        /// <summary>
        ///     The number of all elements.
        /// </summary>
        public int Length => Entity.Length;


        /// <summary>
        ///     The indexer of this NdArray.
        /// </summary>
        /// <param name="indices"></param>
        /// <returns></returns>
        public T this[params int[] indices]
        {
            get
            {
                Guard.AssertIndices(Shape, indices);
                return Entity[indices];
            }
            set
            {
                Guard.AssertIndices(Shape, indices);
                Entity[indices] = value;
            }
        }


        /// <inheritdoc />
        T INdArray<T>.this[params Index[] indices] => this[indices];


        /// <summary>
        ///     The indexer of this NdArray.
        /// </summary>
        /// <param name="indices"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T this[ReadOnlySpan<Index> indices]
        {
            get
            {
                Guard.AssertArgumentRange(indices.Length == Rank, "invalid indices count");

                var len = indices.Length;
                var ix = new int[len];
                for(var i = 0; i < len; ++i)
                {
                    ix[i] = indices[i].Map(Shape[i]);
                }

                Guard.AssertIndices(Shape, ix);
                return Entity[ix];
            }
            set
            {
                Guard.AssertArgumentRange(indices.Length == Rank, "invalid indices count");

                var len = indices.Length;
                var ix = new int[len];
                for(var i = 0; i < len; ++i)
                {
                    ix[i] = indices[i].Map(Shape[i]);
                }

                Guard.AssertIndices(Shape, ix);
                Entity[ix] = value;
            }
        }


        /// <inheritdoc />
        T INdArray<T>.this[ReadOnlySpan<Index> indices] => this[indices];


        /// <summary>
        ///     The indexer of this NdArray.
        /// </summary>
        /// <param name="indices"> [<c>indices.Length &lt;= Rank</c>] </param>
        public MutableNdArray<T> this[params IndexOrRange[] indices]
        {
            get
            {
                Guard.AssertIndices(Shape, indices);

                indices = InternalUtils.FillImplicitSlices(indices, Rank);
                return new
                    MutableNdArray<T>(new MutableSliceViewNdArrayImpl<T>(Entity, indices));
            }
            set
            {
                var sliced = this[indices];
                var len = sliced.Entity.Length;
                for(var i = 0 ; i < len ; ++i)
                {
                    sliced.Entity[i] = value.Entity[i];
                }
            }
        }


        /// <inheritdoc />
        INdArray<T> INdArray<T>.this[params IndexOrRange[] indices]
            => this[indices];

        #endregion


        #region constructors

        /// <summary>
        ///     Creates new NdArray object with entity object.
        /// </summary>
        /// <param name="entity"></param>
        internal MutableNdArray(MutableNdArrayImpl<T> entity)
        {
            Entity = entity;
        }


        /// <summary>
        ///     Creates new NdArray object with values and shapes.
        /// </summary>
        /// <param name="shape"> [<c>shape.Product() == array.Length</c>] </param>
        internal MutableNdArray(IndexArray shape)
        {
            Entity = new RawNdArrayImpl<T>(shape);
        }


        /// <summary>
        ///     Creates new NdArray object.
        /// </summary>
        /// <param name="array"></param>
        internal MutableNdArray(Array array)
        {
            var shape = Enumerable
                       .Range(0, array.Rank)
                       .Select(array.GetLength)
                       .ToIndexArray();
            var entity = new RawNdArrayImpl<T>(shape);
            foreach(var (value, i) in array.Cast<T>().Select((x, i) => (x, i)))
                entity.Buffer.Span[i] = value;
            Entity = entity;
        }

        #endregion


        #region array element access methods

        /// <inheritdoc />
        /// <summary>
        ///     Gets value by flatten index.
        /// </summary>
        /// <param name="flattenIndex"></param>
        /// <returns></returns>
        public T GetItem(int flattenIndex)
            => Entity[flattenIndex];


        /// <summary>
        ///     Sets value by flatten index.
        /// </summary>
        /// <param name="flattenIndex"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetItem(int flattenIndex, T value)
            => Entity[flattenIndex] = value;


        /// <inheritdoc />
        public MutableNdArray<T> ToMutable()
        {
            var entity = new RawNdArrayImpl<T>(Shape);
            Entity.CopyTo(entity.Buffer.Span);
            return new MutableNdArray<T>(entity);
        }


        /// <inheritdoc />
        public NdArray<T> ToImmutable()
        {
            var entity = new RawNdArrayImpl<T>(Shape);
            Entity.CopyTo(entity.Buffer.Span);
            return new NdArray<T>(entity);
        }


        /// <summary>
        ///     Moves NdArray entity to new immutable NdArray.
        /// </summary>
        /// <remarks> This method will destroy this NdArray instance. </remarks>
        /// <returns></returns>
        public NdArray<T> MoveToImmutable()
        {
            var entity = Entity;
            Entity = null!;
            return new NdArray<T>(entity);
        }


        /// <summary>
        ///     [Pure] Calculate the index of NdArray flatten sequence from the shaped indices.
        /// </summary>
        /// <param name="shapedIndices"> [<c>shapedIndices.Length == shape.Length</c>] </param>
        /// <returns></returns>
        public int ToFlattenIndex(ReadOnlySpan<int> shapedIndices)
            => Entity.ToFlattenIndex(shapedIndices);


        /// <summary>
        ///     [Pure] Calculate the shaped indices from the index of NdArray flatten sequence.
        /// </summary>
        /// <param name="flattenIndex"></param>
        /// <returns></returns>
        public IndexArray ToShapedIndices(int flattenIndex)
            => Entity.ToShapedIndices(flattenIndex);

        #endregion


        #region ToString

        /// <summary>
        ///     Returns a string that represents this <see cref="NdArray{T}"/> instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ToString("", CultureInfo.CurrentCulture);


        /// <summary>
        ///     Returns a string that represents this <see cref="NdArray{T}"/> instance.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
            => ToString(format, CultureInfo.CurrentCulture);


        /// <summary>
        ///     Returns a string that represents this <see cref="NdArray{T}"/> instance.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
            => NdArrayFormatter<T>.Default.Format(format, this, formatProvider);

        #endregion


        #region IEnumerable implementation

        /// <summary>
        ///     Returns an enumerator that iterates through the <see cref="NdArray{T}"/>.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
            => new Enumerator(this);


        /// <summary>
        ///     [Pure] Gets enumerable object of this.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> AsEnumerable()
            => new InternalEnumerable(this);


        /// <summary>
        ///     [Pure] Gets enumerable object of this.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MutableNdArray<T>> AsEnumerable(int axis)
        {
            var index = Enumerable
                       .Range(0, Rank)
                       .Select(_ => (IndexOrRange)Range.Whole)
                       .ToArray();
            for(var i = 0; i < Shape[axis]; ++i)
            {
                index[axis] = new IndexOrRange(i);
                yield return this[index];
            }
        }


        /// <summary>
        ///     Provides an enumerator for the element of a <see cref="MutableNdArray{T}"/>.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private NdArrayImpl<T> _Entity;
            private int _Position;


            /// <inheritdoc />
            public T Current => _Entity[_Position];


            /// <inheritdoc />
            object IEnumerator.Current => Current!;


            internal Enumerator(MutableNdArray<T> owner)
            {
                _Entity = owner.Entity;
                _Position = -1;
            }


            /// <inheritdoc />
            public bool MoveNext()
            {
                ++_Position;
                return _Position < _Entity.Length;
            }


            /// <inheritdoc />
            public void Reset()
            {
                _Position = -1;
            }


            /// <inheritdoc />
            public void Dispose()
            {
                _Entity = null!;
            }
        }


        private class InternalEnumerable : IEnumerable<T>
        {
            private readonly MutableNdArray<T> _Owner;


            internal InternalEnumerable(MutableNdArray<T> owner)
                => _Owner = owner;


            public IEnumerator<T> GetEnumerator()
                => _Owner.GetEnumerator();


            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }

        #endregion
    }
}
