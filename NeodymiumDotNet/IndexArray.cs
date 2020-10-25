using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Immutable array specialized to indices expression.
    /// </summary>
    public readonly struct IndexArray : IReadOnlyList<int>, IEquatable<IndexArray>
    {

        private readonly int[] _array;

        private readonly int _hashCode;

        /// <summary>
        ///     Gets the element count of this instance.
        /// </summary>
        int IReadOnlyCollection<int>.Count => _array?.Length ?? 0;

        /// <summary>
        ///     Gets the array length of this instance.
        /// </summary>
        public int Length => _array?.Length ?? 0;

        /// <summary>
        ///     Gets the total length of N-dimensional array which has a shape represented by this instance.
        /// </summary>
        public int TotalLength { get; }

        /// <summary>
        ///     Gets the index value of this instance.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int this[int index] => _array[index];


        /// <summary>
        ///     Initialize a new <see cref="IndexArray"/> instance.
        /// </summary>
        /// <param name="array"></param>
        public IndexArray(ReadOnlySpan<int> array)
            : this(array.ToArray())
        {
        }


        /// <summary>
        ///     Gets the total length of N-dimentional array which has a shape represented by this instance.
        /// </summary>
        /// <param name="array"></param>
        public IndexArray(params int[] array)
        {
            _hashCode = array.Aggregate((x, y) => ((x << 17) | (x >> 15)) ^ y);
            TotalLength = array.Aggregate((x, y) => x * y);
            _array = new int[array.Length];
            Array.Copy(array, _array, array.Length);
        }


        /// <summary>
        ///     Returns an enumerator that iterates throught the <see cref="IndexArray"/>.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
            => new Enumerator(this);


        /// <summary>
        ///     Returns an enumerator that iterates throught the <see cref="IndexArray"/>.
        /// </summary>
        /// <returns></returns>
        IEnumerator<int> IEnumerable<int>.GetEnumerator()
            => GetEnumerator();


        /// <summary>
        ///     Returns an enumerator that iterates throught the <see cref="IndexArray"/>.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();


        /// <summary>
        ///     Returns a value indicating whether this instance and a specified <see cref="IndexArray"/> represent the same value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IndexArray other)
        {
            if(_hashCode != other._hashCode)
                return false;
            if(TotalLength != other.TotalLength)
                return false;
            if(Length != other.Length)
                return false;
            for(int i = 0, len = Length ; i < len ; ++i)
            {
                if(this[i] != other[i])
                    return false;
            }

            return true;
        }


        /// <summary>
        ///     Returns a value indicating whether this instance and a specified <see cref="object"/> represent the same type and value.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
            => obj is IndexArray other && Equals(other);


        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => _hashCode;


        /// <summary>
        ///     Gets the text which presents this instance value.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"({string.Join(", ", this)},)";


        /// <summary>
        ///     Converts the specified <see cref="IndexArray"/> instance to <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <param name="indices"></param>
        public static implicit operator ReadOnlySpan<int>(IndexArray indices)
            => indices._array;


        /// <summary>
        ///     Converts the specified <typeref cref="int"/>[] instance t <see cref="IndexArray"/>.
        /// </summary>
        /// <param name="indices"></param>
        public static implicit operator IndexArray(int[] indices) => new IndexArray(indices);


        /// <summary>
        ///     Returns a value indicating whether two instance of <see cref="Index"/> represent the same value.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator==(IndexArray lhs, IndexArray rhs) => lhs.Equals(rhs);


        /// <summary>
        ///     Returns a value indicating whether two instance of <see cref="Index"/> represent the different value.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator!=(IndexArray lhs, IndexArray rhs) => !lhs.Equals(rhs);


        /// <summary>
        ///     Enumerates the elements of a <see cref="IndexArray"/>.
        /// </summary>
        public struct Enumerator : IEnumerator<int>
        {
            private readonly int[] _array;


            private int _index;


            /// <summary>
            ///     Gets the element at the current position of the enumerator.
            /// </summary>
            public int Current => _array[_index];


            object IEnumerator.Current => Current;


            internal Enumerator(in IndexArray array)
            {
                _array = array._array;
                _index = -1;
            }


            /// <summary>
            ///     Advances the enumerator to the next element of the <see cref="IndexArray"/>.
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
                => ++_index < _array.Length;



            void IEnumerator.Reset()
                => Guard.ThrowNotSupport();


            /// <summary>
            ///     Releases all resources used by this instance.
            /// </summary>
            public void Dispose()
            {
            }

        }

    }

    internal static class IndexArrayEx
    {

        internal static IndexArray ToIndexArray(this IEnumerable<int> source)
            => new IndexArray(source.ToArray());

    }
}
