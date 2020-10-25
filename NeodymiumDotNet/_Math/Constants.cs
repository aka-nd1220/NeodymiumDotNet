using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NeodymiumDotNet
{
    partial class NdMath
    {
        private static class Cache<T>
        {
            public static T E { get; set; } = default!;

            public static T PI { get; set; } = default!;
        }


        static NdMath()
        {
            Cache<float>.E = (float)Math.E;
            Cache<float>.PI =(float)Math.PI;
            Cache<double>.E = Math.E;
            Cache<double>.PI = Math.PI;
            Cache<decimal>.E = 2.718281828459045235360287471352m;
            Cache<decimal>.PI = 3.141592653589793238462643383279m;
            Cache<Complex>.E = Math.E;
            Cache<Complex>.PI = Math.PI;
        }


        /// <summary>
        ///     Gets the natural logarithmic base, specified by the constant, <c>e</c>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T E<T>() => Cache<T>.E;


        /// <summary>
        ///     Gets the ratio of the circumference of a circle to its diameter, specified by the constant, π.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PI<T>() => Cache<T>.PI;
    }
}
