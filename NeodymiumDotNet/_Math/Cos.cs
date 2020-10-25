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
        ///     Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cos(double value)
            => Math.Cos(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float value)
            => (float)Math.Cos(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Cos(decimal value)
            => (decimal)Math.Cos((double)value);


        /// <summary>
        ///     Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Cos(Complex value)
            => Complex.Cos(value);


        /// <summary>
        ///     Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Cos<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Cos(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Cos(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Cos(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Cos(value.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }

    }
}
