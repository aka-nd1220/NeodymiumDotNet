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
        ///     Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log10(double value)
            => Math.Log10(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log10(float value)
            => (float)Math.Log10(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Log10(decimal value)
            => (decimal)Math.Log10((double)value);


        /// <summary>
        ///     Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Log10(Complex value)
            => Complex.Log10(value);


        /// <summary>
        ///     Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Log10<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Log10(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Log10(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Log10(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Log10(value.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }

    }
}
