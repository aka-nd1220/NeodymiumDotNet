using System;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Specialization;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet
{
    partial class NdMath
    {
        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Min(sbyte val1, sbyte val2)
            => Math.Min(val1, val2);


        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Min(short val1, short val2)
            => Math.Min(val1, val2);


        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(int val1, int val2)
            => Math.Min(val1, val2);


        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Min(long val1, long val2)
            => Math.Min(val1, val2);


        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Min(byte val1, byte val2)
            => Math.Min(val1, val2);


        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Min(ushort val1, ushort val2)
            => Math.Min(val1, val2);


        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Min(uint val1, uint val2)
            => Math.Min(val1, val2);


        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Min(ulong val1, ulong val2)
            => Math.Min(val1, val2);


        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(float val1, float val2)
            => Math.Min(val1, val2);


        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(double val1, double val2)
            => Math.Min(val1, val2);


        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Min(decimal val1, decimal val2)
            => Math.Min(val1, val2);


        /// <summary>
        ///     Returns the smaller of two numbers.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(T val1, T val2)
        {
            if(typeof(T) == typeof(sbyte  )) return Math.Min(val1.As<T, sbyte  >(), val2.As<T, sbyte  >()).As<sbyte  , T>();
            if(typeof(T) == typeof(short  )) return Math.Min(val1.As<T, short  >(), val2.As<T, short  >()).As<short  , T>();
            if(typeof(T) == typeof(int    )) return Math.Min(val1.As<T, int    >(), val2.As<T, int    >()).As<int    , T>();
            if(typeof(T) == typeof(long   )) return Math.Min(val1.As<T, long   >(), val2.As<T, long   >()).As<long   , T>();
            if(typeof(T) == typeof(byte   )) return Math.Min(val1.As<T, byte   >(), val2.As<T, byte   >()).As<byte   , T>();
            if(typeof(T) == typeof(ushort )) return Math.Min(val1.As<T, ushort >(), val2.As<T, ushort >()).As<ushort , T>();
            if(typeof(T) == typeof(uint   )) return Math.Min(val1.As<T, uint   >(), val2.As<T, uint   >()).As<uint   , T>();
            if(typeof(T) == typeof(ulong  )) return Math.Min(val1.As<T, ulong  >(), val2.As<T, ulong  >()).As<ulong  , T>();
            if(typeof(T) == typeof(float  )) return Math.Min(val1.As<T, float  >(), val2.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Math.Min(val1.As<T, double >(), val2.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Math.Min(val1.As<T, decimal>(), val2.As<T, decimal>()).As<decimal, T>();

            if(GreaterThanOrEquals(val1, val2))
                return val1;
            else
                return val2;
        }
    }
}
