using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NeodymiumDotNet.Io.Numpy.PythonSyntax;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy
{
    internal class NpyHeader
    {

        private static readonly byte[] _HeaderMagicNumber
            = new[] { (byte)0x93, (byte)'N', (byte)'U', (byte)'M', (byte)'P', (byte)'Y' };


        private static readonly int _MajorVersionAlign = 6;

        private static readonly int _MinorVersionAlign = 7;

        private static readonly int _HeaderLenAlign = 8;


        private static readonly IReadOnlyDictionary<byte, int> _HeaderDicAligns
            = new Dictionary<byte, int>
            {
                { 1, 10 },
                { 2, 12 },
            };


        public byte MajorVersion { get; }

        public byte MinorVersion { get; }

        public DType NumpyType { get; }

        public bool FortranOrder { get; }

        public IndexArray Shape { get; }


        public NpyHeader(byte major, byte minor, DType numpyType, bool fortranOrder,
                         IndexArray shape)
        {
            MajorVersion = major;
            MinorVersion = minor;
            NumpyType = numpyType;
            FortranOrder = fortranOrder;
            Shape = shape;
        }


        /// <summary>
        ///     Loads header data from npy binary stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static NpyHeader LoadHeader(Stream stream)
        {
            ArgumentException invalidArgError()
                => new ArgumentException("The stream is not valid .npy format binary.");

            var magic = new byte[8];
            stream.Read(magic, 0, 8);
            if(!IsValidNpyMagic(magic))
                throw invalidArgError();
            var major = magic[_MajorVersionAlign];
            var minor = magic[_MinorVersionAlign];

            var dicLenBuffer = new byte[4];
            int dicLen;
            switch(major)
            {
            case 1:
                stream.Read(dicLenBuffer, 0, 2);
                dicLen = LittleEndiannessBitConverter.Instance.ReadPrimitive<ushort>(dicLenBuffer);
                break;
            case 2:
                stream.Read(dicLenBuffer, 0, 4);
                dicLen = (int)LittleEndiannessBitConverter.Instance.ReadPrimitive<uint>(dicLenBuffer);
                break;
            default:
                throw invalidArgError();
            }

            var dicTextBuffer = new byte[dicLen];
            stream.Read(dicTextBuffer, 0, dicLen);
            var dic = PyDict.Dict.Parse(Encoding.UTF8.GetString(dicTextBuffer).Trim()).UnPy();
            var numpyType = dic["descr"] as string ?? throw invalidArgError();
            var fortranOrder = dic["fortran_order"] as bool? ?? throw invalidArgError();
            var shape = (dic["shape"] as IReadOnlyList<object>)?.Select(x => (int)(long)x);
            if(shape is null)
                throw invalidArgError();
            return new NpyHeader(major, minor, numpyType, fortranOrder,
                                 new IndexArray(shape.ToArray()));
        }


        /// <summary>
        ///     Generates npy header by this instance.
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateHeader()
        {
            var headerText =
                "{"
                + $"'descr': '{NumpyType.Expression}', "
                + $"'fortran_order': {(FortranOrder ? "True" : "False")}, "
                + $"'shape': ({string.Join(", ", Shape)}{(Shape.Length <= 1 ? "," : "")}), "
                + "}";
            var header = Encoding.UTF8.GetBytes(headerText);

            var bufferLen =
                NdMath.Max(128, Align16(_HeaderMagicNumber.Length + 4 + header.Length + 1));
            var version = bufferLen < 65536 ? (byte)1 : (byte)2;
            var headerDicAlign = _HeaderDicAligns[version];

            var buffer = new byte[bufferLen];
            buffer.AsSpan().Fill(0x20);
            Buffer.BlockCopy(_HeaderMagicNumber, 0, buffer, 0, _HeaderMagicNumber.Length);
            buffer[_MajorVersionAlign] = version;
            buffer[_MinorVersionAlign] = 0;
            switch(version)
            {
            case 1:
                LittleEndiannessBitConverter.Instance.TryWritePrimitive(buffer.AsSpan(_HeaderLenAlign),
                                                                    (ushort)(bufferLen - headerDicAlign));
                break;
            case 2:
                LittleEndiannessBitConverter.Instance.TryWritePrimitive(buffer.AsSpan(_HeaderLenAlign),
                                                                    (ushort)(bufferLen - headerDicAlign));
                break;
            }

            Buffer.BlockCopy(header, 0, buffer, headerDicAlign, header.Length);
            buffer[bufferLen - 1] = 0x0A;
            return buffer;
        }


        private static int Align16(int x)
        {
            var surplus = x & 0b1111;
            return surplus == 0 ? x : (x + 16) & 0x7ffffff0;
        }


        private static bool IsValidNpyMagic(ReadOnlySpan<byte> header)
            => header.Length >= 8
               && header.Slice(0, 6).SequenceEqual(_HeaderMagicNumber);

    }
}
