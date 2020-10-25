using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NeodymiumDotNet.Linq;

namespace NeodymiumDotNet.Io.Numpy
{
    partial class NpyFile
    {

        private static IBitConverter LittleEndianConv => LittleEndiannessBitConverter.Instance;


        /// <summary>
        ///     Saves <see cref="NdArray{Boolean}"/> instance to file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="ndArray"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Save(string filepath, NdArray<bool> ndArray)
        {
            using(var stream = new FileStream(filepath, FileMode.OpenOrCreate))
                Save(stream, ndArray);
        }


        /// <summary>
        ///     Saves <see cref="NdArray{UInt8}"/> instance to file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="ndArray"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Save(string filepath, NdArray<byte> ndArray)
        {
            using(var stream = new FileStream(filepath, FileMode.OpenOrCreate))
                Save(stream, ndArray);
        }


        /// <summary>
        ///     Saves <see cref="NdArray{UInt16}"/> instance to file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="ndArray"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Save(string filepath, NdArray<ushort> ndArray)
        {
            using(var stream = new FileStream(filepath, FileMode.OpenOrCreate))
                Save(stream, ndArray);
        }


        /// <summary>
        ///     Saves <see cref="NdArray{UInt32}"/> instance to file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="ndArray"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Save(string filepath, NdArray<uint> ndArray)
        {
            using(var stream = new FileStream(filepath, FileMode.OpenOrCreate))
                Save(stream, ndArray);
        }


        /// <summary>
        ///     Saves <see cref="NdArray{UInt64g}"/> instance to file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="ndArray"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Save(string filepath, NdArray<ulong> ndArray)
        {
            using(var stream = new FileStream(filepath, FileMode.OpenOrCreate))
                Save(stream, ndArray);
        }


        /// <summary>
        ///     Saves <see cref="NdArray{Int8}"/> instance to file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="ndArray"></param>
        public static void Save(string filepath, NdArray<sbyte> ndArray)
        {
            using(var stream = new FileStream(filepath, FileMode.OpenOrCreate))
                Save(stream, ndArray);
        }


        /// <summary>
        ///     Saves <see cref="NdArray{Int16}"/> instance to file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="ndArray"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Save(string filepath, NdArray<short> ndArray)
        {
            using(var stream = new FileStream(filepath, FileMode.OpenOrCreate))
                Save(stream, ndArray);
        }


        /// <summary>
        ///     Saves <see cref="NdArray{Int32}"/> instance to file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="ndArray"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Save(string filepath, NdArray<int> ndArray)
        {
            using(var stream = new FileStream(filepath, FileMode.OpenOrCreate))
                Save(stream, ndArray);
        }


        /// <summary>
        ///     Saves <see cref="NdArray{Int64}"/> instance to file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="ndArray"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Save(string filepath, NdArray<long> ndArray)
        {
            using(var stream = new FileStream(filepath, FileMode.OpenOrCreate))
                Save(stream, ndArray);
        }


        /// <summary>
        ///     Saves <see cref="NdArray{String}"/> instance to file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="ndArray"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Save(string filepath, NdArray<string> ndArray)
        {
            using(var stream = new FileStream(filepath, FileMode.OpenOrCreate))
                Save(stream, ndArray);
        }


        /// <summary>
        ///     When <typeparamref name="T"/> is primitive type, saves <see cref="NdArray{T}"/> instance to .npy binary.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="ndArray"></param>
        /// <exception cref="NotSupportedException"></exception>
        /// <typeparam name="T"> A primitive type. </typeparam>
        public static void Save<T>(Stream stream, NdArray<T> ndArray)
            where T : unmanaged
            => SaveCore(stream,
                        ndArray,
                        DType.UInt16,
                        (i, buf) => LittleEndianConv.TryWritePrimitive(buf.AsSpan(), ndArray.GetItem(i)));


        /// <summary>
        ///     Saves <see cref="NdArray{String}"/> instance to .npy binary.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="ndArray"></param>
        /// <exception cref="NotSupportedException"></exception>
        public static void Save(Stream stream, NdArray<string> ndArray)
        {
            var temp = ndArray.Select(x => Encoding.UTF32.GetBytes(x));
            var maxlen = temp.AsEnumerable().Select(x => x.Length).Max();
            SaveCore(stream, temp, new DType($"<U{maxlen / 4}"), (i, buf) =>
            {
                var buffer = temp.GetItem(i);
                Buffer.BlockCopy(buffer, 0, buf, i * maxlen, buffer.Length);
            });
        }


        /// <summary>
        ///     Entity of .npy binary saving.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="ndArray"></param>
        /// <param name="dtype"></param>
        /// <param name="writeAt"></param>
        private static void SaveCore<T>(Stream stream, NdArray<T> ndArray, DType dtype,
                                        Action<int, byte[]> writeAt)
        {
            var header = new NpyHeader(1, 0, dtype, false, ndArray.Shape);
            var headerBytes = header.GenerateHeader();
            var bufferSize = ndArray.Shape.TotalLength;
            var buffer =
                new byte[dtype.ByteSize * bufferSize ?? throw new NotSupportedException()];
            for(var i = 0 ; i < bufferSize ; ++i)
                writeAt(i, buffer);

            stream.Write(headerBytes, 0, headerBytes.Length);
            stream.Write(buffer, 0, bufferSize);
        }

    }
}
