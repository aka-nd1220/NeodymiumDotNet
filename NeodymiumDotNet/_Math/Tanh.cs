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
        ///     Returns the hyperbolic tangent of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Tanh(double value)
            => Math.Tanh(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the hyperbolic tangent of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tanh(float value)
            => (float)Math.Tanh(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the hyperbolic tangent of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Tanh(decimal value)
            => (decimal)Math.Tanh((double)value);


        /// <summary>
        ///     Returns the hyperbolic tangent of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Tanh(Complex value)
            => Complex.Tanh(value);


        /// <summary>
        ///     Returns the hyperbolic tangent of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Tanh<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Tanh(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Tanh(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Tanh(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Tanh(value.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }

    }
}
