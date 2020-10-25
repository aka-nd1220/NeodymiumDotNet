using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NeodymiumDotNet
{
    /// <inheritdoc cref="INdArray{T}" />
    /// <summary>
    ///     Immutable multi-dim NdArray.
    ///     This type is not compatible with <see cref="MutableNdArray{T}"/>.
    ///     Use <see cref="NdArray{T}.ToMutable"/> to get mutable NdArray from this.
    /// </summary>
    /// <typeparam name="T"> The data type. </typeparam>
    public partial class NdArray<T> : INdArrayInternal<T>, IEquatable<NdArray<T>>, IFormattable
    {

        /// <summary>
        ///     The NdArray elements entity.
        /// </summary>
        internal NdArrayImpl<T> Entity { get; }
        NdArrayImpl<T> INdArrayInternal<T>.Entity => Entity;


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
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T this[params Index[] indices]
            => this[indices.AsSpan()];


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
                Span<int> ix = stackalloc int[len];
                for(var i = 0 ; i < len ; ++i)
                {
                    ix[i] = indices[i].Map(Shape[i]);
                }

                Guard.AssertIndices(Shape, ix);
                return Entity[ix];
            }
        }


        /// <summary>
        ///     The indexer of this NdArray.
        /// </summary>
        /// <param name="indices"> [<c>indices.Length &lt;= Rank</c>] </param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public NdArray<T> this[params IndexOrRange[] indices]
        {
            get
            {
                indices = InternalUtils.FillImplicitSlices(indices, Rank);
                return new NdArray<T>(new SliceViewNdArrayImpl<T>(Entity, indices));
            }
        }

        /// <inheritdoc />
        INdArray<T> INdArray<T>.this[params IndexOrRange[] indices]
            => this[indices];

        #endregion

        #region constructors

        /// <summary>
        ///     Create new NdArray object with entity object.
        /// </summary>
        /// <param name="entity"></param>
        internal NdArray(NdArrayImpl<T> entity)
        {
            Entity = entity;
        }


        /// <summary></summary>
        /// <param name="shape"></param>
        internal NdArray(IndexArray shape)
        {
            Entity = new RawNdArrayImpl<T>(shape);
        }


        /// <summary>
        ///     Create new NdArray object.
        /// </summary>
        /// <param name="array"></param>
        internal NdArray(Array array)
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

        /// <summary>
        ///     Gets value by flatten index.
        /// </summary>
        /// <param name="flattenIndex"></param>
        /// <returns></returns>
        public T GetItem(int flattenIndex)
            => Entity[flattenIndex];


        /// <inheritdoc />
        public MutableNdArray<T> ToMutable()
        {
            var entity = new RawNdArrayImpl<T>(Shape);
            Entity.CopyTo(entity.Buffer.Span);
            return new MutableNdArray<T>(entity);
        }


        /// <inheritdoc />
        public NdArray<T> ToImmutable()
            => this;


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

        #region equality comparison methods

        /// <inheritdoc />
        /// <summary>
        ///     Returns <c>true</c> if every element of this and <paramref name="obj"/>;
        ///     otherwise <c>false</c>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
            => obj is NdArray<T> other && Equals(other);


        /// <inheritdoc />
        /// <summary>
        ///     Returns <c>true</c> if every element of this and <paramref name="other"/>;
        ///     otherwise <c>false</c>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NdArray<T> other) => Equals(this, other);


        /// <summary>
        ///     Gets hashcode of this NdArray.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => Entity.GetHashCode();


        /// <summary>
        ///     Returns <c>true</c> if every element of <paramref name="lhs"/> and <paramref name="rhs"/>;
        ///     otherwise <c>false</c>.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool Equals(NdArray<T> lhs, NdArray<T> rhs)
        {
            if(lhs.Shape != rhs.Shape)
            {
                return false;
            }

            var len = lhs.Length;
            var lEntity = lhs.Entity;
            for(var i = 0 ; i < len ; ++i)
            {
                if(!EqualityComparer<T>.Default.Equals(lEntity[i], rhs.GetItem(i)))
                    return false;
            }

            return true;
        }

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
        public IEnumerable<NdArray<T>> AsEnumerable(int axis)
        {
            var index = Enumerable
                       .Range(0, Rank)
                       .Select(_ => (IndexOrRange)Range.Whole)
                       .ToArray();
            for(var i = 0 ; i < Shape[axis] ; ++i)
            {
                index[axis] = new IndexOrRange(i);
                yield return this[index];
            }
        }


        /// <summary>
        ///     Provides an enumerator for the elements of a <see cref="NdArray{T}"/>.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private NdArrayImpl<T> _Entity;
            private int            _Position;


            /// <inheritdoc />
            public T Current => _Entity[_Position];


            /// <inheritdoc />
            object IEnumerator.Current => Current!;


            internal Enumerator(NdArray<T> owner)
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

            private readonly NdArray<T> _Owner;


            internal InternalEnumerable(NdArray<T> owner)
                => _Owner = owner;


            public IEnumerator<T> GetEnumerator()
                => _Owner.GetEnumerator();


            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();

        }

        #endregion

    }
}
