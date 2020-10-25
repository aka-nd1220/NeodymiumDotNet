using System;

namespace NeodymiumDotNet.Random
{
    /// <inheritdoc />
    /// <summary>
    ///     Random value generator which implements 64-bit xorshift algorithm.
    /// </summary>
    public sealed partial class XorShift64Generator : RandomGenerator
    {
        private ulong _x;


        /// <summary>
        ///     The seed value.
        /// </summary>
        public int Seed { get; }


        /// <summary>
        ///     Creates new <see cref="XorShift64Generator"/> instance.
        /// </summary>
        /// <param name="seed"> The seed value. </param>
        public XorShift64Generator(int? seed = null)
        {
            _x = (ulong)(Seed = seed ?? Environment.TickCount);
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
            _x = _x ^ (_x << 13);
            _x = _x ^ (_x >> 7);
            return (uint)(_x = _x ^ (_x << 17));
        }

    }
}
