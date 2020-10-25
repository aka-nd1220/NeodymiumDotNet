using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace NeodymiumDotNet.Optimizations
{
    partial class VectorOperation
    {

        #region UnaryNegate

        /// <summary>
        ///     Applies unarynegate operations for a n-d array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"> It can be same instance with <paramref name="value"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void UnaryNegate<T>(INdArray<T> value, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(value.Shape == result.Shape, "There is shape mismatch.");
            if(value.TryGetBufferImpl(out var xvalue) && result is RawNdArray<T> xresult)
            {
                UnaryNegate(xvalue.Buffer, xresult.Entity.Buffer);
                return;
            }
            else
            {
                for(var i = 0; i < result.Length; ++i)
                    result.SetItem(i, ValueTrait.UnaryNegate(value.GetItem(i))); 
            }
        }

        /// <summary>
        ///     Applies unarynegate operations for a memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException" />
        /// <exception cref="NotSupportedException" />
        public static void UnaryNegate<T>(ReadOnlyMemory<T> value, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= value.Length, "result.Length <= value.Length");
            if(typeof(T) == typeof(byte))
            {
                UnaryNegateOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref value),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                UnaryNegateOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref value),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                UnaryNegateOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref value),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                UnaryNegateOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref value),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                UnaryNegateOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref value),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                UnaryNegateOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref value),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                UnaryNegateOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref value),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                UnaryNegateOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref value),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(typeof(T) == typeof(float))
            {
                UnaryNegateOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref value),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                UnaryNegateOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref value),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.UnaryNegate(value.Span[i]);
                return;
            }
        }

        private static void UnaryNegateOptimized<T>(ReadOnlyMemory<T> value, Memory<T> result)
            where T : unmanaged
        {
            var valueVSpan = MemoryMarshal.Cast<T, Vector<T>>(value.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = -valueVSpan[i      ];
                resultVSpan[i + 0x1] = -valueVSpan[i + 0x1];
                resultVSpan[i + 0x2] = -valueVSpan[i + 0x2];
                resultVSpan[i + 0x3] = -valueVSpan[i + 0x3];
                resultVSpan[i + 0x4] = -valueVSpan[i + 0x4];
                resultVSpan[i + 0x5] = -valueVSpan[i + 0x5];
                resultVSpan[i + 0x6] = -valueVSpan[i + 0x6];
                resultVSpan[i + 0x7] = -valueVSpan[i + 0x7];
                resultVSpan[i + 0x8] = -valueVSpan[i + 0x8];
                resultVSpan[i + 0x9] = -valueVSpan[i + 0x9];
                resultVSpan[i + 0xA] = -valueVSpan[i + 0xA];
                resultVSpan[i + 0xB] = -valueVSpan[i + 0xB];
                resultVSpan[i + 0xC] = -valueVSpan[i + 0xC];
                resultVSpan[i + 0xD] = -valueVSpan[i + 0xD];
                resultVSpan[i + 0xE] = -valueVSpan[i + 0xE];
                resultVSpan[i + 0xF] = -valueVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = -valueVSpan[i      ];
                resultVSpan[i + 0x1] = -valueVSpan[i + 0x1];
                resultVSpan[i + 0x2] = -valueVSpan[i + 0x2];
                resultVSpan[i + 0x3] = -valueVSpan[i + 0x3];
                resultVSpan[i + 0x4] = -valueVSpan[i + 0x4];
                resultVSpan[i + 0x5] = -valueVSpan[i + 0x5];
                resultVSpan[i + 0x6] = -valueVSpan[i + 0x6];
                resultVSpan[i + 0x7] = -valueVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = -valueVSpan[i      ];
                resultVSpan[i + 0x1] = -valueVSpan[i + 0x1];
                resultVSpan[i + 0x2] = -valueVSpan[i + 0x2];
                resultVSpan[i + 0x3] = -valueVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = -valueVSpan[i      ];
                resultVSpan[i + 0x1] = -valueVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = -valueVSpan[i      ];
                ++i;
            }

            var valueRemainSpan = value.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.UnaryNegate(valueRemainSpan[j]);
        }

        #endregion


        #region Complement

        /// <summary>
        ///     Applies complement operations for a n-d array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"> It can be same instance with <paramref name="value"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Complement<T>(INdArray<T> value, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(value.Shape == result.Shape, "There is shape mismatch.");
            if(value.TryGetBufferImpl(out var xvalue) && result is RawNdArray<T> xresult)
            {
                Complement(xvalue.Buffer, xresult.Entity.Buffer);
                return;
            }
            else
            {
                for(var i = 0; i < result.Length; ++i)
                    result.SetItem(i, ValueTrait.Complement(value.GetItem(i))); 
            }
        }

        /// <summary>
        ///     Applies complement operations for a memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException" />
        /// <exception cref="NotSupportedException" />
        public static void Complement<T>(ReadOnlyMemory<T> value, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= value.Length, "result.Length <= value.Length");
            if(typeof(T) == typeof(byte))
            {
                ComplementOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref value),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                ComplementOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref value),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                ComplementOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref value),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                ComplementOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref value),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                ComplementOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref value),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                ComplementOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref value),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                ComplementOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref value),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                ComplementOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref value),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Complement(value.Span[i]);
                return;
            }
        }

        private static void ComplementOptimized<T>(ReadOnlyMemory<T> value, Memory<T> result)
            where T : unmanaged
        {
            var valueVSpan = MemoryMarshal.Cast<T, Vector<T>>(value.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = ~valueVSpan[i      ];
                resultVSpan[i + 0x1] = ~valueVSpan[i + 0x1];
                resultVSpan[i + 0x2] = ~valueVSpan[i + 0x2];
                resultVSpan[i + 0x3] = ~valueVSpan[i + 0x3];
                resultVSpan[i + 0x4] = ~valueVSpan[i + 0x4];
                resultVSpan[i + 0x5] = ~valueVSpan[i + 0x5];
                resultVSpan[i + 0x6] = ~valueVSpan[i + 0x6];
                resultVSpan[i + 0x7] = ~valueVSpan[i + 0x7];
                resultVSpan[i + 0x8] = ~valueVSpan[i + 0x8];
                resultVSpan[i + 0x9] = ~valueVSpan[i + 0x9];
                resultVSpan[i + 0xA] = ~valueVSpan[i + 0xA];
                resultVSpan[i + 0xB] = ~valueVSpan[i + 0xB];
                resultVSpan[i + 0xC] = ~valueVSpan[i + 0xC];
                resultVSpan[i + 0xD] = ~valueVSpan[i + 0xD];
                resultVSpan[i + 0xE] = ~valueVSpan[i + 0xE];
                resultVSpan[i + 0xF] = ~valueVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = ~valueVSpan[i      ];
                resultVSpan[i + 0x1] = ~valueVSpan[i + 0x1];
                resultVSpan[i + 0x2] = ~valueVSpan[i + 0x2];
                resultVSpan[i + 0x3] = ~valueVSpan[i + 0x3];
                resultVSpan[i + 0x4] = ~valueVSpan[i + 0x4];
                resultVSpan[i + 0x5] = ~valueVSpan[i + 0x5];
                resultVSpan[i + 0x6] = ~valueVSpan[i + 0x6];
                resultVSpan[i + 0x7] = ~valueVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = ~valueVSpan[i      ];
                resultVSpan[i + 0x1] = ~valueVSpan[i + 0x1];
                resultVSpan[i + 0x2] = ~valueVSpan[i + 0x2];
                resultVSpan[i + 0x3] = ~valueVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = ~valueVSpan[i      ];
                resultVSpan[i + 0x1] = ~valueVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = ~valueVSpan[i      ];
                ++i;
            }

            var valueRemainSpan = value.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Complement(valueRemainSpan[j]);
        }

        #endregion


        #region Add

        /// <summary>
        ///     Applies add operations for n-d arrays pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/> and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Add<T>(INdArray<T> lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                Add(xlhs.Buffer, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies add operations for a scalar and a n-d array pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Add<T>(T lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                Add(lhs, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies add operations for a n-d array and a scalar pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Add<T>(INdArray<T> lhs, T rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && result is RawNdArray<T> xresult)
            {
                Add(xlhs.Buffer, rhs, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies add operations for memory sequences pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Add<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(byte))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(typeof(T) == typeof(float))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Add(lhs.Span[i], rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies add operations for a scalar and a memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Add<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(byte))
            {
                AddOptimized(
                    Unsafe.As<T, byte>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                AddOptimized(
                    Unsafe.As<T, ushort>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                AddOptimized(
                    Unsafe.As<T, uint>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                AddOptimized(
                    Unsafe.As<T, ulong>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                AddOptimized(
                    Unsafe.As<T, sbyte>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                AddOptimized(
                    Unsafe.As<T, short>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                AddOptimized(
                    Unsafe.As<T, int>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                AddOptimized(
                    Unsafe.As<T, long>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(typeof(T) == typeof(float))
            {
                AddOptimized(
                    Unsafe.As<T, float>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                AddOptimized(
                    Unsafe.As<T, double>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Add(lhs, rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies add operations for a memory sequence and a scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Add<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            if(typeof(T) == typeof(byte))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref lhs),
                    Unsafe.As<T, byte>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref lhs),
                    Unsafe.As<T, ushort>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref lhs),
                    Unsafe.As<T, uint>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref lhs),
                    Unsafe.As<T, ulong>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref lhs),
                    Unsafe.As<T, sbyte>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<T, short>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<T, int>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref lhs),
                    Unsafe.As<T, long>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(typeof(T) == typeof(float))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref lhs),
                    Unsafe.As<T, float>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                AddOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref lhs),
                    Unsafe.As<T, double>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Add(lhs.Span[i], rhs);
                return;
            }
        }

        private static void AddOptimized<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] + rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] + rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] + rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] + rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] + rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] + rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] + rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] + rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] + rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] + rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] + rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] + rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] + rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] + rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] + rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] + rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] + rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] + rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] + rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] + rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] + rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] + rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] + rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] + rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] + rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] + rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] + rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] + rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] + rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] + rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] + rhsVSpan[i      ];
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Add(lhsRemainSpan[j], rhsRemainSpan[j]);
        }

        private static void AddOptimized<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVector = new Vector<T>(lhs);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVector + rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector + rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector + rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector + rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector + rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector + rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector + rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector + rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVector + rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVector + rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVector + rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVector + rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVector + rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVector + rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVector + rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVector + rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVector + rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector + rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector + rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector + rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector + rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector + rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector + rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector + rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVector + rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector + rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector + rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector + rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVector + rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector + rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVector + rhsVSpan[i      ];
                ++i;
            }

            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Add(lhs, rhsRemainSpan[j]);
        }

        private static void AddOptimized<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVector = new Vector<T>(rhs);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] + rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] + rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] + rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] + rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] + rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] + rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] + rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] + rhsVector;
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] + rhsVector;
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] + rhsVector;
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] + rhsVector;
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] + rhsVector;
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] + rhsVector;
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] + rhsVector;
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] + rhsVector;
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] + rhsVector;
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] + rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] + rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] + rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] + rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] + rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] + rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] + rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] + rhsVector;
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] + rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] + rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] + rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] + rhsVector;
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] + rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] + rhsVector;
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] + rhsVector;
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Add(lhsRemainSpan[j], rhs);
        }

        #endregion


        #region Subtract

        /// <summary>
        ///     Applies subtract operations for n-d arrays pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/> and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Subtract<T>(INdArray<T> lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                Subtract(xlhs.Buffer, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies subtract operations for a scalar and a n-d array pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Subtract<T>(T lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                Subtract(lhs, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies subtract operations for a n-d array and a scalar pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Subtract<T>(INdArray<T> lhs, T rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && result is RawNdArray<T> xresult)
            {
                Subtract(xlhs.Buffer, rhs, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies subtract operations for memory sequences pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Subtract<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(byte))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(typeof(T) == typeof(float))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Subtract(lhs.Span[i], rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies subtract operations for a scalar and a memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Subtract<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(byte))
            {
                SubtractOptimized(
                    Unsafe.As<T, byte>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                SubtractOptimized(
                    Unsafe.As<T, ushort>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                SubtractOptimized(
                    Unsafe.As<T, uint>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                SubtractOptimized(
                    Unsafe.As<T, ulong>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                SubtractOptimized(
                    Unsafe.As<T, sbyte>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                SubtractOptimized(
                    Unsafe.As<T, short>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                SubtractOptimized(
                    Unsafe.As<T, int>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                SubtractOptimized(
                    Unsafe.As<T, long>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(typeof(T) == typeof(float))
            {
                SubtractOptimized(
                    Unsafe.As<T, float>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                SubtractOptimized(
                    Unsafe.As<T, double>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Subtract(lhs, rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies subtract operations for a memory sequence and a scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Subtract<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            if(typeof(T) == typeof(byte))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref lhs),
                    Unsafe.As<T, byte>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref lhs),
                    Unsafe.As<T, ushort>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref lhs),
                    Unsafe.As<T, uint>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref lhs),
                    Unsafe.As<T, ulong>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref lhs),
                    Unsafe.As<T, sbyte>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<T, short>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<T, int>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref lhs),
                    Unsafe.As<T, long>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(typeof(T) == typeof(float))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref lhs),
                    Unsafe.As<T, float>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                SubtractOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref lhs),
                    Unsafe.As<T, double>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Subtract(lhs.Span[i], rhs);
                return;
            }
        }

        private static void SubtractOptimized<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] - rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] - rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] - rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] - rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] - rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] - rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] - rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] - rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] - rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] - rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] - rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] - rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] - rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] - rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] - rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] - rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] - rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] - rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] - rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] - rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] - rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] - rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] - rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] - rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] - rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] - rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] - rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] - rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] - rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] - rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] - rhsVSpan[i      ];
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Subtract(lhsRemainSpan[j], rhsRemainSpan[j]);
        }

        private static void SubtractOptimized<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVector = new Vector<T>(lhs);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVector - rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector - rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector - rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector - rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector - rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector - rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector - rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector - rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVector - rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVector - rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVector - rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVector - rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVector - rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVector - rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVector - rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVector - rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVector - rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector - rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector - rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector - rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector - rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector - rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector - rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector - rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVector - rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector - rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector - rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector - rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVector - rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector - rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVector - rhsVSpan[i      ];
                ++i;
            }

            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Subtract(lhs, rhsRemainSpan[j]);
        }

        private static void SubtractOptimized<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVector = new Vector<T>(rhs);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] - rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] - rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] - rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] - rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] - rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] - rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] - rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] - rhsVector;
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] - rhsVector;
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] - rhsVector;
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] - rhsVector;
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] - rhsVector;
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] - rhsVector;
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] - rhsVector;
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] - rhsVector;
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] - rhsVector;
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] - rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] - rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] - rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] - rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] - rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] - rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] - rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] - rhsVector;
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] - rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] - rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] - rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] - rhsVector;
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] - rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] - rhsVector;
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] - rhsVector;
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Subtract(lhsRemainSpan[j], rhs);
        }

        #endregion


        #region Multiply

        /// <summary>
        ///     Applies multiply operations for n-d arrays pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/> and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Multiply<T>(INdArray<T> lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                Multiply(xlhs.Buffer, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies multiply operations for a scalar and a n-d array pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Multiply<T>(T lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                Multiply(lhs, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies multiply operations for a n-d array and a scalar pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Multiply<T>(INdArray<T> lhs, T rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && result is RawNdArray<T> xresult)
            {
                Multiply(xlhs.Buffer, rhs, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies multiply operations for memory sequences pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Multiply<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(short))
            {
                MultiplyOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                MultiplyOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(float))
            {
                MultiplyOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                MultiplyOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Multiply(lhs.Span[i], rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies multiply operations for a scalar and a memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Multiply<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(short))
            {
                MultiplyOptimized(
                    Unsafe.As<T, short>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                MultiplyOptimized(
                    Unsafe.As<T, int>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(float))
            {
                MultiplyOptimized(
                    Unsafe.As<T, float>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                MultiplyOptimized(
                    Unsafe.As<T, double>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Multiply(lhs, rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies multiply operations for a memory sequence and a scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Multiply<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            if(typeof(T) == typeof(short))
            {
                MultiplyOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<T, short>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                MultiplyOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<T, int>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(float))
            {
                MultiplyOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref lhs),
                    Unsafe.As<T, float>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                MultiplyOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref lhs),
                    Unsafe.As<T, double>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Multiply(lhs.Span[i], rhs);
                return;
            }
        }

        private static void MultiplyOptimized<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] * rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] * rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] * rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] * rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] * rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] * rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] * rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] * rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] * rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] * rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] * rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] * rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] * rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] * rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] * rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] * rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] * rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] * rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] * rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] * rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] * rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] * rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] * rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] * rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] * rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] * rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] * rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] * rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] * rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] * rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] * rhsVSpan[i      ];
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Multiply(lhsRemainSpan[j], rhsRemainSpan[j]);
        }

        private static void MultiplyOptimized<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVector = new Vector<T>(lhs);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVector * rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector * rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector * rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector * rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector * rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector * rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector * rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector * rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVector * rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVector * rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVector * rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVector * rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVector * rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVector * rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVector * rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVector * rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVector * rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector * rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector * rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector * rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector * rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector * rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector * rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector * rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVector * rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector * rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector * rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector * rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVector * rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector * rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVector * rhsVSpan[i      ];
                ++i;
            }

            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Multiply(lhs, rhsRemainSpan[j]);
        }

        private static void MultiplyOptimized<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVector = new Vector<T>(rhs);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] * rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] * rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] * rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] * rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] * rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] * rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] * rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] * rhsVector;
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] * rhsVector;
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] * rhsVector;
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] * rhsVector;
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] * rhsVector;
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] * rhsVector;
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] * rhsVector;
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] * rhsVector;
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] * rhsVector;
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] * rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] * rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] * rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] * rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] * rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] * rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] * rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] * rhsVector;
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] * rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] * rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] * rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] * rhsVector;
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] * rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] * rhsVector;
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] * rhsVector;
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Multiply(lhsRemainSpan[j], rhs);
        }

        #endregion


        #region Divide

        /// <summary>
        ///     Applies divide operations for n-d arrays pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/> and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Divide<T>(INdArray<T> lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                Divide(xlhs.Buffer, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies divide operations for a scalar and a n-d array pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Divide<T>(T lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                Divide(lhs, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies divide operations for a n-d array and a scalar pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Divide<T>(INdArray<T> lhs, T rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && result is RawNdArray<T> xresult)
            {
                Divide(xlhs.Buffer, rhs, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies divide operations for memory sequences pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Divide<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(float))
            {
                DivideOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                DivideOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Divide(lhs.Span[i], rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies divide operations for a scalar and a memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Divide<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(float))
            {
                DivideOptimized(
                    Unsafe.As<T, float>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                DivideOptimized(
                    Unsafe.As<T, double>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Divide(lhs, rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies divide operations for a memory sequence and a scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Divide<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            if(typeof(T) == typeof(float))
            {
                DivideOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<float>>(ref lhs),
                    Unsafe.As<T, float>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<float>>(ref result));
                return;
            }
            if(typeof(T) == typeof(double))
            {
                DivideOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<double>>(ref lhs),
                    Unsafe.As<T, double>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<double>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.Divide(lhs.Span[i], rhs);
                return;
            }
        }

        private static void DivideOptimized<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] / rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] / rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] / rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] / rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] / rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] / rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] / rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] / rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] / rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] / rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] / rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] / rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] / rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] / rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] / rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] / rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] / rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] / rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] / rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] / rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] / rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] / rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] / rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] / rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] / rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] / rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] / rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] / rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] / rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] / rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] / rhsVSpan[i      ];
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Divide(lhsRemainSpan[j], rhsRemainSpan[j]);
        }

        private static void DivideOptimized<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVector = new Vector<T>(lhs);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVector / rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector / rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector / rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector / rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector / rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector / rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector / rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector / rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVector / rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVector / rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVector / rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVector / rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVector / rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVector / rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVector / rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVector / rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVector / rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector / rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector / rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector / rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector / rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector / rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector / rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector / rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVector / rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector / rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector / rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector / rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVector / rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector / rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVector / rhsVSpan[i      ];
                ++i;
            }

            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Divide(lhs, rhsRemainSpan[j]);
        }

        private static void DivideOptimized<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVector = new Vector<T>(rhs);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] / rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] / rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] / rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] / rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] / rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] / rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] / rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] / rhsVector;
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] / rhsVector;
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] / rhsVector;
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] / rhsVector;
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] / rhsVector;
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] / rhsVector;
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] / rhsVector;
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] / rhsVector;
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] / rhsVector;
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] / rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] / rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] / rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] / rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] / rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] / rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] / rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] / rhsVector;
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] / rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] / rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] / rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] / rhsVector;
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] / rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] / rhsVector;
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] / rhsVector;
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.Divide(lhsRemainSpan[j], rhs);
        }

        #endregion


        #region BitwiseAnd

        /// <summary>
        ///     Applies bitwiseand operations for n-d arrays pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/> and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void BitwiseAnd<T>(INdArray<T> lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                BitwiseAnd(xlhs.Buffer, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwiseand operations for a scalar and a n-d array pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void BitwiseAnd<T>(T lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                BitwiseAnd(lhs, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwiseand operations for a n-d array and a scalar pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void BitwiseAnd<T>(INdArray<T> lhs, T rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && result is RawNdArray<T> xresult)
            {
                BitwiseAnd(xlhs.Buffer, rhs, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwiseand operations for memory sequences pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void BitwiseAnd<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(byte))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.BitwiseAnd(lhs.Span[i], rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwiseand operations for a scalar and a memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void BitwiseAnd<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(byte))
            {
                BitwiseAndOptimized(
                    Unsafe.As<T, byte>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                BitwiseAndOptimized(
                    Unsafe.As<T, ushort>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                BitwiseAndOptimized(
                    Unsafe.As<T, uint>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                BitwiseAndOptimized(
                    Unsafe.As<T, ulong>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                BitwiseAndOptimized(
                    Unsafe.As<T, sbyte>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                BitwiseAndOptimized(
                    Unsafe.As<T, short>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                BitwiseAndOptimized(
                    Unsafe.As<T, int>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                BitwiseAndOptimized(
                    Unsafe.As<T, long>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.BitwiseAnd(lhs, rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwiseand operations for a memory sequence and a scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void BitwiseAnd<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            if(typeof(T) == typeof(byte))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref lhs),
                    Unsafe.As<T, byte>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref lhs),
                    Unsafe.As<T, ushort>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref lhs),
                    Unsafe.As<T, uint>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref lhs),
                    Unsafe.As<T, ulong>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref lhs),
                    Unsafe.As<T, sbyte>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<T, short>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<T, int>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                BitwiseAndOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref lhs),
                    Unsafe.As<T, long>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.BitwiseAnd(lhs.Span[i], rhs);
                return;
            }
        }

        private static void BitwiseAndOptimized<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] & rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] & rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] & rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] & rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] & rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] & rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] & rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] & rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] & rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] & rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] & rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] & rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] & rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] & rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] & rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] & rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] & rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] & rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] & rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] & rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] & rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] & rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] & rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] & rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] & rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] & rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] & rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] & rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] & rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] & rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] & rhsVSpan[i      ];
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.BitwiseAnd(lhsRemainSpan[j], rhsRemainSpan[j]);
        }

        private static void BitwiseAndOptimized<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVector = new Vector<T>(lhs);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVector & rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector & rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector & rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector & rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector & rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector & rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector & rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector & rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVector & rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVector & rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVector & rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVector & rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVector & rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVector & rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVector & rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVector & rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVector & rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector & rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector & rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector & rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector & rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector & rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector & rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector & rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVector & rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector & rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector & rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector & rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVector & rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector & rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVector & rhsVSpan[i      ];
                ++i;
            }

            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.BitwiseAnd(lhs, rhsRemainSpan[j]);
        }

        private static void BitwiseAndOptimized<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVector = new Vector<T>(rhs);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] & rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] & rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] & rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] & rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] & rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] & rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] & rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] & rhsVector;
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] & rhsVector;
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] & rhsVector;
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] & rhsVector;
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] & rhsVector;
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] & rhsVector;
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] & rhsVector;
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] & rhsVector;
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] & rhsVector;
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] & rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] & rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] & rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] & rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] & rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] & rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] & rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] & rhsVector;
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] & rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] & rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] & rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] & rhsVector;
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] & rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] & rhsVector;
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] & rhsVector;
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.BitwiseAnd(lhsRemainSpan[j], rhs);
        }

        #endregion


        #region BitwiseOr

        /// <summary>
        ///     Applies bitwiseor operations for n-d arrays pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/> and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void BitwiseOr<T>(INdArray<T> lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                BitwiseOr(xlhs.Buffer, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwiseor operations for a scalar and a n-d array pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void BitwiseOr<T>(T lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                BitwiseOr(lhs, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwiseor operations for a n-d array and a scalar pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void BitwiseOr<T>(INdArray<T> lhs, T rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && result is RawNdArray<T> xresult)
            {
                BitwiseOr(xlhs.Buffer, rhs, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwiseor operations for memory sequences pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void BitwiseOr<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(byte))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.BitwiseOr(lhs.Span[i], rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwiseor operations for a scalar and a memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void BitwiseOr<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(byte))
            {
                BitwiseOrOptimized(
                    Unsafe.As<T, byte>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                BitwiseOrOptimized(
                    Unsafe.As<T, ushort>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                BitwiseOrOptimized(
                    Unsafe.As<T, uint>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                BitwiseOrOptimized(
                    Unsafe.As<T, ulong>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                BitwiseOrOptimized(
                    Unsafe.As<T, sbyte>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                BitwiseOrOptimized(
                    Unsafe.As<T, short>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                BitwiseOrOptimized(
                    Unsafe.As<T, int>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                BitwiseOrOptimized(
                    Unsafe.As<T, long>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.BitwiseOr(lhs, rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwiseor operations for a memory sequence and a scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void BitwiseOr<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            if(typeof(T) == typeof(byte))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref lhs),
                    Unsafe.As<T, byte>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref lhs),
                    Unsafe.As<T, ushort>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref lhs),
                    Unsafe.As<T, uint>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref lhs),
                    Unsafe.As<T, ulong>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref lhs),
                    Unsafe.As<T, sbyte>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<T, short>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<T, int>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                BitwiseOrOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref lhs),
                    Unsafe.As<T, long>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.BitwiseOr(lhs.Span[i], rhs);
                return;
            }
        }

        private static void BitwiseOrOptimized<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] | rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] | rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] | rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] | rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] | rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] | rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] | rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] | rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] | rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] | rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] | rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] | rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] | rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] | rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] | rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] | rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] | rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] | rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] | rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] | rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] | rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] | rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] | rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] | rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] | rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] | rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] | rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] | rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] | rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] | rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] | rhsVSpan[i      ];
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.BitwiseOr(lhsRemainSpan[j], rhsRemainSpan[j]);
        }

        private static void BitwiseOrOptimized<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVector = new Vector<T>(lhs);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVector | rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector | rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector | rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector | rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector | rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector | rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector | rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector | rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVector | rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVector | rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVector | rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVector | rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVector | rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVector | rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVector | rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVector | rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVector | rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector | rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector | rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector | rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector | rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector | rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector | rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector | rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVector | rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector | rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector | rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector | rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVector | rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector | rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVector | rhsVSpan[i      ];
                ++i;
            }

            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.BitwiseOr(lhs, rhsRemainSpan[j]);
        }

        private static void BitwiseOrOptimized<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVector = new Vector<T>(rhs);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] | rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] | rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] | rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] | rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] | rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] | rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] | rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] | rhsVector;
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] | rhsVector;
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] | rhsVector;
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] | rhsVector;
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] | rhsVector;
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] | rhsVector;
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] | rhsVector;
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] | rhsVector;
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] | rhsVector;
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] | rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] | rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] | rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] | rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] | rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] | rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] | rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] | rhsVector;
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] | rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] | rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] | rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] | rhsVector;
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] | rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] | rhsVector;
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] | rhsVector;
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.BitwiseOr(lhsRemainSpan[j], rhs);
        }

        #endregion


        #region BitwiseXor

        /// <summary>
        ///     Applies bitwisexor operations for n-d arrays pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/> and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void BitwiseXor<T>(INdArray<T> lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                BitwiseXor(xlhs.Buffer, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwisexor operations for a scalar and a n-d array pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Shape &lt;= rhs.Shape</c>] </param>
        /// <param name="result"> It can be same instance with and <paramref name="rhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void BitwiseXor<T>(T lhs, INdArray<T> rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(rhs.Shape == result.Shape, "The shape of " + nameof(rhs) + " and " + nameof(rhs) + " are mismatched.");
            if(rhs.TryGetBufferImpl(out var xrhs) && result is RawNdArray<T> xresult)
            {
                BitwiseXor(lhs, xrhs.Buffer, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwisexor operations for a n-d array and a scalar pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Shape &lt;= lhs.Shape</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"> It can be same instance with <paramref name="lhs"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void BitwiseXor<T>(INdArray<T> lhs, T rhs, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(lhs.Shape == result.Shape, "The shape of " + nameof(lhs) + " and " + nameof(rhs) + " are mismatched.");
            if(lhs.TryGetBufferImpl(out var xlhs) && result is RawNdArray<T> xresult)
            {
                BitwiseXor(xlhs.Buffer, rhs, xresult.Entity.Buffer);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwisexor operations for memory sequences pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void BitwiseXor<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(byte))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.BitwiseXor(lhs.Span[i], rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwisexor operations for a scalar and a memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"> [<c>result.Length &lt;= rhs.Length</c>] </param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void BitwiseXor<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= rhs.Length, "result.Length <= rhs.Length");
            if(typeof(T) == typeof(byte))
            {
                BitwiseXorOptimized(
                    Unsafe.As<T, byte>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                BitwiseXorOptimized(
                    Unsafe.As<T, ushort>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                BitwiseXorOptimized(
                    Unsafe.As<T, uint>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                BitwiseXorOptimized(
                    Unsafe.As<T, ulong>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                BitwiseXorOptimized(
                    Unsafe.As<T, sbyte>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                BitwiseXorOptimized(
                    Unsafe.As<T, short>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                BitwiseXorOptimized(
                    Unsafe.As<T, int>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                BitwiseXorOptimized(
                    Unsafe.As<T, long>(ref lhs),
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.BitwiseXor(lhs, rhs.Span[i]);
                return;
            }
        }

        /// <summary>
        ///     Applies bitwisexor operations for a memory sequence and a scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"> [<c>result.Length &lt;= lhs.Length</c>] </param>
        /// <param name="rhs"></param>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void BitwiseXor<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= lhs.Length, "result.Length <= lhs.Length");
            if(typeof(T) == typeof(byte))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<byte>>(ref lhs),
                    Unsafe.As<T, byte>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<byte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ushort))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ushort>>(ref lhs),
                    Unsafe.As<T, ushort>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ushort>>(ref result));
                return;
            }
            if(typeof(T) == typeof(uint))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<uint>>(ref lhs),
                    Unsafe.As<T, uint>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<uint>>(ref result));
                return;
            }
            if(typeof(T) == typeof(ulong))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<ulong>>(ref lhs),
                    Unsafe.As<T, ulong>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<ulong>>(ref result));
                return;
            }
            if(typeof(T) == typeof(sbyte))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<sbyte>>(ref lhs),
                    Unsafe.As<T, sbyte>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<sbyte>>(ref result));
                return;
            }
            if(typeof(T) == typeof(short))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<short>>(ref lhs),
                    Unsafe.As<T, short>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<short>>(ref result));
                return;
            }
            if(typeof(T) == typeof(int))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<int>>(ref lhs),
                    Unsafe.As<T, int>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<int>>(ref result));
                return;
            }
            if(typeof(T) == typeof(long))
            {
                BitwiseXorOptimized(
                    Unsafe.As<ReadOnlyMemory<T>, ReadOnlyMemory<long>>(ref lhs),
                    Unsafe.As<T, long>(ref rhs),
                    Unsafe.As<Memory<T>, Memory<long>>(ref result));
                return;
            }
            if(true)
            {
                for(var i = 0; i < result.Length; ++i)
                    result.Span[i] = ValueTrait.BitwiseXor(lhs.Span[i], rhs);
                return;
            }
        }

        private static void BitwiseXorOptimized<T>(ReadOnlyMemory<T> lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] ^ rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] ^ rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] ^ rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] ^ rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] ^ rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] ^ rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] ^ rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] ^ rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] ^ rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] ^ rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] ^ rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] ^ rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] ^ rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] ^ rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] ^ rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] ^ rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] ^ rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] ^ rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] ^ rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] ^ rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] ^ rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] ^ rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] ^ rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] ^ rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] ^ rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] ^ rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] ^ rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] ^ rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] ^ rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] ^ rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] ^ rhsVSpan[i      ];
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.BitwiseXor(lhsRemainSpan[j], rhsRemainSpan[j]);
        }

        private static void BitwiseXorOptimized<T>(T lhs, ReadOnlyMemory<T> rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVector = new Vector<T>(lhs);
            var rhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(rhs.Span);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVector ^ rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector ^ rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector ^ rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector ^ rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector ^ rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector ^ rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector ^ rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector ^ rhsVSpan[i + 0x7];
                resultVSpan[i + 0x8] = lhsVector ^ rhsVSpan[i + 0x8];
                resultVSpan[i + 0x9] = lhsVector ^ rhsVSpan[i + 0x9];
                resultVSpan[i + 0xA] = lhsVector ^ rhsVSpan[i + 0xA];
                resultVSpan[i + 0xB] = lhsVector ^ rhsVSpan[i + 0xB];
                resultVSpan[i + 0xC] = lhsVector ^ rhsVSpan[i + 0xC];
                resultVSpan[i + 0xD] = lhsVector ^ rhsVSpan[i + 0xD];
                resultVSpan[i + 0xE] = lhsVector ^ rhsVSpan[i + 0xE];
                resultVSpan[i + 0xF] = lhsVector ^ rhsVSpan[i + 0xF];
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVector ^ rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector ^ rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector ^ rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector ^ rhsVSpan[i + 0x3];
                resultVSpan[i + 0x4] = lhsVector ^ rhsVSpan[i + 0x4];
                resultVSpan[i + 0x5] = lhsVector ^ rhsVSpan[i + 0x5];
                resultVSpan[i + 0x6] = lhsVector ^ rhsVSpan[i + 0x6];
                resultVSpan[i + 0x7] = lhsVector ^ rhsVSpan[i + 0x7];
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVector ^ rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector ^ rhsVSpan[i + 0x1];
                resultVSpan[i + 0x2] = lhsVector ^ rhsVSpan[i + 0x2];
                resultVSpan[i + 0x3] = lhsVector ^ rhsVSpan[i + 0x3];
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVector ^ rhsVSpan[i      ];
                resultVSpan[i + 0x1] = lhsVector ^ rhsVSpan[i + 0x1];
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVector ^ rhsVSpan[i      ];
                ++i;
            }

            var rhsRemainSpan = rhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.BitwiseXor(lhs, rhsRemainSpan[j]);
        }

        private static void BitwiseXorOptimized<T>(ReadOnlyMemory<T> lhs, T rhs, Memory<T> result)
            where T : unmanaged
        {
            var lhsVSpan = MemoryMarshal.Cast<T, Vector<T>>(lhs.Span);
            var rhsVector = new Vector<T>(rhs);
            var resultVSpan = MemoryMarshal.Cast<T, Vector<T>>(result.Span);
            var i = 0;
            for(var len = resultVSpan.Length & ~0b1111 ; i < len ; i += 16)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] ^ rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] ^ rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] ^ rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] ^ rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] ^ rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] ^ rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] ^ rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] ^ rhsVector;
                resultVSpan[i + 0x8] = lhsVSpan[i + 0x8] ^ rhsVector;
                resultVSpan[i + 0x9] = lhsVSpan[i + 0x9] ^ rhsVector;
                resultVSpan[i + 0xA] = lhsVSpan[i + 0xA] ^ rhsVector;
                resultVSpan[i + 0xB] = lhsVSpan[i + 0xB] ^ rhsVector;
                resultVSpan[i + 0xC] = lhsVSpan[i + 0xC] ^ rhsVector;
                resultVSpan[i + 0xD] = lhsVSpan[i + 0xD] ^ rhsVector;
                resultVSpan[i + 0xE] = lhsVSpan[i + 0xE] ^ rhsVector;
                resultVSpan[i + 0xF] = lhsVSpan[i + 0xF] ^ rhsVector;
            }
            if(i < (resultVSpan.Length & ~0b111))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] ^ rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] ^ rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] ^ rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] ^ rhsVector;
                resultVSpan[i + 0x4] = lhsVSpan[i + 0x4] ^ rhsVector;
                resultVSpan[i + 0x5] = lhsVSpan[i + 0x5] ^ rhsVector;
                resultVSpan[i + 0x6] = lhsVSpan[i + 0x6] ^ rhsVector;
                resultVSpan[i + 0x7] = lhsVSpan[i + 0x7] ^ rhsVector;
                i += 8;
            }
            if(i < (resultVSpan.Length & ~0b11))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] ^ rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] ^ rhsVector;
                resultVSpan[i + 0x2] = lhsVSpan[i + 0x2] ^ rhsVector;
                resultVSpan[i + 0x3] = lhsVSpan[i + 0x3] ^ rhsVector;
                i += 4;
            }
            if(i < (resultVSpan.Length & ~0b1))
            {
                resultVSpan[i      ] = lhsVSpan[i      ] ^ rhsVector;
                resultVSpan[i + 0x1] = lhsVSpan[i + 0x1] ^ rhsVector;
                i += 2;
            }
            if(i < resultVSpan.Length)
            {
                resultVSpan[i      ] = lhsVSpan[i      ] ^ rhsVector;
                ++i;
            }

            var lhsRemainSpan = lhs.Span.Slice(Vector<T>.Count * i);
            var resultRemainSpan = result.Span.Slice(Vector<T>.Count * i);
            for(var j = 0; j < resultRemainSpan.Length; ++j)
                resultRemainSpan[j] = ValueTrait.BitwiseXor(lhsRemainSpan[j], rhs);
        }

        #endregion

    }
}

