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
        ///     Returns the sine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sin(double value)
            => Math.Sin(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the sine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float value)
            => (float)Math.Sin(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the sine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Sin(decimal value)
            => (decimal)Math.Sin((double)value);


        /// <summary>
        ///     Returns the sine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sin(Complex value)
            => Complex.Sin(value);


        /// <summary>
        ///     Returns the sine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sin<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Sin(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Sin(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Sin(value.As<T, decimal>()).As<decimal, T>();

            throw new NotImplementedException();
        }

    }
}
