using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NeodymiumDotNet.Io
{
    /// <summary>
    ///     Provides features of <see cref="IBitConverter"/> which reads/writes byte sequence naturally.
    /// </summary>
    public class NormalBitConverter : IBitConverter
    {
        /// <summary>
        ///     The instance of <see cref="NormalBitConverter"/>.
        /// </summary>
        public static IBitConverter Instance => new NormalBitConverter();


        private NormalBitConverter()
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

            value = Unsafe.As<byte, TPrimitive>(ref Unsafe.AsRef(src[0]));
            return true;
        }


        /// <inheritdoc />
        public bool TryReadPrimitives<TPrimitive>(ReadOnlySpan<byte> src,
                                                  Span<TPrimitive> values)
            where TPrimitive : unmanaged
        {
            var size = Unsafe.SizeOf<TPrimitive>();
            var xlen = size * values.Length;
            if(src.Length < xlen)
            {
                return false;
            }

            MemoryMarshal
               .Cast<byte, TPrimitive>(src.Slice(0, xlen))
               .TryCopyTo(values);
            return true;
        }


        /// <inheritdoc />
        public bool TryWritePrimitive<TPrimitive>(Span<byte> dst, in TPrimitive value)
            where TPrimitive : unmanaged
        {
            var size = Unsafe.SizeOf<TPrimitive>();
            if(dst.Length < size)
                return false;

            Unsafe.As<byte, TPrimitive>(ref dst[0]) = value;
            return true;
        }


        /// <inheritdoc />
        public bool TryWritePrimitives<TPrimitive>(
            Span<byte> dst, ReadOnlySpan<TPrimitive> values)
            where TPrimitive : unmanaged
        {
            var size = Unsafe.SizeOf<TPrimitive>();
            var xlen = size * values.Length;
            if(dst.Length < xlen)
            {
                return false;
            }

            MemoryMarshal
               .Cast<TPrimitive, byte>(values)
               .TryCopyTo(dst);
            return true;
        }

    }
}
