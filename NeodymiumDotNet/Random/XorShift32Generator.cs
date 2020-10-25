using System;

namespace NeodymiumDotNet.Random
{
    /// <inheritdoc />
    /// <summary>
    ///     Random value generator which implements 32-bit xorshift algorithm.
    /// </summary>
    public sealed class XorShift32Generator : RandomGenerator
    {
        private uint _seed;


        /// <summary>
        ///     The seed value.
        /// </summary>
        public int Seed { get; }


        /// <summary>
        ///     Creates new <see cref="XorShift32Generator"/> instance.
        /// </summary>
        /// <param name="seed"> The seed value. </param>
        public XorShift32Generator(int? seed = null)
        {
            _seed = (uint)(Seed = seed ?? Environment.TickCount);
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
            _seed = _seed ^ (_seed << 13);
            _seed = _seed ^ (_seed >> 17);
            return _seed = _seed ^ (_seed << 15);
        }

    }
}
