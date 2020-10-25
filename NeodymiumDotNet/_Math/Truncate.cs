using System;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Specialization;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet
{
    partial class NdMath
    {
        /// <summary>
        ///     Calculates the integral part of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Truncate(double value)
            => Math.Truncate(value);


        /// <summary>
        ///     Calculates the integral part of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Truncate(decimal value)
            => Math.Truncate(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Calculates the integral part of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Truncate(float value)
            => (float)Math.Truncate(value);


        /// <summary>
        ///     Calculates the integral part of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Truncate<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Truncate(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Truncate(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Truncate(value.As<T, decimal>()).As<decimal, T>();

            throw new NotImplementedException();
        }

    }
}
