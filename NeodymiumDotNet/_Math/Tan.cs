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
        ///     Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Tan(double value)
            => Math.Tan(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(float value)
            => (float)Math.Tan(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Tan(decimal value)
            => (decimal)Math.Tan((double)value);


        /// <summary>
        ///     Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Tan(Complex value)
            => Complex.Tan(value);


        /// <summary>
        ///     Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Tan<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Tan(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Tan(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Tan(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Tan(value.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }

    }
}
