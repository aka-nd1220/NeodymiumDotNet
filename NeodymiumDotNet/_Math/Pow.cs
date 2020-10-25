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
        ///     Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Pow(double x, double y)
            => Math.Pow(x, y);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float x, float y)
            => (float)Math.Pow(x, y);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Pow(decimal x, decimal y)
            => (decimal)Math.Pow((double)x, (double)y);


        /// <summary>
        ///     Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Pow(Complex x, Complex y)
            => Complex.Pow(x, y);


        /// <summary>
        ///     Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Pow<T>(T x, T y)
        {
            if(typeof(T) == typeof(float  )) return Pow(x.As<T, float  >(), y.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Pow(x.As<T, double >(), y.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Pow(x.As<T, decimal>(), y.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Pow(x.As<T, Complex>(), y.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }
    }
}
