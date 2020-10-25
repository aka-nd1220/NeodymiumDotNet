using System;
using System.Collections.Generic;

namespace NeodymiumDotNet.Io
{
    /// <summary>
    ///     Provides features of <see cref="IBitConverter"/> for big endianness system.
    /// </summary>
    public static class BigEndiannessBitConverter
    {
        /// <summary>
        ///     The instance of <see cref="IBitConverter"/> for big endianness system.
        /// </summary>
        /// <remarks>
        ///     This is same with <see cref="ReverseBitConverter.Instance"/> if runtime byte order is big endianness.
        /// </remarks>
        public static IBitConverter Instance { get; }
            = BitConverter.IsLittleEndian
                  ? ReverseBitConverter.Instance
                  : NormalBitConverter.Instance;
    }
}
