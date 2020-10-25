using System;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Specialization;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet
{
    partial class NdMath
    {
        /// <summary>
        ///     Returns the smallest integral value greater than or equal to the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Ceiling(double value)
            => Math.Ceiling(value);


        /// <summary>
        ///     Returns the smallest integral value greater than or equal to the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Ceiling(decimal value)
            => Math.Ceiling(value);


        // TODO: Improve algorithm
        /// <summary>
        ///     Returns the smallest integral value greater than or equal to the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Ceiling(float value)
            => (float)Math.Ceiling(value);


        /// <summary>
        ///     Returns the smallest integral value greater than or equal to the specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Ceiling<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Ceiling(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Ceiling(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Ceiling(value.As<T, decimal>()).As<decimal, T>();
            
            throw new NotImplementedException();
        }
    }
}
