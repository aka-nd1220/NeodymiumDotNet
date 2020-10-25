using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Specialization;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet
{
    partial class NdMath
    {
        /// <summary>
        ///     Returns e raised to the specified power.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Exp(double value)
            => Math.Exp(value);


        /// <summary>
        ///     Returns e raised to the specified power.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Exp(float value)
        {
            float core(float x1)
            {
                const float a1 = 1;
                const float a2 = a1 * 2;
                const float a3 = a2 * 3;
                const float a4 = a3 * 4;
                const float a5 = a4 * 5;
                const float a6 = a5 * 6;
                const float a7 = a6 * 7;
                const float a8 = a7 * 8;
                const float a9 = a8 * 9;
                var x2 = x1 * x1;
                var x3 = x2 * x1;
                var x4 = x3 * x1;
                var x5 = x4 * x1;
                var x6 = x5 * x1;
                var x7 = x6 * x1;
                var x8 = x7 * x1;
                var x9 = x8 * x1;
                return 1 + x1 / a1 + x2 / a2 + x3 / a3 + x4 / a4 + x5 / a5 + x6 / a6 + x7 / a7 + x8 / a8 + x9 / a9;
            }

            if(value == 0)
                return 1;
            if(value == 1)
                return E<float>();
            if(value < 0)
                return 1f / Exp(-value);
            if(value < 1)
                return core(value);

            var floorValue = (int)value;
            var modValue = value - floorValue;
            var retval = 1f;
            for(var i = 0; i < floorValue; ++i)
                retval *= E<float>();
            return retval * core(modValue);
        }


        /// <summary>
        ///     Returns e raised to the specified power.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Exp(decimal value)
        {
            decimal core(decimal x01)
            {
                const decimal a01 = 1;
                const decimal a02 = a01 * 2;
                const decimal a03 = a02 * 3;
                const decimal a04 = a03 * 4;
                const decimal a05 = a04 * 5;
                const decimal a06 = a05 * 6;
                const decimal a07 = a06 * 7;
                const decimal a08 = a07 * 8;
                const decimal a09 = a08 * 9;
                const decimal a10 = a09 * 10;
                const decimal a11 = a10 * 11;
                const decimal a12 = a11 * 12;
                const decimal a13 = a12 * 13;
                const decimal a14 = a13 * 14;
                const decimal a15 = a14 * 15;
                const decimal a16 = a15 * 16;
                const decimal a17 = a16 * 17;
                const decimal a18 = a17 * 18;
                const decimal a19 = a18 * 19;
                const decimal a20 = a19 * 20;
                const decimal a21 = a20 * 21;
                const decimal a22 = a21 * 22;
                const decimal a23 = a22 * 23;
                const decimal a24 = a23 * 24;
                const decimal a25 = a24 * 25;
                const decimal a26 = a25 * 26;
                const decimal a27 = a26 * 27;
                var x02 = x01 * x01;
                var x03 = x02 * x01;
                var x04 = x03 * x01;
                var x05 = x04 * x01;
                var x06 = x05 * x01;
                var x07 = x06 * x01;
                var x08 = x07 * x01;
                var x09 = x08 * x01;
                var x10 = x09 * x01;
                var x11 = x10 * x01;
                var x12 = x11 * x01;
                var x13 = x12 * x01;
                var x14 = x13 * x01;
                var x15 = x14 * x01;
                var x16 = x15 * x01;
                var x17 = x16 * x01;
                var x18 = x17 * x01;
                var x19 = x18 * x01;
                var x20 = x19 * x01;
                var x21 = x20 * x01;
                var x22 = x21 * x01;
                var x23 = x22 * x01;
                var x24 = x23 * x01;
                var x25 = x24 * x01;
                var x26 = x25 * x01;
                var x27 = x26 * x01;
                return 1         + x01 / a01 + x02 / a02 + x03 / a03 + x04 / a04 + x05 / a05 + x06 / a06 + x07 / a07 + x08 / a08 + x09 / a09
                     + x10 / a10 + x11 / a11 + x12 / a12 + x13 / a13 + x14 / a14 + x15 / a15 + x16 / a16 + x17 / a17 + x18 / a18 + x19 / a19
                     + x20 / a20 + x21 / a21 + x22 / a22 + x23 / a23 + x24 / a24 + x25 / a25 + x26 / a26 + x27 / a27;
            }

            if(value == 0)
                return 1;
            if(value == 1)
                return E<decimal>();
            if(value < 0)
                return 1m / Exp(-value);
            if(value < 1)
                return core(value);

            var floorValue = (int)value;
            var modValue = value - floorValue;
            var retval = 1m;
            for(var i = 0; i < floorValue; ++i)
                retval *= E<decimal>();
            return retval * core(modValue);
        }


        /// <summary>
        ///     Returns e raised to the specified power.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Exp(Complex value)
            => Complex.Exp(value);


        /// <summary>
        ///     Returns e raised to the specified power.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Exp<T>(T value)
        {
            if(typeof(T) == typeof(float  )) return Exp(value.As<T, float  >()).As<float  , T>();
            if(typeof(T) == typeof(double )) return Exp(value.As<T, double >()).As<double , T>();
            if(typeof(T) == typeof(decimal)) return Exp(value.As<T, decimal>()).As<decimal, T>();
            if(typeof(T) == typeof(Complex)) return Exp(value.As<T, Complex>()).As<Complex, T>();

            var prev = Zero<T>();
            var curr = One<T>();
            var coef = One<T>();
            var xn = value;
            for(var i = 0; i < MaximumIteration; ++i)
            {
                coef = Multiply(coef, FromLong<T>(i + 1));
                curr = Add(curr, Divide(xn, coef));
                if(Equals<T>(curr, prev))
                    break;

                xn = Multiply(xn, value);
            }

            return curr;
        }
    }
}
