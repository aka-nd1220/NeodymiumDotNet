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
        ///     Returns the logarithm of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log(double value)
            => Math.Log(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the logarithm of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float value)
            => (float)Math.Log(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the logarithm of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Log(decimal value)
            => (decimal)Math.Log((double)value);


        /// <summary>
        ///     Returns the logarithm of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Log(Complex value)
            => Complex.Log(value);


        /// <summary>
        ///     Returns the logarithm of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Log<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Log(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Log(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Log(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Log(value.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }


        /// <summary>
        ///     Returns the logarithm of a specified number.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="newBase"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log(double a, double newBase)
            => Math.Log(a, newBase);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the logarithm of a specified number.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="newBase"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float a, float newBase)
            => (float)Math.Log(a, newBase);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the logarithm of a specified number.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="newBase"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Log(decimal a, decimal newBase)
            => (decimal)Math.Log((double)a, (double)newBase);


        /// <summary>
        ///     Returns the logarithm of a specified number.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="newBase"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Log(Complex a, Complex newBase)
            => Complex.Log(a, newBase.Real);


        /// <summary>
        ///     Returns the logarithm of a specified number.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="newBase"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Log<T>(T a, T newBase)
        {
            if(typeof(T) == typeof(float  )) return Log(a.As<T, float  >(), newBase.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Log(a.As<T, double >(), newBase.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Log(a.As<T, decimal>(), newBase.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Log(a.As<T, Complex>(), newBase.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }
    }
}
