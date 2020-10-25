using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Specialization;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet
{
    // Square-root functions
    // In explicitly implement, they uses Babylonian method
    partial class NdMath
    {
        /// <summary>
        ///     Returns the square root of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sqrt(double value)
            => Math.Sqrt(value);


        /// <summary>
        ///     Returns the square root of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float value)
        {
            Guard.AssertArgumentRange(0 <= value, "`value` must be greater than or equal to 0.");

            var x = 1f;
            for(; ; )
            {
                var xnext = (x + value / x) / 2;
                if(x == xnext)
                    return xnext;
                x = xnext;
            }
        }


        /// <summary>
        ///     Returns the square root of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Sqrt(decimal value)
        {
            Guard.AssertArgumentRange(0 <= value, "`value` must be greater than or equal to 0.");

            var x = 1m;
            for(; ; )
            {
                var xnext = (x + value / x) / 2;
                if(x == xnext)
                    return xnext;
                x = xnext;
            }
        }


        /// <summary>
        ///     Returns the square root of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sqrt(Complex value)
            => Complex.Sqrt(value);


        /// <summary>
        ///     Returns the square root of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sqrt<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Sqrt(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Sqrt(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Sqrt(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Sqrt(value.As<T, Complex>()).As<Complex, T>();

            Guard.AssertArgumentRange(GreaterThanOrEquals(Zero<T>(), value), "`value` must be greater than or equal to 0.");

            var x = One<T>();
            var two = Add(One<T>(), One<T>());
            for(var i = 0; i < MaximumIteration; ++i)
            {
                var xnext = Divide(Add(x, Divide(value, x)), two);
                if(Equals<T>(x, xnext))
                    return xnext;
                x = xnext;
            }
            return x;
        }
    }
}
