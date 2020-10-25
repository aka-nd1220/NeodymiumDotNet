using System;

namespace NeodymiumDotNet.Random
{
    /// <inheritdoc />
    /// <summary>
    ///     Random value generator which implements 96-bit xorshift algorithm.
    /// </summary>
    public sealed class XorShift96Generator : RandomGenerator
    {

        private uint _x;

        private uint _y;

        private uint _z;


        /// <summary>
        ///     The 1st seed value.
        /// </summary>
        public int SeedX { get; }


        /// <summary>
        ///     The 2nd seed value.
        /// </summary>
        public int SeedY { get; }


        /// <summary>
        ///     The 3rd seed value.
        /// </summary>
        public int SeedZ { get; }


        /// <summary>
        ///     Creates new <see cref="XorShift96Generator"/> instance.
        /// </summary>
        /// <param name="x"> The 1st seed value. </param>
        /// <param name="y"> The 2nd seed value. </param>
        /// <param name="z"> The 3rd seed value. </param>
        public XorShift96Generator(int? x = null,
                                   int? y = null,
                                   int? z = null)
        {
            _x = (uint)(SeedX = x ?? Environment.TickCount);
            _y = (uint)(SeedY = y ?? SeedX << 13);
            _z = (uint)(SeedZ = z ?? (SeedX >> 9) ^ (SeedY << 6));
        }


        /// <inheritdoc />
        /// <summary>
        ///     Get single random value of <see cref="T:System.Int32" />,
        ///     which is between <see cref="F:System.Int32.MinValue" /> - <see cref="F:System.Int32.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     The random value which is between <see cref="F:System.Int32.MinValue" /> - <see cref="F:System.Int32.MaxValue" />.
        /// </returns>
        public override uint NextBitArray()
        {
            var t = (_x ^ (_x << 3)) ^ (_y ^ (_y >> 19)) ^ (_z ^ (_z << 6));
            _x = _y;
            _y = _z;
            return _z = t;
        }

    }
}
