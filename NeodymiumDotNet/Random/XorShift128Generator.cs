using System;
using System.Collections.Generic;

namespace NeodymiumDotNet.Random
{
    /// <inheritdoc />
    /// <summary>
    ///     Random value generator which implements 128-bit xorshift algorithm.
    /// </summary>
    public sealed partial class XorShift128Generator : RandomGenerator
    {

        private uint _w;

        private uint _x;

        private uint _y;

        private uint _z;


        /// <summary>
        ///     1st seed value.
        /// </summary>
        public int SeedW { get; }


        /// <summary>
        ///     2nd seed value.
        /// </summary>
        public int SeedX { get; }


        /// <summary>
        ///     3rd seed value.
        /// </summary>
        public int SeedY { get; }


        /// <summary>
        ///     4th seed value.
        /// </summary>
        public int SeedZ { get; }


        /// <summary>
        ///     Creates new <see cref="XorShift128Generator"/> instance.
        /// </summary>
        /// <param name="w"> The 1st seed value. </param>
        /// <param name="x"> The 2nd seed value. </param>
        /// <param name="y"> The 3rd seed value. </param>
        /// <param name="z"> The 4th seed value. </param>
        public XorShift128Generator(int? w = null,
                                    int? x = null,
                                    int? y = null,
                                    int? z = null)
        {
            _w = (uint)(SeedW = w ?? Environment.TickCount);
            _x = (uint)(SeedX = x ?? SeedW << 13);
            _y = (uint)(SeedY = y ?? (SeedW >> 9) ^ (SeedX << 6));
            _z = (uint)(SeedZ = z ?? SeedY >> 7);
        }


        /// <inheritdoc />
        /// <summary>
        ///     Get single random value of <see cref="int"/>,
        ///     which is between <see cref="int.MinValue"/> - <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>
        ///     The random value which is between <see cref="int.MinValue"/> - <see cref="int.MaxValue"/>.
        /// </returns>
        public override uint NextBitArray()
        {
            var t = _x ^ (_x << 11);
            _x = _y;
            _y = _z;
            _z = _w;
            _w = _w ^ (_w >> 19) ^ t ^ (t >> 8);
            return _w;
        }

    }
}
