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
        ///     Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Atan2(double y, double x)
            => Math.Atan2(y, x);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan2(float y, float x)
            => (float)Math.Atan2(y, x);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Atan2(decimal y, decimal x)
            => (decimal)Math.Atan2((double)y, (double)x);


        /// <summary>
        ///     Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Atan2<T>(T y, T x)
        {
            if(typeof(T) == typeof(float  )) return Atan2(y.As<T, float  >(), x.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Atan2(y.As<T, double >(), x.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Atan2(y.As<T, decimal>(), x.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Atan2(y.As<T, Complex>(), x.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }
    }
}
