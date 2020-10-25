using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Represents a union type of <see cref="Index"/> and <see cref="Range"/>.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct IndexOrRange
    {
        [FieldOffset(0)] private readonly Index _index;


        [FieldOffset(0)] private readonly Range _range;


        /// <summary>
        ///     Gets the <see cref="Index"/> value if this instance has a <see cref="Index"/>.
        /// </summary>
        public Index Index => IsRange ? default : _index;


        /// <summary>
        ///     Gets the <see cref="Range"/> value if this instance has a <see cref="Range"/>.
        /// </summary>
        public Range Range => IsRange ? _range : default;


        /// <summary>
        ///     Gets <c>true</c> if this instance has a <see cref="Range"/>.
        /// </summary>
        public bool IsRange => _range.Step != 0;


        /// <summary>
        ///     Initializes a new instance with <see cref="Index"/>.
        /// </summary>
        /// <param name="index"></param>
        public IndexOrRange(Index index)
        {
            _range  = default;
            _index = index;
        }


        /// <summary>
        ///     Initializes a new instance with <see cref="Range"/>.
        /// </summary>
        /// <param name="range"></param>
        public IndexOrRange(Range range)
        {
            _index = default;
            _range  = range;
        }


        /// <summary>
        ///     Gets the text which presents this instance value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => IsRange ? Range.ToString() : Index.ToString();


        /// <summary>
        ///     Convert the specified <see cref="Index"/> instance to <see cref="IndexOrRange"/>.
        /// </summary>
        /// <param name="index"></param>
        public static implicit  operator IndexOrRange(Index index) => new IndexOrRange(index);


        /// <summary>
        ///     Convert the specified <see cref="Range"/> instance to <see cref="IndexOrRange"/>.
        /// </summary>
        /// <param name="range"></param>
        public static implicit  operator IndexOrRange(Range range) => new IndexOrRange(range);

    }
}
