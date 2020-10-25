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
        ///     Returns the hyperbolic cosine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cosh(double value)
            => Math.Cosh(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the hyperbolic cosine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cosh(float value)
            => (float)Math.Cosh(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the hyperbolic cosine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Cosh(decimal value)
            => (decimal)Math.Cosh((double)value);


        /// <summary>
        ///     Returns the hyperbolic cosine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Cosh(Complex value)
            => Complex.Cosh(value);


        /// <summary>
        ///     Returns the hyperbolic cosine of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Cosh<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Cosh(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Cosh(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Cosh(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Cosh(value.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }

    }
}
