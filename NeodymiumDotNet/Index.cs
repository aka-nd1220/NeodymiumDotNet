using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Presents an index of N-D array element.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = sizeof(int))]
    public readonly struct Index : IEquatable<Index>
    {
        [FieldOffset(0)] private readonly int _Value;


        /// <summary>
        ///     Gets true if this instance presents the index from tail.
        /// </summary>
        public bool FromEnd => _Value < 0;


        /// <summary>
        ///     Gets the numerical value to present the index.
        /// </summary>
        public int Value => FromEnd ? ~_Value : _Value;


        /// <summary>
        ///     Initializes a new instance of the <see cref="Index"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromEnd"></param>
        public Index(int value, bool fromEnd)
        {
            _Value = fromEnd ? ~value : value;
        }


        /// <summary>
        ///     Maps to an actual index value on an array entity.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public int Map(int length) => FromEnd ? length - Value : Value;


        /// <summary>
        ///     Gets the text which presents this instance value.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Index({(FromEnd ? "^" : "")}{Math.Abs(Value)})";


        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => Value.GetHashCode();


        /// <summary>
        ///     Returns a value indicating whether this instance and a specified <see cref="object"/> represent the same type and value.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
            => obj is Index other && Equals(other);


        /// <summary>
        ///     Returns a value indicating whether this instance and a specified <see cref="Index"/> represent the same value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Index other)
            => other.Value == Value;


        /// <summary>
        ///     Returns a value indicating whether two instance of <see cref="Index"/> represent the same value.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(Index lhs, Index rhs)
            => lhs.Value == rhs.Value;
        

        /// <summary>
        ///     Returns a value indicating whether two instance of <see cref="Index"/> represent the different value.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator !=(Index lhs, Index rhs)
            => lhs.Value != rhs.Value;


        /// <summary>
        ///     Defines an explicit conversion of a <see cref="int"/> to <see cref="Index"/>.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Index(int value) =>
            new Index(value < 0 ? -value : value, value < 0);

    }
}
