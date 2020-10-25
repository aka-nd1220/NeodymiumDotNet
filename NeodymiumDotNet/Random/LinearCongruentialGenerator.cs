using System;
using System.Collections.Generic;
using System.Linq;

namespace NeodymiumDotNet.Random
{
    /// <summary>
    ///     Named LCG parameter set identifier.
    /// </summary>
    public enum LcNamedParameterSet
    {
        /// <summary>
        ///     <para> The parameter set of Java java.util.Random. </para>
        ///     <para> <c>m = 0x4000000uL - 1uL, a = 48271, c = 0, bitMaskBottom = 16, bitMaskTop = 32</c> </para>
        /// </summary>
        [LcParameterSet(0x1000000000000uL, 0x5deece66duL, 11, 16, 48)]
        Java,

        /// <summary>
        ///     <para> The parameter set of Borland C/C++ rand(). </para>
        ///     <para> <c>m = 0x4000000uL - 1uL, a = 48271, c = 0, bitMaskBottom = 16, bitMaskTop = 32</c> </para>
        /// </summary>
        [LcParameterSet(0x40000000uL - 1uL, 48271, 0, 16, 32)]
        ParkMiller,
    }

    /// <summary>
    ///     Random value generator which implements LCG algorithm.
    /// </summary>
    public sealed class LinearCongruentialGenerator : RandomGenerator
    {

        private static readonly
            IReadOnlyDictionary<LcNamedParameterSet, LcParameterSetAttribute> _DefaultParamSet;


        static LinearCongruentialGenerator()
        {
            object[] getAttributes(LcNamedParameterSet x)
                => x.GetType().GetField(x.ToString())
                    .GetCustomAttributes(typeof(LcParameterSetAttribute), false);

            _DefaultParamSet = Enum
                              .GetValues(typeof(LcNamedParameterSet))
                              .Cast<LcNamedParameterSet>()
                              .Select(x => (en: x, attr: getAttributes(x)))
                              .Where(x => x.attr.Length == 1)
                              .ToDictionary(x => x.en, x => (LcParameterSetAttribute)x.attr[0]);
        }


        private ulong _current;


        /// <summary>
        ///     The initial seed.
        /// </summary>
        public long Seed { get; }


        /// <summary>
        ///     The modulus.
        /// </summary>
        public long M { get; }


        /// <summary>
        ///     The multiplier.
        /// </summary>
        public long A { get; }


        /// <summary>
        ///     The increment.
        /// </summary>
        public long C { get; }


        /// <summary>
        ///     The bitmask for truncation of lower bits.
        /// </summary>
        public long BitMask { get; }


        /// <summary>
        ///     The lower index of the bitmask range.
        /// </summary>
        public int BitMaskBottom { get; }


        /// <summary>
        ///     The upper index of the bitmask range.
        /// </summary>
        public int BitMaskTop { get; }


        /// <summary>
        ///     [<c>AvailableBitCount == BitMaskTop - BitMaskBottom</c>] The width of the bitmask range.
        /// </summary>
        public int AvailableBitCount { get; }


        private LinearCongruentialGenerator(long seed,
                                            in ValueTuple<long, long, long, int, int> paramSet)
            : this(seed, paramSet.Item1, paramSet.Item2, paramSet.Item3, paramSet.Item4,
                   paramSet.Item5)
        {
        }


        /// <summary>
        ///     <para> Creates new <see cref="LinearCongruentialGenerator"/> instance. </para>
        ///     <para> [<c>this.BitMask == GetBitMask(bitMaskBottom, bitMaskTop)</c>] </para>
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="m"></param>
        /// <param name="a"></param>
        /// <param name="c"></param>
        /// <param name="bitMaskBottom"></param>
        /// <param name="bitMaskTop"></param>
        public LinearCongruentialGenerator(long seed, long m, long a, long c, int bitMaskBottom,
                                           int bitMaskTop)
        {
            _current = (ulong)(Seed = seed);
            M = m;
            A = a;
            C = c;
            BitMaskTop = bitMaskTop;
            BitMaskBottom = bitMaskBottom;
            AvailableBitCount = BitMaskTop - BitMaskBottom;
            BitMask = GetBitMask(bitMaskBottom, bitMaskTop);
        }


        /// <summary>
        ///     Creates new <see cref="LinearCongruentialGenerator"/> instance.
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="parameterSet"></param>
        /// <param name="bitMaskBottom"></param>
        /// <param name="bitMaskTop"></param>
        public LinearCongruentialGenerator(long seed = 1,
                                           LcNamedParameterSet parameterSet =
                                               LcNamedParameterSet.Java,
                                           int? bitMaskBottom = null,
                                           int? bitMaskTop = null)
            : this(seed, _DefaultParamSet[parameterSet].GetParamSet(bitMaskBottom, bitMaskTop))
        {
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
            var retval = 0u;
            for(var i = 0 ; i < 32 ; i += AvailableBitCount)
            {
                _current = (_current * (ulong)A + (ulong)C) % (ulong)M;
                retval = (retval << AvailableBitCount) |
                         (uint)((_current & (ulong)BitMask) >> BitMaskBottom);
            }

            return retval;
        }


        /// <summary>
        ///     Gets a bitmask whose bits between <paramref name="bottom" /> and <paramref name="top" /> are 1.
        /// </summary>
        /// <param name="bottom"> The minimum bit index of 1. </param>
        /// <param name="top"> The maximum bit index of 1. </param>
        /// <returns>
        ///     <para> The bitmask whose bits between <paramref name="bottom" /> and <paramref name="top" /> are 1. </para>
        ///     <para>
        ///         For example, <c>GetBitMask(0, 0) == 0b0000uL</c>,
        ///         <c>GetBitMask(1, 3) == 0b0110uL</c>,
        ///         and <c>GetBitMask(2, 6) == 0b111100uL</c>
        ///     </para>
        /// </returns>
        public static long GetBitMask(int bottom, int top)
        {
            var retval = 0L;
            var bit = 1L << bottom;
            for(var i = bottom ; i < top ; ++i, bit <<= 1)
                retval |= bit;
            return retval;
        }

    }

    internal class LcParameterSetAttribute : Attribute
    {

        public ulong M { get; }

        public ulong A { get; }

        public ulong C { get; }

        public int BitMaskBottom { get; }

        public int BitMaskTop { get; }


        public LcParameterSetAttribute(ulong m, ulong a, ulong c, int bitMaskBottom = 0,
                                       int bitMaskTop = 64)
        {
            M = m;
            A = a;
            C = c;
            BitMaskBottom = bitMaskBottom;
            BitMaskTop = bitMaskTop;
        }


        public ValueTuple<long, long, long, int, int> GetParamSet(
            int? bitMaskBottom, int? bitMaskTop)
            => ((long)M, (long)A, (long)C,
                bitMaskBottom ?? BitMaskBottom, bitMaskTop ?? BitMaskTop);

    }
}
