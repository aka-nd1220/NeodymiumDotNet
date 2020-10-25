using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NeodymiumDotNet.Io
{
    /// <summary>
    ///     Provides features of <see cref="IBitConverter"/> which reads/writes byte sequence with reversing.
    /// </summary>
    public class ReverseBitConverter : IBitConverter
    {
        /// <summary>
        ///     The instance of <see cref="ReverseBitConverter"/>.
        /// </summary>
        public static IBitConverter Instance => new ReverseBitConverter();


        private ReverseBitConverter()
        {
        }


        /// <inheritdoc />
        public bool TryReadPrimitive<TPrimitive>(ReadOnlySpan<byte> src, out TPrimitive value)
            where TPrimitive : unmanaged
        {
            var size = Unsafe.SizeOf<TPrimitive>();
            if(src.Length < size)
            {
                value = default;
                return false;
            }

            Span<byte> tmp = stackalloc byte[size];
            ReverseCopy(src.Slice(0, size), tmp);
            value = Unsafe.As<byte, TPrimitive>(ref Unsafe.AsRef(tmp[0]));
            return true;
        }


        /// <inheritdoc />
        public bool TryReadPrimitives<TPrimitive>(ReadOnlySpan<byte> src, Span<TPrimitive> values)
            where TPrimitive : unmanaged
        {
            var size = Unsafe.SizeOf<TPrimitive>();
            var xlen = size * values.Length;
            if(src.Length < xlen)
                return false;

            var dst = MemoryMarshal.Cast<TPrimitive, byte>(values);
            for(var i = 0 ; i < xlen ; i += size)
                ReverseCopy(src.Slice(i, size), dst.Slice(i, size));

            return true;
        }


        /// <inheritdoc />
        public bool TryWritePrimitive<TPrimitive>(Span<byte> dst, in TPrimitive value)
            where TPrimitive : unmanaged
        {
            var size = Unsafe.SizeOf<TPrimitive>();
            if(dst.Length < size)
                return false;

            Span<byte> tmp = stackalloc byte[size];
            Unsafe.As<byte, TPrimitive>(ref tmp[0]) = value;
            ReverseCopy(tmp, dst.Slice(0, size));
            return true;
        }


        /// <inheritdoc />
        public bool TryWritePrimitives<TPrimitive>(Span<byte> dst, ReadOnlySpan<TPrimitive> values)
            where TPrimitive : unmanaged
        {
            var size = Unsafe.SizeOf<TPrimitive>();
            var xlen = size * values.Length;
            if(dst.Length < xlen)
                return false;

            var src = MemoryMarshal.Cast<TPrimitive, byte>(values);
            for(var i = 0; i < xlen; i += size)
                ReverseCopy(src.Slice(i, size), dst.Slice(i, size));

            return true;
        }


        private static void ReverseCopy(ReadOnlySpan<byte> src, Span<byte> dst)
        {
            var len = src.Length;
            for(var i = 0; i < len; ++i)
                dst[len - i - 1] = src[i];
        }
    }
}
