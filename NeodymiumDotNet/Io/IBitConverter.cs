using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace NeodymiumDotNet.Io
{
    /// <summary>
    ///     Represents conversion between primitive data type and byte sequence.
    /// </summary>
    public interface IBitConverter
    {
        /// <summary>
        ///     Tries to read primitive value from a byte sequence.
        /// </summary>
        /// <typeparam name="TPrimitive"> A reading data type. </typeparam>
        /// <param name="src"> A byte sequence span. </param>
        /// <param name="value"> A result destination if reading was succeeded. </param>
        /// <returns>
        ///     <c>true</c> if reading was succeeded;
        ///     "false" if the length of <paramref name="src"/> is shorter than size of <typeparamref name="TPrimitive"/>.
        /// </returns>
        bool TryReadPrimitive<TPrimitive>(ReadOnlySpan<byte> src, out TPrimitive value)
            where TPrimitive : unmanaged;


        /// <summary>
        ///     Tries to read primitive values from a byte sequence.
        /// </summary>
        /// <typeparam name="TPrimitive"> A read data type. </typeparam>
        /// <param name="src"> A byte sequence span. </param>
        /// <param name="values"> A result destination if reading was succeeded. </param>
        /// <returns>
        ///     <c>true</c> if reading was succeeded;
        ///     "false" if the length of <paramref name="src"/> is shorter than size of <paramref name="values"/>.
        /// </returns>
        bool TryReadPrimitives<TPrimitive>(ReadOnlySpan<byte> src, Span<TPrimitive> values)
            where TPrimitive : unmanaged;


        /// <summary>
        ///     Tries to write primitive value to a byte sequence.
        /// </summary>
        /// <typeparam name="TPrimitive"> A writing data type. </typeparam>
        /// <param name="dst"> A result destination if reading was succeeded. </param>
        /// <param name="value"> A writing value. </param>
        /// <returns>
        ///     <c>true</c> if writing was succeeded;
        ///     "false" if the length of <paramref name="dst"/> is shorter than size of <typeparamref name="TPrimitive"/>.
        /// </returns>
        bool TryWritePrimitive<TPrimitive>(Span<byte> dst, in TPrimitive value)
            where TPrimitive : unmanaged;


        /// <summary>
        ///     Tries to write primitive value to a byte sequence.
        /// </summary>
        /// <typeparam name="TPrimitive"> A writing data type. </typeparam>
        /// <param name="dst"> A result destination if reading was succeeded. </param>
        /// <param name="values"> A writing values. </param>
        /// <returns>
        ///     <c>true</c> if writing was succeeded;
        ///     "false" if the length of <paramref name="dst"/> is shorter than size of <paramref name="values"/>.
        /// </returns>
        bool TryWritePrimitives<TPrimitive>(Span<byte> dst, ReadOnlySpan<TPrimitive> values)
            where TPrimitive : unmanaged;
    }


    /// <summary>
    ///     Extension methods for <see cref="IBitConverter"/>.
    /// </summary>
    public static class BitConverterEx
    {
        /// <summary>
        ///     Reads a primitive value from a byte sequence.
        /// </summary>
        /// <typeparam name="TPrimitive"></typeparam>
        /// <param name="converter"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     Too short <paramref name="src"/>.
        /// </exception>
        public static TPrimitive ReadPrimitive<TPrimitive>(this IBitConverter converter, ReadOnlySpan<byte> src)
            where TPrimitive : unmanaged
            => converter.TryReadPrimitive(src, out TPrimitive value)
                   ? value
                   : throw new ArgumentException("Too short src.");


        /// <summary>
        ///     Writes a primitive value to a byte sequence.
        /// </summary>
        /// <typeparam name="TPrimitive"></typeparam>
        /// <param name="converter"></param>
        /// <param name="dst"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException">
        ///     Too short <paramref name="dst"/>.
        /// </exception>
        public static void WritePrimitive<TPrimitive>(this IBitConverter converter, Span<byte> dst, TPrimitive value)
            where TPrimitive : unmanaged
        {
            if(converter.TryWritePrimitive(dst, value))
                throw new ArgumentException("Too short dst.");
        }

    }
}
