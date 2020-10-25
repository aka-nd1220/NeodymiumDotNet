using System;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Zero or one value container.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal struct Container<T>
    {
        internal bool HasValue { get; private set; }

        internal T Value { get; private set; }


        internal Container(T value)
        {
            HasValue = true;
            Value = value;
        }


        public static explicit operator T(Container<T> container) => container.Value;

        public static implicit operator Container<T>(T value) => new Container<T>(value);

    }
}
