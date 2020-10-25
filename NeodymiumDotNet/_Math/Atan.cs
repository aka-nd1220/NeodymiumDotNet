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
        ///     Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Atan(double value)
            => Math.Atan(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan(float value)
            => (float)Math.Atan(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Atan(decimal value)
            => (decimal)Math.Atan((double)value);


        /// <summary>
        ///     Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Atan(Complex value)
            => Complex.Atan(value);


        /// <summary>
        ///     Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Atan<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Atan(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Atan(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Atan(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Atan(value.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }

    }
}
