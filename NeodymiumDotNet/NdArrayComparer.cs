using System;
using System.Collections.Generic;
using System.Text;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Represents a <see cref="NdArray{T}"/> comparison operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NdArrayComparer<T> : IEqualityComparer<INdArray<T>>
    {
        /// <summary>
        ///     [Pure] Gets a default comparer.
        /// </summary>
        public static IEqualityComparer<INdArray<T>> Default { get; }
            = new NdArrayComparer<T>(EqualityComparer<T>.Default);


        /// <summary>
        ///     Gets an element comparer delegate.
        /// </summary>
        public Func<T, T, bool> CompareElement { get; }


        /// <summary>
        ///     Initializes a new instance of the <see cref="NdArrayComparer{T}"/> class.
        /// </summary>
        /// <param name="compareElement"></param>
        public NdArrayComparer(Func<T, T, bool> compareElement)
            => CompareElement = compareElement;


        /// <summary>
        ///     Initializes a new instance of the <see cref="NdArrayComparer{T}"/> class.
        /// </summary>
        /// <param name="elementComparer"></param>
        public NdArrayComparer(IEqualityComparer<T> elementComparer)
            : this(elementComparer.Equals)
        {
        }


        /// <summary>
        ///     Determines whether the specified NdArrays are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(INdArray<T> x, INdArray<T> y)
        {
            if(x.Shape != y.Shape)
                return false;

            var n = x.Shape.TotalLength;
            for(var i = 0; i < n; ++i)
                if( !CompareElement(x.GetItem(i), y.GetItem(i)) )
                    return false;

            return true;
        }


        /// <summary>
        ///     Returns a hash code for the specified NdArray.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(INdArray<T> obj) => 0;
    }
}
