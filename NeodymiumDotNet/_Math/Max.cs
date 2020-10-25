using System;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Specialization;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet
{
    partial class NdMath
    {
        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Max(sbyte val1, sbyte val2)
            => Math.Max(val1, val2);


        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Max(short val1, short val2)
            => Math.Max(val1, val2);


        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(int val1, int val2)
            => Math.Max(val1, val2);


        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Max(long val1, long val2)
            => Math.Max(val1, val2);


        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Max(byte val1, byte val2)
            => Math.Max(val1, val2);


        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Max(ushort val1, ushort val2)
            => Math.Max(val1, val2);


        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Max(uint val1, uint val2)
            => Math.Max(val1, val2);


        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Max(ulong val1, ulong val2)
            => Math.Max(val1, val2);


        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float val1, float val2)
            => Math.Max(val1, val2);


        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(double val1, double val2)
            => Math.Max(val1, val2);


        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Max(decimal val1, decimal val2)
            => Math.Max(val1, val2);


        /// <summary>
        ///     Returns the larger of two specified numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(T val1, T val2)
        {
            if(typeof(T) == typeof(sbyte  )) return Math.Max(val1.As<T, sbyte  >(), val2.As<T, sbyte  >()).As<sbyte  , T>();
            if(typeof(T) == typeof(short  )) return Math.Max(val1.As<T, short  >(), val2.As<T, short  >()).As<short  , T>();
            if(typeof(T) == typeof(int    )) return Math.Max(val1.As<T, int    >(), val2.As<T, int    >()).As<int    , T>();
            if(typeof(T) == typeof(long   )) return Math.Max(val1.As<T, long   >(), val2.As<T, long   >()).As<long   , T>();
            if(typeof(T) == typeof(byte   )) return Math.Max(val1.As<T, byte   >(), val2.As<T, byte   >()).As<byte   , T>();
            if(typeof(T) == typeof(ushort )) return Math.Max(val1.As<T, ushort >(), val2.As<T, ushort >()).As<ushort , T>();
            if(typeof(T) == typeof(uint   )) return Math.Max(val1.As<T, uint   >(), val2.As<T, uint   >()).As<uint   , T>();
            if(typeof(T) == typeof(ulong  )) return Math.Max(val1.As<T, ulong  >(), val2.As<T, ulong  >()).As<ulong  , T>();
            if(typeof(T) == typeof(float  )) return Math.Max(val1.As<T, float  >(), val2.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Math.Max(val1.As<T, double >(), val2.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Math.Max(val1.As<T, decimal>(), val2.As<T, decimal>()).As<decimal, T>();

            if(GreaterThanOrEquals(val1, val2))
                return val1;
            else
                return val2;
        }
    }
}
