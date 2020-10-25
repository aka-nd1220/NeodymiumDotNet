//
// T4 auto generated code
//
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NeodymiumDotNet
{
    partial class ValueTrait
    {
        /// <summary>
        ///     Returns <c>true</c> if the type supports mathematical operators.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNumber<T>()
        {
            if(typeof(T) == typeof(bool))
            {
                return false;
            }
            if(typeof(T) == typeof(sbyte))
            {
                return true;
            }
            if(typeof(T) == typeof(short))
            {
                return true;
            }
            if(typeof(T) == typeof(int))
            {
                return true;
            }
            if(typeof(T) == typeof(long))
            {
                return true;
            }
            if(typeof(T) == typeof(byte))
            {
                return true;
            }
            if(typeof(T) == typeof(ushort))
            {
                return true;
            }
            if(typeof(T) == typeof(uint))
            {
                return true;
            }
            if(typeof(T) == typeof(ulong))
            {
                return true;
            }
            if(typeof(T) == typeof(float))
            {
                return true;
            }
            if(typeof(T) == typeof(double))
            {
                return true;
            }
            if(typeof(T) == typeof(decimal))
            {
                return true;
            }
            if(typeof(T) == typeof(Complex))
            {
                return true;
            }
            if(typeof(T) == typeof(char))
            {
                return true;
            }
            if(typeof(T) == typeof(string))
            {
                return false;
            }

            return Cache<T>.Trait.IsNumber();
        }


        /// <summary>
        ///     Gets 0 value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Zero<T>()
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'Zero' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)0;
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)0;
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)0;
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)0;
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)0;
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)0;
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)0;
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)0;
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                var retval = (float)0;
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = (double)0;
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = (decimal)0;
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                var retval = (Complex)0;
                return Unsafe.As<Complex, T>(ref retval);
            }
            if(typeof(T) == typeof(char))
            {
                var retval = (char)0;
                return Unsafe.As<char, T>(ref retval);
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'Zero' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.Zero();
        }


        /// <summary>
        ///     Gets 1 value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T One<T>()
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'One' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)1;
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)1;
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)1;
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)1;
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)1;
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)1;
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)1;
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)1;
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                var retval = (float)1;
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = (double)1;
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = (decimal)1;
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                var retval = (Complex)1;
                return Unsafe.As<Complex, T>(ref retval);
            }
            if(typeof(T) == typeof(char))
            {
                var retval = (char)1;
                return Unsafe.As<char, T>(ref retval);
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'One' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.One();
        }


        /// <summary>
        ///     Calculates UnaryPlus operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T UnaryPlus<T>(T value)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'UnaryPlus' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                throw new NotSupportedException("The operation 'UnaryPlus' for type 'sbyte' is not supported.");
            }
            if(typeof(T) == typeof(short))
            {
                throw new NotSupportedException("The operation 'UnaryPlus' for type 'short' is not supported.");
            }
            if(typeof(T) == typeof(int))
            {
                var retval =  Unsafe.As<T, int>(ref value);
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval =  Unsafe.As<T, long>(ref value);
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                throw new NotSupportedException("The operation 'UnaryPlus' for type 'byte' is not supported.");
            }
            if(typeof(T) == typeof(ushort))
            {
                throw new NotSupportedException("The operation 'UnaryPlus' for type 'ushort' is not supported.");
            }
            if(typeof(T) == typeof(uint))
            {
                throw new NotSupportedException("The operation 'UnaryPlus' for type 'uint' is not supported.");
            }
            if(typeof(T) == typeof(ulong))
            {
                throw new NotSupportedException("The operation 'UnaryPlus' for type 'ulong' is not supported.");
            }
            if(typeof(T) == typeof(float))
            {
                var retval =  Unsafe.As<T, float>(ref value);
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval =  Unsafe.As<T, double>(ref value);
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval =  Unsafe.As<T, decimal>(ref value);
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                var retval =  Unsafe.As<T, Complex>(ref value);
                return Unsafe.As<Complex, T>(ref retval);
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'UnaryPlus' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'UnaryPlus' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.UnaryPlus(value);
        }


        /// <summary>
        ///     Calculates UnaryNegate operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T UnaryNegate<T>(T value)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'UnaryNegate' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                throw new NotSupportedException("The operation 'UnaryNegate' for type 'sbyte' is not supported.");
            }
            if(typeof(T) == typeof(short))
            {
                throw new NotSupportedException("The operation 'UnaryNegate' for type 'short' is not supported.");
            }
            if(typeof(T) == typeof(int))
            {
                var retval = -Unsafe.As<T, int>(ref value);
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = -Unsafe.As<T, long>(ref value);
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                throw new NotSupportedException("The operation 'UnaryNegate' for type 'byte' is not supported.");
            }
            if(typeof(T) == typeof(ushort))
            {
                throw new NotSupportedException("The operation 'UnaryNegate' for type 'ushort' is not supported.");
            }
            if(typeof(T) == typeof(uint))
            {
                throw new NotSupportedException("The operation 'UnaryNegate' for type 'uint' is not supported.");
            }
            if(typeof(T) == typeof(ulong))
            {
                throw new NotSupportedException("The operation 'UnaryNegate' for type 'ulong' is not supported.");
            }
            if(typeof(T) == typeof(float))
            {
                var retval = -Unsafe.As<T, float>(ref value);
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = -Unsafe.As<T, double>(ref value);
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = -Unsafe.As<T, decimal>(ref value);
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                var retval = -Unsafe.As<T, Complex>(ref value);
                return Unsafe.As<Complex, T>(ref retval);
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'UnaryNegate' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'UnaryNegate' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.UnaryNegate(value);
        }


        /// <summary>
        ///     Calculates Not operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Not<T>(T value)
        {
            if(typeof(T) == typeof(bool))
            {
                var retval = !Unsafe.As<T, bool>(ref value);
                return Unsafe.As<bool, T>(ref retval);
            }
            if(typeof(T) == typeof(sbyte))
            {
                throw new NotSupportedException("The operation 'Not' for type 'sbyte' is not supported.");
            }
            if(typeof(T) == typeof(short))
            {
                throw new NotSupportedException("The operation 'Not' for type 'short' is not supported.");
            }
            if(typeof(T) == typeof(int))
            {
                throw new NotSupportedException("The operation 'Not' for type 'int' is not supported.");
            }
            if(typeof(T) == typeof(long))
            {
                throw new NotSupportedException("The operation 'Not' for type 'long' is not supported.");
            }
            if(typeof(T) == typeof(byte))
            {
                throw new NotSupportedException("The operation 'Not' for type 'byte' is not supported.");
            }
            if(typeof(T) == typeof(ushort))
            {
                throw new NotSupportedException("The operation 'Not' for type 'ushort' is not supported.");
            }
            if(typeof(T) == typeof(uint))
            {
                throw new NotSupportedException("The operation 'Not' for type 'uint' is not supported.");
            }
            if(typeof(T) == typeof(ulong))
            {
                throw new NotSupportedException("The operation 'Not' for type 'ulong' is not supported.");
            }
            if(typeof(T) == typeof(float))
            {
                throw new NotSupportedException("The operation 'Not' for type 'float' is not supported.");
            }
            if(typeof(T) == typeof(double))
            {
                throw new NotSupportedException("The operation 'Not' for type 'double' is not supported.");
            }
            if(typeof(T) == typeof(decimal))
            {
                throw new NotSupportedException("The operation 'Not' for type 'decimal' is not supported.");
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'Not' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'Not' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'Not' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.Not(value);
        }


        /// <summary>
        ///     Calculates Complement operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Complement<T>(T value)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'Complement' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                throw new NotSupportedException("The operation 'Complement' for type 'sbyte' is not supported.");
            }
            if(typeof(T) == typeof(short))
            {
                throw new NotSupportedException("The operation 'Complement' for type 'short' is not supported.");
            }
            if(typeof(T) == typeof(int))
            {
                var retval = ~Unsafe.As<T, int>(ref value);
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = ~Unsafe.As<T, long>(ref value);
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                throw new NotSupportedException("The operation 'Complement' for type 'byte' is not supported.");
            }
            if(typeof(T) == typeof(ushort))
            {
                throw new NotSupportedException("The operation 'Complement' for type 'ushort' is not supported.");
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = ~Unsafe.As<T, uint>(ref value);
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = ~Unsafe.As<T, ulong>(ref value);
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                throw new NotSupportedException("The operation 'Complement' for type 'float' is not supported.");
            }
            if(typeof(T) == typeof(double))
            {
                throw new NotSupportedException("The operation 'Complement' for type 'double' is not supported.");
            }
            if(typeof(T) == typeof(decimal))
            {
                throw new NotSupportedException("The operation 'Complement' for type 'decimal' is not supported.");
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'Complement' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'Complement' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'Complement' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.Complement(value);
        }


        /// <summary>
        ///     Calculates Add operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'Add' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)(Unsafe.As<T, sbyte>(ref lhs) + Unsafe.As<T, sbyte>(ref rhs));
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)(Unsafe.As<T, short>(ref lhs) + Unsafe.As<T, short>(ref rhs));
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)(Unsafe.As<T, int>(ref lhs) + Unsafe.As<T, int>(ref rhs));
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)(Unsafe.As<T, long>(ref lhs) + Unsafe.As<T, long>(ref rhs));
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)(Unsafe.As<T, byte>(ref lhs) + Unsafe.As<T, byte>(ref rhs));
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)(Unsafe.As<T, ushort>(ref lhs) + Unsafe.As<T, ushort>(ref rhs));
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)(Unsafe.As<T, uint>(ref lhs) + Unsafe.As<T, uint>(ref rhs));
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)(Unsafe.As<T, ulong>(ref lhs) + Unsafe.As<T, ulong>(ref rhs));
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                var retval = (float)(Unsafe.As<T, float>(ref lhs) + Unsafe.As<T, float>(ref rhs));
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = (double)(Unsafe.As<T, double>(ref lhs) + Unsafe.As<T, double>(ref rhs));
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = (decimal)(Unsafe.As<T, decimal>(ref lhs) + Unsafe.As<T, decimal>(ref rhs));
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                var retval = (Complex)(Unsafe.As<T, Complex>(ref lhs) + Unsafe.As<T, Complex>(ref rhs));
                return Unsafe.As<Complex, T>(ref retval);
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'Add' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                var retval = (string)(Unsafe.As<T, string>(ref lhs) + Unsafe.As<T, string>(ref rhs));
                return Unsafe.As<string, T>(ref retval);
            }

            return Cache<T>.Trait.Add(lhs, rhs);
        }


        /// <summary>
        ///     Calculates Subtract operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'Subtract' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)(Unsafe.As<T, sbyte>(ref lhs) - Unsafe.As<T, sbyte>(ref rhs));
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)(Unsafe.As<T, short>(ref lhs) - Unsafe.As<T, short>(ref rhs));
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)(Unsafe.As<T, int>(ref lhs) - Unsafe.As<T, int>(ref rhs));
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)(Unsafe.As<T, long>(ref lhs) - Unsafe.As<T, long>(ref rhs));
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)(Unsafe.As<T, byte>(ref lhs) - Unsafe.As<T, byte>(ref rhs));
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)(Unsafe.As<T, ushort>(ref lhs) - Unsafe.As<T, ushort>(ref rhs));
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)(Unsafe.As<T, uint>(ref lhs) - Unsafe.As<T, uint>(ref rhs));
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)(Unsafe.As<T, ulong>(ref lhs) - Unsafe.As<T, ulong>(ref rhs));
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                var retval = (float)(Unsafe.As<T, float>(ref lhs) - Unsafe.As<T, float>(ref rhs));
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = (double)(Unsafe.As<T, double>(ref lhs) - Unsafe.As<T, double>(ref rhs));
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = (decimal)(Unsafe.As<T, decimal>(ref lhs) - Unsafe.As<T, decimal>(ref rhs));
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                var retval = (Complex)(Unsafe.As<T, Complex>(ref lhs) - Unsafe.As<T, Complex>(ref rhs));
                return Unsafe.As<Complex, T>(ref retval);
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'Subtract' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'Subtract' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.Subtract(lhs, rhs);
        }


        /// <summary>
        ///     Calculates Multiply operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Multiply<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'Multiply' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)(Unsafe.As<T, sbyte>(ref lhs) * Unsafe.As<T, sbyte>(ref rhs));
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)(Unsafe.As<T, short>(ref lhs) * Unsafe.As<T, short>(ref rhs));
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)(Unsafe.As<T, int>(ref lhs) * Unsafe.As<T, int>(ref rhs));
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)(Unsafe.As<T, long>(ref lhs) * Unsafe.As<T, long>(ref rhs));
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)(Unsafe.As<T, byte>(ref lhs) * Unsafe.As<T, byte>(ref rhs));
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)(Unsafe.As<T, ushort>(ref lhs) * Unsafe.As<T, ushort>(ref rhs));
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)(Unsafe.As<T, uint>(ref lhs) * Unsafe.As<T, uint>(ref rhs));
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)(Unsafe.As<T, ulong>(ref lhs) * Unsafe.As<T, ulong>(ref rhs));
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                var retval = (float)(Unsafe.As<T, float>(ref lhs) * Unsafe.As<T, float>(ref rhs));
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = (double)(Unsafe.As<T, double>(ref lhs) * Unsafe.As<T, double>(ref rhs));
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = (decimal)(Unsafe.As<T, decimal>(ref lhs) * Unsafe.As<T, decimal>(ref rhs));
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                var retval = (Complex)(Unsafe.As<T, Complex>(ref lhs) * Unsafe.As<T, Complex>(ref rhs));
                return Unsafe.As<Complex, T>(ref retval);
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'Multiply' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'Multiply' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.Multiply(lhs, rhs);
        }


        /// <summary>
        ///     Calculates Divide operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'Divide' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)(Unsafe.As<T, sbyte>(ref lhs) / Unsafe.As<T, sbyte>(ref rhs));
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)(Unsafe.As<T, short>(ref lhs) / Unsafe.As<T, short>(ref rhs));
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)(Unsafe.As<T, int>(ref lhs) / Unsafe.As<T, int>(ref rhs));
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)(Unsafe.As<T, long>(ref lhs) / Unsafe.As<T, long>(ref rhs));
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)(Unsafe.As<T, byte>(ref lhs) / Unsafe.As<T, byte>(ref rhs));
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)(Unsafe.As<T, ushort>(ref lhs) / Unsafe.As<T, ushort>(ref rhs));
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)(Unsafe.As<T, uint>(ref lhs) / Unsafe.As<T, uint>(ref rhs));
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)(Unsafe.As<T, ulong>(ref lhs) / Unsafe.As<T, ulong>(ref rhs));
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                var retval = (float)(Unsafe.As<T, float>(ref lhs) / Unsafe.As<T, float>(ref rhs));
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = (double)(Unsafe.As<T, double>(ref lhs) / Unsafe.As<T, double>(ref rhs));
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = (decimal)(Unsafe.As<T, decimal>(ref lhs) / Unsafe.As<T, decimal>(ref rhs));
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                var retval = (Complex)(Unsafe.As<T, Complex>(ref lhs) / Unsafe.As<T, Complex>(ref rhs));
                return Unsafe.As<Complex, T>(ref retval);
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'Divide' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'Divide' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.Divide(lhs, rhs);
        }


        /// <summary>
        ///     Calculates Modulo operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Modulo<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'Modulo' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)(Unsafe.As<T, sbyte>(ref lhs) % Unsafe.As<T, sbyte>(ref rhs));
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)(Unsafe.As<T, short>(ref lhs) % Unsafe.As<T, short>(ref rhs));
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)(Unsafe.As<T, int>(ref lhs) % Unsafe.As<T, int>(ref rhs));
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)(Unsafe.As<T, long>(ref lhs) % Unsafe.As<T, long>(ref rhs));
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)(Unsafe.As<T, byte>(ref lhs) % Unsafe.As<T, byte>(ref rhs));
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)(Unsafe.As<T, ushort>(ref lhs) % Unsafe.As<T, ushort>(ref rhs));
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)(Unsafe.As<T, uint>(ref lhs) % Unsafe.As<T, uint>(ref rhs));
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)(Unsafe.As<T, ulong>(ref lhs) % Unsafe.As<T, ulong>(ref rhs));
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                var retval = (float)(Unsafe.As<T, float>(ref lhs) % Unsafe.As<T, float>(ref rhs));
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = (double)(Unsafe.As<T, double>(ref lhs) % Unsafe.As<T, double>(ref rhs));
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = (decimal)(Unsafe.As<T, decimal>(ref lhs) % Unsafe.As<T, decimal>(ref rhs));
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'Modulo' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'Modulo' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'Modulo' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.Modulo(lhs, rhs);
        }


        /// <summary>
        ///     Calculates BitwiseAnd operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T BitwiseAnd<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                var retval = (bool)(Unsafe.As<T, bool>(ref lhs) & Unsafe.As<T, bool>(ref rhs));
                return Unsafe.As<bool, T>(ref retval);
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)(Unsafe.As<T, sbyte>(ref lhs) & Unsafe.As<T, sbyte>(ref rhs));
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)(Unsafe.As<T, short>(ref lhs) & Unsafe.As<T, short>(ref rhs));
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)(Unsafe.As<T, int>(ref lhs) & Unsafe.As<T, int>(ref rhs));
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)(Unsafe.As<T, long>(ref lhs) & Unsafe.As<T, long>(ref rhs));
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)(Unsafe.As<T, byte>(ref lhs) & Unsafe.As<T, byte>(ref rhs));
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)(Unsafe.As<T, ushort>(ref lhs) & Unsafe.As<T, ushort>(ref rhs));
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)(Unsafe.As<T, uint>(ref lhs) & Unsafe.As<T, uint>(ref rhs));
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)(Unsafe.As<T, ulong>(ref lhs) & Unsafe.As<T, ulong>(ref rhs));
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                throw new NotSupportedException("The operation 'BitwiseAnd' for type 'float' is not supported.");
            }
            if(typeof(T) == typeof(double))
            {
                throw new NotSupportedException("The operation 'BitwiseAnd' for type 'double' is not supported.");
            }
            if(typeof(T) == typeof(decimal))
            {
                throw new NotSupportedException("The operation 'BitwiseAnd' for type 'decimal' is not supported.");
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'BitwiseAnd' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'BitwiseAnd' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'BitwiseAnd' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.BitwiseAnd(lhs, rhs);
        }


        /// <summary>
        ///     Calculates BitwiseOr operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T BitwiseOr<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                var retval = (bool)(Unsafe.As<T, bool>(ref lhs) | Unsafe.As<T, bool>(ref rhs));
                return Unsafe.As<bool, T>(ref retval);
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)(Unsafe.As<T, sbyte>(ref lhs) | Unsafe.As<T, sbyte>(ref rhs));
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)(Unsafe.As<T, short>(ref lhs) | Unsafe.As<T, short>(ref rhs));
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)(Unsafe.As<T, int>(ref lhs) | Unsafe.As<T, int>(ref rhs));
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)(Unsafe.As<T, long>(ref lhs) | Unsafe.As<T, long>(ref rhs));
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)(Unsafe.As<T, byte>(ref lhs) | Unsafe.As<T, byte>(ref rhs));
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)(Unsafe.As<T, ushort>(ref lhs) | Unsafe.As<T, ushort>(ref rhs));
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)(Unsafe.As<T, uint>(ref lhs) | Unsafe.As<T, uint>(ref rhs));
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)(Unsafe.As<T, ulong>(ref lhs) | Unsafe.As<T, ulong>(ref rhs));
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                throw new NotSupportedException("The operation 'BitwiseOr' for type 'float' is not supported.");
            }
            if(typeof(T) == typeof(double))
            {
                throw new NotSupportedException("The operation 'BitwiseOr' for type 'double' is not supported.");
            }
            if(typeof(T) == typeof(decimal))
            {
                throw new NotSupportedException("The operation 'BitwiseOr' for type 'decimal' is not supported.");
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'BitwiseOr' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'BitwiseOr' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'BitwiseOr' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.BitwiseOr(lhs, rhs);
        }


        /// <summary>
        ///     Calculates BitwiseXor operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T BitwiseXor<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                var retval = (bool)(Unsafe.As<T, bool>(ref lhs) ^ Unsafe.As<T, bool>(ref rhs));
                return Unsafe.As<bool, T>(ref retval);
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)(Unsafe.As<T, sbyte>(ref lhs) ^ Unsafe.As<T, sbyte>(ref rhs));
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)(Unsafe.As<T, short>(ref lhs) ^ Unsafe.As<T, short>(ref rhs));
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)(Unsafe.As<T, int>(ref lhs) ^ Unsafe.As<T, int>(ref rhs));
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)(Unsafe.As<T, long>(ref lhs) ^ Unsafe.As<T, long>(ref rhs));
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)(Unsafe.As<T, byte>(ref lhs) ^ Unsafe.As<T, byte>(ref rhs));
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)(Unsafe.As<T, ushort>(ref lhs) ^ Unsafe.As<T, ushort>(ref rhs));
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)(Unsafe.As<T, uint>(ref lhs) ^ Unsafe.As<T, uint>(ref rhs));
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)(Unsafe.As<T, ulong>(ref lhs) ^ Unsafe.As<T, ulong>(ref rhs));
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                throw new NotSupportedException("The operation 'BitwiseXor' for type 'float' is not supported.");
            }
            if(typeof(T) == typeof(double))
            {
                throw new NotSupportedException("The operation 'BitwiseXor' for type 'double' is not supported.");
            }
            if(typeof(T) == typeof(decimal))
            {
                throw new NotSupportedException("The operation 'BitwiseXor' for type 'decimal' is not supported.");
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'BitwiseXor' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'BitwiseXor' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'BitwiseXor' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.BitwiseXor(lhs, rhs);
        }


        /// <summary>
        ///     Calculates Increment operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Increment<T>(T value)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'Increment' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = ++Unsafe.As<T, sbyte>(ref value);
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = ++Unsafe.As<T, short>(ref value);
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = ++Unsafe.As<T, int>(ref value);
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = ++Unsafe.As<T, long>(ref value);
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = ++Unsafe.As<T, byte>(ref value);
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = ++Unsafe.As<T, ushort>(ref value);
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = ++Unsafe.As<T, uint>(ref value);
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = ++Unsafe.As<T, ulong>(ref value);
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                var retval = ++Unsafe.As<T, float>(ref value);
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = ++Unsafe.As<T, double>(ref value);
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = ++Unsafe.As<T, decimal>(ref value);
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'Increment' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                var retval = ++Unsafe.As<T, char>(ref value);
                return Unsafe.As<char, T>(ref retval);
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'Increment' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.Increment(value);
        }


        /// <summary>
        ///     Calculates Decrement operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decrement<T>(T value)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'Decrement' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = --Unsafe.As<T, sbyte>(ref value);
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = --Unsafe.As<T, short>(ref value);
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = --Unsafe.As<T, int>(ref value);
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = --Unsafe.As<T, long>(ref value);
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = --Unsafe.As<T, byte>(ref value);
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = --Unsafe.As<T, ushort>(ref value);
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = --Unsafe.As<T, uint>(ref value);
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = --Unsafe.As<T, ulong>(ref value);
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                var retval = --Unsafe.As<T, float>(ref value);
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = --Unsafe.As<T, double>(ref value);
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = --Unsafe.As<T, decimal>(ref value);
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'Decrement' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                var retval = --Unsafe.As<T, char>(ref value);
                return Unsafe.As<char, T>(ref retval);
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'Decrement' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.Decrement(value);
        }


        /// <summary>
        ///     Calculates ShiftLeft operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ShiftLeft<T>(T lhs, int rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'ShiftLeft' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                throw new NotSupportedException("The operation 'ShiftLeft' for type 'sbyte' is not supported.");
            }
            if(typeof(T) == typeof(short))
            {
                throw new NotSupportedException("The operation 'ShiftLeft' for type 'short' is not supported.");
            }
            if(typeof(T) == typeof(int))
            {
                var retval = Unsafe.As<T, int>(ref lhs) << rhs;
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = Unsafe.As<T, long>(ref lhs) << rhs;
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                throw new NotSupportedException("The operation 'ShiftLeft' for type 'byte' is not supported.");
            }
            if(typeof(T) == typeof(ushort))
            {
                throw new NotSupportedException("The operation 'ShiftLeft' for type 'ushort' is not supported.");
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = Unsafe.As<T, uint>(ref lhs) << rhs;
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = Unsafe.As<T, ulong>(ref lhs) << rhs;
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                throw new NotSupportedException("The operation 'ShiftLeft' for type 'float' is not supported.");
            }
            if(typeof(T) == typeof(double))
            {
                throw new NotSupportedException("The operation 'ShiftLeft' for type 'double' is not supported.");
            }
            if(typeof(T) == typeof(decimal))
            {
                throw new NotSupportedException("The operation 'ShiftLeft' for type 'decimal' is not supported.");
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'ShiftLeft' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'ShiftLeft' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'ShiftLeft' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.ShiftLeft(lhs, rhs);
        }


        /// <summary>
        ///     Calculates ShiftRight operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ShiftRight<T>(T lhs, int rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'ShiftRight' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                throw new NotSupportedException("The operation 'ShiftRight' for type 'sbyte' is not supported.");
            }
            if(typeof(T) == typeof(short))
            {
                throw new NotSupportedException("The operation 'ShiftRight' for type 'short' is not supported.");
            }
            if(typeof(T) == typeof(int))
            {
                var retval = Unsafe.As<T, int>(ref lhs) >> rhs;
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = Unsafe.As<T, long>(ref lhs) >> rhs;
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                throw new NotSupportedException("The operation 'ShiftRight' for type 'byte' is not supported.");
            }
            if(typeof(T) == typeof(ushort))
            {
                throw new NotSupportedException("The operation 'ShiftRight' for type 'ushort' is not supported.");
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = Unsafe.As<T, uint>(ref lhs) >> rhs;
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = Unsafe.As<T, ulong>(ref lhs) >> rhs;
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                throw new NotSupportedException("The operation 'ShiftRight' for type 'float' is not supported.");
            }
            if(typeof(T) == typeof(double))
            {
                throw new NotSupportedException("The operation 'ShiftRight' for type 'double' is not supported.");
            }
            if(typeof(T) == typeof(decimal))
            {
                throw new NotSupportedException("The operation 'ShiftRight' for type 'decimal' is not supported.");
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'ShiftRight' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                throw new NotSupportedException("The operation 'ShiftRight' for type 'char' is not supported.");
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'ShiftRight' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.ShiftRight(lhs, rhs);
        }


        /// <summary>
        ///     Calculates Equals operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                return Unsafe.As<T, bool>(ref lhs) == Unsafe.As<T, bool>(ref rhs);
            }
            if(typeof(T) == typeof(sbyte))
            {
                return Unsafe.As<T, sbyte>(ref lhs) == Unsafe.As<T, sbyte>(ref rhs);
            }
            if(typeof(T) == typeof(short))
            {
                return Unsafe.As<T, short>(ref lhs) == Unsafe.As<T, short>(ref rhs);
            }
            if(typeof(T) == typeof(int))
            {
                return Unsafe.As<T, int>(ref lhs) == Unsafe.As<T, int>(ref rhs);
            }
            if(typeof(T) == typeof(long))
            {
                return Unsafe.As<T, long>(ref lhs) == Unsafe.As<T, long>(ref rhs);
            }
            if(typeof(T) == typeof(byte))
            {
                return Unsafe.As<T, byte>(ref lhs) == Unsafe.As<T, byte>(ref rhs);
            }
            if(typeof(T) == typeof(ushort))
            {
                return Unsafe.As<T, ushort>(ref lhs) == Unsafe.As<T, ushort>(ref rhs);
            }
            if(typeof(T) == typeof(uint))
            {
                return Unsafe.As<T, uint>(ref lhs) == Unsafe.As<T, uint>(ref rhs);
            }
            if(typeof(T) == typeof(ulong))
            {
                return Unsafe.As<T, ulong>(ref lhs) == Unsafe.As<T, ulong>(ref rhs);
            }
            if(typeof(T) == typeof(float))
            {
                return Unsafe.As<T, float>(ref lhs) == Unsafe.As<T, float>(ref rhs);
            }
            if(typeof(T) == typeof(double))
            {
                return Unsafe.As<T, double>(ref lhs) == Unsafe.As<T, double>(ref rhs);
            }
            if(typeof(T) == typeof(decimal))
            {
                return Unsafe.As<T, decimal>(ref lhs) == Unsafe.As<T, decimal>(ref rhs);
            }
            if(typeof(T) == typeof(Complex))
            {
                return Unsafe.As<T, Complex>(ref lhs) == Unsafe.As<T, Complex>(ref rhs);
            }
            if(typeof(T) == typeof(char))
            {
                return Unsafe.As<T, char>(ref lhs) == Unsafe.As<T, char>(ref rhs);
            }
            if(typeof(T) == typeof(string))
            {
                return Unsafe.As<T, string>(ref lhs) == Unsafe.As<T, string>(ref rhs);
            }

            return Cache<T>.Trait.Equals(lhs, rhs);
        }


        /// <summary>
        ///     Calculates NotEquals operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NotEquals<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                return Unsafe.As<T, bool>(ref lhs) != Unsafe.As<T, bool>(ref rhs);
            }
            if(typeof(T) == typeof(sbyte))
            {
                return Unsafe.As<T, sbyte>(ref lhs) != Unsafe.As<T, sbyte>(ref rhs);
            }
            if(typeof(T) == typeof(short))
            {
                return Unsafe.As<T, short>(ref lhs) != Unsafe.As<T, short>(ref rhs);
            }
            if(typeof(T) == typeof(int))
            {
                return Unsafe.As<T, int>(ref lhs) != Unsafe.As<T, int>(ref rhs);
            }
            if(typeof(T) == typeof(long))
            {
                return Unsafe.As<T, long>(ref lhs) != Unsafe.As<T, long>(ref rhs);
            }
            if(typeof(T) == typeof(byte))
            {
                return Unsafe.As<T, byte>(ref lhs) != Unsafe.As<T, byte>(ref rhs);
            }
            if(typeof(T) == typeof(ushort))
            {
                return Unsafe.As<T, ushort>(ref lhs) != Unsafe.As<T, ushort>(ref rhs);
            }
            if(typeof(T) == typeof(uint))
            {
                return Unsafe.As<T, uint>(ref lhs) != Unsafe.As<T, uint>(ref rhs);
            }
            if(typeof(T) == typeof(ulong))
            {
                return Unsafe.As<T, ulong>(ref lhs) != Unsafe.As<T, ulong>(ref rhs);
            }
            if(typeof(T) == typeof(float))
            {
                return Unsafe.As<T, float>(ref lhs) != Unsafe.As<T, float>(ref rhs);
            }
            if(typeof(T) == typeof(double))
            {
                return Unsafe.As<T, double>(ref lhs) != Unsafe.As<T, double>(ref rhs);
            }
            if(typeof(T) == typeof(decimal))
            {
                return Unsafe.As<T, decimal>(ref lhs) != Unsafe.As<T, decimal>(ref rhs);
            }
            if(typeof(T) == typeof(Complex))
            {
                return Unsafe.As<T, Complex>(ref lhs) != Unsafe.As<T, Complex>(ref rhs);
            }
            if(typeof(T) == typeof(char))
            {
                return Unsafe.As<T, char>(ref lhs) != Unsafe.As<T, char>(ref rhs);
            }
            if(typeof(T) == typeof(string))
            {
                return Unsafe.As<T, string>(ref lhs) != Unsafe.As<T, string>(ref rhs);
            }

            return Cache<T>.Trait.NotEquals(lhs, rhs);
        }


        /// <summary>
        ///     Calculates LessThan operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'LessThan' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                return Unsafe.As<T, sbyte>(ref lhs) < Unsafe.As<T, sbyte>(ref rhs);
            }
            if(typeof(T) == typeof(short))
            {
                return Unsafe.As<T, short>(ref lhs) < Unsafe.As<T, short>(ref rhs);
            }
            if(typeof(T) == typeof(int))
            {
                return Unsafe.As<T, int>(ref lhs) < Unsafe.As<T, int>(ref rhs);
            }
            if(typeof(T) == typeof(long))
            {
                return Unsafe.As<T, long>(ref lhs) < Unsafe.As<T, long>(ref rhs);
            }
            if(typeof(T) == typeof(byte))
            {
                return Unsafe.As<T, byte>(ref lhs) < Unsafe.As<T, byte>(ref rhs);
            }
            if(typeof(T) == typeof(ushort))
            {
                return Unsafe.As<T, ushort>(ref lhs) < Unsafe.As<T, ushort>(ref rhs);
            }
            if(typeof(T) == typeof(uint))
            {
                return Unsafe.As<T, uint>(ref lhs) < Unsafe.As<T, uint>(ref rhs);
            }
            if(typeof(T) == typeof(ulong))
            {
                return Unsafe.As<T, ulong>(ref lhs) < Unsafe.As<T, ulong>(ref rhs);
            }
            if(typeof(T) == typeof(float))
            {
                return Unsafe.As<T, float>(ref lhs) < Unsafe.As<T, float>(ref rhs);
            }
            if(typeof(T) == typeof(double))
            {
                return Unsafe.As<T, double>(ref lhs) < Unsafe.As<T, double>(ref rhs);
            }
            if(typeof(T) == typeof(decimal))
            {
                return Unsafe.As<T, decimal>(ref lhs) < Unsafe.As<T, decimal>(ref rhs);
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'LessThan' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                return Unsafe.As<T, char>(ref lhs) < Unsafe.As<T, char>(ref rhs);
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'LessThan' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.LessThan(lhs, rhs);
        }


        /// <summary>
        ///     Calculates LessThanOrEquals operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEquals<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'LessThanOrEquals' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                return Unsafe.As<T, sbyte>(ref lhs) <= Unsafe.As<T, sbyte>(ref rhs);
            }
            if(typeof(T) == typeof(short))
            {
                return Unsafe.As<T, short>(ref lhs) <= Unsafe.As<T, short>(ref rhs);
            }
            if(typeof(T) == typeof(int))
            {
                return Unsafe.As<T, int>(ref lhs) <= Unsafe.As<T, int>(ref rhs);
            }
            if(typeof(T) == typeof(long))
            {
                return Unsafe.As<T, long>(ref lhs) <= Unsafe.As<T, long>(ref rhs);
            }
            if(typeof(T) == typeof(byte))
            {
                return Unsafe.As<T, byte>(ref lhs) <= Unsafe.As<T, byte>(ref rhs);
            }
            if(typeof(T) == typeof(ushort))
            {
                return Unsafe.As<T, ushort>(ref lhs) <= Unsafe.As<T, ushort>(ref rhs);
            }
            if(typeof(T) == typeof(uint))
            {
                return Unsafe.As<T, uint>(ref lhs) <= Unsafe.As<T, uint>(ref rhs);
            }
            if(typeof(T) == typeof(ulong))
            {
                return Unsafe.As<T, ulong>(ref lhs) <= Unsafe.As<T, ulong>(ref rhs);
            }
            if(typeof(T) == typeof(float))
            {
                return Unsafe.As<T, float>(ref lhs) <= Unsafe.As<T, float>(ref rhs);
            }
            if(typeof(T) == typeof(double))
            {
                return Unsafe.As<T, double>(ref lhs) <= Unsafe.As<T, double>(ref rhs);
            }
            if(typeof(T) == typeof(decimal))
            {
                return Unsafe.As<T, decimal>(ref lhs) <= Unsafe.As<T, decimal>(ref rhs);
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'LessThanOrEquals' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                return Unsafe.As<T, char>(ref lhs) <= Unsafe.As<T, char>(ref rhs);
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'LessThanOrEquals' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.LessThanOrEquals(lhs, rhs);
        }


        /// <summary>
        ///     Calculates GreaterThan operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'GreaterThan' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                return Unsafe.As<T, sbyte>(ref lhs) > Unsafe.As<T, sbyte>(ref rhs);
            }
            if(typeof(T) == typeof(short))
            {
                return Unsafe.As<T, short>(ref lhs) > Unsafe.As<T, short>(ref rhs);
            }
            if(typeof(T) == typeof(int))
            {
                return Unsafe.As<T, int>(ref lhs) > Unsafe.As<T, int>(ref rhs);
            }
            if(typeof(T) == typeof(long))
            {
                return Unsafe.As<T, long>(ref lhs) > Unsafe.As<T, long>(ref rhs);
            }
            if(typeof(T) == typeof(byte))
            {
                return Unsafe.As<T, byte>(ref lhs) > Unsafe.As<T, byte>(ref rhs);
            }
            if(typeof(T) == typeof(ushort))
            {
                return Unsafe.As<T, ushort>(ref lhs) > Unsafe.As<T, ushort>(ref rhs);
            }
            if(typeof(T) == typeof(uint))
            {
                return Unsafe.As<T, uint>(ref lhs) > Unsafe.As<T, uint>(ref rhs);
            }
            if(typeof(T) == typeof(ulong))
            {
                return Unsafe.As<T, ulong>(ref lhs) > Unsafe.As<T, ulong>(ref rhs);
            }
            if(typeof(T) == typeof(float))
            {
                return Unsafe.As<T, float>(ref lhs) > Unsafe.As<T, float>(ref rhs);
            }
            if(typeof(T) == typeof(double))
            {
                return Unsafe.As<T, double>(ref lhs) > Unsafe.As<T, double>(ref rhs);
            }
            if(typeof(T) == typeof(decimal))
            {
                return Unsafe.As<T, decimal>(ref lhs) > Unsafe.As<T, decimal>(ref rhs);
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'GreaterThan' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                return Unsafe.As<T, char>(ref lhs) > Unsafe.As<T, char>(ref rhs);
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'GreaterThan' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.GreaterThan(lhs, rhs);
        }


        /// <summary>
        ///     Calculates GreaterThanOrEquals operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrEquals<T>(T lhs, T rhs)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'GreaterThanOrEquals' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                return Unsafe.As<T, sbyte>(ref lhs) >= Unsafe.As<T, sbyte>(ref rhs);
            }
            if(typeof(T) == typeof(short))
            {
                return Unsafe.As<T, short>(ref lhs) >= Unsafe.As<T, short>(ref rhs);
            }
            if(typeof(T) == typeof(int))
            {
                return Unsafe.As<T, int>(ref lhs) >= Unsafe.As<T, int>(ref rhs);
            }
            if(typeof(T) == typeof(long))
            {
                return Unsafe.As<T, long>(ref lhs) >= Unsafe.As<T, long>(ref rhs);
            }
            if(typeof(T) == typeof(byte))
            {
                return Unsafe.As<T, byte>(ref lhs) >= Unsafe.As<T, byte>(ref rhs);
            }
            if(typeof(T) == typeof(ushort))
            {
                return Unsafe.As<T, ushort>(ref lhs) >= Unsafe.As<T, ushort>(ref rhs);
            }
            if(typeof(T) == typeof(uint))
            {
                return Unsafe.As<T, uint>(ref lhs) >= Unsafe.As<T, uint>(ref rhs);
            }
            if(typeof(T) == typeof(ulong))
            {
                return Unsafe.As<T, ulong>(ref lhs) >= Unsafe.As<T, ulong>(ref rhs);
            }
            if(typeof(T) == typeof(float))
            {
                return Unsafe.As<T, float>(ref lhs) >= Unsafe.As<T, float>(ref rhs);
            }
            if(typeof(T) == typeof(double))
            {
                return Unsafe.As<T, double>(ref lhs) >= Unsafe.As<T, double>(ref rhs);
            }
            if(typeof(T) == typeof(decimal))
            {
                return Unsafe.As<T, decimal>(ref lhs) >= Unsafe.As<T, decimal>(ref rhs);
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'GreaterThanOrEquals' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                return Unsafe.As<T, char>(ref lhs) >= Unsafe.As<T, char>(ref rhs);
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'GreaterThanOrEquals' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.GreaterThanOrEquals(lhs, rhs);
        }


        /// <summary>
        ///     Cast from <see href="long"/> value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FromLong<T>(long value)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'FromLong' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)value;
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)value;
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)value;
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)value;
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)value;
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)value;
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)value;
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)value;
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                var retval = (float)value;
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = (double)value;
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = (decimal)value;
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                var retval = (Complex)value;
                return Unsafe.As<Complex, T>(ref retval);
            }
            if(typeof(T) == typeof(char))
            {
                var retval = (char)value;
                return Unsafe.As<char, T>(ref retval);
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'FromLong' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.FromLong(value);
        }


        /// <summary>
        ///     Cast from <see href="double"/> value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FromDouble<T>(double value)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'FromDouble' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = (sbyte)value;
                return Unsafe.As<sbyte, T>(ref retval);
            }
            if(typeof(T) == typeof(short))
            {
                var retval = (short)value;
                return Unsafe.As<short, T>(ref retval);
            }
            if(typeof(T) == typeof(int))
            {
                var retval = (int)value;
                return Unsafe.As<int, T>(ref retval);
            }
            if(typeof(T) == typeof(long))
            {
                var retval = (long)value;
                return Unsafe.As<long, T>(ref retval);
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = (byte)value;
                return Unsafe.As<byte, T>(ref retval);
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = (ushort)value;
                return Unsafe.As<ushort, T>(ref retval);
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = (uint)value;
                return Unsafe.As<uint, T>(ref retval);
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = (ulong)value;
                return Unsafe.As<ulong, T>(ref retval);
            }
            if(typeof(T) == typeof(float))
            {
                var retval = (float)value;
                return Unsafe.As<float, T>(ref retval);
            }
            if(typeof(T) == typeof(double))
            {
                var retval = (double)value;
                return Unsafe.As<double, T>(ref retval);
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = (decimal)value;
                return Unsafe.As<decimal, T>(ref retval);
            }
            if(typeof(T) == typeof(Complex))
            {
                var retval = (Complex)value;
                return Unsafe.As<Complex, T>(ref retval);
            }
            if(typeof(T) == typeof(char))
            {
                var retval = (char)value;
                return Unsafe.As<char, T>(ref retval);
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'FromDouble' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.FromDouble(value);
        }


        /// <summary>
        ///     Cast to <see href="long"/> value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ToLong<T>(T value)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'ToLong' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = Unsafe.As<T, sbyte>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(short))
            {
                var retval = Unsafe.As<T, short>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(int))
            {
                var retval = Unsafe.As<T, int>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(long))
            {
                var retval = Unsafe.As<T, long>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = Unsafe.As<T, byte>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = Unsafe.As<T, ushort>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = Unsafe.As<T, uint>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = Unsafe.As<T, ulong>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(float))
            {
                var retval = Unsafe.As<T, float>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(double))
            {
                var retval = Unsafe.As<T, double>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = Unsafe.As<T, decimal>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'ToLong' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                var retval = Unsafe.As<T, char>(ref value);
                return (long)retval;
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'ToLong' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.ToLong(value);
        }


        /// <summary>
        ///     Cast to <see href="double"/> value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToDouble<T>(T value)
        {
            if(typeof(T) == typeof(bool))
            {
                throw new NotSupportedException("The operation 'ToDouble' for type 'bool' is not supported.");
            }
            if(typeof(T) == typeof(sbyte))
            {
                var retval = Unsafe.As<T, sbyte>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(short))
            {
                var retval = Unsafe.As<T, short>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(int))
            {
                var retval = Unsafe.As<T, int>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(long))
            {
                var retval = Unsafe.As<T, long>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(byte))
            {
                var retval = Unsafe.As<T, byte>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(ushort))
            {
                var retval = Unsafe.As<T, ushort>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(uint))
            {
                var retval = Unsafe.As<T, uint>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(ulong))
            {
                var retval = Unsafe.As<T, ulong>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(float))
            {
                var retval = Unsafe.As<T, float>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(double))
            {
                var retval = Unsafe.As<T, double>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(decimal))
            {
                var retval = Unsafe.As<T, decimal>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(Complex))
            {
                throw new NotSupportedException("The operation 'ToDouble' for type 'Complex' is not supported.");
            }
            if(typeof(T) == typeof(char))
            {
                var retval = Unsafe.As<T, char>(ref value);
                return (double)retval;
            }
            if(typeof(T) == typeof(string))
            {
                throw new NotSupportedException("The operation 'ToDouble' for type 'string' is not supported.");
            }

            return Cache<T>.Trait.ToDouble(value);
        }



    }
}

