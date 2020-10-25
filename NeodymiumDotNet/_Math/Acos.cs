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
        ///     Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Acos(double value)
            => Math.Acos(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Acos(float value)
            => (float)Math.Acos(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Acos(decimal value)
            => (decimal)Math.Acos((double)value);


        /// <summary>
        ///     Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acos(Complex value)
            => Complex.Acos(value);


        /// <summary>
        ///     Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Acos<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Acos(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Acos(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Acos(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Acos(value.As<T, Complex>()).As<Complex, T>();

            throw new NotImplementedException();
        }

    }
}
