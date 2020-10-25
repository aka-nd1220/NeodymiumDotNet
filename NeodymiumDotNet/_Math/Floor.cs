using System;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Specialization;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet
{
    partial class NdMath
    {
        /// <summary>
        ///     Returns the largest integral value less than or equal to the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Floor(double value)
            => Math.Floor(value);


        /// <summary>
        ///     Returns the largest integral value less than or equal to the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Floor(decimal value)
            => Math.Floor(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the largest integral value less than or equal to the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Floor(float value)
            => (float)Math.Floor(value);


        /// <summary>
        ///     Returns the largest integral value less than or equal to the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Floor<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Floor(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Floor(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Floor(value.As<T, decimal>()).As<decimal, T>();

            throw new NotImplementedException();
        }

    }
}
