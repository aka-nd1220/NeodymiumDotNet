using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Specialization;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet
{
    partial class NdMath
    {
        #region basic absolute

        /// <summary>
        ///     Returns the absolute value of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Abs(sbyte value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Abs(short value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(int value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Abs(long value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Abs(double value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Abs(decimal value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Abs(Complex value)
            => value.Magnitude;


        /// <summary>
        ///     Returns the absolute value of a specified number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Abs<T>(T value)
        {
            if(typeof(T) == typeof(sbyte)) return Abs(value.As<T, sbyte>()).As<sbyte, T>();
            if(typeof(T) == typeof(short)) return Abs(value.As<T, short>()).As<short, T>();
            if(typeof(T) == typeof(int)) return Abs(value.As<T, int>()).As<int, T>();
            if(typeof(T) == typeof(long)) return Abs(value.As<T, long>()).As<long, T>();
            if(typeof(T) == typeof(float)) return Abs(value.As<T, float>()).As<float, T>();
            if(typeof(T) == typeof(double)) return Abs(value.As<T, double>()).As<double, T>();
            if(typeof(T) == typeof(decimal)) return Abs(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Abs(value.As<T, Complex>()).As<Complex, T>();

            if(GreaterThanOrEquals(value, Zero<T>()))
                return value;
            else
                return UnaryNegate(value);
        }

        #endregion


        #region absolute as double

        /// <summary>
        ///     Returns the absolute value of a specified number as a double value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DoubleAbs(sbyte value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number as a double value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DoubleAbs(short value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number as a double value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DoubleAbs(int value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number as a double value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DoubleAbs(long value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number as a double value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DoubleAbs(float value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number as a double value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DoubleAbs(double value)
            => Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number as a double value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DoubleAbs(decimal value)
            => (double)Math.Abs(value);


        /// <summary>
        ///     Returns the absolute value of a specified number as a double value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DoubleAbs(Complex value)
            => value.Magnitude;


        /// <summary>
        ///     Returns the absolute value of a specified number as a double value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DoubleAbs<T>(T value)
        {
            if(typeof(T) == typeof(sbyte  )) return DoubleAbs(value.As<T, sbyte  >());
            if(typeof(T) == typeof(short  )) return DoubleAbs(value.As<T, short  >());
            if(typeof(T) == typeof(int    )) return DoubleAbs(value.As<T, int    >());
            if(typeof(T) == typeof(long   )) return DoubleAbs(value.As<T, long   >());
            if(typeof(T) == typeof(float  )) return DoubleAbs(value.As<T, float  >());
            if(typeof(T) == typeof(double )) return DoubleAbs(value.As<T, double >());
            if(typeof(T) == typeof(decimal)) return DoubleAbs(value.As<T, decimal>());
            if(typeof(T) == typeof(Complex)) return DoubleAbs(value.As<T, Complex>());

            return Abs(ToDouble(value));
        }

        #endregion
    }
}
