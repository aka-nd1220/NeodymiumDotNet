using System;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Extension class for <see cref="IValueTrait{T}"/>.
    /// </summary>
    public static partial class ValueTrait
    {
        /// <summary>
        ///     Provides operator calculation for custom type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private static class Cache<T>
        {

            internal static IValueTrait<T> Trait
            {
                get
                {
                    var trait = _Trait;
                    Guard.AssertOperation(trait != null,
                                          $"There is no trait for {typeof(T)}.");
                    return trait!;
                }
                set => _Trait = value;
            }


            private static IValueTrait<T> _Trait = default!;

        }


        /// <summary>
        ///     Defines value trait strategy for global system.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="trait"></param>
        public static void RegisterTrait<T>(IValueTrait<T> trait)
            => Cache<T>.Trait = trait;

    }


    /// <summary>
    ///     <para> Operator calculation interface. </para>
    ///     <para> When this interface will be implemented, the following rules must be adhered. </para>
    ///     <para> - The implementation has only a constructor which has no arguments. </para>
    ///     <para> - All of the implemented operations are pure. </para>
    ///     <para>
    ///            - If the implementation does not support the operation, the method throws
    ///              <see cref="NotSupportedException"/> immediately for any arguments.
    ///     </para>
    ///     <para>
    ///            - If the operation will fail for the input arguments, the method throws
    ///              <see cref="ArithmeticException"/> (including <see cref="OverflowException"/>).
    ///     </para>
    ///     <para>
    ///            - If <typeparamref name="T"/> is nullable and some arguments are <c>null</c>,
    ///              the method can throw <see cref="NullReferenceException"/> (not must).
    ///     </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValueTrait<T>
    {

        /// <summary>
        ///     Returns <c>true</c> if <typeparamref name="T"/> supports <c>0</c> and <c>1</c>;
        ///     otherwise <c>false</c>.
        /// </summary>
        bool IsNumber();


        /// <summary>
        ///     Gets zero value of <typeparamref name="T"/>.
        /// </summary>
        T Zero();


        /// <summary>
        ///     Gets one value of <typeparamref name="T"/>.
        /// </summary>
        T One();


        /// <summary>
        ///     [Pure] Unary plus operation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T UnaryPlus(T value);


        /// <summary>
        ///     [Pure] Unary negation operation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T UnaryNegate(T value);


        /// <summary>
        ///     [Pure] Logical-not operation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T Not(T value);


        /// <summary>
        ///     [Pure] Complement operation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T Complement(T value);


        /// <summary>
        ///     [Pure] Mathematical add operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T Add(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Mathematical subtract operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T Subtract(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Mathematical multiply operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T Multiply(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Mathematical divide operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T Divide(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Mathematical modulo operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T Modulo(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Bitwise-add operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T BitwiseAnd(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Bitwise-or operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T BitwiseOr(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Bitwise-xor operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T BitwiseXor(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Numerical increment operation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T Increment(T value);


        /// <summary>
        ///     [Pure] Numerical decrement operation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T Decrement(T value);


        /// <summary>
        ///     [Pure] Bit shift to left operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T ShiftLeft(T lhs, int rhs);


        /// <summary>
        ///     [Pure] Bit shift to right operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        T ShiftRight(T lhs, int rhs);


        /// <summary>
        ///     [Pure] Object equality operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        bool Equals(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Object not-equality operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        bool NotEquals(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Level comparison operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        bool LessThan(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Level comparison operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        bool LessThanOrEquals(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Level comparison operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        bool GreaterThan(T lhs, T rhs);


        /// <summary>
        ///     [Pure] Level comparison operation.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T" /> is not supported. </exception>
        /// <exception cref="ArithmeticException"> The calculation for the arguments was failed mathematically. </exception>
        /// <exception cref="NullReferenceException"> <typeparamref name="T"/> is nullable, and some arguments are null. </exception>
        bool GreaterThanOrEquals(T lhs, T rhs);


        /// <summary>
        ///     Casts from long value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        T FromLong(long value);


        /// <summary>
        ///     Casts from double value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        T FromDouble(double value);


        /// <summary>
        ///     Casts to long value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        long ToLong(T value);


        /// <summary>
        ///     Casts to double value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        double ToDouble(T value);

    }
}
