using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Specialization;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet
{
    partial class NdMath
    {
        /// <summary>
        ///     Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Asin(double value)
            => Math.Asin(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Asin(float value)
            => (float)Math.Asin(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Asin(decimal value)
            => (decimal)Math.Asin((double)value);


        /// <summary>
        ///     Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asin(Complex value)
            => Complex.Asin(value);


        /// <summary>
        ///     Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Asin<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Asin(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Asin(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Asin(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Asin(value.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }

    }
}
