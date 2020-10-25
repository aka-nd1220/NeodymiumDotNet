using System;
using System.Collections.Generic;
using System.Text;

namespace NeodymiumDotNet.Io.Numpy
{
    /// <summary>
    ///     numpy managed type.
    /// </summary>
    public struct DType
    {

        public static readonly DType Int8    = new DType("|i1");
        public static readonly DType Int16   = new DType("<i2");
        public static readonly DType Int32   = new DType("<i4");
        public static readonly DType Int64   = new DType("<i8");
        public static readonly DType UInt8   = new DType("|u1");
        public static readonly DType UInt16  = new DType("<u2");
        public static readonly DType UInt32  = new DType("<u4");
        public static readonly DType UInt64  = new DType("<u8");
        public static readonly DType Float16 = new DType("<f2");
        public static readonly DType Float32 = new DType("<f4");
        public static readonly DType Float64 = new DType("<f8");
        public static readonly DType Bool    = new DType("|b1");


        /// <summary>
        ///     The expression in .npy header.
        /// </summary>
        public string Expression { get; }


        /// <summary>
        ///     The byte endian of each elements in .npy binary.
        /// </summary>
        public Endian Endian => ToEndian(Expression[0]);


        /// <summary>
        ///     The type kind of each elements in .npy binary.
        /// </summary>
        public TypeKind TypeKind
        {
            get
            {
                switch(Expression.Substring(1))
                {
                case "b1": return TypeKind.Boolean;
                case "u1": return TypeKind.UInt8;
                case "u2": return TypeKind.UInt16;
                case "u4": return TypeKind.UInt32;
                case "u8": return TypeKind.UInt64;
                case "i1": return TypeKind.Int8;
                case "i2": return TypeKind.Int16;
                case "i4": return TypeKind.Int32;
                case "i8": return TypeKind.Int64;
                case "f2": return TypeKind.Float16;
                case "f4": return TypeKind.Float32;
                case "f8": return TypeKind.Float64;
                default:
                    switch(Expression[1])
                    {
                    case 'U': return TypeKind.Unicode;
                    case 'O': return TypeKind.Object;
                    default:  return TypeKind.Undefined;
                    }
                }
            }
        }


        /// <summary>
        ///     The byte size of each elements in .npy binary.
        /// </summary>
        public int? ByteSize
        {
            get
            {
                if(!int.TryParse(Expression.Substring(2), out var result))
                    return null;
                if(TypeKind == TypeKind.Unicode)
                    return 4 * result;
                return result;
            }
        }


        public DType(string expr)
        {
            Expression = expr;
        }


        public override bool Equals(object obj)
            => obj is DType other ? Expression == other.Expression : false;


        public override int GetHashCode()
            => Expression.GetHashCode();


        public override string ToString() => Expression;


        public static implicit operator DType(string value)
            => new DType(value);


        public static Endian ToEndian(char c)
        {
            switch(c)
            {
            case '|': return Endian.Undefined;
            case '>': return Endian.Big;
            case '<': return Endian.Little;
            default:  throw new ArgumentOutOfRangeException();
            }
        }


        public static char FromEndian(Endian e)
        {
            switch(e)
            {
            case Endian.Undefined: return '|';
            case Endian.Big:       return '>';
            case Endian.Little:    return '<';
            default:               throw new ArgumentOutOfRangeException();
            }
        }

    }

    public enum TypeKind
    {

        Undefined = 0,
        Boolean,
        UInt8,
        UInt16,
        UInt32,
        UInt64,
        Int8,
        Int16,
        Int32,
        Int64,
        Float16,
        Float32,
        Float64,
        Unicode,
        Object,

    }
}
