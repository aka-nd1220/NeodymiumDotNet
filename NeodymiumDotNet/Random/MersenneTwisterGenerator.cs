using System;
using System.Collections.Generic;

namespace NeodymiumDotNet.Random
{
    /// <inheritdoc />
    /// <summary>
    ///     Random value generator which implements Mersenne twister algorithm.
    /// </summary>
    public class MersenneTwisterGenerator : RandomGenerator
    {

        private const int _N = 624;

        private const int _M = 397;

        private const int _U = 11;

        private const int _S = 7;

        private const int _T = 15;

        private const int _L = 18;

        private const uint _A = 0x9908b0df;

        private const uint _B = 0x9d2c5680;

        private const uint _C = 0xefc60000;

        private const uint _UpperMask = 0x80000000;

        private const uint _LowerMask = 0x7fffffff;

        private readonly uint[] _mt;

        private uint _index;


        /// <summary>
        ///     Creates new <see cref="MersenneTwisterGenerator"/> instance with single seed.
        /// </summary>
        /// <param name="seed"></param>
        public MersenneTwisterGenerator(int seed)
        {
            _mt = new uint[_N];
            _mt[0] = (uint)seed;
            for(_index = 1 ; _index < _N ; ++_index)
            {
                var prev = _mt[_index - 1];
                _mt[_index] = 1812433253u * (prev ^ (prev >> 30)) + _index;
            }
        }


        /// <summary>
        ///     Creates new <see cref="T:NeodymiumDotNet.Random.MersenneTwisterGenerator" /> instance with seed array.
        /// </summary>
        /// <param name="seedArray"></param>
        public MersenneTwisterGenerator(IReadOnlyList<int> seedArray)
            : this(19650218)
        {
            var i = 1;
            var j = 0;
            var k = NdMath.Max(_N, seedArray.Count);
            for(; k > 0 ; --k)
            {
                var prev = _mt[i - 1];
                _mt[i] = (_mt[i] ^ ((prev ^ (prev >> 30)) * 1664525u)) + (uint)seedArray[j] + (uint)j;
                ++i;
                ++j;
                if(i >= _N)
                {
                    _mt[0] = _mt[_N - 1];
                    i = 1;
                }
                if(j >= seedArray.Count)
                {
                    j = 0;
                }
            }

            for(k = _N - 1 ; k > 0 ; --k)
            {
                var prev = _mt[i - 1];
                _mt[i] = (_mt[i] ^ ((prev ^ (prev >> 30)) * 1566083941)) - (uint)i;
                ++i;
                if(i >= _N)
                {
                    _mt[0] = _mt[_N - 1];
                    i = 1;
                }
            }

            _mt[0] = _UpperMask;
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
            if(_index == _N)
                Twist();

            var x = _mt[_index++];
            x ^= (x >> _U);
            x ^= (x << _S) & _B;
            x ^= (x << _T) & _C;
            x ^= (x >> _L);
            return x;
        }


        private void Twist()
        {
            void core(int i, int j, int k, int p)
            {
                var y = (_mt[i] & _UpperMask) | (_mt[j] & _LowerMask);
                _mt[i] = _mt[k] ^ (y >> 1) ^ ((_mt[p] & 1) * _A);
            }

            var l = 0;
            for(; l < _N - _M; ++l)
                core(l, l + 1, l + _M, l);
            for(; l < _N - 1; ++l)
                core(l, l + 1, l + _M - _N, l);
            core(_N - 1, 0, _M - 1, 0);

            _index = 0;
        }
    }
}
