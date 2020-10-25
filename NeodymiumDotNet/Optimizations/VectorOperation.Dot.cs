using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NeodymiumDotNet.Optimizations
{
    partial class VectorOperation
    {

        /// <summary>
        /// Calculates dot product of the two specified sequences.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static T Dot<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs)
        {
            Guard.AssertArgument(lhs.Length == rhs.Length, "lhs.Length == rhs.Length");

            if(typeof(T) == typeof(byte))
            {
                var result = DotOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref rhs)
                );
                return Unsafe.As<byte, T>(ref result);
            }
            if(typeof(T) == typeof(ushort))
            {
                var result = DotOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref rhs)
                );
                return Unsafe.As<ushort, T>(ref result);
            }
            if(typeof(T) == typeof(uint))
            {
                var result = DotOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref rhs)
                );
                return Unsafe.As<uint, T>(ref result);
            }
            if(typeof(T) == typeof(ulong))
            {
                var result = DotOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref rhs)
                );
                return Unsafe.As<ulong, T>(ref result);
            }
            if(typeof(T) == typeof(sbyte))
            {
                var result = DotOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref rhs)
                );
                return Unsafe.As<sbyte, T>(ref result);
            }
            if(typeof(T) == typeof(short))
            {
                var result = DotOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs)
                );
                return Unsafe.As<short, T>(ref result);
            }
            if(typeof(T) == typeof(int))
            {
                var result = DotOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs)
                );
                return Unsafe.As<int, T>(ref result);
            }
            if(typeof(T) == typeof(long))
            {
                var result = DotOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref rhs)
                );
                return Unsafe.As<long, T>(ref result);
            }
            if(typeof(T) == typeof(float))
            {
                var result = DotOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref rhs)
                );
                return Unsafe.As<float, T>(ref result);
            }
            if(typeof(T) == typeof(double))
            {
                var result = DotOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref rhs)
                );
                return Unsafe.As<double, T>(ref result);
            }

            {
                var result = ValueTrait.Zero<T>();
                for(var i = 0; i < lhs.Length; ++i)
                    result = ValueTrait.Add(result, ValueTrait.Multiply(lhs.Span[i], rhs.Span[i]));
                return result;
            }
        }


        /// <summary>
        /// Calculates dot product of the two specified sequences.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        private static T DotOptimized<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var retval = ValueTrait.Zero<T>();
            var i = 0;

            for(var len = lhsVSpan.Length & ~0b1111; i < len; i += 16)
            {
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i], rhsVSpan[i]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x1], rhsVSpan[i + 0x1]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x2], rhsVSpan[i + 0x2]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x3], rhsVSpan[i + 0x3]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x4], rhsVSpan[i + 0x4]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x5], rhsVSpan[i + 0x5]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x6], rhsVSpan[i + 0x6]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x7], rhsVSpan[i + 0x7]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x8], rhsVSpan[i + 0x8]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x9], rhsVSpan[i + 0x9]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0xA], rhsVSpan[i + 0xA]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0xB], rhsVSpan[i + 0xB]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0xC], rhsVSpan[i + 0xC]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0xD], rhsVSpan[i + 0xD]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0xE], rhsVSpan[i + 0xE]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0xF], rhsVSpan[i + 0xF]));
            }
            if(i < (lhsVSpan.Length & ~0b111))
            {
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i], rhsVSpan[i]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x1], rhsVSpan[i + 0x1]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x2], rhsVSpan[i + 0x2]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x3], rhsVSpan[i + 0x3]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x4], rhsVSpan[i + 0x4]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x5], rhsVSpan[i + 0x5]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x6], rhsVSpan[i + 0x6]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x7], rhsVSpan[i + 0x7]));
                i += 8;
            }
            if(i < (lhsVSpan.Length & ~0b11))
            {
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i], rhsVSpan[i]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x1], rhsVSpan[i + 0x1]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x2], rhsVSpan[i + 0x2]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x3], rhsVSpan[i + 0x3]));
                i += 4;
            }
            if(i < (lhsVSpan.Length & ~0b1))
            {
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i], rhsVSpan[i]));
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i + 0x1], rhsVSpan[i + 0x1]));
                i += 2;
            }
            if(i < lhsVSpan.Length)
            {
                retval = ValueTrait.Add(retval, Vector.Dot(lhsVSpan[i], rhsVSpan[i]));
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                retval = ValueTrait.Add(retval, ValueTrait.Multiply(lhsRemainSpan[j], rhsRemainSpan[j]));
            return retval;
        }
    }
}
