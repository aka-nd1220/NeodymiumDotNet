using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NeodymiumDotNet.Random
{
    /// <summary>
    ///     [Thread-Unsafe] Random value base generator interface.
    /// </summary>
    public abstract partial class RandomGenerator
    {
        internal const int MaxInt32Mask = 0x7fffffff;

        internal const float Float32Coef = 1 / (float)uint.MaxValue;

        internal const double Float64Coef = 1 / (double)uint.MaxValue;


        /// <summary>
        ///     Default random generator.
        /// </summary>
        public static RandomGenerator Default { get; } = new XorShift128Generator();


        /// <summary>
        ///     [Thread-Unsafe] Generates next 32-bits random value.
        /// </summary>
        /// <returns></returns>
        public abstract uint NextBitArray();


        /// <summary>
        ///     [Thread-Unsafe] Get single random value of <see cref="int"/>,
        ///     which is between <c>0</c> - <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NextInt32()
        {
            return (int)(NextBitArray() & MaxInt32Mask);
        }


        /// <summary>
        ///     [Thread-Unsafe] Get multiple random values of <see cref="int"/>,
        ///     which are between <c>0</c> - <see cref="int.MaxValue"/>.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int[] NextInt32(int count)
        {
            var retval = new int[count];
            for(var i = 0 ; i < count ; ++i) retval[i] = NextInt32();
            return retval;
        }


        /// <summary>
        ///     [Thread-Unsafe] Get single random value of <see cref="long"/>,
        ///     which is between <c>0</c> - <see cref="long.MaxValue"/>.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long NextInt64()
        {
            return (long)((ulong)NextBitArray() << 32) + NextBitArray();
        }


        /// <summary>
        ///     [Thread-Unsafe] Get multiple random values of <see cref="long"/>,
        ///     which are between <c>0</c> - <see cref="long.MaxValue"/>.
        /// </summary>
        /// <param name="count"> [<c>0 &lt;= count</c>] </param>
        /// <returns></returns>
        public long[] NextInt64(int count)
        {
            var retval = new long[count];
            for(var i = 0 ; i < count ; ++i) retval[i] = NextInt64();
            return retval;
        }


        /// <summary>
        ///     [Thread-Unsafe] Get single random value of <see cref="float"/>.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float NextFloat32()
        {
            return NextBitArray() * Float32Coef;
        }


        /// <summary>
        ///     [Thread-Unsafe] Get multiple random values of <see cref="float"/>.
        /// </summary>
        /// <param name="count"> [<c>0 &lt;= count</c>] </param>
        /// <returns></returns>
        public float[] NextFloat32(int count)
        {
            var retval = new float[count];
            for(var i = 0 ; i < count ; ++i) retval[i] = NextFloat32();
            return retval;
        }


        /// <summary>
        ///     [Thread-Unsafe] Get single random value of <see cref="double"/>.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NextFloat64()
        {
            return NextBitArray() * Float64Coef;
        }


        /// <summary>
        ///     [Thread-Unsafe] Get multiple random values of <see cref="double"/>.
        /// </summary>
        /// <param name="count"> [<c>0 &lt;= count</c>] </param>
        /// <returns></returns>
        public double[] NextFloat64(int count)
        {
            var retval = new double[count];
            for(var i = 0 ; i < count ; ++i) retval[i] = NextFloat64();
            return retval;
        }

        /// <summary>
        ///     [Thread-Unsafe] Get single normal distribution random value of <see cref="float"/>.
        /// </summary>
        /// <returns></returns>
        public float NextNorm32()
        {
            var x = NextFloat32();
            var y = NextFloat32();
            return NdMath.Sqrt(-2 * NdMath.Log(x)) * NdMath.Cos(2 * NdMath.PI<float>() * y);
        }


        /// <summary>
        ///     [Thread-Unsafe] Get multiple normal distribution random values of <see cref="float"/>.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public float[] NextNorm32(int count)
        {
            var half = count / 2;
            var retval = NextFloat32(count);
            for(var i = 0; i < half; ++i)
            {
                var j = i << 1;
                var x = retval[j];
                var y = retval[j + 1];
                var mag = NdMath.Sqrt(-2 * NdMath.Log(x));
                var ang = 2 * NdMath.PI<float>() * y;
                retval[j] = mag * NdMath.Cos(ang);
                retval[j + 1] = mag * NdMath.Sin(ang);
            }

            if((count & 1) == 1) // count % 2 == 1
            {
                var x = retval[count - 1];
                var y = NextFloat32();
                retval[count - 1] = NdMath.Sqrt(-2 * NdMath.Log(x)) * NdMath.Cos(2 * NdMath.PI<float>() * y);
            }

            return retval;
        }


        /// <summary>
        ///     [Thread-Unsafe] Get single normal distribution random value of <see cref="double"/>.
        /// </summary>
        /// <returns></returns>
        public double NextNorm64()
        {
            var x = NextFloat64();
            var y = NextFloat64();
            return NdMath.Sqrt(-2 * NdMath.Log(x)) * NdMath.Cos(2 * NdMath.PI<double>() * y);
        }


        /// <summary>
        ///     [Thread-Unsafe] Get multiple normal distribution random values of <see cref="double"/>.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public double[] NextNorm64(int count)
        {
            var half = count / 2;
            var retval = NextFloat64(count);
            for(var i = 0; i < half; ++i)
            {
                var j = i << 1; // 2 * i
                var x = retval[j];
                var y = retval[j + 1];
                var mag = NdMath.Sqrt(-2 * NdMath.Log(x));
                var ang = 2 * NdMath.PI<double>() * y;
                retval[j] = mag * NdMath.Cos(ang);
                retval[j + 1] = mag * NdMath.Sin(ang);
            }

            if((count & 1) == 1) // count % 2 == 1
            {
                var x = retval[count - 1];
                var y = NextFloat64();
                retval[count - 1] = NdMath.Sqrt(-2 * NdMath.Log(x)) * NdMath.Cos(2 * NdMath.PI<double>() * y);
            }

            return retval;
        }

    }
}
