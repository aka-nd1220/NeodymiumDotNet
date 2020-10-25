using System;
using System.Collections.Generic;

namespace NeodymiumDotNet.Io
{
    /// <summary>
    ///     Provides features of <see cref="IBitConverter"/> for little endianness system.
    /// </summary>
    public static class LittleEndiannessBitConverter
    {
        /// <summary>
        ///     The instance of <see cref="IBitConverter"/> for little endianness system.
        /// </summary>
        /// <remarks>
        ///     This is same with <see cref="NormalBitConverter.Instance"/> if runtime byte order is little endianness.
        /// </remarks>
        public static IBitConverter Instance { get; }
            = BitConverter.IsLittleEndian
                  ? NormalBitConverter.Instance
                  : ReverseBitConverter.Instance;
    }
}
