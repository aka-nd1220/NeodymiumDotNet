using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NeodymiumDotNet.Linq;

namespace NeodymiumDotNet.Io.Numpy
{
    /// <summary>
    ///     File access for .npy format.
    /// </summary>
    public static partial class NpyFile
    {

        /// <summary>
        ///     Loads <see cref="NdArray{T}"/> instance from file with strict type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filepath"></param>
        /// <returns></returns>
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
        /// <exception cref="InvalidCastException"></exception>
        public static NdArray<T> Load<T>(string filepath)
        {
            using(var stream = new FileStream(filepath, FileMode.Open))
                return Load<T>(stream);
        }


        /// <summary>
        ///     Loads <see cref="NdArray{T}"/> instance from .npy binary stream with strict type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        public static NdArray<T> Load<T>(Stream stream)
            => LoadCore<T>(NpyHeader.LoadHeader(stream), stream);


        /// <summary>
        ///     Loads <see cref="NdArray{T}"/> instance from file with using implicit cast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filepath"></param>
        /// <returns></returns>
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
        /// <exception cref="InvalidCastException"></exception>
        public static NdArray<T> LoadAs<T>(string filepath)
        {
            using(var stream = new FileStream(filepath, FileMode.Open))
                return LoadAs<T>(stream);
        }


        /// <summary>
        ///     Loads <see cref="NdArray{T}"/> instance from .npy binary stream with using implicit cast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        public static NdArray<T> LoadAs<T>(Stream stream)
        {
            var header = NpyHeader.LoadHeader(stream);
            switch(header.NumpyType.TypeKind)
            {
            case TypeKind.Boolean:
                return LoadCore<bool  >(header, stream).Select(x => (T)(dynamic)x);
            case TypeKind.UInt8:
                return LoadCore<byte  >(header, stream).Select(x => (T)(dynamic)x);
            case TypeKind.UInt16:
                return LoadCore<ushort>(header, stream).Select(x => (T)(dynamic)x);
            case TypeKind.UInt32:
                return LoadCore<uint  >(header, stream).Select(x => (T)(dynamic)x);
            case TypeKind.UInt64:
                return LoadCore<ulong>(header, stream).Select(x => (T)(dynamic)x);
            case TypeKind.Int8:
                return LoadCore<sbyte>(header, stream).Select(x => (T)(dynamic)x);
            case TypeKind.Int16:
                return LoadCore<short>(header, stream).Select(x => (T)(dynamic)x);
            case TypeKind.Int32:
                return LoadCore<int   >(header, stream).Select(x => (T)(dynamic)x);
            case TypeKind.Int64:
                return LoadCore<long  >(header, stream).Select(x => (T)(dynamic)x);
            case TypeKind.Float16:
                throw new NotImplementedException();
            case TypeKind.Float32:
                return LoadCore<float>(header, stream).Select(x => (T)(dynamic)x);
            case TypeKind.Float64:
                return LoadCore<double>(header, stream).Select(x => (T)(dynamic)x);
            case TypeKind.Unicode:
                return LoadCore<string>(header, stream).Select(x => (T)(dynamic)x);
            default:
                throw new InvalidOperationException();
            }
        }


        /// <summary>
        ///     Entity of .npy binary loading, excluding header loading.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="header"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static NdArray<T> LoadCore<T>(NpyHeader header, Stream stream)
        {
            NotSupportedException unsupportedFileFormatError()
                => new NotSupportedException(".npy binary is unsupported format.");

            InvalidCastException typeMismatchError()
                => new InvalidCastException("Generic type is mismatch with .npy binary.");

            var byteSize = header.NumpyType.ByteSize ?? 0;
            if(byteSize == 0)
                throw unsupportedFileFormatError();
            var totalLength = header.Shape.TotalLength;
            var bufferSize = byteSize * totalLength;

            T[] core<T2>(Func<int, T2> readAt)
            {
                if(typeof(T) != typeof(T2))
                    throw typeMismatchError();
                T2[] retvalArray = new T2[totalLength];
                for(var i = 0 ; i < totalLength ; ++i)
                    retvalArray[i] = readAt(i);
                return retvalArray as T[];
            }

            var shape = header.Shape;
            var buffer = new byte[bufferSize];
            stream.Read(buffer, 0, bufferSize);
            var converter = header.NumpyType.Endian == Endian.Little
                ? LittleEndiannessBitConverter.Instance
                : BigEndiannessBitConverter.Instance;
            switch(header.NumpyType.TypeKind)
            {
            case TypeKind.Boolean:
                return NdArray.Create(core(i => buffer[i] > 0), shape);
            case TypeKind.UInt8:
                return NdArray.Create(core(i => buffer[i]), shape);
            case TypeKind.UInt16:
                return NdArray.Create(core(i => converter.ReadPrimitive<ushort>(buffer.AsSpan(2 * i))),
                                      shape);
            case TypeKind.UInt32:
                return NdArray.Create(core(i => converter.ReadPrimitive<uint>(buffer.AsSpan(4 * i))),
                                      shape);
            case TypeKind.UInt64:
                return NdArray.Create(core(i => converter.ReadPrimitive<ulong>(buffer.AsSpan(8 * i))),
                                      shape);
            case TypeKind.Int8:
                return NdArray.Create(core(i => (sbyte)buffer[i]), shape);
            case TypeKind.Int16:
                return NdArray.Create(core(i => converter.ReadPrimitive<short>(buffer.AsSpan(2 * i))),
                                      shape);
            case TypeKind.Int32:
                return NdArray.Create(core(i => converter.ReadPrimitive<int>(buffer.AsSpan(4 * i))),
                                      shape);
            case TypeKind.Int64:
                return NdArray.Create(core(i => converter.ReadPrimitive<long>(buffer.AsSpan(8 * i))),
                                      shape);
            case TypeKind.Float16:
                throw new NotImplementedException();
            case TypeKind.Float32:
                return NdArray.Create(core(i => converter.ReadPrimitive<float>(buffer.AsSpan(4 * i))),
                                      shape);
            case TypeKind.Float64:
                return NdArray.Create(core(i => converter.ReadPrimitive<double>(buffer.AsSpan(8 * i))),
                                      shape);
            case TypeKind.Unicode:
                return
                    NdArray.Create(core(i => Encoding.UTF32.GetString(buffer, byteSize * i, byteSize)),
                                   shape);
            default:
                throw unsupportedFileFormatError();
            }
        }

    }
}
