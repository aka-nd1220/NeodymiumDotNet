using System;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Specialization;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet
{
    partial class NdMath
    {
        /// <summary>
        ///     Returns an integer that indicates the sign of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(sbyte value)
            => Math.Sign(value);


        /// <summary>
        ///     Returns an integer that indicates the sign of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(short value)
            => Math.Sign(value);


        /// <summary>
        ///     Returns an integer that indicates the sign of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(int value)
            => Math.Sign(value);


        /// <summary>
        ///     Returns an integer that indicates the sign of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(long value)
            => Math.Sign(value);


        /// <summary>
        ///     Returns an integer that indicates the sign of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(float value)
            => Math.Sign(value);


        /// <summary>
        ///     Returns an integer that indicates the sign of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(double value)
            => Math.Sign(value);


        /// <summary>
        ///     Returns an integer that indicates the sign of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(decimal value)
            => Math.Sign(value);


        /// <summary>
        ///     Returns an integer that indicates the sign of a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign<T>(T value)
        {
            if(typeof(T) == typeof(sbyte  )) return Sign(value.As<T, sbyte  >());
            if(typeof(T) == typeof(short  )) return Sign(value.As<T, short  >());
            if(typeof(T) == typeof(int    )) return Sign(value.As<T, int    >());
            if(typeof(T) == typeof(long   )) return Sign(value.As<T, long   >());
            if(typeof(T) == typeof(float  )) return Sign(value.As<T, float  >());
            if(typeof(T) == typeof(double )) return Sign(value.As<T, double >());
            if(typeof(T) == typeof(decimal)) return Sign(value.As<T, decimal>());

            if(GreaterThan(value, Zero<T>()))
                return 1;
            if(LessThan(value, Zero<T>()))
                return -1;
            return 0;
        }
    }
}
